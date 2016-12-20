using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���              ���ʱ��:2009-4-19
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ConditionHChooseForm : ConditionAChooseForm
    {
        private ConditionH condition;

        public ConditionHChooseForm()
        {
            InitializeComponent();
        }

        #region ��д���෽��
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionH();
            this.condition.Employee = this.buttonEditEmployee.EditValue as Model.Employee;
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
                this.condition = value as ConditionH;
            }
        }
        #endregion


        /// <summary>
        /// ѡ��Ա��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK) 
            {
                (sender as DevExpress.XtraEditors.ButtonEdit).EditValue = f.SelectedItem;
            }
        }
    }
}