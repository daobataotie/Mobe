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

// �� �� ��: ���             ���ʱ��:2009-6-8
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q18_2 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCTDetailManager ctDetailManager = new Book.BL.InvoiceCTDetailManager();

        private Model.InvoiceCG invoice = null;

        public Model.InvoiceCG Invoice
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

         //�޲ι���
        public Q18_2()
        {
            InitializeComponent();

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailPrice, "{0:0}");
            this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailMoney0, "{0:0}");

        }

        private void Q18_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.InvoiceCT invoicect = new Book.Model.InvoiceCT();
            invoicect.InvoiceId = this.Invoice.InvoiceId;
            System.Collections.Generic.IList<Model.InvoiceCTDetail> details = this.ctDetailManager.Select(invoicect);

            foreach (Model.InvoiceCTDetail detail in details)
            {
                detail.InvoiceCTDetailMoney0 *= -1;
                detail.InvoiceCTDetailQuantity *= -1;
                detail.InvoiceCTDetailTax *= -1;
                detail.InvoiceCTDetailMoney1 *= -1;
                detail.InvoiceCTDetailDisount *= -1;
            }

            this.bindingSource1.DataSource = details;
        }
    }
}