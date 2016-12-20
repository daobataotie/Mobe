using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Book.UI.ExcelClass
{
	/// <summary>
	/// Excel的基本操作
	/// </summary>
	public class ExcelHandler
	{
		#region 变量
		private object miss = Missing.Value; //忽略的参数OLENULL 
		private Excel.Application m_objExcel;//Excel应用程序实例 
		private Excel.Workbooks m_objBooks;//工作表集合 
		private Excel.Workbook m_objBook;//当前操作的工作表 
		private Excel.Worksheet sheet;//当前操作的表格
		/// <summary>
		/// 列标号
		/// </summary>
		private string AList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		#endregion

		#region 属性
		public Excel.Worksheet CurrentSheet
		{
			get
			{
				return sheet;
			}
			set
			{
				this.sheet = value;
			}
		}

		public Excel.Workbooks CurrentWorkBooks
		{
			get
			{
				return this.m_objBooks;
			}
			set
			{
				this.m_objBooks = value;
			}
		}

		public Excel.Workbook CurrentWorkBook
		{
			get
			{
				return this.m_objBook;
			}
			set
			{
				this.m_objBook = value;
			}
		}
		#endregion

		#region 构造函数
		/// <summary>
		/// 构建ExcelHandler类
		/// </summary>
		public ExcelHandler()
		{
			this.m_objExcel = new Excel.Application();
		}

		/// <summary>
		/// 构建ExcelExcelHandler类
		/// </summary>
		/// <param name="objExcel">Excel.Application</param>
		public ExcelHandler(Excel.Application objExcel)
		{
			this.m_objExcel = objExcel;
		}

		#endregion

		#region 方法
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

		/// <summary>
		/// 给单元格区域赋值
		/// </summary>
		/// <param name="x1">开始行号</param>
		/// <param name="y1">开始列号</param>
		/// <param name="x2">结束行号</param>
		/// <param name="y2">结束列号</param>
		/// <param name="datas">数值</param>
		public void setValues(int x1, int y1, int x2, int y2, object[] datas)
		{
			Excel.Range range = this.getRange(x1, y1, x2, y2);
			range.set_Value(miss, datas);
		}
		/// <summary>
		/// 给单元格赋值1
		/// </summary>
		/// <param name="x">行号</param>
		/// <param name="y">列号</param>
		/// <param name="align">对齐（CENTER、LEFT、RIGHT）</param>
		/// <param name="text">值</param>
		public void setValue(int x, int y, string align, string text)
		{
			Excel.Range range = sheet.get_Range(this.GetAix(y, x), miss);
			range.set_Value(miss, text);
			if (align.ToUpper() == "CENTER")
			{
				range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
			}
			if (align.ToUpper() == "LEFT")
			{
				range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
			}
			if (align.ToUpper() == "RIGHT")
			{
				range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
			}
		}

		/// <summary>
		/// 给单元格赋值2
		/// </summary>
		/// <param name="x">行号</param>
		/// <param name="y">列号</param>
		/// <param name="text">值</param>
		public void setValue(int x, int y, string text)
		{
			Excel.Range range = sheet.get_Range(this.GetAix(y, x), miss);
			range.set_Value(miss, text);
		}

		/// <summary>
		/// 给单元格赋值3
		/// </summary>
		/// <param name="x">行号</param>
		/// <param name="y">列号</param>
		/// <param name="text">值</param>
		/// <param name="font">字符格式</param>
		/// <param name="color">颜色</param>
		public void setValue(int x, int y, string text, System.Drawing.Font font, System.Drawing.Color color)
		{
			this.setValue(x, y, text);
			Excel.Range range = sheet.get_Range(this.GetAix(y, x), miss);
			range.Font.Size = font.Size;
			range.Font.Bold = font.Bold;
			range.Font.Color = color;
			range.Font.Name = font.Name;
			range.Font.Italic = font.Italic;
			range.Font.Underline = font.Underline;
		}

		/// <summary>
		/// 在第x行插入新行
		/// </summary>
		/// <param name="x">模板行号</param>
		public void insertRow(int x)
		{
			#region
			//Excel.Range range = sheet.get_Range(GetAix(1, x), GetAix(25, x));
			//range.Copy(miss);
			//range.Insert(Excel.XlDirection.xlDown, miss);
			//range.get_Range(GetAix(1, x), GetAix(25, x));
			//range.Select();
			//sheet.Paste(miss, miss);
			#endregion

			((Excel.Range)sheet.Cells[x, 1]).EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, 0);
		}

		/// <summary>
		///   在第col列插入新列
		/// </summary>
		/// <param name="y"></param>
		public void insertColumn(object y)
		{
			((Excel.Range)sheet.Cells[1, y]).EntireColumn.Insert(Excel.XlInsertShiftDirection.xlShiftToRight, 0);
		}

		/// <summary>
		/// 在第col列插入一列(复制本列)
		/// </summary>
		/// <param name="y"></param>
		public void insertCopyColumnAt(object y)
		{
			((Excel.Range)sheet.Columns[y, miss]).Copy(miss);
			((Excel.Range)sheet.Cells[2, y]).EntireColumn.Insert(Excel.XlInsertShiftDirection.xlShiftToRight, 0);
		}

		/// <summary>
		/// 在第x行插入一行（复制本行）
		/// </summary>
		/// <param name="row">模板行号</param>
		public void InsertCopyRowAt(object x)
		{
			((Excel.Range)sheet.Rows[x, miss]).Copy(miss);
			((Excel.Range)sheet.Cells[x, 1]).EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, 0);
		}

		/// <summary>
		/// 删除指定区域行，若针对单个单元格进行删除，则设置结束结束单元格为空
		/// </summary>
		/// <param name="beginCell">开始单元格</param>
		/// <param name="endCell">结束单元格</param>
		public void deleteRow(string beginCell, string endCell)
		{
			Excel.Range range = null;
			if (string.IsNullOrEmpty(endCell))
			{
				range = sheet.get_Range(beginCell, miss);
			}
			else
			{
				range = sheet.get_Range(beginCell, endCell);
			}
			range.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
		}

		/// <summary>
		/// 删除指定区域行
		/// </summary>
		/// <param name="beginCell">开始行</param>
		/// <param name="endCell">结束行</param>
		public void deleteRow(int beginX, int endX)
		{
			Excel.Range range = null;
			range = sheet.get_Range(GetAix(1, beginX), GetAix(25, endX));
			range.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
		}

		/// <summary>
		/// 删除指定行
		/// </summary>
		/// <param name="x">行号</param>
		public void deleteRowAt(object x)
		{
			((Excel.Range)sheet.Cells[x, 1]).EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
		}

		/// <summary>
		/// 删除指定列
		/// </summary>
		/// <param name="y">列号</param>
		public void deleteColumnAt(object y)
		{
			((Excel.Range)sheet.Cells[1, y]).EntireColumn.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
		}

		/// <summary>
		/// 把剪切内容粘贴到当前区域
		/// </summary>
		public void past()
		{
			string s = "a,b,c,d,e,f,g";
			sheet.Paste(sheet.get_Range(this.GetAix(10, 10), miss), s);
		}

		/// <summary>
		/// 剪切粘贴，若结束单元格与开始单元格一直则输入空字符
		/// </summary>
		/// <param name="cutCell1">剪切开始单元格</param>
		/// <param name="cutCell2">剪切结束单元格，若结束单元格与开始单元格一直则输入空字符</param>
		/// <param name="pasteCell1">粘贴开始单元格</param>
		/// <param name="pasteCell2">粘贴结束单元格，若结束单元格与开始单元格一直则输入空字符</param>
		public void cutAndPaste(string cutCell1, string cutCell2, string pasteCell1, string pasteCell2)
		{
			Excel.Range range = null;
			if (string.IsNullOrEmpty(cutCell2))
			{
				range = sheet.get_Range(cutCell1, miss);
			}
			else
			{
				range = sheet.get_Range(cutCell1, cutCell2);
			}

			range.Cut(miss);
			if (string.IsNullOrEmpty(pasteCell2))
			{
				range = sheet.get_Range(pasteCell1, miss);
			}
			else
			{
				range = sheet.get_Range(pasteCell1, pasteCell2);
			}
			range.Select();
			sheet.Paste(miss, miss);

		}

		/// <summary>
		/// 复制粘贴
		/// </summary>
		/// <param name="cutCell1">剪切开始单元格</param>
		/// <param name="cutCell2">剪切结束单元格，若结束单元格与开始单元格一直则输入空字符</param>
		/// <param name="pasteCell1">粘贴开始单元格</param>
		/// <param name="pasteCell2">粘贴结束单元格，若结束单元格与开始单元格一直则输入空字符</param>
		public void copyAndPaste(string cutCell1, string cutCell2, string pasteCell1, string pasteCell2)
		{
			Excel.Range range = null;
			if (string.IsNullOrEmpty(cutCell2))
			{
				range = sheet.get_Range(cutCell1, miss);
			}
			else
			{
				range = sheet.get_Range(cutCell1, cutCell2);
			}

			range.Copy(miss);
			if (string.IsNullOrEmpty(pasteCell2))
			{
				range = sheet.get_Range(pasteCell1, miss);
			}
			else
			{
				range = sheet.get_Range(pasteCell1, pasteCell2);
			}
			range.Select();
			sheet.Paste(miss, miss);

		}

		/// <summary>
		/// 设置边框
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="Width"></param>
		public void setBorder(int x1, int y1, int x2, int y2, int Width)
		{
			Excel.Range range = sheet.get_Range(this.GetAix(x1, y1), this.GetAix(x2, y2));
			range.Borders.Weight = Width;
		}

		/// <summary>
		/// 合并单元格
		/// </summary>
		/// <param name="x1">开始行号</param>
		/// <param name="y1">开始列号</param>
		/// <param name="x2">结束行号</param>
		/// <param name="y2">结束列号</param>
		public void mergeCell(int x1, int y1, int x2, int y2)
		{
			Excel.Range range = sheet.get_Range(this.GetAix(y1, x1), this.GetAix(y2, x2));
			range.Merge(true);
		}

		/// <summary>
		/// 获得单元格区域
		/// </summary>
		/// <param name="x1">开始行号</param>
		/// <param name="y1">开始列号</param>
		/// <param name="x2">结束行号</param>
		/// <param name="y2">结束列号</param>
		/// <returns></returns>
		public Excel.Range getRange(int x1, int y1, int x2, int y2)
		{
			Excel.Range range = sheet.get_Range(this.GetAix(y1, x1), this.GetAix(y2, x2));
			return range;
		}

		/// <summary>
		/// 打开Excel文件
		/// </summary>
		/// <param name="filename">路径</param>
		public void OpenExcelFile(string filename)
		{
			UserControl(false);

			m_objExcel.Workbooks.Open(filename, miss, miss, miss, miss, miss, miss, miss,
										  miss, miss, miss, miss, miss, miss, miss);

			m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;

			m_objBook = m_objExcel.ActiveWorkbook;
			sheet = (Excel.Worksheet)m_objBook.ActiveSheet;
		}
        
		/// <summary>
		/// 加载Excel模板
		/// </summary>
		/// <param name="fileName">全路径</param>
		public void loadTemlate(string fileName)
		{
			UserControl(false);
			m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
			m_objBook = (Excel.Workbook)(m_objBooks.Add(fileName));
			sheet = (Excel.Worksheet)m_objBook.ActiveSheet;
		}

		/// <summary>
		/// 设置Application对象属性
		/// </summary>
		/// <param name="usercontrol"></param>
		public void UserControl(bool usercontrol)
		{
			if (m_objExcel == null) { return; }
			m_objExcel.UserControl = usercontrol;
			m_objExcel.DisplayAlerts = usercontrol;//是否弹出警告信息
			m_objExcel.Visible = usercontrol;
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void CreateExcelFile()
		{
			UserControl(false);
			m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
			m_objBook = (Excel.Workbook)(m_objBooks.Add(miss));
			sheet = (Excel.Worksheet)m_objBook.ActiveSheet;
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="FileName"></param>
		public void SaveAs(string FileName)
		{
			m_objBook.SaveAs(FileName, miss, miss, miss, miss,
			 miss, Excel.XlSaveAsAccessMode.xlNoChange,
			 Excel.XlSaveConflictResolution.xlLocalSessionChanges,
			 miss, miss, miss, miss);
			//m_objBook.Close(false, miss, miss); 
		}

		/// <summary>
		/// 另存为
		/// </summary>
		/// <param name="fileName"></param>
		public void SaveCopyAs(string fileName)
		{
			m_objBook.SaveCopyAs(fileName);
			m_objBook.Close(false, miss, miss);
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		public void ReleaseExcel()
		{
			m_objExcel.Quit();
			System.Runtime.InteropServices.Marshal.ReleaseComObject((object)m_objExcel);
			System.Runtime.InteropServices.Marshal.ReleaseComObject((object)m_objBooks);
			System.Runtime.InteropServices.Marshal.ReleaseComObject((object)m_objBook);
			System.Runtime.InteropServices.Marshal.ReleaseComObject((object)sheet);
			m_objExcel = null;
			m_objBooks = null;
			m_objBook = null;
			sheet = null;
			GC.Collect();
		}

		/// <summary>
		/// 强制关闭Excel进程
		/// </summary>
		/// <returns></returns>
		public bool KillAllExcelApp()
		{
			try
			{
				if (m_objExcel != null) // isRunning是判断xlApp是怎么启动的flag.
				{
					m_objExcel.Quit();
					System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);
					//释放COM组件，其实就是将其引用计数减1
					//System.Diagnostics.Process theProc;
					foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
					{
						//先关闭图形窗口。如果关闭失败...有的时候在状态里看不到图形窗口的excel了，
						//但是在进程里仍然有EXCEL.EXE的进程存在，那么就需要杀掉它:p
						if (theProc.CloseMainWindow() == false)
						{
							theProc.Kill();
						}
					}
					m_objExcel = null;
					return true;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}
		#endregion
	}
}

