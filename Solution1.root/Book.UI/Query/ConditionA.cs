using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-4-5
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public class ConditionA : Condition
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime startDate;

        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime endDate;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
           
            get
            {
                return this.startDate.Date;
            }
            set
            {
                this.startDate = value;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this.endDate.Date.AddDays(1).AddSeconds(-1); 
            }
            set
            {
                this.endDate = value;
            }
        }
    }
}
