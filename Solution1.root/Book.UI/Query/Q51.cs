using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Query
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-6-15
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q51 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.ProduceMaterialExitDetailManager detailManager = new Book.BL.ProduceMaterialExitDetailManager();

        #region 构造函数
        public Q51(Book.UI.produceManager.ProduceMaterialExit.ConditionForListCls condition)
        {
            InitializeComponent();
            //IList<Model.ProduceMaterialExitDetail> list = detailManager.SelectBycondition(condition.StartDate, condition.EndDate, condition.ProduceMaterialId0, condition.ProduceMaterialId1, condition.Product0, condition.Product1, condition.DepartmentId0, condition.DepartmentId1, condition.PronoteHeaderId0, condition.PronoteHeaderId1);
            IList<Model.ProduceMaterialExitDetail> list = detailManager.SelectForListForm(condition.StartDate, condition.EndDate, condition.StartPMEid, condition.EndPMEid, condition.StartPronoteHeaderId, condition.EndPronoteHeaderId, condition.StartProduct, condition.EndProduct, condition.WorkhouseId, condition.InvocieXOCusId, condition.HandBookId);
            if (list == null || list.Count <= 0)
            {
                    throw new global::Helper.InvalidValueException("無數據");
            }

            if (!global::Helper.DateTimeParse.DateTimeEquls(condition.StartDate, global::Helper.DateTimeParse.NullDate))
                this.xrLabelDateRange.Text = "自 " + condition.StartDate.ToString("yyyy-MM-dd");
            this.xrLabelDateRange.Text += "至 " + condition.EndDate.ToString("yyyy-MM-dd");
            this.xrLabelRepotName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.ProduceMaterialExitDetail;
            this.DataSource = list;


            this.xrTableDate.DataBindings.Add("Text", this.DataSource, "ProduceMaterialExit." + Model.ProduceMaterialExit.PRO_ProduceExitMaterialDate, "{0:yyyy-MM-dd}");
            this.xrTableProName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableQuanTity.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialExitDetail.PRO_ProduceQuantity);

            this.xrTableMaterialExitId.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialExitDetail.PRO_ProduceMaterialExitId);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialExitDetail.PRO_ProductUnit);
            this.xrTableDepot.DataBindings.Add("Text", this.DataSource, "ProduceMaterialExit.Depot." + Model.Depot.PRO_DepotName);
            this.xrTableDepotPosition.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            this.TCInvoiceXOCusId.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);

        }
        #endregion
    }
}
