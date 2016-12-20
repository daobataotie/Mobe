//------------------------------------------------------------------------------
//
// file name：AtCurrencyCategory.cs
// author: mayanjun
// create date：2011-6-8 10:03:42
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 币种类别
	/// </summary>
	[Serializable]
	public partial class AtCurrencyCategory
	{
        public override string ToString()
        {
            return this._atCurrencyName;
        }
	}
}
