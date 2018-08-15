using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
using DevExpress.XtraTreeList.Nodes;
using Book.UI.produceManager.ProduceOtherCompact;
using Book.UI.Invoices.CO;
using System.Data.SqlClient;

namespace Book.UI.produceManager.MRSHeader
{
    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010   飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 编 码 人: 马艳军             完成时间:2010-3-24
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:s
    //----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        //设置静态字段 在选择生产计划页面赋值
        public static IList<Model.MPSheader> _mpsheader = new List<Model.MPSheader>();
        Model.MRSHeader mrsheader = new Book.Model.MRSHeader();
        Model.Product product = new Book.Model.Product();
        Model.InvoiceCO _invoiceCO;
        Model.InvoiceCODetail _invoiceCODetail;
        Model.ProduceOtherCompact _produceOtherCompact = new Model.ProduceOtherCompact();
        Model.ProduceOtherCompactDetail _produceOtherCompactDetail = null;
        Model.ProduceOtherCompactMaterial _produceOtherCompactMaterial = null;
        Model.PronotedetailsMaterial materials;
        Model.PronoteHeader pronoteHeader = new Book.Model.PronoteHeader();
        Model.PronoteProceduresDetail pronoteProceduresDetail;

        BL.MRSHeaderManager mrsheaderManager = new Book.BL.MRSHeaderManager();
        BL.MRSdetailsManager mrsdetailManager = new Book.BL.MRSdetailsManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.InvoiceXODetailManager xodetailManager = new BL.InvoiceXODetailManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.MPSheaderManager mPSheaderManager = new Book.BL.MPSheaderManager();
        BL.MPSdetailsManager mPSdetailsManager = new BL.MPSdetailsManager();
        BL.InvoiceCOManager _invoiceCOManager = new Book.BL.InvoiceCOManager();
        BL.InvoiceCODetailManager _invoiceCOdetailManager = new Book.BL.InvoiceCODetailManager();
        BL.ProduceOtherCompactManager _produceOtherCompactManager = new BL.ProduceOtherCompactManager();
        BL.ProduceOtherCompactDetailManager _produceOtherCompactDetailManager = new BL.ProduceOtherCompactDetailManager();
        BL.BomComponentInfoManager bomComponentInfoManager = new BL.BomComponentInfoManager();
        BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        BL.ProduceOtherCompactMaterialManager produceOtherComactMaterialManager = new BL.ProduceOtherCompactMaterialManager();
        BL.SupplierManager _supplierManager = new Book.BL.SupplierManager();
        BL.SupplierProductManager _SupplierProductManager = new Book.BL.SupplierProductManager();
        BL.BomPackageDetailsManager bomPackageDetailsManager = new BL.BomPackageDetailsManager();
        BL.TechonlogyHeaderManager techonlogyHeaderManager = new Book.BL.TechonlogyHeaderManager();
        BL.TechnologydetailsManager technologydetailsManager = new Book.BL.TechnologydetailsManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();

        private int LastFlag = 0;

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.MRSHeader.PRO_Id, new AA(Properties.Resources.RequireDataForId, this.textEditMRSHeaderId));

            this.invalidValueExceptions.Add(Model.MRSHeader.PRO_Id, new AA(Properties.Resources.EntityExists, this.textEditMRSHeaderId));
            this.requireValueExceptions.Add(Model.MPSheader.PRO_MPSStartDate, new AA(Properties.Resources.DateIsNull, this.dateEditMRSstartdate as Control));
            this.requireValueExceptions.Add(Model.MRSdetails.PRO_SupplierId, new AA(Properties.Resources.Supplier, this.gridView1.GridControl));
            this.action = "view";
            this.newChooseEmployee0Id.Choose = new ChooseEmployee();
            this.newChooseEmployee1Id.Choose = new ChooseEmployee();
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.bindingSourceSupplier.DataSource = this._supplierManager.Select();
            this.EmpAudit.Choose = new ChooseEmployee();

        }

        public EditForm(Model.MRSHeader mrsheader)
            : this()
        {
            this.mrsheader = mrsheader;
            // this.mrsheader.Details = this.mrsdetailManager.Select(mrsheader);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.MRSHeader mrsheader, string action)
            : this()
        {
            this.mrsheader = mrsheader;
            this.mrsheader.Details = this.mrsdetailManager.Select(mrsheader);
            this.action = action;
            if (this.action == "view")
                if (this.action == "view")
                    LastFlag = 1;
        }

        protected override void Save()
        {

            this.mrsheader.Id = this.textEditMRSHeaderId.Text;
            // this.mrsheader.MRSHeadername = this.textEditMPSHeaderId.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditMRSstartdate.DateTime, new DateTime()))
            {
                this.mrsheader.MRSstartdate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.mrsheader.MRSstartdate = this.dateEditMRSstartdate.DateTime;
            }
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditMRSenddate.DateTime, new DateTime()))
            //{
            //    this.mrsheader.MRSenddate = global::Helper.DateTimeParse.EndDate;
            //}
            //else
            //{
            this.mrsheader.MRSenddate = global::Helper.DateTimeParse.EndDate;
            //}
            this.mrsheader.MRSheaderDesc = this.textEditMRSheaderDesc.Text;
            this.mrsheader.MPSheaderId = this.textEditMps.Text;
            this.mrsheader.Employee0 = (this.newChooseEmployee0Id.EditValue as Model.Employee);
            if (this.mrsheader.Employee0 != null)
            {
                this.mrsheader.Employee0Id = this.mrsheader.Employee0.EmployeeId;
            }
            this.mrsheader.Employee1 = (this.newChooseEmployee1Id.EditValue as Model.Employee);
            if (this.mrsheader.Employee1 != null)
            {
                this.mrsheader.Employee1Id = this.mrsheader.Employee1.EmployeeId;
            }
            this.mrsheader.AuditState = this.saveAuditState;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.mrsheaderManager.Insert(this.mrsheader);
                    break;

                case "update":
                    this.mrsheaderManager.Update(this.mrsheader);
                    break;
            }

        }

        protected override void Delete()
        {
            if (this.mrsheader == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.mrsheaderManager.Delete(this.mrsheader);
            this.mrsheader = this.mrsheaderManager.GetNext(this.mrsheader);
            if (this.mrsheader == null)
            {
                this.mrsheader = this.mrsheaderManager.GetLast();
            }
        }

        public override void Refresh()
        {
            if (this.mrsheader == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.mrsheader = this.mrsheaderManager.GetDetails(mrsheader.MRSHeaderId);
            }

            this.textEditType.Text = this.mrsheader.GetSourceType;
            this.textEditMRSHeaderId.Text = this.mrsheader.MRSHeaderId;
            this.textEditMps.Text = this.mrsheader.MPSheaderId;
            this.textEditMRSheaderDesc.Text = this.mrsheader.MRSheaderDesc;
            if (!string.IsNullOrEmpty(this.mrsheader.MPSheaderId))
            {
                Model.MPSheader mpsHeader = this.mPSheaderManager.Get(this.mrsheader.MPSheaderId);
                if (mpsHeader != null)
                {
                    //this.textEditMps.Text = this.mrsheader.MPSheaderId;
                    if (!string.IsNullOrEmpty(mpsHeader.InvoiceXOId))
                    {
                        Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mpsHeader.InvoiceXOId);
                        this.textEditXOId.Text = invoiceXO == null ? string.Empty : invoiceXO.InvoiceId;
                        this.textEditCustomXOId.Text = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
                        this.newChooseCustomer.EditValue = invoiceXO == null ? null : invoiceXO.xocustomer;
                        this.textEditPiHao.Text = invoiceXO == null ? null : invoiceXO.CustomerLotNumber;
                    }
                    else
                    {
                        this.textEditXOId.Text = string.Empty;
                        this.textEditCustomXOId.Text = string.Empty;
                        this.newChooseCustomer.EditValue = null;
                        this.textEditPiHao.Text = string.Empty;
                    }
                }
                else
                {
                    this.textEditXOId.Text = string.Empty;
                    this.textEditCustomXOId.Text = string.Empty;
                    this.newChooseCustomer.EditValue = null;
                    this.textEditPiHao.Text = string.Empty;
                }
            }
            else
            {
                this.textEditXOId.Text = string.Empty;
                this.textEditCustomXOId.Text = string.Empty;
                this.newChooseCustomer.EditValue = null;
                this.textEditMps.Text = string.Empty;
                this.textEditPiHao.Text = string.Empty;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.mrsheader.MRSstartdate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditMRSstartdate.EditValue = null;
            }
            else
            {
                this.dateEditMRSstartdate.EditValue = this.mrsheader.MRSstartdate;
            }
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.mrsheader.MRSenddate, global::Helper.DateTimeParse.NullDate))

            //{
            //    this.dateEditMRSenddate.EditValue = null;
            //}
            //else
            //{
            //    this.dateEditMRSenddate.EditValue = this.mrsheader.MRSenddate;
            //}
            this.newChooseEmployee1Id.EditValue = this.mrsheader.Employee1;
            this.newChooseEmployee0Id.EditValue = this.mrsheader.Employee0;

            //var details = from ds in this.mrsheader.Details orderby ds.Product.SupplierId select ds;
            foreach (Model.MRSdetails m in this.mrsheader.Details)
            {
                if (m.ProductId != null)
                    m.SpotStock = mrsdetailManager.SumSpotStock(m.ProductId);
            }
            this.EmpAudit.EditValue = this.mrsheader.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this.mrsheader.AuditState);
            this.bindingSourceDetails.DataSource = this.mrsheader.Details;

            base.Refresh();

            this.simpleButton_ViewInvoiceCO.Enabled = false;
            this.simpleButton_ViewTrust.Enabled = false;
            this.simpleButtonOther.Enabled = false;
            //this.sbtn_buitProg.Enabled = false;
            this.sbtn_buitInvoice.Enabled = false;
            this.checkEditCheck.Properties.ReadOnly = false;
            this.checkEditCheck.Checked = false;
            switch (this.action)
            {

                case "insert":
                    gridColumnInumber.OptionsColumn.AllowEdit = true;
                    ColProductId.OptionsColumn.AllowEdit = true;
                    gridColumn2.OptionsColumn.AllowEdit = true;
                    gridColumn3.OptionsColumn.AllowEdit = true;
                    gridColumn8.OptionsColumn.AllowEdit = true;
                    gridColumn6.OptionsColumn.AllowEdit = true;
                    gridColumn9.OptionsColumn.AllowEdit = true;
                    gridColumnWorkHouse.OptionsColumn.AllowEdit = true;
                    gridColumnIsArrange.OptionsColumn.AllowEdit = true;
                    this.gridColumnSupplierId.OptionsColumn.AllowEdit = true;
                    this.simpleButtonPronoteHeader.Enabled = false;
                    this.simplePronoteHeaderQuery.Enabled = false;

                    break;
                case "update":
                    gridColumnInumber.OptionsColumn.AllowEdit = true;
                    ColProductId.OptionsColumn.AllowEdit = true;
                    gridColumn2.OptionsColumn.AllowEdit = true;
                    gridColumn3.OptionsColumn.AllowEdit = true;
                    gridColumn8.OptionsColumn.AllowEdit = true;
                    gridColumn6.OptionsColumn.AllowEdit = true;
                    gridColumn9.OptionsColumn.AllowEdit = true;
                    gridColumnWorkHouse.OptionsColumn.AllowEdit = true;
                    gridColumnIsArrange.OptionsColumn.AllowEdit = true;
                    this.gridColumnSupplierId.OptionsColumn.AllowEdit = true;
                    this.simpleButtonPronoteHeader.Enabled = false;
                    this.simplePronoteHeaderQuery.Enabled = false;

                    break;
                case "view":
                    gridColumnInumber.OptionsColumn.AllowEdit = false;
                    ColProductId.OptionsColumn.AllowEdit = false;
                    gridColumn2.OptionsColumn.AllowEdit = false;
                    gridColumn3.OptionsColumn.AllowEdit = false;
                    gridColumn8.OptionsColumn.AllowEdit = false;
                    gridColumn6.OptionsColumn.AllowEdit = false;
                    gridColumn9.OptionsColumn.AllowEdit = false;
                    gridColumnWorkHouse.OptionsColumn.AllowEdit = false;
                    gridColumnIsArrange.OptionsColumn.AllowEdit = true;
                    this.gridColumnSupplierId.OptionsColumn.AllowEdit = false;

                    if (this.mrsheader.IsbuiltInvoiceCO == true)
                    {
                        if (this.mrsheader.SourceType == "0" || this.mrsheader.SourceType == "4" || this.mrsheader.SourceType == "5")
                        {
                            //sbtn_buitProg.Text = Properties.Resources.IsBuiltTrustOut;
                            this.simpleButtonOther.Enabled = false;
                            this.sbtn_buitInvoice.Enabled = false;
                            this.gridColumnSupplierId.Visible = false;
                            this.simpleButtonPronoteHeader.Enabled = true;
                            this.simplePronoteHeaderQuery.Enabled = true;

                        }

                        if (this.mrsheader.SourceType == "1")
                        {
                            this.sbtn_buitInvoice.Enabled = true;
                            this.simpleButton_ViewInvoiceCO.Enabled = true;
                            this.simpleButton_ViewTrust.Enabled = false;
                            this.simpleButtonOther.Enabled = false;
                            //this.sbtn_buitProg.Enabled = false;
                            this.gridColumnSupplierId.Visible = true;
                        }

                        if (this.mrsheader.SourceType == "3" || this.mrsheader.SourceType == "6")
                        {
                            this.simpleButtonOther.Enabled = true;
                            this.simpleButton_ViewTrust.Enabled = true;
                            this.simpleButton_ViewInvoiceCO.Enabled = false;
                            this.sbtn_buitInvoice.Enabled = false;
                            //this.sbtn_buitProg.Enabled = false;
                            this.gridColumnSupplierId.Visible = true;
                        }
                    }
                    else
                    {
                        if (this.mrsheader.SourceType == "0" || this.mrsheader.SourceType == "4" || this.mrsheader.SourceType == "5")
                        {
                            //sbtn_buitProg.Text = Properties.Resources.IsBuiltTrustOut;
                            this.simpleButtonOther.Enabled = false;
                            this.sbtn_buitInvoice.Enabled = false;
                            this.gridColumnSupplierId.Visible = false;
                            this.simpleButtonPronoteHeader.Enabled = true;
                            this.simplePronoteHeaderQuery.Enabled = true;

                        }

                        if (this.mrsheader.SourceType == "1")
                        {
                            //sbtn_buitInvoice.Text = "生成采購單";
                            this.sbtn_buitInvoice.Enabled = true;
                            this.simpleButton_ViewTrust.Enabled = false;
                            this.simpleButton_ViewInvoiceCO.Enabled = true;
                            this.simpleButtonOther.Enabled = false;
                            //this.sbtn_buitProg.Enabled = false;
                            this.gridColumnSupplierId.Visible = true;
                        }

                        if (this.mrsheader.SourceType == "3" || this.mrsheader.SourceType == "6")
                        {
                            //simpleButtonOther.Text = "形成委外合同";
                            this.simpleButtonOther.Enabled = true;
                            this.simpleButton_ViewTrust.Enabled = true;
                            this.simpleButton_ViewInvoiceCO.Enabled = false;
                            this.sbtn_buitInvoice.Enabled = false;
                            // this.sbtn_buitProg.Enabled = false;
                            this.gridColumnSupplierId.Visible = true;
                        }
                    }

                    break;
            }

            this.textEditMRSHeaderId.Properties.ReadOnly = true;
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.textEditType.Properties.ReadOnly = true;
            this.simpleButtonProduceMaterial.Enabled = true;
            this.btn_MakeProduceMaterial.Enabled = true;
            //colDepotDistributed.OptionsColumn.AllowEdit = false;
            //if (this.mrsheader.SourceType == "1")
            //{
            //    this.sbtn_buitInvoice.Enabled = true;
            //    this.sbtn_buitProg.Enabled = false;
            //}
            //if (this.mrsheader.SourceType == "0")
            //{
            //    this.sbtn_buitInvoice.Enabled = false;
            //    this.sbtn_buitProg.Enabled = true;
            //}
            //if (this.mrsheader.SourceType == "3")
            //{
            //    this.simpleButtonOther.Enabled = true;
            //    this.sbtn_buitInvoice.Enabled = false;
            //    this.sbtn_buitProg.Enabled = false;
            //}
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO1(mrsheader.MRSHeaderId);
        }

        protected override void MoveNext()
        {
            Model.MRSHeader mrsheader = this.mrsheaderManager.GetNext(this.mrsheader);
            if (mrsheader == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.mrsheader = this.mrsheaderManager.Get(mrsheader.MRSHeaderId);
        }

        protected override void MovePrev()
        {
            Model.MRSHeader mrsheader = this.mrsheaderManager.GetPrev(this.mrsheader);
            if (mrsheader == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.mrsheader = this.mrsheaderManager.Get(mrsheader.MRSHeaderId);
        }

        protected override void MoveFirst()
        {
            this.mrsheader = this.mrsheaderManager.Get(this.mrsheaderManager.GetFirst() == null ? "" : this.mrsheaderManager.GetFirst().MRSHeaderId);
        }

        protected override void MoveLast()
        {
            //if (mrsheader == null)
            // {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this.mrsheader = this.mrsheaderManager.Get(this.mrsheaderManager.GetLast() == null ? "" : this.mrsheaderManager.GetLast().MRSHeaderId);
            // }
        }

        protected override bool HasRows()
        {
            return this.mrsheaderManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.mrsheaderManager.HasRowsAfter(this.mrsheader);
        }

        protected override bool HasRowsPrev()
        {
            return this.mrsheaderManager.HasRowsBefore(this.mrsheader);
        }

        protected override void AddNew()
        {
            this.mrsheader = new Model.MRSHeader();
            this.mrsheader.MRSHeaderId = this.mrsheaderManager.GetId(); ;// Guid.NewGuid().ToString();
            this.mrsheader.Employee0 = BL.V.ActiveOperator.Employee;
            this.mrsheader.Details = new List<Model.MRSdetails>();
            if (this.action == "insert")
            {
                Model.MRSdetails detail = new Model.MRSdetails();
                detail.MRSdetailsId = Guid.NewGuid().ToString();
                detail.MRSdetailssum = 0;
                detail.MRSdetailsdes = "";
                detail.ProductUnit = "";
                detail.Product = new Book.Model.Product();
                this.mrsheader.Details.Add(detail);
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this.mrsheader.Details.Count > 0 && this.mrsheader.Details[0] != null && string.IsNullOrEmpty(this.mrsheader.Details[0].ProductId))
                    this.mrsheader.Details.RemoveAt(0);
                Model.MRSdetails mRSdetails2 = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        mRSdetails2 = new Book.Model.MRSdetails();
                        mRSdetails2.MRSdetailsId = Guid.NewGuid().ToString();
                        mRSdetails2.MRSHeaderId = this.mrsheader.MRSHeaderId;
                        mRSdetails2.MRSdetailssum = 0;
                        mRSdetails2.MRSdetailsQuantity = 0;
                        mRSdetails2.Product = this.productManager.Get(product.ProductId);
                        mRSdetails2.ProductId = mRSdetails2.Product.ProductId;
                        mRSdetails2.ProductUnit = mRSdetails2.Product.DepotUnit == null ? "" : mRSdetails2.Product.DepotUnit.CnName;
                        //  mRSdetails2.BeforePackageProductId = bomcom.BeforepPackageProductId;
                        mRSdetails2.Inumber = this.mrsheader.Details.Count + 1;
                        this.mrsheader.Details.Add(mRSdetails2);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    mRSdetails2 = new Book.Model.MRSdetails();
                    mRSdetails2.MRSdetailsId = Guid.NewGuid().ToString();
                    mRSdetails2.MRSHeaderId = this.mrsheader.MRSHeaderId;
                    mRSdetails2.MRSdetailssum = 0;
                    mRSdetails2.MRSdetailsQuantity = 0;
                    mRSdetails2.Product = this.productManager.Get((f.SelectedItem as Model.Product).ProductId);
                    mRSdetails2.ProductId = mRSdetails2.Product.ProductId;
                    mRSdetails2.ProductUnit = mRSdetails2.Product.DepotUnit == null ? "" : mRSdetails2.Product.DepotUnit.CnName;
                    //  mRSdetails2.BeforePackageProductId = bomcom.BeforepPackageProductId;

                    mRSdetails2.Inumber = this.mrsheader.Details.Count + 1;
                    this.mrsheader.Details.Add(mRSdetails2);
                }
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(mRSdetails2);
                //this.bindingSourceMaterial.Position = this.bindingSourceMaterial.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
            f.Dispose();
            System.GC.Collect();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this.mrsheader.Details.Remove(this.bindingSourceDetails.Current as Book.Model.MRSdetails);

                if (this.mrsheader.Details.Count == 0)
                {
                    Model.MRSdetails detail = new Model.MRSdetails();
                    detail.MRSdetailsId = Guid.NewGuid().ToString();
                    detail.MRSdetailsdes = "";
                    detail.MRSdetailssum = 0;
                    detail.ProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.mrsheader.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        protected internal void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.MRSdetails detail = this.gridView1.GetRow(e.RowHandle) as Model.MRSdetails;
            if (detail == null) return;
            if (e.Column == this.ColProductId)
            {
                Model.Product p = productManager.Get(e.Value.ToString());
                detail.MRSdetailsId = Guid.NewGuid().ToString();
                detail.MRSdetailsdes = "";
                detail.MRSdetailssum = 0;
                detail.Product = p;
                detail.ProductId = p.ProductId;
                detail.ProductUnit = p.MainUnit == null ? "" : p.MainUnit.CnName;
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
            //if (e.Column.ColumnEditName == "repositoryItemCheckEdit1")
            //{
            //    if (!string.IsNullOrEmpty(detail.ArrangeDesc))
            //        this.repositoryItemCheckEdit1.ValueChecked = true;
            //}
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.MRSdetails> details = this.bindingSourceDetails.DataSource as IList<Model.MRSdetails>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            //Model.Product madeProduct = details[e.ListSourceRowIndex].MadeProduct;

            switch (e.Column.Name)
            {
                //case "colProductId":
                //    if (detail == null) return;
                //    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                //    break;
                case "gridColumnGuiGe":
                    if (detail == null) return;
                    e.DisplayText = detail.ProductSpecification;
                    break;
                case "gridColumnMPSdate":
                    Model.MPSheader mpsHeader = this.mPSheaderManager.Get(details[e.ListSourceRowIndex].MRSHeader.MPSheaderId);
                    e.DisplayText = mpsHeader == null ? "" : mpsHeader.MPSStartDate.Value.ToShortDateString();
                    break;
                case "gridColumnStock":
                    if (detail == null) return;
                    e.DisplayText = detail.StocksQuantity == null ? "" : detail.StocksQuantity.Value.ToString();
                    break;
                //case "gridColumnSupplierId":
                //    e.DisplayText = detail.Supplier == null ? "" : detail.Supplier.ToString();
                //    break;
                //case "gridColFinishedPro":
                //    if (madeProduct != null)
                //    {
                //        e.DisplayText = string.IsNullOrEmpty(madeProduct.CustomerProductName) ? madeProduct.ProductName : madeProduct.ProductName + "{" + madeProduct.CustomerProductName + "}";
                //    }
                //    break;
                case "gridColumnOnWay":
                    if (detail == null) return;
                    e.DisplayText = detail.OrderOnWayQuantity == null ? "0" : detail.OrderOnWayQuantity.ToString(); ;
                    break;
                case "gridColumnMaterialDistributioned":
                    if (detail == null) return;
                    e.DisplayText = detail.ProduceMaterialDistributioned.HasValue ? detail.ProduceMaterialDistributioned.ToString() : "0";
                    break;
                case "gridColumnOtherDistributioned":
                    if (detail == null) return;
                    e.DisplayText = detail.OtherMaterialDistributioned.HasValue ? detail.OtherMaterialDistributioned.ToString() : "0";
                    break;
                //case "colDepotDistributed":
                //    if (madeProduct != null)
                //        e.DisplayText = "單擊此處,查看詳細";
                //    break;
                //case "gridColumnXOId":
                //      Model.MPSdetails mPSdetails = this.mPSdetailsManager.Get(details[e.ListSourceRowIndex].MPSdetailsId) ;
                //      if(mPSdetails!=null) e.DisplayText = mPSdetails.InvoiceXOId;
                //      break;

            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.gridView1.FocusedColumn.Name == "gridColumn3")
                {
                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.MRSdetails).Product;
                        this.repositoryItemComboBox1.Items.Clear();
                        if (p != null)
                        {
                            if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                            {
                                BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                                IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
                                foreach (Model.ProductUnit item in unitList)
                                {
                                    this.repositoryItemComboBox1.Items.Add(item.CnName);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            MPSheader.ChooseMPSheaderForm f = new Book.UI.produceManager.MPSheader.ChooseMPSheaderForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (_mpsheader.Count != 0)
            {
                this.mrsheader.Details.Clear();
                foreach (Model.MPSheader mpsheader in _mpsheader)
                {
                    if (mpsheader.Details != null)
                        foreach (Model.MPSdetails mpsdetail in mpsheader.Details)
                        {
                            Model.MRSdetails mrsDetail = new Book.Model.MRSdetails();
                            mrsDetail.MRSdetailsId = Guid.NewGuid().ToString();
                            if (mpsdetail.Product != null)
                            {
                                mrsDetail.Product = mpsdetail.Product;
                                mrsDetail.ProductId = mpsdetail.Product.ProductId;
                            }
                            mrsDetail.ProductUnit = mpsdetail.ProductUnit;
                            mrsDetail.MRSdetailssum = mpsdetail.MPSdetailssum;
                            mrsDetail.MRSdetailsdes = "";
                            mrsDetail.MRSHeader = this.mrsheader;
                            mrsDetail.MRSHeaderId = this.mrsheader.MRSHeaderId;
                            mrsDetail.Customer = mpsdetail.Customer;
                            if (mpsdetail.Customer != null)
                                mrsDetail.CustomerId = mpsdetail.Customer.CustomerId;
                            mrsDetail.MPSheaderId = mpsdetail.MPSheaderId;
                            mrsDetail.MPSDate = mpsheader.MPSStartDate;
                            this.mrsheader.Details.Add(mrsDetail);
                        }
                }
                this.gridControl1.RefreshDataSource();
                _mpsheader.Clear();
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            //    string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            //    this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            this.bindingSourceWorkHouse.DataSource = new BL.WorkHouseManager().Select();
        }

        private void dateEditMRSstartdate_EditValueChanged(object sender, EventArgs e)
        {

            //if (this.dateEditMRSstartdate.EditValue != null && this.action != "view")
            //{
            //    this.mrsheader.MRSHeaderId = this.mrsheaderManager.GetId(this.dateEditMRSstartdate.DateTime);
            //    this.textEditMRSHeaderId.Text = this.mrsheader.MRSHeaderId;
            //}
        }

        //导出
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.saveFileDialog1.Filter = "Excel file|*.xls";
            if (this.saveFileDialog1.ShowDialog(this) != DialogResult.OK) return;
            this.gridView1.GridControl.ExportToXls(this.saveFileDialog1.FileName);

        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            if (this.bindingSourceDetails.Current != null)
            {
                this.mrsheader.Details.Remove(this.bindingSourceDetails.Current as Model.MRSdetails);
                this.gridControl1.RefreshDataSource();
            }

        }

        //查询
        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.mrsheader = f.SelectItem;
                this.action = "view";
                this.Refresh();
            }
            f.Dispose();
            GC.Collect();
        }

        //全选
        private void checkEditCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEditCheck.Checked == true)
            {
                foreach (Model.MRSdetails detail in this.mrsheader.Details)
                {
                    detail.CheckSign = true;
                }
            }
            if (this.checkEditCheck.Checked == false)
            {
                foreach (Model.MRSdetails detail in this.mrsheader.Details)
                {
                    detail.CheckSign = false;
                }
            }
            this.gridControl1.RefreshDataSource();
        }

        //生成领料单
        private void btn_MakeProduceMaterial_Click(object sender, EventArgs e)
        {
            IList<Model.MRSdetails> invoices = this.mrsheader.Details.Where(n => n.CheckSign == true).ToList();
            if (invoices == null || invoices.Count() == 0)
            {
                MessageBox.Show("沒有要生成的數據！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ProduceMaterial.EditForm form = new Book.UI.produceManager.ProduceMaterial.EditForm(invoices);
            //MainForm f = new MainForm();
            form.ShowDialog(this);

            //foreach (var item in invoices)
            //{
            //    item.MaterialDesc = "已生成領料單";
            //    this.mrsdetailManager.Update(item);
            //    item.CheckSign = false;
            //}
            this.bindingSourceDetails.DataSource = this.mrsheader.Details = this.mrsdetailManager.Select(this.mrsheader);
            this.gridControl1.RefreshDataSource();
        }

        //查看领料单
        private void simpleButtonProduceMaterial_Click(object sender, EventArgs e)
        {
            if (this.mrsheader == null) return;
            PronoteHeader.SelectProduceMaterial form = new PronoteHeader.SelectProduceMaterial(this.mrsheader);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (form.SelectItem == null) return;
                Model.ProduceMaterial pro = form.SelectItem as Model.ProduceMaterial;
                if (pro == null) return;
                ProduceMaterial.EditForm aa = new ProduceMaterial.EditForm(pro, "view");
                aa.ShowDialog();
            }
        }

        //生成加工单 -- 形成生产排成
        private void simpleButtonPronoteHeader_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確認要形成排程單?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            if (this.mrsheader == null) return;
            if (this.mrsheader.Details.Count != 0)
            {
                if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                    return;
                var invoices = from c in this.mrsheader.Details
                               where c.CheckSign.HasValue && c.CheckSign == true
                               select c;
                if (invoices == null || invoices.Count() == 0)
                {
                    MessageBox.Show("請選擇需要排單的商品！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int no = 0;

                foreach (Model.MRSdetails _mrsdetail in invoices)
                {
                    if (!string.IsNullOrEmpty(_mrsdetail.ArrangeDesc))
                    {
                        MessageBox.Show("選擇的商品中包含已經排單的商品!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
                try
                {


                    //BL.V.BeginTransaction();
                    foreach (Model.MRSdetails _mrsdetail in invoices)
                    {



                        no = 0;
                        pronoteHeader = new Book.Model.PronoteHeader();


                        pronoteHeader.PronoteHeaderID = pronoteHeaderManager.GetId();// Guid.NewGuid().ToString();
                        pronoteHeader.PronoteDate = DateTime.Now;
                        pronoteHeader.Employee0 = this.mrsheader.Employee1;
                        if (pronoteHeader.Employee0 != null)
                            pronoteHeader.Employee0Id = pronoteHeader.Employee0.EmployeeId;
                        pronoteHeader.ProductId = _mrsdetail.ProductId;
                        pronoteHeader.MRSdetailsId = _mrsdetail.MRSdetailsId;
                        pronoteHeader.MRSHeaderId = this.mrsheader.MRSHeaderId;
                        pronoteHeader.InDepotQuantity = 0;
                        Model.InvoiceXO invoicexo = this.invoiceXOManager.SelectMpsIsClose(this.mrsheader.MPSheaderId);
                        pronoteHeader.InvoiceStatus = 1;

                        pronoteHeader.HandbookId = _mrsdetail.HandbookId;
                        pronoteHeader.HandbookProductId = _mrsdetail.HandbookProductId;


                        string KeyIdName = Model.PronoteHeader.PRO_PronoteHeaderID; ;
                        string tableName = "PronoteHeader";
                        //string tableDesc = "生產加工";
                        Model.RoleAuditing roleAuditing = null;

                        if (new BL.RoleAuditingManager().IsNeedAuditByTableName(tableName))
                        {
                            roleAuditing = new Book.Model.RoleAuditing();
                            roleAuditing.RoleAuditingId = Guid.NewGuid().ToString();
                            roleAuditing.AuditRank = 0;
                            roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                            if (roleAuditing.NextAuditRole != null)
                                roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                            roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                            roleAuditing.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                            roleAuditing.InsertTime = DateTime.Now;

                            roleAuditing.InvoiceName = "生產加工";
                            roleAuditing.TableName = tableName;

                            this.pronoteHeader.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;

                        }


                        if (invoicexo != null)
                        {
                            pronoteHeader.InvoiceXOId = invoicexo.InvoiceId;
                            pronoteHeader.InvoiceCusId = invoicexo.CustomerInvoiceXOId;
                            pronoteHeader.InvoiceXODetailQuantity = _mrsdetail.MRSdetailsQuantity;
                            //foreach (Model.InvoiceXODetail detail in xodetailManager.Select(invoicexo, false))
                            //{
                            //    if (detail.ProductId == _mrsdetail.MadeProductId)
                            //        pronoteHeader.InvoiceXODetailQuantity = detail.InvoiceXODetailQuantity.Value;
                            //}
                        }
                        pronoteHeader.WorkHouseId = _mrsdetail.WorkHouseNextId;

                        pronoteHeader.DetailsSum = _mrsdetail.MRSdetailssum;//- (_mrsdetail.MRSHasSingleSum == null ? 0 : _mrsdetail.MRSHasSingleSum);
                        pronoteHeader.ProductUnit = _mrsdetail.ProductUnit;
                        SqlParameter sp = new SqlParameter("@productid", SqlDbType.VarChar, 50);
                        sp.Value = _mrsdetail.ProductId;
                        IList<Model.BomParentPartInfo> bompar = this.bomParentPartInfoManager.DataReaderBind<Model.BomParentPartInfo>(" select Top 1 BomId,TechonlogyHeaderId from BomParentPartInfo where productId=@productid and Status=0", new SqlParameter[] { sp }, CommandType.Text);
                        if (bompar == null || bompar.Count == 0)
                        {
                            MessageBox.Show(this, "請檢驗商品" + _mrsdetail.Product.ProductName + "的BOM資料是否建立或者啟用");
                            throw new Exception();
                        }

                        Model.BomParentPartInfo bomP = bompar[0];

                        //配料
                        SqlParameter[] sqlpar = new SqlParameter[] { new SqlParameter("@BomId", SqlDbType.VarChar, 50) };
                        sqlpar[0].Value = bomP.BomId;
                        foreach (Model.BomComponentInfo component in this.bomComponentInfoManager.DataReaderBind<Model.BomComponentInfo>(" SELECT ProductId,UseQuantity,SubLoseRate,Unit  FROM   [BomComponentInfo] WHERE [BomId] = @BomId ", sqlpar, CommandType.Text))
                        {
                            materials = new Model.PronotedetailsMaterial();
                            materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
                            materials.ProductId = component.ProductId;
                            materials.PronoteHeaderID = pronoteHeader.PronoteHeaderID;
                            materials.PronoteQuantity = component.UseQuantity * pronoteHeader.DetailsSum * (1 + 0.01 * (component.SubLoseRate == null ? 0 : component.SubLoseRate));
                            materials.ProductUnit = component.Unit;
                            no = no + 1;
                            materials.Inumber = no;
                            pronoteHeader.DetailsMaterial.Add(materials);
                        }
                        //包装
                        if (this.mrsheader.SourceType == "4")
                        {

                            IList<Model.BomPackageDetails> packageList = this.bomPackageDetailsManager.DataReaderBind<Model.BomPackageDetails>(" SELECT ProductId,Quantity,PackageUnit  FROM   [bomPackageDetails] WHERE [BomId] = @BomId ", sqlpar, CommandType.Text);
                            if (packageList != null && packageList.Count > 0)
                            {
                                foreach (Model.BomPackageDetails item in packageList)
                                {
                                    Model.PronotedetailsMaterial materials = new Model.PronotedetailsMaterial();
                                    materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
                                    //materials.Product = item.Product;
                                    materials.ProductId = item.ProductId;
                                    materials.PronoteHeader = pronoteHeader;
                                    materials.PronoteHeaderID = pronoteHeader.PronoteHeaderID;
                                    //string b = "";
                                    //if (double.Parse(this.calcEditQuantity.Value.ToString()) % item.UseQuantity != 0)
                                    //{
                                    //    b = ((double.Parse(this.calcEditQuantity.Value.ToString()) / item.UseQuantity) + 1).ToString();
                                    //    materials.PronoteQuantity =double.Parse( b.Substring(0, b.IndexOf('.') < 0 ? b.Length : b.Length - b.IndexOf('.') - 1));
                                    //}
                                    //else
                                    //{
                                    //    materials.PronoteQuantity = double.Parse(this.calcEditQuantity.Value.ToString()) / item.UseQuantity;
                                    //}

                                    materials.PronoteQuantity = pronoteHeader.DetailsSum * item.Quantity;
                                    materials.ProductUnit = item.PackageUnit;
                                    materials.MPSQuantity = pronoteHeader.DetailsSum * item.Quantity;
                                    // materials.MPS headerId = mrsdetail.MRSHeader.MPSheaderId;
                                    no = no + 1;
                                    materials.Inumber = no;
                                    pronoteHeader.DetailsMaterial.Add(materials);
                                }
                            }
                        }

                        //工序
                        if (!string.IsNullOrEmpty(bomP.TechonlogyHeaderId))
                        {
                            // Model.TechonlogyHeader techonlogyHeader = techonlogyHeaderManager.Get(bomP.TechonlogyHeaderId);
                            SqlParameter[] sqlte = new SqlParameter[] { new SqlParameter("@TechonlogyHeaderId", SqlDbType.VarChar, 50) };
                            sqlte[0].Value = bomP.TechonlogyHeaderId;
                            IList<Model.Technologydetails> tedetail = technologydetailsManager.DataReaderBind<Model.Technologydetails>(" SELECT t.TechnologydetailsNo,t.ProceduresId,p.WorkHouseId,p.SupplierId,p.IsOtherProduceOther PackageUnit  FROM   [Technologydetails] t left join Procedures p on t.ProceduresId=p.ProceduresId WHERE [TechonlogyHeaderId] = @TechonlogyHeaderId ", sqlte, CommandType.Text);
                            if (tedetail != null)
                            {
                                foreach (Model.Technologydetails technologydetails in tedetail)
                                {
                                    pronoteProceduresDetail = new Book.Model.PronoteProceduresDetail();
                                    pronoteProceduresDetail.PronoteProceduresDetailId = Guid.NewGuid().ToString();
                                    pronoteProceduresDetail.ProceduresNo = technologydetails.TechnologydetailsNo;
                                    pronoteProceduresDetail.ProceduresId = technologydetails.ProceduresId;
                                    pronoteProceduresDetail.WorkHouseId = technologydetails.WorkHouseId;
                                    if (tedetail.IndexOf(technologydetails) == 0)
                                        pronoteProceduresDetail.PronoteProceduresDate = _mrsdetail.JiaoHuoDate;
                                    if (technologydetails.Procedures != null)
                                    {
                                        pronoteProceduresDetail.IsOtherProduceOther = technologydetails.IsOtherProduceOther;
                                        pronoteProceduresDetail.SupplierId = technologydetails.SupplierId;
                                    }
                                    pronoteHeader.DetailProcedures.Add(pronoteProceduresDetail);
                                }
                            }
                        }
                        if (pronoteHeader != null && !string.IsNullOrEmpty(pronoteHeader.PronoteHeaderID))
                        {
                            //pronoteHeaderManager.Insert(pronoteHeader);
                            //订单详细是否已经生成完毕                        

                            //新增加工单
                            pronoteHeaderManager.InsertWithOutTrans(pronoteHeader);

                            if (roleAuditing != null)
                            {
                                roleAuditing.InvoiceId = pronoteHeader.PronoteHeaderID;
                                new BL.RoleAuditingManager().Insert(roleAuditing);
                            }
                            foreach (Model.MRSdetails item in invoices)
                            {
                                //if (item.CheckSign.HasValue && item.CheckSign.Value)
                                //{
                                //    if (!string.IsNullOrEmpty(item.ArrangeDesc)) continue;
                                //item.DetailsFlag = 2;
                                //item.MRSHasSingleSum = item.MRSdetailssum;
                                item.ArrangeDesc = "已經排單";
                                //this.mrsdetailManager.Update(item);
                                //}
                            }
                            this.gridControl1.RefreshDataSource();

                        }
                    }
                    //BL.V.CommitTransaction();
                    MessageBox.Show("單據形成成功", this.Text, MessageBoxButtons.OK);
                    this.checkEditCheck.Checked = false;
                }
                catch (Exception ex)
                {
                    //BL.V.RollbackTransaction();
                    foreach (Model.MRSdetails item in this.mrsheader.Details)
                    {
                        if (item.CheckSign.HasValue && item.CheckSign.Value)
                        {
                            if (string.IsNullOrEmpty(item.ArrangeDesc)) continue;
                            item.ArrangeDesc = "";
                        }
                    }
                    pronoteHeader = null;
                    MessageBox.Show("單據形成失敗", this.Text, MessageBoxButtons.OK);


                    this.gridControl1.RefreshDataSource();
                    return;
                }
                //if (pronoteHeader != null && !string.IsNullOrEmpty(pronoteHeader.PronoteHeaderID))
                //{
                //    pronoteHeaderManager.Insert(pronoteHeader);
                //    //订单详细是否已经生成完毕
                //    try
                //    {
                //        BL.V.BeginTransaction();

                //        foreach (Model.MRSdetails item in this.mrsheader.Details)
                //        {                          
                //            if(item.CheckSign.HasValue&&item.CheckSign.Value)
                //            {
                //                if (!string.IsNullOrEmpty(item.ArrangeDesc)) continue;
                //                item.ArrangeDesc = "已經排單";
                //                this.mrsdetailManager.Update(item);
                //            }
                //        }
                //        MessageBox.Show("單據形成成功", this.Text, MessageBoxButtons.OK);                     

                //        BL.V.CommitTransaction();
                //    }
                //    catch (Exception ex)
                //    {
                //        BL.V.RollbackTransaction();
                //        throw ex;
                //    }
                //}
            }
        }

        //查看加工单 -- 查看生产排成
        private void simplePronoteHeaderQuery_Click(object sender, EventArgs e)
        {
            if (mrsheader == null || this.action != "view") return;
            PronoteHeader.ListForm f = new Book.UI.produceManager.PronoteHeader.ListForm(this.mrsheader, int.Parse(this.mrsheader.SourceType));
            f.ShowDialog();

            this.mrsheader.Details = this.mrsdetailManager.Select(mrsheader);
            this.bindingSourceDetails.DataSource = this.mrsheader.Details;
            this.gridControl1.RefreshDataSource();
        }

        //生成采购订单
        public void sbtn_buitInvoice_Click(object sender, EventArgs e)
        {
            //if (this.mrsheader.IsbuiltInvoiceCO == null || !this.mrsheader.IsbuiltInvoiceCO.Value)

            BL.SupplierProductManager SupProManager = new Book.BL.SupplierProductManager();
            Dictionary<string, string> PriceRange = new Dictionary<string, string>();
            //获得所有要生成采购订单的单价
            foreach (var item in this.mrsheader.Details)
            {
                if (PriceRange.ContainsKey(item.ProductId))
                    continue;
                PriceRange.Add(item.ProductId, SupProManager.GetPriceRangeForSupAndProduct(item.SupplierId, item.ProductId));
                if (item.SupplierId == null)
                {
                    MessageBox.Show("廠商不能為空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (this.mrsheader.Details.Count != 0)
            {
                if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                    return;
                var invoices = from c in this.mrsheader.Details
                               where c.CheckSign.HasValue && c.CheckSign.Value && string.IsNullOrEmpty(c.ArrangeDesc)
                               group c by c.SupplierId;

                if (invoices == null || invoices.Count() == 0)
                {
                    MessageBox.Show("沒有要生成的數據！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (IGrouping<string, Model.MRSdetails> groups in invoices)
                {
                    this._invoiceCO = new Book.Model.InvoiceCO();
                    this._invoiceCO.Details = new List<Model.InvoiceCODetail>();
                    this._invoiceCO.InvoiceId = this._invoiceCOManager.GetNewId();
                    this._invoiceCO.Employee1Id = BL.V.ActiveOperator.EmployeeId;
                    this._invoiceCO.Employee1 = BL.V.ActiveOperator.Employee;
                    this._invoiceCO.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                    this._invoiceCO.Employee0 = BL.V.ActiveOperator.Employee;
                    this._invoiceCO.SupplierId = groups.Key;
                    this._invoiceCO.Supplier = new BL.SupplierManager().Get(groups.Key);
                    this._invoiceCO.MRSHeaderId = this.mrsheader.MRSHeaderId;
                    this._invoiceCO.InvoiceDate = System.DateTime.Now;
                    this._invoiceCO.InsertTime = System.DateTime.Now;
                    this._invoiceCO.InvoiceTotal = 0;
                    this._invoiceCO.InvoiceFlag = 0;


                    //审核
                    string KeyIdName = null;
                    string tableName = null;
                    string tableDesc = null;
                    KeyIdName = Model.InvoiceCO.PROPERTY_INVOICEID;
                    tableName = "InvoiceCO";

                    tableDesc = "採購訂單";


                    Model.RoleAuditing roleAuditing = null;
                    if (new BL.RoleAuditingManager().IsNeedAuditByTableName(tableName))
                    {
                        roleAuditing = new Book.Model.RoleAuditing();
                        roleAuditing.RoleAuditingId = Guid.NewGuid().ToString();
                        roleAuditing.AuditRank = 0;
                        roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                        if (roleAuditing.NextAuditRole != null)
                            roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                        roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                        roleAuditing.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                        roleAuditing.InsertTime = DateTime.Now;

                        roleAuditing.InvoiceName = "採購訂單";
                        roleAuditing.TableName = tableName;
                        this._invoiceCO.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;

                    }



                    //客户批号
                    this._invoiceCO.SupplierLotNumber = this.textEditPiHao.Text;
                    if (this.newChooseCustomer.EditValue != null)
                    {
                        this._invoiceCO.CustomerId = (this.newChooseCustomer.EditValue as Model.Customer).CustomerId;
                    }
                    this._invoiceCO.InvoiceStatus = 1;
                    this._invoiceCO.InvoiceYjrq = (from n in groups select n.JiaoHuoDate).Max(p => p);
                    Model.MPSheader mps = this.mPSheaderManager.Get(this.mrsheader.MPSheaderId);
                    if (mps != null && !string.IsNullOrEmpty(mps.InvoiceXOId))
                    {
                        this._invoiceCO.InvoiceXOId = mps.InvoiceXOId;
                        Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mps.InvoiceXOId);
                        this._invoiceCO.InvoiceCustomXOId = invoiceXO == null ? "" : invoiceXO.CustomerInvoiceXOId;
                    }
                    this._invoiceCO.InvoiceStatus = Convert.ToInt32(global::Helper.InvoiceStatus.Normal);

                    //var invoice = from i in groups
                    //              group i by new { i.ProductId, i.ProductUnit } into cc
                    //              select new
                    //              {
                    //                  Count = (from t in cc select t.MRSdetailssum).Sum(),
                    //                  Product = from a in cc select a.Product,
                    //                  ProductId = cc.Key.ProductId,
                    //                  ProductUnit = cc.Key.ProductUnit,
                    //                  Supplier = from s in cc select s.Supplier,
                    //                  SupplierId = from sid in cc select sid.SupplierId,
                    //                  MPSdetailsId = from mrsdid in cc select mrsdid.MPSdetailsId,
                    //                  MRSHeaderId = from mrshid in cc select mrshid.MRSHeaderId,
                    //                  MRSdetailsdes = from mrss in cc select mrss.MRSdetailsdes,
                    //                  WorkHouseNextId = from mrss1 in cc select mrss1.WorkHouseNextId,
                    //              };

                    foreach (var item in groups)
                    {
                        //if (item.Count <= 0)
                        //    continue;
                        //this._invoiceCO.InvoiceYjrq = Convert.ToDateTime(item.JiaoHuoDate == null ? null : item.JiaoHuoDate.ToString());
                        this._invoiceCODetail = new Book.Model.InvoiceCODetail();
                        this._invoiceCODetail.InvoiceCODetailId = Guid.NewGuid().ToString();
                        this._invoiceCODetail.InvoiceId = this._invoiceCO.InvoiceId;
                        this._invoiceCODetail.ProductId = item.ProductId;
                        this._invoiceCODetail.Product = this.productManager.Get(item.ProductId);
                        this._invoiceCODetail.OrderQuantity = item.MRSdetailssum.HasValue ? item.MRSdetailssum.Value : 0;

                        this._invoiceCODetail.DetailsPriceRange = this._SupplierProductManager.GetPriceRangeForSupAndProduct(item.SupplierId.ToString(), item.ProductId.ToString());
                        this._invoiceCODetail.InvoiceCODetailPrice = BL.SupplierProductManager.CountPrice(this._invoiceCODetail.DetailsPriceRange, this._invoiceCODetail.OrderQuantity.Value);

                        //
                        this._invoiceCODetail.MRSdetailId = item.MRSdetailsId;

                        //赋值金额
                        //this._invoiceCODetail.InvoiceCODetailMoney = 0;
                        string _minPrice = this._invoiceCODetail.InvoiceCODetailPrice.HasValue ? this._invoiceCODetail.InvoiceCODetailPrice.ToString() : "0";
                        string _minOrderQuantity = this._invoiceCODetail.OrderQuantity.HasValue ? this._invoiceCODetail.OrderQuantity.ToString() : "0";
                        this._invoiceCODetail.InvoiceCODetailMoney = decimal.Parse(_minPrice) * decimal.Parse(_minOrderQuantity);
                        this._invoiceCODetail.ArrivalQuantity = 0;
                        this._invoiceCODetail.NoArrivalQuantity = this._invoiceCODetail.OrderQuantity.HasValue ? this._invoiceCODetail.OrderQuantity : 0;

                        this._invoiceCODetail.InvoiceCTQuantity = 0;
                        this._invoiceCODetail.DetailsFlag = 0;
                        this._invoiceCODetail.InvoiceProductUnit = item.ProductUnit;
                        this._invoiceCODetail.InvoiceCODetailNote = "";
                        if (item.WorkHouseNextId != null)
                            this._invoiceCODetail.NextWorkHouseId = item.WorkHouseNextId;
                        this._invoiceCO.Details.Add(this._invoiceCODetail);
                    }
                    if (_invoiceCO.Details.Count > 0)
                    {

                        this._invoiceCOManager.Insert(this._invoiceCO);
                        if (roleAuditing != null)
                        {
                            roleAuditing.InvoiceId = _invoiceCO.InvoiceId;
                            new BL.RoleAuditingManager().Insert(roleAuditing);

                        }
                    }
                }

                //订单详细是否已经生成完毕
                bool IsoverAll = true;
                foreach (var item in this.mrsheader.Details)
                {
                    if (!item.CheckSign.HasValue || !item.CheckSign.Value)
                        IsoverAll = false;
                    else
                    {
                        if (!string.IsNullOrEmpty(item.ArrangeDesc)) continue;
                        item.ArrangeDesc = "已經生成採購訂單 ";
                        this.mrsdetailManager.Update(item);
                    }
                }

                if (IsoverAll)
                {
                    this.mrsheader.IsbuiltInvoiceCO = true;
                    this.mrsheaderManager.UpdateHeader(this.mrsheader);
                    //this.sbtn_buitInvoice.Text = Properties.Resources.IsBuiltInvoiceCo;
                }

                // MessageBox.Show(Properties.Resources.BuitInvoiceCOIsSuccess, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                //CoSelectForm form = new CoSelectForm(this.mrsheader);
                //if (form.ShowDialog(this) == DialogResult.OK)
                //{
                //    Model.InvoiceCO _invoiceCO = form.SelectItem;
                //    Invoices.CO.EditForm f = new Invoices.CO.EditForm(_invoiceCO.InvoiceId);
                //    f.ShowDialog();
                //}

                //form.Dispose();
                //GC.Collect();
                //this.sbtn_buitInvoice.Enabled = false;

                MessageBox.Show("單據生成成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.simpleButton_ViewInvoiceCO.Enabled = true;
                this.checkEditCheck.Checked = false;
            }
            //else
            //{
            //    CoSelectForm form = new CoSelectForm(this.mrsheader);
            //    if (form.ShowDialog(this) == DialogResult.OK)
            //    {
            //        Model.InvoiceCO _invoiceCO = form.SelectItem;
            //        Invoices.CO.EditForm f = new Invoices.CO.EditForm(_invoiceCO.InvoiceId);
            //        f.ShowDialog();
            //    }
            //    form.Dispose();
            //    GC.Collect();
            //}
        }

        //查看采购单
        private void simpleButton_ViewInvoiceCO_Click(object sender, EventArgs e)
        {
            if (this.mrsheader == null) return;
            Invoices.CO.ListForm f = new Book.UI.Invoices.CO.ListForm(this.mrsheader);
            f.ShowDialog(this);

            //CoSelectForm form = new CoSelectForm(this.mrsheader);
            //if (form.ShowDialog(this) == DialogResult.OK)
            //{
            //    Model.InvoiceCO _invoiceCO = form.SelectItem;
            //    Invoices.CO.EditForm f = new Invoices.CO.EditForm(_invoiceCO.InvoiceId);
            //    f.ShowDialog();
            //}
            f.Dispose();
            GC.Collect();
            this.mrsheader.Details = this.mrsdetailManager.Select(mrsheader);
            this.bindingSourceDetails.DataSource = this.mrsheader.Details;
            this.gridControl1.RefreshDataSource();
        }

        //形成委外合同
        private void simpleButtonOther_Click(object sender, EventArgs e)
        {
            //if (this.mrsheader.IsbuiltInvoiceCO != null && this.mrsheader.IsbuiltInvoiceCO.Value)
            //{
            //    CompactSelectForm form = new CompactSelectForm(this.mrsheader);
            //    if (form.ShowDialog(this) == DialogResult.OK)
            //    {
            //        Model.ProduceOtherCompact pc = form.SelectItem;
            //        ProduceOtherCompact.EditForm fm = new ProduceOtherCompact.EditForm(pc);
            //        fm.ShowDialog();
            //    }
            //    form.Dispose();
            //    GC.Collect();

            //}
            //else
            {
                if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                    return;

                //获得单价
                BL.SupplierProductManager SupProManager = new Book.BL.SupplierProductManager();
                Dictionary<string, string> PriceRange = new Dictionary<string, string>();
                //获得所有要生成采购订单的单价
                foreach (var item in this.mrsheader.Details)
                {
                    if (PriceRange.ContainsKey(item.ProductId))
                        continue;
                    PriceRange.Add(item.ProductId, SupProManager.GetPriceRangeForSupAndProduct(item.SupplierId, item.ProductId));
                }

                var compacts = from cs in this.mrsheader.Details
                               where cs.CheckSign != null && cs.CheckSign.Value == true && string.IsNullOrEmpty(cs.ArrangeDesc)
                               group cs by cs.Product.SupplierId;

                if (compacts == null || compacts.Count() == 0)
                {
                    MessageBox.Show("沒有要生成的數據！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (IGrouping<string, Model.MRSdetails> groups in compacts)
                {
                    this._produceOtherCompact = new Model.ProduceOtherCompact();
                    this._produceOtherCompact.ProduceOtherCompactId = this._produceOtherCompactManager.GetId();
                    this._produceOtherCompact.SupplierId = groups.Key;
                    this._produceOtherCompact.Supplier = new BL.SupplierManager().Get(groups.Key);
                    this._produceOtherCompact.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                    this._produceOtherCompact.ProduceOtherCompactDate = DateTime.Now;
                    this._produceOtherCompact.InsertTime = DateTime.Now;
                    this._produceOtherCompact.UpdateTime = DateTime.Now;
                    this._produceOtherCompact.MRSHeaderId = this.mrsheader.MRSHeaderId;
                    this._produceOtherCompact.Details = new List<Model.ProduceOtherCompactDetail>();
                    this._produceOtherCompact.InvoiceStatus = 1;
                    this._produceOtherCompact.DetailMaterial = new List<Model.ProduceOtherCompactMaterial>();
                    this._produceOtherCompact.TempMaterials = new List<Model.ProduceOtherCompactMaterial>();




                    string KeyIdName = null;
                    string tableName = null;

                    string tableDesc = null;

                    // if (!string.IsNullOrEmpty(tableCode()))
                    //审核

                    KeyIdName = "ProduceOtherCompactId"; ;
                    tableName = "ProduceOtherCompact";
                    tableDesc = "委外合同";
                    Model.RoleAuditing roleAuditing = null;

                    if (new BL.RoleAuditingManager().IsNeedAuditByTableName(tableName))
                    {
                        roleAuditing = new Book.Model.RoleAuditing();
                        roleAuditing.RoleAuditingId = Guid.NewGuid().ToString();
                        roleAuditing.AuditRank = 0;
                        roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                        if (roleAuditing.NextAuditRole != null)
                            roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                        roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                        roleAuditing.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                        roleAuditing.InsertTime = DateTime.Now;
                        roleAuditing.InvoiceName = "委外合同";
                        roleAuditing.TableName = tableName;


                        this._produceOtherCompact.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;

                    }
                    Model.MPSheader mpsheader = this.mPSheaderManager.Get(this.mrsheader.MPSheaderId);
                    if (mpsheader != null)
                    {
                        this._produceOtherCompact.InvoiceXOId = mpsheader.InvoiceXOId;
                        if (this.newChooseCustomer.EditValue != null)
                            this._produceOtherCompact.CustomerId = (this.newChooseCustomer.EditValue as Model.Customer).CustomerId;
                        if (mpsheader.InvoiceXO != null)
                        {
                            this._produceOtherCompact.CustomerInvoiceXOId = mpsheader.InvoiceXO.CustomerInvoiceXOId;
                            this._produceOtherCompact.LotNumber = mpsheader.InvoiceXO.CustomerLotNumber;
                        }
                    }
                    //var Details = from c in groups
                    //              group c by new { c.ProductId, c.ProductUnit } into cc
                    //              select new
                    //              {
                    //                  Count = (from t in cc select t.MRSdetailssum).Sum(),
                    //                  ProductId = cc.Key.ProductId,
                    //                  ProductUnit = cc.Key.ProductUnit,
                    //              };
                    foreach (var cc in groups)
                    {
                        this._produceOtherCompact.JiaoHuoDate = cc.JiaoHuoDate;
                        this._produceOtherCompactDetail = new Model.ProduceOtherCompactDetail();
                        this._produceOtherCompactDetail.OtherCompactDetailId = Guid.NewGuid().ToString();
                        this._produceOtherCompactDetail.ProduceOtherCompactId = this._produceOtherCompact.ProduceOtherCompactId;
                        this._produceOtherCompactDetail.ProductId = cc.ProductId;
                        this._produceOtherCompactDetail.Product = this.productManager.Get(cc.ProductId);
                        this._produceOtherCompactDetail.ProductUnit = cc.ProductUnit;
                        this._produceOtherCompactDetail.JiaoQi = cc.JiaoHuoDate;

                        if (cc.CheckNums != null && cc.CheckNums != 0)
                            this._produceOtherCompactDetail.OtherCompactCount = cc.CheckNums.HasValue ? cc.CheckNums.Value : 0;
                        else
                            this._produceOtherCompactDetail.OtherCompactCount = cc.MRSdetailssum.HasValue ? cc.MRSdetailssum.Value : 0;

                        this._produceOtherCompactDetail.OtherCompactPrice = BL.SupplierProductManager.CountPrice(new BL.SupplierProductManager().GetPriceRangeForSupAndProduct(groups.Key, this._produceOtherCompactDetail.ProductId), this._produceOtherCompactDetail.OtherCompactCount.Value);

                        this._produceOtherCompactDetail.OtherCompactMoney = Convert.ToDecimal(this._produceOtherCompactDetail.OtherCompactCount) * this._produceOtherCompactDetail.OtherCompactPrice;

                        this._produceOtherCompactDetail.WorkHouseNextId = cc.WorkHouseNextId;
                        Model.MPSheader mpsHeader = this.mPSheaderManager.Get(this.mrsheader.MPSheaderId);
                        if (mpsHeader != null)
                        {
                            Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mpsHeader.InvoiceXOId);
                            if (invoiceXO != null)
                            {
                                this._produceOtherCompactDetail.CustomInvoiceXOId = invoiceXO.CustomerInvoiceXOId;
                                this._produceOtherCompact.LotNumber = invoiceXO.CustomerLotNumber;
                            }
                        }

                        //关联物料需求详细生成委外合同,用到物料需求详细主键编号
                        this._produceOtherCompactDetail.MRSdetailsId = cc.MRSdetailsId;

                        this._produceOtherCompact.Details.Add(this._produceOtherCompactDetail);

                        Model.Product pt = productManager.Get(cc.ProductId);

                        foreach (Model.BomComponentInfo com in this.bomComponentInfoManager.Select(this.bomParentPartInfoManager.Get(pt)))
                        {
                            //this._produceOtherCompactMaterial = new Book.Model.ProduceOtherCompactMaterial();
                            //this._produceOtherCompactMaterial.ProduceQuantity = cc.MRSdetailssum * com.UseQuantity;
                            //this._produceOtherCompactMaterial.Product = com.Product;
                            //this._produceOtherCompactMaterial.ProductId = com.Product.ProductId;
                            //this._produceOtherCompactMaterial.ProductUnit = com.Unit;
                            //this._produceOtherCompact.TempMaterials.Add(this._produceOtherCompactMaterial);

                            this._produceOtherCompactMaterial = new Book.Model.ProduceOtherCompactMaterial();
                            this._produceOtherCompactMaterial.ProduceOtherCompactMaterialId = Guid.NewGuid().ToString();

                            if (cc.CheckNums != null && cc.CheckNums != 0)
                                this._produceOtherCompactMaterial.ProduceQuantity = cc.CheckNums * com.UseQuantity * (1 + 0.01 * (com.SubLoseRate == null ? 0 : com.SubLoseRate));
                            else
                                this._produceOtherCompactMaterial.ProduceQuantity = cc.MRSdetailssum * com.UseQuantity * (1 + 0.01 * (com.SubLoseRate == null ? 0 : com.SubLoseRate));
                            this._produceOtherCompactMaterial.ProductId = com.Product.ProductId;
                            this._produceOtherCompactMaterial.Product = com.Product;
                            this._produceOtherCompactMaterial.ProductUnit = com.Unit;
                            this._produceOtherCompactMaterial.ParentProductId = cc.ProductId;
                            this._produceOtherCompact.DetailMaterial.Add(this._produceOtherCompactMaterial);
                        }

                        //var materials = from m in this._produceOtherCompact.TempMaterials
                        //                group m by new { m.ProductId, m.ProductUnit } into g
                        //                select new
                        //                {
                        //                    ProduceQuantity = (from x in g select x.ProduceQuantity).Sum(),
                        //                    ProductId = g.Key.ProductId,
                        //                    ProductUnit = g.Key.ProductUnit,
                        //                    stock = from p in g select p.Product.StocksQuantity, //g.Max(p => p.Product.StocksQuantity)
                        //                    ParentProductId = cc.ProductId,
                        //                };

                        //IList<string> a=new List<string>();
                        //foreach (var im in this._produceOtherCompact.TempMaterials)
                        //{
                        //    if (im.ProduceQuantity <= 0)
                        //        continue;
                        //    if(a.Contains( im.ProduceOtherCompactMaterialId);
                        //    this._produceOtherCompactMaterial = new Book.Model.ProduceOtherCompactMaterial();
                        //    this._produceOtherCompactMaterial.ProduceOtherCompactMaterialId = Guid.NewGuid().ToString();
                        //    this._produceOtherCompactMaterial.ProduceQuantity = im.ProduceQuantity;
                        //    this._produceOtherCompactMaterial.ProductId = im.ProductId;
                        //    this._produceOtherCompactMaterial.Product = this.productManager.Get(im.ProductId);
                        //    this._produceOtherCompactMaterial.ProductUnit = im.ProductUnit;
                        //    this._produceOtherCompactMaterial.ParentProductId = cc.ProductId;
                        //    this._produceOtherCompact.DetailMaterial.Add(this._produceOtherCompactMaterial);
                        //    a.Add(this._produceOtherCompactMaterial.ProduceOtherCompactMaterialId );
                        //}
                    }


                    if (this._produceOtherCompact.Details != null && this._produceOtherCompact.Details.Count > 0)
                    {
                        this._produceOtherCompactManager.Insert(this._produceOtherCompact);

                        if (roleAuditing != null)
                        {
                            roleAuditing.InvoiceId = this._produceOtherCompact.ProduceOtherCompactId;
                            new BL.RoleAuditingManager().Insert(roleAuditing);
                        }


                    }
                }

                //订单详细是否已经生成完毕
                bool IsoverAll = true;
                foreach (var item in this.mrsheader.Details)
                {
                    if (!item.CheckSign.HasValue || !item.CheckSign.Value)
                        IsoverAll = false;
                    else
                    {
                        if (!string.IsNullOrEmpty(item.ArrangeDesc)) continue;
                        item.ArrangeDesc = "已經形成委外合同";
                        this.mrsdetailManager.Update(item);
                    }
                }

                if (IsoverAll)
                {
                    this.mrsheader.IsbuiltInvoiceCO = true;
                    this.mrsheaderManager.UpdateHeader(this.mrsheader);
                    //simpleButtonOther.Text = Properties.Resources.IsBuiltTrustOut;
                }

                //CompactSelectForm form = new CompactSelectForm(this.mrsheader);
                //if (form.ShowDialog(this) == DialogResult.OK)
                //{
                //    Model.ProduceOtherCompact pc = form.SelectItem;
                //    ProduceOtherCompact.EditForm fm = new ProduceOtherCompact.EditForm(pc);
                //    fm.ShowDialog();
                //}

                //form.Dispose();
                //GC.Collect();

                MessageBox.Show("單據生成成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.simpleButton_ViewTrust.Enabled = true;
                this.checkEditCheck.Checked = false;
            }
        }

        //查看委外单
        private void simpleButton_ViewTrust_Click(object sender, EventArgs e)
        {
            if (mrsheader == null) return;
            ProduceOtherCompact.ListForm f = new ProduceOtherCompact.ListForm(this.mrsheader);
            f.ShowDialog();

            this.mrsheader.Details = this.mrsdetailManager.Select(mrsheader);
            this.bindingSourceDetails.DataSource = this.mrsheader.Details;
            this.gridControl1.RefreshDataSource();
        }

        //条件列印
        private void barBtnSomeParmSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionMRSChooseForm form = new Query.ConditionMRSChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Query.ConditionMRS condition = form.Condition as Query.ConditionMRS;
                RO1Details f = new RO1Details(condition);
                f.ShowPreviewDialog();
            }
            form.Dispose();
            GC.Collect();
        }

        private void textEditMRSheaderDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditMRSheaderDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Model.MRSdetails m_detail = this.bindingSourceDetails.Current as Model.MRSdetails;
            if (m_detail != null)
            {
                if (m_detail.Product != null)
                {
                    LookDepotDistributed f = new LookDepotDistributed(m_detail.Product);
                    f.ShowDialog();
                }
            }
        }

        //解析价格区间
        //DEMO: 0/0/0,0/0/0,0/0/0,0/0/0,0/0/0,0/0/0,0/999999999999/0
        //Means: {起始数量/终止数量/价格}
        private double AnalyzePriceRange(string priceR, double Quantity)
        {
            double result = 0;
            if (string.IsNullOrEmpty(priceR))
                return 0;
            string[] inPriceR;
            if (priceR.Contains(','))
                inPriceR = priceR.Split(',');
            else
                inPriceR = new string[] { priceR };

            double startRange, endRange, RangePrice;
            foreach (string s in inPriceR)
            {
                string[] prs = s.Split('/');

                startRange = Convert.ToDouble(prs[0]);
                endRange = Convert.ToDouble(prs[1]);
                RangePrice = Convert.ToDouble(prs[2]);

                if (Quantity >= startRange && Quantity <= endRange)
                {
                    result = RangePrice;
                    break;
                }
            }
            return result;
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.MRSHeader.PRO_MRSHeaderId;
        }

        protected override int AuditState()
        {
            return this.mrsheader.AuditState.HasValue ? this.mrsheader.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "MRSHeader" + "," + this.mrsheader.MRSHeaderId;
        }

        #endregion
    }
}

#region //private void sbtn_buitProg_Click(object sender, EventArgs e)
//{
//    if (this.mrsheader == null) return;
//    ProduceAbility.ProduceScheduleForm f = new Book.UI.produceManager.ProduceAbility.ProduceScheduleForm(this.mrsheader);
//    f.Show();

//}
#endregion

#region //点击数据单元格 private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
//{
//    if (e.Column.Name == "colDepotDistributed")
//    {
//        IList<Model.MRSdetails> details = this.bindingSourceDetails.DataSource as IList<Model.MRSdetails>;
//        if (details == null || details.Count < 1) return;
//        Model.Product detail = details[e.RowHandle].Product;

//        if (detail != null)
//        {
//            LookDepotDistributed f = new LookDepotDistributed(detail);
//            f.ShowDialog();
//        }
//    }
//}
#endregion

