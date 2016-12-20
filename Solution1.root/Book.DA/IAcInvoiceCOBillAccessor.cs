//------------------------------------------------------------------------------
//
// file name：IAcInvoiceCOBillAccessor.cs
// author: mayanjun
// create date：2011-06-27 15:07:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcInvoiceCOBill
    /// </summary>
    public partial interface IAcInvoiceCOBillAccessor : IAccessor
    {
        IList<Model.AcInvoiceCOBill> SelectByDateRange(DateTime startdate, DateTime enddate);
        void UpdateHeXiaobyAcinvoiceCOId(Hashtable ht);
        DataSet SelectMayShou(Model.Supplier supplier1, Model.Supplier supplier2, Model.Employee employee1, Model.Employee employee2, DateTime startDate, DateTime endDate);
  
    }
}

