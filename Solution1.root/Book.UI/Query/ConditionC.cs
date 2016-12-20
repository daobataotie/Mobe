using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���              ���ʱ��:2009-4-11
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public class ConditionC : Condition
    {
        private DateTime? expiringDate;
        //private Model.Company company;
    
        public DateTime? ExpiringDate
        {
            get
            {
                return this.expiringDate.Value.Date.AddDays(1).AddSeconds(-1);
            }
            set
            {
                this.expiringDate = value;
            }
        }

        //public Model.Company Company
        //{
        //    get
        //    {
        //        return this.company;
        //    }
        //    set
        //    {
        //        this.company = value;
        //    }
        //}
    }
}
