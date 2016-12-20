//------------------------------------------------------------------------------
//
// file name：ICustomerAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:25
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Customer
    /// </summary>
    public partial interface ICustomerAccessor : IAccessor
    {
        IList<Model.Customer> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd);
        IList<Model.Customer> selectCustomerInXS();
        bool IsExistFullName(Model.Customer customer);
        bool IsExistShortName(Model.Customer customer);
    }
}

