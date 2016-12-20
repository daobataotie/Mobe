//------------------------------------------------------------------------------
//
// file name：IProductProcessAccessor.cs
// author: peidun
// create date：2010-1-27 10:46:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductProcess
    /// </summary>
    public partial interface IProductProcessAccessor : IAccessor
    {
        void Delete(Model.Product Product);
        IList<Model.ProductProcess> Select(string productId);
        IList<Model.ProductProcess> SelectByBomId(string bomid);
    }
}

