//------------------------------------------------------------------------------
//
// file name：ProceduresAccessor.cs
// author: peidun
// create date：2009-12-8 10:55:36
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
    /// Data accessor of Procedures
    /// </summary>
    public partial class ProceduresAccessor : EntityAccessor, IProceduresAccessor
    {
        #region IProductAccessor 成员

        public IList<Book.Model.Procedures> Select(Book.Model.TechonlogyHeader technologyHeader)
        {
            if (technologyHeader != null)
            {
                return sqlmapper.QueryForList<Model.Procedures>("Procedures.select_byProcedures", technologyHeader.TechonlogyHeaderId);
            }
            else
            {
                return null;
            }
        }
        public IList<Book.Model.Procedures> Select(Book.Model.BomParentPartInfo bomPart)
        {
            return sqlmapper.QueryForList<Model.Procedures>("Procedures.select_byProceduresAndBom", bomPart.BomId);
        }

        public IList<Book.Model.Procedures> Select(string workHouseId)
        {
            return sqlmapper.QueryForList<Model.Procedures>("Procedures.select_byWorkHouseId", workHouseId);
        }
        #endregion
    }
}
