//------------------------------------------------------------------------------
//
// file name：ITablesAccessor.cs
// author: peidun
// create date：2009-12-11 14:53:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Tables
    /// </summary>
    public partial interface ITablesAccessor : IAccessor
    {

       Model.Tables GetIDbyname(string tablename);

     //  Model.Tables GetIDbycode(string tablename);  
        

    }
}

