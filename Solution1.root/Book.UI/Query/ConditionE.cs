using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���              ���ʱ��:2009-4-14
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public class ConditionE : ConditionA
    {
        private string startIdProduct;

        public string StartIdProduct
        {
            get { return startIdProduct; }
            set { startIdProduct = value; }
        }

        private string endIdProduct;

        public string EndIdProduct
        {
            get { return endIdProduct; }
            set { endIdProduct = value; }
        }

        private string startIdCompany;

        public string StartIdCompany
        {
            get { return startIdCompany; }
            set { startIdCompany = value; }
        }

        private string endIdCompany;

        public string EndIdCompany
        {
            get { return endIdCompany; }
            set { endIdCompany = value; }
        }

    }
}
