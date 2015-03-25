using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public partial class TransitsDelBeforeForm : Form
    {
        private QueriesHandler _queriesHandler;
        public TransitsDelBeforeForm(QueriesHandler qh)
        {
            InitializeComponent();
            _queriesHandler = qh;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены?", "Предупреждение", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    _queriesHandler.DelTransitsBefore(dtpTime.Value);
                    Close();
                    MessageBox.Show("Перевозки успешно удалены", "Информация");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }
    }
}
