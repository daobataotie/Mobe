using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Tool.CSManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadExit += new EventHandler(Application_ThreadExit);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void Application_ThreadExit(object sender, EventArgs e)
        {
            //if (log_file_stream != null)
            //{
            //    log_file_stream.Close();
            //}
            if (log_file_stream_writer != null)
            {
                log_file_stream_writer.Close();
            }
        }

        static StreamWriter log_file_stream_writer;
        static Program()
        {
            //log_file_stream = new FileStream(
            //    string.Format(@"{0}\0.log", Application.StartupPath),
            //    FileMode.OpenOrCreate
            //    );\
            log_file_stream_writer = new StreamWriter(
                string.Format(@"{0}\0.log", Application.StartupPath),
                false,
                System.Text.Encoding.Unicode
                );

        }

        public static void WriteLog(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                if (log_file_stream_writer != null) 
                {
                    log_file_stream_writer.WriteLine(
                        string.Format("{0}:{1}", DateTime.Now, content)
                        );
                }
            }
        }
    }
}