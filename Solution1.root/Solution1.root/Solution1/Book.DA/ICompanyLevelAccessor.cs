//------------------------------------------------------------------------------
//
// file name：ICompanyLevelAccessor.cs
// author: peidun
// create date：2008/6/30 14:20:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CompanyLevel
    /// </summary>
    public partial interface ICompanyLevelAccessor : IEntityAccessor
    {
        System.Data.DataTable SelectDateTable();

        void UpdateDataTable(System.Data.DataTable table);
    }
}

