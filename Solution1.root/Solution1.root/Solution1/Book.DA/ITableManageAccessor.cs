//------------------------------------------------------------------------------
//
// file name：ITableManageAccessor.cs
// author: peidun
// create date：2009-11-23 10:26:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.TableManage
    /// </summary>
    public partial interface ITableManageAccessor : IAccessor
    {

        Model.TableManage GetIDbyname(string tablename);

        Model.TableManage GetIDbyDBname(string dbtablename);
       
        
    }
}

