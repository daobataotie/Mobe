using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Accounting.Report
{
    public partial class XRDateProperty : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtPropertyManager detailManager = new Book.BL.AtPropertyManager();
        public XRDateProperty(ConditionDateProperty condition)
        {
            InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "���ڄeؔ�a��";
            decimal? k = 0;
            IList<Model.AtProperty> list = detailManager.Select(condition.StartDate,condition.EndDate);
            this.xrLabel2.Text = "�б����ڣ�" + DateTime.Now.ToShortDateString();
            this.xrLabel1.Text = "�������䣺" + condition.StartDate.ToShortDateString() + "��" + condition.EndDate.ToShortDateString();
            if (list != null)
            {
                foreach (Model.AtProperty at in list)
                {
                    k += at.DepreciationMoney;
                }

            }
            this.DataSource = list;
            this.xrLabel3.Text = "��Ӌ���~��    " + k.ToString();
            this.xrTableCell8.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_Id);
            this.xrTableCell9.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_PropertyName);
            this.xrTableCell10.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_DepreciationSubject);
            this.xrTableCell11.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_DepreciationMoney);

            this.xrTableCell12.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_Mark);
        }
    }
}
