using System;
using System.Collections.Generic;
using System.Text;

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
    public class ConditionI:Condition
    {
        private string startCompanyId;

        public string StartCompanyId
        {
            get { return startCompanyId; }
            set { startCompanyId = value; }
        }
        private string endCompanyId;

        public string EndCompanyId
        {
            get { return endCompanyId; }
            set { endCompanyId = value; }
        }
    }
}
