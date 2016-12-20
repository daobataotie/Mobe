//------------------------------------------------------------------------------
//
// file name：WorkHouse.cs
// author: peidun
// create date：2009-11-18 15:44:01
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 工作中心
	/// </summary>
	[Serializable]
	public partial class WorkHouse
	{
        public override string ToString()
        {
            return this._workhousename;
        }
	}
}
