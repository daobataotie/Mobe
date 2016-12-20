//------------------------------------------------------------------------------
//
// file name：AtProperty.cs
// author: mayanjun
// create date：2010-11-15 10:11:55
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class AtProperty
	{
        public override string ToString()
        {
            return this._propertyName;
        }
	}
}
