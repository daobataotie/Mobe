using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���              ���ʱ��:2009-4-16
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public class ConditionG : ConditionA
    {
        private string _employeeStartId;

        public string EmployeeStartId
        {
            get { return _employeeStartId; }
            set { _employeeStartId = value; }
        }
        private string _employeeEndId;

        public string EmployeeEndId
        {
            get { return _employeeEndId; }
            set { _employeeEndId = value; }
        }
    }
}
