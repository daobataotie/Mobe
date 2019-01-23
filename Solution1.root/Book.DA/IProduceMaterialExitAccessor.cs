//------------------------------------------------------------------------------
//
// file name：IProduceMaterialExitAccessor.cs
// author: peidun
// create date：2010-1-6 10:20:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceMaterialExit
    /// </summary>
    public partial interface IProduceMaterialExitAccessor : IAccessor
    {
        IList<Model.ProduceMaterialExit> SelectByCondition(DateTime start, DateTime end);

        IList<Model.ProduceMaterialExit> SelectByProduceHeaderId(string pronoteHeaderid);

        IList<Book.Model.ProduceMaterialExit> SelectForListForm(DateTime startDate, DateTime endDate, string startPMEId, string endPMEId, string startPronoteHeaderId, string endPronoteHeaderId, Book.Model.Product startProduct, Book.Model.Product endProduct, string workhouseId, string invoiceXOCusId, string handBookId);
    }
}

