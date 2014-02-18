using System;
using System.Configuration;
using System.IO;

namespace ConfigZilla.Encrypter
{
    /// <summary>
    /// This is the class that actually does the donkey work of encrypting and decryping sections.
    /// </summary>
    public static class ConfigurationEncrypter
    {
        enum Operation
        {
            Encrypt,
            Decrypt
        }

        public const string RSAProvider = "RSAProtectedConfigurationProvider";
        //public const string RSAProvider = "DPAPIProtectedConfigurationProvider";



        public static void EncryptAndSave(string file, string section)
        {
            InternalEncryptOrDecrypt(RSAProvider, file, section, Operation.Encrypt);
        }

        public static void EncryptAndSave(string provider, string file, string section)
        {
            InternalEncryptOrDecrypt(provider, file, section, Operation.Encrypt);
        }

        public static void DecryptAndSave(string provider, string file, string section)
        {
            InternalEncryptOrDecrypt(provider, file, section, Operation.Decrypt);
        }

        public static void DecryptAndSave(string file, string section)
        {
            InternalEncryptOrDecrypt(RSAProvider, file, section, Operation.Decrypt);
        }

        public static ConfigurationSection GetSection(string file, string section)
        {
            return GetSection(RSAProvider, file, section);
        }

        public static ConfigurationSection GetSection(string provider, string file, string section)
        {
            if (String.IsNullOrWhiteSpace(provider))
            {
                throw new ArgumentException("Please specify the provider. The default of \"RSAProtectedConfigurationProvider\" is usually ok.");
            }
            if (String.IsNullOrWhiteSpace(file))
            {
                throw new ArgumentException("File must be specified.");
            }
            if (String.IsNullOrEmpty(section))
            {
                throw new ArgumentException("Section must be specified.");
            }
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("The file '" + file + "' does not exist.");
            }

            /*
            var fileMap = new ExeConfigurationFileMap(file);
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var sec = configuration.GetSection(section);
            if (sec == null)
            {
                throw new Exception("Could not load the section '" + section + "'. Check that it exists.");
            }
            */

            var fileMap = new ConfigurationFileMap(file);
            var configuration = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            var sec = configuration.GetSection(section);
            if (sec == null)
            {
                throw new Exception("Could not load the section '" + section + "'. Check that it exists.");
            }

            return sec;
        }


        static void InternalEncryptOrDecrypt(string provider, string file, string section, Operation operation)
        {
            //string tempDir = Path.GetDirectoryName(TempFileName);
            //Directory.CreateDirectory(tempDir);
            //File.Copy(file, TempFileName, true);

            var sec = GetSection(provider, file, section);

            if (operation == Operation.Encrypt)
            {
                if (!sec.SectionInformation.IsProtected && !sec.SectionInformation.IsLocked)
                {
                    sec.SectionInformation.ProtectSection(provider);

                    // So work around is to call aspnet_regiis to do it for us...
                    //string args = "-pef \"" + section + "\" " + tempDir;
                    //CallAspNetRegIIS(args);
                }
            }
            else
            {
                if (sec.SectionInformation.IsProtected && !sec.SectionInformation.IsLocked)
                {
                    sec.SectionInformation.UnprotectSection();
                    //string args = "-pdf \"" + section + "\" " + tempDir;
                    //CallAspNetRegIIS(args);
                }
            }

            sec.SectionInformation.ForceSave = true;
            sec.CurrentConfiguration.Save();
        }

        public static void BackupFile(string file)
        {
            string backupName = String.Format("{0}.{1:dd.MMM.yyyy.HH.mm.ss}.bak", file, DateTime.Now);
            File.Copy(file, backupName, true);
        }

        public static void MakeBackups(SimpleConfigParser scp)
        {
            BackupFile(scp.MainFileName);
            if (!String.IsNullOrEmpty(scp.SubFileName))
            {
                BackupFile(scp.SubFileName);
            }
        }

        public static string FrameworkDirectory
        {
            get
            {
                return System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
            }
        }

        public static string RegIISPath
        {
            get
            {
                return Path.Combine(FrameworkDirectory, "aspnet_regiis.exe");
            }
        }

        public static string TempFileName
        {
            get
            {
                return Path.Combine(ExeFolder, @"InternalTempFolder\web.config");
            }
        }

        public static string ExePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string ExeFolder
        {
            get
            {
                return Path.GetDirectoryName(ExePath);
            }
        }

        /*
        public void CallAspNetRegIIS(string args)
        {
            ProcessStartInfo psi = new ProcessStartInfo(RegIISPath, args);
            psi.CreateNoWindow = true;
            Process proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
            proc.WaitForExit();
        }
        */
    }
}
