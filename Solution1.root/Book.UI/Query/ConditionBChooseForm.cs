using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Invoices;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾             完成时间:2009-4-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionBChooseForm : ConditionChooseForm
    {
        #region Data
        private ConditionB condition;

        #endregion

        private Helper.CompanyKind kind;

        public ConditionBChooseForm(global::Helper.CompanyKind kind)
        {

            InitializeComponent();
            this.newChooseCustomer.Choose =new  Settings.BasicData.Customs.ChooseCustoms();
            this.kind = kind;
            this.labelControl3.Text = kind == global::Helper.CompanyKind.Customer ? Properties.Resources.Customer : Properties.Resources.Supplier;
        }


        /// <summary>
        /// 重写父类方法
        /// </summary>
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionB();

            this.condition.Date1 = this.dateEdit1.DateTime==new DateTime()?new DateTime(1900,1,1):this.dateEdit1.DateTime;
            this.condition.Date2 = this.dateEdit2.DateTime == new DateTime() ? DateTime.Now.Date: this.dateEdit2.DateTime;
            //this.condition.Customer = this.newChooseCustomer.EditValue as Model.Customer;
            this.condition.Depot = this.buttonEditDepot.EditValue as Model.Depot;
            this.condition.Employee = this.buttonEditEmployee.EditValue as Model.Employee;
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionB;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditDepot_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                ChooseDepotForm f = new ChooseDepotForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    (sender as ButtonEdit).EditValue = f.SelectedItem;
                }
            }
            else
            {
                (sender as ButtonEdit).EditValue = null;
            }
        }


        /// <summary>
        /// 选择员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                ChooseEmployeeForm f = new ChooseEmployeeForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    (sender as ButtonEdit).EditValue = f.SelectedItem;
                }
            }
            else
            {
                (sender as ButtonEdit).EditValue = null;
            }
        }

        /// <summary>
        /// 选择单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                Settings.BasicData.BaseChooseForm f = null;
                switch (this.kind)
                {
                    case global::Helper.CompanyKind.Supplier:
                        f = new ChooseSupplier();
                        break;
                    case global::Helper.CompanyKind.Customer:
                        f = new ChooseCustoms();
                        break;
                    default:
                        break;
                }
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    (sender as ButtonEdit).EditValue = f.SelectedItem;
                }
            }
            else
            {
                (sender as ButtonEdit).EditValue = null;
            }
        }
    }
}