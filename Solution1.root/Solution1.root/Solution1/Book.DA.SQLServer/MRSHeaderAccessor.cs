//------------------------------------------------------------------------------
//
// file name：MRSHeaderAccessor.cs
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
    /// Data accessor of MRSHeader
    /// </summary>
    public partial class MRSHeaderAccessor : EntityAccessor, IMRSHeaderAccessor
    {
        public IList<Model.MRSHeader> SelectbySourceType(string type)
        {
            return sqlmapper.QueryForList<Model.MRSHeader>("MRSHeader.selectbySourceType", type);
        }
    }
}
