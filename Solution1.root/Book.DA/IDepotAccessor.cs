//------------------------------------------------------------------------------
//
// file name：IDepotAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Depot
    /// </summary>
    public partial interface IDepotAccessor : IEntityAccessor
    {
        Model.Depot SelectByDepotPosition(string id);
        Model.Depot SelectByDepot(string depotid);
    }
}

