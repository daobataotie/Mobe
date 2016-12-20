//------------------------------------------------------------------------------
//
// file name：IMRSdetailsAccessor.cs
// author: peidun
// create date：2009-12-18 11:12:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.MRSdetails
    /// </summary>
    public partial interface IMRSdetailsAccessor : IAccessor
    {
        IList<Book.Model.MRSdetails> Select(Model.MRSHeader mRSheader);
        //IList<Book.Model.MRSdetails> SelectBySqlMap(Model.MRSHeader mRSheader);
        // IList<Book.Model.MRSdetails> GetMrsdetailBySourceType(string sourceType);

        IList<Book.Model.MRSdetails> Select(string mpsHeaderId, string sourceType, string sourceType1, string sourceType2);
        // IList<Book.Model.MRSdetails> GetDate(DateTime startDate, DateTime endDate, string sourceType, string sourceType1, string sourceType2);

        IList<Book.Model.MRSdetails> GetByMRSIDAndProId(string mrsid, string proid);

        IList<Book.Model.MRSdetails> Select(DateTime startDate, DateTime endDate, string sourceType, string sourceType1, string sourceType2, string cusxoid, int FlagIsProcess);

        IList<Book.Model.MRSdetails> SelectWhere(string sqlwhere);

        IList<Model.MRSdetails> SelectbyCondition(string mpsstartId, string mpsendId, string customerstartId, string customerendId, DateTime startdate, DateTime enddate, int? sourceType, string id1, string id2, string cusxoid, Model.Product product, int OrderColumn, int OrderType, Model.ProductCategory productCate);

        void DeleteByHeader(Model.MRSHeader header);
        double SumSpotStock(string productId);
    }
}

