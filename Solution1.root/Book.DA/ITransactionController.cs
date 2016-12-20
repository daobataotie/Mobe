using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA
{
    public interface ITransactionController
    {
        /// <summary>
        /// ��������
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// �ύ����
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// �������
        /// </summary>
        void RollbackTransaction();

    }
}
