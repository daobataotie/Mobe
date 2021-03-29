using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��:  ������             ���ʱ��:2009-6-16
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/

    public partial class Q23_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXTDetailManager detailManager = new Book.BL.InvoiceXTDetailManager();
        private BL.InvoiceXTManager invoiceManager = new Book.BL.InvoiceXTManager();

        private Model.InvoiceXT invoice = null;

        public Model.InvoiceXT Invoice
        {
            get
            {
                return this.invoice;
            }
            set
            {
                this.invoice = value;
            }
        }


        /// <summary>
        /// �޲ι���
        /// </summary>
        public Q23_1()
        {
            InitializeComponent();

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TC_CustomerProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailPrice, "{0:0.000}");
            this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailMoney0, "{0:0.000}");

            this.TC_InvoiceCusXOId.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);

        }

        private void Q23_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.detailManager.Select(invoice);
        }

    }
}
