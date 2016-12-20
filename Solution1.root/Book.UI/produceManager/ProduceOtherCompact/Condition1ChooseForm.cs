using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;
namespace Book.UI.produceManager.ProduceOtherCompact
{
    public partial class Condition1ChooseForm : ConditionAChooseForm
    {
        string pronoteHeaderId = string.Empty;
        private Condition1 condition;

        public Condition1ChooseForm()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEditEndDate.DateTime = DateTime.Now.Date;
            this.nccCustomer.Choose = new Book.UI.Settings.BasicData.Customs.ChooseCustoms();
            this.nccSupplier.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
        }

        private void buttonEditProHeader_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new PronoteHeader.ChoosePronoteHeaderDetailsForm(1);
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (f.SelectItem == null) return;
            this.buttonEditProHeader.EditValue = f.SelectItem.PronoteHeaderID;
            this.buttonEditProduct.EditValue = f.SelectItem.Product;
        }

        private void buttonEditProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            this.buttonEditProduct.EditValue = f.SelectedItem as Model.Product;
        }

        protected override void OnOK()
        {
            if (condition == null)
                condition = new Condition1();
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

            this.condition.Product = this.buttonEditProduct.EditValue as Model.Product;
            this.condition.mCustomerId = this.nccCustomer.EditValue == null ? "" : (this.nccCustomer.EditValue as Model.Customer).CustomerId;
            this.condition.mSupplierId = this.nccSupplier.EditValue == null ? "" : (this.nccSupplier.EditValue as Model.Supplier).SupplierId;

            this.condition.ProduceOtherCompactId = this.txt_OtherCompactId.Text;
            this.condition.InvoiceCusXOId = this.txt_InvoiceCusXOId.EditValue == null ? null : this.txt_InvoiceCusXOId.EditValue.ToString();
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as Condition1;
            }
        }

        private void nccSupplier_Load(object sender, EventArgs e)
        {

        }
    }
}