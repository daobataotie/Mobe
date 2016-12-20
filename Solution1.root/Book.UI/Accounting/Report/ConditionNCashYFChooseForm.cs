using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;

namespace Book.UI.Accounting.Report
{
    public partial class ConditionNCashYFChooseForm : ConditionAChooseForm
    {
        private ConditionNCashYF condition;
        public ConditionNCashYFChooseForm()
        {
            InitializeComponent();
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionNCashYF();
            this.condition.StartDate = this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.DateTime;
        }
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionNCashYF;
            }
        }
    }
}