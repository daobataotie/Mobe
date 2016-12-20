//------------------------------------------------------------------------------
//
// file name：IWorkflowAccessor.cs
// author: peidun
// create date：2009-11-18 15:33:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Workflow
    /// </summary>
    public partial interface IWorkflowAccessor : IAccessor
    {


        void addprocess(Model.process process);

        bool getTable(string id);

     Model.Workflow getWfbytable(string tableid);
       
    }
}

