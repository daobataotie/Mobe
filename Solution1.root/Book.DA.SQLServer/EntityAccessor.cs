using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Data;
using System.Reflection;
namespace Book.DA.SQLServer
{
    public class EntityAccessor : Accessor, IEntityAccessor
    {
        /// <summary>
        /// Get InvoiceBS by primary key.
        /// </summary>
        public T Get<T>(string id)
        {
            return sqlmapper.QueryForObject<T>(typeof(T).Name + ".select_by_primary_key", id);
        }

        /// <summary>
        /// Get InvoiceBS by primary key.
        /// </summary>
        public bool HasRows<T>(string id)
        {
            return sqlmapper.QueryForObject<bool>(typeof(T).Name + ".has_rows_of", id);
        }
        /// <summary>
        /// Get InvoiceBS by primary key.
        /// </summary>
        public bool HasRowsName<T>(string name)
        {
            return sqlmapper.QueryForObject<bool>(typeof(T).Name + ".has_rows_ofname", name);
        }

        /// <summary>
        /// Get InvoiceBS by primary key.
        /// </summary>
        public bool HasRows<T>()
        {
            return sqlmapper.QueryForObject<bool>(typeof(T).Name + ".has_rows", null);
        }

        /// <summary>
        /// Delete InvoiceBS by primary key.
        /// </summary>
        public void Delete<T>(string id)
        {
            try
            {
                sqlmapper.Delete(typeof(T).Name + ".delete", id);
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        throw new Helper.MessageValueException("當前記錄不能刪除");

                    default:
                        throw;
                }

            }
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int Count<T>()
        {
            return sqlmapper.QueryForObject<int>(typeof(T).Name + ".count_all", null);
        }

        /// <summary>
        /// Insert.
        /// </summary>
        public void Insert<T>(T entity)
        {
            sqlmapper.Insert(typeof(T).Name + ".insert", entity);

        }

        /// <summary>
        /// Update.
        /// </summary>
        public void Update<T>(T entity)
        {
            sqlmapper.Update(typeof(T).Name + ".update", entity);
        }

        /// <summary>
        /// Select.
        /// </summary>
        public IList<T> Select<T>()
        {
            return sqlmapper.QueryForList<T>(typeof(T).Name + ".select_all", null);
        }

        public IList<T> Select<T>(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Row1", pagingDescription.PageSize * (pagingDescription.PageIndex - 1) + 1);
            paras.Add("Row2", pagingDescription.PageSize * (pagingDescription.PageIndex - 1) + pagingDescription.PageSize);
            paras.Add("OrderStatement", orderDescription.Statement);
            return sqlmapper.QueryForList<T>(typeof(T).Name + ".select_all_with_paging", paras);
        }

        public IList<T> DataReaderBind<T>(string sql, SqlParameter[] parems, CommandType type)
        {
            IList<T> list = new List<T>();

            using (SqlDataReader dataReader = SQLDB.SqlHelper.ExecuteReader(SQLDB.SqlHelper.ConnectionStringLocalTransaction, type, sql, parems))
            {
                if (dataReader != null)
                {
                    T _t;
                    while (dataReader.Read())
                    {
                        //T _t = default(T);
                        //创建泛型对象的实例
                         _t = Activator.CreateInstance<T>();

                        //下标，通过下标获取数据库字段的名称

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                          
                            foreach (var item in _t.GetType().GetProperties())
                            {
                                string fieldName = item.Name;//属性名
                                //判断当前迭代出的属性名称是否和迭代出的dataReader的列名称一致
                                if (item.Name.ToLower().Equals(dataReader.GetName(i).ToLower()))
                                {
                                    //从DataReader中读取值
                                    object _value = dataReader[fieldName];
                                    //将当前dataReader的单列值赋予相匹配的属性,否则赋予一个null值.
                                    if (_value != null && _value != DBNull.Value)
                                    {
                                        if (item.PropertyType.Name == "Boolean" && ((int)_value == 1 || (int)_value == 0))
                                        {
                                            item.SetValue(_t, (int)_value == 1 ? true : false, null);

                                        }
                                        else
                                            item.SetValue(_t, _value, null);

                                    }

                                    else
                                        item.SetValue(_t, null, null);
                                }
                            }
                        }
                        list.Add(_t);
                    }
                    dataReader.Close();
                }
                else
                    list = null;
            }
            return list;
        }

        public DataSet Query(string SQLString, int Times,string tabelName)
        {
           return SQLDB.DbHelperSQL.Query(SQLString, Times, tabelName);
        }

        public object QueryObject(string SQLString)
        {
            return SQLDB.DbHelperSQL.QueryObject(SQLString);
        }

        public DataSet QueryProc(string procName, SqlParameter[] pars, string tabelName)
        {
            return SQLDB.DbHelperSQL.RunProcedure(procName, pars,tabelName);
        }

        /// <summary>
        /// 判断该字段是否有重复值
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public bool JudgeValueExists(string strsql)
        {
            return SQLDB.DbHelperSQL.Exists(strsql);
        }

        public int UpdateSql(string sql)
        {
            return SQLDB.DbHelperSQL.ExecuteSql(sql);
        }

        public DateTime? JudgeHasNewVersion<T>(T entity, string PrimaryKeyId)
        {
            return sqlmapper.QueryForObject<DateTime>(typeof(T).Name + ".JudgeHasNewVersion", PrimaryKeyId);
        }
        public SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            return SQLDB.DbHelperSQL.ExecuteReader(SQLString, cmdParms);
        }
        public IList<T> ToTList<T>(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
            Hashtable hh = GetColumnType<T>(dt.Columns);
            IList<string> colNames = GetColumnNames<T>(hh);
            List<T> list = new List<T>();
            T model = default(T);
            foreach (DataRow dr in dt.Rows)
            {
                model = Activator.CreateInstance<T>();
                int i = 0;
                foreach (PropertyInfo p in properties)
                {
                    if (p.PropertyType == typeof(string))
                    {
                        p.SetValue(model, dr[colNames[i++]].ToString(), null);
                    }
                    else if (p.PropertyType == typeof(int?))
                    {
                        if(!string.IsNullOrEmpty(dr[colNames[i++]].ToString()))
                        p.SetValue(model, int.Parse(dr[colNames[i++]].ToString()), null);
                    }
                    else if (p.PropertyType == typeof(DateTime?))
                    {
                        p.SetValue(model, DateTime.Parse(dr[colNames[i++]].ToString()), null);
                    }
                    else if (p.PropertyType == typeof(decimal?))
                    {
                        p.SetValue(model, float.Parse(dr[colNames[i++]].ToString()), null);
                    }
                    else if (p.PropertyType == typeof(double?))
                    {
                        p.SetValue(model, double.Parse(dr[colNames[i++]].ToString()), null);
                    }
                }
                list.Add(model);
            }
            return list;
        }
        private IList<string> GetColumnNames(DataColumnCollection dcc)
        {
            IList<string> list = new List<string>();
            foreach (DataColumn dc in dcc)
            {
                list.Add(dc.ColumnName);
            }
            return list;
        }
        private IList<string> GetColumnNames<T>( Hashtable hh)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            IList<string> ilist = new List<string>();
            int i = 0;
            foreach (PropertyInfo p in properties)
            {
                ilist.Add(GetKey(p.PropertyType.ToString() + i++, hh));
            }
            return ilist;
        }
        private string GetKey(string val, Hashtable hh)
        {
            foreach (DictionaryEntry de in hh)
            {
                if (de.Value.ToString() == val)
                {
                    return de.Key.ToString();
                }
            }
            return null;
        }
        private Hashtable GetColumnType<T>(DataColumnCollection dcc)
        {
            if (dcc == null || dcc.Count == 0)
            {
                return null;
            }
            IList<string> colNameList = GetColumnNames(dcc);
            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            Hashtable hashtable = new Hashtable();
            int i = 0;
            foreach (PropertyInfo p in properties)
            {
                foreach (string col in colNameList)
                {
                    if (col.ToLower().Contains(p.Name.ToLower()))
                    {
                        hashtable.Add(col, p.PropertyType.ToString() + i++);
                    }
                }
            }
            return hashtable;
        }
    }
}
