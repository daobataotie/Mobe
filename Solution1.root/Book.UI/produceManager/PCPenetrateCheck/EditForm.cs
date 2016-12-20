using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.ProduceManager.Workhouselog;

namespace Book.UI.produceManager.PCPenetrateCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        BL.PCPenetrateCheckManager _PCPCManager = new Book.BL.PCPenetrateCheckManager();
        Model.PCPenetrateCheck _PCPC = null;

        int Def_select = 2;

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.PCPenetrateCheck.PRO_PCPenetrateCheckId, new AA(Properties.Resources.NumsIsNotNull, this.txtPCPenetrateCheckId));
            this.requireValueExceptions.Add(Model.PCPenetrateCheck.PRO_PCPenetrateCheckDate, new AA(Properties.Resources.DateIsNull, this.DE_PCPenetrateCheckDate));
            this.requireValueExceptions.Add(Model.PCPenetrateCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.txtProduct));
            this.requireValueExceptions.Add(Model.PCPenetrateCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));

            this.nccEmployee0.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
            this.action = "view";
        }

        int LastFlag = 0;

        public EditForm(string invoiceId)
            : this()
        {
            this._PCPC = this._PCPCManager.Get(invoiceId);
            if (this._PCPC == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCPenetrateCheck mPCPC)
            : this()
        {
            if (mPCPC == null)
                throw new ArithmeticException("invoiceid");
            this._PCPC = mPCPC;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCPenetrateCheck mPCPC, string action)
            : this()
        {
            this._PCPC = mPCPC;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._PCPC = new Book.Model.PCPenetrateCheck();
            this._PCPC.PCPenetrateCheckId = this._PCPCManager.GetId();
            this._PCPC.PCPenetrateCheckDate = DateTime.Now;
            this._PCPC.PCPenetrateCheckQuantity = 1;    //默认检测数量为1
        }

        public override void Refresh()
        {
            if (this._PCPC == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._PCPC = this._PCPCManager.Get(this._PCPC.PCPenetrateCheckId);
                }
            }

            this.txtPCPenetrateCheckId.Text = this._PCPC.PCPenetrateCheckId;
            this.txtInvoiceCusXOId.Text = this._PCPC.InvoiceCusXOId;
            this.txtPCPenetrateCheckDesc.Text = this._PCPC.PCPenetrateCheckDesc;
            this.DE_PCPenetrateCheckDate.EditValue = this._PCPC.PCPenetrateCheckDate;
            this.txtProduct.Text = this._PCPC.Product == null ? "" : this._PCPC.Product.ToString();
            this.CE_PCPenetrateCheckQuantity.EditValue = this._PCPC.PCPenetrateCheckQuantity.HasValue ? this._PCPC.PCPenetrateCheckQuantity.Value : 0;
            this.CE_InvoiceXOQuantity.EditValue = this._PCPC.InvoiceXOQuantity.HasValue ? this._PCPC.InvoiceXOQuantity.Value : 0;
            this.txtPronoteHeaderId.Text = this._PCPC.PronoteHeaderId;
            this.nccEmployee0.EditValue = this._PCPC.Employee;
            this.chkIspassing.Checked = this._PCPC.IsPassing.HasValue ? this._PCPC.IsPassing.Value : false;
            this.spinPCPenetrateCheckCenterCount.EditValue = this._PCPC.PCPenetrateCheckCenterCount.HasValue ? this._PCPC.PCPenetrateCheckCenterCount.Value : 0;
            this.spinPCPenetrateCheckLeftCount.EditValue = this._PCPC.PCPenetrateCheckLeftCount.HasValue ? this._PCPC.PCPenetrateCheckLeftCount.Value : 0;
            this.spinPCPenetrateCheckRightCount.EditValue = this._PCPC.PCPenetrateCheckRightCount.HasValue ? this._PCPC.PCPenetrateCheckRightCount.Value : 0;

            this.newChooseContorlAuditEmp.EditValue = this._PCPC.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCPC.AuditState);

            this.lookUpEditUnit.EditValue = this._PCPC.ProductUnitId;
            base.Refresh();

            this.CE_InvoiceXOQuantity.Enabled = false;
            this.txtPCPenetrateCheckId.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.PCPenetrateCheck pcpc = this._PCPCManager.GetNext(this._PCPC);
            if (pcpc == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCPC = this._PCPCManager.Get(pcpc.PCPenetrateCheckId);
        }

        protected override void MovePrev()
        {
            Model.PCPenetrateCheck pcpc = this._PCPCManager.GetPrev(this._PCPC);
            if (pcpc == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCPC = this._PCPCManager.Get(pcpc.PCPenetrateCheckId);
        }

        protected override void MoveFirst()
        {
            this._PCPC = this._PCPCManager.Get(this._PCPCManager.GetFirst() == null ? "" : this._PCPCManager.GetFirst().PCPenetrateCheckId);
        }

        protected override void MoveLast()
        {
            this._PCPC = this._PCPCManager.Get(this._PCPCManager.GetLast() == null ? "" : this._PCPCManager.GetLast().PCPenetrateCheckId);
        }

        protected override bool HasRows()
        {
            return this._PCPCManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._PCPCManager.HasRowsAfter(this._PCPC);
        }

        protected override bool HasRowsPrev()
        {
            return this._PCPCManager.HasRowsBefore(this._PCPC);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._PCPC);
        }

        protected override void Save()
        {
            this._PCPC.PCPenetrateCheckId = this.txtPCPenetrateCheckId.Text;
            this._PCPC.PCPenetrateCheckDate = this.DE_PCPenetrateCheckDate.DateTime;
            this._PCPC.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._PCPC.PCPenetrateCheckQuantity = double.Parse(this.CE_PCPenetrateCheckQuantity.EditValue == null ? "0" : this.CE_PCPenetrateCheckQuantity.EditValue.ToString());
            this._PCPC.PCPenetrateCheckLeftCount = double.Parse(this.spinPCPenetrateCheckLeftCount.EditValue == null ? "0" : this.spinPCPenetrateCheckLeftCount.EditValue.ToString());
            this._PCPC.PCPenetrateCheckCenterCount = double.Parse(this.spinPCPenetrateCheckCenterCount.EditValue == null ? "0" : this.spinPCPenetrateCheckCenterCount.EditValue.ToString());
            this._PCPC.PCPenetrateCheckRightCount = double.Parse(this.spinPCPenetrateCheckRightCount.EditValue == null ? "0" : this.spinPCPenetrateCheckRightCount.EditValue.ToString());
            this._PCPC.InvoiceXOQuantity = double.Parse(this.CE_InvoiceXOQuantity.EditValue == null ? "0" : this.CE_InvoiceXOQuantity.EditValue.ToString());
            this._PCPC.PCPenetrateCheckDesc = this.txtPCPenetrateCheckDesc.Text;
            if (this.nccEmployee0.EditValue != null)
            {
                this._PCPC.EmployeeId = (this.nccEmployee0.EditValue as Model.Employee).EmployeeId;
            }
            this._PCPC.IsPassing = this.chkIspassing.Checked;

            this._PCPC.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

            switch (this.action)
            {
                case "insert":
                    this._PCPCManager.Insert(this._PCPC);
                    break;
                case "update":
                    this._PCPCManager.Update(this._PCPC);
                    break;
            }
        }

        protected override void Delete()
        {

            if (this._PCPC == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCPCManager.Delete(this._PCPC.PCPenetrateCheckId);
            this._PCPC = this._PCPCManager.GetNext(this._PCPC);
            if (this._PCPC == null)
            {
                this._PCPC = this._PCPCManager.GetLast();
            }
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._PCPC = f.SelectItem as Model.PCPenetrateCheck;
                this.action = "view";
                this.Refresh();
            }
            f.Dispose();
            GC.Collect();
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
                    this._PCPC.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._PCPC.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._PCPC.mCheckStandard = currentModel.CustomerCheckStandard;
                    this._PCPC.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._PCPC.ProductId = this._PCPC.Product.ProductId;
                    this._PCPC.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity;

                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        private void txtPCPenetrateCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.txtPCPenetrateCheckDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCPenetrateCheck.PRO_PCPenetrateCheckId;
        }

        protected override int AuditState()
        {
            return this._PCPC.AuditState.HasValue ? this._PCPC.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCPenetrateCheck" + "," + this._PCPC.PCPenetrateCheckId;
        }

        #endregion
    }
}
