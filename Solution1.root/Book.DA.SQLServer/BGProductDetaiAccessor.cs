//------------------------------------------------------------------------------
//
// file name：BGProductDetaiAccessor.cs
// author: mayanjun
// create date：2013-4-1 11:58:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of BGProductDetai
    /// </summary>
    public partial class BGProductDetaiAccessor : EntityAccessor, IBGProductDetaiAccessor
    {
        public void DeleteByBGProductId(string BGProductId)
        {
            sqlmapper.Delete("BGProductDetai.DeleteByBGProductId", BGProductId);
        }

        /// <summary>
        /// 查询料件
        /// </summary>
        /// <param name="BGProductId"></param>
        /// <returns></returns>
        public IList<Model.BGProductDetai> SelectProductByBGProductId(string BGProductId)
        {
            return sqlmapper.QueryForList<Model.BGProductDetai>("BGProductDetai.SelectProductByBGProductId", BGProductId);
        }

        /// <summary>
        /// 查询成品
        /// </summary>
        /// <param name="BGProductId"></param>
        /// <returns></returns>
        public IList<Model.BGProductDetai> SelectMaterialByBGProductId(string BGProductId)
        {
            return sqlmapper.QueryForList<Model.BGProductDetai>("BGProductDetai.SelectMaterialByBGProductId", BGProductId);
        }
    }
}
