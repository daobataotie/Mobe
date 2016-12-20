using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.ProduceManager.Workhouselog;

namespace Book.UI.produceManager.PCOpticsCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        BL.PCOpticsCheckManager _PCOCManager = new Book.BL.PCOpticsCheckManager();

        Model.PCOpticsCheck _PCOPC = null;

        int Def_select = 2;

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.PCOpticsCheck.PRO_PCOpticsCheckId, new AA(Properties.Resources.NumsIsNotNull, this.txtPCOpticsCheckId));
            this.requireValueExceptions.Add(Model.PCOpticsCheck.PRO_PCOpticsCheckDate, new AA(Properties.Resources.DateIsNull, this.DE_JYDRQ));
            this.requireValueExceptions.Add(Model.PCOpticsCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.txtProduct));
            this.requireValueExceptions.Add(Model.PCOpticsCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));

            this.nccEmployee0.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
            this.action = "view";
        }

        int LastFlag = 0;

        public EditForm(string invoiceId)
            : this()
        {
            this._PCOPC = this._PCOCManager.Get(invoiceId);
            if (this._PCOPC == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCOpticsCheck mPCOC)
            : this()
        {
            if (mPCOC == null)
                throw new ArithmeticException("invoiceid");
            this._PCOPC = mPCOC;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCOpticsCheck mPCOC, string action)
            : this()
        {
            this._PCOPC = mPCOC;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._PCOPC = new Book.Model.PCOpticsCheck();
            this._PCOPC.PCOpticsCheckId = this._PCOCManager.GetId();
            this._PCOPC.PCOpticsCheckDate = DateTime.Now.Date;
            this._PCOPC.PCOpticsCheckQuantity = 1;  //默认检测数量为1
        }

        public override void Refresh()
        {
            if (this._PCOPC == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._PCOPC = this._PCOCManager.Get(this._PCOPC.PCOpticsCheckId);
                }
            }

            this.txtPCOpticsCheckId.Text = this._PCOPC.PCOpticsCheckId;
            this.txtInvoiceCusXOId.Text = this._PCOPC.InvoiceCusXOId;
            this.txtPCOpticsCheckDesc.Text = this._PCOPC.PCOpticsCheckDesc;
            this.DE_JYDRQ.EditValue = this._PCOPC.PCOpticsCheckDate;
            this.txtProduct.Text = this._PCOPC.Product == null ? "" : this._PCOPC.Product.ToString();
            this.CE_PCOpticsCheckQuantity.EditValue = this._PCOPC.PCOpticsCheckQuantity.HasValue ? this._PCOPC.PCOpticsCheckQuantity.Value : 0;
            this.CE_InvoiceXOQuantity.EditValue = this._PCOPC.InvoiceXOQuantity.HasValue ? this._PCOPC.InvoiceXOQuantity.Value : 0;
            this.txtPronoteHeaderId.Text = this._PCOPC.PronoteHeaderId;
            this.nccEmployee0.EditValue = this._PCOPC.Employee;
            this.chkZiWaiXian.Checked = this._PCOPC.IsZiWaiXian.HasValue ? this._PCOPC.IsZiWaiXian.Value : false;
            this.chkKeJianGuang.Checked = this._PCOPC.IsKeJianGuang.HasValue ? this._PCOPC.IsKeJianGuang.Value : false;

            this.newChooseContorlAuditEmp.EditValue = this._PCOPC.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCOPC.AuditState);
            this.lookUpEditUnit.EditValue = this._PCOPC.ProductUnitId;

            base.Refresh();

            this.CE_InvoiceXOQuantity.Enabled = false;
            this.txtPCOpticsCheckId.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.PCOpticsCheck pcopc = this._PCOCManager.GetNext(this._PCOPC);
            if (pcopc == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._PCOPC = this._PCOCManager.Get(pcopc.PCOpticsCheckId);
        }

        protected override void MovePrev()
        {
            Model.PCOpticsCheck pcopc = this._PCOCManager.GetPrev(this._PCOPC);
            if (pcopc == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._PCOPC = this._PCOCManager.Get(pcopc.PCOpticsCheckId);
        }

        protected override void MoveFirst()
        {
            this._PCOPC = this._PCOCManager.Get(this._PCOCManager.GetFirst() == null ? "" : this._PCOCManager.GetFirst().PCOpticsCheckId);
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._PCOPC = this._PCOCManager.Get(this._PCOCManager.GetLast() == null ? "" : this._PCOCManager.GetLast().PCOpticsCheckId);
        }

        protected override bool HasRows()
        {
            return this._PCOCManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._PCOCManager.HasRowsAfter(this._PCOPC);
        }

        protected override bool HasRowsPrev()
        {
            return this._PCOCManager.HasRowsBefore(this._PCOPC);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._PCOPC);
        }

        protected override void Save()
        {
            this._PCOPC.PCOpticsCheckId = this.txtPCOpticsCheckId.Text;
            this._PCOPC.PCOpticsCheckDate = this.DE_JYDRQ.DateTime;
            this._PCOPC.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._PCOPC.PCOpticsCheckQuantity = int.Parse(this.CE_PCOpticsCheckQuantity.Value.ToString());
            this._PCOPC.InvoiceXOQuantity = int.Parse(this.CE_InvoiceXOQuantity.Value.ToString());
            this._PCOPC.PCOpticsCheckDesc = this.txtPCOpticsCheckDesc.Text;
            if (this.nccEmployee0.EditValue != null)
            {
                this._PCOPC.EmployeeId = (this.nccEmployee0.EditValue as Model.Employee).EmployeeId;
            }
            this._PCOPC.IsZiWaiXian = this.chkZiWaiXian.Checked;
            this._PCOPC.IsKeJianGuang = this.chkKeJianGuang.Checked;
            this._PCOPC.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

            string strCusXoId = this.txtInvoiceCusXOId.Text;
            string sqlJudge = string.Empty;
            switch (this.action)
            {
                case "insert":
                    this._PCOCManager.Insert(this._PCOPC);
                    break;
                case "update":
                    this._PCOCManager.Update(this._PCOPC);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._PCOPC == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCOCManager.Delete(this._PCOPC.PCOpticsCheckId);
            this._PCOPC = this._PCOCManager.GetNext(this._PCOPC);
            if (this._PCOPC == null)
            {
                this._PCOPC = this._PCOCManager.GetLast();
            }
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._PCOPC = f.SelectItem as Model.PCOpticsCheck;
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
                    this._PCOPC.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._PCOPC.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._PCOPC.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._PCOPC.ProductId = this._PCOPC.Product.ProductId;
                    this._PCOPC.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity;


                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        private void txtPCFinishCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.txtPCOpticsCheckDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCOpticsCheck.PRO_PCOpticsCheckId;
        }

        protected override int AuditState()
        {
            return this._PCOPC.AuditState.HasValue ? this._PCOPC.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCOpticsCheck" + "," + this._PCOPC.PCOpticsCheckId;
        }

        #endregion
    }
}
