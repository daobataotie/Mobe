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
    public partial class ConditionAdiaryChooseForm : ConditionAChooseForm
    {
        private ConditionAdiary condition;

        public ConditionAdiaryChooseForm()
        {
            InitializeComponent();
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionAdiary();

            this.condition.SummonCatetory = this.radioGroup1.SelectedIndex;
            this.condition.EndDate = this.dateEditEndDate.DateTime;
            this.condition.StartDate = this.dateEditStartDate.DateTime;
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionAdiary;
            }
        }
    }
}