//------------------------------------------------------------------------------
//
// file name：BomParentPartInfo.cs
// author: peidun
// create date：2009-08-25 17:49:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// Bom母件信息
	/// </summary>
	[Serializable]
	public partial class BomParentPartInfo
	{
        public override string ToString()
        {
            return _id;
        }
      //  private Model.Customer _customer;
        private System.Collections.Generic.IList<Model.BomComponentInfo> components = new System.Collections.Generic.List<Model.BomComponentInfo>();

        private System.Collections.Generic.IList<Model.BomPackageDetails> bomPackageDetails = new System.Collections.Generic.List<Model.BomPackageDetails>();

        private System.Collections.Generic.IList<Model.BOMProductProcess> _BOMProductProcess = new System.Collections.Generic.List<Model.BOMProductProcess>();
        private System.Collections.Generic.IList<Model.ProductProcess> _productProcessDetail = new System.Collections.Generic.List<Model.ProductProcess>();

       // private System.Collections.Generic.IList<Model.CustomerProcessingDetail> _customerProcessingDetail = new System.Collections.Generic.List<Model.CustomerProcessingDetail>();
        private string _mPSheaderId;

        public virtual System.Collections.Generic.IList<Model.BomComponentInfo> Components
        {
            get { return components; }
            set { components = value; }
        }
        public virtual System.Collections.Generic.IList<Model.BomPackageDetails> BomPackageDetails
        {
            get { return bomPackageDetails; }
            set { bomPackageDetails = value; }
        }

        public virtual System.Collections.Generic.IList<Model.BOMProductProcess> BOMProductProcess
        {
            get { return _BOMProductProcess; }
            set { _BOMProductProcess = value; }
        }
        public virtual System.Collections.Generic.IList<Model.ProductProcess> ProductProcessDetail
        {
            get { return _productProcessDetail; }
            set { _productProcessDetail = value; }
        }
        //public virtual System.Collections.Generic.IList<Model.CustomerProcessingDetail> CustomerProcessingDetail
        //{
        //    get { return _customerProcessingDetail; }
        //    set { _customerProcessingDetail = value; }
        //}
        public string MPSheaderId
        {
            get { return _mPSheaderId; }
            set { _mPSheaderId = value; }
        }


        //public Model.Customer Customer
        //{
        //    get { return _customer; }
        //    set { _customer = value; }
        //}
	}
}
