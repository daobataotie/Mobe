//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsCheck.cs
// author: mayanjun
// create date：2011-07-22 10:44:54
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 生产车间质检统计
	/// </summary>
	[Serializable]
	public partial class ProduceStatisticsCheck
	{
        private System.Collections.Generic.IList<ProduceStatisticsCheckDetail> details = new System.Collections.Generic.List<Model.ProduceStatisticsCheckDetail>();

        public System.Collections.Generic.IList<ProduceStatisticsCheckDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
        private string customerInvoiceXOId;

        public string CustomerInvoiceXOId
        {
            get { return customerInvoiceXOId; }
            set { customerInvoiceXOId = value; }
        }
        public readonly static string PRO_CustomerInvoiceXOId = "CustomerInvoiceXOId";
	}
}
