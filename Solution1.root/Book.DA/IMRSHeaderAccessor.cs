//------------------------------------------------------------------------------
//
// file name：IMRSHeaderAccessor.cs
// author: peidun
// create date：2009-12-18 11:12:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.MRSHeader
    /// </summary>
    public partial interface IMRSHeaderAccessor : IAccessor
    {
        IList<Model.MRSHeader> SelectbySourceType(string type);
        IList<Model.MRSHeader> SelectbyCondition(string mrsstartId, string mrsendId, string customerstartId, string customerendId, DateTime startdate, DateTime enddate, int? sourceType, string id1, string id2, string cusxoid, Model.Product product);
        bool SelectIsCloseed(string mrsid);

        IList<string> SelectAllProductIdByMRSHeaderId(string MRSHerderId, string handBookProductId);

        IList<string> SelectAllProductIdByInvoiceXOId(string invoiceXOId);
    }
}

