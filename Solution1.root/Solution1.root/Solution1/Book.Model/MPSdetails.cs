//------------------------------------------------------------------------------
//
// file name：MPSdetails.cs
// author: peidun
// create date：2009-12-18 11:23:48
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 主生产计划明细
	/// </summary>
	[Serializable]
	public partial class MPSdetails
	{
        private string _productSpecification;
        private string  _customerInvoiceXOId;
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
        public string  CustomerInvoiceXOId
		{
			get 
			{
                return this._customerInvoiceXOId;
			}
			set 
			{
                this._customerInvoiceXOId = value;
			}
		}
      
	}
}
