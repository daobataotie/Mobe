using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;

namespace Book.UI.produceManager.PronoteHeader
{
    public partial class ProduceProceduresForm : Settings.BasicData.BaseEditForm
    {

        #region 變量對象的定義
        protected BL.PronoteProceduresAbilityManager pronoteProceduresAbilityManager = new Book.BL.PronoteProceduresAbilityManager();

        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private BL.PronoteMachineManager pronoteMachineManager = new Book.BL.PronoteMachineManager();
        private Model.PronoteProceduresAbility _pronoteProceduresAbility;
        private BL.ProductMouldManager productMouldManager = new Book.BL.ProductMouldManager();
        private BL.ProceduresManager proceduresManager = new Book.BL.ProceduresManager();
        private BL.WorkHouseManager workHouseManager = new Book.BL.WorkHouseManager();

        private IList<Model.PronoteMachine> pronoteMachines = new List<Model.PronoteMachine>();
        #endregion

        #region Constructors

        public ProduceProceduresForm()
        {
            InitializeComponent();

            //this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEdit_pronoteProceduresAbilityId));
            //this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOf_pronoteProceduresAbilityDate, this.dateEdit_pronoteProceduresAbilityDate));
            //this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            //this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            //this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));
            //this.requireValueExceptions.Add("Price", new AA("請填寫單價或數量！", this.gridControl1));

            //this.invalidValueExceptions.Add(Model._pronoteProceduresAbilityCJ.PROPERTY__pronoteProceduresAbilityID, new AA(Properties.Resources.EntityExists, this.textEdit_pronoteProceduresAbilityId));

            this.action = "view";
            this.newChooseContorlWorkHouse.Choose = new Book.UI.Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
        }

        //public ProduceProceduresForm(string _pronoteProceduresAbilityId) 
        //    : this() 
        //{
        //    _pronoteProceduresAbility= this.pronoteProceduresAbilityManager.Get(_pronoteProceduresAbilityId);
        //    if(this._pronoteProceduresAbility==null)
        //        throw new ArithmeticException("_pronoteProceduresAbilityid");

        //    this.action = "update";
        //}

        //public EditForm(Model._pronoteProceduresAbilityCJ _pronoteProceduresAbilitycj) 
        //    : this()
        //{
        //    if(_pronoteProceduresAbilitycj==null)
        //        throw new ArithmeticException("_pronoteProceduresAbilityid");
        //    _pronoteProceduresAbility= _pronoteProceduresAbilitycj;

        //    this.action = "update";
        //}

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            this.bindingSourceProduct.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            this.bindingSourceMachine.DataSource = this.pronoteMachineManager.Select();
            this.bindingSourceMould.DataSource = this.productMouldManager.Select();
            TreeLoad();
        }

        /*
        private void update()
        {
            this._pronoteProceduresAbility.Details = this._pronoteProceduresAbilityCJDetailManager.Select(this._pronoteProceduresAbility);

            global::Helper._pronoteProceduresAbilityStatus status = (Helper._pronoteProceduresAbilityStatus)this._pronoteProceduresAbility._pronoteProceduresAbilityStatus.Value;

            this.textEdit_pronoteProceduresAbilityId.Properties.ReadOnly = true;
            

            //this.textEditAbstract.Properties.ReadOnly = (status == global::Helper._pronoteProceduresAbilityStatus.Null);
            this.textEditNote.Properties.ReadOnly = (status == global::Helper._pronoteProceduresAbilityStatus.Null);

            this.buttonEditEmployee.Enabled = (status == global::Helper._pronoteProceduresAbilityStatus.Draft);
            this.buttonEditCompany.Enabled = (status == global::Helper._pronoteProceduresAbilityStatus.Draft);

            this.col_pronoteProceduresAbilityCJDetailQuantity.OptionsColumn.AllowEdit = (status == global::Helper._pronoteProceduresAbilityStatus.Draft);
            this.col_pronoteProceduresAbilityCJDetailNote.OptionsColumn.AllowEdit = (status != global::Helper._pronoteProceduresAbilityStatus.Null);
            this.col_pronoteProceduresAbilityCJDetailPrice.OptionsColumn.AllowEdit = (status == global::Helper._pronoteProceduresAbilityStatus.Draft);

            this.simpleButtonAppend.Enabled = (status == global::Helper._pronoteProceduresAbilityStatus.Draft);
            this.simpleButtonRemove.Enabled = (status == global::Helper._pronoteProceduresAbilityStatus.Draft);

            this.barButtonItemSave.Enabled = (status == global::Helper._pronoteProceduresAbilityStatus.Draft);
        }
         * */
        #endregion

        #region Save

        protected override void Save()
        {
            this._pronoteProceduresAbility.Procedures = this.buttonEditProcedures.EditValue as Model.Procedures;
            if (this._pronoteProceduresAbility.Procedures != null)
                this._pronoteProceduresAbility.ProceduresId = this._pronoteProceduresAbility.Procedures.ProceduresId;
            // this._pronoteProceduresAbility.PronoteMachineId = this.textEditMachine.Text;
            this._pronoteProceduresAbility.WorkHouse = this.newChooseContorlWorkHouse.EditValue as Model.WorkHouse;
            if (this._pronoteProceduresAbility.WorkHouse != null)
                this._pronoteProceduresAbility.WorkHouseId = this._pronoteProceduresAbility.WorkHouse.WorkHouseId;


            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.pronoteProceduresAbilityManager.Insert(this._pronoteProceduresAbility);
                    break;

                case "update":
                    this.pronoteProceduresAbilityManager.Update(this._pronoteProceduresAbility);
                    break;
            }
        }


        #endregion

        #region simpleButton_Click

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteProceduresAbilityDetail detail = new Book.Model.PronoteProceduresAbilityDetail();

                detail.PronoteProceduresAbilityDetailId = Guid.NewGuid().ToString();
                detail.PronoteProceduresAbility = this._pronoteProceduresAbility;

                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.TimeConsuming = 0;
                detail.UnitOutput = 0;
                detail.ProductUnit = detail.Product.ProduceUnit == null ? "" : detail.Product.ProduceUnit.CnName;
                this._pronoteProceduresAbility.Details.Add(detail);
                this.gridControl1.RefreshDataSource();
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this._pronoteProceduresAbility.Details.Remove(this.bindingSource1.Current as Book.Model.PronoteProceduresAbilityDetail);

                if (this._pronoteProceduresAbility.Details.Count == 0)
                {
                    Model.PronoteProceduresAbilityDetail detail = new Book.Model.PronoteProceduresAbilityDetail();
                    detail.PronoteProceduresAbilityDetailId = Guid.NewGuid().ToString();
                    detail.PronoteProceduresAbility = this._pronoteProceduresAbility;
                    detail.Product = new Book.Model.Product();
                    detail.TimeConsuming = 0;
                    detail.UnitOutput = 0;
                    this._pronoteProceduresAbility.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();

            }
        }
        #endregion

        #region CellValueChange



        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId || e.Column == this.colProduct)
            {
                Model.PronoteProceduresAbilityDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.PronoteProceduresAbilityDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.PronoteProceduresAbilityDetailId = Guid.NewGuid().ToString();
                    detail.PronoteProceduresAbility = this._pronoteProceduresAbility;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.TimeConsuming = 0;
                    detail.UnitOutput = 0;
                    detail.ProductUnit = detail.Product.ProduceUnit == null ? "" : detail.Product.ProduceUnit.CnName;
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }
        #endregion

        #region Choose Object





        #endregion

        #region 重載基類的方法
        protected override void Delete()
        {
            if (this.pronoteProceduresAbilityManager == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.pronoteProceduresAbilityManager.Delete(this._pronoteProceduresAbility.PronoteProceduresAbilityId);
        }

        protected override void AddNew()
        {
            this._pronoteProceduresAbility = new Model.PronoteProceduresAbility();
            this._pronoteProceduresAbility.PronoteProceduresAbilityId = Guid.NewGuid().ToString();

            this._pronoteProceduresAbility.Details = new List<Model.PronoteProceduresAbilityDetail>();
            if (this.action == "insert")
            {

                Model.PronoteProceduresAbilityDetail detail = new Book.Model.PronoteProceduresAbilityDetail();
                detail.PronoteProceduresAbilityDetailId = Guid.NewGuid().ToString();
                detail.PronoteProceduresAbility = this._pronoteProceduresAbility;
                detail.Product = new Book.Model.Product();
                detail.TimeConsuming = 0;
                detail.UnitOutput = 0;
                this._pronoteProceduresAbility.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        protected override void MoveNext()
        {
            Model.PronoteProceduresAbility _pronoteProceduresAbility = this.pronoteProceduresAbilityManager.GetNext(this._pronoteProceduresAbility);
            if (_pronoteProceduresAbility == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _pronoteProceduresAbility = this.pronoteProceduresAbilityManager.Get(_pronoteProceduresAbility.PronoteProceduresAbilityId);
        }

        protected override void MovePrev()
        {
            Model.PronoteProceduresAbility _pronoteProceduresAbility = this.pronoteProceduresAbilityManager.GetPrev(this._pronoteProceduresAbility);
            if (_pronoteProceduresAbility == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _pronoteProceduresAbility = this.pronoteProceduresAbilityManager.Get(_pronoteProceduresAbility.PronoteProceduresAbilityId);
        }

        protected override void MoveFirst()
        {
            _pronoteProceduresAbility = this.pronoteProceduresAbilityManager.Get(this.pronoteProceduresAbilityManager.GetFirst() == null ? "" : this.pronoteProceduresAbilityManager.GetFirst().PronoteProceduresAbilityId);
        }

        protected override void MoveLast()
        {
            _pronoteProceduresAbility = this.pronoteProceduresAbilityManager.Get(this.pronoteProceduresAbilityManager.GetLast() == null ? "" : this.pronoteProceduresAbilityManager.GetLast().PronoteProceduresAbilityId);
        }

        public override void Refresh()
        {
            if (_pronoteProceduresAbility == null)
            {
                _pronoteProceduresAbility = new Book.Model.PronoteProceduresAbility();
                this._pronoteProceduresAbility.PronoteProceduresAbilityId = Guid.NewGuid().ToString();
                this._pronoteProceduresAbility.Details = new List<Model.PronoteProceduresAbilityDetail>();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    _pronoteProceduresAbility = this.pronoteProceduresAbilityManager.GetDetail(_pronoteProceduresAbility.PronoteProceduresAbilityId);
                if (_pronoteProceduresAbility == null)
                {
                    _pronoteProceduresAbility = new Book.Model.PronoteProceduresAbility();
                    this._pronoteProceduresAbility.PronoteProceduresAbilityId = Guid.NewGuid().ToString();
                    this._pronoteProceduresAbility.Details = new List<Model.PronoteProceduresAbilityDetail>();
                    this.action = "insert";

                }
            }

            if (this._pronoteProceduresAbility.Procedures != null)
            {
                this.buttonEditProcedures.EditValue = this._pronoteProceduresAbility.Procedures;
                this.richTextBoxProdureName.Rtf = this._pronoteProceduresAbility.Procedures.Procedurename;
            }

            //this.pronoteMachines.Clear();
            //  string text = string.Empty;
            // this.pronoteMachines = this.pronoteMachineManager.SelectMachineByProduresId(this._pronoteProceduresAbility.ProceduresId);
            //if (this.pronoteMachines.Count != 0)
            //{
            //    foreach (Model.PronoteMachine pro in this.pronoteMachines)
            //    {
            //        text += pro.PronoteMachineName + ",";
            //    }
            //    if (text.Length > 0)
            //        text = text.Substring(0, text.Length - 1);
            //}
            // this.textEditMachine.Text = text;

            //this.textEditMachine.Text = this._pronoteProceduresAbility.PronoteMachineId;
            if (this._pronoteProceduresAbility.WorkHouse != null)
                this.newChooseContorlWorkHouse.EditValue = this._pronoteProceduresAbility.WorkHouse;
            this.bindingSource1.DataSource = this._pronoteProceduresAbility.Details;

            //switch (this.action)
            //{
            //    case "insert":
            //        this.buttonEditProcedures.Properties.ReadOnly = false;
            //        this.lookUpEditMachine.Properties.ReadOnly = false;
            //        this.newChooseContorlWorkHouse.Enabled = true;
            //        this.gridView1.OptionsBehavior.Editable = true;
            //        break;

            //    case "update":
            //        this.buttonEditProcedures.Properties.ReadOnly = false;
            //        this.lookUpEditMachine.Properties.ReadOnly = false;
            //        this.newChooseContorlWorkHouse.Enabled = true;
            //        this.gridView1.OptionsBehavior.Editable = true;
            //        break;

            //    case "view":
            //        this.buttonEditProcedures.Properties.ReadOnly = true;
            //        this.lookUpEditMachine.Properties.ReadOnly = true;
            //        this.newChooseContorlWorkHouse.Enabled = false;
            //        this.gridView1.OptionsBehavior.Editable = false;
            //        break;

            //    default:
            //        break;
            //}

            base.Refresh();
            this.buttonEditProcedures.Enabled = false;
            this.newChooseContorlWorkHouse.Enabled = false;
            //this.textEditMachine.Properties.ReadOnly = true;
        }


        protected override bool HasRows()
        {
            return this.pronoteProceduresAbilityManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.pronoteProceduresAbilityManager.HasRowsAfter(this._pronoteProceduresAbility);
        }

        protected override bool HasRowsPrev()
        {
            return this.pronoteProceduresAbilityManager.HasRowsBefore(this._pronoteProceduresAbility);
        }

        #endregion


        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumn1")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.PronoteProceduresAbilityDetail).Product;

                    this.repositoryItemComboBox1.Items.Clear();

                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        BL.ProductUnitManager unitManager = new Book.BL.ProductUnitManager();
                        IList<Model.ProductUnit> unitList = unitManager.Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit item in unitList)
                        {
                            this.repositoryItemComboBox1.Items.Add(item.CnName);
                        }
                    }
                }
            }
        }


        #region 重載父類的方法
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this });
        }
        #endregion


        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {

                if (e.KeyData == Keys.Enter)
                {
                    Model.PronoteProceduresAbilityDetail detail = new Book.Model.PronoteProceduresAbilityDetail();
                    detail.PronoteProceduresAbilityDetailId = Guid.NewGuid().ToString();
                    detail.PronoteProceduresAbility = this._pronoteProceduresAbility;
                    detail.Product = new Book.Model.Product();
                    detail.TimeConsuming = 0;
                    detail.UnitOutput = 0;
                    this._pronoteProceduresAbility.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }

                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButtonRemove.PerformClick();
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void buttonEditProcedures_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Settings.ProduceManager.Techonlogy.ChooseProceduresForm f = new Book.UI.Settings.ProduceManager.Techonlogy.ChooseProceduresForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            Model.Procedures procedure = f.SelectedItem as Model.Procedures;
            if (procedure != null)
            {
                this.buttonEditProcedures.EditValue = procedure;

                this.richTextBoxProdureName.Rtf = procedure.Procedurename;

                this._pronoteProceduresAbility.Procedures = procedure;
                this._pronoteProceduresAbility.ProceduresId = procedure.ProceduresId;
            }
        }

        //private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{

        //if (e.ListSourceRowIndex < 0) return;
        //IList<Model._pronoteProceduresAbilityCJDetail> details = this.bindingSource1.DataSource as IList<Model._pronoteProceduresAbilityCJDetail>;
        //if (details == null || details.Count < 1) return;
        //Model.Product detail = details[e.ListSourceRowIndex].Product;
        //switch (e.Column.Name)
        //{
        //    case "colProductId":
        //        if (detail == null) return;
        //        e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
        //        break;

        //}
        //}
        /// <summary>
        /// 加载tree
        /// </summary>
        protected void TreeLoad()
        {
            this.treeList1.ClearNodes();
            foreach (Model.WorkHouse workHouse in workHouseManager.Select())
            {

                DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { workHouse.Workhousename }, null, workHouse.WorkHouseId);


                foreach (Model.Procedures procedures in proceduresManager.Select(workHouse.WorkHouseId))
                {

                    treeList1.AppendNode(new object[] { procedures.Id }, treeNode, procedures.ProceduresId);

                }


            }
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            if (e.Node != null && e.Node.ParentNode != null)
            {

                this._pronoteProceduresAbility = this.pronoteProceduresAbilityManager.GetByProcedures(e.Node.Tag.ToString());
                if (this._pronoteProceduresAbility == null)
                {
                    this._pronoteProceduresAbility = new Model.PronoteProceduresAbility();
                    this._pronoteProceduresAbility.PronoteProceduresAbilityId = Guid.NewGuid().ToString();
                    this._pronoteProceduresAbility.Procedures = this.proceduresManager.Get(e.Node.Tag.ToString());
                    this._pronoteProceduresAbility.ProceduresId = this._pronoteProceduresAbility.Procedures.ProceduresId;
                    this._pronoteProceduresAbility.WorkHouse = this.workHouseManager.Get(this._pronoteProceduresAbility.Procedures.WorkHouseId);

                    if (this._pronoteProceduresAbility.WorkHouse != null)
                        this._pronoteProceduresAbility.WorkHouseId = this._pronoteProceduresAbility.WorkHouse.WorkHouseId;

                    this._pronoteProceduresAbility.Details = new List<Model.PronoteProceduresAbilityDetail>();
                    this.action = "insert";

                }
                else
                    this.action = "view";
                this.Refresh();
            }
        }


    }
}