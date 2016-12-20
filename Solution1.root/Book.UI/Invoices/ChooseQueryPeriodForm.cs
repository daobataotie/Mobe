using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public partial class ChooseQueryPeriodForm : DevExpress.XtraEditors.XtraForm
    {
        public ChooseQueryPeriodForm()
        {
            InitializeComponent();
        }
        public ChooseQueryPeriodForm(DateTime datetime1, DateTime datetime2) 
            : this()
        {
            this.dateEdit1.DateTime = datetime1;
            this.dateEdit2.DateTime = datetime2;
        }

        public DateTime DateTime1
        {
            get
            {
                return this.dateEdit1.DateTime.Date;
            }
        }

        public DateTime DateTime2
        {
            get
            {
                return this.dateEdit2.DateTime.Date.AddDays(1).AddSeconds(-1);
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}