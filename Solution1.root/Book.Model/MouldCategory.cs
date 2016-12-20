//------------------------------------------------------------------------------
//
// file name：MouldCategory.cs
// author: peidun
// create date：2009-07-24 12:15:38
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 模具类型
	/// </summary>
	[Serializable]
	public partial class MouldCategory
	{
        public override string ToString()
        {
            return this.MouldCategoryName;
        }
	}
}
