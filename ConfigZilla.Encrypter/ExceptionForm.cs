using System;
using System.Windows.Forms;

namespace ConfigZilla.Encrypter
{
    public partial class ExceptionForm : Form
    {
        public Exception Exception { get; set; }

        public ExceptionForm()
        {
            InitializeComponent();
            Text = Properties.Resources.AppTitle + " - Exception";
        }

        private void ExceptionForm_Shown(object sender, EventArgs e)
        {
            if (Exception != null)
            {
                txtException.Text = Exception.ToString();
            }
        }
    }
}
