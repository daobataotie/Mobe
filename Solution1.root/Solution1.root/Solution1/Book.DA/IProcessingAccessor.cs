//------------------------------------------------------------------------------
//
// file name：IProcessingAccessor.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Processing
    /// </summary>
    public partial interface IProcessingAccessor : IAccessor
    {
        //IList<Book.Model.Processing> Select(Book.Model.Customer customer);

        IList<Book.Model.Processing> Select(Book.Model.ProcessCategory processCategory);
       // IList<Book.Model.Processing> Selectbycategorycustomer(string processCategory, Book.Model.Customer customer);
       // DataTable SelectProceCateByCustom(Model.Customer customer);
        bool ExistsConcent(string Content, string id);
    }
}

