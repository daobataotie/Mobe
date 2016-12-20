using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tool.DBManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-TW");
            Form2 f = new Form2();
            f.ShowDialog();

        }
    }
}