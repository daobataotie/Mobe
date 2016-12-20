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

// �� �� ��:  ������             ���ʱ��:2009-6-13
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q28_2 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCTDetailManager cgDetailManager = new Book.BL.InvoiceCTDetailManager();

        private Model.InvoiceCG invoice;

        public Model.InvoiceCG Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }


        /// <summary>
        /// �޲ι���
        /// </summary>
        public Q28_2()
        {
            InitializeComponent();

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellUnitPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailPrice, "{0:0}");
            this.xrTableCell1TotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailMoney0, "{0:0}");

            this.xrTableCellTotalHeji.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalHeji.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalHeji.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalHeji.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailMoney0, "{0:0}");
        
        }

        private void Q28_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.InvoiceCT invoicect = new Book.Model.InvoiceCT();
            invoicect.InvoiceId = this.Invoice.InvoiceId;
            this.bindingSource1.DataSource = this.cgDetailManager.Select(invoicect);
        }

    }
}
