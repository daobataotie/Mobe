using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.produceManager.ProduceInDepot
{
    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人: 马艳军             完成时间:2010-3-24
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        public static IList<Model.Pronotedetails> _pronotedetails = new List<Model.Pronotedetails>();
        Model.ProduceInDepot produceInDepot = new Book.Model.ProduceInDepot();
        BL.ProduceInDepotManager produceInDepotManager = new Book.BL.ProduceInDepotManager();
        BL.ProduceInDepotDetailManager produceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();
        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        private BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        private BL.BomParentPartInfoManager bomParentPartInfoManager = new BL.BomParentPartInfoManager();
        BL.SupplierProductManager _SupplierProductManager = new Book.BL.SupplierProductManager();

        int lastFlag = 0;
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceInDepot.PRO_ProduceInDepotId, new AA(Properties.Resources.RequireDataForId, this.textEditProduceInDepotId));
            this.requireValueExceptions.Add(Model.ProduceInDepot.PRO_WorkHouseId, new AA(Properties.Resources.WorkHouse, this.newChooseWorkHorseId));
            this.invalidValueExceptions.Add(Model.ProduceInDepot.PRO_ProduceInDepotId, new AA(Properties.Resources.EntityExists, this.textEditProduceInDepotId));
            this.invalidValueExceptions.Add(Model.ProduceInDepotDetail.PRO_HeJiProceduresSum, new AA("<合計生產數量>不得大於<前單位轉入數量>", this.gridControl1));
            this.action = "view";
            this.newChooseEmployee0.Choose = new ChooseEmployee();
            this.newChooseEmployee1.Choose = new ChooseEmployee();
            this.newChooseWorkHorseId.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.newChooseContorlDepot.Choose = new Invoices.ChooseDepot();
            //    string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            //    this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text); 
            this.EmpAudit.Choose = new ChooseEmployee();
        }

        public EditForm(Model.ProduceInDepot produceInDepot)
            : this()
        {
            this.produceInDepot = produceInDepot;
            this.produceInDepot.Details = this.produceInDepotDetailManager.Select(produceInDepot);
            this.action = "view";
            this.lastFlag = 1;
        }

        public EditForm(Model.ProduceInDepot produceInDepot, string action)
            : this()
        {
            this.produceInDepot = produceInDepot;
            this.produceInDepot.Details = this.produceInDepotDetailManager.Select(produceInDepot);
            this.action = action;
            if (this.action == "view")
                this.lastFlag = 1;
        }

        protected override void Save()
        {
            this.produceInDepot.ProduceInDepotId = this.textEditProduceInDepotId.Text;
            this.produceInDepot.ProduceInDepotDesc = this.textEditProduceInDepotDesc.Text;
            this.produceInDepot.WorkHouse = this.newChooseWorkHorseId.EditValue as Model.WorkHouse;
            if (this.produceInDepot.WorkHouse != null)
            {
                this.produceInDepot.WorkHouseId = this.produceInDepot.WorkHouse.WorkHouseId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceInDepotDate.DateTime, new DateTime()))
            {
                this.produceInDepot.ProduceInDepotDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.produceInDepot.ProduceInDepotDate = this.dateEditProduceInDepotDate.DateTime;
            }
            this.produceInDepot.Employee0 = (this.newChooseEmployee0.EditValue as Model.Employee);
            if (this.produceInDepot.Employee0 != null)
            {
                this.produceInDepot.Employee0Id = this.produceInDepot.Employee0.EmployeeId;
            }
            this.produceInDepot.Employee1 = (this.newChooseEmployee1.EditValue as Model.Employee);
            if (this.produceInDepot.Employee1 != null)
            {
                this.produceInDepot.Employee1Id = this.produceInDepot.Employee1.EmployeeId;
            }
            this.produceInDepot.Depot = this.newChooseContorlDepot.EditValue as Model.Depot;
            if (this.produceInDepot.Depot != null)
                this.produceInDepot.DepotId = this.produceInDepot.Depot.DepotId;
            //if (this.newChooseContorlDepot.EditValue != null)
            //{
            //    this.produceInDepot.Depot = this.newChooseContorlDepot.EditValue as Model.Depot;
            //    this.produceInDepot.DepotId = this.produceInDepot.Depot.DepotId;
            //}


            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.produceInDepotManager.Insert(this.produceInDepot);
                    break;

                case "update":
                    this.produceInDepotManager.Update(this.produceInDepot);
                    break;
            }
        }

        protected override void Delete()
        {

            if (this.produceInDepot == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.produceInDepotManager.Delete(this.produceInDepot);
            this.produceInDepot = this.produceInDepotManager.GetNext(this.produceInDepot);
            if (this.produceInDepot == null)
            {
                this.produceInDepot = this.produceInDepotManager.GetLast();
            }
        }

        public override void Refresh()
        {

            if (this.produceInDepot == null)
            {
                this.AddNew();
            }
            else
            {
                if (this.action == "view")
                {
                    this.produceInDepot = this.produceInDepotManager.GetDetails(produceInDepot.ProduceInDepotId);

                    foreach (var item in produceInDepot.Details)
                    {
                        item.CusXOId = this.pronoteHeaderManager.SelectCusXOIdByHeaderId(item.PronoteHeaderId);
                    }
                }
            }

            this.textEditProduceInDepotId.Text = this.produceInDepot.ProduceInDepotId;
            this.textEditProduceInDepotDesc.Text = this.produceInDepot.ProduceInDepotDesc;
            this.newChooseWorkHorseId.EditValue = this.produceInDepot.WorkHouse;
            this.newChooseContorlDepot.EditValue = this.produceInDepot.Depot;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.produceInDepot.ProduceInDepotDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceInDepotDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceInDepotDate.EditValue = this.produceInDepot.ProduceInDepotDate;
            }
            this.newChooseEmployee0.EditValue = this.produceInDepot.Employee0;
            this.newChooseEmployee1.EditValue = this.produceInDepot.Employee1;
            //this.newChooseContorlDepot.EditValue = this.produceInDepot.Depot;
            this.EmpAudit.EditValue = this.produceInDepot.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this.produceInDepot.AuditState);

            this.bindingSourceDetails.DataSource = this.produceInDepot.Details;
            //this.SettingForINumber();
            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.bar_BtnSearch.Enabled = false;
                    this.newChooseWorkHorseId.Enabled = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.bar_BtnSearch.Enabled = false;
                    this.newChooseWorkHorseId.Enabled = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.bar_BtnSearch.Enabled = true;
                    this.newChooseWorkHorseId.Enabled = true;
                    break;

            }
            this.textEditProduceInDepotId.Properties.ReadOnly = true;
            this.newChooseEmployee0.Enabled = false;
            this.newChooseEmployee1.Enabled = false;
            //this.dateEditProduceInDepotDate.Enabled = false;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(produceInDepot.ProduceInDepotId, 1);

            //return null;
        }

        private void bar_PrintIndepot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RO r = new RO(produceInDepot.ProduceInDepotId, 2);
            r.ShowPreviewDialog();
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

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 17]);
                r.MergeCells = true;//合并单元格

                //Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter = -4108;
                //Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium= -4138;
                //Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic= -4105;

                excel.Cells[1, 1] = "日報表";

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
                excel.Cells[2, 19] = "项号";

                #endregion

                for (int i = 0; i < details.Count; i++)
                {
                    excel.Cells[i + 3, 1] = this.produceInDepot.ProduceInDepotDate.HasValue ? this.produceInDepot.ProduceInDepotDate.Value.ToString("yyyy-MM-dd") : "";
                    excel.Cells[i + 3, 2] = details[i].ProduceInDepotId;
                    excel.Cells[i + 3, 3] = details[i].Product.ProductName;
                    excel.Cells[i + 3, 4] = this.produceInDepot.WorkHouse == null ? null : this.produceInDepot.WorkHouse.Workhousename;
                    excel.Cells[i + 3, 5] = details[i].ProductUnit;
                    excel.Cells[i + 3, 6] = details[i].PronoteHeaderSum;
                    excel.Cells[i + 3, 7] = details[i].HeJiProceduresSum;
                    excel.Cells[i + 3, 8] = details[i].HeJiCheckOutSum;
                    excel.Cells[i + 3, 9] = details[i].HeJiProduceQuantity;
                    excel.Cells[i + 3, 10] = details[i].HeJiProduceTransferQuantity;
                    excel.Cells[i + 3, 11] = details[i].PronoteHeaderId;

                    Model.PronoteHeader header = pronoteHeaderManager.Get(details[i].PronoteHeaderId);
                    if (header != null)
                    {
                        Model.InvoiceXO xo = invoiceXOManager.Get(header.InvoiceXOId);
                        if (xo != null)
                            excel.Cells[i + 3, 12] = xo.CustomerInvoiceXOId;
                    }
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


        protected override void MoveNext()
        {
            Model.ProduceInDepot produceInDepot = this.produceInDepotManager.GetNext(this.produceInDepot);
            if (produceInDepot == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.produceInDepot = this.produceInDepotManager.Get(produceInDepot.ProduceInDepotId);
        }

        protected override void MovePrev()
        {
            Model.ProduceInDepot produceInDepot = this.produceInDepotManager.GetPrev(this.produceInDepot);
            if (produceInDepot == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.produceInDepot = this.produceInDepotManager.Get(produceInDepot.ProduceInDepotId);
        }

        protected override void MoveFirst()
        {
            this.produceInDepot = this.produceInDepotManager.Get(this.produceInDepotManager.GetFirst() == null ? "" : this.produceInDepotManager.GetFirst().ProduceInDepotId);
        }

        protected override void MoveLast()
        {
            if (this.lastFlag == 1)
            {
                this.lastFlag = 0;
                return;
            }
            this.produceInDepot = this.produceInDepotManager.Get(this.produceInDepotManager.GetLast() == null ? "" : this.produceInDepotManager.GetLast().ProduceInDepotId);
        }

        protected override bool HasRows()
        {
            return this.produceInDepotManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceInDepotManager.HasRowsAfter(this.produceInDepot);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceInDepotManager.HasRowsBefore(this.produceInDepot);
        }

        protected override void AddNew()
        {
            this.repositoryItemComboBox1.Items.Clear();
            this.produceInDepot = new Model.ProduceInDepot();
            this.produceInDepot.ProduceInDepotDate = DateTime.Now;
            this.produceInDepot.Employee0 = BL.V.ActiveOperator.Employee;
            this.produceInDepot.ProduceInDepotId = this.produceInDepotManager.GetId();// Guid.NewGuid().ToString();

            this.produceInDepot.Details = new List<Model.ProduceInDepotDetail>();
            this.action = "insert";
            //if (this.action == "insert")
            //{
            //    Model.ProduceInDepotDetail detail = new Model.ProduceInDepotDetail();
            //    detail.ProduceInDepotDetailId = Guid.NewGuid().ToString();
            //    detail.ProduceQuantity = 0;
            //    detail.ProducePrice = 0;
            //    detail.ProductGuige = "";
            //    detail.ProduceMoney = 0;
            //    detail.ProduceInDepotPrice = 0;
            //    detail.Product = new Book.Model.Product();
            //    this.produceInDepot.Details.Add(detail);
            //    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            //}
        }

        //“+”
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.newChooseWorkHorseId.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.WorkHouse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this.produceInDepot.Details.Count > 0 && this.produceInDepot.Details[0] != null && string.IsNullOrEmpty(this.produceInDepot.Details[0].ProductId))
                    this.produceInDepot.Details.RemoveAt(0);
                Model.ProduceInDepotDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.ProduceInDepotDetail();
                        detail.ProduceInDepotDetailId = Guid.NewGuid().ToString();
                        detail.Product = this.productManager.Get(product.ProductId);
                        detail.ProductId = detail.Product.ProductId;
                        detail.ProductGuige = detail.Product.ProductSpecification;
                        detail.ProduceQuantity = 0;
                        if (string.IsNullOrEmpty(detail.Product.SupplierId))
                        {
                            detail.PriceRange = string.Empty;
                            detail.ProduceInDepotPrice = 0;
                        }
                        else
                        {
                            detail.PriceRange = _SupplierProductManager.GetPriceRangeForSupAndProduct(detail.Product.SupplierId, detail.ProductId);
                            detail.ProduceInDepotPrice = BL.SupplierProductManager.CountPrice(detail.PriceRange, 1);
                        }
                        detail.ProduceMoney = 0;
                        detail.ProceduresSum = 0;
                        detail.ProductUnit = detail.Product.ProduceUnit == null ? null : detail.Product.ProduceUnit.CnName;
                        detail.CheckOutSum = 0;
                        detail.RejectionRate = 0;
                        detail.Inumber = this.produceInDepot.Details.Count + 1;
                        //detail.PriceRange = this.produceInDepotDetailManager.GetSupplierProductPriceRange(detail.ProductId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).Workhousename);
                        this.produceInDepot.Details.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.ProduceInDepotDetail();
                    detail.ProduceInDepotDetailId = Guid.NewGuid().ToString();
                    detail.Product = this.productManager.Get((f.SelectedItem as Model.Product).ProductId);
                    detail.ProductId = detail.Product.ProductId;
                    detail.ProductGuige = detail.Product.ProductSpecification;
                    detail.ProduceQuantity = 0;
                    if (string.IsNullOrEmpty(detail.Product.SupplierId))
                    {
                        detail.PriceRange = string.Empty;
                        detail.ProduceInDepotPrice = 0;
                    }
                    else
                    {
                        detail.PriceRange = _SupplierProductManager.GetPriceRangeForSupAndProduct(detail.Product.SupplierId, detail.ProductId);
                        detail.ProduceInDepotPrice = BL.SupplierProductManager.CountPrice(detail.PriceRange, 1);
                    }
                    detail.ProduceMoney = 0;
                    detail.ProceduresSum = 0;
                    detail.ProductUnit = detail.Product.ProduceUnit == null ? null : detail.Product.ProduceUnit.CnName;
                    detail.CheckOutSum = 0;
                    detail.RejectionRate = 0;
                    detail.Inumber = this.produceInDepot.Details.Count + 1;
                    //detail.PriceRange = this.produceInDepotDetailManager.GetSupplierProductPriceRange(detail.ProductId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).Workhousename);
                    this.produceInDepot.Details.Add(detail);
                }
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                //this.SettingForINumber();
                this.gridControl1.RefreshDataSource();
                f.Dispose();
                GC.Collect();
            }
        }

        //"-"
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this.produceInDepot.Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceInDepotDetail);

                if (this.produceInDepot.Details.Count == 0)
                {
                    Model.ProduceInDepotDetail detail = new Model.ProduceInDepotDetail();
                    detail.ProduceInDepotDetailId = Guid.NewGuid().ToString();
                    detail.ProductGuige = "";
                    detail.ProduceQuantity = 0;
                    detail.ProduceInDepotPrice = 0;
                    detail.ProduceMoney = 0;
                    detail.ProduceInDepotPrice = 0;
                    detail.Inumber = this.produceInDepot.Details.Count + 1;
                    detail.Product = new Book.Model.Product();
                    this.produceInDepot.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.SettingForINumber();
                this.gridControl1.RefreshDataSource();
            }
        }

        //自定义列显示
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.ProduceInDepotDetail> details = this.bindingSourceDetails.DataSource as IList<Model.ProduceInDepotDetail>;
            //if (details == null || details.Count < 1) return;
            //Model.Product product = details[e.ListSourceRowIndex].Product;
            //if (product == null) return;
            //switch (e.Column.Name)
            //{
            //    case "ColProductId":
            //        e.DisplayText = product.Id;
            //        break;
            //    case "ColCusPro":
            //        e.DisplayText = product.CustomerProductName;
            //        break;
            //}
        }

        //列值改变时触发事件 
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProduceInDepotDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceInDepotDetail;
            if (e.Column == this.ColProductId || e.Column == this.gridColumn2 || e.Column == this.ColCusPro)
            {

                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.ProduceInDepotDetailId = Guid.NewGuid().ToString();
                    detail.ProduceQuantity = 0;
                    detail.ProducePrice = 0;
                    detail.ProduceMoney = 0;
                    detail.ProduceInDepotPrice = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.ProductGuige = p.ProductSpecification;

                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                    //this.SettingForINumber();
                }
                this.gridControl1.RefreshDataSource();
            }
        }


        //列值改变后触发事件 
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumnProceduresSum || e.Column == this.gridColumnCheckOutSum)
            {
                decimal proceduresSum = decimal.Zero;
                decimal checkOutSum = decimal.Zero;

                if (e.Column == this.gridColumnProceduresSum)
                {
                    decimal.TryParse(e.Value.ToString(), out proceduresSum);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnCheckOutSum).ToString(), out checkOutSum);
                }
                if (e.Column == this.gridColumnCheckOutSum)
                {
                    decimal.TryParse(e.Value.ToString(), out checkOutSum);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnProceduresSum).ToString(), out proceduresSum);
                }
                if (proceduresSum == 0)
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnRejectionRate, 0);
                else
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnRejectionRate, double.Parse(((proceduresSum - checkOutSum) / proceduresSum * 100).ToString("0.##")));
            }

            if (e.Column == this.ColProduceQuantity)
            {
                Model.ProduceInDepotDetail detail = this.bindingSourceDetails.Current as Model.ProduceInDepotDetail;
                //if (detail.PriceRange == null)
                //    detail.PriceRange = this.produceInDepotDetailManager.GetSupplierProductPriceRange(detail.ProductId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse) == null ? null : (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).Workhousename);

                decimal newPrice = 0;
                newPrice = BL.SupplierProductManager.CountPrice(detail.PriceRange, Convert.ToDouble((e.Value == null ? "0" : e.Value.ToString())));
                if (newPrice > 0)
                {
                    detail.ProduceInDepotPrice = newPrice;
                }
                detail.ProduceMoney = detail.ProduceInDepotPrice * decimal.Parse(e.Value == null ? "0" : e.Value.ToString());

                //decimal price = this.produceInDepotDetailManager.CountPrice(detail.PriceRange, double.Parse(e.Value.ToString()));
                //if (price != 0)
                //    detail.ProduceInDepotPrice = price;
                //if (detail.ProduceInDepotPrice == null)
                //    detail.ProduceInDepotPrice = 0;
            }

            if (e.Column == this.ColProduceInDepotPrice)
            {
                Model.ProduceInDepotDetail detail = this.bindingSourceDetails.Current as Model.ProduceInDepotDetail;

                detail.ProduceMoney = detail.ProduceInDepotPrice * decimal.Parse(e.Value.ToString());
            }
            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                #region 注释
                //if (this.gridView1.FocusedColumn.Name == "gridColumn3")
                //{
                //    Model.ProduceInDepotDetail detail = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceInDepotDetail;
                //    this.repositoryItemComboBox1.Items.Clear();
                //    if (detail != null)
                //    {
                //        if (detail.DepotId != null)
                //        {
                //            IList<Model.DepotPosition> unitList = depotPositionManager.Select(detail.DepotId);
                //            foreach (Model.DepotPosition item in unitList)
                //            {
                //                this.repositoryItemComboBox1.Items.Add(item.Id);
                //            }

                //        }
                //        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                //    }

                //}

                //if (this.gridView1.FocusedColumn.Name == "gridColumnProcedures")
                //{

                //  if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                //    {
                //        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceInDepotDetail).Product;

                //        this.repositoryComboBoxProcedures.Items.Clear();
                //        if (p != null)
                //        {
                //            Model.BomParentPartInfo bom = this.bomParentPartInfoManager.Get(p);
                //            Model.TechonlogyHeader th = new BL.TechonlogyHeaderManager().GetDetail(bom.TechonlogyHeaderId);                       

                //            if (th!=null)
                //            {                               
                //                foreach (Model.Technologydetails item in th.detail) 
                //                {
                //                    this.repositoryComboBoxProcedures.Items.Add(item.Procedures);                                   

                //                }
                //            }

                //        }
                //    }
                //}

                //if (this.gridView1.FocusedColumn.Name == "gridColumnPro")
                //{

                //    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                //    {
                //        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceInDepotDetail).Product;

                //        this.repositoryItemComboBoxPro.Items.Clear();
                //        if (p != null)
                //        {
                //           // this.repositoryItemComboBoxPro.Items.Add(p);
                //            IList<Model.Product> list = this.productManager.SelectProceProduct(p);
                //            if (list != null)
                //            {
                //                foreach (Model.Product item in list)
                //                {
                //                    this.repositoryItemComboBoxPro.Items.Add(item);

                //                }
                //            }

                //        }
                //    }
                //}
                #endregion

                if (this.gridView1.FocusedColumn.Name == "gridColumnProductUnit")
                {

                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceInDepotDetail).Product;

                        this.repositoryItemComboBox2.Items.Clear();
                        if (p != null)
                        {
                            if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                            {
                                BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                                IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
                                foreach (Model.ProductUnit item in unitList)
                                {
                                    this.repositoryItemComboBox2.Items.Add(item.CnName);
                                }

                            }

                        }
                    }
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourcDepot.DataSource = depotManager.Select();
            this.bindingSourceWorkHouse.DataSource = new BL.WorkHouseManager().Select();
        }

        //选择加工单
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (this.newChooseWorkHorseId.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.WorkHouse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(this.newChooseWorkHorseId.EditValue as Model.WorkHouse, 1);
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (this.produceInDepot.Details.Count > 0 && this.produceInDepot.Details[0].ProductId == null)
                this.produceInDepot.Details.Remove(this.produceInDepot.Details[0]);

            if (PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList.Count != 0)
            {
                foreach (Model.PronoteHeader Pronote in PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList)
                {
                    Model.ProduceInDepotDetail produceInDepotDetail = new Book.Model.ProduceInDepotDetail();
                    produceInDepotDetail.ProduceInDepotDetailId = Guid.NewGuid().ToString();
                    produceInDepotDetail.Product = this.productManager.Get(Pronote.ProductId);
                    produceInDepotDetail.HandbookId = Pronote.HandbookId;
                    produceInDepotDetail.HandbookProductId = Pronote.HandbookProductId;
                    if (produceInDepotDetail.Product != null)
                    {
                        produceInDepotDetail.ProductId = produceInDepotDetail.Product.ProductId;
                    }
                    produceInDepotDetail.ProductUnit = Pronote.ProductUnit;
                    produceInDepotDetail.ProduceInDepotId = this.produceInDepot.ProduceInDepotId;
                    produceInDepotDetail.ProduceQuantity = Pronote.DetailsSum - (Pronote.InDepotQuantity == null ? 0 : Pronote.InDepotQuantity);
                    produceInDepotDetail.PronoteHeaderId = Pronote.PronoteHeaderID;
                    produceInDepotDetail.HeJiProceduresSum = this.produceInDepotDetailManager.select_SumbyPronHeaderId(produceInDepotDetail.PronoteHeaderId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).WorkHouseId, produceInDepotDetail.ProductId);
                    produceInDepotDetail.HeJiCheckOutSum = this.produceInDepotDetailManager.select_CheckOutSumByPronHeaderId(produceInDepotDetail.PronoteHeaderId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).WorkHouseId, produceInDepotDetail.ProductId);
                    produceInDepotDetail.ProceduresSum = 0;
                    produceInDepotDetail.PronoteHeaderSum = Pronote.DetailsSum;
                    produceInDepotDetail.CheckOutSum = 0;
                    produceInDepotDetail.RejectionRate = 0;
                    produceInDepotDetail.Inumber = this.produceInDepot.Details.Count + 1;
                    if (newChooseWorkHorseId.EditValue != null)
                    {
                        produceInDepotDetail.beforeTransferQuantity = this.produceInDepotDetailManager.select_TransferSumyPronHeaderWorkHouse(produceInDepotDetail.PronoteHeaderId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).WorkHouseId, DateTime.Now.Date.AddDays(-1));
                        produceInDepotDetail.HeJiBeforeTransferQuantity = this.produceInDepotDetailManager.select_TransferSumyPronHeaderWorkHouse(produceInDepotDetail.PronoteHeaderId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).WorkHouseId, null);
                    }
                    //produceInDepotDetail.PriceRange = this.produceInDepotDetailManager.GetSupplierProductPriceRange(produceInDepotDetail.ProductId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).Workhousename);
                    produceInDepotDetail.PriceRange = _SupplierProductManager.GetPriceRangeForSupAndProduct(produceInDepotDetail.Product.SupplierId, produceInDepotDetail.ProductId);
                    produceInDepotDetail.ProduceInDepotPrice = BL.SupplierProductManager.CountPrice(produceInDepotDetail.PriceRange, (double)produceInDepotDetail.ProduceQuantity);
                    produceInDepotDetail.ProduceQuantity = 0;
                    //if (price != 0)
                    //    produceInDepotDetail.ProduceInDepotPrice = price;
                    //if (produceInDepotDetail.ProduceInDepotPrice == null)
                    //    produceInDepotDetail.ProduceInDepotPrice = 0;
                    produceInDepotDetail.ProduceMoney = produceInDepotDetail.ProduceInDepotPrice * (decimal)produceInDepotDetail.ProduceQuantity;

                    this.produceInDepot.Details.Add(produceInDepotDetail);

                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(produceInDepotDetail);
                    //this.SettingForINumber();
                }
                this.gridControl1.RefreshDataSource();
                produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList.Clear();
            }
            f.Dispose();
            GC.Collect();
        }

        //查询LIST
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProduceInDepotDetail currentModel = form.SelectItem as Model.ProduceInDepotDetail;
                if (currentModel != null)
                {
                    this.produceInDepot = this.produceInDepotManager.GetDetails(currentModel.ProduceInDepotId);
                    //  this.produceInDepot.Details = this.produceInDepotDetailManager.Select(this.produceInDepot);
                    this.Refresh();
                }
            }
        }

        private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseContorlDepot.EditValue != null)
                this.bindingSourceDepotPositionId.DataSource = depotPositionManager.Select(this.newChooseContorlDepot.EditValue as Model.Depot);
        }

        //打印月统计表
        private void barBtnPrintMonth_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region 2017年8月27日22:06:33 不用了
            //Query.ConditionProInDepotChooseForm f = new Book.UI.Query.ConditionProInDepotChooseForm();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    Query.ConditionProInDepotChoose condition = f.Condition as Query.ConditionProInDepotChoose;
            //    IList<Model.ProduceInDepotDetail> list = this.produceInDepotDetailManager.Select(condition.StartPronoteHeader, condition.EndPronoteHeader, condition.StartDate, condition.EndDate, condition.Product, condition.WorkHouse, condition.MDepot, condition.MDepotPosition, condition.Id1, condition.Id2, condition.Cusxoid, condition.Customer1, condition.Customer2, condition.ProductState);
            //    if (list == null || list.Count <= 0)
            //    {
            //        MessageBox.Show("無符合條件數據記錄");
            //    }
            //    else
            //    {
            //        var sumList = from Model.ProduceInDepotDetail item in list
            //                      group item by item.ProductId;
            //        IList<Model.ProduceInDepotDetail> printlist = new List<Model.ProduceInDepotDetail>();
            //        foreach (IGrouping<string, Model.ProduceInDepotDetail> g in sumList)
            //        {
            //            Model.ProduceInDepotDetail d = new Book.Model.ProduceInDepotDetail();
            //            d.ProductId = g.First().ProductId;
            //            d.Product = g.First().Product;
            //            d.ProduceInDepotId = g.First().ProduceInDepotId;
            //            d.ProduceInDepot = g.First().ProduceInDepot;
            //            d.ProceduresSum = (from i in g select i.ProceduresSum).Sum();
            //            d.CheckOutSum = (from i in g select i.CheckOutSum).Sum();
            //            string nocheck = (d.NoHegeQuantity / (d.ProceduresSum.HasValue && d.ProceduresSum != 0 ? d.ProceduresSum : 1)).Value.ToString("0.#%");
            //            d.RejectionRate_1 = nocheck;
            //            d.ProductUnit = g.First().ProductUnit;
            //            d.HeiDian = (from i in g select i.HeiDian).Sum();
            //            d.ZaZhi = (from i in g select i.ZaZhi).Sum();
            //            d.mPaoguanwenti = (from i in g select i.mPaoguanwenti).Sum();
            //            d.mJingdiangudingdian = (from i in g select i.mJingdiangudingdian).Sum();
            //            d.mJingdiangudingdian = (from i in g select i.mJingdiangudingdian).Sum();
            //            d.mWanMocashang = (from i in g select i.mWanMocashang).Sum();
            //            d.mSuoShui = (from i in g select i.mSuoShui).Sum();
            //            d.mHuabancashang = (from i in g select i.mHuabancashang).Sum();
            //            d.mQianghuafangwuxian = (from i in g select i.mQianghuafangwuxian).Sum();
            //            d.mBaiyanHeiYan = (from i in g select i.mBaiyanHeiYan).Sum();
            //            d.HeiDian = (from i in g select i.HeiDian).Sum();
            //            d.mJieHeXianHuiwen = (from i in g select i.mJieHeXianHuiwen).Sum();
            //            d.mYuanliaowenti = (from i in g select i.mYuanliaowenti).Sum();
            //            d.mQiPao = (from i in g select i.mQiPao).Sum();
            //            d.mShechuqita = (from i in g select i.mShechuqita).Sum();
            //            d.mGuaiShouZhuangShang = (from i in g select i.mGuaiShouZhuangShang).Sum();
            //            d.mChaipiancashang = (from i in g select i.mChaipiancashang).Sum();
            //            d.mCaMoSunHua = (from i in g select i.mCaMoSunHua).Sum();
            //            d.mChouliaowenti = (from i in g select i.mChouliaowenti).Sum();
            //            d.mHeidianzazhi = (from i in g select i.mHeidianzazhi).Sum();
            //            d.mQianghuaqiancashang = (from i in g select i.mQianghuaqiancashang).Sum();
            //            d.mKeLimianxu = (from i in g select i.mKeLimianxu).Sum();
            //            d.mLiuheng = (from i in g select i.mLiuheng).Sum();
            //            d.mPengYaodiyao = (from i in g select i.mPengYaodiyao).Sum();
            //            d.mGuohuojizhua = (from i in g select i.mGuohuojizhua).Sum();
            //            d.mYoudian = (from i in g select i.mYoudian).Sum();
            //            d.mChangshangbuliang = (from i in g select i.mChangshangbuliang).Sum();
            //            d.mQianghuaQiTa = (from i in g select i.mQianghuaQiTa).Sum();
            //            d.mZuzhuangcashang = (from i in g select i.mZuzhuangcashang).Sum();
            //            d.mHanyao = (from i in g select i.mHanyao).Sum();
            //            d.mCashang = (from i in g select i.mCashang).Sum();
            //            d.mQianghuahoucashang = (from i in g select i.mQianghuahoucashang).Sum();

            //            printlist.Add(d);
            //        }
            //        //ROProuceInDepotDefectRate qsp = new ROProuceInDepotDefectRate(printlist, condition.ProductState, condition.StartDate.Year.ToString() + "年" + condition.StartDate.Month.ToString() + "月" + condition.StartDate.Day.ToString() + "~" + condition.EndDate.Year.ToString() + "年" + condition.EndDate.Month.ToString() + "月" + condition.EndDate.Day.ToString());
            //        //qsp.ShowPreviewDialog();
            //    }
            //}
            #endregion

            Query.ConditionProInDepotChooseForm f = new Book.UI.Query.ConditionProInDepotChooseForm();
            f.ShowDialog(this);
        }

        //设置排列序号
        public void SettingForINumber()
        {
            IList<Model.ProduceInDepotDetail> mDetails = this.bindingSourceDetails.DataSource as IList<Model.ProduceInDepotDetail>;
            for (int i = 0; i < mDetails.Count; i++)
            {
                mDetails[i].Inumber = i + 1;
            }
        }

        //弹出备注
        private void textEditProduceInDepotDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditProduceInDepotDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        //多条件导出
        private void barButtonItemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionProInDepotChooseForm f = new Book.UI.Query.ConditionProInDepotChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionProInDepotChoose condition = f.Condition as Query.ConditionProInDepotChoose;
                IList<Model.ProduceInDepotDetail> list = this.produceInDepotDetailManager.SelectList(condition.StartPronoteHeader, condition.EndPronoteHeader, condition.StartDate, condition.EndDate, condition.Product, condition.WorkHouse, condition.MDepot, condition.MDepotPosition, condition.Id1, condition.Id2, condition.Cusxoid, condition.Customer1, condition.Customer2, condition.ProductState, condition.HandBookId);
                if (list == null || list.Count <= 0)
                {
                    MessageBox.Show("無符合條件數據記錄");
                }
                else
                {
                    DataSet ds = new DataSet(); ;
                    string productname = string.Empty;

                    var compacts = from cs in list
                                   orderby cs.ProduceInDepotId
                                   group cs by cs.ProductId;
                    int a = 0;
                    foreach (IGrouping<string, Model.ProduceInDepotDetail> groups in compacts)
                    {
                        //处理相同项
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt.Columns.Add("商品", typeof(string));
                        dt.Columns.Add("日期", typeof(string));
                        dt.Columns.Add("數量", typeof(string));
                        dt.Columns.Add("合格數量", typeof(string));
                        dt.Columns.Add("不合格數量", typeof(string));
                        dt.Columns.Add("不良率", typeof(string));
                        dt.Columns.Add("單位", typeof(string));
                        dt.Columns.Add("原料問題", typeof(string));
                        dt.Columns.Add("抽料問題", typeof(string));
                        dt.Columns.Add("砲管問題", typeof(string));
                        dt.Columns.Add("晶點固定點", typeof(string));
                        dt.Columns.Add("插片擦傷", typeof(string));
                        dt.Columns.Add("挽模擦傷", typeof(string));
                        dt.Columns.Add("怪手撞傷", typeof(string));
                        dt.Columns.Add("滑板擦傷", typeof(string));
                        dt.Columns.Add("過火雞爪", typeof(string));
                        dt.Columns.Add("白煙黑煙", typeof(string));
                        dt.Columns.Add("結合線回紋冷料噴須", typeof(string));
                        dt.Columns.Add("縮水", typeof(string));
                        dt.Columns.Add("氣泡", typeof(string));
                        dt.Columns.Add("射出其他", typeof(string));
                        dt.Columns.Add("擦模損壞", typeof(string));
                        dt.Columns.Add("拆片擦傷", typeof(string));
                        dt.Columns.Add("黑點雜質", typeof(string));
                        //新添加
                        dt.Columns.Add("強化前擦傷", typeof(string));
                        dt.Columns.Add("強化後擦傷", typeof(string));
                        dt.Columns.Add("含藥", typeof(string));
                        dt.Columns.Add("顆粒棉絮", typeof(string));
                        dt.Columns.Add("流痕", typeof(string));
                        dt.Columns.Add("噴藥滴藥", typeof(string));
                        dt.Columns.Add("強化防霧線", typeof(string));
                        dt.Columns.Add("油點", typeof(string));
                        dt.Columns.Add("強化其他", typeof(string));
                        dt.Columns.Add("廠商不良", typeof(string));
                        dt.Columns.Add("組裝擦傷", typeof(string));
                        dt.Columns.Add("擦傷", typeof(string));

                        //dt.Columns.Add("其他", typeof(string));
                        //dt.Columns.Add("擦模次數", typeof(string));
                        // dt.Columns.Add("不良品數量", typeof(string));
                        a++;
                        var mGroups = from Model.ProduceInDepotDetail md in groups
                                      group md by md.ProduceInDepotDate.HasValue ? md.ProduceInDepotDate.Value.ToShortDateString() : "";
                        foreach (IGrouping<string, Model.ProduceInDepotDetail> mDateGroups in mGroups)
                        {
                            //var QueryEx = from Model.ProduceInDepotDetail detail in mDateGroups
                            //              select new Model.ProduceInDepotDetail
                            //              {

                            Model.ProduceInDepotDetail cc = new Book.Model.ProduceInDepotDetail();

                            //cc.Product = mDateGroups.First().Product;
                            //cc.ProduceInDepot = mDateGroups.First().ProduceInDepot;
                            cc.ProceduresSum = (from dd in mDateGroups select dd.ProceduresSum).Sum();
                            cc.CheckOutSum = (from dd in mDateGroups select dd.CheckOutSum).Sum();
                            cc.RejectionRate = (from dd in mDateGroups select dd.RejectionRate).Sum();
                            cc.ProductUnit = mDateGroups.First().ProductUnit;
                            cc.mPaoguanwenti = (from dd in mDateGroups select dd.mPaoguanwenti).Sum();
                            cc.mJingdiangudingdian = (from dd in mDateGroups select dd.mJingdiangudingdian).Sum();
                            cc.mChapiancashang = (from dd in mDateGroups select dd.mChapiancashang).Sum();
                            cc.mWanMocashang = (from dd in mDateGroups select dd.mWanMocashang).Sum();
                            cc.mSuoShui = (from dd in mDateGroups select dd.mSuoShui).Sum();
                            cc.mHuabancashang = (from dd in mDateGroups select dd.mHuabancashang).Sum();
                            cc.mQianghuafangwuxian = (from dd in mDateGroups select dd.mQianghuafangwuxian).Sum();
                            cc.mBaiyanHeiYan = (from dd in mDateGroups select dd.mBaiyanHeiYan).Sum();
                            cc.mJieHeXianHuiwen = (from dd in mDateGroups select dd.mJieHeXianHuiwen).Sum();
                            cc.mYuanliaowenti = (from dd in mDateGroups select dd.mYuanliaowenti).Sum();
                            cc.mQiPao = (from dd in mDateGroups select dd.mQiPao).Sum();
                            cc.mShechuqita = (from dd in mDateGroups select dd.mShechuqita).Sum();
                            cc.mGuaiShouZhuangShang = (from dd in mDateGroups select dd.mGuaiShouZhuangShang).Sum();
                            cc.mChaipiancashang = (from dd in mDateGroups select dd.mChaipiancashang).Sum();
                            cc.mCaMoSunHua = (from dd in mDateGroups select dd.mCaMoSunHua).Sum();
                            cc.mChouliaowenti = (from dd in mDateGroups select dd.mChouliaowenti).Sum();
                            cc.mHeidianzazhi = (from dd in mDateGroups select dd.mHeidianzazhi).Sum();
                            cc.mQianghuaqiancashang = (from dd in mDateGroups select dd.mQianghuaqiancashang).Sum();
                            cc.mKeLimianxu = (from dd in mDateGroups select dd.mKeLimianxu).Sum();
                            cc.mLiuheng = (from dd in mDateGroups select dd.mLiuheng).Sum();
                            cc.mPengYaodiyao = (from dd in mDateGroups select dd.mPengYaodiyao).Sum();
                            cc.mGuohuojizhua = (from dd in mDateGroups select dd.mGuohuojizhua).Sum();
                            cc.mYoudian = (from dd in mDateGroups select dd.mYoudian).Sum();
                            cc.mChangshangbuliang = (from dd in mDateGroups select dd.mChangshangbuliang).Sum();
                            cc.mQianghuaQiTa = (from dd in mDateGroups select dd.mQianghuaQiTa).Sum();
                            cc.mZuzhuangcashang = (from dd in mDateGroups select dd.mZuzhuangcashang).Sum();
                            cc.mHanyao = (from dd in mDateGroups select dd.mHanyao).Sum();
                            cc.mCashang = (from dd in mDateGroups select dd.mCashang).Sum();
                            cc.mQianghuahoucashang = (from dd in mDateGroups select dd.mQianghuahoucashang).Sum();

                            //};

                            //Model.ProduceInDepotDetail cc = QueryEx.First<Model.ProduceInDepotDetail>();
                            //foreach (var cc in groups)
                            //{
                            DataRow dr = dt.NewRow();
                            productname = a.ToString() + cc.ProductName;
                            dr["商品"] = cc.ProductName;
                            dr["日期"] = cc.ProduceInDepotDate.HasValue ? cc.ProduceInDepotDate.Value.ToString("MM-dd") : "";
                            dr["數量"] = cc.ProceduresSum.HasValue ? cc.ProceduresSum.ToString() : "0";
                            dr["合格數量"] = cc.CheckOutSum.HasValue ? cc.CheckOutSum.ToString() : "0";
                            dr["不合格數量"] = (cc.ProceduresSum.HasValue && cc.CheckOutSum.HasValue) ? (cc.ProceduresSum.Value - cc.CheckOutSum.Value).ToString() : "0";
                            dr["不良率"] = cc.RejectionRate.HasValue ? cc.RejectionRate.ToString() : "0";
                            dr["單位"] = cc.ProductUnit;
                            dr["原料問題"] = cc.mYuanliaowenti.HasValue ? cc.mYuanliaowenti.ToString() : "0";
                            dr["抽料問題"] = cc.mChouliaowenti.HasValue ? cc.mChouliaowenti.ToString() : "0";
                            dr["砲管問題"] = cc.mPaoguanwenti.HasValue ? cc.mPaoguanwenti.ToString() : "0";
                            dr["晶點固定點"] = cc.mJingdiangudingdian.HasValue ? cc.mJingdiangudingdian.ToString() : "0";
                            dr["插片擦傷"] = cc.mChapiancashang.HasValue ? cc.mChapiancashang.ToString() : "0";
                            dr["挽模擦傷"] = cc.mWanMocashang.HasValue ? cc.mWanMocashang.ToString() : "0";
                            dr["怪手撞傷"] = cc.mSuoShui.HasValue ? cc.mSuoShui.ToString() : "0";
                            dr["滑板擦傷"] = cc.mHuabancashang.HasValue ? cc.mHuabancashang.ToString() : "0";
                            dr["過火雞爪"] = cc.mGuohuojizhua.HasValue ? cc.mGuohuojizhua.ToString() : "0";
                            dr["白煙黑煙"] = cc.mBaiyanHeiYan.HasValue ? cc.mBaiyanHeiYan.ToString() : "0";
                            dr["結合線回紋冷料噴須"] = cc.mJieHeXianHuiwen.HasValue ? cc.mJieHeXianHuiwen.ToString() : "0";
                            dr["縮水"] = cc.mSuoShui.HasValue ? cc.mSuoShui.ToString() : "0";
                            dr["氣泡"] = cc.mQiPao.HasValue ? cc.mQiPao.ToString() : "0";
                            dr["射出其他"] = cc.mShechuqita.HasValue ? cc.mShechuqita.ToString() : "0";
                            dr["擦模損壞"] = cc.mGuaiShouZhuangShang.HasValue ? cc.mGuaiShouZhuangShang.ToString() : "0";
                            dr["拆片擦傷"] = cc.mChaipiancashang.HasValue ? cc.mChaipiancashang.ToString() : "0";
                            dr["黑點雜質"] = cc.mHeidianzazhi.HasValue ? cc.mHeidianzazhi.ToString() : "0";

                            //新添加
                            dr["強化前擦傷"] = cc.mQianghuaqiancashang.HasValue ? cc.mQianghuaqiancashang.ToString() : "0";
                            dr["強化後擦傷"] = cc.mQianghuahoucashang.HasValue ? cc.mQianghuahoucashang.ToString() : "0";
                            dr["含藥"] = cc.mHanyao.HasValue ? cc.mHanyao.ToString() : "0";
                            dr["顆粒棉絮"] = cc.mKeLimianxu.HasValue ? cc.mKeLimianxu.ToString() : "0";
                            dr["流痕"] = cc.mLiuheng.HasValue ? cc.mLiuheng.ToString() : "0";
                            dr["噴藥滴藥"] = cc.mPengYaodiyao.HasValue ? cc.mPengYaodiyao.ToString() : "0";
                            dr["強化防霧線"] = cc.mQianghuafangwuxian.HasValue ? cc.mQianghuafangwuxian.ToString() : "0";
                            dr["油點"] = cc.mYoudian.HasValue ? cc.mYoudian.ToString() : "0";
                            dr["強化其他"] = cc.mQianghuaQiTa.HasValue ? cc.mQianghuaQiTa.ToString() : "0";
                            dr["廠商不良"] = cc.mChangshangbuliang.HasValue ? cc.mChangshangbuliang.ToString() : "0";
                            dr["組裝擦傷"] = cc.mZuzhuangcashang.HasValue ? cc.mZuzhuangcashang.ToString() : "0";
                            dr["擦傷"] = cc.mCashang.HasValue ? cc.mCashang.ToString() : "0";

                            //dr["其他"] = cc.mQiTa.HasValue ? cc.mQiTa.ToString() : "0";
                            //dr["擦模次數"] = cc.mCaMoCiShu.HasValue ? cc.mCaMoCiShu.ToString() : "0";
                            // dr["不良品數量"] = cc.ZaZhi.HasValue ? cc.ZaZhi : "";
                            //RichTextBox rt = new RichTextBox();
                            //rt.Rtf = bp.ProductDesc;
                            //rt.SelectAll();
                            //dr["備註"] = rt.SelectedText;
                            dt.Rows.Add(dr);
                            //}
                        }
                        dt.TableName = productname;
                        ds.Tables.Add(dt);
                    }
                    ExportAllBomToExcel(ds);
                }

                f.Dispose();
                GC.Collect();

            }

        }

        //直接加工结案
        private void barPronoteJieAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();
            f.ShowDialog();
        }

        public static void ExportAllBomToExcel(DataSet ds)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Worksheet sheet = null;
                int i = 1;
                excel.Application.Workbooks.Add(true);
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.Worksheets.get_Item(1);


                foreach (System.Data.DataTable table in ds.Tables)
                {

                    int rowIndex = 3;
                    int colIndex = 1;
                    try
                    {
                        if (i > 1)
                        {
                            excel.Worksheets.Add(System.Reflection.Missing.Value, sheet, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                            sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.Worksheets.get_Item(i);
                        }
                        //i.ToString() +
                        if (table.TableName.Length > 31)
                            table.TableName = table.TableName.Substring(0, 31);
                        table.TableName = table.TableName.Replace('*', ' ');
                        table.TableName = table.TableName.Replace('?', ' ');
                        table.TableName = table.TableName.Replace('/', ' ');
                        sheet.Name = table.TableName;
                        excel.Cells[1, 1] = "常態不良品統計表";
                        excel.Cells[2, 1] = "品名:" + i.ToString() + table.TableName;
                        sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    foreach (DataColumn col in table.Columns)
                    {

                        excel.Cells[3, colIndex] = col.ColumnName;
                        colIndex++;
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        rowIndex++;
                        colIndex = 0;
                        foreach (DataColumn col in table.Columns)
                        {
                            colIndex++;
                            excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                        }
                    }
                    excel.Cells[36, 1] = "合計";

                    //sheet.get_Range(excel.Cells[36, 1], excel.Cells[36, colIndex]).Select();
                    sheet.get_Range(excel.Cells[36, 1], excel.Cells[36, colIndex]).Interior.ColorIndex = 3;
                    double sum3, sum4, sum5, sum6, sum8, sum9, sum10, sum11, sum12, sum13, sum14, sum15, sum16, sum17, sum18, sum19, sum20, sum21, sum22, sum23, sum24, sum26;
                    sum3 = sum4 = sum5 = sum6 = sum8 = sum9 = sum10 = sum11 = sum12 = sum13 = sum14 = sum15 = sum16 = sum17 = sum18 = sum19 = sum20 = sum21 = sum22 = sum23 = sum24 = sum26 = 0;
                    for (int m = 0; m < table.Rows.Count; m++)
                    {
                        sum3 += double.Parse(table.Rows[m][2].ToString());
                        sum4 += double.Parse(table.Rows[m][3].ToString());
                        sum5 += double.Parse(table.Rows[m][4].ToString());
                        sum6 += double.Parse(table.Rows[m][5].ToString());
                        sum8 += double.Parse(table.Rows[m][7].ToString());
                        sum9 += double.Parse(table.Rows[m][8].ToString());
                        sum10 += double.Parse(table.Rows[m][9].ToString());
                        sum11 += double.Parse(table.Rows[m][10].ToString());
                        sum12 += double.Parse(table.Rows[m][11].ToString());
                        sum13 += double.Parse(table.Rows[m][12].ToString());
                        sum14 += double.Parse(table.Rows[m][13].ToString());
                        sum15 += double.Parse(table.Rows[m][14].ToString());
                        sum16 += double.Parse(table.Rows[m][15].ToString());
                        sum17 += double.Parse(table.Rows[m][16].ToString());
                        sum18 += double.Parse(table.Rows[m][17].ToString());
                        sum19 += double.Parse(table.Rows[m][18].ToString());
                        sum20 += double.Parse(table.Rows[m][19].ToString());
                        sum21 += double.Parse(table.Rows[m][20].ToString());
                        sum22 += double.Parse(table.Rows[m][21].ToString());
                        sum23 += double.Parse(table.Rows[m][22].ToString());
                        sum24 += double.Parse(table.Rows[m][23].ToString());
                        sum26 += double.Parse(table.Rows[m][25].ToString());

                        //excel.Cells[36, 3] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][2].ToString());
                        //excel.Cells[36, 4] = double.Parse(excel.Cells[36, 4].ToString()) + double.Parse(table.Rows[m][3].ToString());
                        //excel.Cells[36, 5] = double.Parse(excel.Cells[36, 5].ToString()) + double.Parse(table.Rows[m][4].ToString());
                        //excel.Cells[36, 6] = double.Parse(excel.Cells[36, 6].ToString()) + double.Parse(table.Rows[m][5].ToString());
                        //excel.Cells[36, 8] = double.Parse(excel.Cells[36, 8].ToString()) + double.Parse(table.Rows[m][7].ToString());
                        //excel.Cells[36, 9] = double.Parse(excel.Cells[36, 9].ToString()) + double.Parse(table.Rows[m][8].ToString());
                        //excel.Cells[36, 10] = double.Parse(excel.Cells[36, 10].ToString()) + double.Parse(table.Rows[m][9].ToString());
                        //excel.Cells[36, 11] = double.Parse(excel.Cells[36, 11].ToString()) + double.Parse(table.Rows[m][10].ToString());
                        //excel.Cells[36, 12] = double.Parse(excel.Cells[36, 12].ToString()) + double.Parse(table.Rows[m][11].ToString());
                        //excel.Cells[36, 13] = double.Parse(excel.Cells[36, 13].ToString()) + double.Parse(table.Rows[m][12].ToString());
                        //excel.Cells[36, 14] = double.Parse(excel.Cells[36, 14].ToString()) + double.Parse(table.Rows[m][13].ToString());
                        //excel.Cells[36, 15] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][14].ToString());
                        //excel.Cells[36, 16] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][15].ToString());
                        //excel.Cells[36, 17] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][16].ToString());
                        //excel.Cells[36, 18] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][17].ToString());
                        //excel.Cells[36, 19] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][18].ToString());
                        //excel.Cells[36, 20] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][19].ToString());
                        //excel.Cells[36, 21] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][20].ToString());
                        //excel.Cells[36, 23] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][22].ToString());
                        //excel.Cells[36, 24] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][23].ToString());
                        //excel.Cells[36, 26] = double.Parse(excel.Cells[36, 3].ToString()) + double.Parse(table.Rows[m][25].ToString());
                        //sum3 += double.Parse(table.Rows[m][2].ToString());
                        //if (m == table.Rows.Count - 1)
                        //    excel.Cells[36, 3] = sum3;
                        //sum5 += double.Parse(table.Rows[m][4].ToString());
                        //if (m == table.Rows.Count - 1)
                        //    excel.Cells[36, 5] = sum5;
                    }
                    excel.Cells[36, 3] = sum3;
                    excel.Cells[36, 4] = sum4;
                    excel.Cells[36, 5] = sum5;
                    excel.Cells[36, 6] = sum6;
                    excel.Cells[36, 8] = sum8;
                    excel.Cells[36, 9] = sum9;
                    excel.Cells[36, 10] = sum10;
                    excel.Cells[36, 11] = sum11;
                    excel.Cells[36, 12] = sum12;
                    excel.Cells[36, 13] = sum13;
                    excel.Cells[36, 14] = sum14;
                    excel.Cells[36, 15] = sum15;
                    excel.Cells[36, 16] = sum16;
                    excel.Cells[36, 17] = sum17;
                    excel.Cells[36, 18] = sum18;
                    excel.Cells[36, 19] = sum19;
                    excel.Cells[36, 20] = sum20;
                    excel.Cells[36, 21] = sum21;
                    excel.Cells[36, 22] = sum22;
                    excel.Cells[36, 23] = sum23;
                    excel.Cells[36, 24] = sum24;
                    excel.Cells[36, 26] = sum26;

                    i++;
                }
                excel.Visible = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            GC.Collect();
        }

        #region 审核
        protected override string AuditKeyId()
        {
            return Model.ProduceInDepot.PRO_ProduceInDepotId;
        }

        protected override int AuditState()
        {
            return this.produceInDepot.AuditState.HasValue ? this.produceInDepot.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProduceInDepot" + "," + this.produceInDepot.ProduceInDepotId;
        }
        #endregion

        private void dateEditProduceInDepotDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.dateEditProduceInDepotDate.DateTime != null && this.action == "insert")
            {
                this.produceInDepot.ProduceInDepotId = this.produceInDepotManager.GetId(this.dateEditProduceInDepotDate.DateTime);
                this.textEditProduceInDepotId.Text = this.produceInDepot.ProduceInDepotId;
            }
        }

        //导出Excel
        private void bar_ExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcel(this.produceInDepot.Details);
        }

        private void newChooseWorkHorseId_EditValueChanged(object sender, EventArgs e)
        {
            if (newChooseWorkHorseId.EditValue != null)
            {
                foreach (var item in this.produceInDepot.Details)
                {
                    item.beforeTransferQuantity = this.produceInDepotDetailManager.select_TransferSumyPronHeaderWorkHouse(item.PronoteHeaderId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).WorkHouseId, DateTime.Now.Date.AddDays(-1));
                    item.HeJiBeforeTransferQuantity = this.produceInDepotDetailManager.select_TransferSumyPronHeaderWorkHouse(item.PronoteHeaderId, (this.newChooseWorkHorseId.EditValue as Model.WorkHouse).WorkHouseId, null);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "gridColumn6")
            {
                //string pronoteHeaderid = (this.bindingSourceDetails.Current as Model.ProduceInDepotDetail).PronoteHeaderId;
                Model.PronoteHeader d = new BL.PronoteHeaderManager().Get(e.CellValue.ToString());
                if (d != null)
                {
                    PronoteHeader.EditForm f = new Book.UI.produceManager.PronoteHeader.EditForm(d);
                    f.ShowDialog();
                }
            }
        }

    }
}

#region Note
//private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
//{
//    if (this.newChooseContorlDepot.EditValue != null)
//        this.bindingSourceDepotPositionId.DataSource = depotPositionManager.Select(this.newChooseContorlDepot.EditValue as Model.Depot);
//}
#endregion