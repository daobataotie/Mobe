using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.ProduceManager
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-11-19
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class BOMPackageXR : DevExpress.XtraReports.UI.XtraReport
    {
       // private BL.CustomerPackageDetailManager packageDetailManager = new Book.BL.CustomerPackageDetailManager();

        #region 构造函数
        public BOMPackageXR()
        {
            InitializeComponent();
        }

        public BOMPackageXR(Model.BomParentPartInfo bomPart)
            : this()
        {
           
                this.xrLabelProductID.Text = bomPart.Product.Id;
                this.xrLabelProductName.Text = bomPart.Product.ProductName;
                this.xrLabelDataName.Text = bomPart.Product.ProductName+Properties.Resources.BomPackage;
         

            this.xrLabel1.Text = BL.Settings.CompanyChineseName;

            this.xrLabelLossRate.Text = bomPart.LossRate.ToString();
            this.xrLabelSpec.Text = bomPart.Product.ProductSpecification;
            if (bomPart.Customer != null)
            {
                this.xrLabelCustomer.Text = bomPart.Customer.CustomerShortName;
                this.xrLabelCustomerProductName.Text = bomPart.CustomerProductName;
            }

            this.xrLabelDate.Text = DateTime.Now.ToShortDateString(); ;


            //包装信息
          //  this.DataSource =packageDetailManager.GetByPackageId( bomPart.CustomerPackage.CustomerPackageId);

            this.xrTablePackageProductId.DataBindings.Add("Text", this.DataSource, "Product."+Model.Product.PRO_Id);
          //  this.xrTablePackageProductName.DataBindings.Add("Text", this.DataSource, "Product."+Model.Product.PRO_ProductName); this.xrTablePackageProductRate.DataBindings.Add("Text", this.DataSource, Model.CustomerPackageDetail.PROPERTY_CONSUMERATE);
            //this.xrTablePackageRemarks.DataBindings.Add("Text", this.DataSource,Model.CustomerPackageDetail.PROPERTY_DESCRIPTION);
            //this.xrTablePackageProductCount.DataBindings.Add("Text",this.DataSource, Model.CustomerPackageDetail.PROPERTY_QUANTITY);
            //this.xrTablePackageEffectsDate.DataBindings.Add("Text", this.DataSource,Model.CustomerPackageDetail.PROPERTY_EFFECTSDATE, "{0:yyyy-MM-dd}");
            //this.xrTablePackageExpiringDate.DataBindings.Add("Text",this.DataSource, Model.CustomerPackageDetail.PROPERTY_EXPIRINGDATE, "{0:yyyy-MM-dd}");


        }
        #endregion 

    }
}
