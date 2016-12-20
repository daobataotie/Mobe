//------------------------------------------------------------------------------
//
// file name:IncomingKind.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 其他收入类型
	/// </summary>
	[Serializable]
	public partial class IncomingKind : IComparable
	{
        public override string ToString()
        {
            return this._incomingKindName;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is IncomingKind)
            {
                IncomingKind incomingKind = (IncomingKind)obj;
                return incomingKind.IncomingKindName.CompareTo(this.IncomingKindName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is IncomingKind)
            {
                return (obj as Model.IncomingKind)._incomingKindId == this._incomingKindId ? true : false;
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
