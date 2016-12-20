//------------------------------------------------------------------------------
//
// file name：ICompanyAccessor.cs
// author: peidun
// create date：2009-09-02 上午 10:38:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Company
    /// </summary>
    public partial interface ICompanyAccessor : IAccessor
    {
        bool IsExistsCompanyName(string CompanyId, string CompanyName);
        Model.Company SelectIsDefaultCompany();
    }
}

