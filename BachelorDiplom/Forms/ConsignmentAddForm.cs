using System;
using System.Windows.Forms;
using BachelorLibAPI.Program;

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
            
        }
    }
}
