using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-4-18
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
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
