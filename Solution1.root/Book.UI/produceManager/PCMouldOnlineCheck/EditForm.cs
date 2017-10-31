using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCMouldOnlineCheck
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PCMouldOnlineCheck _pCMouldOnlineCheck = null;
        BL.PCMouldOnlineCheckManager manager = new Book.BL.PCMouldOnlineCheckManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();

        int LastFlag = 0;
        public EditForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.PCMouldOnlineCheck.PRO_PCMouldOnlineCheckDate, new AA(Properties.Resources.DateNotNull, this.dateEdit1));
            this.invalidValueExceptions.Add(Model.PCMouldOnlineCheckDetail.PRO_CheckDate, new AA("檢查日期不能為空！", this.gridControl1));
            this.invalidValueExceptions.Add(Model.PCMouldOnlineCheckDetail.PRO_OnlineDate, new AA(Properties.Resources.OnlineDateNotNull, this.gridControl1));

            this.action = "view";
            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectUseERPer();
        }

        public EditForm(Model.PCMouldOnlineCheck model)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._pCMouldOnlineCheck = model;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCMouldOnlineCheck model, string action)
            : this()
        {
            this._pCMouldOnlineCheck = model;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pCMouldOnlineCheck = new Book.Model.PCMouldOnlineCheck();
            this._pCMouldOnlineCheck.PCMouldOnlineCheckId = this.manager.GetId();
            this._pCMouldOnlineCheck.PCMouldOnlineCheckDate = DateTime.Now;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._pCMouldOnlineCheck);
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._pCMouldOnlineCheck);
        }

        protected override void MoveFirst()
        {
            this._pCMouldOnlineCheck = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._pCMouldOnlineCheck = this.manager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.PCMouldOnlineCheck p = this.manager.GetPrev(this._pCMouldOnlineCheck);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCMouldOnlineCheck = p;
        }

        protected override void MoveNext()
        {
            Model.PCMouldOnlineCheck p = this.manager.GetNext(this._pCMouldOnlineCheck);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCMouldOnlineCheck = p;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            this._pCMouldOnlineCheck.PCMouldOnlineCheckId = this.txt_Id.Text;
            if (this.dateEdit1.EditValue != null)
                this._pCMouldOnlineCheck.PCMouldOnlineCheckDate = this.dateEdit1.DateTime;
            this._pCMouldOnlineCheck.Employee = this.nccEmployee.EditValue as Model.Employee;
            this._pCMouldOnlineCheck.EmployeeId = this._pCMouldOnlineCheck.Employee == null ? null : this._pCMouldOnlineCheck.Employee.EmployeeId;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._pCMouldOnlineCheck);
                    break;
                case "update":
                    this.manager.Update(this._pCMouldOnlineCheck);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._pCMouldOnlineCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._pCMouldOnlineCheck = this.manager.GetDetail(this._pCMouldOnlineCheck.PCMouldOnlineCheckId);
            }

            this.txt_Id.EditValue = this._pCMouldOnlineCheck.PCMouldOnlineCheckId;
            this.dateEdit1.EditValue = this._pCMouldOnlineCheck.PCMouldOnlineCheckDate;
            this.nccEmployee.EditValue = this._pCMouldOnlineCheck.Employee;

            #region LookUpEditor

            DataTable dt = new DataTable();
            //DataColumn dc = new DataColumn("id", typeof(string));
            DataColumn dc1 = new DataColumn("name", typeof(string));
            //dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            DataRow dr;
            dr = dt.NewRow();
            //dr[0] = "-1";
            dr[0] = string.Empty;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "0";
            dr[0] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "1";
            dr[0] = "△";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "2";
            dr[0] = "X";
            dt.Rows.Add(dr);

            for (int i = 0; i < this.gridView1.Columns.Count; i++)
            {
                if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit && this.gridView1.Columns[i].Name != "gridColumnEmployee")
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name",25, "标识"),
                     });
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "name";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "name";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).NullText = "";
                }
            }

            #endregion

            this.bindingSourceDetail.DataSource = this._pCMouldOnlineCheck.Detail;

            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Ro(this._pCMouldOnlineCheck);
        }

        protected override void Delete()
        {
            if (this._pCMouldOnlineCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.PCMouldOnlineCheck model = this.manager.GetNext(this._pCMouldOnlineCheck);
                this.manager.Delete(this._pCMouldOnlineCheck);
                if (model == null)
                    this._pCMouldOnlineCheck = this.manager.GetLast();
                else
                    this._pCMouldOnlineCheck = model;
            }
        }

        //搜索
        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListFrom f = new ListFrom();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCMouldOnlineCheck model = f.SelectItem.PCMouldOnlineCheck as Model.PCMouldOnlineCheck;
                if (model != null)
                {
                    this._pCMouldOnlineCheck = model;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

        //选取客户订单
        private void simpleButtonXO_Click(object sender, EventArgs e)
        {
            Invoices.XS.SearcharInvoiceXSForm f = new Book.UI.Invoices.XS.SearcharInvoiceXSForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.key != null && f.key.Count > 0)
                {
                    Model.PCMouldOnlineCheckDetail model;
                    foreach (var item in f.key)
                    {
                        model = new Book.Model.PCMouldOnlineCheckDetail();
                        model.PCMouldOnlineCheckDetailId = Guid.NewGuid().ToString();
                        model.OnlineDate = DateTime.Now;
                        model.CheckDate = DateTime.Now;
                        model.Product = item.Product;
                        model.ProductId = item.ProductId;
                        model.InvoiceXO = item.Invoice;
                        model.InvoiceXOId = item.InvoiceId;
                        this._pCMouldOnlineCheck.Detail.Add(model);
                    }
                }
            }

            this.gridControl1.RefreshDataSource();
        }

        //選取加工單
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectItems != null && f.SelectItems.Count > 0)
                {
                    Model.PCMouldOnlineCheckDetail model;
                    foreach (var item in f.SelectItems)
                    {
                        model = new Book.Model.PCMouldOnlineCheckDetail();
                        model.PCMouldOnlineCheckDetailId = Guid.NewGuid().ToString();
                        model.OnlineDate = DateTime.Now;
                        model.CheckDate = DateTime.Now;
                        model.ProductId = item.ProductId;
                        model.Product = this.productManager.Get(item.ProductId);
                        model.InvoiceXOId = item.InvoiceXOId;
                        model.InvoiceXO = this.invoiceXOManager.Get(item.InvoiceXOId);
                        this._pCMouldOnlineCheck.Detail.Add(model);
                    }
                }
            }
            this.gridControl1.RefreshDataSource();
        }

        //+
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            //Model.PCMouldOnlineCheckDetail model = new Book.Model.PCMouldOnlineCheckDetail();
            //model.
        }

        //-
        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
                this.gridControl1.RefreshDataSource();
            }
        }


        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCMouldOnlineCheck.PRO_PCMouldOnlineCheckId;
        }

        protected override int AuditState()
        {
            return this._pCMouldOnlineCheck.AuditState.HasValue ? this._pCMouldOnlineCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCMouldOnlineCheck" + "," + this._pCMouldOnlineCheck.PCMouldOnlineCheckId;
        }
        #endregion
    }
}