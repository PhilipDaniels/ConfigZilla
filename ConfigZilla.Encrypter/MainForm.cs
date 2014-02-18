using System;
using System.IO;
using System.Windows.Forms;

namespace ConfigZilla.Encrypter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Text = Properties.Resources.AppTitle;
            LoadLastUsed();
        }

        private void btnReadSection_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;
                var scp = new SimpleConfigParser(TheFile, TheSection);
                ReadSectionAndDisplay(scp);
                SaveLastUsed();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;
                var scp = new SimpleConfigParser(TheFile, TheSection);
                ConfigurationEncrypter.MakeBackups(scp);
                ConfigurationEncrypter.EncryptAndSave(TheFile, TheSection);
                // Reread!
                scp = new SimpleConfigParser(TheFile, TheSection);
                ReadSectionAndDisplay(scp);
                SaveLastUsed();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;
                var scp = new SimpleConfigParser(TheFile, TheSection);
                ConfigurationEncrypter.MakeBackups(scp);
                ConfigurationEncrypter.DecryptAndSave(TheFile, TheSection);
                // Reread!
                scp = new SimpleConfigParser(TheFile, TheSection);
                ReadSectionAndDisplay(scp);
                SaveLastUsed();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.DefaultExt = ".config";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFile.Text = dlg.FileName;
            }
        }

        bool ValidateInputs()
        {
            if (String.IsNullOrWhiteSpace(TheFile))
            {
                MessageBox.Show("Please specify the file.", Properties.Resources.AppTitle + " Error");
                return false;
            }
            if (!File.Exists(TheFile))
            {
                MessageBox.Show("File does not exist.", Properties.Resources.AppTitle + " Error");
                return false;
            }
            if (String.IsNullOrWhiteSpace(TheSection))
            {
                MessageBox.Show("Please specify the section.", Properties.Resources.AppTitle + " Error");
                return false;
            }

            return true;
        }

        private void ReadSectionAndDisplay(SimpleConfigParser scp)
        {
            // The left hand side.
            txtMainOnDisk.Clear();
            if (radEntireFile.Checked)
            {
                txtMainOnDisk.AppendText(scp.MainFileContents);
            }
            else
            {
                txtMainOnDisk.AppendText(scp.MainFileSectionContents);
            }

            txtSubFileOnDisk.Clear();
            if (String.IsNullOrEmpty(scp.SubFileName))
            {
                tabSubFile.HidePage();
            }
            else
            {
                tabSubFile.ShowPageInTabControl(tabFiles);
                txtSubFileOnDisk.AppendText(scp.SubFileContents);
            }


            // The right hand sode.
            var sec = ConfigurationEncrypter.GetSection(TheFile, TheSection);
            var secString = sec.GetReadableString();
            txtDecrypted.Clear();
            txtDecrypted.AppendText(secString);
        }



        string TheFile
        {
            get { return txtFile.Text.Trim(); }
        }

        string TheSection
        {
            get { return txtSection.Text.Trim(); }
        }

        void DisplayException(Exception ex)
        {
            using (var f = new ExceptionForm())
            {
                f.Exception = ex;
                f.ShowDialog();
            }
        }

        void LoadLastUsed()
        {
            txtFile.Text = Properties.Settings.Default.LastUsedFile;
            txtSection.Text = Properties.Settings.Default.LastUsedSection;
        }

        void SaveLastUsed()
        {
            Properties.Settings.Default.LastUsedFile = txtFile.Text.Trim();
            Properties.Settings.Default.LastUsedSection = txtSection.Text.Trim();
            Properties.Settings.Default.Save();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            using (var f = new HelpForm())
            {
                f.ShowDialog();
            }
        }

        private void txtFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void txtFile_DragDrop(object sender, DragEventArgs e)
        {
            // Won't work unless you can find a way to run explorer as admin. Sigh.
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null)
            {
                txtFile.Text = files[0].Trim();
            }
        }
    }
}
