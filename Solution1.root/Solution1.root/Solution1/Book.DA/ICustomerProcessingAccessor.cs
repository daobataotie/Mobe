//------------------------------------------------------------------------------
//
// file name：ICustomerProcessingAccessor.cs
// author: mayanjun
// create date：2010-7-30 19:31:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CustomerProcessing
    /// </summary>
    public partial interface ICustomerProcessingAccessor : IAccessor
    {
        IList<Model.CustomerProcessing> Select(Model.Customer Customer);
     
    }
}

