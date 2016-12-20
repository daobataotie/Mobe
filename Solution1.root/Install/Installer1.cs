using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;

namespace Book.UI
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public Installer1()
        {
            InitializeComponent();

        }
        private string MakeValidPath(string source)
        {
            if (source.EndsWith(@"\"))
                return source;
            else
                return (source + @"\");
        }
        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);            
            string path = this.Context.Parameters["path"];
            path = MakeValidPath(MakeValidPath(path) + "2.0.0.6");
           // ProcessStartInfo psi = new ProcessStartInfo(path + "crack.bat", "> 1.txt");
            ProcessStartInfo psi = new ProcessStartInfo(path + " Register.bat", "> 1.txt");
           
            //ProcessStartInfo psi = new ProcessStartInfo(path + "crack.bat");
            psi.WorkingDirectory = path;

            Process p = null;

            try
            {
                p = Process.Start(psi);
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                throw new InstallException(ex.Message + ":" + path);
            }
        }
    }
}