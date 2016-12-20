using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.Invoices.CO
{
    public partial class ListForm : BaseListForm
    {
        int tag = 0;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceCOManager();
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = ((BL.InvoiceCOManager)this.invoiceManager).SelectDateRangAndWhere(null, null, null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, invoiceCusId, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, 0);
            this.gridControl1.RefreshDataSource();
        }

        private Model.MRSHeader _mrsHeader;

        public ListForm(Model.MRSHeader mrsheader)
            : this()
        {
            if (produceManager.MRSHeader.EditForm.isDelete)
                this.CanDelete();
            this._mrsHeader = mrsheader;
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.popupMenu2.AddItem(this.barButtonItem1);
        }

        protected override void LoadInvoices(DateTime datetime1, DateTime datetime2)
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            if (this._mrsHeader != null && !string.IsNullOrEmpty(this._mrsHeader.MRSHeaderId))
                this.bindingSource1.DataSource = ((BL.InvoiceCOManager)this.invoiceManager).SelectByMrsHeaderId(this._mrsHeader.MRSHeaderId);
            else
                base.LoadInvoices(this.datetimeBase1, this.datetimeBase2);
        }

        protected override Form GetViewForm()
        {
            Model.InvoiceCO invoice = this.SelectedItem as Model.InvoiceCO;
            if (invoice != null)
                return new EditForm(invoice.InvoiceId);
            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceCO>);
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = (string)e.Item.Tag;
            switch (tag)
            {
                case "ToCG":
                    //  Operations.Open("invoices.cg.edit1", this.MdiParent, this.bindingSource1.Current);
                    Invoices.CG.EditForm f = new Book.UI.Invoices.CG.EditForm(this.bindingSource1.Current as Model.InvoiceCO);
                    if (this.bindingSource1.Current != null)
                    {
                        f.ShowDialog(this);

                    }
                    break;

                default:
                    break;
            }
        }

        protected override void ShowSearchForm()
        {
            Query.ConditionCOChooseForm f = new Query.ConditionCOChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionCO con = f.Condition as Query.ConditionCO;
                this.bindingSource1.DataSource = ((BL.InvoiceCOManager)this.invoiceManager).SelectDateRangAndWhere(con.COStartId, con.COEndId, con.SupplierStart, con.SupplierEnd, con.StartDate, con.EndDate, con.ProductStart, con.ProductEnd, con.CusXOId, con.StartJHDate, con.EndJHDate, con.InvoiceFlag.Value);
            }
            f.Dispose();
            GC.Collect();
        }

        private void barBtn_ListPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow()) return;
            IList<Model.InvoiceCO> mCoList;
            mCoList = (from Model.InvoiceCO co in this.bindingSource1.DataSource as IList<Model.InvoiceCO>
                       where co.IsChecked == true
                       select co).ToList<Model.InvoiceCO>();
            if (mCoList == null && mCoList.Count == 0)
                return;
            ROList mRO = new ROList(mCoList);
            mRO.ShowPreviewDialog();
        }
    }
}