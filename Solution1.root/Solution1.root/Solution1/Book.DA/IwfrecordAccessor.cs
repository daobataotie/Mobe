//------------------------------------------------------------------------------
//
// file name：IwfrecordAccessor.cs
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
    /// Interface of data accessor of dbo.wfrecord
    /// </summary>
    public partial interface IwfrecordAccessor : IAccessor
    {

    IList<Model.wfrecord> GetMyexaming(Model.Operators operators);
    //IList<Model.wfrecord> GetWfrcordbyoperator(Model.Operators operators);
          
    }
}

