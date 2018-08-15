using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
using Book.BL;
using Book.UI.Settings.ProduceManager.Techonlogy;
using DevExpress.XtraEditors.Repository;

namespace Book.UI.produceManager.PronoteHeader
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        public static Model.MRSdetails _mrsdetail = new Book.Model.MRSdetails();
        // public static IList<Model.MRSdetails> _MRSdetails = new List<Model.MRSdetails>();
        Model.PronoteHeader pronoteHeader = new Book.Model.PronoteHeader();
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        // BL.PronotedetailsManager pronotedetailsManager = new Book.BL.PronotedetailsManager();
        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        MPSdetailsManager MpsdetailsManager = new MPSdetailsManager();
        BL.PronoteMachineManager PronoteMachineManager = new PronoteMachineManager();
        BL.BomComponentInfoManager bomComponentinfoManager = new BomComponentInfoManager();
        // ManProcedureManager manProcedureManager = new ManProcedureManager();
        TechonlogyHeaderManager techonlogyHeaderManager = new TechonlogyHeaderManager();
        private IList<Model.PronoteMachine> pronoteMachines = new List<Model.PronoteMachine>();
        //Model.Pronotedetails pp = new Book.Model.Pronotedetails();

        Model.PronoteProceduresDetail detail = new Book.Model.PronoteProceduresDetail();
        BL.PronoteProceduresDetailManager _pronoteProceduresDetailManager = new PronoteProceduresDetailManager();
        BL.PronoteMachineManager pronoteMachineManager = new PronoteMachineManager();
        BL.WorkHouseManager _workhouseManager = new WorkHouseManager();
        Model.MRSdetails _mrsDetail;
        BL.MRSdetailsManager mRSdetailsManager = new MRSdetailsManager();
        BL.MRSHeaderManager mRSHeaderManager = new MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new MPSheaderManager();
        BL.InvoiceXOManager invoiceXOManager = new InvoiceXOManager();
        Model.ProduceMaterial produceMaterial = new Book.Model.ProduceMaterial();
        BL.ProduceMaterialManager produceMaterialManager = new Book.BL.ProduceMaterialManager();

        BL.SupplierManager supplierManager = new SupplierManager();
        //生产配料
        BL.PronotedetailsMaterialManager pronotedetailsMaterialManager = new PronotedetailsMaterialManager();
        BL.TechnologydetailsManager technologydetailsManager = new TechnologydetailsManager();
        BL.BomParentPartInfoManager bomParentPartInfoManager = new BomParentPartInfoManager();
        BL.BomPackageDetailsManager bomPackageDetailsManager = new BomPackageDetailsManager();
        BL.ProductProcessManager productProcessManager = new BL.ProductProcessManager();
        private int FlagIsProcee = 0;
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PronoteHeader.PRO_PronoteHeaderID, new AA(Properties.Resources.RequireDataForId, this.textEditPronoteHeaderID));
            //this.requireValueExceptions.Add(Model.PronoteHeader.PROPERTY_WORKHOUSEID, new AA(Properties.Resources.WorkHouse, this.newChooseWorkHorseId));
            this.invalidValueExceptions.Add(Model.PronoteHeader.PRO_PronoteHeaderID, new AA(Properties.Resources.EntityExists, this.textEditPronoteHeaderID));
            this.action = "view";
            this.newChooseEmployee0.Choose = new ChooseEmployee();
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseWorkHouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.EmpAudit.Choose = new ChooseEmployee();
            // this.bindingSourceProcedures.DataSource = this.pronoteHeader.DetailProcedures;           
        }


        int LastFlag = 0; //页面载入时是否执行 last方法
        /// <summary>
        /// 半成品加工
        /// </summary>
        /// <param name="pronoteHeader"></param>
        public EditForm(int FlagIsProcee)
            : this()
        {
            this.FlagIsProcee = FlagIsProcee;
        }
        public EditForm(Model.PronoteHeader pronoteHeader)
            : this()
        {

            this.pronoteHeader = pronoteHeader;
            //this.pronoteHeader.Details = this.pronotedetailsManager.Select(pronoteHeader);
            this.pronoteHeader.DetailsMaterial = this.pronotedetailsMaterialManager.GetByHeader(pronoteHeader);
            this.pronoteHeader.DetailProcedures = this._pronoteProceduresDetailManager.GetPronotedetailsMaterialByHeaderId(pronoteHeader);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }
        public EditForm(Model.PronoteHeader pronoteHeader, int flagIsProcee)
            : this()
        {
            FlagIsProcee = flagIsProcee;
            this.pronoteHeader = pronoteHeader;
            //this.pronoteHeader.Details = this.pronotedetailsManager.Select(pronoteHeader);
            this.pronoteHeader.DetailsMaterial = this.pronotedetailsMaterialManager.GetByHeader(pronoteHeader);
            this.pronoteHeader.DetailProcedures = this._pronoteProceduresDetailManager.GetPronotedetailsMaterialByHeaderId(pronoteHeader);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.MRSdetails mRSdetails)
            : this()
        {
            this._mrsDetail = mRSdetails;
            this.AddNew();
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
            this.Refresh();
            this.MrsLoad(mRSdetails);
            List<int> a = new List<int>();
        }

        public EditForm(Model.PronoteHeader pronoteHeader, string action)
            : this()
        {
            this.pronoteHeader = pronoteHeader;
            this.pronoteHeader.DetailsMaterial = this.pronotedetailsMaterialManager.GetByHeader(pronoteHeader);

            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void Save()
        {
            this.pronoteHeader.PronoteHeaderID = this.textEditPronoteHeaderID.Text;
            this.pronoteHeader.Pronotedesc = this.textEditPronotedesc.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditPronoteDte.DateTime, new DateTime()))
            {
                this.pronoteHeader.PronoteDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.pronoteHeader.PronoteDate = this.dateEditPronoteDte.DateTime;
            }


            //修改一半
            this.pronoteHeader.MRSHeaderId = this.textEditMRP.Text;
            this.pronoteHeader.Product = this.buttonEditProId.EditValue as Model.Product;
            if (this.pronoteHeader.Product != null)
                this.pronoteHeader.ProductId = this.pronoteHeader.Product.ProductId;
            this.pronoteHeader.DetailsSum = double.Parse(this.calcEditQuantity.Value.ToString());
            this.pronoteHeader.InvoiceXODetailQuantity = double.Parse(this.calcInvoiceXODetailQuantity.EditValue.ToString());
            this.pronoteHeader.ProductUnit = this.textEditUnit.Text;
            this.pronoteHeader.Employee0 = this.newChooseEmployee0.EditValue as Model.Employee;// this.newChooseEmployee0.EditValue as Model.Employee;

            // this.pronoteHeader.Employee1 = this.newChooseEmployee0.EditValue as Model.Employee;

            this.pronoteHeader.InvoiceXOId = this.textEditXOId.Text;
            //手册
            this.pronoteHeader.HandbookId = this.txt_HandBookId.Text;
            this.pronoteHeader.HandbookProductId = this.txt_HandBookProductId.Text;

            this.pronoteHeader.WorkHouse = this.newChooseWorkHouse.EditValue as Model.WorkHouse;
            if (this.newChooseWorkHouse.EditValue != null)
            {
                this.pronoteHeader.WorkHouseId = this.pronoteHeader.WorkHouse.WorkHouseId;
            }
            this.pronoteHeader.AuditState = this.saveAuditState;

            //if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            //    return;

            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;

            if (!this.gridView3.PostEditor() || !this.gridView3.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.pronoteHeaderManager.Insert(this.pronoteHeader);
                    break;

                case "update":
                    this.pronoteHeaderManager.Update(this.pronoteHeader);
                    break;
            }

        }

        protected override void Delete()
        {
            if (this.pronoteHeader == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            // try
            //{
            this.pronoteHeaderManager.Delete(this.pronoteHeader.PronoteHeaderID);
            this.pronoteHeader = this.pronoteHeaderManager.GetNext(this.pronoteHeader);
            if (this.pronoteHeader == null)
            {
                this.pronoteHeader = this.pronoteHeaderManager.GetLast();
            }
            // }
            // catch
            //{
            //    throw new Exception("");
            //}

        }

        public override void Refresh()
        {

            if (this.pronoteHeader == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this.pronoteHeader = this.pronoteHeaderManager.GetDetails(pronoteHeader.PronoteHeaderID);
                }

            }
            //  this.bindingSourceProductId.DataSource = productManager.Select();

            this.updateCaption();
            if (this.action == "view")
                barButtonItem3.Enabled = false;
            else
                barButtonItem3.Enabled = true;
            this.textEditPronoteHeaderID.Text = this.pronoteHeader.PronoteHeaderID;
            this.textEditPronotedesc.Text = this.pronoteHeader.Pronotedesc;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.pronoteHeader.PronoteDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditPronoteDte.EditValue = null;
            }
            else
            {
                this.dateEditPronoteDte.EditValue = this.pronoteHeader.PronoteDate;
            }



            this.dateEdit1.EditValue = this.pronoteHeader.JieAnDate;
            // this.bindingSourceDetails.DataSource = this.pronoteHeader.Details;
            this.pronoteHeader.DetailProcedures = this._pronoteProceduresDetailManager.GetPronotedetailsMaterialByHeaderId(pronoteHeader);
            if (this.pronoteHeader.DetailProcedures.Count == 0)
            {
                Model.PronoteProceduresDetail detail = new Book.Model.PronoteProceduresDetail();
                detail.PronoteProceduresDetailId = Guid.NewGuid().ToString();
                this.pronoteHeader.DetailProcedures.Add(detail);
                this.bindingSourceProcedures.Position = this.bindingSourceProcedures.IndexOf(detail);
            }
            //foreach (Model.PronoteProceduresDetail item in this.pronoteHeader.DetailProcedures)
            //{
            //    if (item.PronoteProceduresDetailId == null)
            //        this.pronoteHeader.DetailProcedures.Remove(item);
            //}
            if (this.pronoteHeader.Product != null)
                this.richTextBox1.Rtf = this.pronoteHeader.Product.ProductDescription;
            else
                this.richTextBox1.Text = string.Empty;
            this.newChooseWorkHouse.EditValue = this.pronoteHeader.WorkHouse;
            //foreach (Model.PronoteProceduresDetail item in this.pronoteHeader.DetailProcedures)
            //{
            //    if (item.PronoteProceduresDetailId == null) continue;

            //    string text = string.Empty;
            //    this.pronoteMachines.Clear();
            //    this.pronoteMachines = this.pronoteMachineManager.GetPronoteMachineByPronoteProceduresDetailId(item.PronoteProceduresDetailId);
            //    if (this.pronoteMachines.Count != 0)
            //    {
            //        foreach (Model.PronoteMachine pro in this.pronoteMachines)
            //        {
            //            text += pro.PronoteMachineName + ",";
            //        }
            //        if (text.Length > 0)
            //            text = text.Substring(0, text.Length - 1);
            //    }
            //    item.PronoteMachineId = text;
            //}

            if (this.pronoteHeader.Product != null)
            {
                this.textEditCustomProName.Text = string.IsNullOrEmpty(this.pronoteHeader.Product.CustomerProductName) ? string.Empty : this.pronoteHeader.Product.CustomerProductName;
                this.labelProName.Text = this.pronoteHeader.Product.Id;
            }
            else
            {
                this.textEditCustomProName.EditValue = null;
                this.labelProName.EditValue = null;
            }


            this.textEditUnit.Text = this.pronoteHeader.ProductUnit;
            this.textEditMRP.Text = this.pronoteHeader.MRSHeaderId;
            this.calcEditQuantity.EditValue = this.pronoteHeader.DetailsSum == null ? 0 : this.pronoteHeader.DetailsSum.Value;
            this.textEditXOId.Text = this.pronoteHeader.InvoiceXOId;
            this.calcInvoiceXODetailQuantity.EditValue = this.pronoteHeader.InvoiceXODetailQuantity == null ? 0 : this.pronoteHeader.InvoiceXODetailQuantity.Value;
            this.buttonEditProId.EditValue = this.pronoteHeader.Product;

            if (!string.IsNullOrEmpty(this.pronoteHeader.InvoiceXOId))
            {

                Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(this.pronoteHeader.InvoiceXOId);
                this.textEditCustomXOId.Text = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
                this.newChooseCustomer.EditValue = invoiceXO == null ? null : invoiceXO.xocustomer;
                this.textEditPiHao.EditValue = invoiceXO == null ? null : invoiceXO.CustomerLotNumber;

            }
            else
            {
                this.textEditCustomXOId.EditValue = null;
                this.newChooseCustomer.EditValue = null;
                this.textEditPiHao.EditValue = null;

            }
            Model.MRSdetails mrsdetail = this.mRSdetailsManager.Get(this.pronoteHeader.MRSdetailsId);
            if (mrsdetail != null)
                this.textEditBeforepPackage.Text = mrsdetail.BeforePackageProduct == null ? string.Empty : (mrsdetail.BeforePackageProduct.IsCustomerProduct.HasValue && mrsdetail.BeforePackageProduct.IsCustomerProduct.Value ? mrsdetail.BeforePackageProduct.ProductName + "{" + mrsdetail.BeforePackageProduct.CustomerProductName + "}" : mrsdetail.BeforePackageProduct.ProductName);
            else
                this.textEditBeforepPackage.Text = string.Empty;

            this.newChooseEmployee0.EditValue = this.pronoteHeader.Employee0;
            //foreach (Model.PronoteProceduresDetail item in this.pronoteHeader.DetailProcedures)
            //{
            //    if (item.PronoteMachineId != "" && item.PronoteMachineId != null)
            //        item.PronoteMachineId = item.PronoteMachine.PronoteMachineName;
            //}

            this.EmpAudit.EditValue = this.pronoteHeader.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this.pronoteHeader.AuditState);

            //手册
            this.txt_HandBookId.EditValue = this.pronoteHeader.HandbookId;
            this.txt_HandBookProductId.EditValue = this.pronoteHeader.HandbookProductId;

            this.bindingSourceMaterial.DataSource = this.pronoteHeader.DetailsMaterial;
            this.bindingSourceProcedures.DataSource = this.pronoteHeader.DetailProcedures;

            base.Refresh();
            this.newChooseEmployee0.Enabled = false;
            switch (this.action)
            {
                case "insert":
                    this.barButtonItem2.Enabled = false;
                    this.barButtonItem1.Enabled = true;
                    this.barButtonItem3.Enabled = true;
                    //this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    this.gridView3.OptionsBehavior.Editable = true;
                    this.barButtonItemMaterial.Enabled = true;
                    break;
                case "update":
                    this.barButtonItem2.Enabled = false;
                    this.barButtonItem1.Enabled = true;
                    this.barButtonItem3.Enabled = true;
                    //this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    this.gridView3.OptionsBehavior.Editable = true;
                    this.barButtonItemMaterial.Enabled = true;
                    break;
                case "view":
                    this.barButtonItem2.Enabled = true;
                    this.barButtonItem1.Enabled = false;
                    this.barButtonItem3.Enabled = false;
                    //this.gridView1.OptionsBehavior.Editable = false;
                    //this.gridView2.OptionsBehavior.Editable = false;
                    this.gridView3.OptionsBehavior.Editable = false;
                    this.barButtonItemMaterial.Enabled = true;
                    break;
                default:
                    break;
            }


            this.textEditPronoteHeaderID.Properties.ReadOnly = true;
            this.btn_MakeProduceMateria.Enabled = true;
            //if (this.pronoteHeader.IsBuildMaterial == null || !this.pronoteHeader.IsBuildMaterial.Value)
            //    this.barButtonItemMaterial.Caption = Properties.Resources.IsBuildMaterial;
            //else
            //    this.barButtonItemMaterial.Caption = Properties.Resources.LookMaterialInfo;

        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            if (FlagIsProcee == 4)
            {
                return new RO(pronoteHeader.PronoteHeaderID, FlagIsProcee);
            }
            else
            {
                return new ROZZJiaGong(pronoteHeader.PronoteHeaderID, FlagIsProcee);
            }
        }

        protected override void MoveNext()
        {
            Model.PronoteHeader pronoteHeader = this.pronoteHeaderManager.GetNext(this.pronoteHeader);
            if (pronoteHeader == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.pronoteHeader = this.pronoteHeaderManager.Get(pronoteHeader.PronoteHeaderID);
        }

        protected override void MovePrev()
        {
            Model.PronoteHeader pronoteHeader = this.pronoteHeaderManager.GetPrev(this.pronoteHeader);
            if (pronoteHeader == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.pronoteHeader = this.pronoteHeaderManager.Get(pronoteHeader.PronoteHeaderID);
        }

        protected override void MoveFirst()
        {
            this.pronoteHeader = this.pronoteHeaderManager.Get(this.pronoteHeaderManager.GetFirst() == null ? "" : this.pronoteHeaderManager.GetFirst().PronoteHeaderID);
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            //  if (pronoteHeader == null)
            {
                this.pronoteHeader = this.pronoteHeaderManager.Get(this.pronoteHeaderManager.GetLast() == null ? "" : this.pronoteHeaderManager.GetLast().PronoteHeaderID);
            }
        }

        protected override bool HasRows()
        {
            return this.pronoteHeaderManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.pronoteHeaderManager.HasRowsAfter(this.pronoteHeader);
        }

        protected override bool HasRowsPrev()
        {
            return this.pronoteHeaderManager.HasRowsBefore(this.pronoteHeader);
        }

        protected override void AddNew()
        {
            this.pronoteHeader = new Model.PronoteHeader();
            this.pronoteHeader.PronoteHeaderID = this.pronoteHeaderManager.GetId();// Guid.NewGuid().ToString();
            this.pronoteHeader.PronoteDate = DateTime.Now;
            this.pronoteHeader.Employee0 = BL.V.ActiveOperator.Employee;
            // this.pronoteHeader.Details = new List<Model.Pronotedetails>();
            //if (this.action == "insert")
            //{
            //    Model.Pronotedetails detail = new Model.Pronotedetails();
            //    detail.PronotedetailsID = Guid.NewGuid().ToString();
            //    detail.DetailsSum = 0;
            //    detail.ProductUnit = "";
            //    detail.InDepotQuantity = 0;
            //    detail.ProductStock = 0;
            //    detail.ProductSpecification = "";
            //    detail.Product = new Book.Model.Product();
            //    this.pronoteHeader.Details.Add(detail);
            //    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            //}
        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    ChooseProductForm f = new ChooseProductForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        Model.Product product = f.SelectedItem as Model.Product;
        //        //Model.Pronotedetails detail = new Book.Model.Pronotedetails();
        //        //detail.PronotedetailsID = Guid.NewGuid().ToString();
        //        //detail.Product = f.SelectedItem as Model.Product;
        //        //detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
        //        //detail.DetailsSum = 0;
        //        //detail.InDepotQuantity = 0;
        //        //detail.ProductStock = (f.SelectedItem as Model.Product).StocksQuantity;
        //        //detail.ProductUnit = detail.Product.MainUnit == null ? null : detail.Product.MainUnit.CnName;
        //        //detail.ProductSpecification = (f.SelectedItem as Model.Product).ProductSpecification;
        //        //this.pronoteHeader.Details.Add(detail);
        //        //this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
        //       // this.gridControl1.RefreshDataSource();
        //        this.bindingSourceProductId.DataSource = productManager.Select();
        //    }
        //}

        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    if (this.bindingSourceDetails.Current != null)
        //    {
        //        this.pronoteHeader.Details.Remove(this.bindingSourceDetails.Current as Book.Model.Pronotedetails);

        //        if (this.pronoteHeader.Details.Count == 0)
        //        {
        //            Model.Pronotedetails detail = new Model.Pronotedetails();
        //            detail.PronotedetailsID = Guid.NewGuid().ToString();
        //            detail.DetailsSum = 0;
        //            detail.ProductUnit = "";
        //            detail.InDepotQuantity = 0;
        //            detail.ProductStock = 0;
        //            detail.ProductSpecification = "";
        //            detail.Product = new Book.Model.Product();
        //            this.pronoteHeader.Details.Add(detail);
        //            this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
        //        }

        //       // this.gridControl1.RefreshDataSource();
        //    }
        //}

        private void simpleButtonXO_Click(object sender, EventArgs e)
        {

        }

        //private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if (e.Column == this.ColProductId)
        //    {
        //        Model.Pronotedetails detail = this.gridView1.GetRow(e.RowHandle) as Model.Pronotedetails;
        //        if (detail != null)
        //        {
        //            Model.Product p = productManager.Get(e.Value.ToString());
        //            detail.PronotedetailsID = Guid.NewGuid().ToString();
        //            detail.DetailsSum = 0;
        //            detail.InDepotQuantity = 0;
        //            detail.Product = p;
        //            detail.ProductStock = p.StocksQuantity;
        //            detail.ProductId = p.ProductId;
        //            detail.ProductSpecification = p.ProductSpecification;
        //            detail.ProductUnit = p.MainUnit == null ? "" : p.MainUnit.CnName;
        //            this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
        //        }
        //        this.gridControl1.RefreshDataSource();
        //    }
        //}

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.Pronotedetails> details = this.bindingSourceDetails.DataSource as IList<Model.Pronotedetails>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            //IList<Model.MPSheader> mpsHeader = new BL.MPSheaderManager().SelectById(details[e.ListSourceRowIndex].MPSheaderId);

            switch (e.Column.Name)
            {
                case "ColProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                //case "gridColumnStock":
                //    e.DisplayText = detail.StocksQuantity == null ? "0" : detail.StocksQuantity.ToString();
                //    break;
                case "grid1MPSQuantity":
                    e.DisplayText = this.MpsdetailsManager.Get(details[e.ListSourceRowIndex].MPSDetailId) == null ? "0" : this.MpsdetailsManager.Get(details[e.ListSourceRowIndex].MPSDetailId).MPSdetailssum.ToString();
                    break;


            }
        }

        //private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    if (this.action == "insert" || this.action == "update")
        //    {
        //        if (this.gridView1.FocusedColumn.Name == "gridColumn4")
        //        {

        //            if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
        //            {
        //                Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.Pronotedetails).Product;

        //                this.repositoryItemComboBox1.Items.Clear();
        //                if (p != null)
        //                {
        //                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
        //                    {
        //                        BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
        //                        IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroup);
        //                        foreach (Model.ProductUnit item in unitList)
        //                        {
        //                            this.repositoryItemComboBox1.Items.Add(item.CnName);
        //                        }

        //                    }

        //                }
        //            }
        //        }
        //    }
        //}

        private void EditForm_Load(object sender, EventArgs e)
        {

            //string sql = "SELECT productid,id,productname FROM product";
            //this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            this.bindingSourcePronoteMachine.DataSource = this.PronoteMachineManager.Select();
            this.bindingSourceWorkHouse.DataSource = this._workhouseManager.Select();
            this.bindingSourceSupplier.DataSource = this.supplierManager.Select();
            // this.bindingSourceProductId.DataSource = productManager.GetProduct();
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.PronotedetailsMaterial> details = this.bindingSourceMaterial.DataSource as IList<Model.PronotedetailsMaterial>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            Model.PronotedetailsMaterial material = details[e.ListSourceRowIndex];

            //IList<Model.MPSheader> mpsHeader = new BL.MPSheaderManager().SelectById(details[e.ListSourceRowIndex].MPSheaderId);

            switch (e.Column.Name)
            {
                case "gridColumnProductId":
                    if (detail == null) return;
                    e.DisplayText = detail.Id;
                    break;
                case "gridColumnGuiGe":
                    if (detail == null) return;
                    e.DisplayText = detail.ProductSpecification;
                    break;
                //case "gridColumnStocks":
                //    if (detail == null) return;
                //    e.DisplayText = detail.StocksQuantity == null ? "0" : detail.StocksQuantity.ToString();
                //    break;
                case "gridColumnCusPro":
                    e.DisplayText = detail.CustomerProductName;
                    break;
                case "gridColumnMPSquantity":
                    e.DisplayText = this.mRSdetailsManager.Get(material.MRSdetailsId) == null ? "0" : this.mRSdetailsManager.Get(material.MRSdetailsId).MRSdetailssum.ToString();
                    break;

                case "gridColumnQuantity":
                    e.DisplayText = material.PronoteQuantity1.HasValue ? material.PronoteQuantity1.Value.ToString("0.####") : "0";
                    break;
            }

        }

        //private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if (e.Column == this.gridColumn3)
        //    {
        //        if (e.Value.ToString() == "0") return;
        //        // this.pronoteHeader.Details;

        //        Model.Pronotedetails pronotedetails = this.gridView1.GetRow(e.RowHandle) as Model.Pronotedetails;
        //        foreach (Model.PronotedetailsMaterial materials in this.pronoteHeader.DetailsMaterial)
        //        {
        //            if (materials.PronotedetailsID == pronotedetails.PronotedetailsID)
        //            {
        //                materials.PronoteQuantity = materials.PronoteQuantity / pronotedetails.QuantityTemp * double.Parse(e.Value.ToString());
        //            }

        //        }
        //        pronotedetails.QuantityTemp = double.Parse(e.Value.ToString());
        //        this.gridControl1.RefreshDataSource();
        //        this.gridControl2.RefreshDataSource();

        //    }
        //}

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //this.proceManager.DelelteByProduresMachines(detail.ProceduresId);
            //ChooseMachineForm chooseMachine = new ChooseMachineForm();
            //if (chooseMachine.ShowDialog(this) == DialogResult.OK)
            //{
            //    pronoteMachines.Clear();
            //    pronoteMachines = chooseMachine.SelectItem;
            //    string text = string.Empty;
            //    foreach (Model.PronoteMachine machine in pronoteMachines)
            //    {
            //        text += machine.PronoteMachineName + ",";
            //        Model.ProceduresMachine promachine = new Book.Model.ProceduresMachine();
            //        promachine.ProceduresMachineId = Guid.NewGuid().ToString();
            //        promachine.ProceduresId = detail.ProceduresId;
            //        promachine.PronoteMachineId = machine.PronoteMachineId;
            //        this.proceManager.Insert(promachine);
            //    }
            //    if (text != "")
            //        this.gridView3.SetFocusedValue(text.Substring(0, text.Length - 1));
            //}
        }

        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemHyperLinkEdit3_Click(object sender, EventArgs e)
        {

            // pp = this.bindingSourceDetails.Current as Model.Pronotedetails;
        }

        private void repositoryItemHyperLinkEdit4_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemHyperLinkEdit5_Click(object sender, EventArgs e)
        {

            Model.PronotedetailsMaterial a = this.bindingSourceMaterial.Current as Model.PronotedetailsMaterial;

        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            this.detail = this.bindingSourceProcedures.Current as Model.PronoteProceduresDetail;
            this.pronoteMachines = this.pronoteMachineManager.GetPronoteMachineByPronoteProceduresDetailId(detail.PronoteProceduresDetailId);
            ChooseMachineForm chooseMachine = new ChooseMachineForm(pronoteMachines, detail);
            if (chooseMachine.ShowDialog(this) == DialogResult.OK)
            {
                pronoteMachines.Clear();
                pronoteMachines = chooseMachine.SelectItem;
                string text = string.Empty;
                foreach (Model.PronoteMachine machine in pronoteMachines)
                {
                    text += machine.PronoteMachineName + ",";
                    Model.ProceduresMachine promachine = new Book.Model.ProceduresMachine();
                    promachine.ProceduresMachineId = Guid.NewGuid().ToString();
                    promachine.PronoteProceduresDetailId = this.detail.PronoteProceduresDetailId;
                    promachine.ProceduresId = detail.ProceduresId;
                    promachine.PronoteMachineId = machine.PronoteMachineId;
                    this.pronoteHeader.ProceduresMachineDetail.Add(promachine);
                    //this.proceManager.Insert(promachine);
                }
                if (text != "")
                {
                    this.gridView3.SetFocusedValue(text.Substring(0, text.Length - 1));
                }
                else
                    this.gridView3.SetFocusedValue(text);
            }
        }

        //选择需求单后
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MPSheader.ChooseMPSdetailsForm f = new Book.UI.produceManager.MPSheader.ChooseMPSdetailsForm(this.FlagIsProcee);
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (_mrsdetail.MRSdetailsId != null)
            {
                _mrsdetail = this.mRSdetailsManager.Get(_mrsdetail.MRSdetailsId);
                this.pronoteHeader.Product = _mrsdetail.Product;
                this.pronoteHeader.ProductId = _mrsdetail.ProductId;
                this.pronoteHeader.MRSdetailsId = _mrsdetail.MRSdetailsId;
                this.newChooseWorkHouse.EditValue = _mrsdetail.WorkHouseNext;
                this.textEditBeforepPackage.Text = _mrsdetail.BeforePackageProduct == null ? string.Empty : (_mrsdetail.BeforePackageProduct.IsCustomerProduct.HasValue && _mrsdetail.BeforePackageProduct.IsCustomerProduct.Value ? _mrsdetail.BeforePackageProduct.ProductName + "{" + _mrsdetail.BeforePackageProduct.CustomerProductName + "}" : _mrsdetail.BeforePackageProduct.ProductName);

                MrsLoad(_mrsdetail);
                ////配料用料
                //pronoteHeader.DetailsMaterial.Clear();

                ////工序
                //pronoteHeader.DetailProcedures.Clear();
                //this.pronoteHeader.MRSHeaderId = _mrsdetail.MRSHeaderId;
                //this.pronoteHeader.Product = _mrsdetail.Product;
                //this.pronoteHeader.ProductId = _mrsdetail.ProductId;
                //this.pronoteHeader.DetailsSum = _mrsdetail.MRSdetailssum - (_mrsdetail.MRSHasSingleSum == null ? 0 : _mrsdetail.MRSHasSingleSum);
                //this.pronoteHeader.ProductUnit = _mrsdetail.ProductUnit;
                //this.pronoteHeader.Employee0 = BL.V.ActiveOperator.Employee;
                //this.pronoteHeader.Employee1 = BL.V.ActiveOperator.Employee;

                //this.textEditCustomProName.Text =string.IsNullOrEmpty(_mrsdetail.Product.CustomerProductName)?string.Empty: _mrsdetail.Product.CustomerProductName;
                //this.textEditUnit.Text = _mrsdetail.ProductUnit;
                //this.buttonEditProId.Text = _mrsdetail.Product.Id;
                //this.labelProName.Text = _mrsdetail.Product.ProductName;
                //this.textEditMRP.Text = _mrsdetail.MRSHeaderId;



                ////Model.Pronotedetails pronotedetails = new Book.Model.Pronotedetails();
                ////pronotedetails.PronotedetailsID = Guid.NewGuid().ToString();       
                ////pronotedetails.DetailsSum = _mrsdetail.MRSdetailssum - (_mrsdetail.MRSHasSingleSum == null ? 0 : _mrsdetail.MRSHasSingleSum);
                ////pronotedetails.QuantityTemp = pronotedetails.DetailsSum;
                ////pronotedetails.Product = _mrsdetail.Product;
                ////pronotedetails.ProductId = _mrsdetail.ProductId;
                ////pronotedetails.MPSQuantity = _mrsdetail.MRSdetailssum;
                ////pronotedetails.ProductStock = _mrsdetail.Product.StocksQuantity;
                ////pronotedetails.PronoteHeader = this.pronoteHeader;
                ////pronotedetails.MRSHeaderId = _mrsdetail.MRSHeaderId;
                ////pronotedetails.MRSdetailsId = _mrsdetail.MRSdetailsId;
                ////pronotedetails.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
                ////pronotedetails.MPSheaderId = _mrsdetail.MRSHeader.MPSheaderId;
                ////this.pronoteHeader.Details.Add(pronotedetails);                

                //Model.BomParentPartInfo bomP = new BL.BomParentPartInfoManager().Get(this.pronoteHeader.Product);

                ////配料
                //foreach (Model.BomComponentInfo component in bomComponentinfoManager.Select(bomP))
                //{
                //    Model.PronotedetailsMaterial materials = new Model.PronotedetailsMaterial();
                //    materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
                //    materials.Product = component.Product;
                //    materials.ProductId = component.ProductId;
                //    materials.PronoteHeader = this.pronoteHeader;
                //    materials.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
                //    materials.PronoteQuantity = component.UseQuantity * this.pronoteHeader.DetailsSum;
                //    materials.MPSQuantity = _mrsdetail.MRSdetailssum * component.UseQuantity;
                //    //materials.Pronotedetails = pronotedetails;
                //    //materials.PronotedetailsID = pronotedetails.PronotedetailsID;
                //    materials.MPSheaderId = _mrsdetail.MRSHeader.MPSheaderId;
                //    materials.MRSHeaderId = _mrsdetail.MRSHeaderId;
                //    materials.MRSdetailsId = _mrsdetail.MRSdetailsId;
                //    pronoteHeader.DetailsMaterial.Add(materials);
                //}

                ////工序
                //if (manProcedureManager.Select(bomP, _mrsdetail.Customer) != null)
                //{
                //    Model.TechonlogyHeader techonlogyHeader = techonlogyHeaderManager.Get(manProcedureManager.Select(bomP, _mrsdetail.Customer).TechonlogyHeaderId);
                //    Model.PronoteProceduresDetail pronoteProceduresDetail = null;
                //    foreach (Model.Technologydetails technologydetails in (new TechnologydetailsManager().Select(techonlogyHeader)))
                //    {
                //        pronoteProceduresDetail = new Book.Model.PronoteProceduresDetail();
                //        pronoteProceduresDetail.PronoteProceduresDetailId = Guid.NewGuid().ToString();
                //        pronoteProceduresDetail.Procedures = technologydetails.Procedures;
                //        //  pronoteProceduresDetail.PronoteMachine = technologydetails.Procedures.PronoteMachine;
                //        pronoteProceduresDetail.ProceduresNo = technologydetails.TechnologydetailsNo;
                //        if (technologydetails.Procedures != null)
                //            pronoteProceduresDetail.ProceduresId = technologydetails.Procedures.ProceduresId;

                //        pronoteProceduresDetail.WorkHouseId = technologydetails.Procedures.WourkHoseId;

                //        this.pronoteHeader.DetailProcedures.Add(pronoteProceduresDetail);

                //        this.bindingSourceProcedures.Position = bindingSourceProcedures.IndexOf(pronoteProceduresDetail);
                //    }

                //}

                //this.gridControl2.RefreshDataSource();
                //this.gridControl3.RefreshDataSource();        
                _mrsdetail = new Book.Model.MRSdetails();

            }
            f.Dispose();
            GC.Collect();
        }

        private void gridView3_ShowingEditor(object sender, CancelEventArgs e)
        {

            IList<Model.PronoteMachine> Machines = new List<Model.PronoteMachine>();
            if (this.gridView3.FocusedColumn.Name == "gridColumn20")
            {
                if (this.gridView3.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.PronoteProceduresDetail p = this.gridView3.GetRow(this.gridView3.FocusedRowHandle) as Model.PronoteProceduresDetail;

                    this.repositoryItemComboBox2.Items.Clear();

                    if (!string.IsNullOrEmpty(p.ProceduresId))
                    {
                        BL.PronoteMachineManager pmanger = new Book.BL.PronoteMachineManager();


                        Machines = this.pronoteMachineManager.SelectMachineByProduresId(p.ProceduresId);
                        if (Machines.Count != 0)
                        {
                            foreach (Model.PronoteMachine item in Machines)
                            {
                                this.repositoryItemComboBox2.Items.Add(item.PronoteMachineName);
                            }
                        }

                    }
                }
            }
        }

        // 形成领料单
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //object tag = e.Item.Tag;
            //if (tag == null) return;
            //if (tag.ToString() == "BuiltMaterial" && this.pronoteHeader.DetailsMaterial != null)
            //    Operations.Open("produceManager.ProduceMaterial", this.MdiParent, this.pronoteHeader);

            //if (this.barButtonItemMaterial.Caption == Properties.Resources.IsBuildMaterial)
            //{


            //    this.produceMaterial = new Model.ProduceMaterial();          //  this.produceMaterial.ProduceMaterialID = this.produceMaterialManager.GetId();
            //    this.produceMaterial.Details = new List<Model.ProduceMaterialdetails>();
            //    Model.ProduceMaterialdetails detail;



            //    foreach (var item in this.pronoteHeader.DetailsMaterial)
            //    {   //在制品商品
            //        if (item.Product.IsProcee != null && item.Product.IsProcee == true)
            //        {
            //            //注释在制品和非在制品 同一库房 形成不能领料单
            //            ////Model.ProduceMaterial produceMaterial = new Model.ProduceMaterial();
            //            ////produceMaterial.Details = new List<Model.ProduceMaterialdetails>();
            //            ////produceMaterial.ProduceMaterialID = this.produceMaterialManager.GetId();
            //            ////produceMaterial.ProduceMaterialDate = System.DateTime.Now;
            //            ////produceMaterial.DepotId = product.DepotId;
            //            //////produceMaterial.Employee0 = BL.V.ActiveOperator.Employee;
            //            //////produceMaterial.Employee0Id = BL.V.ActiveOperator.Employee.EmployeeId;
            //            ////produceMaterial.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;

            //            ////    Model.ProduceMaterialdetails ds = new Model.ProduceMaterialdetails();
            //            ////    ds.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
            //            ////    ds.Materialprocessum = item.PronoteQuantity;
            //            ////    ds.Materialprocesedsum = 0;
            //            ////    ds.Product = item.Product;
            //            ////    ds.ProductId = item.ProductId;
            //            ////   // ds.CustomerInvoiceXOId = g.CustomerInvoiceXOId;
            //            ////    ds.ProductUnit = item.ProductUnit;
            //            ////    produceMaterial.Details.Add(ds);

            //            ////this.produceMaterialManager.Insert(produceMaterial);
            //            //修改工序详细 领料单


            //            // IList<Model.Product> productList = this.productManager.SelectProceProduct(bomP.Product);
            //            for (int j = 0; j < this.pronoteHeader.DetailProcedures.Count; j++)
            //            {
            //                Model.PronoteProceduresDetail proDetail = this.pronoteHeader.DetailProcedures[j];
            //                if (item.Product.IsProcee != null && item.Product.IsProcee == true)
            //                {

            //                    IList<Model.ProductProcess> processList = productProcessManager.Select(item.ProductId);
            //                    if (processList.Count == 0)
            //                        continue;
            //                    if (proDetail.ProceduresId == processList.Last().ProceduresId)
            //                    {
            //                        if (j + 1 < this.pronoteHeader.DetailProcedures.Count)
            //                            this.pronoteHeader.DetailProcedures[j + 1].ProduceMaterialID = produceMaterial.ProduceMaterialID;
            //                        new BL.PronoteProceduresDetailManager().Update(this.pronoteHeader.DetailProcedures[j + 1]);
            //                        break;
            //                    }

            //                }
            //            }
            //            this.gridControl3.RefreshDataSource();


            //        }
            //        //注释在制品和非在制品 同一库房 形成不能领料单
            //        //// else
            //        //// {
            //        detail = new Model.ProduceMaterialdetails();
            //        detail.Materialprocessum = item.PronoteQuantity == null ? 0 : item.PronoteQuantity.Value;
            //        detail.Materialprocesedsum = item.AlreadyDrawQuantity == null ? 0 : item.AlreadyDrawQuantity.Value;
            //        detail.ProductStock = item.Product.StocksQuantity == null ? 0 : item.Product.StocksQuantity.Value;
            //        detail.ProductSpecification = item.Product.ProductSpecification;
            //        detail.Product = item.Product;
            //        detail.ProductUnit = item.ProductUnit;
            //        detail.ProductId = item.ProductId;
            //        Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(item.PronoteHeader.InvoiceXOId);
            //        detail.CustomerInvoiceXOId = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
            //        this.produceMaterial.Details.Add(detail);
            //        //// }
            //    }

            //    var details = from ds in this.produceMaterial.Details group ds by ds.Product.DepotId;//into s select new { pt=from a in s select a.Product,key=s.Key};

            //    foreach (IGrouping<string, Model.ProduceMaterialdetails> item in details)
            //    {
            //        Model.ProduceMaterial ph = new Model.ProduceMaterial();
            //        ph.ProduceMaterialID = this.produceMaterialManager.GetId();
            //        ph.ProduceMaterialDate = System.DateTime.Now;
            //        ph.DepotId = item.Key;
            //        ph.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
            //        produceMaterial.Employee0Id = BL.V.ActiveOperator.Employee.EmployeeId;
            //        produceMaterial.Employee1Id = BL.V.ActiveOperator.Employee.EmployeeId;
            //        foreach (var g in item)
            //        {
            //            Model.ProduceMaterialdetails ds = new Model.ProduceMaterialdetails();
            //            ds.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
            //            ds.Materialprocessum = g.Materialprocessum.Value;
            //            ds.Materialprocesedsum = g.Materialprocesedsum.Value;
            //            ds.ProductStock = g.Product.StocksQuantity.Value;
            //            ds.ProductSpecification = g.Product.ProductSpecification;
            //            ds.Product = g.Product;
            //            ds.ProductId = g.Product.ProductId;
            //            ds.CustomerInvoiceXOId = g.CustomerInvoiceXOId;
            //            ds.ProductUnit = g.ProductUnit;
            //            ph.Details.Add(ds);
            //        }
            //        this.produceMaterialManager.Insert(ph);
            //    }

            //    this.pronoteHeader.IsBuildMaterial = true;
            //    this.pronoteHeaderManager.Update(this.pronoteHeader);
            //    this.barButtonItemMaterial.Caption = Properties.Resources.LookMaterialInfo;

            //    SelectProduceMaterial form = new SelectProduceMaterial(this.pronoteHeader);
            //    if (form.ShowDialog(this) == DialogResult.OK)
            //    {
            //        if (form.SelectItem == null) return;
            //        DataRow row = (form.SelectItem as DataRowView).Row;
            //        Model.ProduceMaterial temp = this.produceMaterialManager.Get(row.ItemArray[0].ToString());
            //        if (temp == null) return;
            //        ProduceMaterial.EditForm aa = new ProduceMaterial.EditForm(temp);
            //        aa.ShowDialog();
            //    }
            //}
            // else
            // {

            SelectProduceMaterial form = new SelectProduceMaterial(this.pronoteHeader);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (form.SelectItem == null) return;
                Model.ProduceMaterial pro = form.SelectItem as Model.ProduceMaterial;
                if (pro == null) return;
                ProduceMaterial.EditForm aa = new ProduceMaterial.EditForm(pro, "view");
                aa.ShowDialog();
            }
            // }
        }

        //根据mrsdetail显示3个列表
        private void MrsLoad(Model.MRSdetails mrsdetail)
        {

            this.pronoteHeader.MRSHeaderId = mrsdetail.MRSHeaderId;
            ////制成品
            //this.pronoteHeader.Details.Clear();
            //配料用料
            pronoteHeader.DetailsMaterial.Clear();
            //工序
            pronoteHeader.DetailProcedures.Clear();

            //this.pronoteHeader.MRSHeaderId = _mrsdetail.MRSHeaderId;
            //this.pronoteHeader.Product = _mrsdetail.Product;
            //this.pronoteHeader.ProductId = _mrsdetail.ProductId;
            //this.pronoteHeader.DetailsSum = _mrsdetail.MRSdetailssum - (_mrsdetail.MRSHasSingleSum == null ? 0 : _mrsdetail.MRSHasSingleSum);
            //this.pronoteHeader.ProductUnit = _mrsdetail.ProductUnit;
            //this.pronoteHeader.Employee0 = BL.V.ActiveOperator.Employee;
            //this.pronoteHeader.Employee1 = BL.V.ActiveOperator.Employee;

            //_mrsdetail.Product = this.productManager.Get(_mrsdetail.ProductId);
            this.textEditCustomProName.Text = string.IsNullOrEmpty(_mrsdetail.Product.CustomerProductName) ? string.Empty : _mrsdetail.Product.CustomerProductName;
            this.richTextBox1.Rtf = _mrsdetail.Product.ProductDescription;
            this.textEditUnit.Text = _mrsdetail.ProductUnit;
            this.buttonEditProId.EditValue = _mrsdetail.Product;
            this.labelProName.Text = _mrsdetail.Product.ProductName;
            this.textEditMRP.Text = _mrsdetail.MRSHeaderId;
            this.newChooseWorkHouse.EditValue = _mrsdetail.WorkHouseNext;

            if (!string.IsNullOrEmpty(_mrsdetail.MRSHeaderId))
            {
                Model.MRSHeader mrsHeader = this.mRSHeaderManager.Get(_mrsdetail.MRSHeaderId);
                if (mrsHeader != null)
                {
                    Model.MPSheader mpsheader = this.mPSheaderManager.Get(_mrsdetail.MPSheaderId);
                    if (mpsheader != null)
                    {
                        Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mpsheader.InvoiceXOId);
                        if (invoiceXO != null)
                        {
                            BL.InvoiceXODetailManager xodetailManager = new InvoiceXODetailManager();
                            this.textEditCustomXOId.Text = invoiceXO.CustomerInvoiceXOId;
                            this.newChooseCustomer.EditValue = invoiceXO.xocustomer;
                            this.textEditXOId.Text = invoiceXO.InvoiceId;
                            pronoteHeader.InvoiceCusId = invoiceXO.CustomerInvoiceXOId;
                            foreach (Model.InvoiceXODetail detail in xodetailManager.Select(invoiceXO, false))
                            {
                                if (detail.ProductId == _mrsdetail.MadeProductId)
                                {
                                    pronoteHeader.InvoiceXODetailQuantity = detail.InvoiceXODetailQuantity.Value;
                                    this.calcInvoiceXODetailQuantity.EditValue = pronoteHeader.InvoiceXODetailQuantity.Value;
                                }
                            }
                        }
                    }
                }
            }
            this.calcEditQuantity.Value = decimal.Parse((_mrsdetail.MRSdetailssum - (_mrsdetail.MRSHasSingleSum == null ? 0 : _mrsdetail.MRSHasSingleSum)).ToString());

            // this.textEditCustomXOId.Text=this.MpsdetailsManager.Get(_mrsdetail.MPSdetailsId)==null?"":this.MpsdetailsManager.Get(_mrsdetail.MPSdetailsId).InvoiceXOId
            ////if (pronotedetails.Product.IsCustomerProduct == true)
            ////{
            ////    bomP = new BL.BomParentPartInfoManager().Get(this.productManager.Get(pronotedetails.Product.CustomerBeforeProductId), pronotedetails.Product.Customer);
            ////    if (bomP == null)
            ////        bomP = new BL.BomParentPartInfoManager().Get(this.productManager.Get(pronotedetails.Product.CustomerBeforeProductId));
            ////}

            ////else

            Model.BomParentPartInfo bomP = this.bomParentPartInfoManager.Get(_mrsdetail.Product);
            if (bomP == null) return;
            //考虑在制品库存
            //查询
            Model.TechonlogyHeader th = this.techonlogyHeaderManager.Get(bomP.TechonlogyHeaderId);
            //IList<string> proce = new List<string>();
            //if (th != null)
            //{
            //    foreach (Model.Technologydetails pro in this.technologydetailsManager.Select(th))
            //    {
            //        proce.Add(pro.ProceduresId);

            //    }
            //}

            IList<Model.PronotedetailsMaterial> materiallist = new List<Model.PronotedetailsMaterial>();
            // double? sum = 0;
            int materialNo = 0;
            #region 在制品
            //if (bomP.Product.IsProcee != null && bomP.Product.IsProcee.Value)
            //{
            //    IList<Model.Product> productList = this.productManager.SelectProceProduct(bomP.Product);


            //    for (int i = 0; i < productList.Count; i++)
            //    {
            //        int tag1 = 0;
            //        //sum = sum + product.StocksQuantity;
            //        IList<Model.ProductProcess> processList = productProcessManager.Select(productList[i].ProductId);
            //        for (int j = 0; j < processList.Count; j++)
            //        {
            //            if (!proce.Contains(processList[j].ProceduresId))
            //            {
            //                tag1 = 1;
            //            }
            //        }

            //        if (tag1 == 1)
            //        {
            //            productList.RemoveAt(i);
            //            i--;
            //            continue;
            //        }

            //        productList[i].Indexs = processList == null ? 0 : processList.Count;

            //    }
            //    //根据工序数量倒序
            //    productList = productList.OrderByDescending(p => p.Indexs).ToList();
            //    for (int i = 0; i < productList.Count; i++)
            //    {
            //        sum += productList[i].StocksQuantity;
            //        if (sum <= mrsdetail.MRSdetailssum && productList[i].StocksQuantity != null && productList[i].StocksQuantity > 0)
            //        {
            //            Model.PronotedetailsMaterial materials = new Model.PronotedetailsMaterial();
            //            materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
            //            materials.Product = productList[i];
            //            materials.ProductId = productList[i].ProductId;
            //            materials.PronoteHeader = this.pronoteHeader;
            //            materials.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
            //            //配料为当前在制品库存
            //            materials.PronoteQuantity = productList[i].StocksQuantity;
            //            materials.PronoteQuantity = (materials.PronoteQuantity == null ? 0 : materials.PronoteQuantity);
            //            materials.PronoteQuantity = double.Parse(materials.PronoteQuantity.Value.ToString("F0"));
            //            materials.ProductUnit = mrsdetail.ProductUnit;
            //            materialNo += 1;
            //            materials.Inumber = materialNo;
            //            pronoteHeader.DetailsMaterial.Add(materials);
            //            materiallist.Add(materials);
            //            if (sum == mrsdetail.MRSdetailssum)
            //                break;
            //        }
            //        if (sum > mrsdetail.MRSdetailssum)
            //        {
            //            Model.PronotedetailsMaterial materials = new Model.PronotedetailsMaterial();
            //            materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
            //            materials.Product = productList[i];
            //            materials.ProductId = productList[i].ProductId;
            //            materials.PronoteHeader = this.pronoteHeader;
            //            materials.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
            //            //配料为当前在制品库存
            //            materials.PronoteQuantity = mrsdetail.MRSdetailssum - (sum - productList[i].StocksQuantity);
            //            materials.PronoteQuantity = (materials.PronoteQuantity == null ? 0 : materials.PronoteQuantity);
            //            materials.PronoteQuantity = double.Parse(materials.PronoteQuantity.Value.ToString("F0"));
            //            materials.ProductUnit = mrsdetail.ProductUnit;
            //            materialNo += 1;
            //            materials.Inumber = materialNo;
            //            pronoteHeader.DetailsMaterial.Add(materials);
            //            materiallist.Add(materials);
            //            break;
            //        }

            //    }

            //}
            #endregion
            //子件 用料
            //if (sum < mrsdetail.MRSdetailssum)
            {
                // double? mrpQuantity = ;   //mrsdetail.MRSdetailssum;
                foreach (Model.BomComponentInfo component in bomComponentinfoManager.Select(bomP))
                {
                    Model.PronotedetailsMaterial materials = new Model.PronotedetailsMaterial();
                    materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
                    materials.Product = component.Product;
                    materials.ProductId = component.ProductId;
                    materials.PronoteHeader = this.pronoteHeader;
                    materials.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
                    materials.PronoteQuantity = double.Parse(this.calcEditQuantity.Value.ToString()) * component.UseQuantity * (1 + 0.01 * (component.SubLoseRate == null ? 0 : component.SubLoseRate)); //component.UseQuantity * mrpQuantity * (1 + component.SubLoseRate / 100);
                    materials.PronoteQuantity = (materials.PronoteQuantity == null ? 0 : materials.PronoteQuantity);
                    materials.PronoteQuantity = double.Parse(materials.PronoteQuantity.Value.ToString("F0"));
                    materials.ProductUnit = component.Unit;
                    // materials.MPSQuantity = double.Parse(materials.PronoteQuantity.Value) * component.UseQuantity;
                    //  materials.MPSheaderId = mrsdetail.MRSHeader.MPSheaderId;
                    materialNo += 1;
                    materials.Inumber = materialNo;
                    pronoteHeader.DetailsMaterial.Add(materials);
                }
            }
            //包装
            if (!string.IsNullOrEmpty(bomP.Product.CustomerProductName))
            {
                IList<Model.BomPackageDetails> packageList = this.bomPackageDetailsManager.Select(bomP.BomId);
                if (packageList != null)
                {
                    foreach (Model.BomPackageDetails item in packageList)
                    {
                        Model.PronotedetailsMaterial materials = new Model.PronotedetailsMaterial();
                        materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
                        materials.Product = item.Product;
                        materials.ProductId = item.ProductId;
                        materials.PronoteHeader = this.pronoteHeader;
                        materials.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;

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

                        materials.PronoteQuantity = double.Parse(this.calcEditQuantity.Value.ToString()) * item.Quantity;
                        materials.ProductUnit = item.PackageUnit;
                        materials.MPSQuantity = mrsdetail.MRSdetailssum * item.UseQuantity;
                        // materials.MPSheaderId = mrsdetail.MRSHeader.MPSheaderId;
                        materialNo += 1;
                        materials.Inumber = materialNo;
                        pronoteHeader.DetailsMaterial.Add(materials);
                    }
                }
            }
            //工序(加工流程)
            if (!string.IsNullOrEmpty(bomP.TechonlogyHeaderId))
            {
                Model.TechonlogyHeader techonlogyHeader = this.techonlogyHeaderManager.Get(bomP.TechonlogyHeaderId);
                // Model.ManProcedure manProcedure = manProcedureManager.Select(bomP, mrsdetail.Customer, mrsdetail.MadeProduct);
                if (techonlogyHeader != null)
                {
                    Model.PronoteProceduresDetail pronoteProceduresDetail = null;
                    foreach (Model.Technologydetails technologydetails in (new TechnologydetailsManager().Select(techonlogyHeader)))
                    {
                        pronoteProceduresDetail = new Book.Model.PronoteProceduresDetail();
                        pronoteProceduresDetail.PronoteProceduresDetailId = Guid.NewGuid().ToString();
                        pronoteProceduresDetail.Procedures = technologydetails.Procedures;
                        //   pronoteProceduresDetail.PronoteMachine = technologydetails.Procedures.PronoteMachine;
                        //  if (pronoteProceduresDetail.PronoteMachine != null)
                        //    pronoteProceduresDetail.PronoteMachineId = pronoteProceduresDetail.PronoteMachine.PronoteMachineId;
                        pronoteProceduresDetail.ProceduresNo = technologydetails.TechnologydetailsNo;
                        if (technologydetails.Procedures != null)
                            pronoteProceduresDetail.ProceduresId = technologydetails.Procedures.ProceduresId;
                        pronoteProceduresDetail.WorkHouseId = technologydetails.Procedures.WorkHouseId;
                        pronoteProceduresDetail.IsOtherProduceOther = technologydetails.Procedures.IsOtherProduceOther;
                        pronoteProceduresDetail.Supplier = technologydetails.Procedures.Supplier;
                        pronoteProceduresDetail.SupplierId = technologydetails.Procedures.SupplierId;
                        //if (materiallist.Count == 0)
                        //{
                        //    pronoteProceduresDetail.PronoteYingQuantity = mrsdetail.MRSdetailssum;
                        //}
                        //else
                        //{
                        //    double? diff = mrsdetail.MRSdetailssum;
                        //    IList<Model.Product> productList = this.productManager.SelectProceProduct(bomP.Product);
                        //    foreach (Model.PronotedetailsMaterial material in materiallist)
                        //    {
                        //        //if (material.Product.IsProcee != null && material.Product.IsProcee == true)
                        //        //{
                        //        IList<Model.ProductProcess> processList = productProcessManager.Select(material.ProductId);
                        //        if (processList.Count >= Int32.Parse(technologydetails.TechnologydetailsNo))
                        //        {
                        //            diff -= material.PronoteQuantity;
                        //        }
                        //        //}
                        //    }
                        //    pronoteProceduresDetail.PronoteYingQuantity = diff;

                        //}

                        this.pronoteHeader.DetailProcedures.Add(pronoteProceduresDetail);
                        this.bindingSourceProcedures.Position = bindingSourceProcedures.IndexOf(pronoteProceduresDetail);

                    }


                }

            }


            // this.gridControl1.RefreshDataSource();
            this.gridControl2.RefreshDataSource();
            this.gridControl3.RefreshDataSource();

        }

        //搜索
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            ListForm f = new ListForm(this.FlagIsProcee);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.pronoteHeader = f.SelectItem as Model.PronoteHeader;
                this.action = "view";
                this.Refresh();
            }
            f.Dispose();
            GC.Collect();

        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            #region
            //    Model.Product product1 = (this.gridView2.GetRow(e.RowHandle) as Model.PronotedetailsMaterial).Product;
            //    double quantity = 0;
            //    double mrsCount = double.Parse(this.calcEditQuantity.Value.ToString());

            //    if (e.Column == this.gridColumnQuantity)
            //    {
            //        double.TryParse(e.Value.ToString(), out quantity);
            //        double? sum = quantity;
            //        #region 在制品
            //        foreach (Model.PronotedetailsMaterial meteriadDetail in this.pronoteHeader.DetailsMaterial)
            //        {

            //            if (meteriadDetail.ProductId != product1.ProductId)
            //            {

            //                if (meteriadDetail.Product.IsProcee != null && meteriadDetail.Product.IsProcee.Value)
            //                {
            //                    IList<Model.Product> productList = this.productManager.SelectProceProduct(meteriadDetail.Product);
            //                    foreach (Model.Product product in productList)
            //                    {
            //                        // sum = sum + product.StocksQuantity;
            //                        IList<Model.ProductProcess> processList = productProcessManager.Select(product.ProductId);
            //                        product.Indexs = processList == null ? 0 : processList.Count;
            //                    }
            //                    //根据工序数量倒序
            //                    productList = productList.OrderByDescending(p => p.Indexs).ToList();
            //                    for (int i = 0; i < productList.Count; i++)
            //                    {
            //                        if (productList[i].ProductId == product1.ProductId)
            //                        {
            //                            productList[i].StocksQuantity = quantity;
            //                        }
            //                        sum = sum + productList[i].StocksQuantity;
            //                        if (sum <= mrsCount && productList[i].StocksQuantity != null && productList[i].StocksQuantity > 0)
            //                        {
            //                            //配料为当前在制品库存
            //                            meteriadDetail.PronoteQuantity = productList[i].StocksQuantity;
            //                            if (sum == mrsCount)
            //                                break;
            //                        }
            //                        if (sum > mrsCount)
            //                        {
            //                            //配料为当前在制品库存
            //                            meteriadDetail.PronoteQuantity = mrsCount - (sum - productList[i].StocksQuantity);

            //                            break;
            //                        }
            //                    }
            //                }


            //            }
            //            else
            //            {


            //            }

            //        }
            //        #endregion
            //        #region 原料
            //        for (int i = 0; i < this.pronoteHeader.DetailsMaterial.Count; i++)
            //        {

            //            if (this.pronoteHeader.DetailsMaterial[i].ProductId != product1.ProductId)
            //            {

            //                if (this.pronoteHeader.DetailsMaterial[i].Product.IsProcee == null || this.pronoteHeader.DetailsMaterial[i].Product.IsProcee.Value == false)
            //                {
            //                    if (sum < mrsCount)
            //                    {
            //                        //配料
            //                        this.pronoteHeader.DetailsMaterial.Remove(this.pronoteHeader.DetailsMaterial[i]);
            //                        double? mrpQuantity = mrsCount - sum;
            //                        Model.BomParentPartInfo bom = this.bomParentPartInfoManager.Get(this.pronoteHeader.Product);
            //                        foreach (Model.BomComponentInfo component in bomComponentinfoManager.Select(bom))
            //                        {
            //                            Model.PronotedetailsMaterial materials = new Model.PronotedetailsMaterial();
            //                            materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
            //                            materials.Product = component.Product;
            //                            materials.ProductId = component.ProductId;
            //                            materials.PronoteHeader = this.pronoteHeader;
            //                            materials.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
            //                            materials.PronoteQuantity = component.UseQuantity * mrpQuantity * (1 + component.SubLoseRate / 100);
            //                            materials.ProductUnit = component.Unit;
            //                            materials.MPSQuantity = mrpQuantity * component.UseQuantity;
            //                            pronoteHeader.DetailsMaterial.Add(materials);
            //                        }

            //                    }


            //                }
            //            }
            //        }
            //        #endregion
            //        #region 工序数量
            //        //工序(加工流程)
            //        // if (!string.IsNullOrEmpty(bomP.TechonlogyHeaderId))
            //        {

            //            // //if (materiallist.Count == 0)
            //            // //{
            //            // //    pronoteProceduresDetail.PronoteYingQuantity = mrsdetail.MRSdetailssum;
            //            // //}
            //            //// else
            //            {
            //                if (this.pronoteHeader.DetailProcedures.Count > 0 && this.pronoteHeader.DetailProcedures[0].ProceduresId != null)
            //                {
            //                    // Model.PronoteProceduresDetail pronoteProceduresDetail = null;                 
            //                    //int tag1 = 0;
            //                    int index1 = 0;
            //                    foreach (Model.PronoteProceduresDetail proceduresDetail in this.pronoteHeader.DetailProcedures)
            //                    {

            //                        // IList<Model.Product> productList = this.productManager.SelectProceProduct(bomP.Product);
            //                        double? diff = mrsCount;
            //                        foreach (Model.PronotedetailsMaterial material in this.pronoteHeader.DetailsMaterial)
            //                        {
            //                            IList<Model.Product> productList = this.productManager.SelectProceProduct(material.Product);
            //                            IList<Model.ProductProcess> processList = productProcessManager.Select(material.ProductId);

            //                            //取当前工序在工艺中索引
            //                            Model.TechonlogyHeader th = this.techonlogyHeaderManager.Get(this.bomParentPartInfoManager.Get(this.pronoteHeader.Product).TechonlogyHeaderId);
            //                            if (th == null) continue;
            //                            foreach (Model.Technologydetails detail in this.technologydetailsManager.Select(th))
            //                            {
            //                                if (detail.ProceduresId == proceduresDetail.ProceduresId)
            //                                    index1 = Int32.Parse(detail.TechnologydetailsNo);
            //                            }

            //                            if (processList.Count >= index1)
            //                            {
            //                                diff -= material.PronoteQuantity;
            //                            }
            //                            proceduresDetail.PronoteYingQuantity = diff;
            //                        }

            //                    }
            //                }
            //            }

            //        }

            //        #endregion


            //        this.gridControl2.RefreshDataSource();
            //        this.gridControl3.RefreshDataSource();
            //    }
            #endregion
        }

        //条件打印
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //produceManager.PronoteHeader.RO3 f = new RO3(pronoteHeader.PronoteHeaderID, FlagIsProcee);
            //f.ShowPreviewDialog();

            //连打
            try
            {
                //Query.ConditionPronoteHeaderChooseForm f2 = new Book.UI.Query.ConditionPronoteHeaderChooseForm(this.FlagIsProcee);
                Query.ConditionPronoteHeaderChooseForm f2 = new Book.UI.Query.ConditionPronoteHeaderChooseForm();
                if (f2.ShowDialog(this) == DialogResult.OK)
                {

                    if (this.FlagIsProcee == 4)
                    {
                        RODetail f1 = new RODetail(f2.Condition as Query.ConditionPronoteHeader);
                        f1.ShowPreviewDialog();
                    }
                    else
                    {
                        ROZZJiaGongDetail f1 = new ROZZJiaGongDetail(f2.Condition as Query.ConditionPronoteHeader);
                        f1.ShowPreviewDialog();
                    }
                    //RODetail f1 = new RODetail(f2.Condition as Query.ConditionPronoteHeader);
                    //f1.ShowPreviewDialog();
                }
            }
            catch (Helper.InvalidValueException)
            {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.NoRecords, Properties.Resources.Title_NoRecRecord, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            Model.PronoteProceduresDetail proceduresDetail = this.bindingSourceProcedures.Current as Model.PronoteProceduresDetail;
            this.pronoteMachines = this.pronoteMachineManager.GetPronoteMachineByPronoteProceduresDetailId(detail.PronoteProceduresDetailId);
            //ProduceStatistics.EditForm f = new ProduceStatistics.EditForm(pronoteHeader.PronoteHeaderID, proceduresDetail.ProceduresId, "insert");
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //pronoteMachines.Clear();
            //pronoteMachines = f.SelectItem;
            //string text = string.Empty;
            //foreach (Model.PronoteMachine machine in pronoteMachines)
            //{
            //    text += machine.PronoteMachineName + ",";
            //    Model.ProceduresMachine promachine = new Book.Model.ProceduresMachine();
            //    promachine.ProceduresMachineId = Guid.NewGuid().ToString();
            //    promachine.PronoteProceduresDetailId = this.detail.PronoteProceduresDetailId;
            //    promachine.ProceduresId = detail.ProceduresId;
            //    promachine.PronoteMachineId = machine.PronoteMachineId;
            //    this.pronoteHeader.ProceduresMachineDetail.Add(promachine);
            //    //this.proceManager.Insert(promachine);
            //}
            //if (text != "")
            //{
            //    this.gridView3.SetFocusedValue(text.Substring(0, text.Length - 1));
            //}
            //else
            //    this.gridView3.SetFocusedValue(text);

        }

        private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.PronoteProceduresDetail> details = this.bindingSourceProcedures.DataSource as IList<Model.PronoteProceduresDetail>;
            if (details == null || details.Count < 1) return;
            Model.PronoteProceduresDetail detail = details[e.ListSourceRowIndex];
            //IList<Model.MPSheader> mpsHeader = new BL.MPSheaderManager().SelectById(details[e.ListSourceRowIndex].MPSheaderId);
            //  Model.ProduceStatisticsDetail det = new BL.ProduceStatisticsDetailManager().SelectbyPronoteHeaderProceduresSum(detail.PronoteHeaderID, detail.ProceduresId);
            // if (det == null) return;
            switch (e.Column.Name)
            {


                //case "gridColumn21":

                //    e.DisplayText = det.ProduceQuantity.ToString();
                //    break;
                //case "gridColumn18":

                //    e.DisplayText = det.HeGeQuantity.ToString();
                //    break;
                //case "gridColumn22":

                //    e.DisplayText = det.NoProduceQuantity1 < 0 ? "0" : det.NoProduceQuantity1.ToString();
                //    break;
                case "gridColumn16":

                    e.DisplayText = detail.Procedures == null ? "" : detail.Procedures.Proceduredescription;
                    break;

            }

        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (ChooseProductForm.ProductList.Count == 0) return;
                foreach (Model.Product item in ChooseProductForm.ProductList)
                {
                    Model.PronotedetailsMaterial temp = new Model.PronotedetailsMaterial();
                    temp.PronotedetailsMaterialId = Guid.NewGuid().ToString();
                    temp.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
                    temp.Product = item;
                    temp.ProductId = item.ProductId;
                    temp.ProductUnit = item.DepotUnit == null ? null : item.DepotUnit.CnName;
                    temp.PronoteQuantity = 0;
                    temp.AlreadyDrawQuantity = 0;
                    this.pronoteHeader.DetailsMaterial.Add(temp);
                }
                this.gridControl2.RefreshDataSource();
            }
            f.Dispose();
            System.GC.Collect();
        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceMaterial.Current != null)
            {
                this.pronoteHeader.DetailsMaterial.RemoveAt(this.bindingSourceMaterial.Position);
                this.gridControl2.RefreshDataSource();
            }
        }

        private void textEditPronotedesc_DoubleClick(object sender, EventArgs e)
        {

            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditPronotedesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核
        protected override string AuditKeyId()
        {
            return Model.PronoteHeader.PRO_PronoteHeaderID;
        }

        protected override int AuditState()
        {
            return this.pronoteHeader.AuditState.HasValue ? this.pronoteHeader.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PronoteHeader" + "," + this.pronoteHeader.PronoteHeaderID;
        }
        #endregion

        //查看退料
        private void barBtnMaterialExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.pronoteHeader != null)
            {
                SelectProduceMaterialExit form = new SelectProduceMaterialExit(this.pronoteHeader);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (form.SelectItem == null) return;
                    Model.ProduceMaterialExit pro = form.SelectItem;
                    if (pro == null) return;
                    ProduceMaterialExit.EditForm f = new Book.UI.produceManager.ProduceMaterialExit.EditForm(pro, "view");
                    f.ShowDialog();
                }
            }
        }

        //结案
        private void barButtonItemJieAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.pronoteHeader == null) return;
            if (!this.pronoteHeader.IsClose.Value)
            {
                if (MessageBox.Show("是否強制結案?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }
            if (this.pronoteHeader.IsClose.Value)
            {
                if (MessageBox.Show("是否取消結案?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }
            this.pronoteHeader.IsClose = !this.pronoteHeader.IsClose;
            try
            {
                BL.V.BeginTransaction();
                this.pronoteHeaderManager.UpdateHeaderIsClse(this.pronoteHeader.PronoteHeaderID, this.pronoteHeader.IsClose.Value);
                BL.V.CommitTransaction();
                MessageBox.Show("操作成功", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.updateCaption();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                this.pronoteHeader.IsClose = !this.pronoteHeader.IsClose;
                throw ex;
            }
        }

        private void updateCaption()
        {
            if (this.pronoteHeader.IsClose == null)
            {
                this.pronoteHeader.IsClose = false;
            }
            if (this.pronoteHeader.IsClose.Value)
            {
                this.barButtonItemJieAn.Caption = "取消結案";
                this.dateEdit1.DateTime = DateTime.Now; ;
            }

            else
                this.barButtonItemJieAn.Caption = "結案";
            this.barButtonItemJieAn.Enabled = this.action == "view" ? true : false;
        }

        private void btn_MakeProduceMateria_Click(object sender, EventArgs e)
        {
            if (this.pronoteHeader.IsClose.HasValue && this.pronoteHeader.IsClose.Value)
            {
                MessageBox.Show("已結案，不能生成領料單", this.Text, MessageBoxButtons.OK);
                return;
            }

            IList<Model.PronotedetailsMaterial> detail = this.pronoteHeader.DetailsMaterial.Where(n => n.Checkeds == true).ToList();
            if (detail == null || detail.Count() == 0)
            {
                MessageBox.Show("請選擇需要生成的商品！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ProduceMaterial.EditForm form = new Book.UI.produceManager.ProduceMaterial.EditForm(detail);
            //MainForm f = new MainForm();
            form.Show(this);
        }
    }
}