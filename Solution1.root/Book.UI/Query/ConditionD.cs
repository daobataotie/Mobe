using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��:  ������             ���ʱ��:2009-4-13
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public class ConditionD : Condition
    {
        private string startId;

        public string StartId
        {
            get { return startId; }
            set { startId = value; }
        }

        private string endId;

        public string EndId
        {
            get { return endId; }
            set { endId = value; }
        }


    }
}
