using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCSamplingEar
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionForm()
        {
            InitializeComponent();

            this.dateEditStart.EditValue = DateTime.Now.AddMonths(-1);
            this.dateEditEnd.EditValue = DateTime.Now;

            this.buttonEditEnd.Properties.ReadOnly = true;
            this.buttonEditStart.Properties.ReadOnly = true;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public Condition condition { get; set; }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (condition == null)
                condition = new Condition();
            condition.StartDate = dateEditStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : dateEditStart.DateTime;
            condition.EndDate = dateEditEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : dateEditEnd.DateTime;
            condition.StartPId = (buttonEditStart.EditValue as Model.Product) == null ? null : (buttonEditStart.EditValue as Model.Product).Id;
            condition.EndPId = (buttonEditEnd.EditValue as Model.Product) == null ? null : (buttonEditEnd.EditValue as Model.Product).Id;
            condition.InvoiceCusId = this.textEditInvoiceCusid.Text;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEditStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditStart.EditValue = f.SelectedItem as Model.Product;
                this.buttonEditEnd.EditValue = f.SelectedItem as Model.Product;
            }
        }

        private void buttonEditEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditEnd.EditValue = f.SelectedItem as Model.Product;
            }
        }
    }
}