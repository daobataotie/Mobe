using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Book.UI.Help
{
    public class ExcelHelper
    {
        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int iD);
        private int processId = 0;

        public Microsoft.Office.Interop.Excel.Application app = null;
        public Microsoft.Office.Interop.Excel.Workbook wb = null;

        public ExcelHelper()
        {
            Type t = Type.GetTypeFromProgID("EXCEL.Application");
            if (t == null)
            {
                throw new Exception("本机沒有安装Office-Excel");
            }
        }

        public void OpenFile(string fileName)
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            app.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            GetWindowThreadProcessId((IntPtr)app.Hwnd, out this.processId);
        }

        public object GetCellValue(int row, int col)
        {
            Microsoft.Office.Interop.Excel.Worksheet sheet = app.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
            return sheet.Cells[row, col]; 
        }

        public void Close()
        {
            app.ActiveWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            app.Quit();

            if (this.processId != 0)
            {
                Process p = Process.GetProcessById(this.processId);
                p.Kill();
            }
        }
    }
}
