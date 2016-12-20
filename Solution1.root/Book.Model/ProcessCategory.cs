//------------------------------------------------------------------------------
//
// file name：ProcessCategory.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 加工种类
	/// </summary>
	[Serializable]
	public partial class ProcessCategory
	{

        public override string ToString()
        {
            return _processCategoryName;
        }

        private bool _isCheck;

        public bool IsCheck 
        {
            get { return _isCheck; }
            set { _isCheck = value; }
        }
	}
}
