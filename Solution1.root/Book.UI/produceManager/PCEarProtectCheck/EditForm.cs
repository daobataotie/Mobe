using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using System.Xml;

namespace Book.UI.produceManager.PCEarProtectCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        Model.PCEarProtectCheck _earProtectCheck = null;
        BL.PCEarProtectCheckManager _earProtectCheckManager = new Book.BL.PCEarProtectCheckManager();
        bool IsReport;

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCEarProtectCheck.PRO_CheckDate, new AA(Properties.Resources.DateIsNull, this.Date_PCEarProtectCheck));
            this.requireValueExceptions.Add(Model.PCEarProtectCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.BEProduct));
            this.requireValueExceptions.Add(Model.PCEarProtectCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployeeCheck));
            this.action = "view";
            this.nccEmployeeCheck.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceProductUnit.DataSource = (new BL.ProductUnitManager()).Select();
            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectIdAndName();
            this.bindingSourceBusinessHours.DataSource = new BL.BusinessHoursManager().SelectIdAndName();
        }

        int LastFlag = 0;

        public EditForm(Model.PCEarProtectCheck PCEarProtectCheck)
            : this()
        {
            if (PCEarProtectCheck == null)
                throw new ArithmeticException("invoiceid");
            this._earProtectCheck = PCEarProtectCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCEarProtectCheck PCEarProtectCheck, string action)
            : this()
        {
            this._earProtectCheck = PCEarProtectCheck;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(string tag)
            : this()
        {
            this.IsReport = true;
        }

        protected override void AddNew()
        {
            this._earProtectCheck = new Book.Model.PCEarProtectCheck();
            this._earProtectCheck.PCEarProtectCheckId = this._earProtectCheckManager.GetId();
            this._earProtectCheckManager.TiGuiExists(this._earProtectCheck);
            this._earProtectCheck.CheckDate = DateTime.Now.Date;
            this._earProtectCheck.CheckCount = 1;  //检测数量默认为1
            //this._ansipcic.Employee = BL.V.ActiveOperator.Employee;
            //this._ansipcic.EmployeeId = BL.V.ActiveOperator.EmployeeId;
            //初始化添加一条详细
            this._earProtectCheck.Details = new List<Model.PCEarProtectCheckDetail>();
            //this.AddDataRows();
        }

        protected override void Delete()
        {
            if (this._earProtectCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._earProtectCheckManager.Delete(this._earProtectCheck.PCEarProtectCheckId);

            this._earProtectCheck = this._earProtectCheckManager.GetNext(this._earProtectCheck);
            if (this._earProtectCheck == null)
            {
                this._earProtectCheck = this._earProtectCheckManager.GetLast();
            }
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._earProtectCheck = this._earProtectCheckManager.mGetLast(this.IsReport);
        }

        protected override void MoveFirst()
        {
            this._earProtectCheck = this._earProtectCheckManager.mGetFirst(this.IsReport);
        }

        protected override void MovePrev()
        {
            Model.PCEarProtectCheck model = this._earProtectCheckManager.mGetPrev(this._earProtectCheck);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._earProtectCheck = model;
        }

        protected override void MoveNext()
        {
            Model.PCEarProtectCheck model = this._earProtectCheckManager.mGetNext(this._earProtectCheck);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._earProtectCheck = model;
        }

        protected override bool HasRows()
        {
            return this._earProtectCheckManager.mHasRows(this.IsReport);
        }

        protected override bool HasRowsNext()
        {
            return this._earProtectCheckManager.mHasRowsAfter(this._earProtectCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this._earProtectCheckManager.mHasRowsBefore(this._earProtectCheck);
        }

        public override void Refresh()
        {
            if (this._earProtectCheck == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._earProtectCheck = this._earProtectCheckManager.Get(this._earProtectCheck);
                }
            }

            //初始化控件
            this.txt_PCEarProtectCheckId.Text = this._earProtectCheck.PCEarProtectCheckId;
            this.txtPronoteHeaderId.Text = this._earProtectCheck.PronoteHeaderId;
            this.txtInvoiceCusXOId.Text = this._earProtectCheck.InvoiceCusXOId;
            this.ceInvoiceXOCount.EditValue = this._earProtectCheck.InvoiceXOQuantity.HasValue ? this._earProtectCheck.InvoiceXOQuantity.Value : 0;
            this.calcPCCheckCount.EditValue = this._earProtectCheck.CheckCount.HasValue ? this._earProtectCheck.CheckCount.Value : 0;
            this.Date_PCEarProtectCheck.EditValue = this._earProtectCheck.CheckDate;
            this.BEProduct.EditValue = this._earProtectCheck.Product;
            this.nccEmployeeCheck.EditValue = this._earProtectCheck.Employee;
            this.txtCheckedStadard.Text = this._earProtectCheck.CheckStadard;

            this.newChooseContorlAuditEmp.EditValue = this._earProtectCheck.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._earProtectCheck.AuditState);
            this.lookUpEditUnit.EditValue = this._earProtectCheck.ProductUnitId;
            this.richTextBoxNote.Rtf = this._earProtectCheck.Note;

            this.bindingSourceDetail.DataSource = this._earProtectCheck.Details;

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

            this.ceInvoiceXOCount.Enabled = false;
            this.txt_PCEarProtectCheckId.Properties.ReadOnly = true;
        }

        protected override void Save()
        {
            this._earProtectCheck.PCEarProtectCheckId = this.txt_PCEarProtectCheckId.Text;
            this._earProtectCheck.PronoteHeaderId = this.txtPronoteHeaderId.EditValue == null ? null : this.txtPronoteHeaderId.Text;
            this._earProtectCheck.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._earProtectCheck.CheckDate = this.Date_PCEarProtectCheck.DateTime;
            this._earProtectCheck.CheckStadard = this.txtCheckedStadard.Text;
            this._earProtectCheck.InvoiceXOQuantity = this.ceInvoiceXOCount.EditValue != null ? double.Parse(this.ceInvoiceXOCount.EditValue.ToString()) : 0;
            this._earProtectCheck.CheckCount = this.calcPCCheckCount.EditValue != null ? int.Parse(this.calcPCCheckCount.EditValue.ToString()) : 0;

            this._earProtectCheck.Employee = (this.nccEmployeeCheck.EditValue as Model.Employee);
            if (this._earProtectCheck.Employee != null)
            {
                this._earProtectCheck.EmployeeId = this._earProtectCheck.Employee.EmployeeId;
            }

            this._earProtectCheck.Product = (this.BEProduct.EditValue as Model.Product);
            if (this._earProtectCheck.Product != null)
            {
                this._earProtectCheck.ProductId = this._earProtectCheck.Product.ProductId;
            }

            this._earProtectCheck.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();
            this._earProtectCheck.Note = this.richTextBoxNote.Rtf;
            this._earProtectCheck.ISReport = this.IsReport;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this._earProtectCheckManager.Insert(this._earProtectCheck);
                    break;
                case "update":
                    this._earProtectCheckManager.Update(this._earProtectCheck);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._earProtectCheck);
        }

        //添加行
        private void AddDataRows()
        {
            Model.PCEarProtectCheckDetail EarProtectCheckDetail = new Book.Model.PCEarProtectCheckDetail();
            EarProtectCheckDetail.PCEarProtectCheckDetailId = Guid.NewGuid().ToString();
            EarProtectCheckDetail.PCEarProtectCheckId = this._earProtectCheck.PCEarProtectCheckId;
            EarProtectCheckDetail.CheckDate = DateTime.Now;
            this._earProtectCheck.Details.Add(EarProtectCheckDetail);

            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(EarProtectCheckDetail);
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            this.AddDataRows();
            this.gridControl1.RefreshDataSource();
        }

        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceEmployee.Current != null)
            {
                this._earProtectCheck.Details.Remove(this.bindingSourceDetail.Current as Model.PCEarProtectCheckDetail);
                this.gridControl1.RefreshDataSource();
            }
        }

        //选择加工单据
        private void btnGetPronoteHeader_Click(object sender, EventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(null, 0);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader currentModel = pronoForm.SelectItem;
                if (currentModel != null)
                {
                    this._earProtectCheck.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._earProtectCheck.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._earProtectCheck.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._earProtectCheck.ProductId = currentModel.ProductId;
                    this._earProtectCheck.CheckStadard = currentModel.CustomerCheckStandard;
                    this._earProtectCheck.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity;

                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        //搜索
        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCEarProtectCheck currentModel = form.SelectItem as Model.PCEarProtectCheck;
                if (currentModel != null)
                {
                    this._earProtectCheck = currentModel;
                    this._earProtectCheck = this._earProtectCheckManager.Get(this._earProtectCheck);
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }

        private void BEProduct_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.BEProduct.EditValue = f.SelectedItem;
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCEarProtectCheck.PRO_PCEarProtectCheckId;
        }

        protected override int AuditState()
        {
            return this._earProtectCheck.AuditState.HasValue ? this._earProtectCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCEarProtectCheck" + "," + this._earProtectCheck.PCEarProtectCheckId;
        }

        #endregion

    }
}
