//------------------------------------------------------------------------------
//
// file name：ProduceOtherReturnMaterialAccessor.cs
// author: mayanjun
// create date：2011-08-31 15:05:11
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
    /// Data accessor of ProduceOtherReturnMaterial
    /// </summary>
    public partial class ProduceOtherReturnMaterialAccessor : EntityAccessor, IProduceOtherReturnMaterialAccessor
    {
        public IList<Model.ProduceOtherReturnMaterial> Select(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.ProduceOtherReturnMaterial>("ProduceOtherReturnMaterial.selectByDateRange", ht);
        }
    }
}
