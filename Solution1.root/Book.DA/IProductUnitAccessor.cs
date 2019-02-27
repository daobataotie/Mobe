//------------------------------------------------------------------------------
//
// file name：IProductUnitAccessor.cs
// author: peidun
// create date：2009-4-25 13:55:05
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductUnit
    /// </summary>
    public partial interface IProductUnitAccessor : IAccessor
    {
        Book.Model.ProductUnit GetByTw(string unit);

        /// <summary>
        /// 根据所属计量单位组查找计量单位
        /// </summary>
        IList<Model.ProductUnit> SelectByGroup(string groupId);

        void updates(Model.UnitGroup group);
        bool existsInsertName(string name, Model.UnitGroup group);
        bool existsUpdateName(string name, string id, Model.UnitGroup group);
        double? ConvertUnit(string groupunitId, string Fromunitid, string Tounitid, double FromQuantity);
        IList<string> SelectProductUnitGroup();
    }
}

