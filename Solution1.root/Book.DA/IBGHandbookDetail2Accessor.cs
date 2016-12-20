//------------------------------------------------------------------------------
//
// file name：IBGHandbookDetail2Accessor.cs
// author: mayanjun
// create date：2013-4-16 11:58:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGHandbookDetail2
    /// </summary>
    public partial interface IBGHandbookDetail2Accessor : IAccessor
    {
        IList<Book.Model.BGHandbookDetail2> Select(string pac);
        void UpdateCeIn(string bgid, string lid, double quantity);
        IList<Book.Model.BGHandbookDetail2> SelectbyShouceandId(string bgid, string lid);
        Model.BGHandbookDetail2 SelectBGProduct(string BGHandBookId, string Id);

        IList<Model.BGHandbookDetail2> SelectByShouce(string Id);

        Model.BGHandbookDetail2 SelectByShouceAndId(string Shouce, int id);
    }
}

