//------------------------------------------------------------------------------
//
// file name：Operators.cs
// author: peidun
// create date：2009-09-09 下午 04:00:24
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 操作员
	/// </summary>
	[Serializable]
	public partial class Operators
	{
        public override string ToString()
        {
            return _operatorName;
        }
	}
}
