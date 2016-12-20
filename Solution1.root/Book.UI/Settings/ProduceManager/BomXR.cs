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

// 编 码 人: 裴盾            完成时间:2009-11-24
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class BomXR : DevExpress.XtraReports.UI.XtraReport
    {

        #region 构造函数
        private Model.BomParentPartInfo _bomparentPartInfo = new Book.Model.BomParentPartInfo();
        public BomXR()
        {
            InitializeComponent();
        }
        public BomXR(Model.BomParentPartInfo bomPart):this()
        {

            
            this._bomparentPartInfo = bomPart;

            this.xrLabelCommanyName.Text = BL.Settings.CompanyChineseName ;
            this.xrLabelDataName.Text = Properties.Resources.BomParentInfo;
            if(bomPart.Components.Count==0) return ;

            this.DataSource=bomPart.Components;        
            this.xrLabelBOMid.Text = bomPart.Product.Id;
            this.xrLabelBOMPartName.Text = bomPart.Product.ProductName;
            this.xrLabelBomType.Text = bomPart.BomType;
            this.xrLabelDefaultQuantity.Text = bomPart.DefaultQuantity.ToString();
            this.xrLabelEffectiveDate.Text = Convert.ToDateTime(bomPart.EffectiveDate).ToShortDateString();
            this.xrLabelParentsModel.Text = bomPart.Product.ProductSpecification;
            this.xrLabel8BomVersion.Text = bomPart.BomVersion;
            this.xrLabelLossRate.Text = bomPart.LossRate.ToString();
            this.xrLabelBomDescription.Text = bomPart.BomDescription;      
            //子件信息
            this.xrTableId.DataBindings.Add("Text",this.DataSource,"Product.Id" );
            this.xrTableUserCount.DataBindings.Add("Text",this.DataSource,"UseQuantity");
            this.xrTableUnit.DataBindings.Add("Text",this.DataSource,"Unit");
            this.xrTableName.DataBindings.Add("Text",this.DataSource,"Product.ProductName" );
            this.xrTableRemarks.DataBindings.Add("Text",this.DataSource,"Remarks" );       
            this.xrTableEffectsDate.DataBindings.Add("Text",this.DataSource,"EffectsDate","{0:yyyy-MM-dd}" );
            this.xrTableExpiringDate.DataBindings.Add("Text", this.DataSource, "ExpiringDate","{0:yyyy-MM-dd}");
            this.xrTableGuiGe.DataBindings.Add("Text",this.DataSource,"Product.ProductSpecification" );
            this.xrTableoffset.DataBindings.Add("Text",this.DataSource,"offset");



        }
        #endregion 



    }
}
