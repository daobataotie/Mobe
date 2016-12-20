using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Accounting.Report
{
    public partial class XRSubjectProperty : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtPropertyManager detailManager = new Book.BL.AtPropertyManager();
        public XRSubjectProperty(ConditionSubjectProperty condition)
        {
            InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "科目別財產表";
            decimal? k = 0;
            if (condition.RespectiveSubject != null)
            {
                IList<Model.AtProperty> list = detailManager.Select(condition.RespectiveSubject);
                this.xrLabel2.Text = DateTime.Now.ToShortDateString();
                if (list != null)
                {
                    foreach (Model.AtProperty at in list)
                    {
                        k += at.ObtainRegular;
                    }

                }
                this.DataSource = list;
                this.xrLabel3.Text = "取得原價合計：    " + k.ToString();
                this.xrTableCell8.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_Id);
                this.xrTableCell9.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_ToDate, "{0:yyyy-MM-dd}");
                this.xrTableCell10.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_Often);
                this.xrTableCell11.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_RespectiveSubject);

                this.xrTableCell12.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_ObtainRegular);
                this.xrTableCell13.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_DurableMonths);
                this.xrTableCell14.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_Mark);
            }

        }
    }
}
