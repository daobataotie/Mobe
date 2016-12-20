using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���              ���ʱ��:2009-4-5
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public class ConditionA : Condition
    {
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime startDate;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endDate;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime StartDate
        {
           
            get
            {
                return this.startDate.Date;
            }
            set
            {
                this.startDate = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this.endDate.Date.AddDays(1).AddSeconds(-1); 
            }
            set
            {
                this.endDate = value;
            }
        }
    }
}
