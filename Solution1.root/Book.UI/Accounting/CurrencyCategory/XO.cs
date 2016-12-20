using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Accounting.CurrencyCategory
{
	public partial class XO : DevExpress.XtraReports.UI.XtraReport
	{	
		public XO()
		{
			InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "ÿõé≈∑NÓê";
            this.xrLabelPrintDate.Text = DateTime.Now.ToShortDateString();
            this.DataSource = new BL.AtCurrencyCategoryManager().Select();
            this.xrTableCellId.DataBindings.Add("Text", this.DataSource, Model.AtCurrencyCategory.PRO_Id);
            this.xrTableCellName.DataBindings.Add("Text", this.DataSource, Model.AtCurrencyCategory.PRO_AtCurrencyName);
            this.xrTableCellEnglishName.DataBindings.Add("Text", this.DataSource, Model.AtCurrencyCategory.PRO_EnglishName);
		}

	}
}
