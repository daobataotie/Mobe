//------------------------------------------------------------------------------
//
// file name：MRSdetails.cs
// author: peidun
// create date：2009-12-18 11:23:48
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 物料需求计划明细
	/// </summary>
	[Serializable]
	public partial class MRSdetails
	{
        private string _productSpecification;
        /// <summary>
        /// 规格型号
        /// </summary>
        public string ProductSpecification
        {
            get
            {
                return this._productSpecification;
            }
            set
            {
                this._productSpecification = value;
            }
        }

        public bool _isChecked;

        public readonly static string PROPERTY_ISCHECKED = "IsChecked";
        public bool IsChecked
        {
            get
            {
                return this._isChecked;
            }
            set
            {
                this._isChecked = value;
            }
        }

        public string ShowSupplier
        {
            get { return this._product.Supplier == null ? "" : this._product.Supplier.ToString(); }
        }

        public string ShowStockQuerty
        {
            get
            {
                return this._product.StocksQuantity.Value.ToString();
            }
        }
        //public string InvoiceXOId
        //{
        //    get { new  m.Get(this.MPSdetailsId); }
        //}
	}
}
