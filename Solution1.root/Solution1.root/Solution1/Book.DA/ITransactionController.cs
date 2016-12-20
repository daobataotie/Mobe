using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA
{
    public interface ITransactionController
    {
        /// <summary>
        /// 启动事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 会滚事务
        /// </summary>
        void RollbackTransaction();

    }
}
