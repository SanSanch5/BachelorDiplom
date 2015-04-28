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
    public partial class DriverAddForm : Form
    {
        private QueriesHandler _queriesHandler;

        public DriverAddForm(QueriesHandler qh)
        {
            InitializeComponent();
            _queriesHandler = qh;
            tmrForGreatFocus.Enabled = true;
            tmrForGreatFocus.Interval = 200;
        }

        private void AddDriverBtnClick(object sender, EventArgs e)
        {
            try
            {
                string lName = edtDriverLastName.Text;
                string name = edtDriverName.Text;
                string mName = edtDriverMidName.Text;
                string num1 = System.Text.RegularExpressions.Regex.Replace(edtMainPhoneNumber.Text, "[^0-9]", "");
                string num2 = System.Text.RegularExpressions.Regex.Replace(edtAddPhoneNumber.Text, "[^0-9]", ""); 
                if (lName == "" || name == "" || num1 == "")
                    throw new FormatException("Звёздочкой (*) отмечены поля для обязательного заполнения!");
                if (!(num1.Length == 10 && (num2.Length == 10 || num2.Length == 0)))
                    throw new FormatException("Номер телефона должен состоять из 10 цифр!");

                _queriesHandler.AddNewDriver(lName, name, mName, num1, num2);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение");
            }
        }

        private void tmrForGreatFocus_Tick(object sender, EventArgs e)
        {
            if(!this.Focused)
            {
                this.Activate();
            }
        }
    }
}
