using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Depots
{
    public partial class AddDepotpositionForm : BaseEditForm
    {
        protected BL.DepotManager depotManager = new Book.BL.DepotManager();
        protected BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private Model.Depot depot;
        private int flag = 0;
        public AddDepotpositionForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.Depot.PRO_Id, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.requireValueExceptions.Add(Model.DepotPosition.PROPERTY_ID + "aa", new AA(Properties.Resources.RequireDataForId, this.gridControl1));
            this.invalidValueExceptions.Add(Model.Depot.PRO_Id, new AA(Properties.Resources.EntityExists, this.textEditId));
            this.invalidValueExceptions.Add(Model.DepotPosition.PROPERTY_ID + "aa", new AA(Properties.Resources.EntityExists, this.gridControl1));
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
            this.action = "insert";
        }


        #region treelist绑定数据源
        void band()
        {
            this.treeList1.ClearNodes();
            IList<Model.Depot> list = depotManager.Select();
            foreach (Model.Depot depot in list)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { depot.DepotName }, null, depot.DepotId);
                foreach (Model.DepotPosition dp in this.depotPositionManager.Select(depot))
                {
                    treeList1.AppendNode(new object[] { dp.Id }, treeNode, null);
                }
            }
        }
        #endregion

        private void AddDepotpositionForm_Load(object sender, EventArgs e)
        {
          

            this.Visibles();
            //band();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (flag == 0)
            {
                if (e.Node.Tag != null)
                {
                    this.action = "view";
                    depot = this.depotManager.SelectByDepot(e.Node.Tag.ToString());
                    //this.bindingSource1.DataSource = this.depotPositionManager.Select(depot.DepotId);
                    this.Refresh();
                }
            }
        }

        #region Override

        protected override void AddNew()
        {
            this.depot = new Model.Depot();
            this.depot.DepotId = Guid.NewGuid().ToString();
            this.depot.Details = new List<Model.DepotPosition>();
            this.depot.IsFinalProductDepot = false;

        }

        protected override void Delete()
        {
            if (this.depot == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.depotManager.Delete(this.depot);
            this.depot = this.depotManager.GetNext(this.depot);
            if (this.depot == null)
            {
                this.depot = this.depotManager.GetLast();
            }
            flag = 1;
            band();
        }

        protected override bool HasRows()
        {
            return this.depotManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.depotManager.HasRowsAfter(this.depot);
        }

        protected override bool HasRowsPrev()
        {
            return this.depotManager.HasRowsBefore(this.depot);
        }

        protected override void IMECtrl()
        {
            //Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditUnitGroupName }, new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit[] { this.repositoryItemSpinEdit1 }, new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit[] { this.repositoryItemTextEdit1 });
        }

        protected override void MoveFirst()
        {
            this.depot = this.depotManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.depot = this.depotManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.Depot dt = this.depotManager.GetNext(this.depot);
            if (depot == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.depot = dt;
        }

        protected override void MovePrev()
        {
            Model.Depot dt = this.depotManager.GetPrev(this.depot);
            if (dt == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.depot = dt;
        }


        public override void Refresh()
        {

            if (this.depot == null)
            {
                this.depot = new Book.Model.Depot();
                this.depot.Details = new List<Model.DepotPosition>();
                
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this.depot = this.depotManager.GetDetails(depot.DepotId);

                }
            }
            if (this.action == "insert" || (this.action == "update" && this.depot.Details.Count == 0))
            {
                Model.DepotPosition detail = new Model.DepotPosition();
                detail.DepotPositionId = Guid.NewGuid().ToString();
                detail.Depot = this.depot;
                this.depot.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }

            this.textEditId.Text = this.depot.Id;
            this.textEditDepotName.Text = this.depot.DepotName;
            this.textEditKeeper.Text = this.depot.StoreKeeper;
            this.textEditFax.Text = this.depot.Fax;
            this.textEditTelphone.Text = this.depot.Telphone;
            this.textEditDepotAddress.Text = this.depot.DepotAddress;
            this.textEditBarCode.Text = this.depot.DepotCode;
            this.textEditDepotDescription.Text = this.depot.DepotDescription;
            this.comboBoxEditDepot.Text = this.depot.DepotTypeName;
             this.radioGroup1.SelectedIndex =this.depot.IsFinalProductDepot==true?1:0 ;
            this.bindingSource1.DataSource = this.depot.Details;
            this.gridControl1.RefreshDataSource();
            flag = 1;
            if (this.action != "view")
                band();

            this.textEditDepotName.EditValue = this.depot.DepotName;
            flag = 0;
            switch (this.action)
            {
                case "insert":
                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditDepotName.Properties.ReadOnly = false;
                    this.textEditKeeper.Properties.ReadOnly = false;
                    this.textEditFax.Properties.ReadOnly = false;
                    this.textEditTelphone.Properties.ReadOnly = false;
                    this.textEditDepotAddress.Properties.ReadOnly = false;
                    this.textEditDepotDescription.Properties.ReadOnly = false;
                    this.textEditBarCode.Properties.ReadOnly = false;
                    this.comboBoxEditDepot.Properties.ReadOnly = false;
                    this.comboBoxEditDepot.Properties.Buttons[0].Visible = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditDepotName.Properties.ReadOnly = false;
                    this.textEditKeeper.Properties.ReadOnly = false;
                    this.textEditFax.Properties.ReadOnly = false;
                    this.textEditTelphone.Properties.ReadOnly = false;
                    this.textEditDepotAddress.Properties.ReadOnly = false;
                    this.textEditDepotDescription.Properties.ReadOnly = false;
                    this.textEditBarCode.Properties.ReadOnly = false;
                    this.comboBoxEditDepot.Properties.ReadOnly = false;
                    this.comboBoxEditDepot.Properties.Buttons[0].Visible = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "view":
                    this.textEditId.Properties.ReadOnly = true;
                    this.textEditDepotName.Properties.ReadOnly = true;
                    this.textEditKeeper.Properties.ReadOnly = true;
                    this.textEditFax.Properties.ReadOnly = true;
                    this.textEditTelphone.Properties.ReadOnly = true;
                    this.textEditDepotAddress.Properties.ReadOnly = true;
                    this.textEditDepotDescription.Properties.ReadOnly = true;
                    this.textEditBarCode.Properties.ReadOnly = true;
                    this.comboBoxEditDepot.Properties.ReadOnly = true;
                    this.comboBoxEditDepot.Properties.Buttons[0].Visible = false;
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            this.depot.Id = this.textEditId.Text;
            this.depot.DepotName = this.textEditDepotName.Text;
            this.depot.DepotAddress = this.textEditDepotAddress.Text;
            this.depot.DepotCode = this.textEditBarCode.Text;
            this.depot.Fax = this.textEditFax.Text;
            this.depot.StoreKeeper = this.textEditKeeper.Text;
            this.depot.Telphone = this.textEditTelphone.Text;
            this.depot.DepotDescription = this.textEditDepotDescription.Text;
            this.depot.DepotTypeName = this.comboBoxEditDepot.Text;
            this.depot.IsFinalProductDepot = this.radioGroup1.SelectedIndex == 0 ? false : true; 
            switch (this.action)
            {
                case "insert":
                    this.depotManager.Insert(this.depot);
                    break;
                case "update":
                    this.depotManager.Update(this.depot);
                    break;
                default:
                    break;
            }
            flag = 1;
            band();
        }

        #endregion

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    this.simpleButtonAppend.PerformClick();

                }
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButtonRemover.PerformClick();
                }
                this.gridControl1.RefreshDataSource();

            }
        }

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
                Model.DepotPosition detail = new Model.DepotPosition();
                detail.DepotPositionId = Guid.NewGuid().ToString();
                detail.Depot = this.depot;
                this.depot.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
        }

        private void simpleButtonRemover_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.depot.Details.Remove(this.bindingSource1.Current as Book.Model.DepotPosition);

                if (this.depot.Details.Count == 0)
                {
                    Model.DepotPosition detail = new Model.DepotPosition();
                    detail.DepotPositionId = Guid.NewGuid().ToString();
                    detail.Depot = this.depot;
                    this.depot.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);      
                }
                this.gridControl1.RefreshDataSource();
           
            }
        }

    }
}