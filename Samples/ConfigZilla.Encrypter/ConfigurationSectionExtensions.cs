using System.Configuration;

namespace ConfigZilla.Encrypter
{
    public static class ConfigurationSectionExtensions
    {
        public static string GetReadableString(this ConfigurationSection section)
        {
            string s = section.SectionInformation.GetRawXml();
            // Hmm.
            if (s.Contains("\n") && !s.Contains("\r\n"))
            {
                s = s.Replace("\n", "\r\n");
            }

            return s;
        }
    }
}
