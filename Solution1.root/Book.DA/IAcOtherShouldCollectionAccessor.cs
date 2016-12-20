//------------------------------------------------------------------------------
//
// file name：IAcOtherShouldCollectionAccessor.cs
// author: mayanjun
// create date：2011-6-10 11:19:27
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcOtherShouldCollection
    /// </summary>
    public partial interface IAcOtherShouldCollectionAccessor : IAccessor
    {
        IList<Model.AcOtherShouldCollection> SelectByDateRange(DateTime startdate, DateTime enddate);

        IList<Model.AcOtherShouldCollection> SelectByDateRangeAndCustomerCompany(DateTime startdate, DateTime enddate, Model.Customer customer,Model.Company company);
    }
}

