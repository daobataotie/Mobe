using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AccQuery
{
    public partial class CustomerMayDetail1 : DevExpress.XtraReports.UI.XtraReport
    {
        public CustomerMayDetail1()
        {
            InitializeComponent();
            //报表标题
            this.xrLabelCustomerNameTxt.Text = "";
            this.xrLabelLLPeopleNameTxt.Text = "";
        }

    }
}
