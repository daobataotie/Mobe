using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���              ���ʱ��:2009-4-20
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public class ConditionJ : ConditionA
    {
        private string startId;

        public string StartId
        {
            get { return this.startId; }
            set { this.startId = value; }
        }

        private string endId;

        public string EndId 
        {
            get { return this.endId; }
            set { this.endId = value; }
        }

    }
}
