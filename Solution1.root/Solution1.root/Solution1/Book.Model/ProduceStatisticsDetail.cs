//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsDetail.cs
// author: mayanjun
// create date：2011-4-8 09:17:42
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 生产车间数量详细
	/// </summary>
	[Serializable]
	public partial class ProduceStatisticsDetail
	{
        private double? noProduceQuantity;

        public double? NoProduceQuantity
        {
            get 
            {
                return (this._produceQuantity - this._heGeQuantity) < 0 ? 0 : this._produceQuantity - this._heGeQuantity;
            }
            set { noProduceQuantity = value; }
        }

        //未生產數量
        public readonly static string PRO_NoProduceQuantity = "NoProduceQuantity";
        private double? _noProduceQuantity;

        public double? NoProduceQuantity1
        {
            get
            {
                return _noProduceQuantity;
            }
            set { _noProduceQuantity = value; }
        }

        //未生產數量
        public readonly static string PRO_NoProduceQuantity1 = "NoProduceQuantity1";
	}
}
