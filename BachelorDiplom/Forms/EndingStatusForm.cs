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
    public partial class EndingStatusForm : Form
    {
        QueriesHandler _queriesHandler;

        public EndingStatusForm(QueriesHandler _qh)
        {
            InitializeComponent();
            _queriesHandler = _qh;
        }

        /// <summary>
        /// В перспективе можно сделать ComboBox для времени отправления.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetEndingStatusClick(object sender, EventArgs e)
        {
            try
            {
                string num = System.Text.RegularExpressions.Regex.Replace(edtDriverPhoneNumber.Text, "[^0-9]", "");
                _queriesHandler.SetEndingStatus(num, dtpStart.Value, dtpArr.Value);
                MessageBox.Show("Статус установлен.", "Информация");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка.");
            }
        }
    }
}
