//------------------------------------------------------------------------------
//
// file name：IBGProductDetaiAccessor.cs
// author: mayanjun
// create date：2013-4-1 11:58:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGProductDetai
    /// </summary>
    public partial interface IBGProductDetaiAccessor : IAccessor
    {
        void DeleteByBGProductId(string BGProductId);

        IList<Model.BGProductDetai> SelectProductByBGProductId(string BGProductId);

        IList<Model.BGProductDetai> SelectMaterialByBGProductId(string BGProductId);
    }
}

