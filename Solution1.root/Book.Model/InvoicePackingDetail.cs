//------------------------------------------------------------------------------
//
// file name：InvoicePackingDetail.cs
// author: mayanjun
// create date：2013-1-14 10:58:48
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 装箱详细
    /// </summary>
    [Serializable]
    public partial class InvoicePackingDetail
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 选择
        /// </summary>
        public bool Checked { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is InvoicePackingDetail)
            {
                if ((obj as InvoicePackingDetail)._invoicePackingDetailId == _invoicePackingDetailId)
                    return true;
            }
            return false;
        }

        //private double? _NOPackingNum;

        ///// <summary>
        ///// 未装箱数量
        ///// </summary>
        //public double? NOPackingNum
        //{
        //    get { return this.InvoiceXOQuantity - this.PackingNum; }
        //    set { _NOPackingNum = value; }
        //}

        //报表使用
        public string UnitNumReport
        {
            get
            {
                if (this.UnitNum == null || this.UnitNum == 0)
                    return null;
                return "@" + this.UnitNum;
            }
        }

        public string UnitJWeightReport { 
            get 
            {
                if (this.UnitJWeight == null || this.UnitJWeight == 0)
                    return null;
                return "@" + this.UnitJWeight;
            }
        }

        public string UnitMWeightReport { 
            get
            {
                if (this.UnitMWeight == null || this.UnitMWeight == 0)
                    return null;
                return "@" + this.UnitMWeight;
            } 
        }

        public readonly static string PRO_UnitNumReport = "UnitNumReport";

        public readonly static string PRO_UnitJWeightReport = "UnitJWeightReport";

        public readonly static string PRO_UnitMWeightReport = "UnitMWeightReport";
    }
}
