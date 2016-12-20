//------------------------------------------------------------------------------
//
// file name：AcademicBackGround.cs
// author: peidun
// create date：2009-09-02 上午 10:38:15
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 学历
	/// </summary>
	[Serializable]
	public partial class AcademicBackGround
	{
        public override string ToString()
        {
            return _academicBackGroundName;
        }
	}
}
