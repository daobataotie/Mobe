using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Employees
{
    public partial class EmployeeSearchForm : DevExpress.XtraEditors.XtraForm
    {
        public EmployeeSearchForm()
        {
            InitializeComponent();
        }

        private void DateLzBegin_EditValueChanged(object sender, EventArgs e)
        {
            //if (DateLzBegin.Text != "")
            //{
            //    rdoType.SelectedIndex = 1;
            //    this.rdoType.Properties.ReadOnly = true;
            //}
            //else
            //{
            //    this.rdoType.Properties.ReadOnly = false;
            //    rdoType.SelectedIndex = 0;
            //}
        }

        private void DateRzBegin_EditValueChanged(object sender, EventArgs e)
        {
            //if (DateRzBegin.Text != "" && DateLzBegin.Text != "")
            //    rdoType.SelectedIndex = 2;
            //else
            //    rdoType.SelectedIndex = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int flag = 0;
            if (DateRzBegin.Text == "" && DateLzBegin.Text == "" && DateRzTo.Text == "" && DateLzTo.Text == "")
            {
                flag = 1;
            }
            DateTime RzBegindate = string.IsNullOrEmpty(DateRzBegin.Text) ? global::Helper.DateTimeParse.NullDate : DateRzBegin.DateTime;
            DateTime RzTodate = string.IsNullOrEmpty(DateRzTo.Text) ? System.DateTime.Now.Date : DateRzTo.DateTime;
            DateTime LzBegindate = string.IsNullOrEmpty(DateLzBegin.Text) ? global::Helper.DateTimeParse.NullDate : DateLzBegin.DateTime;
            DateTime LzTodate = string.IsNullOrEmpty(DateLzTo.Text) ? System.DateTime.Now.Date : DateLzTo.DateTime;
            string type = rdoType.SelectedIndex.ToString();
            this.Visible = false;
            RO f = new RO(RzBegindate, RzTodate, LzBegindate, LzTodate, type, flag);
            f.ShowPreviewDialog();
            this.Close();
        }

        private void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoType.SelectedIndex == 0)
            {
                SetRzDate(true);
                SetLzDate(false);
            }
            if (rdoType.SelectedIndex == 1)
            {
                SetRzDate(false);
                SetLzDate(true);
            }
            if (rdoType.SelectedIndex == 2)
            {
                SetRzDate(true);
                SetLzDate(true);
            }
        }
        void SetRzDate(bool istrue)
        {
            DateRzBegin.Enabled = istrue;
            DateRzTo.Enabled = istrue;
            if (istrue == false)
            {
                DateRzBegin.Text = "";
                DateRzTo.Text = "";
            }
        }
        void SetLzDate(bool istrue)
        {
            DateLzBegin.Enabled = istrue;
            DateLzTo.Enabled = istrue;
            if (istrue == false)
            {
                DateLzBegin.Text = "";
                DateLzTo.Text = "";
            }
        }
    }
}