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

// �� �� ��: ���              ���ʱ��:2009-6-13
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q21_2 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXTDetailManager xtDetailManager = new Book.BL.InvoiceXTDetailManager();

        private Model.InvoiceXS invoice = null;

        public Model.InvoiceXS Invoice
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


        //����
        public Q21_2()
        {
            InitializeComponent();

           
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailPrice, "{0:0}");
            this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailMoney0, "{0:0}");

        }

        private void Q21_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.InvoiceXT invoicext = new Book.Model.InvoiceXT();
            invoicext.InvoiceId = this.Invoice.InvoiceId;

            System.Collections.Generic.IList<Model.InvoiceXTDetail> details = this.xtDetailManager.Select(invoicext);

            foreach (Model.InvoiceXTDetail detail in details)
            {
                detail.InvoiceXTDetailMoney0 *= -1;
                detail.InvoiceXTDetailQuantity *= -1;
                detail.InvoiceXTDetailTax *= -1;
                detail.InvoiceXTDetailMoney1 *= -1;
                detail.InvoiceXTDetailDiscount *= -1;
            }

            this.bindingSource1.DataSource = details;
        }
    }
}