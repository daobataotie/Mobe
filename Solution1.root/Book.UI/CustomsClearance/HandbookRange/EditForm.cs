using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.CustomsClearance;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.CustomsClearance.HandbookRange
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        BL.BGHandbookRangeManager _manager = new Book.BL.BGHandbookRangeManager();
        BL.BGHandbookRangeDetailManager _detailManager = new Book.BL.BGHandbookRangeDetailManager();
        Model.BGHandbookRange _BGHandbookRange;

        public EditForm()
        {
            InitializeComponent();

            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";
        }

        protected override bool HasRows()
        {
            return this._manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._manager.HasRowsAfter(this._BGHandbookRange);
        }

        protected override bool HasRowsPrev()
        {
            return this._manager.HasRowsBefore(this._BGHandbookRange);
        }

        protected override void MoveFirst()
        {
            this._BGHandbookRange = this._manager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._BGHandbookRange = this._manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.BGHandbookRange model = this._manager.GetNext(this._BGHandbookRange);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._BGHandbookRange = model;
        }

        protected override void MovePrev()
        {
            Model.BGHandbookRange model = this._manager.GetPrev(this._BGHandbookRange);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._BGHandbookRange = model;
        }

        protected override void AddNew()
        {
            this._BGHandbookRange = new Book.Model.BGHandbookRange();
            this._BGHandbookRange.BGHandbookRangeId = Guid.NewGuid().ToString();
            this._BGHandbookRange.BGHandbookRangeDate = DateTime.Now;
            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._BGHandbookRange == null)
                AddNew();
            else
            {
                if (this.action == "view")
                    this._BGHandbookRange = this._manager.Get(this._BGHandbookRange.BGHandbookRangeId);
            }
            this.txt_CompanyNameAndId.EditValue = this._BGHandbookRange.CompanyNameAndId;
            this.date_Invoice.EditValue = this._BGHandbookRange.BGHandbookRangeDate;
            this.txt_Employee.EditValue = this._BGHandbookRange.EmployeeId;
            this.txt_Tel.EditValue = this._BGHandbookRange.Tel;
            //this.cobProductType.EditValue = this._BGHandbookRange.ProductType;
            this.richTextBoxOpinion.Rtf = this._BGHandbookRange.Opinion;
            this.newChooseContorlAuditEmp.EditValue = this._BGHandbookRange.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._BGHandbookRange.AuditState);

            this._BGHandbookRange.DetailProducts = this._detailManager.SelectByBGHandbookId(this._BGHandbookRange.BGHandbookRangeId, "成品");
            this._BGHandbookRange.DetailMaterials = this._detailManager.SelectByBGHandbookId(this._BGHandbookRange.BGHandbookRangeId, "料件");
            this.bindingSourceProduct.DataSource = this._BGHandbookRange.DetailProducts;
            this.bindingSourceMaterial.DataSource = this._BGHandbookRange.DetailMaterials;
            if (this._BGHandbookRange.DetailProducts.Count > 0)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 0;
                this.xtraTabPage1.PageVisible = true;
                this.xtraTabPage2.PageVisible = false;
            }
            if (this._BGHandbookRange.DetailMaterials.Count > 0)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 1;
                this.xtraTabPage1.PageVisible = false;
                this.xtraTabPage2.PageVisible = true;
            }
            base.Refresh();
            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.gridView2.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    break;
            }
        }

        protected override void Save()
        {
            this._BGHandbookRange.CompanyNameAndId = this.txt_CompanyNameAndId.Text;
            this._BGHandbookRange.BGHandbookRangeDate = this.date_Invoice.DateTime;
            this._BGHandbookRange.EmployeeId = this.txt_Employee.Text;
            this._BGHandbookRange.Tel = this.txt_Tel.Text;
            //this._BGHandbookRange.ProductType = this.cobProductType.EditValue == null ? null : this.cobProductType.EditValue.ToString();
            this._BGHandbookRange.Opinion = this.richTextBoxOpinion.Rtf;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this._manager.Insert(this._BGHandbookRange);
                    break;

                case "update":
                    this._manager.Update(this._BGHandbookRange);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._BGHandbookRange == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._manager.Delete(this._BGHandbookRange.BGHandbookRangeId);
            this._BGHandbookRange = this._manager.GetNext(this._BGHandbookRange);
            if (this._BGHandbookRange == null)
            {
                this._BGHandbookRange = this._manager.GetLast();
            }
        }

        private void barSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.BGHandbookRange model = f.SelectItem as Model.BGHandbookRange;
                if (model != null)
                {
                    this._BGHandbookRange = model;
                    this.Refresh();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Model.BGHandbookRangeDetail detail = new Book.Model.BGHandbookRangeDetail();
            detail.BGHandbookRangeDetailId = Guid.NewGuid().ToString();
            this.bindingSourceProduct.Add(detail);
            this.bindingSourceProduct.Position = this.bindingSourceProduct.IndexOf(detail);
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceProduct.Current != null)
            {
                this._BGHandbookRange.DetailProducts.Remove(this.bindingSourceProduct.Current as Model.BGHandbookRangeDetail);
            }
            if (this.bindingSourceMaterial.Current != null)
            {
                this._BGHandbookRange.DetailMaterials.Remove(this.bindingSourceMaterial.Current as Model.BGHandbookRangeDetail);
            }
            this.gridControl1.RefreshDataSource();
            this.gridControl2.RefreshDataSource();
        }

        #region 导入导出

        private void barIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExcelClass.ExcelClass1 ec = new Book.UI.ExcelClass.ExcelClass1();
                ec.Open(openFileDialog1.FileName);

                try
                {
                    BL.V.BeginTransaction();
                    Model.BGHandbookRange bGHandbookRange = null;
                    this.action = "insert";

                    Model.BGHandbookRangeDetail detail = null;
                    for (int i = 1; i <= ec.wb.Worksheets.Count; i++)
                    {
                        bGHandbookRange = new Book.Model.BGHandbookRange();
                        bGHandbookRange.DetailProducts = new List<Model.BGHandbookRangeDetail>();
                        bGHandbookRange.DetailMaterials = new List<Model.BGHandbookRangeDetail>();
                        bGHandbookRange.BGHandbookRangeId = Guid.NewGuid().ToString();
                        bGHandbookRange.BGHandbookRangeDate = DateTime.Now;
                        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)ec.wb.Worksheets[i];
                        bGHandbookRange.CompanyNameAndId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[3, 1]).Text.ToString().Substring(((Microsoft.Office.Interop.Excel.Range)ws.Cells[3, 1]).Text.ToString().IndexOf('：') + 1);

                        if (openFileDialog1.FileName.Contains("成品"))
                        {
                            //if (ws.Name != "草稿")
                            //{
                            for (int j = 6; j < 10000; j++)
                            {
                                if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() == "经办关员意见：" || (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() == "" && ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString() == ""))
                                {
                                    break;
                                }
                                detail = new Book.Model.BGHandbookRangeDetail();
                                detail.BGHandbookRangeDetailId = Guid.NewGuid().ToString();
                                detail.BGHandbookRangeId = bGHandbookRange.BGHandbookRangeId;
                                if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() != "")
                                    detail.Id = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString();
                                if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString() != "")
                                    detail.ProductName = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString();
                                if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 3]).Text.ToString() != "")
                                    detail.ProductSpecification = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 3]).Text.ToString();
                                if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 4]).Text.ToString() != "")
                                    detail.ProductUnit = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 4]).Text.ToString();
                                if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 5]).Text.ToString() != "")
                                    detail.CompanyProductId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 5]).Text.ToString();
                                if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 6]).Text.ToString() != "")
                                    detail.CustomProductId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 6]).Text.ToString();
                                if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 7]).Text.ToString() != "")
                                    detail.Note = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 7]).Text.ToString();

                                detail.ProductType = "成品";
                                bGHandbookRange.DetailProducts.Add(detail);
                            }
                            //}
                            //else
                            //{
                            //    for (int j = 6; j < 10000; j++)
                            //    {
                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() == "经办关员意见：" || (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() == "" && ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString() == ""))
                            //        {
                            //            break;
                            //        }
                            //        detail = new Book.Model.BGHandbookRangeDetail();
                            //        detail.BGHandbookRangeDetailId = Guid.NewGuid().ToString();
                            //        detail.BGHandbookRangeId = bGHandbookRange.BGHandbookRangeId;
                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() != "")
                            //            detail.Id = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString();

                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString() != "")
                            //            detail.CustomId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString();

                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 3]).Text.ToString() != "")
                            //            detail.ProductName = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 3]).Text.ToString();
                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 4]).Text.ToString() != "")
                            //            detail.ProductSpecification = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 4]).Text.ToString();
                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 5]).Text.ToString() != "")
                            //            detail.ProductUnit = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 5]).Text.ToString();
                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 6]).Text.ToString() != "")
                            //            detail.CompanyProductId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 6]).Text.ToString();
                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 7]).Text.ToString() != "")
                            //            detail.CustomProductId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 7]).Text.ToString();
                            //        if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 8]).Text.ToString() != "")
                            //            detail.Note = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 8]).Text.ToString();

                            //        detail.ProductType = "成品";
                            //        bGHandbookRange.DetailProducts.Add(detail);
                            //    }
                            //}
                        }

                        else if (openFileDialog1.FileName.Contains("料件"))
                        {
                            if (ws.Name == "Sheet4")
                            {
                                continue;
                            }
                            else if (ws.Name == "申报要素")
                            {
                                continue;
                            }
                            else
                            {
                                for (int j = 6; j < 10000; j++)
                                {
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() == "经办关员意见：" || (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() == "" && ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString() == ""))
                                    {
                                        break;
                                    }
                                    detail = new Book.Model.BGHandbookRangeDetail();
                                    detail.BGHandbookRangeDetailId = Guid.NewGuid().ToString();
                                    detail.BGHandbookRangeId = bGHandbookRange.BGHandbookRangeId;
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString() != "")
                                        detail.Id = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 1]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString() != "")
                                        detail.ProductName = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 2]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 3]).Text.ToString() != "")
                                        detail.ProductSpecification = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 3]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 4]).Text.ToString() != "")
                                        detail.ProductUnit = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 4]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 5]).Text.ToString() != "")
                                        detail.CompanyProductId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 5]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 6]).Text.ToString() != "")
                                        detail.CustomProductId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 6]).Text.ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 7]).Text.ToString() != "")
                                        detail.Note = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[j, 7]).Text.ToString();

                                    detail.ProductType = "料件";
                                    bGHandbookRange.DetailMaterials.Add(detail);
                                }
                            }
                        }

                        bGHandbookRange.EmployeeId = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[4, 1]).Text.ToString().Substring(((Microsoft.Office.Interop.Excel.Range)ws.Cells[4, 1]).Text.ToString().IndexOf("：") + 1, 4).Trim();
                        bGHandbookRange.Tel = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[4, 1]).Text.ToString().Trim().Substring(((Microsoft.Office.Interop.Excel.Range)ws.Cells[4, 1]).Text.ToString().Trim().Length - 11);
                        this._manager.Insert(bGHandbookRange);
                    }
                    BL.V.CommitTransaction();
                    //this.bindingSourceProduct.DataSource = bGHandbookRange.DetailProducts;
                    //this.bindingSourceMaterial.DataSource = bGHandbookRange.DetailMaterials;
                    //this.gridControl1.RefreshDataSource();
                    //this.gridControl2.RefreshDataSource();
                    this.MoveLast();
                    this.action = "view";
                    this.Refresh();
                }
                catch
                {
                    BL.V.RollbackTransaction();
                    ec.Close();
                    throw;
                }
                ec.Close();
            }
        }

        private void barOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelClass.ExcelClass1 ex = new Book.UI.ExcelClass.ExcelClass1();
            ex.Create();

            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)ex.app.Application.Worksheets.get_Item(1);

            ex.UniteCells(sheet, 1, 1, 1, 7);
            ex.UniteCells(sheet, 2, 1, 2, 7);
            ex.UniteCells(sheet, 3, 1, 3, 7);
            ex.UniteCells(sheet, 4, 1, 4, 7);

            int a = this._BGHandbookRange.DetailProducts.Count;
            Range range;

            sheet.Cells[1, 1] = "                                                       编码：厦[ ]加归类___号";
            sheet.Cells[3, 1] = "企业名称及企业十位数代码：" + this._BGHandbookRange.CompanyNameAndId;
            sheet.Cells[4, 1] = "联系人及电话：" + this._BGHandbookRange.EmployeeId + this._BGHandbookRange.Tel;
            sheet.Cells[5, 1] = "序号";
            sheet.Cells[5, 3] = "规格型号";
            sheet.Cells[5, 4] = "计量单位";
            sheet.Cells[5, 5] = "企业申报商品编码";
            sheet.Cells[5, 6] = "海关归类商品编码";
            sheet.Cells[5, 7] = "备注";

            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                sheet.Cells[2, 1] = "电子化手册出口成品归类申报表";
                sheet.Cells[5, 2] = "成品名称";

                for (int i = 0; i < a; i++)
                {
                    sheet.Cells[i + 6, 1] = this._BGHandbookRange.DetailProducts[i].Id;
                    sheet.Cells[i + 6, 2] = this._BGHandbookRange.DetailProducts[i].ProductName;
                    sheet.Cells[i + 6, 3] = this._BGHandbookRange.DetailProducts[i].ProductSpecification;
                    sheet.Cells[i + 6, 4] = this._BGHandbookRange.DetailProducts[i].ProductUnit;
                    sheet.Cells[i + 6, 5] = this._BGHandbookRange.DetailProducts[i].CompanyProductId;
                    sheet.Cells[i + 6, 6] = this._BGHandbookRange.DetailProducts[i].CustomProductId;
                    sheet.Cells[i + 6, 7] = this._BGHandbookRange.DetailProducts[i].Note;
                }
            }
            else
            {
                sheet.Cells[2, 1] = "电子化手册进口料件归类申报表";
                sheet.Cells[5, 2] = "料件名称";

                a = this._BGHandbookRange.DetailMaterials.Count;
                for (int i = 0; i < a; i++)
                {
                    sheet.Cells[i + 6, 1] = this._BGHandbookRange.DetailMaterials[i].Id;
                    sheet.Cells[i + 6, 2] = this._BGHandbookRange.DetailMaterials[i].ProductName;
                    sheet.Cells[i + 6, 3] = this._BGHandbookRange.DetailMaterials[i].ProductSpecification;
                    sheet.Cells[i + 6, 4] = this._BGHandbookRange.DetailMaterials[i].ProductUnit;
                    sheet.Cells[i + 6, 5] = this._BGHandbookRange.DetailMaterials[i].CompanyProductId;
                    sheet.Cells[i + 6, 6] = this._BGHandbookRange.DetailMaterials[i].CustomProductId;
                    sheet.Cells[i + 6, 7] = this._BGHandbookRange.DetailMaterials[i].Note;
                }
            }
            //宽度
            ex.SetWidth(sheet, "A:A", 6);
            ex.SetWidth(sheet, "B:B", 12.25);
            ex.SetWidth(sheet, "C:C", 25);
            ex.SetWidth(sheet, "D:D", 9);
            ex.SetWidth(sheet, "E:E", 16);
            ex.SetWidth(sheet, "F:F", 16);
            ex.SetWidth(sheet, "G:G", 16);

            ex.UniteCells(sheet, a + 6, 1, a + 6, 2);
            ex.UniteCells(sheet, a + 8, 6, a + 8, 7);
            sheet.Cells[a + 6, 1] = "经办关员意见：" + this._BGHandbookRange.Opinion;
            sheet.Cells[a + 8, 6] = "         年    月    日";
            sheet.Cells[a + 9, 1] = "备注栏可填写货物的形态、性质、成分、加工程度、结构原理、功能用途等技术参数";
            sheet.Cells[a + 10, 1] = "注意事项：序号栏所填写序号应与备案资料库内商品序号相一致";

            ((Range)sheet.Columns["A:G", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Rows["1:1", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignRight;
            ((Range)sheet.Rows["2:2", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Rows["5:5", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ex.setBorder(sheet, 1, 5, 7, a + 8, 2);
            //ex.setBorder();

            range = sheet.get_Range("A1", "G" + (a + 5).ToString());
            range.WrapText = true;
            range.EntireRow.AutoFit();

            ex.app.Visible = true;
            ex.release_xlsObj();
            GC.Collect();
        }
        #endregion

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.BGHandbookRange.PRO_BGHandbookRangeId;
        }

        protected override int AuditState()
        {
            return this._BGHandbookRange.AuditState.HasValue ? this._BGHandbookRange.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "BGHandbookRange" + "," + this._BGHandbookRange.BGHandbookRangeId;
        }

        #endregion

    }
}


