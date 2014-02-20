using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSingleConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name;
            Console.WriteLine(name);
            Console.WriteLine(new String('=', name.Length));

            Console.WriteLine("Setting1 = " + CZ.AppSettings.Setting1);
            Console.WriteLine("Setting2 = " + CZ.AppSettings.Setting2);
            Console.WriteLine();
            Console.WriteLine("ConnStr1 = " + CZ.ConnStrings.ConnStr1);
            Console.WriteLine("ConnStr2 = " + CZ.ConnStrings.ConnStr2);
            Console.WriteLine();
            Console.WriteLine("PaymentSettings.PaymentSystem = " + CZ.PaymentSettings.Current.PaymentSystem);
            Console.WriteLine("PaymentSettings.URL = " + CZ.PaymentSettings.Current.URL);
            Console.WriteLine("PaymentSettings.Timeout = " + CZ.PaymentSettings.Current.Timeout);
            Console.WriteLine();
            Console.WriteLine("Reporting.PageSize = " + CZ.ReportingSettings.Current.PageSize.ToString());
            Console.WriteLine("Reporting.Server = " + CZ.ReportingSettings.Current.Server);
            Console.WriteLine("Reporting.RecipientEmail = " + CZ.ReportingSettings.Current.RecipientEmail);
            Console.WriteLine();
            Console.Read();
        }
    }
}
