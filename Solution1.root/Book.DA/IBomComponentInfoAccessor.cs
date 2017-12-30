//------------------------------------------------------------------------------
//
// file name：IBomComponentInfoAccessor.cs
// author: peidun
// create date：2009-08-25 17:08:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BomComponentInfo
    /// </summary>
    public partial interface IBomComponentInfoAccessor : IAccessor
    {
        IList<Book.Model.BomComponentInfo> Select(Book.Model.BomParentPartInfo par);

        void Delete(Book.Model.BomParentPartInfo bomParentPartInfo);

        IList<Model.BomComponentInfo> SelectNotContent(Model.BomParentPartInfo bom);

        Model.BomComponentInfo IsExistsIndexOfBom(Model.BomComponentInfo bomcom);

        IList<Model.BomComponentInfo> SelectLessInfoByHeaderId(string BomId);

        IList<Model.BomComponentInfo> SelectBomIdAndUseQty(string productIds);
    }
}

