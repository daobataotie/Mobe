//------------------------------------------------------------------------------
//
// file name：FamilyMembers.cs
// author: peidun
// create date：2009-09-02 上午 10:38:15
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 家庭成员
	/// </summary>
	[Serializable]
	public partial class FamilyMembers : IComparable
	{
        public override string ToString()
        {
            return _familyMembersName;
        }

        public override bool Equals(object obj)
        {
            if (obj is FamilyMembers)
            {
                return (obj as Model.FamilyMembers)._employeeId == this._employeeId ? true : false;
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

        #region IComparable Members

        public int CompareTo(object obj)
        {
           if (obj is FamilyMembers)
            {
                FamilyMembers member = (FamilyMembers)obj;
                return member._familyMembersName.CompareTo(this._familyMembersName);
            }

            throw new ArgumentException("obj");
        }

        #endregion
    }
}
