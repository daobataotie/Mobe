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
        /// <param name="start">��ѯ�����еĿ�ʼʱ��</param>
        /// <param name="end">��ѯ�����еĽ���ʱ��</param>
        /// <param name="company">��λ</param>
        /// <param name="employee">������</param>
        /// <param name="depot">�����ⷿ</param>
        /// <param name="invoiceStatus">״̬</param>
        //DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Book.Model.Depot depot, Helper.InvoiceStatus invoiceStatus, string queryId);

        //DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Helper.InvoiceStatus invoiceStatus, string queryId);

        DataTable SelectDataTable(string queryId);
        System.Data.DataTable SelectProductStock();
        System.Data.DataTable SelectByCondition(string queryId, string depotId, string depotPositionId, string productCategoryId);

        DataTable SelectDataTable(DateTime endDate);
        /// <summary>
        /// ��ѯӦ������
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        DataTable Q15(int day);

        /// <summary>
        /// ���������
        /// </summary>
        IList<Model.Stock> Select(string startid, string endid);

        DataTable Select(DateTime start, DateTime end, string startId, string endId);
        DataTable Select1(DateTime start, DateTime end, string startId, string endId);

        DataTable SelectDataTable(DateTime startDate, DateTime endDate, string startId, string endId);

        //DataTable SelectDataTable(DateTime startDate, DateTime endDate, Model.Company company);
    }
}
