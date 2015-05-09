using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public partial class NamesakesBox : Form
    {
        public NamesakesBox(string ln, string n, string mn)
        {
            InitializeComponent();
            lblName.Text = ln + " " + n + " " + mn;
        }
    }
}
