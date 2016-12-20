//------------------------------------------------------------------------------
//
// file name：DepotInDetailAccessor.cs
// author: mayanjun
// create date：2010-10-25 16:14:48
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
    /// Data accessor of DepotInDetail
    /// </summary>
    public partial class DepotInDetailAccessor : EntityAccessor, IDepotInDetailAccessor
    {
        public IList<Model.DepotInDetail> GetDetailByDepotInId(string depotInId)
        {
            return sqlmapper.QueryForList<Model.DepotInDetail>("DepotInDetail.GetDetailByDepotInId", depotInId);
        }        
         public void Delete(Model.DepotIn depotIn)
        {
             sqlmapper.Delete("DepotInDetail.delete_byheader",depotIn.DepotInId);
        }
    }
}
