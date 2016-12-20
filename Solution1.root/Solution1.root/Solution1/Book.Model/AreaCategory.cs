//------------------------------------------------------------------------------
//
// file name：AreaCategory.cs
// author: peidun
// create date：2009-08-03 9:37:30
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 地区分类
	/// </summary>
	[Serializable]
	public partial class AreaCategory
	{
        public override string ToString()
        {
            return this._areaCategoryName;
        }
	}
}
