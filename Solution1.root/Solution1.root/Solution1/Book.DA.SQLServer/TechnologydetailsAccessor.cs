//------------------------------------------------------------------------------
//
// file name：TechnologydetailsAccessor.cs
// author: peidun
// create date：2009-12-8 16:11:35
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
    /// Data accessor of Technologydetails
    /// </summary>
    public partial class TechnologydetailsAccessor : EntityAccessor, ITechnologydetailsAccessor
    {
        public Book.Model.Technologydetails Select(string proceduresId)
        {
            return sqlmapper.QueryForObject<Model.Technologydetails>("Technologydetails.select_byTechnologydetails", proceduresId);
        }
        public IList<Book.Model.Technologydetails> Select(Model.TechonlogyHeader TechonlogyHeader)
        {
            return sqlmapper.QueryForList<Model.Technologydetails>("Technologydetails.select_byTechnologyId", TechonlogyHeader.TechonlogyHeaderId);
        }
        public bool IsExists_TechnologydetailsNo(Model.Technologydetails tec)
        {
            return sqlmapper.QueryForObject<bool>("Technologydetails.IsExists_TechnologydetailsNo", tec.TechnologydetailsNo);
        }

        public IList<Model.Technologydetails> SelectByProceduresId(string ProceduresId, string TechnologydetailsNo)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", ProceduresId);
            ht.Add("no", TechnologydetailsNo);
            return sqlmapper.QueryForList<Model.Technologydetails>("Technologydetails.SelectByProceduresId", ht);
        }
        public  void Delete(Model.TechonlogyHeader techonlogyHeader)
        {
            sqlmapper.Delete("Technologydetails.deleteByTechnology", techonlogyHeader.TechonlogyHeaderId);               
        
        }
    }
}
