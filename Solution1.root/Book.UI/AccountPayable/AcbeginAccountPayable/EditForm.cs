using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using System.Linq;

namespace Book.UI.AccountPayable.AcbeginAccountPayable
{
    public partial class EditForm : BaseEditForm
    {

        #region 变量对象定义

        BL.AcbeginAccountPayableManager acbeginAccountPaybleManager = new BL.AcbeginAccountPayableManager();
        BL.AcbeginAccountPayableDetailManager acbeginAccountPayableDetailManager = new Book.BL.AcbeginAccountPayableDetailManager();
        Model.AcbeginAccountPayable _acbeginAccountPayble = null;
        BL.SupplierManager supplierManager = new BL.SupplierManager();
        #endregion

        public EditForm()
        {
            InitializeComponent();
            this.bindingSourceSupplier.DataSource = this.supplierManager.Select();
            this.action = "view";
            this.newChooseEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseEmp0.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            //this.newChooseAtCurrencyCategory.Choose = new Book.UI.Accounting.CurrencyCategory.ChooseAtCurrencyCategory();
        }

        public EditForm(Model.AcbeginAccountPayable acbeginAccountPayable)
            : this()
        {
            this._acbeginAccountPayble = acbeginAccountPayable;
            this.action = "update";
        }

        public EditForm(Model.AcbeginAccountPayable acbeginAccountPayable, string action)
            : this()
        {
            this._acbeginAccountPayble = acbeginAccountPayable;
            this.action = action;
        }

        #region 重载基类的方法

        protected override void MovePrev()
        {
            Model.AcbeginAccountPayable temp = this.acbeginAccountPaybleManager.GetPrev(this._acbeginAccountPayble);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acbeginAccountPayble = this.acbeginAccountPaybleManager.Get(temp.AcbeginAccountPayableId);
        }

        protected override void MoveFirst()
        {
            this._acbeginAccountPayble = this.acbeginAccountPaybleManager.Get(this.acbeginAccountPaybleManager.GetFirst() == null ? "" : this.acbeginAccountPaybleManager.GetFirst().AcbeginAccountPayableId);
        }

        protected override void MoveLast()
        {
            // if (_acbeginAccountPayble == null)
            {
                this._acbeginAccountPayble = this.acbeginAccountPaybleManager.Get(this.acbeginAccountPaybleManager.GetLast() == null ? "" : this.acbeginAccountPaybleManager.GetLast().AcbeginAccountPayableId);
            }
        }

        protected override void MoveNext()
        {
            Model.AcbeginAccountPayable temp = this.acbeginAccountPaybleManager.GetNext(this._acbeginAccountPayble);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acbeginAccountPayble = this.acbeginAccountPaybleManager.Get(temp.AcbeginAccountPayableId);
        }

        protected override bool HasRows()
        {
            return this.acbeginAccountPaybleManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.acbeginAccountPaybleManager.HasRowsAfter(this._acbeginAccountPayble);
        }

        protected override bool HasRowsPrev()
        {
            return this.acbeginAccountPaybleManager.HasRowsBefore(this._acbeginAccountPayble);
        }

        protected override void AddNew()
        {
            _acbeginAccountPayble = new Model.AcbeginAccountPayable();
            _acbeginAccountPayble.AcbeginAccountPayableId = this.acbeginAccountPaybleManager.GetId();
            this._acbeginAccountPayble.AcbeginAccountPayableDate = DateTime.Now;
            _acbeginAccountPayble.Employee = BL.V.ActiveOperator.Employee;
            //_acbeginAccountPayble.Details = new List<Model.AcbeginAccountPayableDetail>();
            _acbeginAccountPayble.Details = this.acbeginAccountPayableDetailManager.SelectDefaultDetails();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.acbeginAccountPaybleManager.Delete(this._acbeginAccountPayble);
                this._acbeginAccountPayble = this.acbeginAccountPaybleManager.GetNext(this._acbeginAccountPayble);
                if (this._acbeginAccountPayble == null)
                {
                    this._acbeginAccountPayble = this.acbeginAccountPaybleManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void Save()
        {
            this._acbeginAccountPayble.AcbeginAccountPayableId = textEditAcbeginAccountPayableId.Text;
            this._acbeginAccountPayble.AcbeginAccountPayableDate = this.dateEditAcbeginAccountPayableDate.Text == "" ? System.DateTime.Now : this.dateEditAcbeginAccountPayableDate.DateTime;
            this._acbeginAccountPayble.AcbeginAccountPayableDesc = this.memoEditAcbeginAccountPayableDesc.Text;
            this._acbeginAccountPayble.Employee = this.newChooseEmp.EditValue as Model.Employee;
            if (this._acbeginAccountPayble.Employee != null)
                this._acbeginAccountPayble.EmployeeId = this._acbeginAccountPayble.Employee.EmployeeId;
            this._acbeginAccountPayble.Employee0 = this.newChooseEmp0.EditValue as Model.Employee;
            if (this._acbeginAccountPayble.Employee0 != null)
                this._acbeginAccountPayble.Employee0Id = this._acbeginAccountPayble.Employee0.EmployeeId;
            // this._acbeginAccountPayble.AtCurrencyCategory = this.newChooseAtCurrencyCategory.EditValue as Model.AtCurrencyCategory;
            //   if (this._acbeginAccountPayble.AtCurrencyCategory != null)
            //      this._acbeginAccountPayble.AtCurrencyCategoryId = this._acbeginAccountPayble.AtCurrencyCategory.AtCurrencyCategoryId;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.acbeginAccountPaybleManager.Insert(this._acbeginAccountPayble);
                    break;

                case "update":
                    this.acbeginAccountPaybleManager.Update(this._acbeginAccountPayble);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._acbeginAccountPayble == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action != "insert")
                {
                    this._acbeginAccountPayble = this.acbeginAccountPaybleManager.GetDetails(_acbeginAccountPayble);
                    if (this._acbeginAccountPayble == null)
                    {
                        this._acbeginAccountPayble = new Book.Model.AcbeginAccountPayable();
                    }
                }
            }

            this.textEditAcbeginAccountPayableId.Text = this._acbeginAccountPayble.AcbeginAccountPayableId;
            this.dateEditAcbeginAccountPayableDate.DateTime = this._acbeginAccountPayble.AcbeginAccountPayableDate.Value;
            this.memoEditAcbeginAccountPayableDesc.Text = this._acbeginAccountPayble.AcbeginAccountPayableDesc;
            // this.newChooseAtCurrencyCategory.EditValue = this._acbeginAccountPayble.AtCurrencyCategory;
            this.newChooseEmp.EditValue = this._acbeginAccountPayble.Employee;
            this.newChooseEmp0.EditValue = this._acbeginAccountPayble.AuditEmp;
            this.textEditAuditingState.EditValue = this.GetAuditName(this._acbeginAccountPayble.AuditState);
            this.bindingSourceAcbeginAccountPayableDetail.DataSource = this._acbeginAccountPayble.Details;


            //this.gridView1.GroupPanelText = "共: " + this.acbeginAccountPayble.Details.Count + " 項";
            base.Refresh();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Ro(this._acbeginAccountPayble);
        }

        #endregion

        //private void simpleButton_Add_Click(object sender, EventArgs e)
        //{
        //    Model.AcbeginAccountPayableDetail temp = new Model.AcbeginAccountPayableDetail();
        //    temp.AcbeginAccountPayableDetailId = Guid.NewGuid().ToString();
        //    this._acbeginAccountPayble.Details.Add(temp);
        //    this.gridControl1.RefreshDataSource();
        //}

        //private void simpleButton_Remove_Click(object sender, EventArgs e)
        //{
        //    Model.AcbeginAccountPayableDetail temp = this.bindingSourceAcbeginAccountPayableDetail.Current as Model.AcbeginAccountPayableDetail;
        //    if (temp == null) return;
        //    this._acbeginAccountPayble.Details.Remove(temp);
        //    this.gridControl1.RefreshDataSource();
        //}

        private void barButtonItem_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm list = new ListForm();
            if (list.ShowDialog() != DialogResult.OK) return;
            this._acbeginAccountPayble = this.acbeginAccountPaybleManager.GetDetails(list.SelectItem as Model.AcbeginAccountPayable);
            this.action = "view";
            this.Refresh();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcbeginAccountPayable.PRO_AcbeginAccountPayableId;
        }

        protected override int AuditState()
        {
            return this._acbeginAccountPayble.AuditState.HasValue ? this._acbeginAccountPayble.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcbeginAccountPayable" + "," + this._acbeginAccountPayble.AcbeginAccountPayableId;
        }

        #endregion

    }
}