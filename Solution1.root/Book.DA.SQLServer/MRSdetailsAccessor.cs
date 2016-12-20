//------------------------------------------------------------------------------
//
// file name：MRSdetailsAccessor.cs
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
    /// Data accessor of MRSdetails
    /// </summary>
    public partial class MRSdetailsAccessor : EntityAccessor, IMRSdetailsAccessor
    {
        public IList<Book.Model.MRSdetails> Select(Model.MRSHeader mRSheader)
        {
            //StringBuilder sb = new StringBuilder("SELECT *,(SELECT (ProduceMaterialDistributioned+OtherMaterialDistributioned) FROM Product WHERE Product.ProductId = MRSdetails.ProductId) AS ProductFPSLsum");
            //sb.Append(" ,(SELECT ProductName FROM Product WHERE Product.ProductId = MRSdetails.ProductId) AS ProductName");
            //sb.Append(" ,(SELECT ProductName FROM Product WHERE Product.ProductId = MRSdetails.MadeProductId) AS MadeProductName");
            //sb.Append(" ,(SELECT SupplierShortName FROM Supplier WHERE Supplier.SupplierId = MRSdetails.SupplierId) AS SupplierShortName");
            //sb.Append(" FROM MRSdetails WHERE MRSHeaderId='" + mRSheader.MRSHeaderId + "' order by Inumber");

            //return this.DataReaderBind<Model.MRSdetails>(sb.ToString(), null, CommandType.Text);
            return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.select_byMRSheaderId", mRSheader.MRSHeaderId);
        }

        //public IList<Book.Model.MRSdetails> SelectBySqlMap(Model.MRSHeader mRSheader)
        //{

        //}


        public IList<Book.Model.MRSdetails> Select(string mpsHeaderId, string sourceType, string sourceType1, string sourceType2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("mpsHeaderId", mpsHeaderId);
            ht.Add("sourceType", sourceType);
            ht.Add("SourceType1", sourceType1);
            ht.Add("SourceType2", sourceType2);
            return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.selectBySourceTypeAndMPS", ht);
        }

        public IList<Book.Model.MRSdetails> Select(DateTime startDate, DateTime endDate, string sourceType, string sourceType1, string sourceType2, string CusXOId, int single)
        {
            //最后参数0查询已排和未排
            SqlParameter[] parames = {  
                                         new SqlParameter("@startdate", DbType.DateTime),
                                         new SqlParameter("@enddate", DbType.DateTime),
                                         new SqlParameter("@SourceType", DbType.String),
                                         new SqlParameter("@SourceType1", DbType.String),
                                         new SqlParameter("@SourceType2", DbType.String),                                      
                                         new SqlParameter("@cusxoid", DbType.String),
                                         new SqlParameter("@single", DbType.String),
                                     };

            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (sourceType == null)
                parames[2].Value = DBNull.Value;
            else
                parames[2].Value = sourceType;
            if (sourceType1 == null)
                parames[3].Value = DBNull.Value;
            else
                parames[3].Value = sourceType1;
            if (sourceType2 == null)
                parames[4].Value = DBNull.Value;
            else
                parames[4].Value = sourceType2;
            if (CusXOId == null)
                parames[5].Value = DBNull.Value;
            else
                parames[5].Value = CusXOId;

            parames[6].Value = single;



            StringBuilder sql = new StringBuilder();
            sql.Append(" select a.mrsdetailsid,a.MadeProductId,a.productid,b.ProductName,b.CustomerProductName,a.mrsheaderid,a.mrsdetailsid,a.mpsheaderid,a.productunit,a.MRSdetailssum, a.MRSHasSingleSum,(select productname from product p where p.productid=a.MadeProductId ) as MadeProductName, (select  EmployeeName from employee where employee.employeeid=(select employee0id from mrsheader m where m.mrsheaderid=a.mrsheaderid  )) as Employee0Name,(select  CustomerInvoiceXOId  from invoicexo where invoiceid=(select invoicexoid from mpsheader where  mpsheader.mpsheaderid=(select mpsheaderid from mrsheader m where m.mrsheaderid=a.mrsheaderid  ))  ) as CustomerInvoiceXOId,");
            sql.Append("(select invoicexoid from mpsheader where  mpsheader.mpsheaderid=(select mpsheaderid from mrsheader m where m.mrsheaderid=a.mrsheaderid  ) ) as InvoiceXOId ,");
            sql.Append(" (select  CustomerShortName from customer where customerid=(select xocustomerId from  invoicexo where invoiceid=(select invoicexoid from mpsheader where  mpsheader.mpsheaderid=(select mpsheaderid from mrsheader m where m.mrsheaderid=a.mrsheaderid  ))))  as CustomerName");
            sql.Append(" from mrsDetails a left join product b on a.productid=b.productid  ");
            sql.Append("where MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE MRSstartdate between @startdate and @enddate and SourceType=@SourceType or SourceType=@SourceType1  or SourceType=@SourceType2 )  and MRSdetailssum <>0 ");
            sql.Append("and ( (mrsheaderid in(select mrsheaderid where  MPSheaderId in(select MPSheaderId from MPSheader where InvoiceXOId in(select InvoiceId from InvoiceXO where CustomerInvoiceXOId=@cusxoid ) ))) or (@cusxoid is null))  and (@single =0 or MRSHasSingleSum<MRSdetailssum ) order by MRSHeaderId desc");

            return this.DataReaderBind<Model.MRSdetails>(sql.ToString(), parames, CommandType.Text);

            //Hashtable ht = new Hashtable();                   
            //ht.Add("SourceType", sourceType);           
            //ht.Add("SourceType1", sourceType1);
            //ht.Add("SourceType2", sourceType2);
            //ht.Add("startdate", startDate);
            //ht.Add("enddate", endDate);
            //ht.Add("cusxoid", CusXOId);
            //ht.Add("single", single);

            //return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.select_GetDate", ht);

        }

        public IList<Book.Model.MRSdetails> GetByMRSIDAndProId(string mrsid, string proid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MRSHeaderId", mrsid);
            ht.Add("ProductId", proid);
            return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.select_GetByMRSIDAndProId", ht);
        }

        public IList<Book.Model.MRSdetails> SelectWhere(string sqlwhere)
        {
            return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.select_where", sqlwhere);
        }

        public void DeleteByHeader(Model.MRSHeader header)
        {
            sqlmapper.Delete("MRSdetails.DeleteByHeader", header.MRSHeaderId);
        }

        public IList<Book.Model.MRSdetails> SelectbyCondition(string mpsstartId, string mpsendId, string customerstartId, string customerendId, DateTime startdate, DateTime enddate, int? sourceType, string id1, string id2, string cusxoid, Book.Model.Product product, int OrderColumn, int OrderType, Model.ProductCategory productCate)
        {


            StringBuilder str = new StringBuilder();
            str.Append(" select  c.ProductCategoryName , a.MRSHeaderId,b.ProductName,(ISNULL(b.ProduceMaterialDistributioned,0)+isnull( b.OtherMaterialDistributioned,0)) AS ProduceDistributioned , b.OrderOnWayQuantity, b.StocksQuantity ,b.CustomerProductName,a.ArrangeDesc,a.MPSheaderId,a.ProductUnit,a.MRSdetailssum,a.MRSdetailsQuantity,a.JiaoHuoDate ,(select  EmployeeName from employee where employee.employeeid=(select employee0id from mrsheader m where m.mrsheaderid=a.mrsheaderid  )) as Employee0Name,(select  CustomerInvoiceXOId  from invoicexo where invoiceid=(select invoicexoid from mpsheader where  mpsheader.mpsheaderid=(select mpsheaderid from mrsheader m where m.mrsheaderid=a.mrsheaderid  ))  ) as CustomerInvoiceXOId,");
            str.Append(" (select SupplierShortName from Supplier s where s.SupplierId=a.SupplierId) as SupplierId,");


            str.Append(" (select MRSstartdate from MRSHeader m where m.MRSHeaderId=a.MRSHeaderId) as MRSstartdate,");
            str.Append(" (select case  SourceType when '0' then '自製' when '1' then '外購' when '2' then '耗用' when '3' then '委外' when '4' then '自製(組裝)' when '5' then '自製(半成品加工)' when '6' then '委外(半成品加工)' end   from MRSHeader m where m.MRSHeaderId=a.MRSHeaderId) as SourceTypeName ,");

            str.Append(" (select  CustomerShortName from customer where customerid=(select xocustomerId from  invoicexo where invoiceid=(select invoicexoid from mpsheader where  mpsheader.mpsheaderid=(select mpsheaderid from mrsheader m where m.mrsheaderid=a.mrsheaderid  ))))  as CustomerName");
            str.Append(" from product b right join  mrsDetails a   on a.productid=b.productid left  join ProductCategory c  on c.ProductCategoryId=b.ProductCategoryId  ");


            str.Append(" where a.MRSHeaderId IN (SELECT MRSHeader.MRSHeaderId FROM MRSHeader WHERE  MRSstartdate between '" + startdate.ToString("yyyy-MM-dd") + "' and  '" + enddate.ToString("yyyy-MM-dd") + "' )");

            if (!string.IsNullOrEmpty(customerstartId) && !string.IsNullOrEmpty(customerendId))
                str.Append(" AND a.MRSHeaderId IN (SELECT MRSHeader.MRSHeaderId FROM MRSHeader WHERE  CustomerId between '" + customerstartId + "' and '" + customerendId + "' )");
            if (!string.IsNullOrEmpty(mpsstartId) && !string.IsNullOrEmpty(mpsendId))
                str.Append(" AND a.MRSHeaderId IN (SELECT MRSHeader.MRSHeaderId FROM MRSHeader WHERE MPSheaderId between '" + mpsstartId + "' and '" + mpsendId + "' )");

            if (sourceType != -1) //非全部
            {
                str.Append(" AND a.MRSHeaderId IN (SELECT MRSHeader.MRSHeaderId FROM MRSHeader WHERE SourceType = '" + sourceType.ToString() + "')");
            }
            if (!string.IsNullOrEmpty(id1) && !string.IsNullOrEmpty(id2))
            {
                str.Append(" AND a.MRSHeaderId BETWEEN '" + id1 + "' AND '" + id2 + "'");
            }
            if (!string.IsNullOrEmpty(cusxoid))
            {
                str.Append(" AND a.MRSHeaderId in (select MRSHeaderId from MRSHeader where MPSheaderId in (select MPSheaderId from MPSheader where InvoiceXOId in(select InvoiceId from InvoiceXO where CustomerInvoiceXOId like '%" + cusxoid + "%' )))");
            }
            if (product != null)
            {
                str.Append(" AND  productid='" + product.ProductId + "'");
            }
            if (productCate != null)
            {
                str.Append(" AND  productid in  (select productid from product where ProductCategoryId = '" + productCate.ProductCategoryId + "')");
            }


            str.Append(" ORDER BY ");
            switch (OrderColumn)
            {
                case 0:
                    str.Append(" a.MRSHeaderId,a.Inumber ");         //头编号
                    break;
                case 1:
                    str.Append(" (SELECT ProductName FROM Product WHERE Product.ProductId=a.ProductId)");     //商品名称
                    break;
                case 2:
                    str.Append(" (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE InvoiceId =(SELECT InvoiceXOId FROM MPSheader WHERE MPSheader.MPSheaderId = MRSHeader.MPSheaderId))");
                    break;
                case 3:
                    str.Append(" (SELECT Supplier.Id FROM Supplier WHERE Supplier.SupplierId = a.SupplierId)"); //供应商
                    break;
                case 4:
                    str.Append(" (SELECT Id FROM ProductCategory WHERE ProductCategory.ProductCategoryId = (SELECT Product.ProductCategoryId FROM  Product WHERE Product.ProductId = a.ProductId))");
                    break;
            }
            if (OrderType == 0)
                str.Append(" ASC");
            else
                str.Append(" DESC");
            return this.DataReaderBind<Model.MRSdetails>(str.ToString(), null, CommandType.Text);


            //return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.selectByCondition", str.ToString());
        }

        public double SumSpotStock(string productId)
        {
            return sqlmapper.QueryForObject<double>("MRSdetails.SumSpotStock", productId);
        }
    }
}
#region 注释方法
//public IList<Book.Model.MRSdetails> GetMrsdetailBySourceType(string sourceType)
//{
//    return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.GetMrsdetailBySourceType", sourceType);
//}

//public IList<Book.Model.MRSdetails> GetDate(DateTime startDate, DateTime endDate, string sourceType, string sourceType1, string sourceType2)
//{
//    Hashtable ht = new Hashtable();
//    ht.Add("startdate", startDate);
//    ht.Add("enddate", endDate);
//    ht.Add("SourceType", sourceType);
//    ht.Add("SourceType1", sourceType1);
//    ht.Add("SourceType2", sourceType2);
//    return sqlmapper.QueryForList<Model.MRSdetails>("MRSdetails.select_GetDateSourceType", ht);
//}
#endregion