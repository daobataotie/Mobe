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
   // Copyright (C) 2008 - 2010  ����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: �Γ����(�����Γ�һЩԔ����Ϣ��չʾ)
     * �^���˻���w,�L��yһ,������^���^
   // �� �� ����ViewForm
   // �� �� ��: ƈ����                   ���ʱ��:2009-05-10
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   //----------------------------------------------------------------*/
    public partial class ViewForm : BaseViewForm
    {

        #region ׃������Ķ��x
        protected BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();
        protected BL.InvoiceCGDetailManager invoiceDetailManager = new Book.BL.InvoiceCGDetailManager();

        /// <summary>
        /// ���޸ĵĵ���
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