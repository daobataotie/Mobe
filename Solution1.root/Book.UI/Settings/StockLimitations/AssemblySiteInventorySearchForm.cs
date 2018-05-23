using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.StockLimitations
{
    public partial class AssemblySiteInventorySearchForm : DevExpress.XtraEditors.XtraForm
    {
        public AssemblySiteInventorySearchForm()
        {
            InitializeComponent();

            this.date_Start.EditValue = DateTime.Now.AddMonths(-1);
            this.date_end.EditValue = DateTime.Now;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProductId { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null)
                this.StartDate = global::Helper.DateTimeParse.NullDate;
            else
                this.StartDate = date_Start.DateTime;
            if (this.date_end.EditValue == null)
                this.EndDate = global::Helper.DateTimeParse.EndDate;
            else this.EndDate = this.date_end.DateTime;
            if (this.btn_Product.EditValue != null)
                this.ProductId = (this.btn_Product.EditValue as Model.Product).ProductId;
            else
                this.ProductId = "";

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Product_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_Product.EditValue = f.SelectedItem;
            }
        }
    }
}