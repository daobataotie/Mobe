//------------------------------------------------------------------------------
//
// file name：UnitGroup.cs
// author: peidun
// create date：2009-08-03 9:37:30
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 单位组
	/// </summary>
	[Serializable]
	public partial class UnitGroup
	{
        private System.Collections.Generic.IList<ProductUnit> details;

        public System.Collections.Generic.IList<ProductUnit> Details
        {
            get { return details; }
            set { details = value; }
        }
        public override string ToString()
        {
            return this._id + "-" + this._unitGroupName;
        }
	}
}
