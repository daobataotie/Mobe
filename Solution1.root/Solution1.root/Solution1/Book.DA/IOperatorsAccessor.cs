//------------------------------------------------------------------------------
//
// file name：IOperatorsAccessor.cs
// author: peidun
// create date：2009-09-09 下午 04:08:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Operators
    /// </summary>
    public partial interface IOperatorsAccessor : IAccessor
    {
        IList<Book.Model.Operators> SelectOperators();

        Book.Model.Operators GetByOperatorName(string operatorName);

        IList<Book.Model.Operators> SelectOrderByName();
    }
}

