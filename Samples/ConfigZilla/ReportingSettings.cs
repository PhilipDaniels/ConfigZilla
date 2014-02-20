using System.Configuration;

namespace CZ
{
    /// <summary>
    /// Demonstrates the simplest form of custom configuration section, a read-only set of properties.
    /// See http://www.codeproject.com/Articles/16466/Unraveling-the-Mysteries-of-NET-2-0-Configuration
    /// for lots more details. Many other tutorials on the 'net.
    /// This allows you to create a section where each property is expressed as an attribute:
    /// &lt;reportingSettings PageSize="11" Server="http://Debug.example.com" RecipientEmail="phil@Debug.example.com" /&gt;
    /// For the style where each attribute is a node, try subclassing <code>XmlDeserializeConfigSectionHandler</code>.
    /// </summary>
    public class ReportingSettings : ConfigurationSection
    {
        // The string is the name as it must appear in the .config file.
        public static readonly ReportingSettings Current = (ReportingSettings)ConfigurationManager.GetSection("reportingSettings");

        [ConfigurationProperty("PageSize", DefaultValue = "20")]
        public int PageSize
        {
            get { return (int)base["PageSize"]; }
        }

        [ConfigurationProperty("Server", DefaultValue = "http://example.com/ReportRequest")]
        public string Server
        {
            get { return (string)base["Server"]; }
        }

        [ConfigurationProperty("RecipientEmail", DefaultValue = "phil@example.com")]
        public string RecipientEmail
        {
            get { return (string)base["RecipientEmail"]; }
        }
    }
}
