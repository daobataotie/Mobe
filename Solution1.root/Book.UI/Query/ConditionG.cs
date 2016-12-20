using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-4-16
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
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
