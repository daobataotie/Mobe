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
    public partial class ConditionPCBoxFootCheck : DevExpress.XtraEditors.XtraForm
    {
        public ConditionPCBoxFootCheck()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.date_End.EditValue = DateTime.Now;
            this.date_Start.EditValue = DateTime.Now.AddDays(-15);
        }

        private void btn_Product_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_Product.EditValue = form.SelectedItem as Model.Product;
            }
            form.Dispose();
            GC.Collect();
        }

        private ConditionPCBoxFoot condition;

        internal ConditionPCBoxFoot Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (condition == null)
                condition = new ConditionPCBoxFoot();
            condition.StartDate = this.date_Start.EditValue == null ? DateTime.Now.AddDays(-15) : this.date_Start.DateTime;
            condition.EndDate = this.date_End.EditValue == null ? DateTime.Now : this.date_End.DateTime;
            condition.InvoiceXOId = this.txt_InvoiceXOId.EditValue == null ? null : this.txt_InvoiceXOId.EditValue.ToString();
            condition.PronoteHeaderId = this.txt_PronoteHeaderId.EditValue == null ? null : this.txt_PronoteHeaderId.EditValue.ToString();
            condition.Product = this.btn_Product.EditValue == null ? null : this.btn_Product.EditValue as Model.Product;
            this.DialogResult = DialogResult.OK;
        }
    }
}