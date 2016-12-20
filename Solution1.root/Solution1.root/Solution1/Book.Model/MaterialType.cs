//------------------------------------------------------------------------------
//
// file name：MaterialType.cs
// author: peidun
// create date：2009-12-2 16:19:30
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class MaterialType
	{
        public override string ToString()
        {
            return this._materialTypeName;
        }
	}
}
