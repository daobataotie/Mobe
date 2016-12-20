//------------------------------------------------------------------------------
//
// file name:PayMethod.cs
// author: peidun
// create date:2008/6/30 14:20:08
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 支付方式
	/// </summary>
	[Serializable]
	public partial class PayMethod : IComparable
	{
        public override string ToString()
        {
            return this._payMethodName;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is PayMethod)
            {
                PayMethod paymethod = (PayMethod)obj;
                return paymethod.PayMethodName.CompareTo(this.PayMethodName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is PayMethod)
            {
                return (obj as Model.PayMethod)._payMethodId == this._payMethodId ? true : false;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
