using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-6-13
// 修改原因：添加了报表的查询条件以及修改了报表的打印格式
// 修 改 人: 刘永亮                    修改时间:2011-01-24
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q50_2 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceMaterialManager produceMaterialManager = new Book.BL.ProduceMaterialManager();
        private BL.ProduceMaterialdetailsManager detailManager = new BL.ProduceMaterialdetailsManager();
        private BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        private BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();

        private ConditionMaterial MaterialCondidtion;

        public Q50_2(ConditionMaterial condition)
        {
            InitializeComponent();
            this.MaterialCondidtion = condition;
            IList<Model.ProduceMaterial> list = produceMaterialManager.SelectBycondition(condition.StartDate, condition.EndDate, condition.ProduceMaterialId0, condition.ProduceMaterialId1, condition.Product0, condition.Product1, condition.DepartmentId0, condition.DepartmentId1, condition.PronoteHeaderId0, condition.PronoteHeaderId1, condition.CusInvoiceXOId);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException("查詢無記錄.");
            }

            this.xrLabelDates.Text += System.DateTime.Now.ToString("yyyy-MM-dd");
            this.xrLabelRepotName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelData.Text = Properties.Resources.ProduceMaterialInfo;

            this.DataSource = list;

            //this.xrLabelProduceMaterialId.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_ProduceMaterialID);
            //this.xrLabelDepartment.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_WorkhouseName);
            //this.xrLabelPronoteHeaderID.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_InvoiceId);
            //this.xrLabelProduceMaterialDate.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_ProduceMaterialDate, "{0:yyyy-MM-dd}");
            //this.xrLabelProduct.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_ParenProductName, "{0:yyyy-MM-dd}");
            //this.xrSubreport1.ReportSource = new Q50_1();
            //this.xrLabelCusXOId.DataBindings.Add("Text", this.DataSource, "CusXOId");

            //数据加总
            StringBuilder str = new StringBuilder();
            foreach (Model.ProduceMaterial detail in list)
            {
                if (detail.ProduceMaterialID != null)
                    str.Append("'" + detail.ProduceMaterialID + "',");
            }
            IList<Model.ProduceMaterialdetails> detailTotal = (new BL.ProduceMaterialdetailsManager()).SelectTotalByProduceMaterialID(this.MaterialCondidtion.Product0, this.MaterialCondidtion.Product1, str.ToString().Length > 1 ? str.ToString().Substring(0, str.Length - 1) : "''");
            foreach (Model.ProduceMaterialdetails detail in detailTotal)
            {
                this.xrRichText1.Text += "\r\n" + detail.ProductName + " :" + "     总数:" + detail.Materialprocessum + "     已领数量:" + detail.Materialprocesedsum;
            }
        }
    }
}
