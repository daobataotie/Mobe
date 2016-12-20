using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Accounting.Report
{
    public partial class XRProperty : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtPropertyManager detailManager = new Book.BL.AtPropertyManager();
        public XRProperty(ConditionProperty condition)
        {
            InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "Ø”®a„eÕÛÅf±í";
            decimal? k = 0;
            IList<Model.AtProperty> list = detailManager.SelectByPropertyId(condition.StartPropertyId, condition.EndPropertyId);
            this.xrLabel2.Text = DateTime.Now.ToShortDateString();
            if (list != null)
            {
                foreach (Model.AtProperty at in list)
                {
                    k += at.DepreciationMoney;
                }

            }
            this.DataSource = list;
            this.xrLabel3.Text = "ºÏÓ‹½ðî~£º    " + k.ToString();
            this.xrTableCell8.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_Id);
            this.xrTableCell9.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_PropertyName);
            this.xrTableCell10.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_DepreciationDate, "{0:yyyy-MM-dd}");
            this.xrTableCell11.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_DepreciationMoney);

            this.xrTableCell12.DataBindings.Add("Text", this.DataSource, Model.AtProperty.PRO_Mark);
        }

    }
}
