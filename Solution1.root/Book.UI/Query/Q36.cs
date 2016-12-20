using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ������             ���ʱ��:2009-6-20
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q36 : BaseReport
    {

        //����
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
