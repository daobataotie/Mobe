//------------------------------------------------------------------------------
//
// file name：Technologydetails.cs
// author: peidun
// create date：2009-12-8 16:29:53
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 工艺 路线明细
	/// </summary>
	[Serializable]
	public partial class Technologydetails
	{
        private System.Collections.Generic.IList<Model.Procedures> _detail = new System.Collections.Generic.List<Model.Procedures>();
        public System.Collections.Generic.IList<Model.Procedures> detail
        {
            get { return _detail; }
            set { _detail = value; }

        }

        private  bool? _isOtherProduceOther;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsOtherProduceOther
        {
            get
            {
                return this._isOtherProduceOther;
            }
            set
            {
                this._isOtherProduceOther = value;
            }
        }
        private string _supplierId;
        /// <summary>
        /// 
        /// </summary>
        public string SupplierId
        {
            get
            {
                return this._supplierId;
            }
            set
            {
                this._supplierId = value;
            }
        }

        private string _workHouseId;
        /// <summary>
        /// 工作中心编号
        /// </summary>
        public string WorkHouseId
        {
            get
            {
                return this._workHouseId;
            }
            set
            {
                this._workHouseId = value;
            }
        }

	}
}
