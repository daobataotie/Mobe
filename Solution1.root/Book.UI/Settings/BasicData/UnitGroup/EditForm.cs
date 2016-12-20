using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.Settings.BasicData.UnitGroup
{
    public partial class EditForm : BaseEditForm
    {
        private Model.UnitGroup _unitGroup;
        int flag = 0;
        private BL.UnitGroupManager unitGroupManager = new Book.BL.UnitGroupManager();
        private BL.ProductUnitManager productUnitManager = new Book.BL.ProductUnitManager();
        public EditForm()
        {
            InitializeComponent();

            //this.requireValueExceptions.Add(Model.UnitGroup.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.requireValueExceptions.Add(Model.UnitGroup.PROPERTY_UNITGROUPNAME, new AA(Properties.Resources.RequireDataForNames, this.textEditUnitGroupName));
            this.requireValueExceptions.Add(Model.UnitGroup.PROPERTY_UNITGROUPTYPE, new AA(Properties.Resources.RequireChooseUnitGroupType, this.comboBoxEditUnitGroupType));
            //this.invalidValueExceptions.Add(Model.UnitGroup.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditId));
            this.invalidValueExceptions.Add(Model.UnitGroup.PROPERTY_UNITGROUPNAME, new AA(Properties.Resources.EntityName, this.textEditUnitGroupName));
            this.invalidValueExceptions.Add(Model.ProductUnit.PROPERTY_CNNAME, new AA(Properties.Resources.EntityName, this));
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
            this.action = "insert";
        }

        public EditForm(Model.UnitGroup _unitGroup)
            : this()
        {
            this._unitGroup = _unitGroup;
            this._unitGroup.Details = this.productUnitManager.Select(_unitGroup==null?"":_unitGroup.UnitGroupId);
            this.action = "update";
        }
        public EditForm(Model.UnitGroup _unitGroup, string action)
            : this()
        {
            this._unitGroup = _unitGroup;
            this._unitGroup.Details = this.productUnitManager.Select(_unitGroup == null ? "" : _unitGroup.UnitGroupId);
            this.action = action;
        }
        #region Override

        protected override void AddNew()
        {
            this._unitGroup = new Model.UnitGroup();
            this._unitGroup.UnitGroupId = Guid.NewGuid().ToString();
            this._unitGroup.Details = new List<Model.ProductUnit>();

        }

        protected override void Delete()
        {
            if (this._unitGroup == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.unitGroupManager.Delete(this._unitGroup.UnitGroupId);
            this._unitGroup = this.unitGroupManager.GetNext(this._unitGroup);
            if (this._unitGroup == null)
            {
                this._unitGroup = this.unitGroupManager.GetLast();
            }
            flag = 1;
            TreeLoad();
        }

        protected override bool HasRows()
        {
            return this.unitGroupManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.unitGroupManager.HasRowsAfter(this._unitGroup);
        }

        protected override bool HasRowsPrev()
        {
            return this.unitGroupManager.HasRowsBefore(this._unitGroup);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditUnitGroupName }, new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit[] { this.repositoryItemSpinEdit1 }, new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit[] { this.repositoryItemTextEdit1 });
        }

        protected override void MoveFirst()
        {
            this._unitGroup = this.unitGroupManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._unitGroup = this.unitGroupManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.UnitGroup unitGroup = this.unitGroupManager.GetNext(this._unitGroup);
            if (unitGroup == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._unitGroup = unitGroup;
        }

        protected override void MovePrev()
        {
            Model.UnitGroup unitGroup = this.unitGroupManager.GetPrev(this._unitGroup);
            if (unitGroup == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._unitGroup = unitGroup;
        }


        public override void Refresh()
        {

            if (this._unitGroup == null)
            {
                this._unitGroup = new Book.Model.UnitGroup();
                this._unitGroup.Details = new List<Model.ProductUnit>();

                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._unitGroup = this.unitGroupManager.GetDetails(_unitGroup.UnitGroupId);

                }
            }
            if (this.action == "insert" || (this.action == "update" && this._unitGroup.Details.Count == 0))
            {


                Model.ProductUnit detail = new Model.ProductUnit();
                detail.UnitGroupId = Guid.NewGuid().ToString();
                detail.CnName = "";
                detail.ConvertRate = 1;
                detail.IsMainUnit = false;
                this._unitGroup.Details.Add(detail);
                this.bindingSourceUnitGroup.Position = this.bindingSourceUnitGroup.IndexOf(detail);
            }



            this.bindingSourceUnitGroup.DataSource = this._unitGroup.Details;
            this.gridControl1.RefreshDataSource();
            flag = 1;
            if (this.action != "view")
                TreeLoad();

            //this.textEditId.Text = this._unitGroup.Id;
            this.textEditUnitGroupName.EditValue = this._unitGroup.UnitGroupName;
            this.comboBoxEditUnitGroupType.Text = this._unitGroup.UnitGroupType;
            flag = 0;
            switch (this.action)
            {
                case "insert":
                    // this.textEditId.Properties.ReadOnly = false;
                    this.textEditUnitGroupName.Properties.ReadOnly = false;
                    this.comboBoxEditUnitGroupType.Properties.ReadOnly = false;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2.Enabled = true;

                    break;

                case "update":
                    // this.textEditId.Properties.ReadOnly = false ;
                    this.textEditUnitGroupName.Properties.ReadOnly = false;
                    this.comboBoxEditUnitGroupType.Properties.ReadOnly = false;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2.Enabled = true;
                    break;

                case "view":
                    // this.textEditId.Properties.ReadOnly = true;
                    this.textEditUnitGroupName.Properties.ReadOnly = true;
                    this.comboBoxEditUnitGroupType.Properties.ReadOnly = true;
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.simpleButton1.Enabled = false;
                    this.simpleButton2.Enabled = false;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();
            //this._unitGroup.Id = this.textEditId.Text;
            this._unitGroup.UnitGroupName = this.textEditUnitGroupName.Text;
            this._unitGroup.UnitGroupType = this.comboBoxEditUnitGroupType.Text;
            switch (this.action)
            {
                case "insert":
                    this.unitGroupManager.Insert(this._unitGroup);
                    break;
                case "update":
                    this.unitGroupManager.Update(this._unitGroup);
                    break;
                default:
                    break;
            }
            flag = 1;
            TreeLoad();
        }

        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();
            //    TreeLoad();
        }
        protected void TreeLoad()
        {
            this.treeList1.ClearNodes();
            foreach (Model.UnitGroup ug in unitGroupManager.Select())
            {

                DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { ug.UnitGroupName }, null, ug.UnitGroupId);


                foreach (Model.ProductUnit ProductUnit in productUnitManager.Select(ug.UnitGroupId))
                {

                    treeList1.AppendNode(new object[] { ProductUnit.CnName }, treeNode, ProductUnit.UnitGroup.UnitGroupId);

                }


            }
        }
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (flag == 0)
            {
                if (e.Node != null && e.Node.ParentNode == null)
                {
                    this.action = "view";
                    this._unitGroup = unitGroupManager.Get(e.Node.Tag.ToString());

                    this.Refresh();
                }
            }
            //  flag = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceUnitGroup.Current != null)
            {
                try
                {

                    new BL.ProductUnitManager().Delete(this.bindingSourceUnitGroup.Current as Book.Model.ProductUnit);

                }
                catch
                {
                    MessageBox.Show(Properties.Resources.DeleteError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this._unitGroup.Details.Remove(this.bindingSourceUnitGroup.Current as Book.Model.ProductUnit);



                if (this._unitGroup.Details.Count == 0)
                {
                    Model.ProductUnit detail = new Model.ProductUnit();
                    detail.UnitGroupId = Guid.NewGuid().ToString();
                    detail.CnName = "";
                    detail.ConvertRate = 1;
                    detail.IsMainUnit = false;
                    this._unitGroup.Details.Add(detail);
                    this.bindingSourceUnitGroup.Position = this.bindingSourceUnitGroup.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.ProductUnit detail = new Model.ProductUnit();
                    detail.UnitGroupId = Guid.NewGuid().ToString();
                    detail.CnName = "";
                    detail.ConvertRate = 1;
                    detail.IsMainUnit = false;
                    this._unitGroup.Details.Add(detail);
                    this.bindingSourceUnitGroup.Position = this.bindingSourceUnitGroup.IndexOf(detail);

                }
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButton1.PerformClick();
                }
                this.gridControl1.RefreshDataSource();

            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.action != "view")
            {
                Model.ProductUnit detail = new Model.ProductUnit();
                detail.UnitGroupId = Guid.NewGuid().ToString();
                detail.CnName = "";
                detail.ConvertRate = 1;
                detail.IsMainUnit = false;
                this._unitGroup.Details.Add(detail);
                this.bindingSourceUnitGroup.Position = this.bindingSourceUnitGroup.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}