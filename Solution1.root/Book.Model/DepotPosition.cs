//------------------------------------------------------------------------------
//
// file name：DepotPosition.cs
// author: peidun
// create date：2009-07-24 12:15:38
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 库存位置
    /// </summary>
    [Serializable]
    public partial class DepotPosition : IComparable
    {
        public override string ToString()
        {
            string str = string.Empty;
            if (this.number >= 0)
                str = this._id + "（" + this.Number + "）";
            else
                str = this._id;
            return str;
        }

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            if (obj is DepotPosition)
            {
                DepotPosition position = (DepotPosition)obj;
                return position.Id.CompareTo(this.Id);
            }

            throw new ArgumentException("obj");
        }

        private double? number = null;

        public double? Number
        {
            get { return number; }
            set { number = value; }
        }
        private double? _stockQuantity1;
        public double? StockQuantity1
        {
            get { return _stockQuantity1; }
            set { _stockQuantity1 = value; }
        }

        #endregion
    }
}
