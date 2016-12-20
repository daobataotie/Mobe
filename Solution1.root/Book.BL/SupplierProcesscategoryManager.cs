//------------------------------------------------------------------------------
//
// file name：SupplierProcesscategoryManager.cs
// author: mayanjun
// create date：2012-8-30 17:02:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class SupplierProcesscategoryManager : BaseManager
    {
        public void Delete(string supplierProcesscategoryId)
        {
            accessor.Delete(supplierProcesscategoryId);
        }

        public void Insert(Model.SupplierProcesscategory supplierProcesscategory)
        {
            Validate(supplierProcesscategory);

            supplierProcesscategory.SupplierProcesscategoryId = Guid.NewGuid().ToString();
            supplierProcesscategory.InsertTime = DateTime.Now;
            supplierProcesscategory.UpdateTime = DateTime.Now;
            accessor.Insert(supplierProcesscategory);
        }

        public void Update(Model.SupplierProcesscategory supplierProcesscategory)
        {
            Validate(supplierProcesscategory);

            supplierProcesscategory.UpdateTime = DateTime.Now;
            accessor.Update(supplierProcesscategory);
        }

        //数据验证
        private void Validate(Model.SupplierProcesscategory model)
        {
            if (string.IsNullOrEmpty(model.ProductId))
                throw new Helper.RequireValueException(Model.SupplierProcesscategory.PRO_ProductId);
        }

        //拉取厂商商品表
        public System.Data.DataTable SelectSupplierProduct(string SupplierId)
        {
            return accessor.SelectSupplierProduct(SupplierId);
        }

        public IList<Model.SupplierProcesscategory> SelectBySupAndProc(string SupplierId)
        {
            return accessor.SelectBySupAndProc(SupplierId);
        }

        #region 分类构建
        public Model.SupplierProcesscategory mGetFirst(string SupplierId)
        {
            return accessor.mGetFirst(SupplierId);
        }

        public Model.SupplierProcesscategory mGetLast(string SupplierId)
        {
            return accessor.mGetLast(SupplierId);
        }

        public Model.SupplierProcesscategory mGetPrev(DateTime InsertDate, string SupplierId)
        {
            return accessor.mGetPrev(InsertDate, SupplierId);
        }

        public Model.SupplierProcesscategory mGetNext(DateTime InsertDate, string SupplierId)
        {
            return accessor.mGetNext(InsertDate, SupplierId);
        }

        public bool mHasRows(string SupplierId)
        {
            return accessor.mHasRows(SupplierId);
        }

        public bool mHasRowsBefore(DateTime InsertTime, string SupplierId)
        {
            return accessor.mHasRowsBefore(InsertTime, SupplierId);
        }

        public bool mHasRowsAfter(DateTime InsertTime, string SupplierId)
        {
            return accessor.mHasRowsAfter(InsertTime, SupplierId);
        }

        public IList<Model.SupplierProcesscategory> mSelect(string SupplierId)
        {
            return accessor.mSelect(SupplierId);
        }
        #endregion
    }
}

