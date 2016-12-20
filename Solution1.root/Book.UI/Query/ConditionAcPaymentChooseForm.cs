using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    public partial class ConditionAcPaymentChooseForm : Book.UI.Query.ConditionAChooseForm
    {
        private ConditionAcPayment condition;
        public ConditionAcPaymentChooseForm()
        {
            InitializeComponent();
            this.newChooseSup1.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseSup2.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionAcPayment;
            }
        }

        protected override void OnOK1()
        {
            if (this.condition == null)
                this.condition = new ConditionAcPayment();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEditStartDate.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEditEndDate.DateTime;
            }

            this.condition.StartSupplier = this.newChooseSup1.EditValue as Model.Supplier;
            this.condition.EndSupplier = this.newChooseSup2.EditValue as Model.Supplier;
            this.condition.StartAcInvoiceCOBill = string.IsNullOrEmpty(this.textEditCOBill2.Text) ? null : this.textEditCOBill2.Text;
            this.condition.EndAcinvoiceCOBill = string.IsNullOrEmpty(this.textEditCOBill1.Text) ? null : this.textEditCOBill1.Text;
            this.condition.StartAcpayment = string.IsNullOrEmpty(this.textEditAcPayment1.Text) ? null : this.textEditAcPayment1.Text;
            this.condition.EndAcpayment = string.IsNullOrEmpty(this.textEditAcPayment2.Text) ? null : this.textEditAcPayment2.Text;
        }
    }
}
