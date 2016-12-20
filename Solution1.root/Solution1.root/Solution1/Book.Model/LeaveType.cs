//------------------------------------------------------------------------------
//
// file name：LeaveType.cs
// author: peidun
// create date：2010-2-6 10:33:10
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 休假类别
	/// </summary>
	[Serializable]
	public partial class LeaveType
	{
        public override string ToString()
        {
            return this._leaveTypeName;
        }

	}
}
