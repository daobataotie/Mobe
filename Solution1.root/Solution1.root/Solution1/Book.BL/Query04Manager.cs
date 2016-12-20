using System;
using System.Collections.Generic;
using System.Text;

namespace Book.BL
{
    public class Query04Manager
    {
        private static readonly DA.IQuery04Accessor accessor = (DA.IQuery04Accessor)Accessors.Get("Query04Accessor");

        //public System.Data.DataTable SelectDataTable(Model.Company company, DateTime? dt) 
        //{
        //    return accessor.SelectDataTable(company, dt);
        //}
    }
}
