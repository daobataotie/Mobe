//------------------------------------------------------------------------------
//
// file name：IPCImpactCheckAccessor.cs
// author: mayanjun
// create date：2011-11-15 13:56:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCImpactCheck
    /// </summary>
    public partial interface IPCImpactCheckAccessor : IAccessor
    {
        IList<Book.Model.PCImpactCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string CusXOId);


        #region 适用于首件上线检查表

        Model.PCImpactCheck PFCGetFirst(string PCFirstOnlineCheckDetailId);

        Model.PCImpactCheck PFCGetLast(string PCFirstOnlineCheckDetailId);

        Model.PCImpactCheck PFCGetPrev(DateTime InsertDate, string PCFirstOnlineCheckDetailId);

        Model.PCImpactCheck PFCGetNext(DateTime InsertDate, string PCFirstOnlineCheckDetailId);

        bool PFCHasRows(string PCFirstOnlineCheckDetailId);

        bool PFCHasRowsBefore(Model.PCImpactCheck e, string PCFirstOnlineCheckDetailId);

        bool PFCHasRowsAfter(Model.PCImpactCheck e, string PCFirstOnlineCheckDetailId);

        IList<Model.PCImpactCheck> PFCSelect(string PCFirstOnlineCheckDetailId);
        
        #endregion
    }
}

