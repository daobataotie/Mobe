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

// 编 码 人: 周欣亮            完成时间:2009-6-12
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q49 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.PronoteHeaderManager detailManager = new Book.BL.PronoteHeaderManager();
        IList<Model.PronoteHeader> headerList1 = new List<Model.PronoteHeader>();
        BL.PronotedetailsMaterialManager materialManager = new Book.BL.PronotedetailsMaterialManager();
        private ConditionPronoteHeader condition;
        /// <summary>
        /// 构造函数，初始化 加工单
        /// </summary>
        /// <param name="condition"></param>
        public Q49(ConditionPronoteHeader condition)
        {       
           
            InitializeComponent();
            this.condition = condition;
            IList<Model.PronoteHeader> list = detailManager.GetByDateMa(condition.StartDate, condition.EndDate, condition.Customer, condition.CusXOId, condition.Product, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd, condition.SourceTpye, null, false, condition.ProNameKey, condition.ProCusNameKey, condition.PronoteHeaderIdKey);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            this.xrLabelRepotName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.PronoteHeaderDetails;
            if (list != null)
            {
                foreach (Model.PronoteHeader ph in list)
                {
                    Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(ph.InvoiceXOId);
                    if (xo != null)
                    {
                        ph.CustomerShortName = xo.xocustomer.CustomerShortName;
                        ph.CustomerInvoiceXOId = xo.CustomerInvoiceXOId;
                    }
                    headerList1.Add(ph);
                }
            }
            this.DataSource = headerList1;     
            //生產通知
            this.xrLabelPronoteHeaderID.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_PronoteHeaderID);
            this.xrLabelPronoteDte.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_PronoteDate, "{0:yyyy-MM-dd}");
            this.xrLabelEmployee.DataBindings.Add("Text", this.DataSource, "Employee0Name");
            this.xrLabelProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.xrLabelCustomerProductName.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            this.xrLabelCustomer.DataBindings.Add("Text", this.DataSource, "CustomerShortName");
            this.xrLabelCustomerXOId.DataBindings.Add("Text", this.DataSource, "CustomerInvoiceXOId");
            this.xrLabelCount.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_DetailsSum);
            this.xrLabelUnit.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_ProductUnit);
            this.xrLabelPronotedesc.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_Pronotedesc);
            this.xrRichTextProDesc.DataBindings.Add("Rtf", this.DataSource, "ProductDesc");
            this.xrSubreport1.ReportSource = new Q49_1();
            this.xrSubreport2.ReportSource = new Q49_2();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q49_1 material = this.xrSubreport1.ReportSource as Q49_1;
            material.Pronote = this.GetCurrentRow() as Model.PronoteHeader;
            material.Pronote.DetailsMaterial= materialManager.selectByHeaderIdAndPid(  material.Pronote.PronoteHeaderID,condition.Product==null?null:condition.Product.ProductName,condition.Product==null?null:condition.Product.ProductName);
        }
        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q49_2 material = this.xrSubreport2.ReportSource as Q49_2;
            material.Pronote = this.GetCurrentRow() as Model.PronoteHeader;
        }

    }
}
