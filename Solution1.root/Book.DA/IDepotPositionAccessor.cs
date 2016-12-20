//------------------------------------------------------------------------------
//
// file name：IDepotPositionAccessor.cs
// author: peidun
// create date：2009-07-24 11:18:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.DepotPosition
    /// </summary>
    public partial interface IDepotPositionAccessor : IAccessor
    {
        IList<Book.Model.DepotPosition> Select(Book.Model.Depot depot);
        IList<Book.Model.DepotPosition> Select(string depotId);
        IList<Book.Model.DepotPosition> SelectByDepot(Book.Model.Depot depot);
        bool existsInsertName(string id, Model.Depot depot);
        IList<Model.DepotPosition> GetDepotPositionsByDepotAndProduct(string ProductId, string DepotId);
        IList<Model.DepotPosition> GetStockByDepotAndProduct(string ProductId, string DepotId);
    }
}

