//------------------------------------------------------------------------------
//
// file name：AcInvoiceCOBillDetailAccessor.cs
// author: mayanjun
// create date：2011-06-27 15:07:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of AcInvoiceCOBillDetail
    /// </summary>
    public partial class AcInvoiceCOBillDetailAccessor : EntityAccessor, IAcInvoiceCOBillDetailAccessor
    {
        public IList<Model.AcInvoiceCOBillDetail> SelectByAcInvoiceCOBill(Model.AcInvoiceCOBill acInvoiceCoBill)
        {
            return sqlmapper.QueryForList<Model.AcInvoiceCOBillDetail>("AcInvoiceCOBillDetail.selectByAcInvoiceCOBill", acInvoiceCoBill.AcInvoiceCOBillId);
        }

        public void Delete(Model.AcInvoiceCOBill acInvoiceCoBill)
        {
            sqlmapper.Delete("AcInvoiceCOBillDetail.delete_detail_AcinvoiceCoBilID", acInvoiceCoBill.AcInvoiceCOBillId);
        }
    }
}
