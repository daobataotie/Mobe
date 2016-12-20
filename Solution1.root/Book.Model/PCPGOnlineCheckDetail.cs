//------------------------------------------------------------------------------
//
// file name：PCPGOnlineCheckDetail.cs
// author: mayanjun
// create date：2011-12-6 14:34:43
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 品管线上检查详细
    /// </summary>
    [Serializable]
    public partial class PCPGOnlineCheckDetail
    {
        //private string _ProductName;

        //public string ProductName
        //{
        //    get { return this._product == null ? "" : this._product.ProductName; }
        //}

        //public readonly static string PRO_ProductName = "ProductName";
        //private string _ProductDesc;

        //public string ProductDesc
        //{
        //    get { return this._product == null ? "" : this._product.ProductDescription; }
        //}

        //public readonly static string PRO_ProductDesc = "ProductDesc";

        //private string _ProductXingHao;

        //public string ProductXingHao
        //{
        //    get { return this._product == null ? "" : this._product.ProductSpecification; }
        //}

        //public readonly static string PRO_ProductXingHao = "ProductXingHao";

        /// <summary>
        /// 0 未更改,1修改,2新增
        /// </summary>
        public int Status { get; set; }
    }
}
