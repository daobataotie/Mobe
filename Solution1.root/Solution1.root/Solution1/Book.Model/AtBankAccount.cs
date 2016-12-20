//------------------------------------------------------------------------------
//
// file name：AtBankAccount.cs
// author: mayanjun
// create date：2010-11-20 10:16:20
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 开户银行
	/// </summary>
	[Serializable]
	public partial class AtBankAccount
	{
        public override string ToString()
        {
            return this._bankAccountName;
        }
	}
}
