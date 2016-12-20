//------------------------------------------------------------------------------
//
// file name：PackageDetails.cs
// author: peidun
// create date：2009-08-12 9:51:49
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 包装类型材料
	/// </summary>
	[Serializable]
	public partial class PackageDetails
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is PackageDetails)
            {
                if ((obj as PackageDetails)._packageTypeId == _packageTypeId)
                    return true;
            }
            return false;
        }
	}
}
