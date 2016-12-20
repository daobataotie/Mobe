using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCSampling
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PCSampling _PCSampling = null;
        BL.PCSamplingManager manager = new Book.BL.PCSamplingManager();

        int LastFlag = 0;
        public EditForm()
        {
            InitializeComponent();

            this.action = "view";

            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccEmployee1.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();

            this.invalidValueExceptions.Add(Model.PCSampling.PRO_PCSamplingDate, new AA("日期不能为空", this.date_PCSamplingDate));
            this.invalidValueExceptions.Add(Model.PCSampling.PRO_PronoteHeaderId, new AA("加工单不能为空", this.txt_PronoteHeaderId));

            #region LookUpEdit
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            //dt.Columns.Add("name", typeof(string));
            DataRow dr;
            dr = dt.NewRow();
            //dr[0] = "";
            dr[0] = " ";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "0";
            dr[0] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "1";
            dr[0] = "×";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "2";
            dr[0] = "△";
            dt.Rows.Add(dr);

            for (int i = 0; i < this.gridView1.Columns.Count; i++)
            {
                if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).NullText = "";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id",25, "标识"),
                     });
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "id";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "id";
                }
            }
            #endregion

        }

        public EditForm(string PCSamplingId)
            : this()
        {
            this._PCSampling = this.manager.Get(PCSamplingId);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCSampling model)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._PCSampling = model;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCSampling model, string action)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._PCSampling = model;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._PCSampling = new Book.Model.PCSampling();
            this._PCSampling.PCSamplingId = this.manager.GetId();
            this._PCSampling.PCSamplingDate = DateTime.Now;
            this._PCSampling.Employee = BL.V.ActiveOperator.Employee;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._PCSampling);
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._PCSampling);
        }

        protected override void MoveFirst()
        {
            this._PCSampling = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._PCSampling = this.manager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.PCSampling p = this.manager.GetPrev(this._PCSampling);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCSampling = p;
        }

        protected override void MoveNext()
        {
            Model.PCSampling p = this.manager.GetNext(this._PCSampling);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCSampling = p;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            this._PCSampling.PCSamplingId = this.txt_PCSamplingId.Text;
            if (this.date_PCSamplingDate.EditValue != null)
                this._PCSampling.PCSamplingDate = this.date_PCSamplingDate.DateTime;
            else
                this._PCSampling.PCSamplingDate = null;
            this._PCSampling.PronoteHeaderId = this.txt_PronoteHeaderId.Text;
            this._PCSampling.InvoiceCusId = this.txt_InvoiceCusId.Text;
            this._PCSampling.EmployeeId = this.nccEmployee.EditValue == null ? null : (this.nccEmployee.EditValue as Model.Employee).EmployeeId;
            this._PCSampling.CustomerId = this.nccCustomer.EditValue == null ? null : (this.nccCustomer.EditValue as Model.Customer).CustomerId;
            this._PCSampling.Model = this.txt_Model.Text;
            this._PCSampling.Note = this.txt_Note.Text;

            this._PCSampling.Employee1Id = (this.nccEmployee1.EditValue as Model.Employee) == null ? null : (this.nccEmployee1.EditValue as Model.Employee).EmployeeId;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._PCSampling);
                    break;
                case "update":
                    this.manager.Update(this._PCSampling);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._PCSampling == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._PCSampling = this.manager.GetDetail(this._PCSampling.PCSamplingId);
            }

            this.txt_PCSamplingId.Text = this._PCSampling.PCSamplingId;
            this.date_PCSamplingDate.EditValue = this._PCSampling.PCSamplingDate;
            this.txt_PronoteHeaderId.Text = this._PCSampling.PronoteHeaderId;
            this.txt_InvoiceCusId.Text = this._PCSampling.InvoiceCusId;
            this.nccEmployee.EditValue = this._PCSampling.Employee;
            this.nccCustomer.EditValue = this._PCSampling.Customer;
            this.txt_Model.Text = this._PCSampling.Model;
            this.txt_Note.Text = this._PCSampling.Note;
            this.nccEmployee1.EditValue = this._PCSampling.Employee1;
            this.bindingSourceDetail.DataSource = this._PCSampling.Details;

            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
            }

            this.txt_PronoteHeaderId.Properties.ReadOnly = true;
            this.txt_PCSamplingId.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._PCSampling);
        }

        protected override void Delete()
        {
            if (this._PCSampling == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.PCSampling model = this.manager.GetNext(this._PCSampling);
                this.manager.Delete(this._PCSampling.PCSamplingId);
                if (model == null)
                    this._PCSampling = this.manager.GetLast();
                else
                    this._PCSampling = model;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            //Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    if (Invoices.ChooseProductForm.ProductList != null && Invoices.ChooseProductForm.ProductList.Count > 0)
            //    {
            //        foreach (Model.Product product in Invoices.ChooseProductForm.ProductList)
            //        {
            //            Model.PCSamplingDetail detail = new Book.Model.PCSamplingDetail();
            //            detail.PCSamplingDetailId = Guid.NewGuid().ToString();
            //            detail.PCSamplingId = this._PCSampling.PCSamplingId;
            //            detail.PCSamplingDetailDate = DateTime.Now;
            //            detail.Product = product;
            //            detail.ProductId = product.ProductId;
            //            this._PCSampling.Details.Add(detail);
            //            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
            //        }
            //    }
            //    else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
            //    {
            //        Model.PCSamplingDetail detail = new Book.Model.PCSamplingDetail();
            //        detail.PCSamplingDetailId = Guid.NewGuid().ToString();
            //        detail.PCSamplingId = this._PCSampling.PCSamplingId;
            //        detail.PCSamplingDetailDate = DateTime.Now;
            //        detail.Product = f.SelectedItem as Model.Product;
            //        detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
            //        this._PCSampling.Details.Add(detail);
            //        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
            //    }
            //}
            if (this._PCSampling.Details == null || this._PCSampling.Details.Count == 0)
            {
                MessageBox.Show("请先从加工单拉取第一条测试内容。", this.Text, MessageBoxButtons.OK);
                return;
            }
            else
            {
                Model.PCSamplingDetail detail = new Book.Model.PCSamplingDetail();
                detail.PCSamplingDetailId = Guid.NewGuid().ToString();
                detail.PCSamplingId = this._PCSampling.PCSamplingId;
                detail.PCSamplingDetailDate = DateTime.Now;
                detail.Product = this._PCSampling.Details[0].Product;
                detail.ProductId = this._PCSampling.Details[0].ProductId;
                this._PCSampling.Details.Add(detail);
                this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);

            }

            this.gridControl1.RefreshDataSource();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            f.ShowDialog(this);
            this.Refresh();
        }

        private void barPronoteHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(null, 0);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader currentModel = pronoForm.SelectItem;
                if (currentModel != null)
                {
                    this.txt_PronoteHeaderId.Text = currentModel.PronoteHeaderID;
                    //this.nccCustomer.EditValue = currentModel.InvoiceXO.Customer;
                    Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(currentModel.InvoiceXOId);
                    if (xo != null)
                    {
                        this.nccCustomer.EditValue = xo.Customer;
                        this.txt_InvoiceCusId.Text = xo.CustomerInvoiceXOId;
                    }
                    //this.txt_Model.Text = (currentModel.Product) == null ? "" : (currentModel.Product).CustomerProductName;
                    Model.Product p = new BL.ProductManager().Get(currentModel.ProductId);
                    if (p != null)
                        this.txt_Model.Text = p.CustomerProductName;

                    Model.PCSamplingDetail detail = new Book.Model.PCSamplingDetail();
                    detail.PCSamplingDetailId = Guid.NewGuid().ToString();
                    detail.PCSamplingDetailDate = DateTime.Now;
                    detail.PCSamplingId = this._PCSampling.PCSamplingId;
                    detail.Product = p;
                    detail.ProductId = currentModel.ProductId;
                    this._PCSampling.Details.Add(detail);
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);

                    this.gridControl1.RefreshDataSource();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        protected override string tableCode()
        {
            return "PCSampling" + "," + this._PCSampling.PCSamplingId;
        }


    }
}