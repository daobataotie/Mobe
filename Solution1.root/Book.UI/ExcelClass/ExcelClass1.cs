using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using System.Collections.Specialized;
using System.Data.OleDb;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
namespace Book.UI.ExcelClass
{
    class ExcelClass1
    {
        private string AList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
         [DllImport("User32.dll", CharSet = CharSet.Auto)]
        /// <summary>
        /// 列标号
        /// </summary>     
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int ID);
        /// <summary>
        /// 文件名（含路径）
        /// </summary>
        public string mFilename;
        /// <summary>
        /// Excel工作进程
        /// </summary>
        public Microsoft.Office.Interop.Excel.Application app;
        /// <summary>
        /// 工作簿
        /// </summary>
        public Microsoft.Office.Interop.Excel.Workbooks wbs;
        /// <summary>
        /// 工作本
        /// </summary>
        public Microsoft.Office.Interop.Excel.Workbook wb;
        /// <summary>
        /// 工作表集
        /// </summary>
        public Microsoft.Office.Interop.Excel.Worksheets wss;
        /// <summary>
        /// 工作表
        /// </summary>
        public Microsoft.Office.Interop.Excel.Worksheet ws;
        /// <summary>
        /// 构造函数，不创建Microsoft.Office.Interop.Excel工作薄
        /// </summary>
        public ExcelClass1()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 创建Microsoft.Office.Interop.Excel工作薄
        /// </summary>
        public void Create()//创建一个Microsoft.Office.Interop.Excel对象
        {
            try
            {
                app = new Microsoft.Office.Interop.Excel.Application();
                wbs = app.Workbooks;
                wb = wbs.Add(true);
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 显示Microsoft.Office.Interop.Excel
        /// </summary>
        public void ShowExcel()
        {
            app.Visible = true;
        }
        /// <summary>
        /// 打开一个存在的Microsoft.Office.Interop.Excel文件
        /// </summary>
        /// <param name="FileName">Microsoft.Office.Interop.Excel完整路径加文件名</param>
        public void Open(string FileName)//打开一个Microsoft.Office.Interop.Excel文件
        {
            try
            {
                app = new Microsoft.Office.Interop.Excel.Application();
                wbs = app.Workbooks;
                wb = wbs.Add(FileName);
                //wb = wbs.Open(FileName, 0, true, 5,"", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "t", false, false, 0, true,Type.Missing,Type.Missing);
                //wb = wbs.Open(FileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
                mFilename = FileName;
                //设置禁止弹出保存和覆盖的询问提示框
                app.DisplayAlerts = false;
                app.AlertBeforeOverwriting = false;
                app.UserControl = true;//如果只想用程序控制该excel而不想让用户操作时候，可以设置为false
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 获取一个工作表
        /// </summary>
        /// <param name="SheetName">工作表名</param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Worksheet  GetSheet(string SheetName)
        {
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[SheetName];
            return s;
        }




        /// <summary>
        /// 返回指定单元格的文本
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <returns></returns>
        public string GetCellStr(string sheetName, int row, int col) //返回指定单元格的文本
        {
            string str = "";
            try
            {
                //Open(xlsFileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
                Microsoft.Office.Interop.Excel.Range r = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[row, col]);
                str = r.Text.ToString();
                r = null;
                worksheet = null;
                //Close();
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
            return str;
            //Microsoft.Office.Interop.Excel.Range rng3 = xSheet.get_Range("C6", System.Reflection.Missing.Value);     
            //rng3.Cells.FormulaR1C1   =   txtCellText.Text;         
            // rng3.Interior.ColorIndex = 6;   //设置Range的背景色 
        }
        /// <summary>
        /// 添加一个工作表
        /// </summary>
        /// <param name="SheetName">表名</param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Worksheet AddSheet(string SheetName)
        {
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            try
            {
                s.Name = SheetName;
                return s;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 删除一个工作表
        /// </summary>
        /// <param name="SheetName">表名</param>
        public void DelSheet(string SheetName)
        {
            try
            {
                ((Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[SheetName]).Delete();
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 重命名一个工作表
        /// </summary>
        /// <param name="OldSheetName">原名</param>
        /// <param name="NewSheetName">新名</param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Worksheet ReNameSheet(string OldSheetName, string NewSheetName)
        {
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[OldSheetName];
            try
            {
                s.Name = NewSheetName;
                return s;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// 重命名一个工作表
        /// </summary>
        /// <param name="Sheet">工作表</param>
        /// <param name="NewSheetName">工作表新名</param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Worksheet ReNameSheet(Microsoft.Office.Interop.Excel.Worksheet Sheet, string NewSheetName)
        {
            try
            {
                Sheet.Name = NewSheetName;
                return Sheet;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return null;
            }
        }
        //_____________________________ole自定义函数___________________________________________________________________________
        //______________________________excel和datagridview之间互操作函数_______________________________________________________
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //把EXCEl中的某工作表显示到datagridview中
        /// <summary>
        /// ExcelEdit myExcel = new ExcelEdit();
        /// myExcel.Open("d:\\数据库表格20071217.xls");
        /// myExcel.Excel2DBView("908", this.dataGridView1);
        /// myExcel.Close();
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="dataGridView1"></param>
    //    public void Excel2DBView(string xlsFilaName, string tablename, DataGridView dataGridView1)
    //    {
    //        try
    //        {
    //            string sExcelFile = xlsFilaName;
    //            //string strExcelFileName = @""+ myPath +"";
    //            //string strString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + strExcelFileName + ";Extended Properties = &apos;Microsoft.Office.Interop.Excel 8.0;HDR=NO;IMEX=1 &apos;";
    //            //string sConnectionString = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + sExcelFile + ";Extended Properties=Microsoft.Office.Interop.Excel 8.0;";
    //            string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExcelFile + ";Extended Properties=\"Microsoft.Office.Interop.Excel 8.0;HDR=NO;IMEX=1\"";
    //            OleDbConnection connection = new OleDbConnection(sConnectionString);
    //            string sql_select_commands = "Select * from [" + tablename + "$]";
    //            OleDbDataAdapter adp = new OleDbDataAdapter(sql_select_commands, connection);
    //            DataSet ds = new DataSet();
    //            adp.Fill(ds, tablename);
    //            dataGridView1.Rows.Clear();
    //            dataGridView1.Columns.Clear();
    //            //写入dataGridView控件标题
    //            for (int j = 0; j < ds.Tables[tablename].Columns.Count; j++)
    //            {
    //                dataGridView1.Columns.Add(ds.Tables[tablename].Rows[0][j].ToString(), ds.Tables[tablename].Rows[0][j].ToString());
    //            }
    //            for (int i = 1; i < ds.Tables[tablename].Rows.Count; i++)
    //            {
    //                dataGridView1.Rows.Add();
    //            }
    //            //写入dataGridView控件行数据
    //            for (int i = 1; i < ds.Tables[tablename].Rows.Count; i++)
    //                for (int j = 0; j < ds.Tables[tablename].Columns.Count; j++)
    //                {
    //                    dataGridView1.Rows[i - 1].Cells[j].Value = Convert.ToString(ds.Tables[tablename].Rows[i][j]);
    //                }
    //        }
    //        catch (Exception e)
    //        { MessageBox.Show(e.Message.ToString()); }
    //        /*
    //        for (int i = 0; i < ds.Tables["Book1"].Rows.Count; i++)
    //        {
    //            sum1 += Convert.ToInt32(ds.Tables["Book1"].Rows[i]["字段A"]);
    //        }
    //        for (int j = 0; j < ds.Tables["Book1"].Rows.Count; j++)
    //        {
    //            sum2 += Convert.ToInt32(ds.Tables["Book1"].Rows[j]["字段B"]);
    //        }
    //        MessageBox.Show(sum1.ToString() + " and " + sum2.ToString());
    //         * */
    //        /*
    //         *备注：
    //         * 用OLEDB进行Microsoft.Office.Interop.Excel文件数据的读取，并返回DataSet数据集。其中有几点需要注意的：
    //          1.连接字符串中参数IMEX 的值： 
    //          0 is Export mode 1 is Import mode 2 is Linked mode (full update capabilities)
    //          IMEX有3个值：当IMEX=2 时，EXCEL文档中同时含有字符型和数字型时，比如第C列有3个值，2个为数值型 123，1个为字符型 ABC，当导入时，
    //          页面不报错了，但库里只显示数值型的123，而字符型的ABC则呈现为空值。当IMEX=1时，无上述情况发生，库里可正确呈现 123 和 ABC.
    //         2.参数HDR的值：
    //         HDR=Yes，这代表第一行是标题，不做为数据使用 ，如果用HDR=NO，则表示第一行不是标题，做为数据来使用。系统默认的是YES
    //         3.参数Microsoft.Office.Interop.Excel 8.0
    //         对于Microsoft.Office.Interop.Excel 97以上版本都用Microsoft.Office.Interop.Excel 8.0
    //          @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcel.xls;Extended Properties=""Microsoft.Office.Interop.Excel 8.0;HDR=Yes;IMEX=1""" 
    //          "HDR=Yes;" indicates that the first row contains columnnames, not data
    //          "IMEX=1;" tells the driver to always read "intermixed" data columns as text
    //          TIP! SQL syntax: "SELECT * FROM [sheet1$]" - i.e. worksheet name followed by a "$" and wrapped in "[" "]" brackets.
    //          如果第一行是数据而不是标题的话, 应该写: "HDR=No;"
    //         "IMEX=1;" tells the driver to always read "intermixed" data columns as text 
    //         * */
    //        /*
    //         你可以先用代码打开xls文件:
    //         Set xlApp = CreateObject("Microsoft.Office.Interop.Excel.Application")
    //         Set xlBook = xlApp.Workbooks.Open("d:\text2.xls")
    //         for i=0 to xlBook.Worksheets.Count-1
    //         set xlSheet = xlBook.Worksheets(i)
    //         xlSheet.Name   //这就是你需要的每个sheet的名字,保存起来,备后用
    //         next i
    //         这里使用的VB写的范例,变成c#即可.
    //         */
    //}



        /// <summary>
        /// 把EXCEl中的某工作表中字段值为FieldNameStr的行显示到datagridview中
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="FieldValueStr"></param>
        /// <param name="dataGridView1"></param>
        public void Excel2DBView_SelectFieldValue(string tablename, string FieldName, string FieldNameStr, DataGridView dataGridView1)
        {
            string sExcelFile = mFilename;
            try
            {
                string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExcelFile + ";Extended Properties=\"Microsoft.Office.Interop.Excel 8.0;HDR=Yes;IMEX=1\"";
                OleDbConnection connection = new OleDbConnection(sConnectionString);
                string sql_select_commands = "select * from [" + tablename + "$] where " + FieldName + "=" + FieldNameStr;
                //MessageBox.Show(sql_select_commands);
                OleDbDataAdapter adp = new OleDbDataAdapter(sql_select_commands, connection);
                DataSet ds = new DataSet();
                adp.Fill(ds, tablename);
                //设置DataGridView控件的字段标题
                DataSet ds2 = new DataSet();
                ds2 = GetNewDataSet(tablename);
                for (int j = 0; j < ds2.Tables[tablename].Columns.Count; j++)
                {
                    dataGridView1.Columns.Add(ds2.Tables[tablename].Rows[0][j].ToString(), ds2.Tables[tablename].Rows[0][j].ToString());
                }
                for (int i = 0; i < ds.Tables[tablename].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                }
                for (int i = 0; i < ds.Tables[tablename].Rows.Count; i++)
                    for (int j = 0; j < ds.Tables[tablename].Columns.Count; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = Convert.ToString(ds.Tables[tablename].Rows[i][j]);
                    }
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 返回excel中不把第一行当做标题看待的数据集
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataSet GetNewDataSet(string tablename)
        {
            string sExcelFile = mFilename;
            try
            {
                string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExcelFile + ";Extended Properties=\"Microsoft.Office.Interop.Excel 8.0;HDR=NO;IMEX=1\"";
                OleDbConnection connection = new OleDbConnection(sConnectionString);
                string sql_select_commands = "Select * from [" + tablename + "$]";
                OleDbDataAdapter adp = new OleDbDataAdapter(sql_select_commands, connection);
                DataSet ds = new DataSet();
                adp.Fill(ds, tablename);
                return ds;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); return null; }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //根据字段名，删除EXCEl中的某工作表中的记录(SQL语句未成功执行)
        /// <summary>
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="FieldValueStr"></param>
        /// <param name="dataGridView1"></param>
        public void DeleteExcelFieldValue(string tablename, string FieldName, string FieldNameStr)
        {
            string sExcelFile = mFilename;
            try
            {
                string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExcelFile + ";Extended Properties=\"Microsoft.Office.Interop.Excel 8.0;HDR=Yes;IMEX=1\"";
                OleDbConnection connection = new OleDbConnection(sConnectionString);
                string sql_select_commands = "delete from [" + tablename + "$] where " + FieldName + "=" + FieldNameStr;
                //string sql_select_commands = "delete from [" + tablename + "$]";
                //string sql_select_commands = "drop table [" + tablename + "$]";
                MessageBox.Show(sql_select_commands);
                OleDbDataAdapter adp = new OleDbDataAdapter(sql_select_commands, connection);
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 在工作表中插入行，并调整其他行以留出空间
        /// </summary>
        /// <param name="sheet">当前工作表</param>
        /// <param name="rowIndex">欲插入的行索引</param>
        public void InsertRows(Microsoft.Office.Interop.Excel.Worksheet sheet, int rowIndex)
        {
            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[rowIndex, Type.Missing];
            //object Range.Insert(object shift, object copyorigin);
            //shift: Variant类型，可选。指定单元格的调整方式。可以为下列 XlInsertShiftDirection 常量之一：
            //xlShiftToRight 或 xlShiftDown。如果省略该参数，Microsoft Microsoft.Office.Interop.Excel 将根据区域形状确定调整方式。
            range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
            range = null;
        }
        /// <summary>
        /// 在工作表中删除行
        /// </summary>
        /// <param name="sheet">当前工作表</param>
        /// <param name="rowIndex">欲删除的行索引</param>
        public void DeleteRows(Microsoft.Office.Interop.Excel.Worksheet sheet, int rowIndex)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[rowIndex, Type.Missing];
                range.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
                range = null;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }

        /// <summary>
        /// 在工作表中删除所有行名为rowText的行
        /// </summary>
        /// <param name="sheetName">当前工作表</param>
        /// <param name="FieldName">字段名</param>
        /// <param name="rowText">字段值</param>
        public void DeleteRows(string sheetName, string FieldName, string rowText)
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
            try
            {
                if (worksheet == null)
                {
                    throw new Exception("Worksheet   error ");
                    return;
                }
                //获取工作表行列数
                int rowCounts = rowcount(sheetName);
                int colCounts = colcount(sheetName);
                // MessageBox.Show(rowCounts.ToString() +"|" +colCounts.ToString());
                int colIndex = 0;
                //定位字段索引
                for (int i = 1; i <= colCounts; i++)
                {
                    if (GetCellStr(sheetName, 1, i) == FieldName)
                    {
                        colIndex = i;
                        break;
                    }
                }
                if (colIndex == 0)
                {
                    MessageBox.Show("找不到字段名：" + FieldName);
                    return;
                }
                //int jj = 0;
                //int jjj = 0;
                for (int i = 1; i <= rowCounts; i++)
                {
                    //jj++;
                    if (GetCellStr(sheetName, i, colIndex) == rowText)
                    {
                        DeleteRows(worksheet, i);
                        i--;
                        rowCounts = rowCounts - 1;
                        //jjj++;
                    }
                }
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }

        /// <summary>
        /// 根据字段名，追加记录
        /// </summary>
        /// <param name="sheetName">当前工作表</param>
        /// <param name="FieldName">字段名</param>
        /// <param name="recordText">字段值</param>
        public void AddRecords(string sheetName, string FieldName, string recordText)
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
            try
            {
                if (worksheet == null)
                {
                    throw new Exception("Worksheet   error ");
                    return;
                }
                //获取工作表行列数
                int rowCounts = rowcount(sheetName);
                int colCounts = colcount(sheetName);
                // MessageBox.Show(rowCounts.ToString() +"|" +colCounts.ToString());
                int colIndex = 0;
                //定位字段索引
                for (int i = 1; i <= colCounts; i++)
                {
                    if (GetCellStr(sheetName, 1, i) == FieldName)
                    {
                        colIndex = i;
                        break;
                    }
                }
                if (colIndex == 0)
                {
                    MessageBox.Show("找不到字段名：" + FieldName);
                    return;
                }
                SetCellStr(sheetName, rowCounts + 1, colIndex, recordText);
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 获取字段中的字段名称集
        /// </summary>
        /// <param name="sheetName">当前工作表</param>
        /// <param name="FieldName">字段名</param>
        /// <returns></returns>
        public StringCollection GetFieldValues(string sheetName, string FieldName)
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
            if (worksheet == null)
            {
                throw new Exception("Worksheet   error ");
                return null;
            }
            //获取工作表行列数
            int rowCounts = rowcount(sheetName);
            int colCounts = colcount(sheetName);
            // MessageBox.Show(rowCounts.ToString() +"|" +colCounts.ToString());
            int colIndex = 0;
            //定位字段索引
            for (int i = 1; i <= colCounts; i++)
            {
                if (GetCellStr(sheetName, 1, i) == FieldName)
                {
                    colIndex = i;
                    break;
                }
            }
            if (colIndex == 0)
            {
                MessageBox.Show("找不到字段名：" + FieldName);
                return null;
            }
            StringCollection fieldStrs = new StringCollection();
            string cellStr;
            for (int i = 2; i <= rowCounts; i++)
            {
                cellStr = GetCellStr(sheetName, i, colIndex);
                if (!fieldStrs.Contains(cellStr))
                {
                    fieldStrs.Add(cellStr);
                }
            }
            return fieldStrs;

        }
        /// <summary>
        /// DataGridView导出到Microsoft.Office.Interop.Excel 
        /// ExcelEdit myExcel = new ExcelEdit();
        /// myExcel.Create();
        /// myExcel.DGView2Excel(this.dataGridView1, "d:\\xiao.xls", "xiaobiao");
        /// myExcel.Save();
        /// myExcel.Close();
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="sheetName">当前工作表</param>
        public void DGView2Excel(DataGridView dgv, string sheetName)
        {
            try
            {
                int rowCount = dgv.RowCount;
                int columnCount = dgv.ColumnCount;
                //Microsoft.Office.Interop.Excel.Workbooks workbook=(Microsoft.Office.Interop.Excel.Workbook)app
                //Open(xlsFileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
                int sheet_rowCounts = rowcount(sheetName);
                int sheet_colCounts = colcount(sheetName);
                //int rowIndex = 0;
                if (worksheet == null)
                {
                    throw new Exception("Worksheet   error ");
                }
                //
                Microsoft.Office.Interop.Excel.Range r = worksheet.get_Range("A1 ", Missing.Value);
                if (r == null)
                {
                    MessageBox.Show("Range无法启动 ");
                    throw new Exception("Range   error ");
                }
                //以上是一些例行的初始化工作,下面进行具体的信息填充 
                //填充标题 
                int ColIndex = 1;
                foreach (DataGridViewColumn dHeader in dgv.Columns)
                {
                    worksheet.Cells[1, ColIndex++] = dHeader.HeaderText;
                    //worksheet.Cells[21, ColIndex++] = dHeader.HeaderText;
                }
                ColIndex = 1;
                //获取DataGridView中的所有行和列的数值,填充到一个二维数组中. 
                object[,] myData = new object[rowCount + 1, columnCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        myData[i, j] = dgv[j, i].Value;         //这里的获取注意行列次序 
                    }
                }
                //将填充好的二维数组填充到Microsoft.Office.Interop.Excel对象中. 
                r = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[rowCount + 1, columnCount]);
                r.Value2 = myData;
                r = null;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// DataGridView追加到Microsoft.Office.Interop.Excel指定表格中 
        /// ExcelEdit myExcel = new ExcelEdit();
        /// myExcel.Create();
        /// myExcel.DGViewAdd2Excel(this.dataGridView1, "d:\\xiao.xls", "xiaobiao");
        /// myExcel.Save();
        /// myExcel.Close();
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sheetName"></param>
        public void DGViewAdd2Excel(DataGridView dgv, string sheetName)
        {
            try
            {
                //DataGridView控件行列数
                int rowCount = dgv.RowCount;
                int columnCount = dgv.ColumnCount;
                //工作表行列数
                int sheet_row_count = rowcount(sheetName);
                int sheet_column_count = colcount(sheetName);
                //        MessageBox.Show(sheet_row_count.ToString() + " | "+sheet_column_count.ToString());
                //Microsoft.Office.Interop.Excel.Workbooks workbook=(Microsoft.Office.Interop.Excel.Workbook)app
                //Open(xlsFileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
                if (worksheet == null)
                {
                    throw new Exception("Worksheet   error ");
                }
                // 
                Microsoft.Office.Interop.Excel.Range r = worksheet.get_Range("A1 ", Missing.Value);
                if (r == null)
                {
                    MessageBox.Show("Range无法启动 ");
                    throw new Exception("Range   error ");
                }
                //以上是一些例行的初始化工作,下面进行具体的信息填充 
                //填充标题 
                int ColIndex = 1;
                foreach (DataGridViewColumn dHeader in dgv.Columns)
                {
                    worksheet.Cells[1, ColIndex++] = dHeader.HeaderText;
                }
                ColIndex = 1;
                //获取DataGridView中的所有行和列的数值,填充到一个二维数组中. 
                object[,] myData = new object[rowCount + 1, columnCount];
                for (int i = 1; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        myData[i - 1, j] = dgv[j, i - 1].Value;         //这里的获取注意行列次序 
                    }
                }
                //将填充好的二维数组填充到Microsoft.Office.Interop.Excel对象中. 
                //r = worksheet.get_Range(worksheet.Cells[sheet_row_count + 1, 1], worksheet.Cells[sheet_row_count + rowCount, columnCount]);
                r = worksheet.get_Range(worksheet.Cells[sheet_row_count + 1, 1], worksheet.Cells[sheet_row_count + rowCount, columnCount]);
                r.Value2 = myData;
                r = null;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }

        //_________________________________________________________________________________
        /// <summary>
        /// 返回工作表名
        /// </summary>
        /// <param name="_filename">文件名（含地址）</param>
        /// <returns>工作表名集</returns>
        public StringCollection countexcel(string _filename) //
        {
            if (System.IO.File.Exists(_filename))
            {
                Microsoft.Office.Interop.Excel.ApplicationClass myExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook excel_wb;
                excel_wb = myExcelApp.Workbooks.Open(_filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                StringCollection a = new StringCollection();
                for (int i = 1; i <= excel_wb.Worksheets.Count; i++)
                {
                    a.Add(((Microsoft.Office.Interop.Excel.Worksheet)excel_wb.Worksheets[i]).Name);
                }
                myExcelApp.Workbooks.Close();
                myExcelApp.Quit();
                myExcelApp = null;
                return a;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 用datset返回整个excel
        /// </summary>
        /// <param name="_filename">文件名（含地址）</param>
        /// <returns></returns>
        public DataSet proces(string _filename) //
        {
            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;";
            strConn += "data source=" + _filename + ";";
            strConn += "Extended Properties=Microsoft.Office.Interop.Excel 8.0;";
            strConn += "HDR=Yes;IMEX=1";
            OleDbConnection objConn = new OleDbConnection(strConn);
            DataSet ds = new DataSet();
            OleDbDataAdapter oldda = new OleDbDataAdapter();
            foreach (string sheetname in countexcel(_filename))
            {
                string a = "select * from ";
                a += sheetname;
                oldda.SelectCommand.CommandText = a;
                oldda.Fill(ds, sheetname);
            }
            return ds;
        }
        /// <summary>
        /// 行数
        /// </summary>
        /// <param name="_filename">文件名（含地址）</param>
        /// <param name="sheetname">工作表</param>
        /// <returns></returns>
        public int rowcount(string _filename, string sheetname) //行数
        {
            Microsoft.Office.Interop.Excel.ApplicationClass myExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook excel_wb;
            excel_wb = myExcelApp.Workbooks.Open(_filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            int counts = ((Microsoft.Office.Interop.Excel.Worksheet)excel_wb.Worksheets.get_Item(sheetname)).UsedRange.Rows.Count;
            myExcelApp.Workbooks.Close();
            myExcelApp.Quit();
            myExcelApp = null;
            return counts;
        }
        /// <summary>
        /// 当前打开的表的行数
        /// </summary>
        /// <param name="sheetname">当前工作表</param>
        /// <returns></returns>
        public int rowcount(string sheetname) //行数
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetname);
            if (worksheet == null)
            {
                throw new Exception("Worksheet   error ");
            }
            int counts = worksheet.UsedRange.Rows.Count;
            worksheet = null;
            return counts;
        }
        /// <summary>
        /// 列数
        /// </summary>
        /// <param name="_filename">文件名（含地址）</param>
        /// <param name="sheetname">工作表名</param>
        /// <returns></returns>
        public int colcount(string _filename, string sheetname) //列数
        {
            Microsoft.Office.Interop.Excel.ApplicationClass myExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook excel_wb;
            excel_wb = myExcelApp.Workbooks.Open(_filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            int counts = ((Microsoft.Office.Interop.Excel.Worksheet)excel_wb.Worksheets.get_Item(sheetname)).UsedRange.Columns.Count;
            myExcelApp.Workbooks.Close();
            myExcelApp.Quit();
            myExcelApp = null;
            return counts;
        }
        /// <summary>
        /// 当前工作表列数
        /// </summary>
        /// <param name="sheetname">当前工作表</param>
        /// <returns></returns>
        public int colcount(string sheetname) //列数
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetname);
            if (worksheet == null)
            {
                throw new Exception("Worksheet   error ");
            }
            int counts = worksheet.UsedRange.Columns.Count;
            worksheet = null;
            return counts;
        }

        /// <summary>
        /// 将字符串写入指定单元格
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="writeStr">值</param>
        public void SetCellStr(string sheetName, int row, int col, string writeStr)
        {
            try
            {
                //Open(xlsFileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = GetSheet(sheetName);
                Microsoft.Office.Interop.Excel.Range r = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[row, col]);
                //r.Text.FormulaR1C1;
                r.Value2 = writeStr;
                worksheet = null;
                r = null;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
            //Save();
            // Close();
            //Microsoft.Office.Interop.Excel.Range rng3 = xSheet.get_Range("C6", System.Reflection.Missing.Value);     
            //rng3.Cells.FormulaR1C1   =   txtCellText.Text;         
            // rng3.Interior.ColorIndex = 6;   //设置Range的背景色 
        }

        //_______________________________________添加自定义函数_________________________________________________________________
        /// <summary>
        /// 将数据写入Microsoft.Office.Interop.Excel
        /// </summary>
        /// <param name="data">要写入的二维数组数据</param>
        /// <param name="startRow">Microsoft.Office.Interop.Excel中的起始行</param>
        /// <param name="startColumn">Microsoft.Office.Interop.Excel中的起始列</param>
        public void WriteData(string[,] data, string fileName, string sheetName, int startRow, int startColumn)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application myExcel;
                Microsoft.Office.Interop.Excel.Workbook myWorkBook;
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                //myWorkBook = myExcel.Application.Workbooks.Add(true);
                myWorkBook = myExcel.Workbooks.Add(fileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                worksheet.Name = sheetName;
                //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcel.Worksheets[sheetName];
                worksheet.Activate();
                int rowNumber = data.GetLength(0);
                int columnNumber = data.GetLength(1);
                for (int i = 0; i < rowNumber; i++)
                {
                    for (int j = 0; j < columnNumber; j++)
                    {
                        //在Microsoft.Office.Interop.Excel中，如果某单元格以单引号“'”开头，表示该单元格为纯文本，因此，我们在每个单元格前面加单引号。 
                        myExcel.Cells[startRow + i, startColumn + j] = "'" + data[i, j];
                    }
                }
                myExcel.Quit();
                myWorkBook = null;
                myExcel = null;
                GC.Collect();
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
        }
        /// <summary>
        /// 将数据写入Microsoft.Office.Interop.Excel
        /// </summary>
        /// <param name="data">要写入的字符串</param>
        /// <param name="starRow">写入的行</param>
        /// <param name="startColumn">写入的列</param>
        public void WriteData(string data, string fileName, string sheetName, int row, int column)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application myExcel;
                Microsoft.Office.Interop.Excel.Workbook myWorkBook;
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                //myWorkBook = myExcel.Application.Workbooks.Add(true);
                myWorkBook = myExcel.Workbooks.Add(fileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                worksheet.Name = sheetName;
                //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcel.Worksheets[sheetName];
                worksheet.Activate();
                myExcel.Cells[row, column] = data;
                myExcel.Quit();
                myWorkBook = null;
                myExcel = null;
                GC.Collect();
            }
            catch (Exception e)
            { MessageBox.Show(e.Message.ToString()); }
            //Microsoft.Office.Interop.Excel.Range rng3 = xSheet.get_Range("C6", System.Reflection.Missing.Value);     
            //rng3.Cells.FormulaR1C1   =   txtCellText.Text;         
            // rng3.Interior.ColorIndex = 6;   //设置Range的背景色   
        }





        /// <summary>
        /// 读取指定单元格数据
        /// </summary>
        /// <param name="row">行序号</param>
        /// <param name="column">列序号</param>
        /// <returns>该格的数据</returns>
        public string ReadData(string fileName, string sheetName, int row, int column)
        {
            Microsoft.Office.Interop.Excel.Application myExcel;
            Microsoft.Office.Interop.Excel.Workbook myWorkBook;
            myExcel = new Microsoft.Office.Interop.Excel.Application();
            //myWorkBook = myExcel.Application.Workbooks.Add(true);
            myWorkBook = myExcel.Workbooks.Add(fileName);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            worksheet.Name = sheetName;
            //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcel.Worksheets[sheetName];
            worksheet.Activate();
            Microsoft.Office.Interop.Excel.Range range = myExcel.get_Range(myExcel.Cells[row, column], myExcel.Cells[row, column]);
            string str = range.Text.ToString();
            myExcel.Quit();
            myWorkBook = null;
            myExcel = null;
            GC.Collect();
            return str;
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void UniteCells(Microsoft.Office.Interop.Excel.Worksheet ws, int x1, int y1, int x2, int y2)
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);
        }
        /// <summary>
        /// 将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置 为在使用模板时控制格式时使用一
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="ws">工作表名</param>
        /// <param name="startX">行</param>
        /// <param name="startY">列</param>
        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX + i, j + startY] = dt.Rows[i][j].ToString();
                }
            }
        }
        /// <summary>
        /// 将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置二
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="ws">工作表</param>
        /// <param name="startX">开始行</param>
        /// <param name="startY">开始列</param>
        public void InsertTable(System.Data.DataTable dt, Microsoft.Office.Interop.Excel.Worksheet ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    ws.Cells[startX + i, j + startY] = dt.Rows[i][j];
                }
            }
        }
        /// <summary>
        /// 将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置一
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="ws">工作表名</param>
        /// <param name="startX">开始行</param>
        /// <param name="startY">开始列</param>
        public void AddTable(System.Data.DataTable dt, string ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[i + startX, j + startY] = dt.Rows[i][j];
                }
            }
        }
        /// <summary>
        /// 将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表（包括字段名）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        public void AddTable(System.Data.DataTable dt, string ws)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                GetSheet(ws).Cells[1, i + 1] = dt.Columns[i].ColumnName.ToString();
            }
            AddTable(dt, ws, 2, 1);
        }
        /// <summary>
        /// 将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置二
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void AddTable(System.Data.DataTable dt, Microsoft.Office.Interop.Excel.Worksheet ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    ws.Cells[i + startX, j + startY] = dt.Rows[i][j];
                }
            }
        }
        /// <summary>
        /// 设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        /// </summary>
        /// <param name="ws">工作表</param>
        /// <param name="Startx">开始行</param>
        /// <param name="Starty">开始列</param>
        /// <param name="Endx">结束行</param>
        /// <param name="Endy">结束列</param>
        /// <param name="size">字体大小</param>
        /// <param name="name">字体名</param>
        /// <param name="color">颜色</param>
        /// <param name="HorizontalAlignment">对齐方式</param>
        public void SetCellProperty(Microsoft.Office.Interop.Excel.Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name, Microsoft.Office.Interop.Excel.Constants color, Microsoft.Office.Interop.Excel.Constants HorizontalAlignment)
        {
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }
        /// <summary>
        /// 设置一个单元格的属性
        /// </summary>
        /// <param name="wsn">工作表名</param>
        /// <param name="Startx">开始行</param>
        /// <param name="Starty">开始列</param>
        /// <param name="Endx">结束行</param>
        /// <param name="Endy">结束列</param>
        /// <param name="size">字体大小</param>
        /// <param name="name">字体名</param>
        /// <param name="color">颜色</param>
        /// <param name="HorizontalAlignment">对齐方式</param>
        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name, Microsoft.Office.Interop.Excel.Constants color, Microsoft.Office.Interop.Excel.Constants HorizontalAlignment)
        {
            Microsoft.Office.Interop.Excel.Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
  
        }
        /// <summary>
        /// 设置一个工作表的数字全为文本
        /// </summary>
        /// <param name="wsn"></param>
        public void SetNumberFormat(string wsn)
        {
            Microsoft.Office.Interop.Excel.Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[1, 1], ws.Cells[65536, 256]).NumberFormatLocal = "@";
        }
        public void SetWidth(Worksheet ws, string AB, double i)
        {
            ((Range)ws.Columns[AB, System.Type.Missing]).ColumnWidth = i;
        }

        /// <summary>
        /// 保存Microsoft.Office.Interop.Excel
        /// </summary>
        /// <returns>保存成功返回True</returns>
        public bool Save()
        {
            if (mFilename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();//似乎失效，所以加一下一个语句
                    wb.SaveAs(mFilename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        /// <summary>
        /// Microsoft.Office.Interop.Excel文档另存为
        /// </summary>
        /// <param name="fileName">保存完整路径加文件名</param>
        /// <returns>保存成功返回True</returns>
        public bool SaveAs(object FileName)
        {
            try
            {
                wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// 去掉文件后缀
        /// </summary>
        /// <param name="FileName">文件名（含地址）</param>
        public void RemoveExefilter(string FileName)
        {
            string filename = FileName;
            string substr;
            if (System.IO.File.Exists(FileName))
            {
                System.IO.FileInfo myfile = new System.IO.FileInfo(FileName);
                substr = myfile.Extension;
                filename = filename.Substring(0, filename.Length - substr.Length);
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
                myfile.MoveTo(filename.ToString());
            }
        }
        /// <summary>
        /// 关闭一个Microsoft.Office.Interop.Excel对象，销毁对象
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            app.Quit();
            release_xlsObj();
            //调用window api查找Microsoft.Office.Interop.Excel进程,并用关闭
            IntPtr t = new IntPtr(app.Hwnd);
            int ProcessById;
            GetWindowThreadProcessId(t, out ProcessById);
            System.Diagnostics.Process ExcelProcess = System.Diagnostics.Process.GetProcessById(ProcessById);
            ExcelProcess.Kill();
            app = null;
            wbs = null;
            wb = null;
            wss = null;
            ws = null;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void release_xlsObj()
        {
            //if (app != null)
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            //app = null;
            if (wbs != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wbs);
            wbs = null;
            if (wb != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            wb = null;
            if (wss != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wss);
            wss = null;
            if (ws != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            ws = null;
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(Range);
            // Range=null;
            GC.Collect();
        }
        /// <summary>
        /// 关闭word,excel等进程
        /// </summary>
        public void KillProcess()
        {
            string processName = "EXCEL";
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程 
            try
            {
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(processName))
                {
                    if (!thisproc.CloseMainWindow())
                    {

                        thisproc.Kill();
                    }
                }
            }
            catch (Exception e)
            { }
        }

        /// <summary>
        /// 设置边框
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="Width"></param>
        public void setBorder(Worksheet ws,int x1, int y1, int x2, int y2, int Width)
        {
            Range range = ws.get_Range(this.GetAix(x1, y1), this.GetAix(x2, y2));
            range.Borders.Weight = Width;
        }

        /// <summary>
        /// 获取描述区域的字符
        /// </summary>
        /// <param name="x">列号</param>
        /// <param name="y">行号</param>
        /// <returns></returns>
        public string GetAix(int x, int y)
        {
            char[] AChars = AList.ToCharArray();
            int num1 = x / 26;
            int num2 = x % 26;
            string s = "";

            if (x > 26)
            {
                if (num1 > 1 && num2 == 0)
                {
                    s = s + AChars[num1 - 2].ToString() + AChars[25].ToString() + y.ToString();
                }
                else
                {
                    s = s + AChars[num1 - 1].ToString();
                    if (num2 == 0)
                    {
                        s = s + AChars[25].ToString() + y.ToString();
                    }
                    else
                    {
                        s = s + AChars[num2 - 1].ToString() + y.ToString();
                    }
                }

            }
            else
            {
                s = s + AChars[x - 1].ToString() + y.ToString();
            }

            return s;
        }



    }
}
