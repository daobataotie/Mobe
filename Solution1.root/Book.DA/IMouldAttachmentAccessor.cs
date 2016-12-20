//------------------------------------------------------------------------------
//
// file name：IMouldAttachmentAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:25
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.MouldAttachment
    /// </summary>
    public partial interface IMouldAttachmentAccessor : IAccessor
    {
        void DeleteByMouldid(string mouldid);
        IList<Model.MouldAttachment> SelectByMouldId(Model.ProductMould mould);
    }
}

