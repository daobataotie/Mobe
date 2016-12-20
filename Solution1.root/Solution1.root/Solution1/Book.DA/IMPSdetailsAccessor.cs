//------------------------------------------------------------------------------
//
// file name：IMPSdetailsAccessor.cs
// author: peidun
// create date：2009-12-18 11:12:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.MPSdetails
    /// </summary>
    public partial interface IMPSdetailsAccessor : IAccessor
    {
        IList<Book.Model.MPSdetails> Select(Model.MPSheader mPSheader);
        IList<Book.Model.MPSdetails> Select(Model.Customer customer);
        IList<Book.Model.MPSdetails> Select(string customerStart, string customerEnd, string mpsheaderIdStart, string mpsheaderIdEnd, DateTime dateStart, DateTime dateEnd, string productId);

        IList<Book.Model.MPSdetails> Select(Model.InvoiceXO invoiceXO);
        double GetByInvoiceXODetailId(string invoiceXODetailId);
        double GetByMPSdetailsId(string mPSdetailsId);
        IList<Book.Model.MPSdetails> SelectState();
        IList<Book.Model.MPSdetails> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd);
    }
}

