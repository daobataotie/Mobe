//------------------------------------------------------------------------------
//
// file name：BGHandbook.cs
// author: mayanjun
// create date：2013-4-16 11:59:00
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 手册
	/// </summary>
	[Serializable]
	public partial class BGHandbook
	{
        private System.Collections.Generic.IList<Model.BGHandbookDetail1> _bGHandbookDetail1;

        /// <summary>
        /// 成品
        /// </summary>
        public System.Collections.Generic.IList<Model.BGHandbookDetail1> Detail1
        {
            get { return _bGHandbookDetail1; }
            set { _bGHandbookDetail1 = value; }
        }
        private System.Collections.Generic.IList<Model.BGHandbookDetail2> _bGHandbookDetail2;

        /// <summary>
        /// 原料
        /// </summary>
        public System.Collections.Generic.IList<Model.BGHandbookDetail2> Detail2
        {
            get { return _bGHandbookDetail2; }
            set { _bGHandbookDetail2 = value; }
        }
	}
}
