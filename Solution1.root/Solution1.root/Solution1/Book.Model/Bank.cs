//------------------------------------------------------------------------------
//
// file name：Bank.cs
// author: peidun
// create date：2009-09-02 上午 10:38:15
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 银行
	/// </summary>
	[Serializable]
	public partial class Bank
	{
        public override string ToString()
        {
            return _bankName;
        }
	}
}
