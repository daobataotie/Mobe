using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCIncomingCheck
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PCIncomingCheck _PCIncomingCheck = null;
        BL.PCIncomingCheckManager manager = new Book.BL.PCIncomingCheckManager();

        int LastFlag = 0;
        public EditForm()
        {
            InitializeComponent();

            this.action = "view";

            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            //this.invalidValueExceptions.Add(Model.PCSampling.PRO_PCSamplingDate, new AA("日期不能为空", this.date_PurchaseDate));
            //this.invalidValueExceptions.Add(Model.PCSampling.PRO_PronoteHeaderId, new AA("采购单不能为空", this.txt_InvoiceCOId));

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

        public EditForm(string PCIncomingCheckId)
            : this()
        {
            this._PCIncomingCheck = this.manager.Get(PCIncomingCheckId);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCIncomingCheck model)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._PCIncomingCheck = model;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCIncomingCheck model, string action)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._PCIncomingCheck = model;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._PCIncomingCheck = new Book.Model.PCIncomingCheck();
            this._PCIncomingCheck.PCIncomingCheckId = this.manager.GetId();
            this._PCIncomingCheck.CheckDate = DateTime.Now;
            this._PCIncomingCheck.Employee = BL.V.ActiveOperator.Employee;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._PCIncomingCheck);
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._PCIncomingCheck);
        }

        protected override void MoveFirst()
        {
            this._PCIncomingCheck = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._PCIncomingCheck = this.manager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.PCIncomingCheck p = this.manager.GetPrev(this._PCIncomingCheck);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCIncomingCheck = p;
        }

        protected override void MoveNext()
        {
            Model.PCIncomingCheck p = this.manager.GetNext(this._PCIncomingCheck);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCIncomingCheck = p;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            this._PCIncomingCheck.PCIncomingCheckId = this.txt_Id.Text;
            if (this.date_PurchaseDate.EditValue != null)
                this._PCIncomingCheck.PurchaseDate = this.date_PurchaseDate.DateTime;
            else
                this._PCIncomingCheck.PurchaseDate = null;
            if (this.date_CheckDate.EditValue != null)
                this._PCIncomingCheck.CheckDate = this.date_CheckDate.DateTime;
            else
                this._PCIncomingCheck.CheckDate = null;
            if (this.date_IncomingDate.EditValue != null)
                this._PCIncomingCheck.IncomingDate = this.date_IncomingDate.DateTime;
            else
                this._PCIncomingCheck.IncomingDate = null;

            this._PCIncomingCheck.EmployeeId = this.nccEmployee.EditValue == null ? null : (this.nccEmployee.EditValue as Model.Employee).EmployeeId;
            this._PCIncomingCheck.MaterialCategory = this.txt_materialCategory.Text;
            this._PCIncomingCheck.Note = this.txt_Pihao.Text;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._PCIncomingCheck);
                    break;
                case "update":
                    this.manager.Update(this._PCIncomingCheck);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._PCIncomingCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._PCIncomingCheck = this.manager.GetDetail(this._PCIncomingCheck.PCIncomingCheckId);
            }

            this.txt_Id.Text = this._PCIncomingCheck.PCIncomingCheckId;
            this.date_PurchaseDate.EditValue = this._PCIncomingCheck.PurchaseDate;
            this.date_IncomingDate.EditValue = this._PCIncomingCheck.IncomingDate;
            this.date_CheckDate.EditValue = this._PCIncomingCheck.CheckDate;
            this.txt_materialCategory.Text = this._PCIncomingCheck.MaterialCategory;
            this.nccEmployee.EditValue = this._PCIncomingCheck.Employee;
            this.txt_Pihao.Text = this._PCIncomingCheck.Note;
            this.bindingSourceDetail.DataSource = this._PCIncomingCheck.Detail;

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

            this.txt_Id.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._PCIncomingCheck);
        }

        protected override void Delete()
        {
            if (this._PCIncomingCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.PCIncomingCheck model = this.manager.GetNext(this._PCIncomingCheck);
                this.manager.Delete(this._PCIncomingCheck.PCIncomingCheckId);
                if (model == null)
                    this._PCIncomingCheck = this.manager.GetLast();
                else
                    this._PCIncomingCheck = model;
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
            //            Model.PCIncomingCheckDetail detail = new Book.Model.PCIncomingCheckDetail();
            //            detail.PCIncomingCheckDetailId = Guid.NewGuid().ToString();
            //            detail.PCIncomingCheckId = this._PCIncomingCheck.PCIncomingCheckId;
            //            detail.CheckDate = DateTime.Now;
            //            detail.Product = product;
            //            detail.ProductId = product.ProductId;
            //            this._PCIncomingCheck.Detail.Add(detail);
            //            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
            //        }
            //    }
            //    else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
            //    {
            //        Model.PCIncomingCheckDetail detail = new Book.Model.PCIncomingCheckDetail();
            //        detail.PCIncomingCheckDetailId = Guid.NewGuid().ToString();
            //        detail.PCIncomingCheckId = this._PCIncomingCheck.PCIncomingCheckId;
            //        detail.CheckDate = DateTime.Now;
            //        detail.Product = f.SelectedItem as Model.Product;
            //        detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
            //        this._PCIncomingCheck.Detail.Add(detail);
            //        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
            //    }
            //}
            Model.PCIncomingCheckDetail detail = new Book.Model.PCIncomingCheckDetail();
            detail.PCIncomingCheckDetailId = Guid.NewGuid().ToString();
            detail.PCIncomingCheckId = this._PCIncomingCheck.PCIncomingCheckId;
            detail.CheckDate = DateTime.Now;
            this._PCIncomingCheck.Detail.Add(detail);
            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);

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

        protected override string tableCode()
        {
            return "PCIncomingCheck" + "," + this._PCIncomingCheck.PCIncomingCheckId;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                Invoices.CG.CGForm form = new Invoices.CG.CGForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.key != null && form.key.Count > 0)
                    {
                        this.date_PurchaseDate.EditValue = form.key[0].Invoice.InvoiceDate;
                        Model.PCIncomingCheckDetail detail;
                        foreach (Model.InvoiceCODetail item in form.key)
                        {
                            detail = new Book.Model.PCIncomingCheckDetail();
                            detail.PCIncomingCheckDetailId = Guid.NewGuid().ToString();
                            detail.PCIncomingCheckId = this._PCIncomingCheck.PCIncomingCheckId;
                            detail.CheckDate = DateTime.Now;
                            detail.Product = item.Product;
                            detail.ProductId = item.ProductId;
                            this._PCIncomingCheck.Detail.Add(detail);
                            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                        }
                        this.gridControl1.RefreshDataSource();
                    }
                }
                form.Dispose();
                GC.Collect();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RelationXOForm f = new RelationXOForm();
            f.Show(this);
        }


    }
}