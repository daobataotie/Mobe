//------------------------------------------------------------------------------
//
// file name：Processing.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 加工
	/// </summary>
	[Serializable]
	public partial class Processing
	{
        public override string ToString()
        {
            return _description;
        }
	}
}
