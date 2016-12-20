//------------------------------------------------------------------------------
//
// file name：ProductMaterial.cs
// author: mayanjun
// create date：2010-9-23 15:27:44
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 模具材质
	/// </summary>
	[Serializable]
	public partial class ProductMaterial
	{
        public override string ToString()
        {
            return this.ProductMaterialName;
        }
	}
}
