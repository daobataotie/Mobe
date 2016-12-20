//------------------------------------------------------------------------------
//
// file name：IProduceMaterialAccessor.cs
// author: peidun
// create date：2009-12-30 16:33:30
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceMaterial
    /// </summary>
    public partial interface IProduceMaterialAccessor : IAccessor
    {
        IList<Model.ProduceMaterial> SelectbypronoteHeaderId(string pronoteHeaderId);
        DataTable GetbypronoteHeaderId(string pronoteHeaderId);
        void UpdateProduceMaterial(DataTable dt);
        IList<Model.ProduceMaterial> SelectByDateRage(DateTime startdate, DateTime enddate);
        IList<Model.ProduceMaterial> SelectState();
        IList<Model.ProduceMaterial> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, string pId0, string pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1);
    }
}

