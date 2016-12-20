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

// �� �� ��:  ������             ���ʱ��:2009-4-22
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ConditionKChooseForm : ConditionChooseForm
    {
        private ConditionK condition;


        //�޲ι���
        public ConditionKChooseForm()
        {
            InitializeComponent();
            this.dateEditEndDate.DateTime = DateTime.Now;
        }


        #region ��д���෽��
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionK();
            condition.EndDate = this.dateEditEndDate.DateTime;
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionK;
            }
        }
        #endregion
    }
}