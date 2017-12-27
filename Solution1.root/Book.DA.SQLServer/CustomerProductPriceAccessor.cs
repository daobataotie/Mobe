//------------------------------------------------------------------------------
//
// file name：CustomerProductPriceAccessor.cs
// author: mayanjun
// create date：2013-3-8 16:09:37
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
    /// Data accessor of CustomerProductPrice
    /// </summary>
    public partial class CustomerProductPriceAccessor : EntityAccessor, ICustomerProductPriceAccessor
    {
        public IList<Model.CustomerProductPrice> SelectByCustomerId(string CustomerId)
        {
            string sql = "SELECT cpp.CustomerProductPriceId,cpp.CustomerId,cpp.CustomerProductPriceRage,cpp.CustomerProductsId ,cpp.ProductId,p.Id AS ProductIDNo,p.ProductName,p.ProductDescription AS ProductDesc,cp.Version AS ProductVersion,cp.CustomerProductId FROM CustomerProductPrice  cpp LEFT JOIN CustomerProducts cp ON cp.PrimaryKeyId = cpp.CustomerProductsId LEFT JOIN Product p ON p.ProductId = cpp.ProductId WHERE cpp.CustomerId='" + CustomerId + "'";

            return this.DataReaderBind<Model.CustomerProductPrice>(sql, null, CommandType.Text);
        }

        public IList<Model.CustomerProductPrice> SelectByProductId(string ProductId)
        {
            return sqlmapper.QueryForList<Model.CustomerProductPrice>("CustomerProductPrice.SelectByProductId", ProductId);
        }

        public void UpdateByCustomerProductsId(Model.CustomerProductPrice model)
        {
            sqlmapper.Update("CustomerProductPrice.UpdateByCustomerProductsId", model);
        }

        public string SelectPriceByProductId(string ProductId)
        {
            return sqlmapper.QueryForObject<string>("CustomerProductPrice.SelectPriceByProductId", ProductId);
        }

        public IList<Model.CustomerProductPrice> SelectAll()
        {
            string sql = "select ProductId,CustomerProductPriceRage from CustomerProductPrice";

            return this.DataReaderBind<Model.CustomerProductPrice>(sql, null, CommandType.Text);
        }
    }
}
