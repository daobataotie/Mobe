using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.BY
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 報溢單
   // 文 件 名：ViewForm
   // 编 码 人: 茍波濤                   完成时间:2009-05-08
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ViewForm : BaseViewForm
    {

        #region 變量對象的定義
        protected BL.InvoiceBYManager invoiceBYManager = new Book.BL.InvoiceBYManager();
        protected BL.InvoiceBYDetailManager invoiceDetailManager = new Book.BL.InvoiceBYDetailManager();
        private Model.InvoiceBY invoice;
        #endregion

        #region Constructors

        private ViewForm() 
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceBYManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoiceBY initInvoiceBY)
            : this()
        {
            if (initInvoiceBY == null)
                throw new ArithmeticException("InvoiceBY");
            this.invoice = initInvoiceBY;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            //this.buttonEditDepot.EditValue = this.invoice.Depot;

            this.bindingSource1.DataSource = this.invoice.Details;
        }
        #endregion

        #region Overloaded

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R01(this.invoice.InvoiceId);
        }

        #endregion
    }
}