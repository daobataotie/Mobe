//------------------------------------------------------------------------------
//
// file name：ITechnologydetailsAccessor.cs
// author: peidun
// create date：2009-12-8 16:11:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Technologydetails
    /// </summary>
    public partial interface ITechnologydetailsAccessor : IAccessor
    {
        Book.Model.Technologydetails Select(string proceduresId);
        IList<Book.Model.Technologydetails>  Select(Model.TechonlogyHeader TechonlogyHeader);
        bool IsExists_TechnologydetailsNo(Model.Technologydetails tec);
        IList<Model.Technologydetails> SelectByProceduresId(string ProceduresId, string TechnologydetailsNo);
        void Delete(Model.TechonlogyHeader techonlogyHeader);
       
    }
}

