using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;

namespace Book.UI.AccountPayable.AcbeginbillReceivable
{
    public partial class EditForm : BaseEditForm
    {

        #region 变量对象定义

        BL.AcbeginbillReceivableManager billManager = new BL.AcbeginbillReceivableManager();
        BL.AcbeginbillReceivableDetailManager billDetailManager = new Book.BL.AcbeginbillReceivableDetailManager();
        Model.AcbeginbillReceivable _bill = null;
        BL.CustomerManager customerManager = new BL.CustomerManager();
        #endregion

        public EditForm()
        {
            InitializeComponent();

            this.action = "view";
            this.bindingSourceCustomer.DataSource = this.customerManager.Select();
            this.newChooseContorlEmployee1Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployeeId.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            //  this.newChooseContorlAtCurrencyCategoryId.Choose = new Book.UI.Accounting.CurrencyCategory.ChooseAtCurrencyCategory();
        }

        public EditForm(Model.AcbeginbillReceivable receivable)
            : this()
        {
            this._bill = receivable;
            this.action = "update";
        }

        public EditForm(Model.AcbeginbillReceivable receivable, string action)
            : this()
        {
            this._bill = receivable;
            this.action = action;
        }

        #region 重载基类的方法

        protected override void MovePrev()
        {
            Model.AcbeginbillReceivable temp = this.billManager.GetPrev(this._bill);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bill = this.billManager.Get(temp.AcbeginbillReceivableId);
        }


        /// <summary>
        /// 首笔
        /// </summary>
        protected override void MoveFirst()
        {
            this._bill = this.billManager.Get(this.billManager.GetFirst() == null ? "" : this.billManager.GetFirst().AcbeginbillReceivableId);
        }


        /// <summary>
        /// 尾笔
        /// </summary>
        protected override void MoveLast()
        {
            //if (mpsheader == null)
            {
                this._bill = this.billManager.Get(this.billManager.GetLast() == null ? "" : this.billManager.GetLast().AcbeginbillReceivableId);
            }
        }

        protected override void MoveNext()
        {
            Model.AcbeginbillReceivable temp = this.billManager.GetNext(this._bill);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bill = this.billManager.Get(temp.AcbeginbillReceivableId);
        }

        /// <summary>
        /// 是否有返回行
        /// </summary>
        /// <returns></returns>
        protected override bool HasRows()
        {
            return this.billManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.billManager.HasRowsAfter(this._bill);
        }

        protected override bool HasRowsPrev()
        {
            return this.billManager.HasRowsBefore(this._bill);
        }

        protected override void AddNew()
        {
            this._bill = new Model.AcbeginbillReceivable();
            this._bill.AcbeginbillReceivableId = this.billManager.GetId();
            this._bill.AcbeginbillReceivableDate = DateTime.Now;
            this._bill.Employee = BL.V.ActiveOperator.Employee;
            this._bill.Details = this.billDetailManager.SelectDefaultDetails();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.billManager.Delete(this._bill);
                this._bill = this.billManager.GetNext(this._bill);
                if (this._bill == null)
                {
                    this._bill = this.billManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void Save()
        {
            this._bill.AcbeginbillReceivableId = textEditAcbeginbillReceivableId.Text;
            this._bill.AcbeginbillReceivableDate = this.dateEditAcbeginbillReceivableDate.Text == "" ? System.DateTime.Now : this.dateEditAcbeginbillReceivableDate.DateTime;
            this._bill.AcbeginbillReceivableDesc = this.memoEditAcbeginbillReceivableDesc.Text;
            this._bill.Employee1 = this.newChooseContorlEmployeeId.EditValue as Model.Employee;
            if (this._bill.Employee1 != null)
                this._bill.Employee1Id = this._bill.Employee1.EmployeeId;
            this._bill.Employee = this.newChooseContorlEmployeeId.EditValue as Model.Employee;
            if (this._bill.Employee != null)
                this._bill.EmployeeId = this._bill.Employee.EmployeeId;
            //  this._bill.AtCurrencyCategory = this.newChooseContorlAtCurrencyCategoryId.EditValue as Model.AtCurrencyCategory;
            if (this._bill.AtCurrencyCategory != null)
                this._bill.AtCurrencyCategoryId = this._bill.AtCurrencyCategory.AtCurrencyCategoryId;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.billManager.Insert(this._bill);
                    break;
                case "update":
                    this.billManager.Update(this._bill);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._bill == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._bill = this.billManager.GetDetail(this._bill);
                    if (this._bill == null)
                    {
                        this._bill = new Book.Model.AcbeginbillReceivable();
                    }
                }
            }

            this.textEditAcbeginbillReceivableId.Text = this._bill.AcbeginbillReceivableId;
            if (this._bill.AcbeginbillReceivableDate != null)
                this.dateEditAcbeginbillReceivableDate.DateTime = this._bill.AcbeginbillReceivableDate.Value;
            else
                this.dateEditAcbeginbillReceivableDate.Text = "";
            this.memoEditAcbeginbillReceivableDesc.Text = this._bill.AcbeginbillReceivableDesc;
            this.newChooseContorlEmployee1Id.EditValue = this._bill.AuditEmp;
            this.newChooseContorlEmployeeId.EditValue = this._bill.Employee;
            //  this.newChooseContorlAtCurrencyCategoryId.EditValue = this._bill.AtCurrencyCategory;
            //this.textEditAuditingState.Text = this._bill.AuditingState;
            this.textEditAuditingState.EditValue = this.GetAuditName(this._bill.AuditState);
            this.bindingSourceAcbeginbillReceivable.DataSource = this._bill.Details;

            //this.gridView1.GroupPanelText = "共: " + this.acbeginAccountPayble.Details.Count + " 項";
            base.Refresh();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Ro(this._bill);
        }

        #endregion

        //private void simpleButton_Add_Click(object sender, EventArgs e)
        //{
        //    Model.AcbeginbillReceivableDetail temp = new Model.AcbeginbillReceivableDetail();
        //    temp.AcbeginbillReceivableDetailId = Guid.NewGuid().ToString();
        //    this._bill.Details.Add(temp);
        //    this.gridControl1.RefreshDataSource();
        //}

        //private void simpleButton_Remove_Click(object sender, EventArgs e)
        //{
        //    Model.AcbeginbillReceivableDetail temp = this.bindingSourceAcbeginbillReceivable.Current as Model.AcbeginbillReceivableDetail;
        //    if (temp == null) return;
        //    this._bill.Details.Remove(temp);
        //    this.gridControl1.RefreshDataSource();
        //}

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcbeginbillReceivable.PRO_AcbeginbillReceivableId;
        }

        protected override int AuditState()
        {
            return this._bill.AuditState.HasValue ? this._bill.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcbeginbillReceivable" + "," + this._bill.AcbeginbillReceivableId;
        }

        #endregion
    }
}