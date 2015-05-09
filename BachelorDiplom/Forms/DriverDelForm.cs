using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BachelorLibAPI.Program;

namespace BachelorLibAPI.Forms
{
    public partial class DriverDelForm : Form
    {
        private QueriesHandler _queriesHandler;
        private List<string> _numbers;

        public DriverDelForm(QueriesHandler qh)
        {
            InitializeComponent();
            _queriesHandler = qh;
            _numbers = _queriesHandler.GetNumbers();

            cmbNumbers.Items.AddRange(_numbers.Select(x => (object)x).ToArray());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                var contact = cmbNumbers.Text;
                var driverDelBox = new DriverDelBox(_queriesHandler.CheckDriver(contact));
                driverDelBox.ShowDialog();

                if (driverDelBox.DialogResult == DialogResult.OK)
                {
                    _queriesHandler.DelDriver(contact);
                    MessageBox.Show("Водитель удалён.", "Информация");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Информация");
            }
        }

        private void cmbNumbers_KeyUp(object sender, KeyEventArgs e)
        {
            if (_numbers.Contains(cmbNumbers.Text))
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }
    }
}
