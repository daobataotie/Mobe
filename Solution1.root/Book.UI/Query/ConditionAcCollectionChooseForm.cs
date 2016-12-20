using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    public partial class ConditionAcCollectionChooseForm : Book.UI.Query.ConditionAChooseForm
    {
        private ConditionAcCollection condition;
        public ConditionAcCollectionChooseForm()
        {
            InitializeComponent();
            this.newChooseCustomer1.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseCustomer2.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionAcCollection;
            }
        }

        protected override void OnOK1()
        {
            if (this.condition == null)
                this.condition = new ConditionAcCollection();

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

            this.condition.StartCustomer = this.newChooseCustomer1.EditValue as Model.Customer;
            this.condition.EndCustomer = this.newChooseCustomer2.EditValue as Model.Customer;
            this.condition.StartAcInvoiceXOBill = string.IsNullOrEmpty(this.textEditXOBill1.Text) ? null : this.textEditXOBill1.Text;
            this.condition.EndAcInvoiceXOBill = string.IsNullOrEmpty(this.textEditXOBill2.Text) ? null : this.textEditXOBill2.Text;
            this.condition.StartAcCollection = string.IsNullOrEmpty(this.textEditAcCollection1.Text) ? null : this.textEditAcCollection1.Text;
            this.condition.EndAcInvoiceXOBill = string.IsNullOrEmpty(this.textEditAcCollection2.Text) ? null : this.textEditAcCollection2.Text;
        }
    }
}
