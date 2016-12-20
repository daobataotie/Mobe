using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.BS
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  ����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: ��p�εĆΓ�ݔ��
   // �� �� ����ViewForm
   // �� �� ��: ƈ����                   ���ʱ��:2009-05-07
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   //----------------------------------------------------------------*/
    public partial class ViewForm : BaseViewForm
    {

        #region ׃������Ķ��x
        protected BL.InvoiceBSManager invoiceBSManager = new Book.BL.InvoiceBSManager();
        protected BL.InvoiceBSDetailManager invoiceDetailManager = new Book.BL.InvoiceBSDetailManager();
        #endregion

        /// <summary>
        /// ���༭�ĵ���
        /// </summary>
        private Model.InvoiceBS invoice = null;

        #region Constructors    

        private ViewForm() 
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceBSManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoiceBS initInvoiceBS)
            : this()
        {
            if (initInvoiceBS == null)
                throw new ArithmeticException("InvoiceBS");
            this.invoice = initInvoiceBS;
        }

        #endregion
      
        #region From Load

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