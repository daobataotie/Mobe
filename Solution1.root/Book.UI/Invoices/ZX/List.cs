using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.ZX
{
    public partial class List : BaseListForm
    {
        BL.InvoicePackingManager invoicePackingManager = new Book.BL.InvoicePackingManager();
        public List()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        protected override Form GetViewForm()
        {
            Model.InvoicePacking invoicePacking = this.bindingSource1.Current as Model.InvoicePacking;
            if (invoicePacking != null)
                return new EditForm(invoicePacking);
            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            IList<Model.InvoicePacking> list = this.bindingSource1.DataSource as IList<Model.InvoicePacking>;
            return new RO2(list);
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }

        protected override void ShowSearchForm()
        {
            Query.ConditionPacking form = new Book.UI.Query.ConditionPacking();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPK condition = form.Condition;
                this.bindingSource1.DataSource = this.invoicePackingManager.SelectByCondition(condition.StartDate, condition.EndDate, condition.NO, condition.InvoiceOf, condition.ShippedById, condition.ConsigneeId);
            }
            if (this.bindingSource1.DataSource == null || this.bindingSource1.Count < 1)
                MessageBox.Show("無數據", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.gridControl1.RefreshDataSource();
            form.Dispose();
            GC.Collect();
        }

        private void List_Load(object sender, EventArgs e)
        {

        }
    }
}