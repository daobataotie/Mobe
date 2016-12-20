using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.AccountPayable.AccQuery
{
    public partial class CustomerMayChooseForm :Query.ConditionAChooseForm
    {
        private CustomerMayChoose condition;
        public CustomerMayChooseForm()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEditEndDate.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.newChooseEmp1.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseEmp2.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseCustomer1.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseCustomer2.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }
        /// <summary>
        /// 重写父类方法
        /// </summary>
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new CustomerMayChoose();
            condition.Employee1 = this.newChooseEmp1.EditValue as Model.Employee;
            condition.Employee2 = this.newChooseEmp2.EditValue as Model.Employee;
            condition.Customer1 = this.newChooseCustomer1.EditValue as Model.Customer;
            condition.Customer2 = this.newChooseCustomer2.EditValue as Model.Customer;
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

       
            //condition.YJDate = this.dateEditJZ.DateTime;
            //condition.Hasother = this.checkEdit1.Checked;


        }



        public override Query.Condition Condition
        {
            get
            {
                return condition;
            }
            set
            {
                this.condition = value as CustomerMayChoose;
            }
        }

        private void newChooseCustomer1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseCustomer1.EditValue != null)
                this.newChooseCustomer2.EditValue = this.newChooseCustomer1.EditValue;
        }

        private void newChooseEmp1_EditValueChanged(object sender, EventArgs e)
        {

            if (this.newChooseEmp1.EditValue != null)
                this.newChooseEmp2.EditValue = this.newChooseEmp1.EditValue;
        }
    }
}