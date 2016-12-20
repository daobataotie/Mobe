using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AccQuery
{
    public partial class CustomerMayDetailB : Query.BaseReport
    {

        public CustomerMayDetailB()
        {
            InitializeComponent();
        }

        public CustomerMayDetailB(CustomerMayChoose conn)
            : this()
        {
            this.xrLabelReportName.Text = "應收賬款明細表";
            this.xrLabelDateRange.Text = conn.StartDate.ToString("yyyy-MM-dd") + " ~ " + conn.EndDate.ToString("yyyy-MM-dd");


        }
    }
}
