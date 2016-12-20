using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q26 : BaseReport
    {
        private BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();

        public Q26(ConditionF condition)
        {            
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.YFZKJYB;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy-MM-dd"), condition.EndDate.ToString("yyyy-MM-dd"));

            System.Collections.Generic.IList<Model.InvoiceCG> list = this.invoiceManager.Select(condition.StartDate,condition.EndDate,condition.StartId,condition.EndId);

            if (list == null || list.Count <= 0) 
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = list;

            this.xrLabelCompanyId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PRO_SupplierId);
            //this.xrLabelCompanyName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);

            this.xrTableCellKind.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_KIND);
            //this.xrTableCellFPHM.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEFPBH);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //this.xrTableCellInvoiceHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEHEJI, "{0:0}");
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEID);
            //this.xrTableCellInvoiceZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrTableCellTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrTableCellWeiFu.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEOWED, "{0:0}");
            this.xrTableCellYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_YIFU, "{0:0}");
            //this.xrTableCellZre.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZRE, "{0:0}");

            this.xrLabelBenQiHeji.Summary.FormatString = "{0:0}";
            this.xrLabelBenQiZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelZRE.Summary.FormatString = "{0:0}";
            this.xrLabelZSE.Summary.FormatString = "{0:0}";
            this.xrLabelTax.Summary.FormatString = "{0:0}";
            this.xrLabelYiShou.Summary.FormatString = "{0:0}";

            this.xrLabelZSE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelZRE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelBenQiZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelBenQiHeji.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelYiShou.Summary.Func = SummaryFunc.Sum;

            this.xrLabelBenQiHeji.Summary.IgnoreNullValues = true;
            this.xrLabelBenQiZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelZRE.Summary.IgnoreNullValues = true;
            this.xrLabelZSE.Summary.IgnoreNullValues = true;
            this.xrLabelTax.Summary.IgnoreNullValues = true;
            this.xrLabelYiShou.Summary.IgnoreNullValues = true;

            this.xrLabelZSE.Summary.Running = SummaryRunning.Group;
            this.xrLabelZRE.Summary.Running = SummaryRunning.Group;
            this.xrLabelBenQiZongJi.Summary.Running = SummaryRunning.Group;
            this.xrLabelBenQiHeji.Summary.Running = SummaryRunning.Group;
            this.xrLabelTax.Summary.Running = SummaryRunning.Group;
            this.xrLabelYiShou.Summary.Running = SummaryRunning.Group;

            //this.xrLabelBenQiHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrLabelBenQiZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrLabelZRE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZRE, "{0:0}");
            //this.xrLabelZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZSE, "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICETAX, "{0:0}");
            this.xrLabelYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_YIFU, "{0:0}");

            //this.GroupHeader1.GroupFields.Add(new GroupField("Company." + Model.Company.PROPERTY_COMPANYID));
        }        
    }
}
