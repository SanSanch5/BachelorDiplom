using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BachelorLibAPI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new BachelorLibAPI.Forms.MainForm());
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Необработанное исключение: " + ex.Message);
            //}
        }
    }
}
