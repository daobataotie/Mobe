//------------------------------------------------------------------------------
//
// file name：IOperationAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Operation
    /// </summary>
    public partial interface IOperationAccessor : IEntityAccessor
    {
        IList<Model.Operation> Select_KeyTag0();
        IList<Model.Operation> Select_ByParent(string ParentId);
        string GetOperationNamebyTabel(string tableName);
      
    }
}

