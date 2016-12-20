using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace Book.DA
{
    public interface IEntityAccessor : IAccessor
    {
        bool HasRows<T>();
        bool HasRows<T>(string id);

        /// <summary>
        /// Get InvoiceBS by primary key.
        /// </summary>
        T Get<T>(string id);

        /// <summary>
        /// Delete InvoiceBS by primary key.
        /// </summary>
        void Delete<T>(string invoiceId);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        int Count<T>();

        /// <summary>
        /// Insert.
        /// </summary>
        void Insert<T>(T entity);

        /// <summary>
        /// Update.
        /// </summary>
        void Update<T>(T entity);

        /// <summary>
        /// Select all.
        /// </summary>
        IList<T> Select<T>();

        /// <summary>
        /// 获取指定状态、指定分页，并按指定要求排序的记录
        /// </summary>
        IList<T> Select<T>(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
        IList<T> DataReaderBind<T>(string sql, SqlParameter[] parems, CommandType type);

        /// <summary>
        /// 判断该字段是否有重复值
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        bool JudgeValueExists(string strsql);

        int UpdateSql(string sql);

        DateTime? JudgeHasNewVersion<T>(T entity,string PrimaryKeyId);
        DataSet Query(string SQLString, int Times, string tabelName);
        SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms);
        DataSet QueryProc(string procName, SqlParameter[] pars, string tabelName);
    }
}
