//------------------------------------------------------------------------------
//
// file name:Account.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 资金账户
	/// </summary>
	[Serializable]
	public partial class Account : IComparable
	{
        public override string ToString()
        {
            return this._accountName;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is Account)
            {
                Account account = (Account)obj;
                return account.AccountName.CompareTo(this.AccountName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is Account)
            {
                return (obj as Model.Account)._accountId == this._accountId ? true : false;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
