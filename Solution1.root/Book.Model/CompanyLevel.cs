//------------------------------------------------------------------------------
//
// file name:CompanyLevel.cs
// author: peidun
// create date:2008/6/30 14:20:08
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 客户级别
	/// </summary>
	[Serializable]
	public partial class CompanyLevel : IComparable
	{
        public override string ToString()
        {
            return this._companyLevelName;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is CompanyLevel)
            {
                CompanyLevel companyLevel = (CompanyLevel)obj;
                return companyLevel.CompanyLevelName.CompareTo(this.CompanyLevelName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is CompanyLevel)
            {
                return (obj as Model.CompanyLevel)._companyLevelId == this._companyLevelId ? true : false;
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
