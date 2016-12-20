//------------------------------------------------------------------------------
//
// file name：IProceduresAccessor.cs
// author: peidun
// create date：2009-12-8 10:55:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Procedures
    /// </summary>
    public partial interface IProceduresAccessor : IAccessor
    {
        IList<Model.Procedures> Select(Model.TechonlogyHeader techonlogyHeader);
        IList<Model.Procedures> Select(Model.BomParentPartInfo bomPart);
        IList<Book.Model.Procedures> Select(string workHouseId);
    }
}

