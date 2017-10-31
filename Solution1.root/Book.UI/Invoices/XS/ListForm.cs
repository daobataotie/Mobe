using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.XS
{
    public partial class ListForm : BaseListForm
    {
        private Model.InvoiceXO invoiceXO;
        int tag = 0;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceXSManager();

        }
        public ListForm(Model.InvoiceXO xo)
        {
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceXSManager();
            this.invoiceXO = xo;

        }

        public ListForm(string InvoiceCusId)
            : this()
        {
            this.tag = 1;

            this.bindingSource1.DataSource = ((BL.InvoiceXSManager)this.invoiceManager).SelectDateRangAndWhere(null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, InvoiceCusId, null, null, null);
            this.gridControl1.RefreshDataSource();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        protected override Form GetViewForm()
        {
            Model.InvoiceXS invoice = this.SelectedItem as Model.InvoiceXS;
            if (invoice != null)
                return new EditForm(invoice.InvoiceId);

            return null;
        }
        protected override void ShowSearchForm()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            Query.ConditionXChooseForm f = new Query.ConditionXChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionX con = f.Condition as Query.ConditionX;
                this.bindingSource1.DataSource = ((BL.InvoiceXSManager)this.invoiceManager).SelectDateRangAndWhere(con.Customer1, con.Customer2, con.StartDate, con.EndDate, con.Yjri1, con.Yjri2, con.CusXOId, con.Product, con.XOId1, con.XOId2);
            }
            f.Dispose();
            GC.Collect();
        }
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceXS>);
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