//------------------------------------------------------------------------------
//
// file name：IANSIPCImpactCheckAccessor.cs
// author: mayanjun
// create date：2011-11-23 09:49:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ANSIPCImpactCheck
    /// </summary>
    public partial interface IANSIPCImpactCheckAccessor : IAccessor
    {
        IList<Model.ANSIPCImpactCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string cusXOId, string ForANSIOrJIS);

        Model.ANSIPCImpactCheck GetLastByForANSIOrJIS(string ForANSIOrJIS);

        Model.ANSIPCImpactCheck GetFirstByForANSIOrJIS(string ForANSIOrJIS);

        bool HasRowsByForANSIOrJIS(string ForANSIOrJIS);

        bool HasRowsBeforeByForANSIOrJIS(Model.ANSIPCImpactCheck e);

        bool HasRowsAfterByForANSIOrJIS(Model.ANSIPCImpactCheck e);

        Model.ANSIPCImpactCheck GetNextByForANSIOrJIS(Model.ANSIPCImpactCheck e);

        Model.ANSIPCImpactCheck GetPrevByForANSIOrJIS(Model.ANSIPCImpactCheck e);
    }
}

