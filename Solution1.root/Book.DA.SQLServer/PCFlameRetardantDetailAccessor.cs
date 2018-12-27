//------------------------------------------------------------------------------
//
// file name：PCFlameRetardantDetailAccessor.cs
// author: mayanjun
// create date：2018/12/27 13:17:16
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
    /// Data accessor of PCFlameRetardantDetail
    /// </summary>
    public partial class PCFlameRetardantDetailAccessor : EntityAccessor, IPCFlameRetardantDetailAccessor
    {
        public IList<Model.PCFlameRetardantDetail> SelectByPrimaryId(string primaryId)
        {
            return sqlmapper.QueryForList<Model.PCFlameRetardantDetail>("PCFlameRetardantDetail.SelectByPrimaryId", primaryId);
        }

        public void DeleteByPrimaryId(string primaryid)
        {
            sqlmapper.Delete("PCFlameRetardantDetail.DeleteByPrimaryId", primaryid);
        }

        public IList<Model.PCFlameRetardantDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productid, string cusXOId)
        {
            StringBuilder sb = new StringBuilder("select pfd.*,pf.InvoiceDate,p.ProductName,e.EmployeeName,xo.CustomerInvoiceXOId from PCFlameRetardantDetail pfd left join PCFlameRetardant pf on pfd.PCFlameRetardantId=pf.PCFlameRetardantId left join Product p on pfd.ProductId=p.ProductId left join Employee e on pfd.EmployeeId=e.EmployeeId left join InvoiceXO xo on pfd.InvoiceXOId=xo.InvoiceId where pfd.PCFlameRetardantId in (select PCFlameRetardantId from PCFlameRetardant where invoiceDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");

            if (!string.IsNullOrEmpty(productid))
                sb.Append(" and p.ProductId='" + productid + "'");
            if (!string.IsNullOrEmpty(cusXOId))
                sb.Append(" and xo.CustomerInvoiceXOId='" + cusXOId + "'");

            sb.Append("order by pfd.PCFlameRetardantId,pfd.Number");

            return this.DataReaderBind<Model.PCFlameRetardantDetail>(sb.ToString(), null, CommandType.Text);
        }
    }
}
