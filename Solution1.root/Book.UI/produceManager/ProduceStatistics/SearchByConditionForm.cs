using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;

namespace Book.UI.produceManager.ProduceStatistics
{
    public partial class SearchByConditionForm : ConditionAChooseForm
    {
        public SearchByConditionForm()
        {
            InitializeComponent();
        }

        SearchByCondition condition;

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new SearchByCondition();
            condition.StartDate = this.dateEditStartDate.Text == "" ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime;
            condition.EndDate = this.dateEditEndDate.Text == "" ? global::Helper.DateTimeParse.EndDate : this.dateEditEndDate.DateTime;
            condition.PronoteHeaderId = this.textEditPronoteHeaderID.Text == "" ? null : this.textEditPronoteHeaderID.Text;
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as SearchByCondition;
            }
        }
    }
}