using System.Configuration;

namespace CZ
{
    /// <summary>
    /// Demonstrates how to use the Xml Deserialization technique.
    /// Use <code>PaymentSettings.Current</code> to get read the settings.
    /// </summary>
    public class PaymentSettings : XmlDeserializeConfigSectionHandler
    {
        public static readonly PaymentSettings Current = (PaymentSettings)ConfigurationManager.GetSection("PaymentSettings");

        public string PaymentSystem { get; set; }
        public string URL { get; set; }
        public int Timeout { get; set; }
    }
}
