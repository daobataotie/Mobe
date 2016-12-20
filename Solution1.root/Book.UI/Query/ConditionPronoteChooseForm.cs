using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  够波涛             完成时间:2009-5-13
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionPronoteChooseForm : ConditionChooseForm
    {
        //Q49  生產通知單
        private ConditionPronote condition;

        //无参构造，初始化
        public ConditionPronoteChooseForm()
        {
            InitializeComponent();
            this.newChooseEndCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseStartCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }

        #region 重写父类方法
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionPronote;
            }
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionPronote();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit1.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEdit1.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit2.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEdit2.DateTime;
            }

            //this.condition.Company = this.buttonEditCompany.EditValue as Model.Company;
            this.condition.StartCustomer = this.newChooseStartCustomer.EditValue as Model.Customer;
            this.condition.EndCustomer = this.newChooseEndCustomer.EditValue as Model.Customer;
        }
        #endregion

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void newChooseStartCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseEndCustomer.EditValue != null)
                this.newChooseEndCustomer.EditValue = this.newChooseStartCustomer.EditValue;
        }
    }
}