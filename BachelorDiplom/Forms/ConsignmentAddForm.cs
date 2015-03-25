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
    public partial class ConsignmentAddForm : Form
    {
        private QueriesHandler _queriesHandler;

        public ConsignmentAddForm(QueriesHandler qh)
        {
            InitializeComponent();
            _queriesHandler = qh;
        }

        private void AddNewConsignmentClick(object sender, EventArgs e)
        {
            try
            {
                string name = edtDriverLastName.Text;
                int dangerDegree = cmbDangerDegrees.SelectedIndex;
                string afterCrash = edtCrashActions.Text;
                if (name == "" || dangerDegree < 0 || afterCrash == "")
                    throw new FormatException("Звёздочкой (*) отмечены поля для обязательного заполнения!");

                _queriesHandler.AddNewConsignment(name, dangerDegree, afterCrash);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
