using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.Query
{
    public partial class MaterialOutAndInDepotForExcelForm : DevExpress.XtraEditors.XtraForm
    {
        public MaterialOutAndInDepotForExcelForm()
        {
            InitializeComponent();
        }

        private void btn_StartCategory_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Settings.BasicData.ProductCategories.ChooseForm f = new Settings.BasicData.ProductCategories.ChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_StartCategory.EditValue = f.SelectedItem;
                this.btn_EndCategory.EditValue = f.SelectedItem;
            }
        }

        private void btn_EndCategory_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Settings.BasicData.ProductCategories.ChooseForm f = new Settings.BasicData.ProductCategories.ChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_EndCategory.EditValue = f.SelectedItem;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期区间不完整", "提示", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Type objClassType = null;
                objClassType = Type.GetTypeFromProgID("Excel.Application");
                if (objClassType == null)
                {
                    MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 5]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 10;
                excel.Cells[1, 1] = "原料进出仓明细(" + this.date_Start.DateTime.ToString("yyyy-MM-dd") + " ~ " + this.date_End.DateTime.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 5], excel.Cells[1, 5]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "日期";
                excel.Cells[2, 2] = "单据类型";
                excel.Cells[2, 3] = "商品名称";
                excel.Cells[2, 4] = "货位";
                excel.Cells[2, 5] = "数量";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 5]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 3], excel.Cells[2, 3]).ColumnWidth = 50;



                DateTime startDate = this.date_Start.DateTime.Date;
                DateTime endDate = this.date_End.DateTime.Date.AddDays(1).AddSeconds(-1);
                string startCategoryID = (this.btn_StartCategory.EditValue == null ? null : (this.btn_StartCategory.EditValue as Model.ProductCategory).Id);
                string endCategoryID = (this.btn_EndCategory.EditValue == null ? null : (this.btn_EndCategory.EditValue as Model.ProductCategory).Id);

                var list = new BL.StockManager().SelectOutAndInDepot(startDate, endDate, startCategoryID, endCategoryID);
                if (list.Count == 0)
                {
                    MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                    return;
                }

                List<Model.StockSeach> haveThreeCategory = list.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.StockSeach> haveTwoCategory = list.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.StockSeach> haveOneCategory = list.Where(P => string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();

                int row = 3;
                int col = 1;

                foreach (var item in haveThreeCategory.GroupBy(p => p.ProductCategoryName3))
                {
                    excel.Cells[row, 1] = item.Key;
                    excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 5]).Interior.Color = "255";    //红色

                    row++;

                    foreach (var stock in item)
                    {
                        excel.Cells[row, 1] = stock.InvoiceDate.Value.ToString("yyyy-MM-dd");
                        excel.Cells[row, 2] = stock.InvoiceType;
                        excel.Cells[row, 3] = stock.ProductName;
                        excel.Cells[row, 4] = stock.PositionName;
                        excel.Cells[row, 5] = stock.InvoiceQuantity;

                        row++;
                    }
                    row++;
                }

                foreach (var item in haveTwoCategory.GroupBy(p => p.ProductCategoryName2))
                {
                    excel.Cells[row, 1] = item.Key;
                    excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 5]).Interior.Color = "255";    //红色

                    row++;

                    foreach (var stock in item)
                    {
                        excel.Cells[row, 1] = stock.InvoiceDate.Value.ToString("yyyy-MM-dd");
                        excel.Cells[row, 2] = stock.InvoiceType;
                        excel.Cells[row, 3] = stock.ProductName;
                        excel.Cells[row, 4] = stock.PositionName;
                        excel.Cells[row, 5] = stock.InvoiceQuantity;

                        row++;
                    }
                    row++;
                }

                foreach (var item in haveOneCategory.GroupBy(p => p.ProductCategoryName1))
                {
                    excel.Cells[row, 1] = item.Key;
                    excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 5]).Interior.Color = "255";    //红色

                    row++;

                    foreach (var stock in item)
                    {
                        excel.Cells[row, 1] = stock.InvoiceDate.Value.ToString("yyyy-MM-dd");
                        excel.Cells[row, 2] = stock.InvoiceType;
                        excel.Cells[row, 3] = stock.ProductName;
                        excel.Cells[row, 4] = stock.PositionName;
                        excel.Cells[row, 5] = stock.InvoiceQuantity;

                        row++;
                    }
                    row++;
                }

                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = XlWindowState.xlMaximized;
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}