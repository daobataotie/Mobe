using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    /// <summary>
    /// ��ҳ˵��
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
        /// ÿҳ��ʾ����
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }


        private int pageIndex;

        /// <summary>
        /// ҳ�ţ���1ҳҳ��Ϊ1
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

    }
}
