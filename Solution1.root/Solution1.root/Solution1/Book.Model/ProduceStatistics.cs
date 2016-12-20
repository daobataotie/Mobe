//------------------------------------------------------------------------------
//
// file name：ProduceStatistics.cs
// author: mayanjun
// create date：2011-4-8 09:17:42
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 生产车间数量统计
	/// </summary>
	[Serializable]
	public partial class ProduceStatistics
	{
        private System.Collections.Generic.IList<ProduceStatisticsDetail> details = new System.Collections.Generic.List<Model.ProduceStatisticsDetail>();

        public System.Collections.Generic.IList<ProduceStatisticsDetail> Details
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
