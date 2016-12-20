//------------------------------------------------------------------------------
//
// file name：IAcInvoiceXOBillAccessor.cs
// author: mayanjun
// create date：2011-09-28 08:45:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcInvoiceXOBill
    /// </summary>
    public partial interface IAcInvoiceXOBillAccessor : IAccessor
    {
        IList<Model.AcInvoiceXOBill> SelectByDateRange(DateTime startdate, DateTime enddate);
        void UpdateHeXiaoByAcInvoiceXOBillId(Hashtable param);
        DataSet SelectCuiShou(Model.Customer customer1, Model.Customer customer2, Model.Employee employee1, Model.Employee employee2, DateTime ysdate);
        DataSet SelectMayShou(Model.Customer customer1, Model.Customer customer2, Model.Employee employee1, Model.Employee employee2, DateTime startDate, DateTime endDate);
      
     
    }
}

