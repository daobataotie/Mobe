using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
namespace Book.UI.Query
{
    public partial class BaseReport2 : DevExpress.XtraReports.UI.XtraReport
    {
        public BaseReport2()
        {
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelPrintDate.Text = string.Format(Properties.Resources.Book_UI_Query_BaseReport_InvoiceDate, DateTime.Now.ToString("yyyy-MM-dd"));
            InitializeComponent();
        }

    }
}
