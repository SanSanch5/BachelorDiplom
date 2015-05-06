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
            
        }
    }
}
