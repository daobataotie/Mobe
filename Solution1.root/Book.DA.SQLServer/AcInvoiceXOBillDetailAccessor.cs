//------------------------------------------------------------------------------
//
// file name：AcInvoiceXOBillDetailAccessor.cs
// author: mayanjun
// create date：2011-09-28 08:45:16
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
    /// Data accessor of AcInvoiceXOBillDetail
    /// </summary>
    public partial class AcInvoiceXOBillDetailAccessor : EntityAccessor, IAcInvoiceXOBillDetailAccessor
    {
        public IList<Model.AcInvoiceXOBillDetail> SelectByAcInvoiceXOBill(Model.AcInvoiceXOBill acInvoiceXoBill)
        {
            return sqlmapper.QueryForList<Model.AcInvoiceXOBillDetail>("AcInvoiceXOBillDetail.selectByAcInvoiceXOBill", acInvoiceXoBill.AcInvoiceXOBillId);
        }

        public void Delete(Model.AcInvoiceXOBill acInvoiceXoBill)
        {
            sqlmapper.Delete("AcInvoiceXOBillDetail.delete_detail_AcinvoiceXoBilID", acInvoiceXoBill.AcInvoiceXOBillId);
        }

        public IList<Book.Model.AcInvoiceXOBillDetail> selectByConditionInvoiceXODetail(DateTime? startdate, DateTime? enddate, string IdStart, string IdEnd, Book.Model.Customer startCustomer, Book.Model.Customer endCustomer)
        {
            StringBuilder sb = new StringBuilder();
            if (startdate.HasValue && enddate.HasValue)
            {
                sb.Append(" AND AcInvoiceXOBillDetail.AcInvoiceXOBillId IN (SELECT AcInvoiceXOBill.AcInvoiceXOBillId FROM AcInvoiceXOBill WHERE AcInvoiceXOBill.AcInvoiceXOBillDate BETWEEN '" + startdate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + enddate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            if (!string.IsNullOrEmpty(IdStart) && !string.IsNullOrEmpty(IdEnd))
            {
                sb.Append("AND AcInvoiceXOBillDetail.AcInvoiceXOBillId IN (SELECT AcInvoiceXOBill.AcInvoiceXOBillId FROM AcInvoiceXOBill WHERE AcInvoiceXOBill.Id BETWEEN '" + IdStart + "' AND '" + IdEnd + "')");
            }
            if (startCustomer != null && endCustomer != null)
            {
                sb.Append("AND AcInvoiceXOBillDetail.AcInvoiceXOBillId IN (SELECT AcInvoiceXOBill.AcInvoiceXOBillId FROM AcInvoiceXOBill WHERE AcInvoiceXOBill.CustomerId BETWEEN '" + startCustomer.CustomerId + "' AND '" + endCustomer.CustomerId + "')");
            }

            return sqlmapper.QueryForList<Model.AcInvoiceXOBillDetail>("AcInvoiceXOBillDetail.selectByConditionInvoiceXODetail", sb.ToString());
        }
    }
}
