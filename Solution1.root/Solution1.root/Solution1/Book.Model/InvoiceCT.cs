//------------------------------------------------------------------------------
//
// file name:InvoiceCT.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 进货退货单
	/// </summary>
	[Serializable]
	public partial class InvoiceCT : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceCTDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceCTDetail> Details
        {
            get { return details; }
            set { details = value; }            
        }
        /// <summary>
        /// 库房编号
        /// </summary>
        private string _depotId;
  		/// <summary>
		/// 库房编号
		/// </summary>
		public string DepotId
		{
			get 
			{
				return this._depotId;
			}
			set 
			{
				this._depotId = value;
			}
		}
        /// <summary>
        /// 库房
        /// </summary>
        private  Depot depot; 
        /// <summary>
        /// 库房
        /// </summary>
        public virtual Depot Depot
        {
            get
            {
                return this.depot;
            }
            set
            {
                this.depot = value;
            }

        }
	}
  
}
