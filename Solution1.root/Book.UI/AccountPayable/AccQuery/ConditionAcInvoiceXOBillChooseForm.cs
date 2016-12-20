using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.AccountPayable.AccQuery
{
    public partial class ConditionAcInvoiceXOBillChooseForm : Book.UI.Query.ConditionAChooseForm
    {
        private ConditionAcInvoiceXOBill ConditionAcInvoiceXObill;

        public override Book.UI.Query.Condition Condition
        {
            get
            {
                return this.ConditionAcInvoiceXObill;
            }
            set
            {
                base.Condition = value as ConditionAcInvoiceXOBill;
            }
        }

        public ConditionAcInvoiceXOBillChooseForm()
        {
            InitializeComponent();
            this.nccCustomerStart.EditValueChanged += new EventHandler(nccCustomerStart_EditValueChanged);
            this.nccCustomerStart.Choose = new Book.UI.Settings.BasicData.Customs.ChooseCustoms();
            this.nccCustomerEnd.Choose = new Book.UI.Settings.BasicData.Customs.ChooseCustoms();
        }

        private void nccCustomerStart_EditValueChanged(object sender, EventArgs e)
        {
            if (this.nccCustomerStart.EditValue != null)
            {
                this.nccCustomerEnd.EditValue = this.nccCustomerStart.EditValue;
            }
        }

        protected override void OnOK()
        {
            if (this.ConditionAcInvoiceXObill == null)
                this.ConditionAcInvoiceXObill = new ConditionAcInvoiceXOBill();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.ConditionAcInvoiceXObill.StartDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.ConditionAcInvoiceXObill.StartDate = this.dateEditStartDate.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.ConditionAcInvoiceXObill.EndDate = global::Helper.DateTimeParse.EndDate;
            }
            else
            {
                this.ConditionAcInvoiceXObill.EndDate = this.dateEditEndDate.DateTime;
            }
            this.ConditionAcInvoiceXObill.StartXOid = txtXOidStart.Text;
            this.ConditionAcInvoiceXObill.EndXOid = txtXOidEnd.Text;
            this.ConditionAcInvoiceXObill.mStartCustomer = this.nccCustomerStart.EditValue as Model.Customer;
            this.ConditionAcInvoiceXObill.mEndCustomer = this.nccCustomerEnd.EditValue as Model.Customer;
        }
    }
}