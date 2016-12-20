//------------------------------------------------------------------------------
//
// file name:ProductCategory.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 商品类型
	/// </summary>
	[Serializable]
	public partial class ProductCategory : IComparable  
	{
        public override string ToString()
        {
            return string.Format("{0}-{1}", this._id, this._productCategoryName);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is ProductCategory)
            {
                ProductCategory productCategory = (ProductCategory)obj;
                return productCategory.ProductCategoryName.CompareTo(this.ProductCategoryName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is ProductCategory)
            {
                return (obj as Model.ProductCategory)._productCategoryId == this._productCategoryId ? true : false;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
