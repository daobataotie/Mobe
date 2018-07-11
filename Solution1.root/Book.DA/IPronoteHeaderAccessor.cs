//------------------------------------------------------------------------------
//
// file name：IPronoteHeaderAccessor.cs
// author: peidun
// create date：2009-12-29 11:58:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronoteHeader
    /// </summary>
    public partial interface IPronoteHeaderAccessor : IAccessor
    {
        IList<Book.Model.PronoteHeader> GetByDate(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5);

        IList<Book.Model.PronoteHeader> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd, string CusXOId);

        IList<Book.Model.PronoteHeader> Select(Model.MRSHeader mrsheader);
        //void UpdatePronoteIsClose(string pronoteheaderid, double? indepotquantity);
        void UpdateHeaderIsClse(string pronoteheaderid, bool isclose);

        IList<Book.Model.PronoteHeader> GetByDateMa(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5);

        IList<Book.Model.PronoteHeader> GetByDateZJ(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5);
        void UpdateHeaderIsClseByXOId(string InvoiceXOId, bool isclose);
        IList<Book.Model.PronoteHeader> Select(IList<string> ids);

        IList<Model.PronoteHeader> SelectByProductId(DateTime startDate, DateTime endDate, string productid);

        IList<Model.PronoteHeader> SelectByProductIdAll(string productid);
    }
}
