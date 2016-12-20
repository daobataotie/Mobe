//------------------------------------------------------------------------------
//
// file name：IProduceMaterialdetailsAccessor.cs
// author: peidun
// create date：2009-12-30 16:33:30
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceMaterialdetails
    /// </summary>
    public partial interface IProduceMaterialdetailsAccessor : IAccessor
    {
        IList<Book.Model.ProduceMaterialdetails> Select(Model.ProduceMaterial produceMaterial);
        IList<Book.Model.ProduceMaterialdetails> Select(string houseid, DateTime startDate, DateTime endDate);
        IList<Model.ProduceMaterialdetails> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, string pId0, string pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1);
        IList<Book.Model.ProduceMaterialdetails> SelectByState(Model.ProduceMaterial produceMaterial);
        Model.ProduceMaterialdetails SelectByProductIdAndHeadId(string productId, string produceMaterialId);
    }
}

