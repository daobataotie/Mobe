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

// �� �� ��: ������             ���ʱ��:2009-6-6
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q17_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCTDetailManager detailManager = new Book.BL.InvoiceCTDetailManager();
        private BL.InvoiceCTManager invoiceManager = new Book.BL.InvoiceCTManager();

        private Model.InvoiceCT invoice = null;

        public Model.InvoiceCT Invoice
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
        public Q17_1()
        {
            InitializeComponent();

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product."+Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailPrice, "{0:0}");
            this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailMoney0,"{0:0}");

        }

        private void Q17_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {  
            this.bindingSource1.DataSource = this.detailManager.Select(invoice);
        }

    }
}
