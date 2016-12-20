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
        //# region
        //public IList<Model.BomComponentInfo> XRband()
        //{                    

        //     Model.MRSHeader mrp= this.mRSHeaderManager.Get(this.pronoteHeader.MRSHeaderId);           

        //     IList<Model.MRSdetails> mrsdetailList=null;

        //        foreach (Model.PronotedetailsMaterial mater in this.pronoteHeader.DetailsMaterial)
        //        {                 

        //            Model.BomComponentInfo comm = new Model.BomComponentInfo();                   
        //            comm.Jibie = 1;
        //            comm.UseQuantity =mater.PronoteQuantity;
        //            comm.IndexOfBom = mater.Inumber;
        //            comm.Product = mater.Product;
        //            comm.Unit = mater.ProductUnit;
        //            comm.Product.ProductName = string.IsNullOrEmpty(mater.Product.CustomerProductName) ? mater.Product.ProductName : mater.Product.ProductName + "{" + mater.Product.CustomerProductName + "}";
        //            comm.ProductId = mater.ProductId;
        //            comm.JiaoQi = mater.JiaoQi;
        //            _comDetailss.Add(comm);
        //        }                       

        //    if (this._comDetailss.Count != 0&&!string.IsNullOrEmpty(this.pronoteHeader.Product.CustomerProductName))
        //    {
        //        IList<Model.BomComponentInfo> a = null;
        //        string strlenth = "";
        //        for (int i = 0; i < this._comDetailss.Count; i++)
        //        {
        //            if(_comDetailss[i].Product.IsProcee!=true) //非半成品加工跳过
        //                continue;
        //            //在物料中查询 是否 存在此子件

        //            this._bomparent = this.bomParmentInfoManager.Get(_comDetailss[i].Product);
        //            if (this._bomparent != null)
        //            {                        
        //                //交期
        //            if(!string.IsNullOrEmpty( this.pronoteHeader.MRSHeaderId))
        //            {

        //                if(mrp!=null&&!string.IsNullOrEmpty( mrp.MPSheaderId))
        //                {                                         
        //                    mrsdetailList=this.mrsdetailsManager.SelectWhere(" MadeProductId='"+this._bomparent.ProductId+"' and MRSHeaderId in(select MRSHeaderId from MRSHeader where mpsheaderid='"+mrp.MPSheaderId+"') ");

        //                }

        //            }
        //                a = this.BomComManager.Select(this._bomparent);
        //                int m = this._comDetailss.Count;
        //                for (int j = i + 1; j < m; j++)
        //                {
        //                    _comDetails.Add(this._comDetailss[i + 1]);
        //                    this._comDetailss.RemoveAt(i + 1);
        //                }

        //                foreach (Model.BomComponentInfo bom in a)
        //                {
        //                    foreach(Model.MRSdetails mrpdetail in mrsdetailList)
        //                    {
        //                       if(mrpdetail.ProductId==bom.ProductId)
        //                           bom.JiaoQi=mrpdetail.JiaoHuoDate;
        //                    }

        //                    bom.IndexOfBom = null;
        //                    bom.Jibie = _comDetailss[i].Jibie + 1;
        //                    bom.UseQuantity = _comDetailss[i].UseQuantity * bom.UseQuantity*(1+0.01*(bom.SubLoseRate.HasValue?bom.SubLoseRate.Value:0));
        //                    for (int g = 0; g < bom.Jibie; g++)
        //                    {
        //                        strlenth += "   ";
        //                    }
        //                    if (bom.Product != null)
        //                    {
        //                        bom.Product.ProductName = strlenth + (string.IsNullOrEmpty(bom.Product.CustomerProductName) ? bom.Product.ProductName : bom.Product.ProductName + "{" + bom.Product.CustomerProductName + "}");
        //                    }

        //                    this._comDetailss.Add(bom);
        //                    strlenth = "";
        //                }
        //                foreach (Model.BomComponentInfo boms in _comDetails)
        //                {
        //                    this._comDetailss.Add(boms);
        //                }
        //                _comDetails.Clear();
        //                a.Clear();
        //            }
        //        }
        //    }
        //    return _comDetailss;
        //}
        //#endregion

    }
}
