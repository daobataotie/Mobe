//------------------------------------------------------------------------------
//
// file name:InvoiceXODetailAccessor.cs
// author: peidun
// create date:2008/6/20 15:52:13
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
    /// Data accessor of InvoiceXODetail
    /// </summary>
    public partial class InvoiceXODetailAccessor : EntityAccessor, IInvoiceXODetailAccessor
    {
        public IList<Book.Model.InvoiceXODetail> Select(Book.Model.InvoiceXO invoiceXO, bool detailsFlag)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceId", invoiceXO == null ? "" : invoiceXO.InvoiceId);
            StringBuilder str = new StringBuilder();
            if (detailsFlag)
                str.Append(" and DetailsFlag<>2");
            ht.Add("sql", str.ToString());
            return sqlmapper.QueryForList<Model.InvoiceXODetail>("InvoiceXODetail.select_by_invoiceid", ht);
        }

        public void Delete(Book.Model.InvoiceXO invoice)
        {
            sqlmapper.Delete("InvoiceXODetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public float GetByInvoiceXODetailId(string invoiceXODetailId)
        {
            return sqlmapper.QueryForObject<float>("InvoiceXODetail.select_by_InvoiceXODetailId", invoiceXODetailId);
        }

        public Book.Model.InvoiceXODetail GetInvoiceXOAndProductById(string invoiceXODetailId)
        {
            return sqlmapper.QueryForObject<Model.InvoiceXODetail>("InvoiceXODetail.select_by_InvoiceXODetailAndProductId", invoiceXODetailId);
        }

        public Book.Model.InvoiceXODetail GetAllCurrentNum()
        {
            return sqlmapper.QueryForObject<Model.InvoiceXODetail>("InvoiceXODetail.select_by_AllCurrentNum", null);
        }

        public IList<Book.Model.InvoiceXODetail> select_XOnotInMps()
        {
            return sqlmapper.QueryForList<Model.InvoiceXODetail>("InvoiceXODetail.select_XOnotInMps", null);
        }

        public IList<Model.InvoiceXODetail> SelectByDateRangeAndPid(string productid, DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", productid);
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.InvoiceXODetail>("InvoiceXODetail.SelectByDateRangeAndPid", ht);
        }

        public IList<Model.InvoiceXODetail> SelectByHeaderProRang(Model.InvoiceXO invoicexo, Model.Product product1, Model.Product product2, bool isclose)
        {
            if (invoicexo == null)
                return null;
            StringBuilder str = new StringBuilder();
            str.Append(" where invoiceid='" + invoicexo.InvoiceId + "'");
            if (product1 != null && product2 != null)
                str.Append(" and productid in (select productid from product where productname between '" + product1.ProductName + "'  and '" + product2.ProductName + "')");
            if (isclose)
                str.Append(" and detailsflag !=2  ");
            return sqlmapper.QueryForList<Model.InvoiceXODetail>("InvoiceXODetail.SelectBywhere", str.ToString());
        }

        public void UpdateProofUnitPrice(Book.Model.InvoiceXODetail e)
        {
            //修改详细
            Hashtable htDetail = new Hashtable();
            htDetail.Add("InvoiceXODetailId", e.InvoiceXODetailId);
            htDetail.Add("InvoiceXODetailPrice", e.InvoiceXODetailPrice);
            htDetail.Add("InvoiceXODetailMoney", e.InvoiceXODetailMoney);
            sqlmapper.Update("InvoiceXODetail.UpdateProofUnitPrice", htDetail);
        }

        public IList<Book.Model.InvoiceXODetail> Select(Model.Customer customer1, Model.Customer customer2, DateTime startDate, DateTime endDate, DateTime yjrq1, DateTime yjrq2, Model.Employee employee1, Model.Employee employee2, string xoid1, string xoid2, string cusxoidkey, Model.Product product, Model.Product product2, bool isclose, bool mpsIsClose, int orderColumn, int orderType, bool detailFlag)
        {
            StringBuilder str = new StringBuilder("SELECT i.InvoiceId,i.InvoiceDate,i.InvoiceYjrq,c1.CustomerShortName as CustomerName,c2.CustomerShortName as XOCustomerName,i.CustomerInvoiceXOId,p.ProductName,p.CustomerProductName,d.InvoiceXODetailPrice,d.InvoiceXODetailMoney,d.InvoiceXODetailQuantity,d.InvoiceXODetailBeenQuantity,isnull(d.InvoiceXTDetailQuantity,0) as InvoiceXTDetailQuantity FROM InvoiceXODetail d  LEFT JOIN InvoiceXO i ON i.InvoiceId = d.InvoiceId left join Customer c1 on c1.customerid=i.CustomerId left join Customer c2 on c2.CustomerId=i.xocustomerid left join Product p on p.ProductId=d.ProductId where 1=1");

            str.Append(" and i.InvoiceYjrq BETWEEN '" + yjrq1.ToString("yyyy-MM-dd") + "' AND '" + yjrq2.ToString("yyyy-MM-dd") + "'");
            str.Append(" and i.InvoiceDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            if (customer1 != null && customer2 != null)
                str.Append(" and i.xocustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + customer1.Id + "' AND '" + customer2.Id + "')");
            if (employee1 != null && employee2 != null)
                str.Append(" and i.Employee0Id IN (SELECT Employee.EmployeeId FROM Employee WHERE IDNo BETWEEN '" + employee1.IDNo + "' AND '" + employee2.IDNo + "')");
            if (!string.IsNullOrEmpty(xoid1) && !string.IsNullOrEmpty(xoid2))
                str.Append(" and InvoiceXODetail.InvoiceId BETWEEN '" + xoid1 + "' AND '" + xoid2 + "'");
            if (!string.IsNullOrEmpty(cusxoidkey))
                str.Append(" and i.CustomerInvoiceXOId like '%" + cusxoidkey + "%'");
            if (product != null && product2 != null)
                str.Append(" and ProductId IN ( SELECT Product.ProductId FROM Product WHERE ProductName BETWEEN '" + product.ProductName + "' AND '" + product2.ProductName + "')");
            if (isclose)    //true 时只查询未结案
                str.Append(" and i.IsClose=0");
            if (mpsIsClose)  //true 只查询未排完单
                str.Append(" and d.InvoiceMPSState<>2");
            if (detailFlag)
                str.Append(" and d.DetailsFlag=2");
            str.Append(" ORDER BY");

            switch (orderColumn)
            {
                case 0:
                    str.Append(" d.InvoiceId");
                    break;
                case 1:
                    str.Append(" (SELECT Product.CustomerProductName FROM Product WHERE Product.ProductId = InvoiceXODetail.ProductId)");
                    break;
                case 2:
                    str.Append(" (SELECT Customer.Id FROM Customer WHERE Customer.CustomerId = i.CustomerId)");
                    break;
            }

            if (orderType == 0)
                str.Append(" ASC");
            else
                str.Append(" DESC");


            //return sqlmapper.QueryForList<Model.InvoiceXODetail>("InvoiceXODetail.select_byYJRQCustomEmp", str.ToString());
            return this.DataReaderBind<Model.InvoiceXODetail>(str.ToString(), null, CommandType.Text);
        }


        /// <summary>
        /// 通过手册号和项号计算对应订单总量
        /// </summary>
        /// <returns></returns>
        public double SumOrderQuantityByHandbook(string handbookId, string handbookProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("HandbookId", handbookId);
            ht.Add("HandbookProductId", handbookProductId);
            return sqlmapper.QueryForObject<double>("InvoiceXODetail.SumOrderQuantityByHandbook", ht);
        }
    }
}
