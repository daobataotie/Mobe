//------------------------------------------------------------------------------
//
// file name：IAtPropertyAccessor.cs
// author: mayanjun
// create date：2010-11-15 10:11:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtProperty
    /// </summary>
    public partial interface IAtPropertyAccessor : IAccessor
    {

        IList<Model.AtProperty> Select(string atProperty);
        IList<Book.Model.AtProperty> Select(DateTime startDate, DateTime endDate);
        IList<Book.Model.AtProperty> SelectByPropertyId(string startPropertyId, string endPropertyId);
    }
}

