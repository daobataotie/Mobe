using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.PT
{
    public partial class ConditionPT : DevExpress.XtraEditors.XtraForm
    {
        public ConditionPT()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            date_Start.EditValue = DateTime.Now.Date.AddMonths(-1);
            date_End.EditValue = DateTime.Now;

            this.newChooseEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseDepot.Choose = new ChooseDepot();
            this.newChooseDepotIn.Choose = new ChooseDepot();
        }

        public DateTime startdate;
        public DateTime enddate;
        public string invoiceId;
        public string employeeId;
        public string depot;
        public string depotIn;
        public string productId;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null)
            {
                MessageBox.Show("日期不能為空", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.date_End.EditValue == null)
            {
                MessageBox.Show("日期不能為空", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.startdate = this.date_Start.DateTime;
            this.enddate = this.date_End.DateTime;
            this.invoiceId = this.txt_InvoiceId.EditValue == null ? null : this.txt_InvoiceId.EditValue.ToString();
            this.employeeId = this.newChooseEmployee.EditValue == null ? null : this.newChooseEmployee.EditValue.ToString();
            this.depot = this.newChooseDepot.EditValue == null ? null : this.newChooseDepot.EditValue.ToString();
            this.depotIn = this.newChooseDepotIn.EditValue == null ? null : this.newChooseDepotIn.EditValue.ToString();
            this.productId = this.btne_Product.EditValue == null ? null : (this.btne_Product.EditValue as Model.Product).ProductId;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btne_Product.EditValue = f.SelectedItem;
            }
        }
    }
}