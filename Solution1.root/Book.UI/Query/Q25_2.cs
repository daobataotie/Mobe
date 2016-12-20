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

// �� �� ��:  ������             ���ʱ��:2009-6-8
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q25_2 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceZZDetailManager detailManager = new Book.BL.InvoiceZZDetailManager();


        //�޲ι��죬��ʼ��
        public Q25_2()
        {
            InitializeComponent();

            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILNOTE);
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILQUANTITY, "{0:0}");

            this.xrLabelTotalQuantity.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalQuantity.Summary.IgnoreNullValues = true;
            this.xrLabelTotalQuantity.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalQuantity.Summary.FormatString = Properties.Resources.QuantityHeJi;
            this.xrLabelTotalQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILQUANTITY, Properties.Resources.QuantityHeJi);
        }
        private Model.InvoiceZZ invoice;

        public Model.InvoiceZZ Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }

        private void Q25_2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this.detailManager.Select("I", this.invoice);
        }
    }
}
