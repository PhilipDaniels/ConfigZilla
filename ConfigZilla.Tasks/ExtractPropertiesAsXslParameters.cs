using Microsoft.Build.Framework;
using System;
using System.Linq;
using System.Text;

namespace ConfigZilla.Tasks
{
    /// <summary>
    /// Gather all the properties in a project and make them available as an XslParameter
    /// string. This allows per-project customisation in the Xslt file.
    /// This is something of a hack to get around the fact that only a single Xslt file
    /// is generated, so per-project customisations of the generated config files
    /// are not possible unless some sort of run-time variable mechanism (i.e. Xsl parameters)
    /// is used.
    /// </summary>
    public class ExtractPropertiesAsXslParameters : TaskWithProperties
    {
        [Output]
        public string XslParameterString { get; set; }

        public string Prefix { get; set; }

        public override bool Execute()
        {
            string prefix = String.IsNullOrWhiteSpace(Prefix) ? "czp" : Prefix.Trim();

            var sb = new StringBuilder();

            foreach (var name in Properties.Keys.OrderBy(n => n))
            {
                string value = Properties[name];
                // Blanks are irrelevant and newlines lead to breakage.
                if (String.IsNullOrWhiteSpace(value) || value.Contains(Environment.NewLine))
                {
                    continue;
                }
                
                // Deal with embedded special chars such as <, etc.
                value = System.Security.SecurityElement.Escape(value);

                // Spaces are not allowed in Xsl parameter names. Just in case anybody ever
                // sneaks one into MSBuild, don't crash...
                string n = name.Replace(" ", "");

                sb.AppendFormat("<Parameter Name='{0}{1}' Value='{2}' />", prefix, n, value);
                sb.AppendLine();
            }

            XslParameterString = sb.ToString();

            return true;
        }
    }
}
