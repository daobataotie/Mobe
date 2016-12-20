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

// �� �� ��: ���              ���ʱ��:2009-6-8
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q18_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCGDetailManager cgDetailManager = new Book.BL.InvoiceCGDetailManager();

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
        public Q18_1()
        {
            InitializeComponent();

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceProductUnit);
            this.xrTableCellGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            //this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PROPERTY_INVOICECGDETAILPRICE, "{0:0}");
            //this.xrTableCellTotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PROPERTY_INVOICECGDETAILMONEY0, "{0:0}");

        }

        private void Q18_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.cgDetailManager.Select(invoice);
        }
    }
}
