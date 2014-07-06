using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConfigZilla.Tasks
{
    /// <summary>
    /// This task merges all the individual .xslt files specified in <code>XsltFiles</code>
    /// and writes out a single .xslt file that will perform all the transformations.
    /// </summary>
    public class CreateMergedXslt : TaskWithProperties
    {
        /// <summary>
        /// The name of the file to create.
        /// </summary>
        [Required]
        public string MergedXsltFileName { get; set; }

        [Required]
        public string LogLevel { get; set; }

        [Required]
        public ITaskItem[] XsltFiles { get; set; }

        public string Comment1 { get; set; }
        public string Comment2 { get; set; }
        public string Comment3 { get; set; }

        MessageImportance LoggingLevel;
        int NumPropertiesReplaced;
        List<string> MissedProperties;

        public override bool Execute()
        {
            DetermineLoggingLevel();
            Log.LogMessage(LoggingLevel, "Creating merged Xslt file {0}", MergedXsltFileName);
            DumpProperties(LoggingLevel);

            MissedProperties = new List<string>();

            // File.Delete is supposed to not throw an exception if the file does not exist,
            // yet I seemed to have a case where it did, when I had manually deleted the
            // bin folder from a project. Issue 1.
            if (File.Exists(MergedXsltFileName))
            {
                File.Delete(MergedXsltFileName);
            }
            string contents = GetMergedFile();
            WriteNewFile(contents);

            Log.LogMessage
                (
                LoggingLevel,
                "Merged Xslt file created, {0} properties replaced in {1} xslt files, {2} characters written.",
                NumPropertiesReplaced, XsltFiles.Count(), contents.Length
                );

            if (MissedProperties.Count > 0)
            {
                MissedProperties.Sort();
                Log.LogWarning("No replacements found for these properties:");
                foreach (string msg in MissedProperties)
                {
                    Log.LogWarning("  " + msg);
                }
            }

            return true;
        }

        void WriteNewFile(string contents)
        {
            string dir = Path.GetDirectoryName(MergedXsltFileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(MergedXsltFileName, contents);
        }

        string GetMergedFile()
        {
            var sb = new StringBuilder(4096);

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:msxsl=\"urn:schemas-microsoft-com:xslt\" exclude-result-prefixes=\"msxsl\">");
            sb.AppendLine("<xsl:output method=\"xml\" indent=\"yes\" omit-xml-declaration=\"yes\" encoding=\"UTF-8\"/>");
            sb.AppendLine();
            sb.AppendLine("  <!-- Default 'Identity' template copies all input to output -->");
            sb.AppendLine("  <xsl:template match=\"node()|@*\">");
            sb.AppendLine("    <xsl:copy>");
            sb.AppendLine("      <xsl:apply-templates select=\"node()|@*\"/>");
            sb.AppendLine("    </xsl:copy>");
            sb.AppendLine("  </xsl:template>");
            sb.AppendLine();

            WriteComment(sb);

            var files = GetSortedItems(XsltFiles);
            foreach (string file in files)
            {
                sb.AppendFormat("  <!-- Transforms from {0} -->{1}", file, Environment.NewLine);
                sb.Append("  ");
                string body = LoadXsltFile(file, Properties);
                sb.AppendLine(body);
                sb.AppendLine();
            }

            sb.AppendLine("</xsl:stylesheet>");

            return sb.ToString();
        }

        void WriteComment(StringBuilder sb)
        {
            if (String.IsNullOrWhiteSpace(Comment1) &&
                String.IsNullOrWhiteSpace(Comment2) &&
                String.IsNullOrWhiteSpace(Comment3))
            {
                return;
            }

            sb.AppendLine("  <!-- This template stamps the transformed config file with a comment.");
            sb.AppendLine("       The default says who/when/where the file was generated. -->");
            sb.AppendLine("  <xsl:template match=\"/\">");

            if (!String.IsNullOrWhiteSpace(Comment1))
            {
                sb.AppendFormat("    <xsl:comment>{0}</xsl:comment>{1}", Comment1, Environment.NewLine);
            }
            if (!String.IsNullOrWhiteSpace(Comment2))
            {
                sb.AppendFormat("    <xsl:comment>{0}</xsl:comment>{1}", Comment2, Environment.NewLine);
            }
            if (!String.IsNullOrWhiteSpace(Comment3))
            {
                sb.AppendFormat("    <xsl:comment>{0}</xsl:comment>{1}", Comment3, Environment.NewLine);
            }

            sb.AppendLine("    <xsl:apply-templates />");
            sb.AppendLine("  </xsl:template>");
            sb.AppendLine();
        }

        static Regex TemplateVariableRegex = new Regex
            (
            @"(?<!\$\$)" +      // Do not match $(var) when it is actually $$$(var)
            @"\$\((.*?)\)",     // match $(var)
            RegexOptions.Multiline | RegexOptions.Compiled
            );

        string LoadXsltFile(string file, IDictionary<string, string> properties)
        {
            string input = File.ReadAllText(file);
            if (String.IsNullOrWhiteSpace(input))
                return "";

            input = StripXsltFluff(input);
            if (properties == null || properties.Count == 0)
            {
                return input;
            }

            string replacedText = TemplateVariableRegex.Replace(input, m => PropertyReplacer(m, file, properties));
            return replacedText;
        }

        string PropertyReplacer(Match match, string file, IDictionary<string, string> properties)
        {
            // Matches come in the form "$(asSetting1)"
            string propertyName = match.Value.Substring(2, match.Value.Length - 3);
            string message = String.Format("Testing for property {0} in file {1}...", propertyName, file);

            string replacement;
            if (properties.TryGetValue(propertyName, out replacement))
            {
                message += "replacing with value " + replacement;
                Log.LogMessage(LoggingLevel, message);
                NumPropertiesReplaced++;
                return replacement;
            }
            else
            {
                message += "no replacement found.";
                Log.LogMessage(LoggingLevel, message);
                string missedMessage = String.Format("$({0}) in file {1}", propertyName, file);
                if (!MissedProperties.Contains(missedMessage))
                {
                    MissedProperties.Add(missedMessage);
                }
                return match.Value;
            }
        }

        string StripXsltFluff(string input)
        {
            // Get rid of the opening xml declaration.
            input = Regex.Replace(input, @"\<\?xml.*?\?\>", "");

            // Get rid of the xsl:output clause.
            input = Regex.Replace(input, @"\<xsl:output.*?\/\>", "");

            // Get rid of the stylesheet clause and its ending tag.
            input = Regex.Replace(input, @"\<xsl\:stylesheet.*?\>", "");
            input = Regex.Replace(input, @"\<\/xsl\:stylesheet\>", "");

            return input.Trim(); ;
        }

        void DetermineLoggingLevel()
        {
            switch (LogLevel.ToUpperInvariant())
            {
                case "LOW":
                    LoggingLevel = MessageImportance.Low;
                    break;
                case "NORMAL":
                    LoggingLevel = MessageImportance.Normal;
                    break;
                case "HIGH":
                    LoggingLevel = MessageImportance.High;
                    break;
                default:
                    LoggingLevel = MessageImportance.Normal;
                    break;
            }
        }

        List<string> GetSortedItems(ITaskItem[] items)
        {
            var files = new List<string>();
            files.AddRange(items.Select(iv => iv.GetMetadata("FullPath")));
            files.Sort();
            return files;
        }
    }
}
