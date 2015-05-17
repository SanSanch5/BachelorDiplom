using System;
using System.Diagnostics;
using System.Windows.Forms;
using BachelorLibAPI.Forms;

namespace BachelorLibAPI.Program
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
                Application.Run(new MainForm());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(@"Необработанное исключение: " + ex.Message);
            //}
        }
    }
}
