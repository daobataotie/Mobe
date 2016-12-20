//------------------------------------------------------------------------------
//
// file name：ICustomerProcessingDetailAccessor.cs
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
    /// Interface of data accessor of dbo.CustomerProcessingDetail
    /// </summary>
    public partial interface ICustomerProcessingDetailAccessor : IAccessor
    {
        IList<Model.CustomerProcessingDetail> Select(Model.CustomerProcessing CustomerProcessing);
        IList<Model.CustomerProcessingDetail> SelectbyBomId(string bomid);   
    }
}

