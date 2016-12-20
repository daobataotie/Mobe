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
    public partial class ConditionYS2ChooseForm :ConditionAChooseForm
    {
        private ConditionYS2 condition;
        public ConditionYS2ChooseForm()
        {
            InitializeComponent();
            this.newChooseContorl1.Choose = new AtBankAccount.ChooseAtBankAccount();
            this.newChooseContorl2.Choose = new AtBankAccount.ChooseAtBankAccount();
        }

       
        #region 重写父类方法
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionYS2();
            this.condition.StartDate = this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.DateTime;
            if (this.newChooseContorl1.EditValue != null)
            {
                this.condition.StartBankAccountId = (this.newChooseContorl1.EditValue as Model.AtBankAccount).BankAccountId;
            }
            if (this.newChooseContorl2.EditValue != null)
            {
                this.condition.EndBankAccountId = (this.newChooseContorl2.EditValue as Model.AtBankAccount).BankAccountId;
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
                this.condition = value as ConditionYS2;
            }
        }
        #endregion

    }
}