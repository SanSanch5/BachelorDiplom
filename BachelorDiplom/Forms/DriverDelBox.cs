using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public partial class DriverDelBox : Form
    {
        public DriverDelBox(string fullName)
        {
            InitializeComponent();
            lblName.Text = fullName;
        }
    }
}
