//------------------------------------------------------------------------------
//
// file name：ISalesFordetailsAccessor.cs
// author: peidun
// create date：2009-12-17 15:29:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.SalesFordetails
    /// </summary>
    public partial interface ISalesFordetailsAccessor : IAccessor
    {
         IList<Model.SalesFordetails> Getdetails(Model.SalesForHeader salesForHeader);
         IList<Model.SalesFordetails> GetdetailsByProductId(string productId);
    }
}

