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
    /// Interface of data accessor of dbo.InvoiceXO
    /// </summary>
    public partial interface IInvoiceXOAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceXO> Select(DateTime start, DateTime end);

        IList<Model.InvoiceXO> Select(Helper.InvoiceStatus status);
        IList<Book.Model.InvoiceXO> SelectNoDeal();
        //修改訂單狀態invoiceFlag
        void Updates(Book.Model.InvoiceXO invoiceXO);

        IList<Book.Model.InvoiceXO> SelectByYJRQCustomEmp(Model.Customer customer, string startDate, string endDate, Model.Employee employee);

        IList<Book.Model.InvoiceXO> SelectByCustomers(Model.Customer customer);
        IList<Model.InvoiceXO> SelectFlagNot2();
    }
}

