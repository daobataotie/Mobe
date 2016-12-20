//------------------------------------------------------------------------------
//
// file name:DepotAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:49
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
    /// Data accessor of Depot
    /// </summary>
    public partial class DepotAccessor : EntityAccessor, IDepotAccessor
    {
        public  Model.Depot SelectByDepotPosition(string id)
        {
            return sqlmapper.QueryForObject<Model.Depot>("Depot.select_byPosition", id);
        
        }
        public Model.Depot SelectByDepot(string depotId)
        {
            return sqlmapper.QueryForObject<Model.Depot>("Depot.select_by_primary_key", depotId);
        }
    }
   
}
