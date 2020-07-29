using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.XO
{
    public partial class ListForm : BaseListForm
    {
        int tag = 0;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceXOManager();

            //进来直接进去条件查询，默认查询时间太长
            this.ShowSearchForm();
            this.tag = 1;
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            IList<Model.InvoiceXO> list = ((BL.InvoiceXOManager)this.invoiceManager).SelectByYJRQCustomEmpCusXOId(null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, null, null, invoiceCusId, null, null, false, false, false, null);

            this.bindingSource1.DataSource = list;
        }

        protected override void LoadInvoices(DateTime datetime1, DateTime datetime2)
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            IList<Model.InvoiceXO> list = ((BL.InvoiceXOManager)this.invoiceManager).SelectByYJRQCustomEmpCusXOId(null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, null, null, null, null, null, false, false, false, null);

            this.bindingSource1.DataSource = list;
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.popupMenu2.AddItem(this.barButtonItem1);
        }

        protected override Form GetViewForm()
        {
            Model.InvoiceXO invoice = this.SelectedItem as Model.InvoiceXO;
            if (invoice != null)
                return new EditForm(invoice.InvoiceId);
            //        return new ViewForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceXO>);
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
                case "ToXS":
                    // Operations.Open("invoices.xs.edit1", this.MdiParent, this.bindingSource1.Current);
                    Invoices.XS.EditForm f = new Book.UI.Invoices.XS.EditForm(this.bindingSource1.Current as Model.InvoiceXO);
                    if (this.bindingSource1.Current != null)
                    {
                        f.ShowDialog(this);

                    }
                    break;
            }
        }
        protected override void ShowSearchForm()
        {
            Query.ConditionXChooseForm f = new Query.ConditionXChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionX condition = f.Condition as Query.ConditionX;
                this.bindingSource1.DataSource = ((BL.InvoiceXOManager)this.invoiceManager).SelectByYJRQCustomEmpCusXOId(condition.Customer1, condition.Customer2, condition.StartDate, condition.EndDate, condition.Yjri1, condition.Yjri2, condition.Employee1, condition.Employee2, condition.XOId1, condition.XOId2, condition.CusXOId, condition.Product, condition.Product2, condition.IsClose, false, false, condition.HandBookId);
               
            }
            f.Dispose();
            GC.Collect();
        }
    }
}