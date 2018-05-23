using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.StockLimitations
{
    public partial class AssemblySiteInventoryForm : Settings.BasicData.BaseEditForm
    {
        private Model.AssemblySiteInventory _assemblySiteInventory;
        private BL.AssemblySiteInventoryManager manager = new Book.BL.AssemblySiteInventoryManager();
        int isLast = 0;

        public AssemblySiteInventoryForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.AssemblySiteInventory.PRO_InvoiceDate, new AA(Properties.Resources.DateNotNull, this.date_Inventory));

            this.ncc_Employee.Choose = new BasicData.Employees.ChooseEmployee();
            this.action = "view";
        }

        public AssemblySiteInventoryForm(Model.AssemblySiteInventory model)
            : this()
        {
            this._assemblySiteInventory = model;
            this.isLast = 1;
        }

        public AssemblySiteInventoryForm(Model.AssemblySiteInventory model, string action)
            : this()
        {
            this._assemblySiteInventory = model;
            this.isLast = 1;
            this.action = action;
        }

        protected override void AddNew()
        {
            this._assemblySiteInventory = new Book.Model.AssemblySiteInventory();
            this._assemblySiteInventory.AssemblySiteInventoryId = this.manager.GetId();
            this._assemblySiteInventory.Employee = BL.V.ActiveOperator.Employee;
            this.action = "insert";
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            if (this.date_Inventory.EditValue != null)
                this._assemblySiteInventory.InvoiceDate = this.date_Inventory.DateTime;
            this._assemblySiteInventory.EmployeeId = (this.ncc_Employee.EditValue == null ? null : (this.ncc_Employee.EditValue as Model.Employee).EmployeeId);
            this._assemblySiteInventory.Note = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(_assemblySiteInventory);
                    break;
                case "update":
                    this.manager.Update(_assemblySiteInventory);
                    break;
            }
        }

        public override void Refresh()
        {
            if (_assemblySiteInventory == null)
                this.AddNew();
            else if (this.action == "view")
            {
                this._assemblySiteInventory = this.manager.GetDetail(this._assemblySiteInventory.AssemblySiteInventoryId);
            }
            if (this._assemblySiteInventory.InvoiceState.HasValue && this._assemblySiteInventory.InvoiceState.Value)
            {
                this.bar_GenerateInvoice.Enabled = false;

                if (this.action == "update")
                {
                    this.action = "view";
                    MessageBox.Show("该单据已经生成组装现场盘点差异单，请勿修改！", this.Text, MessageBoxButtons.OK);
                    return;
                }
            }
            else
                this.bar_GenerateInvoice.Enabled = true;

            this.txt_ID.EditValue = this._assemblySiteInventory.AssemblySiteInventoryId;
            this.date_Inventory.EditValue = this._assemblySiteInventory.InvoiceDate;
            this.ncc_Employee.EditValue = this._assemblySiteInventory.Employee;
            this.txt_Note.EditValue = this._assemblySiteInventory.Note;


            this.bindingSourceDetail.DataSource = _assemblySiteInventory.Details;
            this.gridControl1.RefreshDataSource();
            base.Refresh();

            switch (this.action)
            {
                case "insert":
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }
            this.txt_ID.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.AssemblySiteInventory model = this.manager.GetNext(_assemblySiteInventory);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _assemblySiteInventory = model;
        }

        protected override void MovePrev()
        {
            Model.AssemblySiteInventory model = this.manager.GetPrev(_assemblySiteInventory);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _assemblySiteInventory = model;
        }

        protected override void MoveFirst()
        {
            _assemblySiteInventory = this.manager.Get(this.manager.GetFirst() == null ? "" : this.manager.GetFirst().AssemblySiteInventoryId);
        }

        protected override void MoveLast()
        {
            if (this.isLast == 1)
            {
                this.isLast = 0;
                return;
            }
            _assemblySiteInventory = this.manager.Get(this.manager.GetLast() == null ? "" : this.manager.GetLast().AssemblySiteInventoryId);
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(_assemblySiteInventory);
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(_assemblySiteInventory);
        }

        protected override void Delete()
        {
            if (this._assemblySiteInventory.InvoiceState.HasValue && _assemblySiteInventory.InvoiceState.Value)
            {
                throw new Exception("该单据已经生成组装现场盘点差异单，请勿删除！");
            }

            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.manager.Delete(_assemblySiteInventory.AssemblySiteInventoryId);
                _assemblySiteInventory = this.manager.GetNext(_assemblySiteInventory);
                if (_assemblySiteInventory == null)
                {
                    _assemblySiteInventory = this.manager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new AssemblySiteInventoryRO(this._assemblySiteInventory);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.AssemblySiteInventoryDetail detail = null;
                if (Invoices.ChooseProductForm.ProductList != null || Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in Invoices.ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.AssemblySiteInventoryDetail();
                        detail.AssemblySiteInventoryDetailId = Guid.NewGuid().ToString();
                        detail.AssemblySiteInventoryId = this._assemblySiteInventory.AssemblySiteInventoryId;
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        this._assemblySiteInventory.Details.Add(detail);
                    }
                }
                else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.AssemblySiteInventoryDetail();
                    detail.AssemblySiteInventoryDetailId = Guid.NewGuid().ToString();
                    detail.AssemblySiteInventoryId = this._assemblySiteInventory.AssemblySiteInventoryId;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    this._assemblySiteInventory.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this._assemblySiteInventory.Details.Remove(this.bindingSourceDetail.Current as Book.Model.AssemblySiteInventoryDetail);

                this.gridControl1.RefreshDataSource();
            }
        }

        private void bar_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssemblySiteInventoryList f = new AssemblySiteInventoryList();
            f.ShowDialog(this);
        }

        private void bar_GenerateInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssemblySiteDifferenceForm f = new AssemblySiteDifferenceForm(this._assemblySiteInventory);
            f.ShowDialog(this);
        }
    }
}