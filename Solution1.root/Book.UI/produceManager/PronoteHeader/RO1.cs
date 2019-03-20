using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PronoteHeader
{
    public partial class RO1 : DevExpress.XtraReports.UI.XtraReport
    {

        private BL.MRSdetailsManager mrsdetailsManager = new BL.MRSdetailsManager();
        private IList<Model.BomComponentInfo> _comDetailss = new List<Model.BomComponentInfo>();
        private IList<Model.BomComponentInfo> _comDetails = new List<Model.BomComponentInfo>();
        private Model.BomParentPartInfo _bomParmentPartInfo = new Book.Model.BomParentPartInfo();
        private Model.BomParentPartInfo _bomparent = new Book.Model.BomParentPartInfo();
        private BL.BomComponentInfoManager BomComManager = new Book.BL.BomComponentInfoManager();
        private BL.BomParentPartInfoManager bomParmentInfoManager = new Book.BL.BomParentPartInfoManager();
        private BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        private BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private BL.PronotedetailsMaterialManager pronotedetailsMaterialManager = new Book.BL.PronotedetailsMaterialManager();

        public RO1()
        {
            InitializeComponent();

            //展开 数据源_comDetailss
            //// this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            //this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellDetailsSum.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_USEQUANTITY, "{0:0.####}");
            //// this.xrTableCellMPSHeaderId.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_MPSheaderId);
            //this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_UNIT);
            ////  this.xrTableCellProductStock.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_StringStocksQuantity);
            //this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            //// this.xrTableCellProductSpecification.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            //this.xrTableCellNo.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_INDEXOFBOM);
            //this.xrTableJibie.DataBindings.Add("Text", this.DataSource, "Jibie");
            //this.xrTableCellJiaoQi.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PRO_JiaoQi, "{0:yyyy-MM-dd}");
            ////this.xrTableCellSupplierName.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_SupplierName);


            //不展开 数据源 this.pronoteHeader.DetailsMaterial 
            // this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellDetailsSum.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_PronoteQuantity1, "{0:0.####}");
            // this.xrTableCellMPSHeaderId.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_MPSheaderId);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_ProductUnit);
            //  this.xrTableCellProductStock.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_StringStocksQuantity);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            // this.xrTableCellProductSpecification.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellNo.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_Inumber);
            this.xrTableCellJiaoQi.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_JiaoQi, "{0:yyyy-MM-dd}");
            //this.xrTableCellSupplierName.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_SupplierName);

            this.TCProduct_Id.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
        }

        private Model.PronoteHeader pronoteHeader;

        public Model.PronoteHeader PronoteHeader
        {
            get { return pronoteHeader; }
            set { pronoteHeader = value; }
        }

        private void RO1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.pronoteHeader == null)
                return;
            this.pronoteHeader.DetailsMaterial = pronotedetailsMaterialManager.GetByHeader(this.pronoteHeader);
            this._bomParmentPartInfo = this.bomParmentInfoManager.Get(this.pronoteHeader.Product);
            IList<Model.MRSdetails> mrsdetailList;
            for (int i = 0; i < this.pronoteHeader.DetailsMaterial.Count; i++)
            {
                mrsdetailList = mrsdetailsManager.GetByMRSIDAndProId(this.pronoteHeader.DetailsMaterial[i].PronoteHeader.MRSHeaderId, this.pronoteHeader.DetailsMaterial[i].ProductId);
                if (mrsdetailList != null && mrsdetailList.Count > 0)
                {
                    this.pronoteHeader.DetailsMaterial[i].JiaoQi = mrsdetailList[0].JiaoHuoDate;

                }
            }
            //  this.DataSource = XRband();           
            this.DataSource = this.pronoteHeader.DetailsMaterial;
        }
     
    }
}
