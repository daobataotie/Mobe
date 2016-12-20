using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    /// <summary>
    /// 分页说明
    /// </summary>
    public class PagingDescription
    {
        public PagingDescription(int pageSize, int pageIndex)
        {
            this.pageSize = pageSize;
            this.pageIndex = pageIndex;
        }

        private int pageSize;

        /// <summary>
        /// 每页显示项数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }


        private int pageIndex;

        /// <summary>
        /// 页号，第1页页号为1
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

    }
}
