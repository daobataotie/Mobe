using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.ProduceManager.ManProcedureProcess
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-25
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class XRpRrocedure : DevExpress.XtraReports.UI.XtraReport
    {
       private Model.BomParentPartInfo _bomparentPartInfo = new Book.Model.BomParentPartInfo();
       //存储管理
       private BL.ProceduresManager proceduresMana = new Book.BL.ProceduresManager();

       #region 构造
       public XRpRrocedure()
        {
            InitializeComponent();
         
        }
        public XRpRrocedure(Model.BomParentPartInfo bomPart):this()
        {               

          

            this.xrLabelCommanyName.Text = BL.Settings.CompanyChineseName ;
            this.xrLabelDataName.Text = "物料加工生產序號";
        

            this.DataSource = proceduresMana.Select(bomPart);       
            this.xrLabelBOMid.Text = bomPart.Id;
             this.xrLabelProductId.Text = bomPart.Product.Id;
            this.xrLabelBOMPartName.Text = bomPart.Product.ProductName;
            this.xrLabelBomType.Text = bomPart.BomType;
            this.xrLabelDefaultQuantity.Text = bomPart.DefaultQuantity.ToString();
            this.xrLabelEffectiveDate.Text = Convert.ToDateTime(bomPart.EffectiveDate).ToShortDateString();
            this.xrLabelParentsModel.Text = bomPart.Product.ProductSpecification;
            this.xrLabel8BomVersion.Text = bomPart.BomVersion;
            this.xrLabelLossRate.Text = bomPart.LossRate.ToString();
            this.xrLabelBomDescription.Text = bomPart.BomDescription;

            //生产序号信息
            this.xrTableIds.DataBindings.Add("Text", this.DataSource, "Id");
            this.xrTableNames.DataBindings.Add("Text", this.DataSource, "Workhouse.Workhousename");
            this.xrTableProcedurenames.DataBindings.Add("Text", this.DataSource, "Procedurename");
            this.xrTableProceduresates.DataBindings.Add("Text", this.DataSource, "Proceduresate");
            this.xrTableProcedureTypes.DataBindings.Add("Text", this.DataSource, "ProcedureType");
            this.xrTableStartdates.DataBindings.Add("Text", this.DataSource, "Startdate", "{0:yyyy-MM-dd}");
            this.xrTableEnddates.DataBindings.Add("Text", this.DataSource, "Enddate", "{0:yyyy-MM-dd}");
            this.xrTableLeadtimes.DataBindings.Add("Text", this.DataSource, "Leadtime");
            this.xrTableProceduredescriptions.DataBindings.Add("Text", this.DataSource, "Proceduredescription");



        }
       #endregion 

    }
}
