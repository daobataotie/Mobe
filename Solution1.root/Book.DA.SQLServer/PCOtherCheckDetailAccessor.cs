//------------------------------------------------------------------------------
//
// file name：PCOtherCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-10 15:05:57
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
    /// Data accessor of PCOtherCheckDetail
    /// </summary>
    public partial class PCOtherCheckDetailAccessor : EntityAccessor, IPCOtherCheckDetailAccessor
    {
        public IList<Book.Model.PCOtherCheckDetail> Selct(Book.Model.PCOtherCheck _Pcoc)
        {
            return sqlmapper.QueryForList<Model.PCOtherCheckDetail>("PCOtherCheckDetail.select_byPCOtherCheckId", _Pcoc.PCOtherCheckId);
        }

        public void DeleteByPCOCId(string pcocId)
        {
            sqlmapper.Delete("PCOtherCheckDetail.DeleteByPCOCId", pcocId);
        }

        public IList<Model.PCOtherCheckDetail> SelectByConditon(DateTime StartDate, DateTime EndDate, Book.Model.Product product, string CusXOId)
        {
            string startDate = StartDate.ToString("yyyy-MM-dd");
            string endDate = EndDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder sql = new StringBuilder();
            sql.Append("select p.PCOtherCheckId,p.PCOtherCheckDate,pd.InvoiceCusXOId,e.EmployeeName,ee.EmployeeName as EmployeeName1,s.SupplierFullName,pd.PCOtherCheckDetailDesc1,pro.ProductName from PCOtherCheckDetail pd left join PCOtherCheck p on pd.PCOtherCheckId=p.PCOtherCheckId left join Employee e on p.Employee0Id=e.EmployeeId  left join Product pro on pd.ProductId=pro.ProductId left join Employee ee on p.CBEmployeeId=ee.EmployeeId left join Supplier s on s.SupplierId=p.SupplierId where p.PCOtherCheckDate between '" + startDate + "' and '" + endDate + "'");
            if (product != null)
                sql.Append(" and pd.ProductId='" + product.ProductId + "'");
            if (!string.IsNullOrEmpty(CusXOId))
                sql.Append(" and pd.InvoiceCusXOId='" + CusXOId + "'");

            return this.DataReaderBind<Model.PCOtherCheckDetail>(sql.ToString(), null, CommandType.Text);
        }
    }
}
