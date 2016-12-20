using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;

namespace Book.UI.Invoices.BS
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 報損單一覽表
   // 文 件 名：ListForm
   // 编 码 人: 茍波濤                   完成时间:2009-05-07
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ListForm : BaseListForm
    {
        //protected BL.InvoiceBSManager invoiceManager = new Book.BL.InvoiceBSManager();

        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();

            this.invoiceManager = new BL.InvoiceBSManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
        }

        protected override Form GetViewForm()
        {
            Model.InvoiceBS invoice = this.SelectedItem as Model.InvoiceBS;
            if (invoice != null)
                return new ViewForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceBS>);
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