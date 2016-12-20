using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  够波涛             完成时间:2009-5-11
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public   class ConditionPronote:Condition
    {

        private DateTime startDate;
        private DateTime endDate;
        private Model.Customer startCustomer;
        private Model.Customer endCustomer;
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        public Model.Customer StartCustomer
        {
            get { return this.startCustomer; }
            set { this.startCustomer = value; }
        }

        public Model.Customer EndCustomer
        {
            get { return this.endCustomer; }
            set { this.endCustomer = value; }
        }



    }
}
