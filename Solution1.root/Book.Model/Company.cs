//------------------------------------------------------------------------------
//
// file name：Company.cs
// author: peidun
// create date：2009-09-02 上午 10:38:15
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 公司
	/// </summary>
	[Serializable]
	public partial class Company
	{
        public override string ToString()
        {
            return _companyName;
        }
	}
}
