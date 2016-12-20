using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-4-14
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
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
