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
     *    生成入库明细

// 编 码 人: 马艳军            完成时间:2009-6-17
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q52 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceInDepotDetailManager detailManager = new Book.BL.ProduceInDepotDetailManager();

        #region 构造
        public Q52(ConditionProInDepotChoose condition)
        {
            InitializeComponent();

            IList<Model.ProduceInDepotDetail> list = detailManager.Select(condition.StartPronoteHeader, condition.EndPronoteHeader, condition.StartDate, condition.EndDate, condition.Product, condition.WorkHouse, condition.MDepot, condition.MDepotPosition, condition.Id1, condition.Id2, condition.Cusxoid, condition.Customer1, condition.Customer2, condition.ProductState, condition.HandBookId);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.MessageValueException("無數據！");
            }

            if (!global::Helper.DateTimeParse.DateTimeEquls(condition.EndDate, global::Helper.DateTimeParse.NullDate))
                this.xrLabelDateRange.Text += " " + condition.StartDate.ToString("yyyy-MM-dd");
            this.xrLabelDateRange.Text += " 至：" + condition.EndDate.ToString("yyyy-MM-dd");
            this.xrLabelDates.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.ReportName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.ProduceInDepotDetail;
            this.DataSource = list;


            this.xrTablInDepotId.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceInDepotId);
            this.xrTableDate.DataBindings.Add("Text", this.DataSource, "HeaderDate", "{0:yyyy-MM-dd}");
            //this.xrTableProID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            //this.xrTableProName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableQuanTity.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceQuantity);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProductUnit);
            //this.xrTableDepot.DataBindings.Add("Text", this.DataSource, "ProduceInDepot.Depot." + Model.Depot.PRO_DepotName);
            //this.TCInvoiceCusXOId.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);

            //2017年10月27日21:19:57
            this.TCPNTId.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_PronoteHeaderId);
            this.xrTableProName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TCDepartment.DataBindings.Add("Text", this.DataSource, "Workhousename");
            this.TCProduceQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProceduresSum);
            this.TCTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceTransferQuantity);
            this.xrTableDepot.DataBindings.Add("Text", this.DataSource, "DepotName");
            this.TCInvoiceCusXOId.DataBindings.Add("Text", this.DataSource, "CusXOId");


            //this.xrTableZongJi.Summary.FormatString = "{0:0}";
            //this.xrTableZongJi.Summary.Func = SummaryFunc.Sum;
            //this.xrTableZongJi.Summary.Running = SummaryRunning.Report;
            //this.xrTableZongJi.Summary.IgnoreNullValues = true;

            //this.xrTableZongJi.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceMoney, "{0:c0}");
        }
        #endregion

    }
}
