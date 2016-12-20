//------------------------------------------------------------------------------
//
// file name：ProductUnit.cs
// author: peidun
// create date：2009-4-25 13:55:06
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class ProductUnit
	{
        public ProductUnit() { this._productUnitId = Guid.NewGuid().ToString(); }

        public override string ToString()
        {
            return _cnName;
        } 

	}
}
