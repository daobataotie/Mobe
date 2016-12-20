using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.CG
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 單據錄入(包括單據一些詳細信息的展示)
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

        #region 變量對象的定義
        protected BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();
        protected BL.InvoiceCGDetailManager invoiceDetailManager = new Book.BL.InvoiceCGDetailManager();

        /// <summary>
        /// 被修改的单据
        /// </summary>
        protected Book.Model.InvoiceCG invoice = null;
        #endregion

        #region Constructors

        private ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceid)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceid);
            if (this.invoice == null)
                throw new ArgumentNullException();
        }

        public ViewForm(Model.InvoiceCG invoice)
            : this()
        {
            if (invoice == null)
                throw new ArgumentNullException();
            this.invoice = invoice;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditCompany.EditValue = this.invoice.Supplier;
            //this.buttonEditDepot.EditValue = this.invoice.Depot;
            
            //this.spinEditInvoiceTaxRate1.EditValue = this.invoice.InvoiceTaxRate == null ? 5 : this.invoice.InvoiceTaxRate;
            //this.calcEditInvoiceTax1.EditValue = this.invoice.InvoiceTax == null ? 0 : this.invoice.InvoiceTax;
            //this.calcEditInvoiceTotal0.EditValue = this.invoice.InvoiceZongJi == null ? 0 : this.invoice.InvoiceZongJi; ;
            //this.calcEditInvoiceTotal1.EditValue = this.invoice.InvoiceHeJi == null ? 0 : this.invoice.InvoiceHeJi; ;
            //this.calcEditInvoiceZSE.EditValue = this.invoice.InvoiceZSE == null ? 0 : this.invoice.InvoiceZSE; ;

            //this.dateEditInvoicePayTimeLimit.DateTime = this.invoice.InvoicePayTimeLimit.Value;

            this.bindingSource1.DataSource = this.invoice.Details;

        }

        #endregion

        #region Overloaded

        //protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        //{
        //    return new R01(this.invoice.InvoiceId);
        //}

        #endregion

    }
}