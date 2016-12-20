using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-4-11
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
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
