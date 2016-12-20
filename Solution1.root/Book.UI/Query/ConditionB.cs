using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���            ���ʱ��:2009-4-9
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public class ConditionB : Condition
    {
        //����
        private DateTime date1;
        private DateTime date2;
        private Model.Customer customer;
        private Model.Depot depot;
        //Ա��model
        private Model.Employee employee;


        //����������
        public DateTime Date1
        {
            get
            {
                return this.date1.Date;
            }
            set
            {
                this.date1 = value;
            }
        }

        public DateTime Date2
        {
            get
            {
                return this.date2.Date.AddDays(1).AddSeconds(-1);
            }
            set
            {
                this.date2 = value;
            }
        }

        public Model.Customer Customer
        {
            get
            {
                return this.customer;
            }
            set
            {
                this.customer = value;
            }
        }

        public Model.Depot Depot
        {
            get
            {
                return this.depot;
            }
            set
            {
                this.depot = value;
            }
        }


        //Ա��������
        public Model.Employee Employee
        {
            get
            {
                return this.employee;
            }
            set
            {
                this.employee = value;
            }
        }
    }
}
