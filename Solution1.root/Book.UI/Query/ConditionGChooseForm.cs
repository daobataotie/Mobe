using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���              ���ʱ��:2009-4-18
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ConditionGChooseForm : ConditionAChooseForm
    {
        //Ա������
        private BL.EmployeeManager emplpoyeeManager = new Book.BL.EmployeeManager();

        private ConditionG condition;

        public ConditionGChooseForm()
        {
            InitializeComponent();
        }

        #region ��д���෽��
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionG;
            }
        }
        protected override void OnOK()
        {
            if (condition == null)
                condition = new ConditionG();
            condition.StartDate = this.dateEditStartDate.DateTime;
            condition.EndDate = this.dateEditEndDate.DateTime;
            condition.EmployeeStartId = this.comboBoxEditEmployeeStartId.Text.Split(new char[] { ' ' })[0];
            condition.EmployeeEndId = this.comboBoxEditEmployeeEndId.Text.Split(new char[] { ' ' })[0];

        }
        #endregion


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionGChooseForm_Load(object sender, EventArgs e)
        {
            System.Collections.Generic.IList<Model.Employee> list = this.emplpoyeeManager.Select();
            foreach (Model.Employee emp in list)    
            {
                this.comboBoxEditEmployeeEndId.Properties.Items.Add(string.Format("{0} {1}", emp.EmployeeId, emp.EmployeeName));
                this.comboBoxEditEmployeeStartId.Properties.Items.Add(string.Format("{0} {1}", emp.EmployeeId, emp.EmployeeName));
            }
        }
 
    }
}