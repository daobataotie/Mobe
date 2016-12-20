//------------------------------------------------------------------------------
//
// file name：IAcInvoiceXOBillDetailAccessor.cs
// author: mayanjun
// create date：2011-09-28 08:45:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcInvoiceXOBillDetail
    /// </summary>
    public partial interface IAcInvoiceXOBillDetailAccessor : IAccessor
    {
        IList<Model.AcInvoiceXOBillDetail> SelectByAcInvoiceXOBill(Model.AcInvoiceXOBill acInvoiceXoBill);
        void Delete(Model.AcInvoiceXOBill acInvoiceXoBill);
        IList<Model.AcInvoiceXOBillDetail> selectByConditionInvoiceXODetail(DateTime? startdate, DateTime? enddate, string IdStart, string IdEnd, Model.Customer startCustomer, Model.Customer endCustomer);
    }
}

