//------------------------------------------------------------------------------
//
// file name：IAcInvoiceCOBillDetailAccessor.cs
// author: mayanjun
// create date：2011-06-27 15:07:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcInvoiceCOBillDetail
    /// </summary>
    public partial interface IAcInvoiceCOBillDetailAccessor : IAccessor
    {
        IList<Model.AcInvoiceCOBillDetail> SelectByAcInvoiceCOBill(Model.AcInvoiceCOBill acInvoiceCoBill);
        void Delete(Model.AcInvoiceCOBill acInvoiceCOBill);
    }
}

