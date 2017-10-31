using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class ConditionPCMouldCheckForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionPCMouldCheckForm()
        {
            InitializeComponent();

            this.date_OnlineStart.EditValue = DateTime.Now.AddMonths(-1);
            this.date_OnlineEnd.EditValue = DateTime.Now;
            this.date_CheckStart.EditValue = DateTime.Now.AddMonths(-1);
            this.date_CheckEnd.EditValue = DateTime.Now;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public ConditionPCMouldCheck condition { get; set; }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.condition = new ConditionPCMouldCheck();
            this.condition.OnlineDateStart = this.date_OnlineStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.date_OnlineStart.DateTime;
            this.condition.OnlineDateEnd = this.date_OnlineEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.date_OnlineEnd.DateTime;
            this.condition.CheckDateStart = this.date_CheckStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.date_CheckStart.DateTime;
            this.condition.CheckDateEnd = this.date_OnlineEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.date_CheckEnd.DateTime;
            this.condition.ProductId = this.buttonEditProduct.EditValue == null ? null : (this.buttonEditProduct.EditValue as Model.Product).ProductId;
            this.condition.InvoiceCusId = this.txt_InvoiceCusid.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.buttonEditProduct.EditValue = f.SelectedItem;
        }
    }
}