using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCMaterialCheck
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PCMaterialCheck _PCMaterialCheck = null;
        BL.PCMaterialCheckManager manager = new Book.BL.PCMaterialCheckManager();

        int LastFlag = 0;
        public EditForm()
        {
            InitializeComponent();

            this.action = "view";

            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            this.invalidValueExceptions.Add(Model.PCMaterialCheck.PRO_PCMaterialCheckDate, new AA("日期不能为空", this.date_PCMaterialCheckDate));
            this.invalidValueExceptions.Add(Model.PCMaterialCheck.PRO_InvoiceCOId, new AA("采购单不能为空", this.txt_InvoiceCOId));

            #region LookUpEdit
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));
            DataRow dr;
            dr = dt.NewRow();
            dr[0] = "";
            dr[1] = " ";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "×";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "△";
            dt.Rows.Add(dr);

            for (int i = 0; i < this.gridView1.Columns.Count; i++)
            {
                if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).NullText = "";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name",25, "标识"),
                     });
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "name";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "id";
                }
            }
            #endregion

        }

        public EditForm(string PCMaterialCheckId)
            : this()
        {
            this._PCMaterialCheck = this.manager.Get(PCMaterialCheckId);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCMaterialCheck model)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._PCMaterialCheck = model;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCMaterialCheck model, string action)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._PCMaterialCheck = model;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._PCMaterialCheck = new Book.Model.PCMaterialCheck();
            this._PCMaterialCheck.PCMaterialCheckId = this.manager.GetId();
            this._PCMaterialCheck.PCMaterialCheckDate = DateTime.Now;
            this._PCMaterialCheck.Employee = BL.V.ActiveOperator.Employee;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._PCMaterialCheck);
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._PCMaterialCheck);
        }

        protected override void MoveFirst()
        {
            this._PCMaterialCheck = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._PCMaterialCheck = this.manager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.PCMaterialCheck p = this.manager.GetPrev(this._PCMaterialCheck);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCMaterialCheck = p;
        }

        protected override void MoveNext()
        {
            Model.PCMaterialCheck p = this.manager.GetNext(this._PCMaterialCheck);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCMaterialCheck = p;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            this._PCMaterialCheck.PCMaterialCheckId = this.txt_PCMaterialCheckId.Text;
            if (this.date_PCMaterialCheckDate.EditValue != null)
                this._PCMaterialCheck.PCMaterialCheckDate = this.date_PCMaterialCheckDate.DateTime;
            else
                this._PCMaterialCheck.PCMaterialCheckDate = null;
            this._PCMaterialCheck.InvoiceCOId = this.txt_InvoiceCOId.Text;
            this._PCMaterialCheck.InvoiceCusId = this.txt_InvoiceCusId.Text;
            this._PCMaterialCheck.EmployeeId = this.nccEmployee.EditValue == null ? null : (this.nccEmployee.EditValue as Model.Employee).EmployeeId;
            this._PCMaterialCheck.Note = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._PCMaterialCheck);
                    break;
                case "update":
                    this.manager.Update(this._PCMaterialCheck);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._PCMaterialCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._PCMaterialCheck = this.manager.GetDetail(this._PCMaterialCheck.PCMaterialCheckId);
            }

            this.txt_PCMaterialCheckId.Text = this._PCMaterialCheck.PCMaterialCheckId;
            this.date_PCMaterialCheckDate.EditValue = this._PCMaterialCheck.PCMaterialCheckDate;
            this.txt_InvoiceCOId.Text = this._PCMaterialCheck.InvoiceCOId;
            this.txt_InvoiceCusId.Text = this._PCMaterialCheck.InvoiceCusId;
            this.nccEmployee.EditValue = this._PCMaterialCheck.Employee;
            this.txt_Note.Text = this._PCMaterialCheck.Note;
            this.bindingSourceDetail.DataSource = this._PCMaterialCheck.Details;

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

            this.txt_InvoiceCOId.Properties.ReadOnly = true;
            this.txt_PCMaterialCheckId.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._PCMaterialCheck);
        }

        protected override void Delete()
        {
            if (this._PCMaterialCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.PCMaterialCheck model = this.manager.GetNext(this._PCMaterialCheck);
                this.manager.Delete(this._PCMaterialCheck.PCMaterialCheckId);
                if (model == null)
                    this._PCMaterialCheck = this.manager.GetLast();
                else
                    this._PCMaterialCheck = model;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (Invoices.ChooseProductForm.ProductList != null && Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in Invoices.ChooseProductForm.ProductList)
                    {
                        Model.PCMaterialCheckDetail detail = new Book.Model.PCMaterialCheckDetail();
                        detail.PCMaterialCheckDetailId = Guid.NewGuid().ToString();
                        detail.PCMaterialCheckId = this._PCMaterialCheck.PCMaterialCheckId;
                        detail.CheckDate = DateTime.Now;
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        this._PCMaterialCheck.Details.Add(detail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    Model.PCMaterialCheckDetail detail = new Book.Model.PCMaterialCheckDetail();
                    detail.PCMaterialCheckDetailId = Guid.NewGuid().ToString();
                    detail.PCMaterialCheckId = this._PCMaterialCheck.PCMaterialCheckId;
                    detail.CheckDate = DateTime.Now;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    this._PCMaterialCheck.Details.Add(detail);
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }
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

        private void barInvoiceCO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                Invoices.CG.CGForm form = new Invoices.CG.CGForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.key != null && form.key.Count > 0)
                    {
                        this.txt_InvoiceCOId.Text = form.key[0].InvoiceId;
                        this.txt_InvoiceCusId.Text = form.key[0].Invoice.InvoiceCustomXOId;
                        foreach (Model.InvoiceCODetail item in form.key)
                        {
                            Model.PCMaterialCheckDetail detail = new Book.Model.PCMaterialCheckDetail();
                            detail.PCMaterialCheckDetailId = Guid.NewGuid().ToString();
                            detail.PCMaterialCheckId = this._PCMaterialCheck.PCMaterialCheckId;
                            detail.CheckDate = DateTime.Now;
                            detail.Product = item.Product;
                            detail.ProductId = item.ProductId;
                            this._PCMaterialCheck.Details.Add(detail);
                            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                        }

                        this.gridControl1.RefreshDataSource();
                    }
                }
                form.Dispose();
                GC.Collect();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            f.ShowDialog(this);
            this.Refresh();
        }

    }
}