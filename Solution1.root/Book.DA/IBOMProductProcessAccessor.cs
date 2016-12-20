//------------------------------------------------------------------------------
//
// file name：IBOMProductProcessAccessor.cs
// author: peidun
// create date：2009-11-14 9:44:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BOMProductProcess
    /// </summary>
    public partial interface IBOMProductProcessAccessor : IAccessor
    {
        void Delete(Model.BomParentPartInfo bomParentPartInfo);
        IList<Model.BOMProductProcess> Select(string bomId);
    }
}

