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

namespace Book.UI.produceManager.ProduceMaterialExit
{
    public partial class ConditionForList : Query.ConditionChooseForm
    {
        private ConditionForListCls condition;
        BL.ProduceMaterialExitDetailManager detailManage = new Book.BL.ProduceMaterialExitDetailManager();

        public override Book.UI.Query.Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                base.Condition = value as ConditionForListCls;
            }
        }

        public ConditionForList()
        {
            InitializeComponent();

            this.ncc_Workhouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();

            IList<string> handBookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in handBookIds)
            {
                this.cob_HandBook.Properties.Items.Add(item);
            }
        }

        private void ConditionForList_Load(object sender, EventArgs e)
        {
            this.StartdateEdit.DateTime = DateTime.Now.AddDays(-15).Date;
            this.EnddateEdit.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionForListCls();

            if (this.StartdateEdit.EditValue == null)
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            else
                this.condition.StartDate = this.StartdateEdit.DateTime;

            if (this.EnddateEdit.EditValue == null)
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            else
                this.condition.EndDate = this.EnddateEdit.DateTime;


            this.condition.StartPMEid = this.btn_StartPMEId.Text;
            this.condition.EndPMEid = this.btn_EndPMEId.Text;
            this.condition.StartPronoteHeaderId = this.btn_StartPNTId.Text;
            this.condition.EndPronoteHeaderId = this.btn_EndPNTId.Text;

            this.condition.StartProduct = this.btnEditStartProduct.EditValue as Model.Product;
            this.condition.EndProduct = this.btnEditEndProduct.EditValue as Model.Product;

            this.condition.WorkhouseId = this.ncc_Workhouse.EditValue == null ? null : (this.ncc_Workhouse.EditValue as Model.WorkHouse).WorkHouseId;
            this.condition.InvocieXOCusId = this.txt_InvoiceXOCusId.Text;
            this.condition.HandBookId = this.cob_HandBook.Text;
        }

        private void btnEditStartProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEditStartProduct.EditValue = f.SelectedItem as Model.Product;
                this.btnEditEndProduct.EditValue = f.SelectedItem as Model.Product;
            }
            f.Dispose();
            GC.Collect();
        }

        private void btnEditEndProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEditEndProduct.EditValue = f.SelectedItem as Model.Product;
            }
            f.Dispose();
            GC.Collect();
        }

        private void btn_StartPMEId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProduceMaterialExit form = new ChooseProduceMaterialExit();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (form.detailList != null)
                {
                    Model.ProduceMaterialExitDetail detail = form.detailList.FirstOrDefault(D => D.IsChecked == true);
                    if (detail != null)
                    {
                        this.btn_EndPMEId.EditValue = this.btn_StartPMEId.EditValue = detail.ProduceMaterialExitId;
                    }
                }
            }
        }

        private void btn_EndPMEId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProduceMaterialExit form = new ChooseProduceMaterialExit();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (form.detailList != null)
                {
                    Model.ProduceMaterialExitDetail detail = form.detailList.FirstOrDefault(D => D.IsChecked == true);
                    if (detail != null)
                    {
                        this.btn_EndPMEId.EditValue = detail.ProduceMaterialExitId;
                    }
                }
            }
        }

        private void btn_StartPNTId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.btn_EndPNTId.EditValue = this.btn_StartPNTId.EditValue = (form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID);
            }
            GC.Collect();
            form.Dispose();
        }

        private void btn_EndPNTId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.btn_EndPNTId.EditValue = (form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID);
            }
            GC.Collect();
            form.Dispose();
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            OnOK();

            System.Data.DataTable source = this.detailManage.SelectForExcel(condition.StartDate, condition.EndDate, condition.StartPMEid, condition.EndPMEid, condition.StartPronoteHeaderId, condition.EndPronoteHeaderId, condition.StartProduct, condition.EndProduct, condition.WorkhouseId, condition.InvocieXOCusId, condition.HandBookId);

            System.Data.DataTable dt = source.Clone();

            //转换RTF的商品描述到普通String
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            foreach (DataRow item in source.Rows)
            {
                rtBox.Rtf = item["ProductDescription"].ToString();
                item["ProductDescription"] = rtBox.Text.Trim();

                if (rtBox.Text.Trim() == "保税" && this.chk_Baoshui.Checked)
                    dt.Rows.Add(item.ItemArray);
            }

            if (!this.chk_Baoshui.Checked)  //保税
                dt = source;

            foreach (DataRow item in dt.Rows)
            {
                if (string.IsNullOrEmpty(item["CustomerProductName"].ToString()))
                {
                    item["CustomerProductName"] =  CommonHelp.GetCustomerProductNameByPronoteHeaderId(item["PronoteHeaderID"].ToString(), item["ProductId"].ToString());
                }
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
                sheet.Name = "生产加工领料退料明细";

                sheet.Rows.AutoFit();
                sheet.Rows.WrapText = true;
                sheet.Rows.RowHeight = 15;
                sheet.Rows.Font.Size = 9;
                sheet.Columns.ColumnWidth = 13;


                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 19]).Interior.ColorIndex = 15;   //浅灰色
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 19]).HorizontalAlignment = -4108;
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[dt.Rows.Count + 1, 19]).Borders.Value = 1;
                sheet.get_Range(sheet.Cells[2, 3], sheet.Cells[dt.Rows.Count + 1, 3]).NumberFormat = "yyyy/MM/dd";
                sheet.get_Range(sheet.Cells[2, 11], sheet.Cells[dt.Rows.Count + 1, 11]).NumberFormat = "yyyy/MM/dd";
                sheet.get_Range(sheet.Cells[2, 13], sheet.Cells[dt.Rows.Count + 1, 13]).NumberFormat = "yyyy/MM/dd";
                sheet.get_Range(sheet.Cells[2, 2], sheet.Cells[dt.Rows.Count + 1, 2]).NumberFormat = "@";
                sheet.get_Range(sheet.Cells[1, 6], sheet.Cells[1, 6]).ColumnWidth = 30;
                sheet.get_Range(sheet.Cells[1, 10], sheet.Cells[1, 10]).ColumnWidth = 30;

                sheet.Cells[1, 1] = "客户订单编号";
                sheet.Cells[1, 2] = "客户型号";
                sheet.Cells[1, 3] = "通知日期";
                sheet.Cells[1, 4] = "加工单编号";
                sheet.Cells[1, 5] = "商品编号";
                sheet.Cells[1, 6] = "商品名称";
                sheet.Cells[1, 7] = "订单数量";
                sheet.Cells[1, 8] = "生产数量";
                sheet.Cells[1, 9] = "原料编号";
                sheet.Cells[1, 10] = "原料名称";
                sheet.Cells[1, 11] = "领料日期";
                sheet.Cells[1, 12] = "领料数量";
                sheet.Cells[1, 13] = "退料日期";
                sheet.Cells[1, 14] = "退料数量";
                sheet.Cells[1, 15] = "生产站";
                sheet.Cells[1, 16] = "手册号";
                sheet.Cells[1, 17] = "手册项号";
                sheet.Cells[1, 18] = "来源单据";
                sheet.Cells[1, 19] = "描述";


                int row2 = 2;
                foreach (DataRow item in dt.Rows)
                {
                    sheet.Cells[row2, 1] = item["CustomerInvoiceXOId"].ToString();
                    sheet.Cells[row2, 2] = item["CustomerProductName"].ToString();
                    sheet.Cells[row2, 3] = item["PronoteDate"].ToString();
                    sheet.Cells[row2, 4] = item["PronoteHeaderID"].ToString();
                    sheet.Cells[row2, 5] = item["Id"].ToString();
                    sheet.Cells[row2, 6] = item["ProductName"].ToString();
                    sheet.Cells[row2, 7] = item["InvoiceXODetailQuantity"].ToString();
                    sheet.Cells[row2, 8] = item["DetailsSum"].ToString();
                    sheet.Cells[row2, 9] = item["MId"].ToString();
                    sheet.Cells[row2, 10] = item["MProductName"].ToString();
                    sheet.Cells[row2, 11] = item["ProduceMaterialDate"].ToString();
                    sheet.Cells[row2, 12] = item["Materialprocesedsum"].ToString();
                    sheet.Cells[row2, 13] = item["ProduceExitMaterialDate"].ToString();
                    sheet.Cells[row2, 14] = item["ProduceQuantity"].ToString();
                    sheet.Cells[row2, 15] = item["Workhousename"].ToString();
                    sheet.Cells[row2, 16] = item["HandbookId"].ToString();
                    sheet.Cells[row2, 17] = item["HandbookProductId"].ToString();
                    sheet.Cells[row2, 18] = item["MRSHeaderId"].ToString();
                    sheet.Cells[row2, 19] = item["ProductDescription"].ToString();

                    row2++;
                }


                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = XlWindowState.xlMaximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }
    }
}