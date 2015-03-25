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
    public partial class DriverDelBox : Form
    {
        public DriverDelBox(string fullName)
        {
            InitializeComponent();
            lblName.Text = fullName;
        }
    }
}
