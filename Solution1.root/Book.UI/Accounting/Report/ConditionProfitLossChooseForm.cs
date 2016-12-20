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
    public partial class ConditionProfitLossChooseForm : ConditionAChooseForm
    {
        private ConditionProfitLoss condition;
        public ConditionProfitLossChooseForm()
        {
            InitializeComponent();
        }
        #region 重写父类方法
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionProfitLoss();

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
                this.condition = value as ConditionProfitLoss;
            }
        }
        #endregion
    }
}