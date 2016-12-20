//------------------------------------------------------------------------------
//
// file name：PersonworkAccessor.cs
// author: peidun
// create date：2009-11-26 15:16:40
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
    /// Data accessor of Personwork
    /// </summary>
    public partial class PersonworkAccessor : EntityAccessor, IPersonworkAccessor
    {
        public Model.Personwork GetPersonwork(Model.wfrecord wfr)
        {
            return sqlmapper.QueryForObject<Model.Personwork>("Personwork.select_by_wfrid", wfr.wfrecordId);

        }
    }
}
