using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;


namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010   咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-6-9
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q48 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.MPSdetailsManager mpsDetailManager = new Book.BL.MPSdetailsManager();

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q48(ConditionMPS condition)
        {
            InitializeComponent();
            IList<Model.MPSdetails> list = mpsDetailManager.Select(condition.StartCustomer == null ? null : condition.StartCustomer.Id, condition.EndCustomer == null ? null : condition.EndCustomer.Id, condition.StartDate, condition.EndDate);
            if (list == null || list.Count <= 0)
                throw new global::Helper.InvalidValueException("查詢無記錄.");
            if (!global::Helper.DateTimeParse.DateTimeEquls(condition.StartDate, global::Helper.DateTimeParse.NullDate))
                this.xrLabelDateRage.Text += condition.StartDate.ToShortDateString();
            this.xrLabelDateRage.Text += "至：" + condition.EndDate.ToShortDateString();
            this.xrLabelDates.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.xrLabelRepotName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelTitle.Text = Properties.Resources.MPSdetails;
            bindingSource1.DataSource = list;
            this.xrTableMPSid.DataBindings.Add("Text", this.DataSource, Model.MPSdetails.PRO_MPSheaderId);
            this.xrTableMPSdate.DataBindings.Add("Text", this.DataSource, "MPSheader." + Model.MPSheader.PRO_MPSStartDate, "{0:yyyy-MM-dd}");
            //this.xrTableProductID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableStock.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_StocksQuantity);
            this.xrTableXOId.DataBindings.Add("Text", this.DataSource, "MPSheader.InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.xrTablePaiDan.DataBindings.Add("Text", this.DataSource, Model.MPSdetails.PRO_MPSdetailssum);
            //this.xrTableProduce.DataBindings.Add("Text", this.DataSource, "MPSheader", Model.Product.PRO_Id);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.MPSdetails.PRO_ProductUnit);
            this.xrTableCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerShortName); this.xrTableCount.DataBindings.Add("Text", this.DataSource, Model.MPSdetails.PRO_InvoiceXODetailSum);


        }


    }
}
