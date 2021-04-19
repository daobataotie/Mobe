using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;
using Book.UI.Settings.BasicData;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-4-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionMaterialChooseForm : ConditionAChooseForm
    {
        //*----------------------------------------------------------------
        // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
        //                     版權所有 圍著必究

        // 编 码 人: 刘永亮              完成时间:2011-01-20
        // 修改原因：
        // 修 改 人:                          修改时间:
        // 修改原因：
        // 修 改 人:                          修改时间:
        //----------------------------------------------------------------*/

        private ConditionMaterial condition;
        BL.ProduceMaterialdetailsManager detailManage = new Book.BL.ProduceMaterialdetailsManager();

        public ConditionMaterialChooseForm()
        {
            InitializeComponent();

            this.newChooseWorkHouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();

            //this.bindingSourcePronoteHeader.DataSource = new BL.PronoteHeaderManager().Select();
            //this.bindingSourceProduceMaterialID.DataSource = new BL.ProduceMaterialManager().Select();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddDays(-15);
            this.dateEditEndDate.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);


            IList<string> handBookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in handBookIds)
            {
                this.cob_HandBookId.Properties.Items.Add(item);
            }
        }

        private void ConditionMaterialChooseForm_Load(object sender, EventArgs e)
        {

        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionMaterial;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionMaterial();

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

            this.condition.ProduceMaterialId0 = this.buttonEditMaterial1.EditValue == null ? null : this.buttonEditMaterial1.EditValue.ToString();
            this.condition.ProduceMaterialId1 = this.buttonEditMaterial2.EditValue == null ? null : this.buttonEditMaterial2.EditValue.ToString();
            this.condition.Product0 = this.buttonEditProduct1.EditValue == null ? null : this.buttonEditProduct1.EditValue as Model.Product;
            this.condition.Product1 = this.buttonEditProduct2.EditValue == null ? null : this.buttonEditProduct2.EditValue as Model.Product;
            this.condition.PronoteHeaderId0 = this.buttonEditPronoteHeader1.EditValue == null ? null : this.buttonEditPronoteHeader1.Text;
            this.condition.PronoteHeaderId1 = this.buttonEditPronoteHeader2.EditValue == null ? null : this.buttonEditPronoteHeader2.Text;
            this.condition.DepartmentId0 = (this.newChooseWorkHouse.EditValue as Model.WorkHouse) == null ? null : (this.newChooseWorkHouse.EditValue as Model.WorkHouse).WorkHouseId;

            this.condition.CusInvoiceXOId = this.txtCusInvoiceXOId.Text;
            this.condition.HandBookId = this.cob_HandBookId.Text;
        }

        private void buttonEditProduct1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduct1.EditValue = form.SelectedItem as Model.Product;
                this.buttonEditProduct2.EditValue = form.SelectedItem as Model.Product;
            }
        }

        private void buttonEditProduct2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduct2.EditValue = form.SelectedItem as Model.Product;
        }

        private void buttonEditMaterial1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceMaterial.ChooseMaterialForm form = new produceManager.ProduceMaterial.ChooseMaterialForm();
            //Settings.StockLimitations.TakeMaterialChooseForm form = new Settings.StockLimitations.TakeMaterialChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditMaterial1.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceMaterialID;
                this.buttonEditMaterial2.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceMaterialID;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditMaterial2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceMaterial.ChooseMaterialForm form = new produceManager.ProduceMaterial.ChooseMaterialForm();
            //Settings.StockLimitations.TakeMaterialChooseForm form = new Settings.StockLimitations.TakeMaterialChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditMaterial2.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceMaterialID;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditPronoteHeader1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditPronoteHeader1.EditValue = form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditPronoteHeader2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditPronoteHeader2.EditValue = form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID;
            }
            GC.Collect();
            form.Dispose();
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            OnOK();

            IList<Model.ProduceMaterialdetails> source = this.detailManage.SelectBycondition2(condition.StartDate, condition.EndDate, condition.ProduceMaterialId0, condition.ProduceMaterialId1, condition.Product0, condition.Product1, condition.DepartmentId0, condition.DepartmentId1, condition.PronoteHeaderId0, condition.PronoteHeaderId1, condition.CusInvoiceXOId, condition.HandBookId);

            IList<Model.ProduceMaterialdetails> list = new List<Model.ProduceMaterialdetails>();
            if (this.chk_Baoshui.Checked)  //保税
            {
                //转换RTF的商品描述到普通String
                System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                foreach (var item in source)
                {
                    rtBox.Rtf = item.ProductDescription;

                    if (rtBox.Text.Trim() == "保税")
                        list.Add(item);
                }
            }
            else
                list = source;

            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.CustomerProductName))
                {
                    item.CustomerProductName = CommonHelp.GetCustomerProductNameByPronoteHeaderId(item.PronoteHeaderID, item.ProductId);
                }
            }

            if (list == null || list.Count == 0)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                #region 生产领料

                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
                sheet.Name = "生产领料单";

                sheet.Rows.AutoFit();
                sheet.Rows.WrapText = true;
                sheet.Rows.RowHeight = 15;
                sheet.Rows.Font.Size = 9;
                sheet.Columns.ColumnWidth = 13;

                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 14]).Interior.ColorIndex = 15;   //浅灰色
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 14]).HorizontalAlignment = -4108;
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[list.Count + 1, 14]).Borders.Value = 1;
                sheet.get_Range(sheet.Cells[2, 2], sheet.Cells[list.Count + 1, 2]).NumberFormat = "yyyy/MM/dd";
                sheet.get_Range(sheet.Cells[2, 5], sheet.Cells[list.Count + 1, 5]).NumberFormat = "@";
                sheet.get_Range(sheet.Cells[1, 4], sheet.Cells[1, 4]).ColumnWidth = 30;

                sheet.Cells[1, 1] = "编号";
                sheet.Cells[1, 2] = "领料日期";
                sheet.Cells[1, 3] = "商品编号";
                sheet.Cells[1, 4] = "商品名称";
                sheet.Cells[1, 5] = "客户型号";
                sheet.Cells[1, 6] = "手册号";
                sheet.Cells[1, 7] = "手册项号";
                sheet.Cells[1, 8] = "数量";
                sheet.Cells[1, 9] = "生产站";
                sheet.Cells[1, 10] = "已分配量";
                sheet.Cells[1, 11] = "客户订单编号";
                sheet.Cells[1, 12] = "库存";
                sheet.Cells[1, 13] = "描述";
                sheet.Cells[1, 14] = "来源单据";


                int row2 = 2;
                foreach (var item in list)
                {
                    sheet.Cells[row2, 1] = item.ProduceMaterialID;
                    sheet.Cells[row2, 2] = item.ProduceMaterialDate;
                    sheet.Cells[row2, 3] = item.PID;
                    sheet.Cells[row2, 4] = item.ProductName;
                    sheet.Cells[row2, 5] = item.CustomerProductName;
                    sheet.Cells[row2, 6] = item.HandbookId;
                    sheet.Cells[row2, 7] = item.HandbookProductId;
                    sheet.Cells[row2, 8] = item.Materialprocesedsum;
                    sheet.Cells[row2, 9] = item.WorkhouseName;
                    sheet.Cells[row2, 10] = item.Distributioned;
                    sheet.Cells[row2, 11] = item.CusXOId;
                    sheet.Cells[row2, 12] = item.ProductStock;
                    sheet.Cells[row2, 13] = item.ProduceMaterialdesc;
                    sheet.Cells[row2, 14] = item.PronoteHeaderID;

                    row2++;
                }

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
    }
}