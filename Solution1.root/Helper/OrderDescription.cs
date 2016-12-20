using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    /// <summary>
    /// 排序说明
    /// </summary>
    public class OrderDescription
    {
        public OrderDescription(string statement)
        {
            this.statement = statement;
        }


        private string statement;

        /// <summary>
        /// 排序语句，sql语句order关键字后面的部分
        /// </summary>
        public string Statement
        {
            get { return statement; }
            set { statement = value; }
        }
    }
}
