//------------------------------------------------------------------------------
//
// file name：IBGHandbookDetail1Accessor.cs
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
    /// Interface of data accessor of dbo.BGHandbookDetail1
    /// </summary>
    public partial interface IBGHandbookDetail1Accessor : IAccessor
    {
        IList<Book.Model.BGHandbookDetail1> Select(string pac);

        string SelectProName(string BGHandBookId, string Id);

        Model.BGHandbookDetail1 SelectBGProduct(string BGHandBookId, string Id);


        System.Data.DataTable GetBGPrompt();
    }
}

