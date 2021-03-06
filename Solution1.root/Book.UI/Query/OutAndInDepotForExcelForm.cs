﻿using System;
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
    public partial class OutAndInDepotForExcelForm : DevExpress.XtraEditors.XtraForm
    {
        public OutAndInDepotForExcelForm()
        {
            InitializeComponent();

            this.bindingSourceDepot.DataSource = new BL.DepotManager().Select();
            IList<string> handBookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in handBookIds)
            {
                this.cob_HandBookId.Properties.Items.Add(item);
            }
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
            try
            {
                Type objClassType = null;
                objClassType = Type.GetTypeFromProgID("Excel.Application");
                if (objClassType == null)
                {
                    MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                    return;
                }

                if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
                {
                    MessageBox.Show("日期区间不完整", "提示", MessageBoxButtons.OK);
                    return;
                }
                DateTime startDate = this.date_Start.DateTime.Date;
                DateTime endDate = this.date_End.DateTime.Date.AddDays(1).AddSeconds(-1);
                string startCategoryID = (this.btn_StartCategory.EditValue == null ? null : (this.btn_StartCategory.EditValue as Model.ProductCategory).Id);
                string endCategoryID = (this.btn_EndCategory.EditValue == null ? null : (this.btn_EndCategory.EditValue as Model.ProductCategory).Id);
                string depotId = this.lue_Depot.EditValue == null ? null : this.lue_Depot.EditValue.ToString();
                string bgHandBookId = "";
                if (!string.IsNullOrEmpty(this.cob_HandBookId.Text))
                {

                    string[] bgHandBookIds = this.cob_HandBookId.Text.Split(',');
                    foreach (var item in bgHandBookIds)
                    {
                        bgHandBookId += "'" + item.Trim() + "',";
                    }
                    bgHandBookId = bgHandBookId.TrimEnd(',');
                }

                var list = new BL.StockManager().SelectOutAndInDepot(startDate, endDate, startCategoryID, endCategoryID, depotId, bgHandBookId);
                if (list.Count == 0)
                {
                    MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                    return;
                }


                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 11]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 15;
                excel.Cells[1, 1] = "商品进出仓明细(" + this.date_End.DateTime.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 7], excel.Cells[1, 8]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "日期";
                excel.Cells[2, 2] = "单据类型";
                excel.Cells[2, 3] = "商品编号";
                excel.Cells[2, 4] = "商品名称";
                excel.Cells[2, 5] = "单据编号";
                excel.Cells[2, 6] = "客户订单编号";
                excel.Cells[2, 7] = "货位";
                excel.Cells[2, 8] = "数量";
                excel.Cells[2, 9] = "加工单";
                excel.Cells[2, 10] = "手册号";
                excel.Cells[2, 11] = "项号";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 11]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 4], excel.Cells[2, 4]).ColumnWidth = 50;


                List<Model.StockSeach> haveThreeCategory = list.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.StockSeach> haveTwoCategory = list.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.StockSeach> haveOneCategory = list.Where(P => string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();

                int row = 3;
                int col = 1;

                foreach (var item in haveThreeCategory.GroupBy(p => p.ProductCategoryName3))
                {
                    excel.Cells[row, 1] = item.Key;
                    excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 11]).Interior.Color = "255";    //红色

                    row++;

                    foreach (var stock in item)
                    {
                        excel.Cells[row, 1] = stock.InvoiceDate.Value.ToString("yyyy-MM-dd");
                        excel.Cells[row, 2] = stock.InvoiceType;
                        excel.Cells[row, 3] = stock.PId;
                        excel.Cells[row, 4] = stock.ProductName;
                        excel.Cells[row, 5] = stock.InvoiceNO;
                        excel.Cells[row, 6] = stock.CusXOId;
                        excel.Cells[row, 7] = stock.PositionName;
                        excel.Cells[row, 8] = stock.InvoiceQuantity;
                        excel.Cells[row, 9] = stock.PronoteHeaderID;
                        excel.Cells[row, 10] = stock.HandbookId;
                        excel.Cells[row, 11] = stock.HandbookProductId;

                        row++;
                    }
                    row++;
                }

                foreach (var item in haveTwoCategory.GroupBy(p => p.ProductCategoryName2))
                {
                    excel.Cells[row, 1] = item.Key;
                    excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 11]).Interior.Color = "255";    //红色

                    row++;

                    foreach (var stock in item)
                    {
                        excel.Cells[row, 1] = stock.InvoiceDate.Value.ToString("yyyy-MM-dd");
                        excel.Cells[row, 2] = stock.InvoiceType;
                        excel.Cells[row, 3] = stock.PId;
                        excel.Cells[row, 4] = stock.ProductName;
                        excel.Cells[row, 5] = stock.InvoiceNO;
                        excel.Cells[row, 6] = stock.CusXOId;
                        excel.Cells[row, 7] = stock.PositionName;
                        excel.Cells[row, 8] = stock.InvoiceQuantity;
                        excel.Cells[row, 9] = stock.PronoteHeaderID;
                        excel.Cells[row, 10] = stock.HandbookId;
                        excel.Cells[row, 11] = stock.HandbookProductId;

                        row++;
                    }
                    row++;
                }

                foreach (var item in haveOneCategory.GroupBy(p => p.ProductCategoryName1))
                {
                    excel.Cells[row, 1] = item.Key;
                    excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 11]).Interior.Color = "255";    //红色

                    row++;

                    foreach (var stock in item)
                    {
                        excel.Cells[row, 1] = stock.InvoiceDate.Value.ToString("yyyy-MM-dd");
                        excel.Cells[row, 2] = stock.InvoiceType;
                        excel.Cells[row, 3] = stock.PId;
                        excel.Cells[row, 4] = stock.ProductName;
                        excel.Cells[row, 5] = stock.InvoiceNO;
                        excel.Cells[row, 6] = stock.CusXOId;
                        excel.Cells[row, 7] = stock.PositionName;
                        excel.Cells[row, 8] = stock.InvoiceQuantity;
                        excel.Cells[row, 9] = stock.PronoteHeaderID;
                        excel.Cells[row, 10] = stock.HandbookId;
                        excel.Cells[row, 11] = stock.HandbookProductId;

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