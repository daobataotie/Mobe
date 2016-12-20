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

// 编 码 人: 裴盾              完成时间:2009-6-14
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q29 : BaseReport
    {
        private BL.InvoiceXSManager invoiceManaager = new Book.BL.InvoiceXSManager();


        //构造函数
        public Q29(ConditionF conditioon)
        {
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.YSZKDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, conditioon.StartDate.ToString("yyyy-MM-dd"), conditioon.EndDate.ToString("yyyy-MM-dd"));

            this.xrLableAddress.Text = BL.Settings.CompanyAddress1;
            this.xrLabelCpyFax.Text = string.Format("Fax:{0}", BL.Settings.CompanyFax);
            this.xrLabelCpyTel.Text = string.Format("Tel:{0}", BL.Settings.CompanyPhone);

            System.Collections.Generic.IList<Model.InvoiceXS> list = this.invoiceManaager.Select(conditioon.StartDate, conditioon.EndDate, conditioon.StartId, conditioon.EndId);

            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.xrLabelCustomId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PRO_CustomerId);
            //this.xrLabelCustomName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);
            //this.xrLabelFax.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYFAX);
            //this.xrLabelLinkMan.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYCONTACT);
            //this.xrLabelTel.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYPHONE);
            //this.xrLabelToYiBianHao.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNUMBER);

            this.bindingSource1.DataSource = list;
            //this.xrTableCellFphm.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEFPBH);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEID);
            this.xrTableCellKind.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_KIND);

            this.xrLabelZRE.Summary.FormatString = "{0:0}";
            this.xrLabelZSE.Summary.FormatString = "{0:0}";
            this.xrLabelYiShou.Summary.FormatString = "{0:0}";
            this.xrLabelBeqiHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelBenQiZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTax.Summary.FormatString = "{0:0}";

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

            //this.xrLabelZRE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZRE, "{0:0}");
            //this.xrLabelZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZSE, "{0:0}");
            this.xrLabelYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_YISHOU, "{0:0}");
            //this.xrLabelBeqiHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrLabelBenQiZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");

            this.GroupHeader1.GroupFields.Add(new GroupField("Company." + Model.InvoiceXS.PRO_CustomerId));
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.InvoiceXS invoicexs = this.GetCurrentRow() as Model.InvoiceXS;

            switch (invoicexs.Kind)
            {
                case "銷退":
                    Q29_2 q29_2_1 = new Q29_2();
                    this.xrSubreport1.ReportSource = q29_2_1;
                    q29_2_1.Invoice = invoicexs;
                    break;
                case "銷售":
                    Q29_1 q29_1_1 = new Q29_1();
                    this.xrSubreport1.ReportSource = q29_1_1;
                    q29_1_1.Invoice = invoicexs;
                    break;
                case "销售":
                    Q29_1 q29_1_2 = new Q29_1();
                    this.xrSubreport1.ReportSource = q29_1_2;
                    q29_1_2.Invoice = invoicexs;
                    break;
                case "销退":
                    Q29_2 q29_2_2 = new Q29_2();
                    this.xrSubreport1.ReportSource = q29_2_2;
                    q29_2_2.Invoice = invoicexs;
                    break;
                default:
                    break;
            }
        }
    }
}
