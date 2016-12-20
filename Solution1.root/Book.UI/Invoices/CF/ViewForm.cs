using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.CF
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 組裝單(包括入庫出庫的一些詳細信息的展示)
     * 繼承了基類窗體,風格統一,介面比較美觀
   // 文 件 名：ViewForm
   // 编 码 人: 茍波濤                   完成时间:2009-05-10
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ViewForm : BaseViewForm
    {
        #region 變量對象定義
        protected BL.InvoiceZZDetailManager invoiceDetailManager = new Book.BL.InvoiceZZDetailManager();
        protected BL.InvoiceZZManager invoiceZZManager = new Book.BL.InvoiceZZManager();
        protected Model.InvoiceZZ invoice;
        #endregion

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceZZManager.Get(invoiceId);
            if (this.invoice == null)
            {
                throw new ArithmeticException("invoiceid");
            }
        }
        public ViewForm(Model.InvoiceZZ initZZ)
            : this()
        {
            if (initZZ == null)
            {
                throw new ArithmeticException("invoiceid");
            }
            this.invoice = initZZ;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.DetailsIn = invoiceDetailManager.Select("I", this.invoice);
            this.invoice.DetailsOut = invoiceDetailManager.Select("O", this.invoice);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.bindingSourceIn.DataSource = this.invoice.DetailsIn;
            this.bindingSourceOut.DataSource = this.invoice.DetailsOut;
        }

        #endregion

        #region Overloaded

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
            //return new R01(this.invoice.InvoiceId);
        }

        #endregion
    }
}