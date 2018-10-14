using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Settings.ProduceManager
{
    public partial class ROBOM : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.BomPackageDetailsManager packageManager = new Book.BL.BomPackageDetailsManager();
        private BL.BomComponentInfoManager comManager = new Book.BL.BomComponentInfoManager();
        private BL.TechnologydetailsManager technologydetailsManager = new BL.TechnologydetailsManager();
        private BL.TechonlogyHeaderManager techonlogyHeaderManager = new BL.TechonlogyHeaderManager();

        public ROBOM()
        {
            InitializeComponent();
        }
        public ROBOM(Model.BomParentPartInfo bomParentPartInfo)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = "物料清單";

            this.lblBomId.Text = bomParentPartInfo.Id;
            if (bomParentPartInfo.Product != null)
            {
                lblProductId.Text = bomParentPartInfo.Product.Id;
                lblProductName.Text = bomParentPartInfo.Product.ProductName;

                if (bomParentPartInfo.Product.OutSourcing != null && bomParentPartInfo.Product.OutSourcing.Value)
                    lblSource.Text = Properties.Resources.IsOutSourcing;
                if (bomParentPartInfo.Product.Consume != null && bomParentPartInfo.Product.Consume.Value)
                    lblSource.Text = "耗用";
                if (bomParentPartInfo.Product.TrustOut != null && bomParentPartInfo.Product.TrustOut.Value)
                {
                    lblSource.Text = "委外";
                    if (bomParentPartInfo.Product.IsProcee != null && bomParentPartInfo.Product.IsProcee.Value)
                        this.lblSource.Text = "委外半成品加工";
                }
                if (bomParentPartInfo.Product.HomeMade != null && bomParentPartInfo.Product.HomeMade.Value)
                {
                    lblSource.Text = Properties.Resources.IsHomeMade;
                    if (bomParentPartInfo.Product.IsProcee != null && bomParentPartInfo.Product.IsProcee.Value)
                        this.lblSource.Text = "自製半成品加工";
                }
            }

            IList<Model.BomComponentInfo> list = new List<Model.BomComponentInfo>();
            //IList<Model.BomComponentInfo> bomcomList1 = null;
            IList<Model.BomComponentInfo> bomcomList2 = null;
            IList<Model.BomComponentInfo> bomcomList3 = null;
            IList<Model.BomComponentInfo> bomcomList4 = null;
            IList<Model.BomComponentInfo> bomcomList5 = null;
            IList<Model.BomComponentInfo> bomcomList6 = null;
            IList<Model.BomComponentInfo> bomcomList7 = null;
            IList<Model.BomComponentInfo> bomcomList8 = null;
            IList<Model.BomComponentInfo> bomcomList9 = null;

            foreach (var item1 in bomParentPartInfo.Components)                  //第一层子件
            {
                item1.ReportInumber = (bomParentPartInfo.Components.IndexOf(item1) + 1).ToString();
                list.Add(item1);

                bomcomList2 = comManager.SelectByProductId(item1.ProductId);
                foreach (var item2 in bomcomList2)                               //第二层
                {
                    item2.ReportInumber = item1.ReportInumber + "-" + (bomcomList2.IndexOf(item2) + 1).ToString();
                    list.Add(item2);

                    bomcomList3 = comManager.SelectByProductId(item2.ProductId);
                    foreach (var item3 in bomcomList3)                           //第三层
                    {
                        item3.ReportInumber = item2.ReportInumber + "-" + (bomcomList3.IndexOf(item3) + 1).ToString();
                        list.Add(item3);

                        bomcomList4 = comManager.SelectByProductId(item3.ProductId);
                        foreach (var item4 in bomcomList4)                       //第四层
                        {
                            item4.ReportInumber = item3.ReportInumber + "-" + (bomcomList4.IndexOf(item4) + 1).ToString();
                            list.Add(item4);

                            bomcomList5 = comManager.SelectByProductId(item4.ProductId);
                            foreach (var item5 in bomcomList5)                   //第五层
                            {
                                item5.ReportInumber = item4.ReportInumber + "-" + (bomcomList5.IndexOf(item5) + 1).ToString();
                                list.Add(item5);

                                bomcomList6 = comManager.SelectByProductId(item5.ProductId);
                                foreach (var item6 in bomcomList6)               //第六层
                                {
                                    item6.ReportInumber = item5.ReportInumber + "-" + (bomcomList6.IndexOf(item6) + 1).ToString();
                                    list.Add(item6);

                                    bomcomList7 = comManager.SelectByProductId(item6.ProductId);
                                    foreach (var item7 in bomcomList7)           //第七层
                                    {
                                        item7.ReportInumber = item6.ReportInumber + "-" + (bomcomList7.IndexOf(item7) + 1).ToString();
                                        list.Add(item7);

                                        bomcomList8 = comManager.SelectByProductId(item7.ProductId);
                                        foreach (var item8 in bomcomList8)       //第八层 
                                        {
                                            item8.ReportInumber = item7.ReportInumber + "-" + (bomcomList8.IndexOf(item8) + 1).ToString();
                                            list.Add(item8);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            this.DataSource = list;
            this.TCInumber.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PRO_ReportInumber);
            this.TCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCMaterial.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.TCProductUnit.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_UNIT);
            this.TCUseQuantity.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_USEQUANTITY);
            this.TCSunhaolv.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_SUBLOSERATE);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);

            Model.TechonlogyHeader TechonlogyHeader = this.techonlogyHeaderManager.Get(bomParentPartInfo.TechonlogyHeaderId);
            IList<Model.Technologydetails> Technologydetails=new List<Model.Technologydetails>();
            if(TechonlogyHeader!=null)
                Technologydetails = this.technologydetailsManager.Select(TechonlogyHeader);
            //ROBOMProcess ROBOMProcess = new ROBOMProcess(Technologydetails);
            this.xrSubreport2.ReportSource = new ROBOMProcess(Technologydetails);

            IList<Model.BomPackageDetails> packList = this.packageManager.Select(bomParentPartInfo.BomId);
            //ROBOMPackage ROBOMPackage = new ROBOMPackage(packList);
            this.xrSubreport1.ReportSource = new ROBOMPackage(packList);
        }
    }
}
