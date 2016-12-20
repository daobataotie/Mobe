using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.XJ
{
    public partial class ListForm : BaseListForm
    {
        private BL.InvoiceXJManager _invoicexjManager = new Book.BL.InvoiceXJManager();
        public Model.InvoiceXJ _invoicexj = null;

        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceXJManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            //this.popupMenu2.AddItem(this.barButtonItem1);
            //this.popupMenu2.AddItem(this.barButtonItem2);
            this.bindingSource1.DataSource = this._invoicexjManager.Select(DateTime.Now.AddMonths(-1).Date, DateTime.Now.AddDays(1).Date, null, null, null, null);
        }

        protected override Form GetViewForm()
        {
            Model.InvoiceXJ invoice = this.SelectedItem as Model.InvoiceXJ;
            if (invoice != null)
                return new EditForm(invoice);
            return null;
        }

        protected override void ShowSearchForm()
        {
            ListConditionSearch f = new ListConditionSearch();
            if (DialogResult.OK == f.ShowDialog())
            {
                this.bindingSource1.DataSource = this._invoicexjManager.Select(f._startDate, f._endDate, f._customerid, f._productid, f._invoiceXJId, f._companyid);
            }
        }

        protected override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this._invoicexj = this.bindingSource1.Current as Model.InvoiceXJ;
            this.DialogResult = DialogResult.OK;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            //return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceXJ>);
            return null;
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }

        //private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    string tag = (string)e.Item.Tag;
        //    switch (tag)
        //    {
        //        case "ToXO":
        //            Operations.Open("invoices.xo.edit1", this.MdiParent, this.bindingSource1.Current);
        //            break;
        //        case "ToXS":
        //            Operations.Open("invoices.xs.edit1", this.MdiParent, this.bindingSource1.Current);
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}