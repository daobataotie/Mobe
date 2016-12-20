//------------------------------------------------------------------------------
//
// file name：MPSheaderAccessor.cs
// author: peidun
// create date：2009-12-18 11:12:41
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
    /// Data accessor of MPSheader
    /// </summary>
    public partial class MPSheaderAccessor : EntityAccessor, IMPSheaderAccessor
    {
        public IList<Book.Model.MPSheader> SelectById(string mPSheaderId)
        {
            return sqlmapper.QueryForList<Model.MPSheader>("MPSheader.select_byMRSheaderId",mPSheaderId);
        }
        public IList<Book.Model.MPSheader> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.MPSheader>("MPSheader.select_byTime", pars);
        }
    }
}
