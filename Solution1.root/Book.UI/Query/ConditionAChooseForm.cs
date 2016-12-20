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

// �� �� ��:  ������             ���ʱ��:2009-4-6
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ConditionAChooseForm : ConditionChooseForm
    {
        #region Data
        private ConditionA conditionA;

        #endregion

        public ConditionAChooseForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ConditionAChooseForm_Load);
        }

        #region  ��ʼ��ʱ���鷽��
        protected virtual void Init()
        {
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEditEndDate.DateTime = DateTime.Now.Date;
        }
        private void ConditionAChooseForm_Load(object sender, EventArgs e)
        {
            Init();
        }
        #endregion

        /// <summary>
        /// ��д���෽��
        /// </summary>
        protected override void OnOK()
        {
            if (this.conditionA == null)
                this.conditionA = new ConditionA();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.conditionA.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.conditionA.StartDate = this.dateEditStartDate.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.conditionA.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.conditionA.EndDate = this.dateEditEndDate.DateTime;
            }
            OnOK1();

        }

        protected virtual void OnOK1()
        {
           
        }

        public override Condition Condition
        {
            get
            {
                return this.conditionA;
            }
            set
            {
                this.conditionA = value as ConditionA;
            }
        }

        private void dateEditEndDate_EditValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}