//------------------------------------------------------------------------------
//
// file name：IInvoiceXOAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.In voiceXO
    /// </summary>
    public partial interface IInvoiceXOAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceXO> Select(DateTime start, DateTime end);

        IList<Model.InvoiceXO> Select(Helper.InvoiceStatus status);
        IList<Book.Model.InvoiceXO> SelectNoDeal();
        IList<Book.Model.InvoiceXO> SelectByYJRQCustomEmpCusXOId(Model.Customer customer1, Model.Customer customer2, DateTime startDate, DateTime endDate, DateTime yjrq1, DateTime yjrq2, Model.Employee employee1, Model.Employee employee2, string xoid1, string xoid2, string cusxoidkey, Model.Product product, Model.Product product2, bool isclose, bool mpsIsClose, bool isForeigntrade);
        IList<Book.Model.InvoiceXO> SelectByCustomers(Model.Customer customer);
        IList<Model.InvoiceXO> SelectFlagNot2();
        IList<Model.InvoiceXO> SelectDateRangCusXOCustomer(DateTime startdate, DateTime enddate, string cusxoid, Model.Customer customer);
        Model.InvoiceXO SelectMpsIsClose(string mpsheader);
  
    }
}

