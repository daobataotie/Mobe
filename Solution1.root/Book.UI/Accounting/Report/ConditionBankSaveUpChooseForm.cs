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
    public partial class ConditionBankSaveUpChooseForm : ConditionAChooseForm
    {
        private ConditionBankSaveUp condition;
        public ConditionBankSaveUpChooseForm()
        {
            InitializeComponent();
            this.newChooseContorl1.Choose = new AtBankAccount.ChooseAtBankAccount();
        }
        #region 重写父类方法
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionBankSaveUp();
            this.condition.StartDate = this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.DateTime;
            if (this.newChooseContorl1.EditValue != null)
            {
                this.condition.BankAccountId = (this.newChooseContorl1.EditValue as Model.AtBankAccount).BankAccountId;
            }
        }
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionBankSaveUp;
            }
        }
        #endregion
    }
}