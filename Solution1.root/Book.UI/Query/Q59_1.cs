using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q59_1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Q59_1()
        {
            InitializeComponent();

            this.CellInumber.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_Inumber);
            this.CellDetailSum.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MRSdetailssum);
            this.CellMadeProductName.DataBindings.Add("Text", this.DataSource, "MadeProduct." + Model.Product.PRO_ProductName);
            this.CellMRSdetailsdes.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MRSdetailsdes);
            this.CellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.CellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.CellProductUnit.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_ProductUnit);
        }

        private Model.MRSHeader mrsHeader;

        public Model.MRSHeader MrsHeader
        {
            get { return mrsHeader; }
            set { mrsHeader = value; }
        }

        private void Q59_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this.MrsHeader.Details;
        }
    }
}
