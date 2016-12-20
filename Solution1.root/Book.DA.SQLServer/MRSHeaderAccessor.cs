//------------------------------------------------------------------------------
//
// file name：MRSHeaderAccessor.cs
// author: peidun
// create date：2009-12-18 11:12:41
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
    /// Data accessor of MRSHeader
    /// </summary>
    public partial class MRSHeaderAccessor : EntityAccessor, IMRSHeaderAccessor
    {
        public IList<Model.MRSHeader> SelectbySourceType(string type)
        {
            return sqlmapper.QueryForList<Model.MRSHeader>("MRSHeader.selectbySourceType", type);
        }

        public IList<Model.MRSHeader> SelectbyCondition(string mpsstartId, string mpsendId, string customerstartId, string customerendId, DateTime startdate, DateTime enddate, int? sourceType, string id1, string id2, string cusxoid, Model.Product product)
        {
            //    Hashtable ht = new Hashtable();
            //    ht.Add("startcustomerId", customerstartId);
            //    ht.Add("endcustomerId", customerendId);
            //    ht.Add("startmpsheaderId", mpsstartId);
            //    ht.Add("endmpsheaderId", mpsendId);
            //    ht.Add("startdate", startdate);
            //    ht.Add("enddate", enddate);
            SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@xocustomerId", DbType.String), new SqlParameter("@CustomerInvoiceXOId", DbType.String), new SqlParameter("@productid", DbType.String) };
            parames[0].Value = startdate.Date.ToString("yyyy-MM-dd");
            parames[1].Value = enddate.AddDays(1).Date.ToString("yyyy-MM-dd");
            StringBuilder str = new StringBuilder();
            str.Append(" select m.*,c.CustomerShortName,1 as  IsChecked, (select case WHEN exists(select * FROM MRSdetails WHERE MRSdetailssum<>0 AND  (ArrangeDesc IS  NULL or  ArrangeDesc='' )  and MRSdetails.MRSHeaderId=m.MRSHeaderId ) THEN 0 ELSE 1 END) as IsCloseed ");
            str.Append(" ,(SELECT  EmployeeName FROM employee where employee.employeeid=m.Employee0Id) as Employee0Name");
            str.Append(" ,(SELECT  EmployeeName FROM employee where employee.employeeid=m.Employee1Id) as Employee1Name");
            str.Append(",(select CustomerInvoiceXOId from invoicexo where invoiceid=(select InvoiceXOId from MPSheader where MPSheader.MPSheaderId=m.MPSheaderId)) as CustomerInvoiceXOId");
            str.Append(",(select InvoiceYjrq from invoicexo where invoiceid=(select InvoiceXOId from MPSheader where MPSheader.MPSheaderId=m.MPSheaderId))  as YjrqDate ");
            str.Append(",(select CustomerLotNumber from invoicexo where invoiceid=(select InvoiceXOId from MPSheader where MPSheader.MPSheaderId=m.MPSheaderId))  as PiHao ");

            str.Append("  from mrsheader m left join Customer c on c.CustomerId=m.CustomerId    where  (mrsstartdate between @startdate and @enddate )");
            if (!string.IsNullOrEmpty(cusxoid))
                str.Append("  and  m.MPSheaderId in(select MPSheaderId from MPSheader where InvoiceXOId in(select InvoiceId from InvoiceXO where CustomerInvoiceXOId like '%" + cusxoid + "%' ) )");

            if (sourceType != -1) //非全部
            {
                str.Append(" and  SourceType='" + sourceType.ToString() + "'");
            }


            if (!string.IsNullOrEmpty(id1) && !string.IsNullOrEmpty(id2))
            {
                str.Append(" and  m.MRSHeaderId between '" + id1 + "' and '" + id2 + "' ");
            }
            if (!string.IsNullOrEmpty(mpsstartId) && !string.IsNullOrEmpty(mpsendId))
            {
                str.Append(" and  m.MPSHeaderId between '" + mpsstartId + "' and '" + mpsendId + "' ");
            }

            if (product != null)
            {
                str.Append(" and  m.MRSHeaderId in(select MRSHeaderId  from MRSdetails where productid='" + product.ProductId + "')");
            }

            str.Append(" order by  m.mrsheaderid desc ");
            return this.DataReaderBind<Model.MRSHeader>(str.ToString(), parames, CommandType.Text);
        }

        public bool SelectIsCloseed(string mrsid)
        {
            return sqlmapper.QueryForObject<bool>("MRSHeader.IsCloseed", mrsid);
        }
    }
}
