using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Book.DA
{
    public interface IMiscDataAccessor : IAccessor
    {
        System.Collections.IList Q03();
        /// <summary>
        /// Q05
        /// </summary>
        /// <param name="start">查询周期中的开始时间</param>
        /// <param name="end">查询周期中的结束时间</param>
        /// <param name="company">单位</param>
        /// <param name="employee">经手人</param>
        /// <param name="depot">出货库房</param>
        /// <param name="invoiceStatus">状态</param>
        //DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Book.Model.Depot depot, Helper.InvoiceStatus invoiceStatus, string queryId);

        //DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Helper.InvoiceStatus invoiceStatus, string queryId);

        DataTable SelectDataTable(string queryId);
        System.Data.DataTable SelectProductStock();
        System.Data.DataTable SelectByCondition(string queryId, string depotId, string depotPositionId, string productCategoryId);

        DataTable SelectDataTable(DateTime endDate);
        /// <summary>
        /// 查询应请款情况
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        DataTable Q15(int day);

        /// <summary>
        /// 库存上下限
        /// </summary>
        IList<Model.Stock> Select(string startid, string endid);

        DataTable Select(DateTime start, DateTime end, string startId, string endId);
        DataTable Select1(DateTime start, DateTime end, string startId, string endId);

        DataTable SelectDataTable(DateTime startDate, DateTime endDate, string startId, string endId);

        //DataTable SelectDataTable(DateTime startDate, DateTime endDate, Model.Company company);
    }
}
