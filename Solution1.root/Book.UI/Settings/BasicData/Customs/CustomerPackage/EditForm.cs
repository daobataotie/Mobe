using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;
using DevExpress.XtraTreeList.Nodes;

namespace Book.UI.Settings.BasicData.Customs.CustomerPackage
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        int flag = 0;
        protected BL.ProductUnitManager productUnitManager = new Book.BL.ProductUnitManager();
        Model.CustomerPackage _customerPackage = new Book.Model.CustomerPackage();
        Model.CustomerPackageDetail _customerPackageDetail = new Book.Model.CustomerPackageDetail();

        BL.CustomerPackageManager CustomerPackageManager = new Book.BL.CustomerPackageManager();
        BL.CustomerPackageDetailManager CustomerPackageDetailManager = new Book.BL.CustomerPackageDetailManager();

        BL.CustomerManager customerManager = new Book.BL.CustomerManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        //临时客户在  点击treelist 包装添加时  赋客户值
        private Model.Customer customer = new Book.Model.Customer();
        public EditForm()
        {
            //flag1 = 1;
            flag = 1;
            InitializeComponent();
            this.requireValueExceptions.Add(Model.CustomerPackage.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditCustomerPackageId));
            this.invalidValueExceptions.Add(Model.CustomerPackage.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditCustomerPackageId));
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.action = "insert";
        }
        //public EidtForm(Model.CustomerPackage CustomerPackage):this()
        //{
        //    // this =CustomerPackage;


        //    //this.CustomerPackage.detail = this.proceduresManager.Select(this.CustomerPackage);
        //    //this.bindingSourceProcedures.DataSource = this.proceduresManager.Select();
        //   // this.action = "update";
        //}
        //public EidtForm(Model.CustomerPackage CustomerPackage, string action):this()
        //{
        //    //this.CustomerPackage = CustomerPackage;

        //    //this.CustomerPackage.detail = this.proceduresManager.Select(this.CustomerPackage);
        //    //this.bindingSourceProcedures.DataSource = this.proceduresManager.Select();
        //    //this.action = action;
        //}
        #region Override
        //  int flag1 = 0;
        protected override void AddNew()
        {
            //flag1 = 1;


            this._customerPackage = new Model.CustomerPackage();

            if (customer != null)
            {
                this._customerPackage.Customer = customer;
            }

            this._customerPackage.CustomerPackageId = Guid.NewGuid().ToString();

            this._customerPackage.detail = new List<Model.CustomerPackageDetail>();

            // if (this.action == "insert")
            {
                Model.CustomerPackageDetail detail = new Model.CustomerPackageDetail();
                detail.CustomerPackageDetailId = Guid.NewGuid().ToString();
                detail.Product = new Book.Model.Product();
                this._customerPackage.detail.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }
        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._customerPackage.Id = this.textEditCustomerPackageId.Text;
            this._customerPackage.Customer = this.newChooseCustomer.EditValue as Model.Customer;
            if (this._customerPackage.Customer != null)
                this._customerPackage.CustomerId = (this._customerPackage.Customer).CustomerId;
           
            switch (this.action)
            {
                case "insert":
                    this.CustomerPackageManager.Insert(this._customerPackage);
                    break;
                case "update":
                    this.CustomerPackageManager.Update(this._customerPackage);
                    break;
                default:
                    break;
            }
            flag = 1;
            band();
        }


        public override void Refresh()
        {
            //flag = 0;
            if (this._customerPackage == null)
            {
                //this._customerPackage = new Book.Model.CustomerPackage();
                //this.action = "insert";
                this.AddNew();
            }

            if (this.action != "insert")
            {
                this._customerPackage.detail = this.CustomerPackageDetailManager.GetByPackageId(this._customerPackage.CustomerPackageId);


            }
            if (this.action == "update")
            {
                if (this._customerPackage.detail.Count == 0)
                {
                    Model.CustomerPackageDetail detail = new Model.CustomerPackageDetail();
                    detail.CustomerPackageDetailId = Guid.NewGuid().ToString();
                    this._customerPackage.detail.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
            }

            this.textEditCustomerPackageId.Text = this._customerPackage.Id;
            this.newChooseCustomer.EditValue = this._customerPackage.Customer;
            this.bindingSource1.DataSource = this._customerPackage.detail;
            
            base.Refresh();
        }
        protected override void Delete()
        {
            if (this._customerPackage == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.CustomerPackageManager.Delete(this._customerPackage.CustomerPackageId);


                foreach (TreeListNode listNode in this.treeList1.Nodes)
                {

                    for (int i = 0; i < listNode.Nodes.Count; i++)
                    {
                        if (listNode.Nodes[i].Tag.ToString() == this._customerPackage.CustomerPackageId && listNode.Nodes[i].ParentNode != null)
                        {
                            listNode.Nodes.Remove(listNode.Nodes[i]);
                        }
                    }
                }
                this._customerPackage = this.CustomerPackageManager.GetNext(this._customerPackage);
                if (this._customerPackage == null)
                {
                    this._customerPackage = this.CustomerPackageManager.GetLast();
                }
            }
            catch
            {
                throw;
            }

            return;

        }
        protected override void MoveFirst()
        {
            this._customerPackage = this.CustomerPackageManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.CustomerPackage CustomerPackage = this.CustomerPackageManager.GetPrev(this._customerPackage);
            if (CustomerPackage == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._customerPackage = CustomerPackage;
        }
        protected override void MoveLast()
        {
            //  if (CustomerPackage == null)
            {
                this._customerPackage = this.CustomerPackageManager.GetLast();
            }
        }
        protected override void MoveNext()
        {
            Model.CustomerPackage CustomerPackage = this.CustomerPackageManager.GetNext(this._customerPackage);
            if (CustomerPackage == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._customerPackage = CustomerPackage;
        }
        protected override bool HasRows()
        {
            return this.CustomerPackageManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.CustomerPackageManager.HasRowsAfter(this._customerPackage);
        }
        protected override bool HasRowsPrev()
        {
            return this.CustomerPackageManager.HasRowsBefore(this._customerPackage);
        }
        protected override void IMECtrl()
        {
            //Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textCustomerPackagename, this.textEditDescription });
        }
        #endregion


        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }
        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceProduct.DataSource = this.productManager.Select();
            band();
        }
        private void band()
        {
            this.treeList1.ClearNodes();


            foreach (Model.Customer customer in this.customerManager.Select())
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { customer.CustomerShortName }, null, customer.CustomerId);

                foreach (Model.CustomerPackage customerPackage in this.CustomerPackageManager.Select(customer))
                {

                    treeList1.AppendNode(new object[] { customerPackage.Id }, treeNode, customerPackage.CustomerPackageId);

                }


            }

        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (flag == 1)
            {
                flag = 0;
                return;
            }

            if (flag == 0)
            {
                // if (flag1 == 0)
                {
                    if (e.Node != null && e.Node.ParentNode != null)
                    {
                        this._customerPackage = CustomerPackageManager.Get(e.Node.Tag.ToString());
                        if (this._customerPackage != null)
                        {
                            this._customerPackage.detail = this.CustomerPackageDetailManager.GetByPackageId(this._customerPackage.CustomerPackageId);


                            this.bindingSource1.DataSource = this._customerPackage.detail;
                            this.action = "view";
                            this.Refresh();
                        }
                    }
                    if (e.Node != null && e.Node.ParentNode == null)
                    {
                        customer = customerManager.Get(e.Node.Tag.ToString());

                        if (this.action == "insert")
                        {
                            this._customerPackage.Customer = customer;
                            this.newChooseCustomer.EditValue = this._customerPackage.Customer as Model.Customer;


                        }

                    }

                }
                //  flag1 = 0;
            }

        }

        private void gridView1_CellValueChanging_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumnProductId || e.Column == this.gridColumn2)
            {
                Model.CustomerPackageDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.CustomerPackageDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    if (p != null)
                    {

                        detail.CustomerPackageDetailId = Guid.NewGuid().ToString();
                        detail.ConsumeRate = 0;
                        detail.Quantity = 1;
                        detail.CustomerPackage = this._customerPackage;
                        if (this._customerPackage != null)
                            detail.CustomerPackageId = this._customerPackage.CustomerPackageId;
                        detail.Description = "";
                        detail.ExpiringDate = global::Helper.DateTimeParse.EndDate;
                        detail.EffectsDate = DateTime.Now; ;
                        detail.Product = p;
                        detail.ProductId = p.ProductId;
                        //  detail.InvoiceProductUnit = p.DepotUnit == null ? "" : p.DepotUnit.CnName;
                    }
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_KeyDown_1(object sender, KeyEventArgs e)
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
                // this.gridControl1.RefreshDataSource();
            }
        }
        private void simpleButtonRemover_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this._customerPackage.detail.Remove(this.bindingSource1.Current as Book.Model.CustomerPackageDetail);
                if (this._customerPackage.detail.Count == 0)
                {
                    Model.CustomerPackageDetail detail = new Model.CustomerPackageDetail();
                    detail.CustomerPackageDetailId = Guid.NewGuid().ToString();
                    detail.Product = new Book.Model.Product();
                    this._customerPackage.detail.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }
        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {

                Model.CustomerPackageDetail detail = new Model.CustomerPackageDetail();
                detail.CustomerPackageDetailId = Guid.NewGuid().ToString();
                this._customerPackage.detail.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this._customerPackage.detail.Count == 1 && string.IsNullOrEmpty(this._customerPackage.detail[0].Product.ProductId))
                    this._customerPackage.detail.Remove(this._customerPackage.detail[0]);

                Model.CustomerPackageDetail detail = new Book.Model.CustomerPackageDetail();
                detail.CustomerPackageDetailId = Guid.NewGuid().ToString();
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.ConsumeRate = 0;
                detail.Quantity = 1;
                detail.CustomerPackage = this._customerPackage;
                if (this._customerPackage != null)
                    detail.CustomerPackageId = this._customerPackage.CustomerPackageId;
                detail.Description = "";
                detail.ExpiringDate = global::Helper.DateTimeParse.EndDate;
                detail.EffectsDate = DateTime.Now; ;
                this._customerPackage.detail.Add(detail);
                this.gridControl1.RefreshDataSource();

                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == this.gridColumn1.Name)
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.CustomerPackageDetail).Product;

                    this.repositoryItemComboBox1.Items.Clear();

                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        IList<Model.ProductUnit> units = productUnitManager.Select(p.BasedUnitGroup);

                        foreach (Model.ProductUnit ut in units)
                        {
                            this.repositoryItemComboBox1.Items.Add(ut.CnName);
                        }
                    }
                }
            }
        }



    }
}