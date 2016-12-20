//------------------------------------------------------------------------------
//
// file name：MRSdetailsManager.cs
// author: peidun
// create date：2009-12-18 11:12:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MRSdetails.
    /// </summary>
    public partial class MRSdetailsManager
    {
        private readonly DA.IProductAccessor ProductAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        byte[] pic = new byte[] { };
        /// <summary>
        /// Delete MRSdetails by primary key.
        /// </summary>
        public void Delete(string mRSdetailsID)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(mRSdetailsID);
        }

        /// <summary>
        /// Insert a MRSdetails.
        /// </summary>
        public void Insert(Model.MRSdetails mRSdetails)
        {
            //
            // todo:add other logic here
            //
            // mRSdetails.MRSdetailsId = Guid.NewGuid().ToString();
            //Model.Product product = ProductAccessor.Get(mRSdetails.ProductId);
            //if (product.MRSStockQuantity == null)
            //    product.MRSStockQuantity = 0;
            //if (mRSdetails.MRSdetailssum >= (mRSdetails.Product.StocksQuantity -  product.MRSStockQuantity))
            //    product.MRSStockQuantity += mRSdetails.Product.StocksQuantity -  product.MRSStockQuantity;
            //else {
            //    product.MRSStockQuantity += mRSdetails.MRSdetailssum;
            //}
            //    if (product.ProductImage == null)
            //        product.ProductImage = pic;
            //    if (product.ProductImage1 == null)
            //        product.ProductImage1 = pic;
            //    if (product.ProductImage2 == null)
            //        product.ProductImage2 = pic;
            //    if (product.ProductImage3 == null)
            //        product.ProductImage3 = pic;

            //    ProductAccessor.Update(product);

            accessor.Insert(mRSdetails);
        }

        /// <summary>
        /// Update a MRSdetails.
        /// </summary>
        public void Update(Model.MRSdetails mRSdetails)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(mRSdetails);
        }

        public IList<Book.Model.MRSdetails> Select(Model.MRSHeader mRSheader)
        {
            return accessor.Select(mRSheader);
        }

        public IList<Book.Model.MRSdetails> SelectBySqlMap(Model.MRSHeader mRSheader)
        {
            return accessor.Select(mRSheader);
        }

        //public IList<Book.Model.MRSdetails> GetMrsdetailBySourceType(string sourceType)
        //{
        //    return accessor.GetMrsdetailBySourceType(sourceType);
        //}
        public IList<Book.Model.MRSdetails> Select(string mpsHeaderId, string sourceType, string sourceType1, string sourceType2)
        {
            return accessor.Select(mpsHeaderId, sourceType, sourceType1, sourceType2);
        }
        //public IList<Book.Model.MRSdetails> GetDate(DateTime startDate, DateTime endDate, string sourceType,string sourceType1,string sourceType2)
        //{
        //    return accessor.GetDate(startDate,endDate,sourceType,sourceType1, sourceType2);
        //}
        public IList<Book.Model.MRSdetails> GetByMRSIDAndProId(string mrsid, string proid)
        {
            return accessor.GetByMRSIDAndProId(mrsid, proid);
        }
        public IList<Book.Model.MRSdetails> Select(DateTime startDate, DateTime endDate, string sourceType, string sourceType1, string sourceType2, string cusxoid, int FlagIsProcess)
        {
            return accessor.Select(startDate, endDate, sourceType, sourceType1, sourceType2, cusxoid, FlagIsProcess);
        }
        public IList<Book.Model.MRSdetails> SelectWhere(string sqlwhere)
        {
            return accessor.SelectWhere(sqlwhere);
        }

        public void DeleteByHeader(Model.MRSHeader header)
        {
            accessor.DeleteByHeader(header);
        }

        public IList<Model.MRSdetails> SelectbyCondition(string mpsstartId, string mpsendId, string customerstartId, string customerendId, DateTime startdate, DateTime enddate, int? sourceType, string id1, string id2, string cusxoid, Book.Model.Product product, int OrderColumn, int OrderType, Model.ProductCategory productCate)
        {
            return accessor.SelectbyCondition(mpsstartId, mpsendId, customerstartId, customerendId, startdate, enddate, sourceType, id1, id2, cusxoid, product, OrderColumn, OrderType, productCate);
        }

        public double SumSpotStock(string productId)
        {
            return accessor.SumSpotStock(productId);
        }
    }
}

