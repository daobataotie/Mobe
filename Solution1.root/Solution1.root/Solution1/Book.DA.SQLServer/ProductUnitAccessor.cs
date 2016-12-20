//------------------------------------------------------------------------------
//
// file name：ProductUnitAccessor.cs
// author: peidun
// create date：2009-4-25 13:55:05
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
    /// Data accessor of ProductUnit
    /// </summary>
    public partial class ProductUnitAccessor : EntityAccessor, IProductUnitAccessor
    {
        #region IProductUnitAccessor 成员

        public Book.Model.ProductUnit GetByTw(string unit)
        {
            return sqlmapper.QueryForObject<Model.ProductUnit>("ProductUnit.getUnitbyName", unit);
        }

        public IList<Book.Model.ProductUnit> SelectByGroup(Book.Model.UnitGroup group)
        {
            return sqlmapper.QueryForList<Model.ProductUnit>("ProductUnit.selectbygroup", group == null ? "" : group.UnitGroupId);
        }

        public void updates(Model.UnitGroup group)
        {
            sqlmapper.QueryForList<Model.ProductUnit>("ProductUnit.updatebygroup", group.UnitGroupId);
         
        }
        public bool existsInsertName(string name, Model.UnitGroup group)
        {
            Hashtable ht = new Hashtable();
            ht.Add("name",name);
            ht.Add("groupunitd",group==null?null:group.UnitGroupId);
            return sqlmapper.QueryForObject<bool>("ProductUnit.existsInsertName", ht);
        }
        public bool existsUpdateName(string name, string id, Model.UnitGroup group)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CnName", name);
            ht.Add("ProductUnitId", id);
            ht.Add("groupunitid", group == null ? null : group.UnitGroupId);
            return sqlmapper.QueryForObject<bool>("ProductUnit.existsUpdateName", ht);
        }
        //public double productUnit(string unit1, string unit2, double unit1Quantity)
        //{
        //    Hashtable ht = new Hashtable();
        //    ht.Add("CnName", name);
        //    ht.Add("ProductUnitId", id);
        //    ht.Add("groupunitid", group == null ? null : group.UnitGroupId);
        //    return sqlmapper.QueryForObject<bool>("ProductUnit.existsUpdateName", ht);
        //}


            
        #endregion
    }
}
