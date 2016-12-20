//------------------------------------------------------------------------------
//
// 说明: 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name:Accessor.cs
// author: peidun
// create date:2008/6/16 10:27:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Commands;
using IBatisNet.DataMapper;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// General Data Accessor.
    /// </summary>
    public class Accessor : IAccessor
    {
 
        #region IBATIS sql mapper

        public static volatile IBatisNet.DataMapper.ISqlMapper _sqlmapper = null;

        public static IBatisNet.DataMapper.ISqlMapper sqlmapper
        {
            get
            {
                InitializeSqlMapper();
                return _sqlmapper;
            }

        }

        public static void Configure(object obj)
        {
            
            _sqlmapper = (IBatisNet.DataMapper.SqlMapper)obj;
        }

        public static void InitializeSqlMapper()
        {
            if (_sqlmapper == null)
            {
                lock (typeof(IBatisNet.DataMapper.SqlMapper))
                {
                    //if (_sqlmapper == null) // double-check
                    //{
                    IBatisNet.Common.Utilities.ConfigureHandler handler = new IBatisNet.Common.Utilities.ConfigureHandler(Configure);
                    IBatisNet.DataMapper.Configuration.DomSqlMapBuilder builder = new IBatisNet.DataMapper.Configuration.DomSqlMapBuilder();
#if DEBUG
                    try
                    {
                        _sqlmapper = builder.ConfigureAndWatch("Book.SQLServer.SQLMap.config", handler);
                
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

#else 
                            sqlmapper = builder.ConfigureAndWatch("Book.SQLServer.SQLMap.config", handler);
#endif

                    //   }
                }
            }

        }
        #endregion

        #region Static Constructors
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Accessor()
        {
            // initialize IBATIS sql mapper.
            InitializeSqlMapper();
        }
        #endregion

        #region IAccessor<T> 成员

        #endregion



    }
}
