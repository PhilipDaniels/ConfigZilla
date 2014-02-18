using System;
using System.IO;

namespace ConfigZilla.Encrypter
{
    /// <summary>
    /// Simple class to parse a .config file. Intended for display purposes
    /// for this app only.
    /// </summary>
    public class SimpleConfigParser
    {
        public string MainFileName { get; private set; }
        public string MainFileContents { get; private set; }
        public string MainFileSectionContents { get; private set; }
        public string SectionName { get; private set; }
        public string SubFileName { get; private set; }
        public string SubFileContents { get; private set; }

        public SimpleConfigParser(string mainFileName, string sectionName)
        {
            if (String.IsNullOrWhiteSpace(mainFileName))
            {
                throw new ArgumentNullException("mainFileName");
            }
            if (!File.Exists(mainFileName))
            {
                throw new FileNotFoundException("The file '" + mainFileName + "' does not exist.");
            }

            MainFileName = mainFileName;
            MainFileContents = File.ReadAllText(mainFileName);
            SectionName = sectionName;

            if (!String.IsNullOrWhiteSpace(SectionName))
            {
                ParseMainFileSectionContents();
                ParseSubFile();
            }
        }

        void ParseMainFileSectionContents()
        {
            string startTag = "<" + SectionName;
            var idx1 = MainFileContents.IndexOf(startTag, StringComparison.OrdinalIgnoreCase);

            string endTag = "</" + SectionName + ">";
            int idx2 = MainFileContents.IndexOf(endTag, idx1, StringComparison.OrdinalIgnoreCase);
            if (idx2 == -1)
            {
                // It's a self-closing tag.
                endTag = "/>";
                idx2 = MainFileContents.IndexOf(endTag, idx1, StringComparison.OrdinalIgnoreCase);
            }

            idx2 += endTag.Length;
            MainFileSectionContents = MainFileContents.Substring(idx1, idx2 - idx1);
        }

        void ParseSubFile()
        {
            const string attr = "configSource";
            if (String.IsNullOrWhiteSpace(MainFileSectionContents))
            {
                return;
            }

            int idx1 = MainFileSectionContents.IndexOf(attr, StringComparison.OrdinalIgnoreCase);
            if (idx1 == -1)
            {
                // There is no sub file.
                SubFileName = null;
                SubFileContents = null;
            }
            else
            {
                idx1 = MainFileSectionContents.IndexOf("\"", idx1, StringComparison.OrdinalIgnoreCase) + 1;
                int idx2 = MainFileSectionContents.IndexOf("\"", idx1);
                SubFileName = MainFileSectionContents.Substring(idx1, idx2 - idx1);
                SubFileName = Path.Combine(Path.GetDirectoryName(MainFileName), SubFileName);
                SubFileContents = File.ReadAllText(SubFileName);
            }
        }
    }
}
