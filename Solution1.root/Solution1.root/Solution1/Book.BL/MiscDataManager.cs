using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Book.BL
{
    public class MiscDataManager
    {
        private static readonly DA.IMiscDataAccessor accessor = (DA.IMiscDataAccessor)Accessors.Get("MiscDataAccessor");

        //public DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Helper.InvoiceStatus status,string queryId) 
        //{
        //    return accessor.SelectDataTable(start, end, company, employee, status,queryId);
        //}
        //public DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Book.Model.Depot depot, Helper.InvoiceStatus invoiceStatus, string queryId)
        //{
        //    return accessor.SelectDataTable(start, end, company, employee, depot, invoiceStatus, queryId);
        //}
        public DataTable SelectDataTable(string queryId)
        {
            return accessor.SelectDataTable(queryId);
        }

        public System.Data.DataTable SelectByCondition(string queryId, string depotId, string depotPositionId, string productCategoryId)
        {
            return accessor.SelectByCondition(queryId, depotId, depotPositionId, productCategoryId);
        }

        public DataTable SelectDataTable(DateTime endTime)
        {
            return accessor.SelectDataTable(endTime);
        }

        public System.Data.DataTable SelectProductStock()
        {
            return accessor.SelectProductStock();
        }
        /// <summary>
        /// 应情况情况
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public DataTable Q15(int day)
        {
            return accessor.Q15(day);
        }
        /// <summary>
        /// 库存上下限
        /// </summary>
        public System.Collections.Generic.IList<Model.Stock> Select(string startid, string endid) 
        {
            return accessor.Select(startid, endid);
        }

        public DataTable Select(DateTime start, DateTime end, string startId, string endId)
        {
            return accessor.Select(start, end, startId, endId);
        }

        public DataTable Select1(DateTime start, DateTime end, string startId, string endId)
        {
            return accessor.Select1(start, end, startId, endId);
        }

        public DataTable SelectDataTable(DateTime startDate, DateTime endDate, string startId, string endId)
        {
            return accessor.SelectDataTable(startDate, endDate, startId, endId);
        }

        //public DataTable SelectDataTable(DateTime startDate, DateTime endDate, Model.Company company)
        //{
        //    return accessor.SelectDataTable(startDate, endDate, company);
        //}
    }
}
