using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.FK
{
    public partial class ListForm : BaseListForm
    {
        //protected BL.InvoiceFKManager invoiceManager = new Book.BL.InvoiceFKManager();
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();

            this.invoiceManager = new BL.InvoiceFKManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
        }

        protected override Form GetViewForm()
        {
            if (this.SelectedItem != null)
                return new ViewForm(this.SelectedItem.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceFK>);
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }
    }
}