//------------------------------------------------------------------------------
//
// file name：IInvoiceCODetailAccessor.cs
// author: peidun
// create date：2008/6/20 15:51:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceCODetail
    /// </summary>
    public partial interface IInvoiceCODetailAccessor : IEntityAccessor
    {
        IList<Model.InvoiceCODetail> Select(Model.InvoiceCO invoice);
        IList<Book.Model.InvoiceCODetail> Select(string invoiceId);
        void Delete(Model.InvoiceCO invoice);
        IList<Model.InvoiceCODetail> SelectByDateRangeAndPid(string pid, DateTime startdate, DateTime enddate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pid">头编号</param>
        /// <param name="productStart">商品 初始</param>
        /// <param name="productEnd">商品 结束</param>
        /// <returns></returns>
        IList<Model.InvoiceCODetail> SelectByHeaderProRang(string pid, Model.Product productStart, Model.Product productEnd);
        /// <summary>
        /// 修改单价
        /// </summary>
        /// <param name="e"></param>
        void UpdateProofUnitPrice(Model.InvoiceCODetail e);

        IList<Model.InvoiceCODetail> Select(string costartid, string coendid, Model.Supplier SupplierStart, Model.Supplier SupplierEnd, DateTime? dateStart, DateTime? dateEnd, Model.Product productStart, Model.Product productEnd, string cusxoid, DateTime dateJHStart, DateTime dateJHEnd, int? invoiceFlag, Model.Employee empStart, Model.Employee empEnd);
    }
}

