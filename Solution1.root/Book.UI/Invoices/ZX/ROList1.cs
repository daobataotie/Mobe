using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Invoices.ZX
{
    public partial class ROList1 : DevExpress.XtraReports.UI.XtraReport
    {
        public ROList1()
        {
            InitializeComponent();
        }

        public ROList1(IList<Model.InvoicePackingDetail> list)
            : this()
        {
            this.DataSource = list;
            this.TCPackingNo.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_HandPackingId);
            this.TCDescription.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_UnitNumReport, "{0:f2}");
            this.TCQuantityUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_ProductUnit);
            this.TCJWeight.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_UnitJWeightReport, "{0:f2}");
            this.TCJWeightUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_WeightUnit);
            this.TCMWeight.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_UnitMWeightReport, "{0:f2}");
            this.TCMWeightUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_WeightUnit);
            this.TCHandBookProductId.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_HandbookProductId);

            this.TCSumQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_PackingNum, "{0:f2}");
            this.TCSumQuantityUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_ProductUnit);
            this.TCSumJWeight.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_AllJweight, "{0:f2}");
            this.TCSumJWeightUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_WeightUnit);
            this.TCSumMWeight.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_AllMWeight, "{0:f2}");
            this.TCSumMWeightUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_WeightUnit);

            this.TCQuantityTotalUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_ProductUnit, "{0:f2}");
            this.TCJWeightTotalUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_WeightUnit, "{0:f2}");
            this.TCMWeightTotalUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_WeightUnit, "{0:f2}");

            this.TCQuantityTotal.Summary.FormatString = "{0:f2}";
            this.TCQuantityTotal.Summary.Func = SummaryFunc.Sum;
            this.TCQuantityTotal.Summary.IgnoreNullValues = true;
            this.TCQuantityTotal.Summary.Running = SummaryRunning.Report;
            this.TCQuantityTotal.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_PackingNum, "{0:f2}");
            this.TCJWeightTotal.Summary.FormatString = "{0:f2}";
            this.TCJWeightTotal.Summary.Func = SummaryFunc.Sum;
            this.TCJWeightTotal.Summary.IgnoreNullValues = true;
            this.TCJWeightTotal.Summary.Running = SummaryRunning.Report;
            this.TCJWeightTotal.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_AllJweight, "{0:f2}");
            this.TCMWeightTotal.Summary.FormatString = "{0:f2}";
            this.TCMWeightTotal.Summary.Func = SummaryFunc.Sum;
            this.TCMWeightTotal.Summary.IgnoreNullValues = true;
            this.TCMWeightTotal.Summary.Running = SummaryRunning.Report;
            this.TCMWeightTotal.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_AllMWeight, "{0:f2}");

        }
    }
}
