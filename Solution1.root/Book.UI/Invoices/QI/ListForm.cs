using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.QI
{
    public partial class ListForm : BaseListForm
    {
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceQIManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        protected override Form GetViewForm()
        {
            Model.InvoiceQI invoice = this.SelectedItem as Model.InvoiceQI;
            if (invoice != null)
                return new ViewForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceQI>);
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