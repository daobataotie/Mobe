//------------------------------------------------------------------------------
//
// file name：Duty.cs
// author: peidun
// create date：2008-11-24 11:10:31
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
    public partial class Duty : IComparable
	{
        public override string ToString()
        {
            return this._dutyName;
        }
        public int CompareTo(object obj)
        {
            if (obj is Duty)
            {
                Duty duty = (Duty)obj;
                return duty._dutyName.CompareTo(this._dutyName);
            }

            throw new ArgumentException("obj");
        }
        public override bool Equals(object obj)
        {
            if (obj is Duty)
            {
                return (obj as Model.Duty)._dutyId == this._dutyId ? true : false;
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
