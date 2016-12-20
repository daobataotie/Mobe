using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public partial class BaseViewForm : DevExpress.XtraEditors.XtraForm
    {
        public BaseViewForm()
        {
            InitializeComponent();
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = null;
            switch ((string)e.Item.Tag)
            {
                case "print":
                    DevExpress.XtraReports.UI.XtraReport r = this.GetReport();
                    if (r != null)
                    {
                        r.ShowPreviewDialog();
                    }
                    break;
                case "pro":
                    f = this.GetProViewForm();
                    (f as BaseViewForm).ShowDialog(this);
                    break;
                case "next":
                    f = this.GetNextViewForm();
                    (f as BaseViewForm).ShowDialog(this);
                    break;
                default:
                    break;
            }
        }

        protected virtual DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
        }
        protected virtual Form GetProViewForm() 
        {
            return null;
        }
        protected virtual Form GetNextViewForm()
        {
            return null;
        }
    }
}