using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSeparateConfig
{
    class Program
    {
        static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var name = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name;
            var underline = new String('=', name.Length);
            Console.WriteLine(name);
            Console.WriteLine(underline);

            log.Info(name);
            log.Info(underline);

            Console.WriteLine("Setting1 = " + CZ.AppSettings.Setting1);
            Console.WriteLine("Setting2 = " + CZ.AppSettings.Setting2);
            Console.WriteLine();
            Console.WriteLine("ConnStr1 = " + CZ.ConnStrings.ConnStr1);
            Console.WriteLine("ConnStr2 = " + CZ.ConnStrings.ConnStr2);
            Console.WriteLine();
            Console.WriteLine("Reporting.PageSize = " + CZ.ReportingSettings.Current.PageSize.ToString());
            Console.WriteLine("Reporting.Server = " + CZ.ReportingSettings.Current.Server);
            Console.WriteLine("Reporting.RecipientEmail = " + CZ.ReportingSettings.Current.RecipientEmail);
            Console.Read();
        }
    }
}
