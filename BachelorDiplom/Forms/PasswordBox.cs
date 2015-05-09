using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public partial class PasswordBox : Form
    {
        public string Password
        {
            get { return edtPassword.Text; }
        }

        public PasswordBox()
        {
            InitializeComponent();
            edtPassword.PasswordChar = '*';
        }
    }
}
