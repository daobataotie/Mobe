//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitAccessor.cs
// author: peidun
// create date：2010-1-6 10:20:17
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
    /// Data accessor of ProduceMaterialExit
    /// </summary>
    public partial class ProduceMaterialExitAccessor : EntityAccessor, IProduceMaterialExitAccessor
    {
        public IList<Model.ProduceMaterialExit> SelectByCondition(DateTime start, DateTime end)
        {
            Hashtable ht=new Hashtable();
            ht.Add("start", start);
            ht.Add("end", end);
            return sqlmapper.QueryForList<Model.ProduceMaterialExit>("ProduceMaterialExit.selectByDateRange", ht);
        }
    }
}
