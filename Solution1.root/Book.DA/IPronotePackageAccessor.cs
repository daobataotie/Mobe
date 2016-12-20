//------------------------------------------------------------------------------
//
// file name：IPronotePackageAccessor.cs
// author: mayanjun
// create date：2011-07-20 16:57:14
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronotePackage
    /// </summary>
    public partial interface IPronotePackageAccessor : IAccessor
    {
        void UpdateData(DataTable pronotePackage);
        DataSet GetDataTable(DateTime date);
        IList<Model.PronotePackage> SelectByDateRange(DateTime startdate, DateTime enddate);
    }
}

