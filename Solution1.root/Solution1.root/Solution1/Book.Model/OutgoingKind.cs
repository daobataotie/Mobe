//------------------------------------------------------------------------------
//
// file name:OutgoingKind.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 其他支出类型
	/// </summary>
	[Serializable]
	public partial class OutgoingKind : IComparable
	{
        public override string ToString()
        {
            return this._outgoingKindName;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is OutgoingKind)
            {
                OutgoingKind outgoingKind = (OutgoingKind)obj;
                return outgoingKind.OutgoingKindName.CompareTo(this.OutgoingKindName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is OutgoingKind)
            {
                return (obj as Model.OutgoingKind)._outgoingKindId == this._outgoingKindId ? true : false;
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
