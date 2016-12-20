using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q56_1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Q56_1()
        {
            InitializeComponent();
            this.xrTableCusProName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableProduceQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProduceQuantity);
            this.xrTableTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProduceTransferQuantity);
            this.InDepotQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProduceInDepotQuantity);

            this.xrTableCellProducePrice.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProducePrice);
            this.xrTableCellProduceMoney.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProduceMoney);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProductUnit);
            this.xrTablePosition.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDescription");
            //开始委外合同单号，现改客户订单号
            this.TCProduceOtherCompactId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_InvoiceCusId);
        }

        private Model.ProduceOtherInDepot produceOtherInDepot = null;
        public Model.ProduceOtherInDepot ProduceOtherInDepot
        {
            get { return produceOtherInDepot; }
            set { produceOtherInDepot = value; }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSourceDetail.DataSource = this.produceOtherInDepot.Details;
        }
    }
}
