using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Linq;

namespace Book.UI.Query
{
    public partial class ConditionPronoteHeaderChooseForm : ConditionAChooseForm
    {
        //Q49 生产加工明细表
        private ConditionPronoteHeader condition;
        private int FlagIsProcee;

        public ConditionPronoteHeaderChooseForm()
        {
            InitializeComponent();

            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();

            IList<string> bgHandbookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in bgHandbookIds)
            {
                this.ccb_BGHandBookIds.Properties.Items.Add(item);
            }

            this.coBoxSourceType.SelectedIndex = 0;
        }

        //public ConditionPronoteHeaderChooseForm(int flagIsProcee)
        //{
        //    InitializeComponent();

        //    this.FlagIsProcee = flagIsProcee;
        //    if (flagIsProcee == 0)
        //        this.coBoxSourceType.SelectedIndex = 1;
        //    else if (flagIsProcee == 1)
        //        this.coBoxSourceType.SelectedIndex = 2;
        //    else if (flagIsProcee == 2)
        //        this.coBoxSourceType.SelectedIndex = 3;

        //}

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionPronoteHeader;
            }
        }

        protected override void OnOK()
        {
            SetValue();
        }

        private void SetValue()
        {
            if (this.condition == null)
                this.condition = new ConditionPronoteHeader();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEditStartDate.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEditEndDate.DateTime;
            }
            this.condition.Product = this.buttonEditPro.EditValue as Model.Product;
            this.condition.PronoteHeaderIdStart = this.buttonEditProHeader1.Text == "" ? null : this.buttonEditProHeader1.Text;
            this.condition.PronoteHeaderIdEnd = this.buttonEditProHeader2.Text == "" ? null : this.buttonEditProHeader2.Text;
            this.condition.Customer = this.newChooseCustomer.EditValue as Model.Customer;
            if (this.coBoxSourceType.SelectedIndex == 0 || this.coBoxSourceType.SelectedIndex == -1)
                this.condition.SourceTpye = -1;
            else if (this.coBoxSourceType.SelectedIndex == 1)
                this.condition.SourceTpye = 0;
            else if (this.coBoxSourceType.SelectedIndex == 2)
                this.condition.SourceTpye = 5;
            else if (this.coBoxSourceType.SelectedIndex == 3)
                this.condition.SourceTpye = 4;

            this.condition.ProNameKey = this.TXTproNameKey.Text;
            this.condition.ProCusNameKey = this.TXTproCusNameKey.Text;
            this.condition.PronoteHeaderIdKey = this.txtpronoteHeaderIdKey.Text;
            this.condition.CusXOId = this.textEditCusXOId.Text;

            if (!string.IsNullOrEmpty(this.ccb_BGHandBookIds.Text))
            {
                string bgHandBookId = "";
                string[] bgHandBookIds = this.ccb_BGHandBookIds.Text.Split(',');
                foreach (var item in bgHandBookIds)
                {
                    bgHandBookId += "'" + item.Trim() + "',";
                }
                bgHandBookId = bgHandBookId.TrimEnd(',');
                this.condition.HandbookId = bgHandBookId;
            }
        }

        private void buttonEditPro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro.EditValue = form.SelectedItem as Model.Product;

            }
            form.Dispose();
            GC.Collect();
        }

        private void ConditionPronoteHeaderChooseForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonEditProHeader1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(0);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditProHeader1.Text = f.SelectItem.PronoteHeaderID;
                this.buttonEditProHeader2.Text = f.SelectItem.PronoteHeaderID;
            }
            f.Dispose();
            GC.Collect();

        }

        private void buttonEditProHeader2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(0);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditProHeader2.Text = f.SelectItem.PronoteHeaderID;
            }
            f.Dispose();
            GC.Collect();

        }

        //导出Excel
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            SetValue();

            IList<Book.Model.PronoteHeader> listPronoteHeader = new BL.PronoteHeaderManager().GetDataForExcel(condition.StartDate, condition.EndDate, condition.Customer, condition.CusXOId, condition.Product, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd, condition.SourceTpye, null, false, condition.ProNameKey, condition.ProCusNameKey, condition.PronoteHeaderIdKey, false, false, false, condition.HandbookId);

            if (listPronoteHeader == null || listPronoteHeader.Count == 0)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                return;
            }

            //转换RTF的商品描述到普通String
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();

            foreach (var item in listPronoteHeader)
            {
                rtBox.Rtf = item.ProductDesc;
                item.ProductDesc = rtBox.Text.Trim();

                if (string.IsNullOrEmpty(item.CustomerProductName))
                {
                    item.CustomerProductName = new produceManager.Help().GetCustomerProductNameByPronoteHeaderId(item, item.ProductId, item.HandbookProductId);
                }
            }

            var listPronoteHeaderFilter = this.chk_Baoshui.Checked ? listPronoteHeader.Where(l => l.ProductDesc == "保税") : listPronoteHeader;

            if (listPronoteHeaderFilter == null || listPronoteHeaderFilter.Count() == 0)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                return;
            }


            //根据加工单号查询对应的领料单
            //var listPronoteHeaderIds = listPronoteHeaderFilter.GroupBy(l => l.PronoteHeaderID).Select(g => g.Key);
            //string pronoteHeaderIds = string.Empty;
            //foreach (var item in listPronoteHeaderIds)
            //{
            //    pronoteHeaderIds += "'" + item + "',";
            //}

            //pronoteHeaderIds = pronoteHeaderIds.TrimEnd(',');
            //IList<Model.ProduceMaterialdetails> listProduceMaterialdetails = new BL.ProduceMaterialdetailsManager().GetDataByPronoteHeaders(pronoteHeaderIds);


            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                #region 生产加工单

                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
                sheet.Name = "生产加工单";
                Microsoft.Office.Interop.Excel.Range r = sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 16]);
                r.MergeCells = true;

                sheet.Rows.AutoFit();
                sheet.Rows.WrapText = true;
                sheet.Rows.RowHeight = 15;
                sheet.Rows.Font.Size = 9;
                sheet.Columns.ColumnWidth = 13;

                sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[2, 16]).Interior.ColorIndex = 15;   //浅灰色
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[2, 16]).HorizontalAlignment = -4108;  //居中对齐
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[listPronoteHeaderFilter.Count() + 2, 16]).Borders.Value = 1;
                sheet.get_Range(sheet.Cells[3, 4], sheet.Cells[listPronoteHeaderFilter.Count() + 2, 4]).NumberFormat = "yyyy/MM/dd";

                sheet.get_Range(sheet.Cells[2, 6], sheet.Cells[2, 6]).ColumnWidth = 30;
                sheet.get_Range(sheet.Cells[2, 10], sheet.Cells[2, 10]).ColumnWidth = 30;

                sheet.Cells[1, 1] = "生产加工单明细表";

                sheet.Cells[2, 1] = "客户订单编号";
                sheet.Cells[2, 2] = "客户型号";
                sheet.Cells[2, 3] = "加工单编号";
                sheet.Cells[2, 4] = "通知日期";
                sheet.Cells[2, 5] = "商品编号";
                sheet.Cells[2, 6] = "商品名称";
                sheet.Cells[2, 7] = "订单数量";
                sheet.Cells[2, 8] = "生产数量";
                sheet.Cells[2, 9] = "原料编号";
                sheet.Cells[2, 10] = "原料名称";
                sheet.Cells[2, 11] = "领料数量";
                sheet.Cells[2, 12] = "生产站";
                sheet.Cells[2, 13] = "手册号";
                sheet.Cells[2, 14] = "手册项号";
                sheet.Cells[2, 15] = "来源单据";
                sheet.Cells[2, 16] = "描述";

                int row = 3;
                foreach (var item in listPronoteHeaderFilter)
                {
                    sheet.Cells[row, 1] = item.CustomerInvoiceXOId;
                    sheet.Cells[row, 2] = item.CustomerProductName;
                    sheet.Cells[row, 3] = item.PronoteHeaderID;
                    sheet.Cells[row, 4] = item.PronoteDate;
                    sheet.Cells[row, 5] = item.Id;
                    sheet.Cells[row, 6] = item.ProductName;
                    sheet.Cells[row, 7] = item.InvoiceXODetailQuantity;
                    sheet.Cells[row, 8] = item.DetailsSum;
                    sheet.Cells[row, 9] = item.MaterialId;
                    sheet.Cells[row, 10] = item.MaterialName;
                    sheet.Cells[row, 11] = item.MaterialQty;
                    sheet.Cells[row, 12] = item.Workhousename;
                    sheet.Cells[row, 13] = item.HandbookId;
                    sheet.Cells[row, 14] = item.HandbookProductId;
                    sheet.Cells[row, 15] = item.MRSHeaderId;
                    sheet.Cells[row, 16] = item.ProductDesc;


                    row++;
                }

                #endregion


                #region 生产领料

                //if (listProduceMaterialdetails != null && listProduceMaterialdetails.Count > 0)
                //{
                //    excel.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                //    Microsoft.Office.Interop.Excel.Worksheet sheet2 = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[2];
                //    sheet2.Name = "生产领料";

                //    sheet2.Rows.AutoFit();
                //    sheet2.Rows.WrapText = true;
                //    sheet2.Rows.RowHeight = 15;
                //    sheet2.Rows.Font.Size = 9;
                //    sheet2.Columns.ColumnWidth = 13;

                //    sheet2.get_Range(sheet2.Cells[1, 1], sheet2.Cells[1, 14]).Interior.ColorIndex = 15;   //浅灰色
                //    sheet2.get_Range(sheet2.Cells[1, 1], sheet2.Cells[1, 14]).HorizontalAlignment = -4108;
                //    sheet2.get_Range(sheet2.Cells[1, 1], sheet2.Cells[listProduceMaterialdetails.Count + 1, 14]).Borders.Value = 1;
                //    sheet2.get_Range(sheet2.Cells[2, 2], sheet2.Cells[listProduceMaterialdetails.Count + 1, 2]).NumberFormat = "yyyy/MM/dd";
                //    sheet2.get_Range(sheet2.Cells[1, 4], sheet2.Cells[1, 4]).ColumnWidth = 30;

                //    sheet2.Cells[1, 1] = "编号";
                //    sheet2.Cells[1, 2] = "领料日期";
                //    sheet2.Cells[1, 3] = "商品编号";
                //    sheet2.Cells[1, 4] = "商品名称";
                //    sheet2.Cells[1, 5] = "客户型号";
                //    sheet2.Cells[1, 6] = "手册号";
                //    sheet2.Cells[1, 7] = "手册项号";
                //    sheet2.Cells[1, 8] = "数量";
                //    sheet2.Cells[1, 9] = "生产站";
                //    sheet2.Cells[1, 10] = "已分配量";
                //    sheet2.Cells[1, 11] = "客户订单编号";
                //    sheet2.Cells[1, 12] = "库存";
                //    sheet2.Cells[1, 13] = "描述";
                //    sheet2.Cells[1, 14] = "来源单据";


                //    int row2 = 2;
                //    foreach (var item in listProduceMaterialdetails)
                //    {
                //        sheet2.Cells[row2, 1] = item.ProduceMaterialID;
                //        sheet2.Cells[row2, 2] = item.ProduceMaterialDate;
                //        sheet2.Cells[row2, 3] = item.PID;
                //        sheet2.Cells[row2, 4] = item.ProductName;
                //        sheet2.Cells[row2, 5] = item.CustomerProductName;
                //        sheet2.Cells[row2, 6] = item.HandbookId;
                //        sheet2.Cells[row2, 7] = item.HandbookProductId;
                //        sheet2.Cells[row2, 8] = item.Materialprocessum;
                //        sheet2.Cells[row2, 9] = item.WorkhouseName;
                //        sheet2.Cells[row2, 10] = item.Distributioned;
                //        sheet2.Cells[row2, 11] = item.CusXOId;
                //        sheet2.Cells[row2, 12] = item.ProductStock;
                //        sheet2.Cells[row2, 13] = item.ProduceMaterialdesc;
                //        sheet2.Cells[row2, 14] = item.PronoteHeaderID;

                //        row2++;
                //    }
                //}

                #endregion


                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = XlWindowState.xlMaximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void FilterByProductDesc(IList<Book.Model.PronoteHeader> listPronoteHeader)
        {
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();

            foreach (var item in listPronoteHeader)
            {
                rtBox.Rtf = item.ProductDesc;
                item.ProductDesc = rtBox.Text.Trim();
            }
        }
    }
}