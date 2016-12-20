//------------------------------------------------------------------------------
//
// file name：AtAccountingCategories.cs
// author: mayanjun
// create date：2010-11-10 11:04:52
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class AtAccountingCategories
	{
        public override string ToString()
        {
            return this._accountingCategoriesName;
        }
	}
}
