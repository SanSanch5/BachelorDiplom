using System.Collections.Generic;
using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public partial class NamesakesForm : Form
    {
        private List<string> _numbers;
        public string Number
        {
            get { return cmbNumbers.Text; }
        }

        public NamesakesForm(List<string> nums)
        {
            InitializeComponent();
            _numbers = nums;

            foreach (var num in nums)
                cmbNumbers.Items.Add(num);
        }

        private void cmbNumbers_KeyUp(object sender, KeyEventArgs e)
        {
            if (!_numbers.Contains(cmbNumbers.Text))
                btnOK.Enabled = false;
            else
                btnOK.Enabled = true;
        }
    }
}
