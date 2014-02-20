using System;
using System.Windows.Forms;

namespace ConfigZilla.Encrypter
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            Text = Properties.Resources.AppTitle;
            lblHelp1.Text = Help1;
            //lblHelp2.Text = Properties.Resources.Help2;
            //lblHelp3.Text = Properties.Resources.Help3;
            //lblHelp4.Text = Properties.Resources.Help4;
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        const string Help1 =
@"
For maximum security settings such as connection strings should be encrypted.The encryption uses the <machineKey> as the private key so it cannot be done at publish time, you must do it on the deployment machine as a post-installation step.

Copy this application up to your server and then use it to pick the files and sections to encrypt. The program can also be used to display the plain-text of encrypted sections. A command line mode is available to enable usage in automatic deployment scenarios.

If you have created custom sections such as 'reportingSettings' then they will not be loadable unless you make the type that implements them available to Config.Encrypter. Copy the DLL that contains the type into the EXE's folder and restart.

Not every section of a config file can be encrypted, but you may be able to encrypt individual sub-sections or settings. Test your app.

If you are using separate 'sub-files' to store your settings you should still pick the main web.config or app.config file in the GUI. The config system is smart enough to understand that your settings are in a separate file and will encrypt the sub-file.
";
    }
}
