//------------------------------------------------------------------------------
//
// file name：CurrencyCategoryAccessor.cs
// author: peidun
// create date：2009-09-09 下午 04:08:32
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
    /// Data accessor of CurrencyCategory
    /// </summary>
    public partial class CurrencyCategoryAccessor : EntityAccessor, ICurrencyCategoryAccessor
    {
        public IList<Model.CurrencyCategory> SelectByEffectDate()
        {
            return sqlmapper.QueryForList<Model.CurrencyCategory>("CurrencyCategory.getCurrencyCategory",null);
        }
    }
}
