using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-4-9
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public class ConditionB : Condition
    {
        //变量
        private DateTime date1;
        private DateTime date2;
        private Model.Customer customer;
        private Model.Depot depot;
        //员工model
        private Model.Employee employee;


        //日期索引器
        public DateTime Date1
        {
            get
            {
                return this.date1.Date;
            }
            set
            {
                this.date1 = value;
            }
        }

        public DateTime Date2
        {
            get
            {
                return this.date2.Date.AddDays(1).AddSeconds(-1);
            }
            set
            {
                this.date2 = value;
            }
        }

        public Model.Customer Customer
        {
            get
            {
                return this.customer;
            }
            set
            {
                this.customer = value;
            }
        }

        public Model.Depot Depot
        {
            get
            {
                return this.depot;
            }
            set
            {
                this.depot = value;
            }
        }


        //员工索引器
        public Model.Employee Employee
        {
            get
            {
                return this.employee;
            }
            set
            {
                this.employee = value;
            }
        }
    }
}
