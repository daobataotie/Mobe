//------------------------------------------------------------------------------
//
// file name：PCPGOnlineCheckAccessor.cs
// author: mayanjun
// create date：2011-12-6 14:19:08
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
    /// Data accessor of PCPGOnlineCheck
    /// </summary>
    public partial class PCPGOnlineCheckAccessor : EntityAccessor, IPCPGOnlineCheckAccessor
    {
        public IList<Book.Model.PCPGOnlineCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId, string StartPronoteHeader, string EndPronoteHeader)
        {
            SqlParameter[] parames = { 
                new SqlParameter("@StartDate", SqlDbType.DateTime), 
                new SqlParameter("@EndDate", SqlDbType.DateTime), 
                new SqlParameter("@CustomerId", SqlDbType.VarChar), 
                new SqlParameter("@InvoiceCusXOId", SqlDbType.VarChar), 
                new SqlParameter("@ProductId", SqlDbType.VarChar), 
                new SqlParameter("@StartPronoteHeader", SqlDbType.VarChar), 
                new SqlParameter("@EndPronoteHeader", SqlDbType.VarChar)
            };

            parames[0].Value = StartDate.ToString("yyyy-MM-dd");
            parames[1].Value = EndDate.ToString("yyyy-MM-dd");
            if (customer == null)
                parames[2].Value = DBNull.Value;
            else
                parames[2].Value = customer.CustomerId;
            if (string.IsNullOrEmpty(CusXOId))
                parames[3].Value = DBNull.Value;
            else
                parames[3].Value = "%" + CusXOId + "%";
            if (product == null)
                parames[4].Value = DBNull.Value;
            else
                parames[4].Value = product.ProductId;
            if (string.IsNullOrEmpty(StartPronoteHeader))
                parames[5].Value = DBNull.Value;
            else
                parames[5].Value = StartPronoteHeader;
            if (string.IsNullOrEmpty(EndPronoteHeader))
                parames[6].Value = DBNull.Value;
            else
                parames[6].Value = EndPronoteHeader;



            StringBuilder sql = new StringBuilder("SELECT PCPGOnlineCheckId,PCPGOnlineCheckDate,xo.CustomerInvoiceXOId as InvoiceCusXOId,(SELECT EmployeeName FROM Employee WHERE Employee.EmployeeId = PCPGOnlineCheck.EmployeeId) AS EmployeeName,(SELECT ProductName FROM Product WHERE Product.ProductId = PCPGOnlineCheck.ProductId) AS ProductName,(SELECT Customer.CustomerShortName FROM Customer WHERE Customer.CustomerId = PCPGOnlineCheck.CustomerId) AS CustomerShortName,(SELECT Convert(varchar(30),PCPGOnlineCheckDetailDate,120)+'.' FROM PCPGOnlineCheckDetail WHERE PCPGOnlineCheckDetail.PCPGOnlineCheckId=PCPGOnlineCheck.PCPGOnlineCheckId FOR xml path('')) AS DescTime FROM PCPGOnlineCheck left join InvoiceXO xo on PCPGOnlineCheck.InvoiceXOId=xo.InvoiceId WHERE 1 = 1 ");
            sql.Append(" AND PCPGOnlineCheckDate BETWEEN @StartDate AND @EndDate");
            if (customer != null)
                sql.Append(" AND CustomerId = @CustomerId");
            if (!string.IsNullOrEmpty(CusXOId))
                sql.Append(" AND xo.CustomerInvoiceXOId like @InvoiceCusXOId");
            if (product != null)
                sql.Append(" AND PCPGOnlineCheck.ProductId=@ProductId");
            if (!string.IsNullOrEmpty(StartPronoteHeader) && !string.IsNullOrEmpty(EndPronoteHeader))
                sql.Append(" AND FromPCId BETWEEN @StartPronoteHeader AND @EndPronoteHeader");

            return this.DataReaderBind<Model.PCPGOnlineCheck>(sql.ToString(), parames, CommandType.Text);

        }
    }
}
