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

// 编 码 人: 裴盾            完成时间:2009-11-23
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class BomProcessXR : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造
        public BomProcessXR()
        {
            InitializeComponent();
        }
        public BomProcessXR(Model.BomParentPartInfo bomPart)
            : this()
        {

            this.xrLabelCommanyName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.BomPackage;
            if (bomPart.Components.Count == 0) return;
            this.DataSource = bomPart.Components;
            this.xrLabelBOMid.Text = bomPart.Product.Id;
            this.xrLabelBOMPartName.Text = bomPart.Product.ProductName;
            this.xrLabelBomType.Text = bomPart.BomType;
            this.xrLabelDefaultQuantity.Text = bomPart.DefaultQuantity.ToString();
            this.xrLabelEffectiveDate.Text = Convert.ToDateTime(bomPart.EffectiveDate).ToShortDateString();
            this.xrLabelParentsModel.Text = bomPart.Product.ProductSpecification;
            this.xrLabel8BomVersion.Text = bomPart.BomVersion;
            this.xrLabelLossRate.Text = bomPart.LossRate.ToString();
            this.xrLabelBomDescription.Text = bomPart.BomDescription;

            //包装信息
            this.DataSource = bomPart.BOMProductProcess;
        
            this.xrTableProcessId.DataBindings.Add("Text", this.DataSource, "BOMProductProcessId");           
            this.xrTableProcessNote.DataBindings.Add("Text", this.DataSource, "Process.Description");
            this.xrTableProcessType.DataBindings.Add("Text", this.DataSource, "ProcessCategory.ProcessCategoryName");
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Process.Content");

        }
        #endregion


    }
}
