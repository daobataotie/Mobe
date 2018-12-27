using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCFlameRetardant
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProductId { get; set; }
        public string CusXOId { get; set; }

        public ConditionForm()
        {
            InitializeComponent();
            this.date_Start.DateTime = DateTime.Now.AddMonths(-1);
            this.date_End.DateTime = DateTime.Now;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不完整", this.Text, MessageBoxButtons.OK);
                return;
            }
            this.StartDate = this.date_Start.DateTime;
            this.EndDate = this.date_End.DateTime;
            this.ProductId = (this.btn_Product.EditValue as Model.Product) == null ? null : (this.btn_Product.EditValue as Model.Product).ProductId;
            this.CusXOId = this.txt_CusXOId.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Product_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.btn_Product.EditValue = f.SelectedItem as Model.Product;
            }
        }
    }
}