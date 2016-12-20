using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA
{
    public interface IDataBaseUpdate
    {
        /// <summary>
        /// 修改表结构
        /// </summary>
        /// <param name="fileContent"></param>
        void Update(String fileContent);
        /// <summary>
        /// 获取当前数据库版本
        /// </summary>
        /// <returns></returns>
        int GetCurrentDataBaseVersion();
    }
}
