using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    /// <summary>
    /// ����˵��
    /// </summary>
    public class OrderDescription
    {
        public OrderDescription(string statement)
        {
            this.statement = statement;
        }


        private string statement;

        /// <summary>
        /// ������䣬sql���order�ؼ��ֺ���Ĳ���
        /// </summary>
        public string Statement
        {
            get { return statement; }
            set { statement = value; }
        }
    }
}
