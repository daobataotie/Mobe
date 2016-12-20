using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ExportSendMail
{
    public partial class ChooseConditionExport : Query.ConditionAChooseForm
    {
        private ConditionExport condition;

        public ChooseConditionExport()
        {
            InitializeComponent();
            this.btnEditProduct.Click += new EventHandler(btnEditProduct_Click);

            this.nccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.cmbExpType.SelectedIndex = 0;

        }

        public override Book.UI.Query.Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionExport;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionExport();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            else
                this.condition.StartDate = this.dateEditStartDate.DateTime;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            else
                this.condition.EndDate = this.dateEditEndDate.DateTime;

            this.condition.Product = this.btnEditProduct.EditValue as Model.Product;
            this.condition.Customer = this.nccCustomer.EditValue as Model.Customer;
            this.condition.CusXOId = this.txtInvoiceXOId.Text;
            string mExpType = string.Empty;
            switch (this.cmbExpType.SelectedIndex)
            {
                case -1:
                case 0:
                    mExpType = "";
                    break;
                case 1:
                    mExpType = "ANSI";
                    break;
                case 2:
                    mExpType = "CSA";
                    break;
                case 3:
                    mExpType = "BS/EN";
                    break;
            }
            this.condition.ExpType = mExpType;
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEditProduct.EditValue = form.SelectedItem as Model.Product;
            }
            form.Dispose();
            GC.Collect();
        }
    }
}