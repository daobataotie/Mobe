//------------------------------------------------------------------------------
//
// file name：IProduceMaterialdetailsAccessor.cs
// author: peidun
// create date：2009-12-30 16:33:30
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceMaterialdetails
    /// </summary>
    public partial interface IProduceMaterialdetailsAccessor : IAccessor
    {
        IList<Book.Model.ProduceMaterialdetails> Select(Model.ProduceMaterial produceMaterial);
        IList<Book.Model.ProduceMaterialdetails> Select(string houseid, DateTime startDate, DateTime endDate);
        IList<Model.ProduceMaterialdetails> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, string pId0, string pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1);
        IList<Book.Model.ProduceMaterialdetails> SelectByState(Model.ProduceMaterial produceMaterial);
        Model.ProduceMaterialdetails SelectByProductIdAndHeadId(string productId, string produceMaterialId);
        IList<Model.ProduceMaterialdetails> SelectByProductIdAndHeadId(Model.Product pId0, Model.Product pId1, string produceMaterialId);

        double GetMaterialprocesedsumForPDMId(string PDMid);

        IList<Model.ProduceMaterialdetails> SelectTotalByProduceMaterialID(Model.Product pId0, Model.Product pId1, string str);
        IList<Model.ProduceMaterialdetails> SelectForDistributioned(string productid, DateTime InsertTime);
        IList<Model.ProduceMaterialdetails> SelectBycondition2(DateTime startDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, Book.Model.Product pId0, Book.Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1, string CusInvoiceXOId, string handBookId);

        double SelectMaterialQty(string productid, DateTime dateEnd, string workHouseId, string invoiceXOIds);

        DataTable SelectMaterialQty(string productid, DateTime dateStart, DateTime dateEnd, string workHouseId, string invoiceXOIds);

        double SelectMaterialQtyAll(string productid, DateTime dateEnd, string workHouseId);

        IList<Model.ProduceMaterialdetails> SelectMaterialsByProductIds(string productids, DateTime dateStart, DateTime dateEnd, string workHouseId, string invoiceXOIds);
    }
}

