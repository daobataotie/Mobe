//------------------------------------------------------------------------------
//
// file name：ConveyanceMethod.cs
// author: mayanjun
// create date：2010-8-9 15:00:28
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 货运方式
	/// </summary>
	[Serializable]
	public partial class ConveyanceMethod
	{
        public override string ToString()
        {
            return _conveyanceMethodName;
        }
	}
}
