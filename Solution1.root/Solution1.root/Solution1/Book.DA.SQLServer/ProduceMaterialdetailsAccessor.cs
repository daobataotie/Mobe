//------------------------------------------------------------------------------
//
// file name：ProduceMaterialdetailsAccessor.cs
// author: peidun
// create date：2009-12-30 16:33:31
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
    /// Data accessor of ProduceMaterialdetails
    /// </summary>
    public partial class ProduceMaterialdetailsAccessor : EntityAccessor, IProduceMaterialdetailsAccessor
    {
        public IList<Book.Model.ProduceMaterialdetails> Select(Model.ProduceMaterial produceMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.select_byProduceMaterialID", produceMaterial.ProduceMaterialID);
        }
        public IList<Book.Model.ProduceMaterialdetails> SelectByState(Model.ProduceMaterial produceMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.select_byState", produceMaterial.ProduceMaterialID);
        }
        public IList<Book.Model.ProduceMaterialdetails> Select(string houseid ,DateTime startDate,DateTime endDate )
        {
            Hashtable ht = new Hashtable();
            ht.Add("houseid",houseid);
            ht.Add("startDate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.SelectByHouseDates", ht);
        }

        public IList<Model.ProduceMaterialdetails> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, string pId0, string pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            Hashtable ht = new Hashtable();
            ht.Add("starDate", starDate);
            ht.Add("endDate", endDate);
            ht.Add("produceMaterialId0", produceMaterialId0);
            ht.Add("produceMaterialId1", produceMaterialId1);
            ht.Add("pId0", pId0);
            ht.Add("pId1", pId1);
            ht.Add("dId0", departmentId0);
            ht.Add("dId1", departmentId1);
            ht.Add("pronoteId0", PronoteHeaderId0);
            ht.Add("pronoteId1", PronoteHeaderId1);
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.selectBycondition", ht);
        }

        public Model.ProduceMaterialdetails SelectByProductIdAndHeadId(string productId, string produceMaterialId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("produceMaterialId", produceMaterialId);
            ht.Add("productId", productId);
            return sqlmapper.QueryForObject<Model.ProduceMaterialdetails>("ProduceMaterialdetails.selectByproductIdAndHeadId", ht);
        }
        
    }
}
