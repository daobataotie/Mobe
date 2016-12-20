//------------------------------------------------------------------------------
//
// file name：IPCInputCheckAccessor.cs
// author: mayanjun
// create date：2015/4/18 上午 11:58:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCInputCheck
    /// </summary>
    public partial interface IPCInputCheckAccessor : IAccessor
    {
        IList<Model.PCInputCheck> SelectByCondition(DateTime startdate, DateTime enddate, string productid, string testProductid, string supplierid, string lotnumber, bool IsClosed);
        IList<Model.PCInputCheck> SelectByInvoiceCusId(string invoiceCusId);
        bool ExistsLotNumberInsert(string lotNumber, string ProductId);
        bool ExistsLotNumberUpdate(string lotNumber, string PCInputCheckId, string ProductId);
        void UpdateIsClosed(Model.PCInputCheck model);
    }
}
