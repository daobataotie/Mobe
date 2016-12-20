//------------------------------------------------------------------------------
//
// file name：IInvoiceXTAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceXT
    /// </summary>
    public partial interface IInvoiceXTAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceXT> Select(DateTime start, DateTime end);
        void OwedIncrement(Model.InvoiceXT invoice, decimal money);
        void OwedDecrement(Model.InvoiceXT invoice, decimal money);
        void OwedIncrement(Model.InvoiceXT invoice, decimal? money);
        void OwedDecrement(Model.InvoiceXT invoice, decimal? money);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice">销售退货单单据编号</param>
        /// <param name="money"></param>
        void OwedIncrement(string invoice, decimal money);
        void OwedDecrement(string invoice, decimal money);
        void OwedIncrement(string invoice, decimal? money);
        void OwedDecrement(string invoice, decimal? money);

        IList<Model.InvoiceXT> Select(Helper.InvoiceStatus status);
    }
}

