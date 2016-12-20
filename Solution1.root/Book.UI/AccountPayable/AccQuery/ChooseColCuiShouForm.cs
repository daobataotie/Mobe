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
    public partial class ChooseColCuiShouForm :Query.ConditionChooseForm
    {
        private ChooseColCuiShou ConditionCuiShou;
        public ChooseColCuiShouForm()
        {
            InitializeComponent();
            this.dateEditJZ.DateTime = DateTime.Now.Date;
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
            if (this.ConditionCuiShou == null)
                this.ConditionCuiShou = new ChooseColCuiShou();
            ConditionCuiShou.Employee1 = this.newChooseEmp1.EditValue as Model.Employee;
            ConditionCuiShou.Employee2 = this.newChooseEmp2.EditValue as Model.Employee;
            ConditionCuiShou.Customer1 = this.newChooseCustomer1.EditValue as Model.Customer;
            ConditionCuiShou.Customer2 = this.newChooseCustomer2.EditValue as Model.Customer;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditJZ.DateTime, new DateTime()))
            {
                this.ConditionCuiShou.YJDate = DateTime.Now;
            }

            else
            {
                this.ConditionCuiShou.YJDate = this.dateEditJZ.DateTime;
            }

          //  ConditionCuiShou.Hasother = this.checkEdit1.Checked;
         

        }

  

        public override Query.Condition Condition
        {
            get
            {
                return ConditionCuiShou;
            }
            set
            {
                this.ConditionCuiShou = value as ChooseColCuiShou;
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