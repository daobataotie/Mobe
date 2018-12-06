using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class ListForm : BaseListForm
    {
        int tag = 0;
        int flag = 0;

        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.ProduceInDepotDetailManager();
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.flag = 1;
            this.bindingSource1.DataSource = (this.manager as BL.ProduceInDepotDetailManager).SelectList(null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, null, null, null, null, invoiceCusId, null, null, -1,null);
            this.gridControl1.RefreshDataSource();
            this.gridView1.OptionsBehavior.Editable = true;
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = (this.manager as BL.ProduceInDepotDetailManager).SelectList(null, null, DateTime.Now.Date.AddDays(-3), DateTime.Now, null, null, null, null, null, null, null, null, null, -1,null);

            double? procedureSum, checkoutsum;
            procedureSum = (this.manager as BL.ProduceInDepotDetailManager).select_SumPronoteHeaderWorkhouseDateRang(DateTime.Now.Date.AddDays(-3), global::Helper.DateTimeParse.EndDate, null, null);
            checkoutsum = (this.manager as BL.ProduceInDepotDetailManager).select_CheckOutSumPronoteHeaderWorkhouseDateRang(DateTime.Now.Date.AddDays(-3), global::Helper.DateTimeParse.EndDate, null, null);
            this.barButtonProduceSum.Caption += procedureSum + "   ";
            this.barButtonCheckSum.Caption += checkoutsum + "   ";
            if (procedureSum != 0)
                this.barButtonCheckPercent.Caption += ((procedureSum - (checkoutsum == null ? 0 : checkoutsum)) / procedureSum * 100).Value.ToString("F1") + "%";

            this.gridView1.OptionsBehavior.Editable = true;
        }

        //更改时间周期
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionProInDepotChooseForm condition = new Query.ConditionProInDepotChooseForm();
            if (condition.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionProInDepotChoose con = condition.Condition as Query.ConditionProInDepotChoose;
                this.bindingSource1.DataSource = (this.manager as BL.ProduceInDepotDetailManager).SelectList(con.StartPronoteHeader, con.EndPronoteHeader, con.StartDate, con.EndDate, con.Product, con.WorkHouse, con.MDepot, con.MDepotPosition, con.Id1, con.Id2, con.Cusxoid, con.Customer1, con.Customer2, con.ProductState, con.HandBookId);
                this.gridView1.GroupPanelText = "時間從 " + con.StartDate.ToString("yyyy年MM月dd日") + " 到 " + con.EndDate.ToString("yyyy年MM月dd日");
                double? procedureSum, checkoutsum;
                if (this.bindingSource1.DataSource == null)
                {
                    procedureSum = 0;
                    checkoutsum = 0;
                    this.barStaticItem1.Caption = "0";
                }
                else
                {
                    this.barStaticItem1.Caption = (this.bindingSource1.DataSource as IList<Model.ProduceInDepotDetail>).Count.ToString();
                    procedureSum = (this.manager as BL.ProduceInDepotDetailManager).select_SumPronoteHeaderWorkhouseDateRang(con.StartDate, con.EndDate, con.StartPronoteHeader == null ? null : con.StartPronoteHeader, con.WorkHouse == null ? null : con.WorkHouse.WorkHouseId);
                    checkoutsum = (this.manager as BL.ProduceInDepotDetailManager).select_CheckOutSumPronoteHeaderWorkhouseDateRang(con.StartDate, con.EndDate, con.StartPronoteHeader == null ? null : con.StartPronoteHeader, con.WorkHouse == null ? null : con.WorkHouse.WorkHouseId);
                }
                this.barButtonProduceSum.Caption = "總生產數量" + procedureSum + "   ";
                this.barButtonCheckSum.Caption = "總合格數量" + checkoutsum + "   ";
                if (procedureSum != 0)
                    this.barButtonCheckPercent.Caption = "總不良率" + ((procedureSum - (checkoutsum == null ? 0 : checkoutsum)) / procedureSum * 100).Value.ToString("F1") + "%";
                else
                    this.barButtonCheckPercent.Caption = "總不良率:0";

            }
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            Model.ProduceInDepot model = new BL.ProduceInDepotManager().Get((args[0] as Model.ProduceInDepotDetail) == null ? null : (args[0] as Model.ProduceInDepotDetail).ProduceInDepotId);
            args[0] = model;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        //列印数据
        private void barBtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<Model.ProduceInDepotDetail> details = this.bindingSource1.DataSource as IList<Model.ProduceInDepotDetail>;
            if (details != null || details.Count > 0)
            {
                string RowFilter = this.gridView1.RowFilter;
                if (RowFilter.Contains("WorkHousename"))
                {
                    string condition = RowFilter.Substring(RowFilter.IndexOf("'") + 1, RowFilter.LastIndexOf("'") - RowFilter.IndexOf("'") - 2);
                    //if (RowFilter.Contains("防雾") || RowFilter.Contains("强化"))
                    if ("强化/防雾".StartsWith(condition))
                    {
                        details = details.Where(d => d.WorkHousename == "强化/防雾").ToList();
                        ExportExcel(details, "防霧");
                    }
                    //else if (RowFilter.Contains("验片"))
                    else if ("验片".StartsWith(condition))
                    {
                        details = details.Where(d => d.WorkHousename == "验片").ToList();
                        ExportExcel(details, "品檢");
                    }
                    //else if (RowFilter.Contains("半成品组装"))
                    else if ("半成品组装".StartsWith(condition))
                    {
                        details = details.Where(d => d.WorkHousename == "半成品组装").ToList();
                        ExportExcel(details, "組A(半)");
                    }
                    //else if (RowFilter.Contains("成品组装"))
                    else if ("成品组装".StartsWith(condition))
                    {
                        details = details.Where(d => d.WorkHousename == "成品组装").ToList();
                        ExportExcel(details, "組A");
                    }
                    else if ("射出".StartsWith(condition))
                    {
                        details = details.Where(d => d.WorkHousename == "射出").ToList();
                        ExportExcelForSheChu(details);
                    }
                }
                else
                {
                    ExportExcel(details);
                }
            }
        }

        /// <summary>
        /// 射出日报表
        /// </summary>
        /// <param name="details"></param>
        private void ExportExcelForSheChu(IList<Model.ProduceInDepotDetail> details)
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

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 20]);
                r.MergeCells = true;//合并单元格

                //Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter = -4108;
                //Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium= -4138;
                //Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic= -4105;

                excel.Cells[1, 1] = "生产日报表";

                #region Set Header
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[2, 20]).HorizontalAlignment = -4108;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 20]).ColumnWidth = 12;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 2]).ColumnWidth = 20;
                excel.get_Range(excel.Cells[2, 3], excel.Cells[2, 3]).ColumnWidth = 30;
                excel.get_Range(excel.Cells[2, 11], excel.Cells[2, 12]).ColumnWidth = 20;

                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 20]).Interior.Color = 12566463;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 20]).RowHeight = 20;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 20]).Font.Size = 13;
                excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 20]).WrapText = true;
                excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 20]).EntireRow.AutoFit();

                excel.Cells[2, 1] = "入庫日期";
                excel.Cells[2, 2] = "入庫單號";
                excel.Cells[2, 3] = "產品名稱";
                excel.Cells[2, 4] = "公司部門";
                excel.Cells[2, 5] = "單位";
                excel.Cells[2, 6] = "生產數量";
                excel.Cells[2, 7] = "合計生產";
                excel.Cells[2, 8] = "合計合格";
                excel.Cells[2, 9] = "合計入庫";
                excel.Cells[2, 10] = "合計轉生產";
                excel.Cells[2, 11] = "加工單";
                excel.Cells[2, 12] = "客戶訂單號";
                excel.Cells[2, 13] = "生產數量";
                excel.Cells[2, 14] = "合格數量";
                excel.Cells[2, 15] = "轉生產數量";
                excel.Cells[2, 16] = "入庫數量";
                excel.Cells[2, 17] = "班別";
                excel.Cells[2, 18] = "機台";
                excel.Cells[2, 19] = "手册号";
                excel.Cells[2, 20] = "手册项号";

                #endregion

                for (int i = 0; i < details.Count; i++)
                {
                    excel.Cells[i + 3, 1] = details[i].mProduceInDepotDate.HasValue ? details[i].mProduceInDepotDate.Value.ToString("yyyy-MM-dd") : "";
                    excel.Cells[i + 3, 2] = details[i].ProduceInDepotId;
                    excel.Cells[i + 3, 3] = details[i].ProductName;
                    excel.Cells[i + 3, 4] = details[i].WorkHousename;
                    excel.Cells[i + 3, 5] = details[i].ProductUnit;
                    excel.Cells[i + 3, 6] = details[i].PronoteHeaderSum;
                    excel.Cells[i + 3, 7] = details[i].HeJiProceduresSum;
                    excel.Cells[i + 3, 8] = details[i].HeJiCheckOutSum;
                    excel.Cells[i + 3, 9] = details[i].HeJiProduceQuantity;
                    excel.Cells[i + 3, 10] = details[i].HeJiProduceTransferQuantity;
                    excel.Cells[i + 3, 11] = details[i].PronoteHeaderId;
                    excel.Cells[i + 3, 12] = details[i].CusXOId;
                    excel.Cells[i + 3, 13] = details[i].ProceduresSum;
                    excel.Cells[i + 3, 14] = details[i].CheckOutSum;
                    excel.Cells[i + 3, 15] = details[i].ProduceTransferQuantity;
                    excel.Cells[i + 3, 16] = details[i].ProduceQuantity;
                    excel.Cells[i + 3, 17] = details[i].BusinessHoursType;
                    excel.Cells[i + 3, 18] = details[i].Machine;
                    excel.Cells[i + 3, 19] = details[i].HandbookId;
                    excel.Cells[i + 3, 20] = details[i].HandbookProductId;
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

        /// <summary>
        /// 有条件导出
        /// </summary>
        /// <param name="details"></param>
        /// <param name="type"></param>
        private void ExportExcel(IList<Model.ProduceInDepotDetail> details, string type)
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

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 19]);
                r.MergeCells = true;//合并单元格

                //Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter = -4108;
                //Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium= -4138;
                //Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic= -4105;

                switch (type)
                {
                    case "防霧":
                        excel.Cells[1, 1] = "強化/防霧 工作日報表";
                        excel.Cells[3 + details.Count, 19] = "QR8-06-05-1";
                        break;
                    case "品檢":
                        excel.Cells[1, 1] = "品檢日報表";
                        excel.Cells[3 + details.Count, 19] = "QR8-03-01-1";
                        break;
                    case "組A(半)":
                        excel.Cells[1, 1] = "組裝半成品日報表";
                        excel.Cells[3 + details.Count, 19] = "QR8-03-02-1";
                        break;
                    case "組A":
                        excel.Cells[1, 1] = "成品组装日报表";
                        excel.Cells[3 + details.Count, 19] = "QR8-03-03-1";
                        break;
                }
                #region Set Header
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[2, 19]).HorizontalAlignment = -4108;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 19]).ColumnWidth = 12;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 2]).ColumnWidth = 20;
                excel.get_Range(excel.Cells[2, 3], excel.Cells[2, 3]).ColumnWidth = 40;
                excel.get_Range(excel.Cells[2, 11], excel.Cells[2, 12]).ColumnWidth = 20;

                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 19]).Interior.Color = 12566463;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 19]).RowHeight = 20;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 19]).Font.Size = 13;
                excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 19]).WrapText = true;
                excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 19]).EntireRow.AutoFit();

                excel.Cells[2, 1] = "入庫日期";
                excel.Cells[2, 2] = "入庫單號";
                excel.Cells[2, 3] = "產品名稱";
                excel.Cells[2, 4] = "型號";
                excel.Cells[2, 5] = "公司部門";
                excel.Cells[2, 6] = "單位";
                excel.Cells[2, 7] = "生產數量";
                excel.Cells[2, 8] = "合計生產";
                excel.Cells[2, 9] = "合計合格";
                excel.Cells[2, 10] = "合計入庫";
                excel.Cells[2, 11] = "合計轉生產";
                excel.Cells[2, 12] = "加工單";
                excel.Cells[2, 13] = "客戶訂單號";
                excel.Cells[2, 14] = "生產數量";
                excel.Cells[2, 15] = "合格數量";
                excel.Cells[2, 16] = "轉生產數量";
                excel.Cells[2, 17] = "入庫數量";
                excel.Cells[2, 18] = "手册号";
                excel.Cells[2, 19] = "手册项号";

                #endregion

                #region 取商品對應的母件型號
                foreach (var item in details)
                {
                    if (string.IsNullOrEmpty(item.CustomerProductName))
                    {
                        //item.CustomerProductName = (this.manager as BL.ProduceInDepotDetailManager).SelectCustomerProductNameByPronoteHeaderId(item.PronoteHeaderId);  如果一个订单里面多个商品同时用到某个子件，在物料需求里面该子件会合并计算为一笔，其实它对应有多个主件
                        item.CustomerProductName = new Help().GetCustomerProductNameByPronoteHeaderId(item.PronoteHeaderId, item.ProductId, item.HandbookProductId);
                    }
                }
                #endregion

                for (int i = 0; i < details.Count; i++)
                {
                    excel.Cells[i + 3, 1] = details[i].mProduceInDepotDate.HasValue ? details[i].mProduceInDepotDate.Value.ToString("yyyy-MM-dd") : "";
                    excel.Cells[i + 3, 2] = details[i].ProduceInDepotId;
                    excel.Cells[i + 3, 3] = details[i].ProductName;
                    excel.Cells[i + 3, 4] = details[i].CustomerProductName;
                    excel.Cells[i + 3, 5] = details[i].WorkHousename;
                    excel.Cells[i + 3, 6] = details[i].ProductUnit;
                    excel.Cells[i + 3, 7] = details[i].PronoteHeaderSum;
                    excel.Cells[i + 3, 8] = details[i].HeJiProceduresSum;
                    excel.Cells[i + 3, 9] = details[i].HeJiCheckOutSum;
                    excel.Cells[i + 3, 10] = details[i].HeJiProduceQuantity;
                    excel.Cells[i + 3, 11] = details[i].HeJiProduceTransferQuantity;
                    excel.Cells[i + 3, 12] = details[i].PronoteHeaderId;
                    excel.Cells[i + 3, 13] = details[i].CusXOId;
                    excel.Cells[i + 3, 14] = details[i].ProceduresSum;
                    excel.Cells[i + 3, 15] = details[i].CheckOutSum;
                    excel.Cells[i + 3, 16] = details[i].ProduceTransferQuantity;
                    excel.Cells[i + 3, 17] = details[i].ProduceQuantity;
                    excel.Cells[i + 3, 18] = details[i].HandbookId;
                    excel.Cells[i + 3, 19] = details[i].HandbookProductId;
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

        /// <summary>
        /// 无条件导出
        /// </summary>
        /// <param name="details"></param>
        private void ExportExcel(IList<Model.ProduceInDepotDetail> details)
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

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 19]);
                r.MergeCells = true;//合并单元格

                //Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter = -4108;
                //Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium= -4138;
                //Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic= -4105;

                excel.Cells[1, 1] = "日報表";
                excel.Cells[3 + details.Count, 19] = "QR8-06-03-1";

                #region Set Header
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[2, 19]).HorizontalAlignment = -4108;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 19]).ColumnWidth = 12;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 2]).ColumnWidth = 20;
                excel.get_Range(excel.Cells[2, 3], excel.Cells[2, 3]).ColumnWidth = 30;
                excel.get_Range(excel.Cells[2, 11], excel.Cells[2, 12]).ColumnWidth = 20;

                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 19]).Interior.Color = 12566463;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 19]).RowHeight = 20;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 19]).Font.Size = 13;
                excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 19]).WrapText = true;
                excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 19]).EntireRow.AutoFit();

                excel.Cells[2, 1] = "入庫日期";
                excel.Cells[2, 2] = "入庫單號";
                excel.Cells[2, 3] = "產品名稱";
                excel.Cells[2, 4] = "公司部門";
                excel.Cells[2, 5] = "單位";
                excel.Cells[2, 6] = "生產數量";
                excel.Cells[2, 7] = "合計生產";
                excel.Cells[2, 8] = "合計合格";
                excel.Cells[2, 9] = "合計入庫";
                excel.Cells[2, 10] = "合計轉生產";
                excel.Cells[2, 11] = "加工單";
                excel.Cells[2, 12] = "客戶訂單號";
                excel.Cells[2, 13] = "生產數量";
                excel.Cells[2, 14] = "合格數量";
                excel.Cells[2, 15] = "轉生產數量";
                excel.Cells[2, 16] = "入庫數量";
                excel.Cells[2, 17] = "不良率";
                excel.Cells[2, 18] = "手册号";
                excel.Cells[2, 19] = "手册项号";

                #endregion

                for (int i = 0; i < details.Count; i++)
                {
                    excel.Cells[i + 3, 1] = details[i].mProduceInDepotDate.HasValue ? details[i].mProduceInDepotDate.Value.ToString("yyyy-MM-dd") : "";
                    excel.Cells[i + 3, 2] = details[i].ProduceInDepotId;
                    excel.Cells[i + 3, 3] = details[i].ProductName;
                    excel.Cells[i + 3, 4] = details[i].WorkHousename;
                    excel.Cells[i + 3, 5] = details[i].ProductUnit;
                    excel.Cells[i + 3, 6] = details[i].PronoteHeaderSum;
                    excel.Cells[i + 3, 7] = details[i].HeJiProceduresSum;
                    excel.Cells[i + 3, 8] = details[i].HeJiCheckOutSum;
                    excel.Cells[i + 3, 9] = details[i].HeJiProduceQuantity;
                    excel.Cells[i + 3, 10] = details[i].HeJiProduceTransferQuantity;
                    excel.Cells[i + 3, 11] = details[i].PronoteHeaderId;
                    excel.Cells[i + 3, 12] = details[i].CusXOId;
                    excel.Cells[i + 3, 13] = details[i].ProceduresSum;
                    excel.Cells[i + 3, 14] = details[i].CheckOutSum;
                    excel.Cells[i + 3, 15] = details[i].ProduceTransferQuantity;
                    excel.Cells[i + 3, 16] = details[i].ProduceQuantity;
                    excel.Cells[i + 3, 17] = details[i].RejectionRate;
                    excel.Cells[i + 3, 18] = details[i].HandbookId;
                    excel.Cells[i + 3, 19] = details[i].HandbookProductId;
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

        //加工单查看
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            string pronoteHeaderid = (this.bindingSource1.Current as Model.ProduceInDepotDetail).PronoteHeaderId;
            Model.PronoteHeader d = new BL.PronoteHeaderManager().Get(pronoteHeaderid);
            if (d != null)
            {
                PronoteHeader.EditForm f = new Book.UI.produceManager.PronoteHeader.EditForm(d);
                f.ShowDialog();
            }
        }

        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            if (!(this.bindingSource1.Current as Model.ProduceInDepotDetail).PIsClose.HasValue || (this.bindingSource1.Current as Model.ProduceInDepotDetail).PIsClose.Value)
                return;

            if (MessageBox.Show("結 案后,此加工單將不能做領料,退料與入庫動作,是否繼續?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;


            this.gridView1.SetRowCellValue((this.bindingSource1.DataSource as IList<Model.ProduceInDepotDetail>).IndexOf(this.bindingSource1.Current as Model.ProduceInDepotDetail), this.gridColumn17, true);

            new BL.PronoteHeaderManager().UpdateHeaderIsClse((this.bindingSource1.Current as Model.ProduceInDepotDetail).PronoteHeaderId, true);
            MessageBox.Show("結案成功");
        }

    }
}