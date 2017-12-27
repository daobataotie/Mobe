//------------------------------------------------------------------------------
//
// file name：SupplierProductManager.cs
// author: mayanjun
// create date：2012-8-30 17:02:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.SupplierProduct.
    /// </summary>
    public partial class SupplierProductManager : BaseManager
    {
        public void Delete(string supplierProductId)
        {
            accessor.Delete(supplierProductId);
        }

        public void Insert(Model.SupplierProduct supplierProduct)
        {
            Validate(supplierProduct);
            supplierProduct.SupplierProductId = Guid.NewGuid().ToString();
            supplierProduct.InsertTime = DateTime.Now;
            supplierProduct.UpdateTime = DateTime.Now;

            accessor.Insert(supplierProduct);
        }

        public void Update(Model.SupplierProduct supplierProduct)
        {
            Validate(supplierProduct);
            supplierProduct.UpdateTime = DateTime.Now;

            accessor.Update(supplierProduct);
        }

        private void Validate(Model.SupplierProduct model)
        {
            if (string.IsNullOrEmpty(model.ProductId))
                throw new Helper.RequireValueException(Model.SupplierProduct.PRO_ProductId);
        }

        public string GetPriceRangeForSupAndProduct(string SupplierId, string ProductId)
        {
            return accessor.GetPriceRangeForSupAndProduct(SupplierId, ProductId);
        }

        //拉取所有厂商该商品记录包括 加工&外购
        public System.Data.DataTable SelectALLRefProduct(string ProductId)
        {
            return accessor.SelectALLRefProduct(ProductId);
        }

        //计算价格
        //public static decimal CountPrice(string priceSection, double num)
        //{
        //    if (!string.IsNullOrEmpty(priceSection))
        //    {
        //        string[] str;
        //        if (priceSection.Contains(","))
        //            str = priceSection.Split(',');
        //        else
        //            str = new string[] { priceSection };
        //        foreach (var item in str)
        //        {
        //            string[] price = item.Split('/');
        //            if ((num > 0 && double.Parse(price[1]) >= num) || (num < 0 && double.Parse(price[1]) >= -num))
        //                return decimal.Parse(price[2]);
        //        }
        //    }
        //    return 0;
        //}

        //(返回double) 解析价格区间
        //DEMO: 0/0/0,0/0/0,0/0/0,0/0/0,0/0/0,0/0/0,0/999999999999/0
        //Means: {起始数量/终止数量/价格}
        public static decimal CountPrice(string priceR, double Quantity)
        {
            decimal result = 0;
            if (string.IsNullOrEmpty(priceR))
                return 0;
            string[] inPriceR;
            if (priceR.Contains(","))
                inPriceR = priceR.Split(',');
            else
                inPriceR = new string[] { priceR };

            decimal startRange, endRange, RangePrice;
            foreach (string s in inPriceR)
            {
                if (!s.Contains("/"))
                    continue;

                string[] prs = s.Split('/');

                startRange = Convert.ToDecimal(prs[0]);
                endRange = Convert.ToDecimal(prs[1]);
                RangePrice = Convert.ToDecimal(prs[2]);

                if (Convert.ToDecimal(Quantity) >= startRange && Convert.ToDecimal(Quantity) <= endRange)
                {
                    result = RangePrice;
                    break;
                }
            }
            return result;
        }

        #region 分类构建
        public Model.SupplierProduct mGetFirst(string SupplierId)
        {
            return accessor.mGetFirst(SupplierId);
        }

        public Model.SupplierProduct mGetLast(string SupplierId)
        {
            return accessor.mGetLast(SupplierId);
        }

        public Model.SupplierProduct mGetPrev(DateTime InsertDate, string SupplierId)
        {
            return accessor.mGetPrev(InsertDate, SupplierId);
        }

        public Model.SupplierProduct mGetNext(DateTime InsertDate, string SupplierId)
        {
            return accessor.mGetNext(InsertDate, SupplierId);
        }

        public bool mHasRows(string SupplierId)
        {
            return accessor.mHasRows(SupplierId);
        }

        public bool mHasRowsBefore(DateTime InsertDate, string SupplierId)
        {
            return accessor.mHasRowsBefore(InsertDate, SupplierId);
        }

        public bool mHasRowsAfter(DateTime InsertDate, string SupplierId)
        {
            return accessor.mHasRowsAfter(InsertDate, SupplierId);
        }

        public IList<Model.SupplierProduct> mSelect(string SupplierId)
        {
            return accessor.mSelect(SupplierId);
        }
        #endregion

        public IList<Model.SupplierProduct> SelectAll()
        {
            return accessor.SelectAll();
        }
    }
}

