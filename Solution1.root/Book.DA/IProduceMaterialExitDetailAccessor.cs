//------------------------------------------------------------------------------
//
// file name：IProduceMaterialExitDetailAccessor.cs
// author: peidun
// create date：2010-1-6 10:26:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceMaterialExitDetail
    /// </summary>
    public partial interface IProduceMaterialExitDetailAccessor : IAccessor
    {
        IList<Model.ProduceMaterialExitDetail> Select(Model.ProduceMaterialExit ProduceMaterialExit);
        IList<Book.Model.ProduceMaterialExitDetail> Select(string houseid, DateTime startDate, DateTime endDate);
        IList<Model.ProduceMaterialExitDetail> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialExitId0, string produceMaterialExitId1, Model.Product pId0, Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1);
        void Delete(Model.ProduceMaterialExit produceMaterialExit);
    }
}

