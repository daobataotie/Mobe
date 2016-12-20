using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.BasicData.Supplier;
using Book.UI.Invoices;
using System.Collections;

namespace Book.UI.produceManager.ProduceOtherCompact
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        public static IList<Model.MPSdetails> _MPSdetails = new List<Model.MPSdetails>();
        Model.ProduceOtherCompact _produceOtherCompact = new Book.Model.ProduceOtherCompact();
        BL.ProduceOtherCompactManager produceOtherCompactManager = new Book.BL.ProduceOtherCompactManager();

        BL.ProduceOtherCompactDetailManager produceOtherCompactDetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        BL.ProduceOtherCompactMaterialManager produceOtherCompactMaterialManager = new Book.BL.ProduceOtherCompactMaterialManager();

        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        Model.MRSHeader _mrsheader;
        Model.ProduceOtherCompactDetail _produceOtherCompactDetail;
        Model.ProduceOtherCompactMaterial _produceOtherCompactMaterial;
        BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();
        BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        BL.MRSdetailsManager mRSdetailsManager = new BL.MRSdetailsManager();
        private int LastFlag = 0;

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceOtherCompact.PRO_ProduceOtherCompactId, new AA(Properties.Resources.RequireDataForId, this.textEditProduceOtherCompactId));

            this.invalidValueExceptions.Add(Model.ProduceOtherCompact.PRO_ProduceOtherCompactId, new AA(Properties.Resources.EntityExists, this.textEditProduceOtherCompactId));
            this.action = "view";
            this.newChooseEmployee0Id.Choose = new ChooseEmployee();
            this.newChooseEmployee1Id.Choose = new ChooseEmployee();
            this.newChooseSupplierId.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseDepartment.Choose = new Invoices.ChooseDepartment();
            this.EmpAudit.Choose = new ChooseEmployee();
        }

        public EditForm(Model.ProduceOtherCompact produceOtherCompact)
            : this()
        {
            this._produceOtherCompact = produceOtherCompact;
            this._produceOtherCompact.Details = this.produceOtherCompactDetailManager.Select(produceOtherCompact);
            this._produceOtherCompact.DetailMaterial = this.produceOtherCompactMaterialManager.Select(produceOtherCompact);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.ProduceOtherCompact produceOtherCompact, string action)
            : this()
        {
            this._produceOtherCompact = produceOtherCompact;
            this._produceOtherCompact.Details = this.produceOtherCompactDetailManager.Select(produceOtherCompact);
            this._produceOtherCompact.DetailMaterial = this.produceOtherCompactMaterialManager.Select(produceOtherCompact);
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.MRSHeader mrsheader)
            : this()
        {

            this._mrsheader = mrsheader;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void Save()
        {
            this._produceOtherCompact.ProduceOtherCompactId = this.textEditProduceOtherCompactId.Text;
            this._produceOtherCompact.ProduceOtherCompactDesc = this.textEditProduceOtherCompactDesc.Text;
            this._produceOtherCompact.OtherProduceType = this.comboBoxProduceType.SelectedIndex;
            //this._produceOtherCompact.OtherOperationType = this.comboBoxOperationType.SelectedIndex;
            this._produceOtherCompact.PaymentCondition = this.textEditPaymentCondition.Text;

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceOtherCompactDate.DateTime, new DateTime()))
            {
                this._produceOtherCompact.ProduceOtherCompactDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._produceOtherCompact.ProduceOtherCompactDate = this.dateEditProduceOtherCompactDate.DateTime;
            }
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditJHDate.DateTime, new DateTime()))
            //{
            //    this._produceOtherCompact.JiaoHuoDate = global::Helper.DateTimeParse.NullDate;
            //}
            //else
            //{
            //    this._produceOtherCompact.JiaoHuoDate = this.dateEditJHDate.DateTime;
            //}

            this._produceOtherCompact.Supplier = (this.newChooseSupplierId.EditValue as Model.Supplier);
            if (this._produceOtherCompact.Supplier != null)
            {
                this._produceOtherCompact.SupplierId = this._produceOtherCompact.Supplier.SupplierId;
            }
            this._produceOtherCompact.Employee0 = this.newChooseEmployee0Id.EditValue as Model.Employee;
            if (this._produceOtherCompact.Employee0 != null)
            {
                this._produceOtherCompact.Employee0Id = this._produceOtherCompact.Employee0.EmployeeId;
            }
            this._produceOtherCompact.Employee1 = this.newChooseEmployee1Id.EditValue as Model.Employee;
            if (this._produceOtherCompact.Employee1 != null)
            {
                this._produceOtherCompact.Employee1Id = this._produceOtherCompact.Employee1.EmployeeId;
            }
            this._produceOtherCompact.AuditState = this.saveAuditState;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this.produceOtherCompactManager.Insert(this._produceOtherCompact);
                    break;

                case "update":
                    this.produceOtherCompactManager.Update(this._produceOtherCompact);
                    break;
            }

        }

        protected override void Delete()
        {

            if (this.produceOtherCompactManager == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            // try
            //{
            this.produceOtherCompactManager.Delete(this._produceOtherCompact);
            this._produceOtherCompact = this.produceOtherCompactManager.GetNext(this._produceOtherCompact);
            if (this._produceOtherCompact == null)
            {
                this._produceOtherCompact = this.produceOtherCompactManager.GetLast();
            }
            // TreeLoad();
        }

        public override void Refresh()
        {

            if (this._produceOtherCompact == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {

                    this._produceOtherCompact = this.produceOtherCompactManager.GetDetails(_produceOtherCompact.ProduceOtherCompactId);

                }

            }

            this.textEditProduceOtherCompactId.Text = this._produceOtherCompact.ProduceOtherCompactId;
            this.textEditProduceOtherCompactDesc.Text = this._produceOtherCompact.ProduceOtherCompactDesc;
            // this.comboBoxOperationType.SelectedIndex =this._produceOtherCompact.OtherOperationType==null?-1: this._produceOtherCompact.OtherOperationType.Value;
            this.comboBoxProduceType.SelectedIndex = this._produceOtherCompact.OtherProduceType == null ? -1 : this._produceOtherCompact.OtherProduceType.Value;
            this.textEditPaymentCondition.Text = this._produceOtherCompact.PaymentCondition;
            if (global::Helper.DateTimeParse.DateTimeEquls(this._produceOtherCompact.ProduceOtherCompactDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceOtherCompactDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceOtherCompactDate.EditValue = this._produceOtherCompact.ProduceOtherCompactDate;
            }
            //if (global::Helper.DateTimeParse.DateTimeEquls(this._produceOtherCompact.JiaoHuoDate, global::Helper.DateTimeParse.NullDate))
            //{
            //    this.dateEditJHDate.EditValue = null;
            //}
            //else
            //{
            //    this.dateEditJHDate.EditValue = this._produceOtherCompact.JiaoHuoDate;
            //}


            this.newChooseEmployee0Id.EditValue = this._produceOtherCompact.Employee0;
            this.newChooseEmployee1Id.EditValue = this._produceOtherCompact.Employee1;
            this.newChooseSupplierId.EditValue = this._produceOtherCompact.Supplier;
            if (!string.IsNullOrEmpty(this._produceOtherCompact.MRSHeaderId))
            {
                Model.MRSHeader mRSHeader = this.mRSHeaderManager.Get(this._produceOtherCompact.MRSHeaderId);
                if (mRSHeader != null)
                {
                    Model.MPSheader mPSheader = this.mPSheaderManager.Get(mRSHeader.MPSheaderId);
                    if (mPSheader != null)
                    {
                        this.textEditCustomerXOId.Text = this.invoiceXOManager.Get(mPSheader.InvoiceXOId) == null ? "" : this.invoiceXOManager.Get(mPSheader.InvoiceXOId).CustomerInvoiceXOId;
                    }
                    else
                        this.textEditCustomerXOId.Text = string.Empty;
                }
                else
                    this.textEditCustomerXOId.Text = string.Empty;
            }
            else
                this.textEditCustomerXOId.Text = string.Empty;

            this.textEditAuditState.Text = this.GetAuditName(this._produceOtherCompact.AuditState);
            this.txtLotNumber.Text = this._produceOtherCompact.LotNumber;

            this.EmpAudit.EditValue = this._produceOtherCompact.AuditEmp;
            this.bindingSourceDetails.DataSource = this._produceOtherCompact.Details;
            this.bindingSourceMaterial.DataSource = this._produceOtherCompact.DetailMaterial;
            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.barButtonItemProcedures.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.barButtonItemProcedures.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.barButtonItemProcedures.Enabled = false;
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.gridView2.OptionsBehavior.Editable = false;
                    break;
            }

            this.textEditProduceOtherCompactId.Properties.ReadOnly = true;
            this.txtLotNumber.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.ProduceOtherCompact produceOtherCompact = this.produceOtherCompactManager.GetNext(this._produceOtherCompact);
            if (produceOtherCompact == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceOtherCompact = this.produceOtherCompactManager.Get(produceOtherCompact.ProduceOtherCompactId);
        }

        protected override void MovePrev()
        {
            Model.ProduceOtherCompact produceOtherCompact = this.produceOtherCompactManager.GetPrev(this._produceOtherCompact);
            if (produceOtherCompact == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceOtherCompact = this.produceOtherCompactManager.Get(produceOtherCompact.ProduceOtherCompactId);
        }

        protected override void MoveFirst()
        {
            this._produceOtherCompact = this.produceOtherCompactManager.Get(this.produceOtherCompactManager.GetFirst() == null ? "" : this.produceOtherCompactManager.GetFirst().ProduceOtherCompactId);
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            //  if (produceOtherCompact == null)
            // {
            this._produceOtherCompact = this.produceOtherCompactManager.Get(this.produceOtherCompactManager.GetLast() == null ? "" : this.produceOtherCompactManager.GetLast().ProduceOtherCompactId);
            // }
        }

        protected override bool HasRows()
        {
            return this.produceOtherCompactManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceOtherCompactManager.HasRowsAfter(this._produceOtherCompact);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceOtherCompactManager.HasRowsBefore(this._produceOtherCompact);
        }

        protected override void AddNew()
        {
            this._produceOtherCompact = new Model.ProduceOtherCompact();
            this._produceOtherCompact.ProduceOtherCompactDate = DateTime.Now;
            this._produceOtherCompact.ProduceOtherCompactId = this.produceOtherCompactManager.GetId();// Guid.NewGuid().ToString();
            this._produceOtherCompact.Employee0 = BL.V.ActiveOperator.Employee;
            this._produceOtherCompact.Details = new List<Model.ProduceOtherCompactDetail>();
            this._produceOtherCompact.DetailMaterial = new List<Model.ProduceOtherCompactMaterial>();
            this._produceOtherCompact.TempMaterials = new List<Model.ProduceOtherCompactMaterial>();
            //if (this.action == "insert")
            //{
            //Model.ProduceOtherCompactDetail detail = new Model.ProduceOtherCompactDetail();
            //detail.OtherCompactDetailId = Guid.NewGuid().ToString();
            //detail.OtherCompactCount = 0;
            //detail.OtherCompactPrice = 0;
            //detail.InDepotCount = 0;
            //detail.OtherCompactMoney = 0;
            //detail.ProductSpecification = "";
            //detail.Product = new Book.Model.Product();
            //this.produceOtherCompact.Details.Add(detail);
            //this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            // }
            if (this._mrsheader != null)
            {
                foreach (Model.MRSdetails mrsdetail in _mrsheader.Details)
                {
                    this._produceOtherCompactDetail = new Book.Model.ProduceOtherCompactDetail();
                    this._produceOtherCompactDetail.OtherCompactDetailId = Guid.NewGuid().ToString();
                    this._produceOtherCompactDetail.OtherCompactCount = mrsdetail.MRSdetailssum;
                    this._produceOtherCompactDetail.Product = mrsdetail.Product;
                    this._produceOtherCompactDetail.ProductId = mrsdetail.Product.ProductId;
                    this._produceOtherCompactDetail.ProductUnit = mrsdetail.ProductUnit;
                    this._produceOtherCompact.Details.Add(this._produceOtherCompactDetail);
                }
                foreach (Model.MRSdetails mrsdetail in _mrsheader.Details)
                {

                    foreach (Model.BomComponentInfo com in this.bomComponentInfoManager.Select(this.bomParentPartInfoManager.Get(mrsdetail.Product)))
                    {
                        this._produceOtherCompactMaterial = new Book.Model.ProduceOtherCompactMaterial();
                        this._produceOtherCompactMaterial.ProduceQuantity = mrsdetail.MRSdetailssum * com.UseQuantity;
                        this._produceOtherCompactMaterial.Product = com.Product;
                        this._produceOtherCompactMaterial.ProductId = com.Product.ProductId;
                        this._produceOtherCompactMaterial.ProductUnit = com.Unit;
                        this._produceOtherCompact.TempMaterials.Add(this._produceOtherCompactMaterial);

                    }
                }

                var materials = from m in this._produceOtherCompact.TempMaterials
                                group m by new { m.ProductId, m.ProductUnit } into g
                                select new
                                {
                                    ProduceQuantity = (from x in g select x.ProduceQuantity).Sum(),
                                    ProductId = g.Key.ProductId,
                                    ProductUnit = g.Key.ProductUnit,
                                    stock = g.Max(p => p.Product.StocksQuantity)
                                };

                foreach (var item in materials)
                {
                    this._produceOtherCompactMaterial = new Book.Model.ProduceOtherCompactMaterial();
                    this._produceOtherCompactMaterial.ProduceOtherCompactMaterialId = Guid.NewGuid().ToString();
                    this._produceOtherCompactMaterial.ProduceQuantity = item.ProduceQuantity;
                    this._produceOtherCompactMaterial.ProductId = item.ProductId;
                    this._produceOtherCompactMaterial.ProductUnit = item.ProductUnit;
                    this._produceOtherCompact.DetailMaterial.Add(this._produceOtherCompactMaterial);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this._produceOtherCompact.Details.Count > 0 && this._produceOtherCompact.Details[0] != null && string.IsNullOrEmpty(this._produceOtherCompact.Details[0].ProductId))
                    this._produceOtherCompact.Details.RemoveAt(0);
                Model.ProduceOtherCompactDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {

                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.ProduceOtherCompactDetail();
                        detail.OtherCompactDetailId = Guid.NewGuid().ToString();
                        detail.Product = this.productManager.Get(product.ProductId);
                        detail.ProductId = product.ProductId;
                        detail.OtherCompactCount = 0;
                        detail.OtherCompactPrice = 0;
                        detail.InDepotCount = 0;
                        detail.ProductUnit = product.DepotUnit == null ? null : product.DepotUnit.CnName;
                        detail.OtherCompactMoney = 0;
                        Model.BomParentPartInfo bompar = this.bomParentPartInfoManager.Get(product);
                        if (bompar != null)
                        {

                            foreach (Model.BomComponentInfo comm in this.bomComponentInfoManager.Select(bompar))
                            {
                                Model.ProduceOtherCompactMaterial mater = new Model.ProduceOtherCompactMaterial();
                                mater.ProduceOtherCompactMaterialId = Guid.NewGuid().ToString();
                                mater.Product = comm.Product;
                                mater.ProductId = comm.ProductId;
                                mater.ProductUnit = comm.Unit;
                                mater.ProduceQuantity = 0;
                                mater.ParentProduct = product;
                                mater.ParentProductId = product.ProductId;

                                this._produceOtherCompact.DetailMaterial.Add(mater);
                            }
                        }

                        // detail.Inumber = this._produceOtherMaterial.Details.Count + 1;
                        //detail.ProductUnit = detail.Product.MainUnit == null ? null : detail.Product.MainUnit.CnName;
                        detail.ProductSpecification = product.ProductSpecification;
                        detail.Inumber = this._produceOtherCompact.Details.Count + 1;
                        this._produceOtherCompact.Details.Add(detail);
                    }
                }

                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.ProduceOtherCompactDetail();
                    detail.OtherCompactDetailId = Guid.NewGuid().ToString();
                    detail.Product = this.productManager.Get((f.SelectedItem as Model.Product).ProductId);
                    detail.ProductId = detail.Product.ProductId;
                    detail.OtherCompactCount = 0;
                    detail.OtherCompactPrice = 0;
                    detail.InDepotCount = 0;
                    detail.OtherCompactMoney = 0;
                    detail.ProductUnit = detail.Product.DepotUnit == null ? null : detail.Product.DepotUnit.CnName;
                    Model.BomParentPartInfo bompar = this.bomParentPartInfoManager.Get(detail.Product);
                    if (bompar != null)
                    {

                        foreach (Model.BomComponentInfo comm in this.bomComponentInfoManager.Select(bompar))
                        {
                            Model.ProduceOtherCompactMaterial mater = new Model.ProduceOtherCompactMaterial();
                            mater.ProduceOtherCompactMaterialId = Guid.NewGuid().ToString();
                            mater.Product = comm.Product;
                            mater.ProductId = comm.ProductId;
                            mater.ProductUnit = comm.Unit;
                            mater.ProduceQuantity = 0;
                            mater.ParentProduct = product;
                            mater.ParentProductId = product.ProductId;
                            this._produceOtherCompact.DetailMaterial.Add(mater);
                        }
                    }

                    // detail.Inumber = this._produceOtherMaterial.Details.Count + 1;
                    //detail.ProductUnit = detail.Product.MainUnit == null ? null : detail.Product.MainUnit.CnName;
                    detail.ProductSpecification = (f.SelectedItem as Model.Product).ProductSpecification;
                    detail.Inumber = this._produceOtherCompact.Details.Count + 1;
                    this._produceOtherCompact.Details.Add(detail);

                }

                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                //this.bindingSourceMaterial.Position = this.bindingSourceMaterial.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
                this.gridControl2.RefreshDataSource();

            }
            f.Dispose();
            System.GC.Collect();

            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    Model.Product product = f.SelectedItem as Model.Product;
            //    Model.ProduceOtherCompactDetail detail = new Book.Model.ProduceOtherCompactDetail();
            //    detail.OtherCompactDetailId = Guid.NewGuid().ToString();
            //    detail.Product = f.SelectedItem as Model.Product;
            //    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
            //    detail.OtherCompactCount = 0;
            //    detail.OtherCompactPrice = 0;
            //    detail.InDepotCount = 0;
            //    detail.OtherCompactMoney = 0;
            //    //detail.ProductUnit = detail.Product.MainUnit == null ? null : detail.Product.MainUnit.CnName;
            //    detail.ProductSpecification = (f.SelectedItem as Model.Product).ProductSpecification;
            //    this._produceOtherCompact.Details.Add(detail);
            //    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            //    this.gridControl1.RefreshDataSource();

            //}
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this._produceOtherCompact.Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceOtherCompactDetail);

                if (this._produceOtherCompact.Details.Count == 0)
                {
                    Model.ProduceOtherCompactDetail detail = new Model.ProduceOtherCompactDetail();
                    detail.OtherCompactDetailId = Guid.NewGuid().ToString();
                    detail.OtherCompactCount = 0;
                    detail.OtherCompactPrice = 0;
                    detail.InDepotCount = 0;
                    detail.OtherCompactMoney = 0;
                    detail.ProductSpecification = "";
                    detail.Inumber = this._produceOtherCompact.Details.Count + 1;
                    detail.Product = new Book.Model.Product();
                    this._produceOtherCompact.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }

                this.SettingForINumber();

                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButtonXO_Click(object sender, EventArgs e)
        {
            //    MPSheader.ChooseMPSdetailsForm f = new Book.UI.produceManager.MPSheader.ChooseMPSdetailsForm();
            //    if (f.ShowDialog(this) != DialogResult.OK) return;
            //    if (_MPSdetails.Count != 0)
            //    {
            //        this._produceOtherCompact.Details.Clear();
            //        if (_MPSdetails != null)
            //        {
            //            foreach (Model.MPSdetails mpsdetail in _MPSdetails)
            //            {
            //                Model.ProduceOtherCompactDetail produceOtherCompactDetail = new Book.Model.ProduceOtherCompactDetail();
            //                produceOtherCompactDetail.OtherCompactDetailId = Guid.NewGuid().ToString();
            //                if (mpsdetail.PrimaryKey != null)
            //                {
            //                   // produceOtherCompactDetail.PrimaryKey = mpsdetail.PrimaryKey;
            //                    produceOtherCompactDetail.PrimaryKeyId = mpsdetail.PrimaryKey.PrimaryKeyId;
            //                    produceOtherCompactDetail.Product = mpsdetail.PrimaryKey.Product;
            //                    produceOtherCompactDetail.ProductId = mpsdetail.PrimaryKey.Product.ProductId;
            //                    produceOtherCompactDetail.ProductSpecification = mpsdetail.PrimaryKey.Product.ProductSpecification;
            //                    if (mpsdetail.PrimaryKey.Product.MainUnit != null)
            //                    {
            //                        produceOtherCompactDetail.ProductUnit = mpsdetail.PrimaryKey.Product.MainUnit.CnName;
            //                    }
            //                }
            //                produceOtherCompactDetail.MPSDetailId = mpsdetail.MPSdetailsId;
            //                double Sum = new BL.MPSdetailsManager().GetByMPSdetailsId(mpsdetail.MPSdetailsId);
            //                produceOtherCompactDetail.OtherCompactCount = mpsdetail.MPSdetailssum - Sum;
            //                //produceOtherCompactDetail.DetailsSum = Convert.ToDouble(mpsdetail.MPSdetailssum);
            //                produceOtherCompactDetail.ProduceOtherCompact = this._produceOtherCompact;
            //                produceOtherCompactDetail.ProduceOtherCompactId = this._produceOtherCompact.ProduceOtherCompactId;
            //                produceOtherCompactDetail.MPSheaderId = mpsdetail.MPSheaderId;
            //                produceOtherCompactDetail.InvoiceXOId = mpsdetail.InvoiceXOId;
            //                produceOtherCompactDetail.InvoiceXODetailId = mpsdetail.InvoiceXODetailId;
            //                if (mpsdetail.InvoiceXODetailId != null)
            //                {
            //                    Model.InvoiceXODetail invoiceXODetail = new BL.InvoiceXODetailManager().Get(mpsdetail.InvoiceXODetailId);
            //                    if (invoiceXODetail != null)
            //                    {
            //                        produceOtherCompactDetail.OtherCompactPrice = invoiceXODetail.InvoiceXODetailPrice;
            //                        //produceOtherCompactDetail.OtherCompactCount = invoiceXODetail.InvoiceXODetailQuantity;
            //                        produceOtherCompactDetail.OtherCompactMoney = (invoiceXODetail.InvoiceXODetailPrice) * ((decimal)produceOtherCompactDetail.OtherCompactCount);
            //                    }
            //                }
            //                this._produceOtherCompact.Details.Add(produceOtherCompactDetail);

            //            }
            //        }
            //        this.gridControl1.RefreshDataSource();
            //        _MPSdetails.Clear();
            //    }

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherCompactDetail> details = this.bindingSourceDetails.DataSource as IList<Model.ProduceOtherCompactDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            if (detail == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnStock":
                    e.DisplayText = detail.StocksQuantity.ToString();
                    break;
                case "ColCusPro":
                    e.DisplayText = detail.CustomerProductName;
                    break;
                case "ColProductId":
                    e.DisplayText = detail.Id;
                    break;


            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.ColProductId)
            {
                Model.ProduceOtherCompactDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceOtherCompactDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.OtherCompactDetailId = Guid.NewGuid().ToString();
                    detail.OtherCompactCount = 0;
                    detail.OtherCompactPrice = 0;
                    detail.InDepotCount = 0;
                    detail.OtherCompactMoney = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.ProductSpecification = p.ProductSpecification;
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(_produceOtherCompact.ProduceOtherCompactId);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.ColOtherCompactPrice || e.Column == this.ColOtherCompactCount)
            {
                decimal price = decimal.Zero;
                decimal quantity = decimal.Zero;

                if (e.Column == this.ColOtherCompactPrice)
                {
                    decimal.TryParse(e.Value.ToString(), out price);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.ColOtherCompactCount).ToString(), out quantity);
                }
                if (e.Column == this.ColOtherCompactCount)
                {
                    decimal.TryParse(e.Value.ToString(), out quantity);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.ColOtherCompactPrice).ToString(), out price);
                }

                this.gridView1.SetRowCellValue(e.RowHandle, this.ColOtherCompactMoney, price * quantity);
            }
            this.gridControl1.RefreshDataSource();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            //string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            //this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            this.bindingSourceWorkHouse.DataSource = new BL.WorkHouseManager().Select();
            _MPSdetails.Clear();
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherCompactMaterial> details = this.bindingSourceMaterial.DataSource as IList<Model.ProduceOtherCompactMaterial>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumnStock1":
                    if (detail == null) return;
                    e.DisplayText = detail.StocksQuantity.HasValue ? detail.StocksQuantity.ToString() : "0";
                    break;

            }
        }

        //private void barButtonItemProcedures_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    #region
        //    PronoteHeader.SelectPronoteProceduresDetail f = new PronoteHeader.SelectPronoteProceduresDetail();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        if (f._pronoteProcedureDetails != null&&f._pronoteProcedureDetails.Count>0)
        //        {
        //            this._produceOtherCompact.Details.Clear();
        //            this._produceOtherCompact.DetailMaterial.Clear();
        //            this.comboBoxOperationType.SelectedIndex = (int)global::Helper.ProduceOther.OtherOperationType.ProceduresOther;
        //            this._produceOtherCompact.PronoteHeaderId = f._pronoteProcedureDetails[0].PronoteHeaderID;
        //            this._produceOtherCompact.MRSHeaderId = (new BL.PronoteHeaderManager()).Get(this._produceOtherCompact.PronoteHeaderId).MRSHeaderId;
        //            this.newChooseSupplierId.EditValue = f._pronoteProcedureDetails[0].Supplier;

        //            if (!string.IsNullOrEmpty(this._produceOtherCompact.MRSHeaderId))
        //            {
        //                Model.MRSHeader mRSHeader = this.mRSHeaderManager.Get(this._produceOtherCompact.MRSHeaderId);
        //                if (mRSHeader != null)
        //                {
        //                    Model.MPSheader mPSheader = this.mPSheaderManager.Get(mRSHeader.MPSheaderId);
        //                    if (mPSheader != null)
        //                    {
        //                        this.textEditCustomerXOId.Text = this.invoiceXOManager.Get(mPSheader.InvoiceXOId) == null ? "" : this.invoiceXOManager.Get(mPSheader.InvoiceXOId).CustomerInvoiceXOId;
        //                    }
        //                    else
        //                        this.textEditCustomerXOId.Text = string.Empty;
        //                }
        //                else
        //                    this.textEditCustomerXOId.Text = string.Empty;

        //            }
        //            else
        //                this.textEditCustomerXOId.Text = string.Empty;
        //            foreach(Model.PronoteProceduresDetail item in   f._pronoteProcedureDetails )
        //            {
        //                Model.ProduceOtherCompactDetail detail = new Model.ProduceOtherCompactDetail();
        //                  detail.OtherCompactDetailId=Guid.NewGuid().ToString();
        //                  detail.Product=item.PronoteHeader.Product;
        //                  if(detail.Product!=null )
        //                  detail.ProductId=detail.Product.ProductId;
        //                  detail.CustomInvoiceXOId = invoiceXOManager.Get(item.PronoteHeader.InvoiceXOId).CustomerInvoiceXOId;
        //                  detail.Procedures=item.Procedures;
        //                  detail.ProceduresId=item.ProceduresId;
        //                  detail.ProductUnit=item.PronoteHeader.ProductUnit;
        //                  detail.OtherCompactCount=item.PronoteHeader.DetailsSum;
        //                  this._produceOtherCompact.Details.Add(detail);


        //                  Model.ProduceOtherCompactMaterial material = new Model.ProduceOtherCompactMaterial();
        //                  material.ProduceOtherCompactMaterialId = Guid.NewGuid().ToString();
        //                  material.ProduceQuantity = item.PronoteHeader.DetailsSum;

        //                  //取前道工序 商品名称
        //                  Model.Product productUp=new Model.Product();
        //                  Model.BomParentPartInfo bom=new BL.BomParentPartInfoManager().Get(detail.Product);
        //                 if(bom !=null)
        //                 {
        //                     Model.TechonlogyHeader th=new BL.TechonlogyHeaderManager().Get(bom.TechonlogyHeaderId);
        //                      if(th!=null)
        //                      {
        //                         IList<Model.Technologydetails> tHdetail=new BL.TechnologydetailsManager().Select(th);

        //                         IList<string> proceduresList = new List<string>();
        //                          string a="";
        //                          foreach (Model.Technologydetails td in tHdetail)
        //                          {
        //                              if (td.ProceduresId == detail.ProceduresId)
        //                              {
        //                                  a = td.TechnologydetailsNo;
        //                              }
        //                          } 
        //                           foreach(Model.Technologydetails td in tHdetail )
        //                          {
        //                                if(Int32.Parse(td.TechnologydetailsNo)<Int32.Parse(a))
        //                                    proceduresList.Add(td.ProceduresId);
        //                          }

        //                          BL.ProductProcessManager productProcessManager=new BL.ProductProcessManager();
        //                          int  flag=0;


        //                          foreach( Model.Product pro  in  this.productManager.SelectProceProduct(detail.Product))
        //                          {
        //                                  flag = 0;
        //                                 IList<Model.ProductProcess> processList= productProcessManager.Select(pro.ProductId);                                       
        //                                 foreach(Model.ProductProcess process in processList)
        //                                 {
        //                                    if((!proceduresList.Contains(process.ProceduresId))||proceduresList.Count!=processList.Count)
        //                                     flag=1;
        //                                 }                                       
        //                                 if (flag == 0)
        //                                 {
        //                                     if (processList.Count == 0)
        //                                     {
        //                                         if (productUp.ProductId == null)
        //                                              productUp = pro;                                                 
        //                                     }
        //                                     else
        //                                         productUp = pro;                                                
        //                                 }
        //                          }
        //                      }
        //                 }
        //                 if (productUp.ProductId != null)
        //                 {
        //                     material.Product = productUp;
        //                     material.ProductId = productUp.ProductId;
        //                     material.ProductUnit = item.PronoteHeader.ProductUnit;
        //                     this._produceOtherCompact.DetailMaterial.Add(material);
        //                 }

        //            }                 
        //         }
        //        this.gridControl1.RefreshDataSource();
        //        this.gridControl2.RefreshDataSource();
        //        f.Dispose();
        //        GC.Collect();
        //    }
        //    #endregion
        //}

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._produceOtherCompact = f.SelectItem as Model.ProduceOtherCompact;
                this.action = "view";
                this.Refresh();
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.gridView1.FocusedColumn.Name == "gridColumnProductUnit")
                {

                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceOtherCompactDetail).Product;

                        this.repositoryItemComboBox3.Items.Clear();
                        if (p != null)
                        {
                            if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                            {
                                BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                                IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
                                foreach (Model.ProductUnit item in unitList)
                                {
                                    this.repositoryItemComboBox3.Items.Add(item.CnName);
                                }

                            }
                        }

                    }
                }
            }
        }

        private void gridView2_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.gridView2.FocusedColumn.Name == "gridColumnProductUnit1")
                {

                    if (this.gridView2.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as Model.ProduceOtherCompactMaterial).Product;

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
            if (this.bindingSourceMaterial.Current != null)
            {
                this._produceOtherCompact.DetailMaterial.Remove(this.bindingSourceMaterial.Current as Book.Model.ProduceOtherCompactMaterial);
                this.gridControl2.RefreshDataSource();
            }
        }

        private void textEditProduceOtherCompactDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditProduceOtherCompactDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        private void barBtnCondContinuousPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionOtherCompactChooseForm form = new Query.ConditionOtherCompactChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Query.ConditionOtherCompact condition = form.Condition as Query.ConditionOtherCompact;
                ROContinuous f = new ROContinuous(condition);
                f.ShowPreviewDialog();
            }
            form.Dispose();
            GC.Collect();
        }

        #region 审核
        protected override string AuditKeyId()
        {
            return Model.ProduceOtherCompact.PRO_ProduceOtherCompactId;
        }

        protected override int AuditState()
        {
            return this._produceOtherCompact.AuditState.HasValue ? this._produceOtherCompact.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProduceOtherCompact" + "," + this._produceOtherCompact.ProduceOtherCompactId;
        }
        #endregion

        //设置排列序号
        public void SettingForINumber()
        {
            IList<Model.ProduceOtherCompactDetail> mDetails = this.bindingSourceDetails.DataSource as IList<Model.ProduceOtherCompactDetail>;
            for (int i = 0; i < mDetails.Count; i++)
            {
                mDetails[i].Inumber = i + 1;
            }
        }
    }
}