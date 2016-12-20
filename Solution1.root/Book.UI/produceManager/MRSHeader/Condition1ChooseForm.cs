using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;
namespace Book.UI.produceManager.MRSHeader
{
    public partial class Condition1ChooseForm : ConditionAChooseForm
    {
        string pronoteHeaderId = string.Empty;
        private Condition1 condition;
        public Condition1ChooseForm()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEditEndDate.DateTime = DateTime.Now.Date;
        }
        protected override void OnOK()
        {
            if (condition == null)
                condition = new Condition1();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEditStartDate.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEditEndDate.DateTime;
            }
            if (this.coBoxSourceType.SelectedIndex == 0 || this.coBoxSourceType.SelectedIndex == -1)
            this.condition.SourceType=-1;
            else if (this.coBoxSourceType.SelectedIndex <=2)
                this.condition.SourceType = this.coBoxSourceType.SelectedIndex-1;
            else
                this.condition.SourceType = this.coBoxSourceType.SelectedIndex;

            this.condition.CusXOId = this.textEditCusXOId.EditValue==null?null:this.textEditCusXOId.Text;
        }
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as Condition1;
            }
        }

    }
}