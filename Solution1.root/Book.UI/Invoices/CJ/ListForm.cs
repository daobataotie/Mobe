using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.CJ
{
    public partial class ListForm : BaseListForm
    {
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceCJManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.popupMenu2.AddItem(this.barButtonItem1);
            this.popupMenu2.AddItem(this.barButtonItem2);
        }

        protected override Form GetViewForm()
        {
            Model.InvoiceCJ invoice = this.SelectedItem as Model.InvoiceCJ;
            if (invoice != null)
                return new ViewForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceCJ>);
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = (string)e.Item.Tag;
            switch (tag)
            {
                case "ToCO":
                    Operations.Open("invoices.co.edit1", this.MdiParent, this.bindingSource1.Current);
                    break;

                case "ToCG":
                    Operations.Open("invoices.cg.edit1", this.MdiParent, this.bindingSource1.Current);
                    break;

                default:
                    break;
            }

        }
    }
}