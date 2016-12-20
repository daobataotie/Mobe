using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-6-12
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q28 : BaseReport
    {

        private BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();

        /// <summary>
        /// 一参构造
        /// </summary>
        /// <param name="conditioon"></param>
        public Q28(ConditionF conditioon)
        {
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.YFZKDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, conditioon.StartDate.ToString("yyyy-MM-dd"), conditioon.EndDate.ToString("yyyy-MM-dd"));
                        
            this.xrLableAddress.Text = BL.Settings.CompanyAddress1;
            this.xrLabelCpyFax.Text = string.Format("Fax:{0}", BL.Settings.CompanyFax);
            this.xrLabelCpyTel.Text = string.Format("Tel:{0}", BL.Settings.CompanyPhone);

            System.Collections.Generic.IList<Model.InvoiceCG> list = this.invoiceManager.Select(conditioon.StartDate, conditioon.EndDate, conditioon.StartId, conditioon.EndId);

            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }            

            this.bindingSource1.DataSource = list;

            this.xrLabelCustomId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PRO_SupplierId);
            //this.xrLabelCustomName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);
            //this.xrLabelFax.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYFAX);
            //this.xrLabelLinkMan.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYCONTACT);
            //this.xrLabelTel.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYPHONE);
            //this.xrLabelToYiBianHao.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNUMBER);

            //this.xrTableCellFphm.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEFPBH);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEID);
            this.xrTableCellKind.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_KIND);
            //this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_Id);
            //this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PROPERTY_INVOICEDETAILQUANTITY);
            //this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PROPERTY_PRODUCTBASEUNIT);
            //this.xrTableCellUnitPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PROPERTY_INVOICEDETAILPRICE, "{0:0}");
            //this.xrTableCell1TotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PROPERTY_INVOICEDETAILMONEY0, "{0:0}");

            this.xrLabelZRE.Summary.FormatString="{0:0}";
            this.xrLabelZSE.Summary.FormatString="{0:0}";
            this.xrLabelYiShou.Summary.FormatString="{0:0}";
            this.xrLabelBeqiHeJi.Summary.FormatString="{0:0}";
            this.xrLabelBenQiZongJi.Summary.FormatString="{0:0}";
            this.xrLabelTax.Summary.FormatString="{0:0}";

            this.xrLabelZRE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelZSE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelYiShou.Summary.Func = SummaryFunc.Sum;
            this.xrLabelBeqiHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelBenQiZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTax.Summary.Func = SummaryFunc.Sum;

            this.xrLabelZRE.Summary.IgnoreNullValues = true;
            this.xrLabelZSE.Summary.IgnoreNullValues = true;
            this.xrLabelYiShou.Summary.IgnoreNullValues = true;
            this.xrLabelBeqiHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelBenQiZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTax.Summary.IgnoreNullValues = true;

            this.xrLabelZRE.Summary.Running = SummaryRunning.Group;
            this.xrLabelZSE.Summary.Running = SummaryRunning.Group;
            this.xrLabelYiShou.Summary.Running = SummaryRunning.Group;
            this.xrLabelBeqiHeJi.Summary.Running = SummaryRunning.Group;
            this.xrLabelBenQiZongJi.Summary.Running = SummaryRunning.Group;
            this.xrLabelTax.Summary.Running = SummaryRunning.Group;

            //this.xrLabelZRE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZRE, "{0:0}");
            //this.xrLabelZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZSE, "{0:0}");
            this.xrLabelYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_YIFU, "{0:0}");
            //this.xrLabelBeqiHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrLabelBenQiZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICETAX, "{0:0}");

            this.GroupHeader1.GroupFields.Add(new GroupField("Company." + Model.InvoiceCG.PRO_SupplierId));
        }


        //打印前触发
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.InvoiceCG invoicecg = this.GetCurrentRow() as Model.InvoiceCG;

            switch (invoicecg.Kind)
            {
                case "进退":
                    Q28_2 q28_2_1 = new Q28_2();
                    this.xrSubreport1.ReportSource = q28_2_1;
                    q28_2_1.Invoice = invoicecg;
                    break;
                case "採退":
                    Q28_2 q28_2_2 = new Q28_2();
                    this.xrSubreport1.ReportSource = q28_2_2;
                    q28_2_2.Invoice = invoicecg;
                    break;
                case "进货":
                    Q28_1 q28_1_1 = new Q28_1();
                    this.xrSubreport1.ReportSource = q28_1_1;
                    q28_1_1.Invoice = invoicecg;
                    break;
                case "採購":
                    Q28_1 q28_1_2 = new Q28_1();
                    this.xrSubreport1.ReportSource = q28_1_2;
                    q28_1_2.Invoice = invoicecg;
                    break;
                default:
                    break;
            }
        }
    }
}
