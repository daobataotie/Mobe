//------------------------------------------------------------------------------
//
// file name：IProductClassifyAccessor.cs
// author: mayanjun
// create date：2017-08-24 21:36:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductClassify
    /// </summary>
    public partial interface IProductClassifyAccessor : IAccessor
    {
        bool IsExistsKeyWordForInsert(Model.ProductClassify productClassify);

        bool IsExistsKeyWordForUpdate(Model.ProductClassify productClassify);

        IList<string> SelectAllKeyWord();
    }
}
