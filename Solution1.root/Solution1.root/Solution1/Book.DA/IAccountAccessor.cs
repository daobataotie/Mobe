//------------------------------------------------------------------------------
//
// file name：IAccountAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Account
    /// </summary>
    public partial interface IAccountAccessor : IEntityAccessor
    {
        System.Data.DataTable SelectDataTable();
        void UpdateDataTable(System.Data.DataTable accounts);

        void Increment(Model.Account account, decimal value);
        void Decrement(Model.Account account, decimal value);
        void Increment(Model.Account account, decimal? value);
        void Decrement(Model.Account account, decimal? value);
    }
}

