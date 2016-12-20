using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 够波涛             完成时间:2009-6-20
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q36 : BaseReport
    {

        //构造
        public Q36(ConditionI condition)
        {
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.YSZKYJBQ;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.CustomOrSupplierRange, condition.StartCompanyId, condition.EndCompanyId);

            //System.Collections.Generic.IList<Model.Company> list = this.manager.Select(condition.StartCompanyId, condition.EndCompanyId, global::Helper.CompanyKind.Customer);

            //if (list == null || list.Count <= 0)
            //    throw new Helper.InvalidValueException();
            //this.bindingSource1.DataSource = list;

            //this.xrLabelAddress.DataBindings.Add("Text", this.DataSource, Model.Company.PROPERTY_COMPANYADDRESS);
            //this.xrLabelCompanyName.DataBindings.Add("Text", this.DataSource, Model.Company.PROPERTY_COMPANYNAME1);
            
        }

    }
}
