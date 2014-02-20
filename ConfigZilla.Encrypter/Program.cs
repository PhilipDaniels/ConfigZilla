using System;
using System.Linq;
using System.Windows.Forms;

namespace ConfigZilla.Encrypter
{
    static class Program
    {
        static GUIConsoleWriter theConsole;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                RunCommandLine(args);
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                Application.Exit();
            }
        }

        static void ShowUsage()
        {
            theConsole.WriteLine();
            theConsole.WriteLine("  -r section file     read the section and display in plain text");
            theConsole.WriteLine("  -e section file     encrypt the section");
            theConsole.WriteLine("  -d section file     decrypt the section");
            theConsole.WriteLine("  -h                  display this message");
            theConsole.WriteLine();
            theConsole.WriteLine("Example:");
            theConsole.WriteLine(@"  Config.Encrypter.exe -e connectionStrings c:\temp\web.config");
        }

        private static void RunCommandLine(string[] args)
        {
            theConsole = new GUIConsoleWriter();

            var validArgs = new string[] { "-r", "-e", "-d", "-h" };
            string first = args[0].ToLowerInvariant();
            string section = args[1];
            string file = args[2];

            switch (first)
            {
                case "-r":
                    if (args.Count() != 3)
                    {
                        ShowUsage();
                        return;
                    }
                    ReadSection(file, section);
                    break;
                case "-e":
                    if (args.Count() != 3)
                    {
                        ShowUsage();
                        return;
                    }
                    EncryptSection(file, section);
                    break;
                case "-d":
                    if (args.Count() != 3)
                    {
                        ShowUsage();
                        return;
                    }
                    DecryptSection(file, section);
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        static void ReadSection(string file, string section)
        {
            var scp = new SimpleConfigParser(file, section);

            theConsole.WriteLine();
            theConsole.Underline("Main file " + scp.MainFileName + ":");
            theConsole.WriteLine(scp.MainFileSectionContents);   // scp.MainFileSectionContents or MainFileContents

            if (!String.IsNullOrEmpty(scp.SubFileName))
            {
                theConsole.WriteLine();
                theConsole.Underline("Sub-file " + scp.SubFileName + ":");
                theConsole.WriteLine(scp.SubFileContents);
            }

            var sec = ConfigurationEncrypter.GetSection(file, section);
            if (sec.SectionInformation.IsProtected)
            {
                theConsole.WriteLine();
                theConsole.Underline("Decrypted:");
                theConsole.WriteLine(sec.GetReadableString());
            }
            else
            {
                theConsole.WriteLine();
                theConsole.WriteLine("The section " + section + " in file " + file + " is not encrypted.");
            }
        }

        static void EncryptSection(string file, string section)
        {
            var scp = new SimpleConfigParser(file, section);
            ConfigurationEncrypter.MakeBackups(scp);
            ConfigurationEncrypter.EncryptAndSave(file, section);

            theConsole.WriteLine();
            theConsole.WriteLine("Encryption successful. Showing current configuration.");
            ReadSection(file, section);
        }

        static void DecryptSection(string file, string section)
        {
            var scp = new SimpleConfigParser(file, section);
            ConfigurationEncrypter.MakeBackups(scp);
            ConfigurationEncrypter.DecryptAndSave(file, section);

            theConsole.WriteLine();
            theConsole.WriteLine("Decryption successful. Showing current configuration.");
            ReadSection(file, section);
        }
    }
}
