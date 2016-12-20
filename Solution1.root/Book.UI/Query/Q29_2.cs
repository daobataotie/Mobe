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

// �� �� ��: ���              ���ʱ��:2009-6-16
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q29_2 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXTDetailManager xtDetailManager = new Book.BL.InvoiceXTDetailManager();

        private Model.InvoiceXS invoice;

        public Model.InvoiceXS Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }


        /// <summary>
        /// �޲ι��죬��ʼ��
        /// </summary>
        public Q29_2()
        {
            InitializeComponent();

          //  this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_PrimaryKeyId);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellUnitPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailPrice, "{0:0}");
            this.xrTableCell1TotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailMoney0, "{0:0}");

            this.xrTableCellTotalHeji.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalHeji.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalHeji.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalHeji.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailMoney0, "{0:0}");
        }

        //��ӡǰ�����¼�
        private void Q29_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.InvoiceXT invoicext = new Book.Model.InvoiceXT();
            invoicext.InvoiceId = this.invoice.InvoiceId;

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
