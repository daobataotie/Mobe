using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q53_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private Model.ProduceOtherCompact produceOtherCompact;
        public Model.ProduceOtherCompact ProduceOtherCompact
        {
            get { return produceOtherCompact; }
            set { produceOtherCompact = value; }
        }

        public Q53_1()
        {
            InitializeComponent();

            //Ã÷Ï¸
            // this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_RPProductName);
            this.xrTableCusProName.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_RPCustomerProductName);
            // this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_ProductUnit);
            this.xrTableCellOtherCompactCount.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_OtherCompactCount);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_ProductUnit);
            this.xrTableCancelQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_CancelQuantity);
            this.xrTableInDepotCount.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_InDepotCount);
            this.xrTableJiaoQi.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_JiaoQi, "{0:yyyy-MM-dd}");
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDesc");
        }

        private void Q53_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this.produceOtherCompact.Details;
        }
    }
}
