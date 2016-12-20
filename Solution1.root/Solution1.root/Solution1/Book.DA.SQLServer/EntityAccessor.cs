using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Linq;

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
                        throw new Helper.ViolateConstraintException("��ǰӛ䛲��܄h��", ex);

                    default:
                        throw;
                }

            }
        }


        /// <summary>
        /// ��ȡ��¼����
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


        public IList<T> DataReaderBind<T>(string sql)
        {
            IList<T> list = new List<T>();

            using (SqlDataReader dataReader = SQLDB.SqlHelper.ExecuteReader(SQLDB.SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null))
            {
                while (dataReader.Read())
                {
                    //T _t = default(T);
                    //�������Ͷ����ʵ��
                    T _t = Activator.CreateInstance<T>();

                    //�±꣬ͨ���±��ȡ���ݿ��ֶε�����

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {


                        foreach (var item in _t.GetType().GetProperties())
                        {
                            string fieldName = item.Name;//������
                            //�жϵ�ǰ�����������������Ƿ�͵�������dataReader��������һ��
                            if (item.Name.ToLower().Equals(dataReader.GetName(i).ToLower()))
                            {
                                //��DataReader�ж�ȡֵ
                                object _value = dataReader[fieldName];
                                //����ǰdataReader�ĵ���ֵ������ƥ�������,������һ��nullֵ.
                                if (_value != null && _value != DBNull.Value)
                                    item.SetValue(_t, _value, null);
                                else
                                    item.SetValue(_t, null, null);
                            }
                        }
                    }
                    list.Add(_t);
                }
            }
            return list;
        }

        public IList<T> NewDataReaderBind<T>(string sql)
        {
            IList<T> list = new List<T>();

            using (SqlDataReader dataReader = SQLDB.SqlHelper.ExecuteReader(SQLDB.SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null))
            {
                while (dataReader.Read())
                {
                    //T _t = default(T);
                    //�������Ͷ����ʵ��
                    T _t = Activator.CreateInstance<T>();
                    var dd = from it in _t.GetType().GetProperties() select new { };
                    //�±꣬ͨ���±��ȡ���ݿ��ֶε�����
                    //for (int i = 0; i < dataReader.FieldCount; i++)
                    //{
                    //    foreach (var item in _t.GetType().GetProperties())
                    //    {
                    //        string fieldName = item.Name;//������
                    //        //�жϵ�ǰ�����������������Ƿ�͵�������dataReader��������һ��
                    //        if (item.Name.ToLower().Equals(dataReader.GetName(i).ToLower()))
                    //        {
                    //            //��DataReader�ж�ȡֵ
                    //            object _value = dataReader[fieldName];
                    //            //����ǰdataReader�ĵ���ֵ������ƥ�������,������һ��nullֵ.
                    //            if (_value != null && _value != DBNull.Value)
                    //                item.SetValue(_t, _value, null);
                    //            else
                    //                item.SetValue(_t, null, null);
                    //        }
                    //    }
                    //}
                    list.Add(_t);
                }
            }
            return list;
        }

    }
}
