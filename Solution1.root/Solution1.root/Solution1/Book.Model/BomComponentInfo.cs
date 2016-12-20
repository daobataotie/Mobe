//------------------------------------------------------------------------------
//
// file name：BomComponentInfo.cs
// author: peidun
// create date：2009-08-25 17:49:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// BOM子件信息
	/// </summary>
	[Serializable]
	public partial class BomComponentInfo
	{
        private int jibie;
        public int Jibie
        {
            get { return jibie; }
            set { jibie = value; }
        }
        private Model.MPSheader _MPSheader;
        public Model.MPSheader MPSheader
        {
            get { return _MPSheader; }
            set { _MPSheader = value; }
        }

        private string  _MPSheaderId;
        public string MPSheaderId
        {
            get { return _MPSheaderId; }
            set { _MPSheaderId = value; }
        }
        private Model.Customer _Customer;
        public Model.Customer Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        private string _InvoiceXOId;
        public string InvoiceXOId
        {
            get { return _InvoiceXOId; }
            set { _InvoiceXOId = value; }
        }
        private double? _mrpQuantity;
        public  double? MrpQuantity
        {
            get { return _mrpQuantity; }
            set { _mrpQuantity = value; }
        }
        private double? _mpsQuantity;
        public double? MpsQuantity
        {
            get { return _mpsQuantity; }
            set { _mpsQuantity = value; }
        }
        private string _mPSdetailsId;
        public string MPSdetailsId
        {
            get { return _mPSdetailsId; }
            set { _mPSdetailsId = value; }
        }
        private string _bomComponentInfoDesc;
        public string BomComponentInfoDesc
        {
            get { return this._bomComponentInfoDesc; }
            set { this._bomComponentInfoDesc = value; }

        }
        public string ProductDesc
        {
            get { return this._product.ProductDescription; }
           

        }
        
	}
}
