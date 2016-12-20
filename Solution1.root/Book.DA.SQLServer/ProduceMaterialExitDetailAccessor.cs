//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitDetailAccessor.cs
// author: peidun
// create date：2010-1-6 10:26:19
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
    /// Data accessor of ProduceMaterialExitDetail
    /// </summary>
    public partial class ProduceMaterialExitDetailAccessor : EntityAccessor, IProduceMaterialExitDetailAccessor
    {
      public     IList<Model.ProduceMaterialExitDetail> Select(Model.ProduceMaterialExit ProduceMaterialExit)
      {
          return sqlmapper.QueryForList<Model.ProduceMaterialExitDetail>("ProduceMaterialExitDetail.select_byProduceExitMaterialId", ProduceMaterialExit.ProduceMaterialExitId);        
      }
      public IList<Book.Model.ProduceMaterialExitDetail> Select(string houseid, DateTime startDate, DateTime endDate)
      {
          Hashtable ht = new Hashtable();
          ht.Add("houseid", houseid);
          ht.Add("startDate", startDate);
          ht.Add("enddate", endDate);
          return sqlmapper.QueryForList<Model.ProduceMaterialExitDetail>("ProduceMaterialExitDetail.SelectByHouseDates", ht);
      }

      public IList<Model.ProduceMaterialExitDetail> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialExitId0, string produceMaterialExitId1, Model.Product pId0, Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
      {
          Hashtable ht = new Hashtable();
          ht.Add("starDate", starDate);
          ht.Add("endDate", endDate);
          ht.Add("produceMaterialExitId0", produceMaterialExitId0);
          ht.Add("produceMaterialExitId1", produceMaterialExitId1);
          ht.Add("pId0", pId0 == null ? null : pId0.ProductName);
          ht.Add("pId1", pId1 == null ? null : pId1.ProductName);
          ht.Add("dId0", departmentId0);
          ht.Add("dId1", departmentId1);
          ht.Add("pronoteId0", PronoteHeaderId0);
          ht.Add("pronoteId1", PronoteHeaderId1);
          return sqlmapper.QueryForList<Model.ProduceMaterialExitDetail>("ProduceMaterialExitDetail.selectByCondition", ht);
      }
      public void Delete(Model.ProduceMaterialExit produceMaterialExit)
      {
          sqlmapper.Delete("ProduceMaterialExitDetail.delete_byheader", produceMaterialExit.ProduceMaterialExitId);
      }
        
    }
}
