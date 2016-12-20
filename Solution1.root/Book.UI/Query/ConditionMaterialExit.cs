using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 够波涛             完成时间:2009-4-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
   public  class ConditionMaterialExit:Condition
    {
        private DateTime startDate;
        private DateTime endDate;
       // private Model.WorkHouse startWorkHouse;
        private Model.WorkHouse endWorkHouse;
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

        //public Model.WorkHouse StartWorkHouse
        //{
        //    get { return this.startWorkHouse; }
        //    set { this.startWorkHouse = value; }
        //}

        public Model.WorkHouse WorkHouse
        {
            get { return this.endWorkHouse; }
            set { this.endWorkHouse = value; }
        }
    }
}
