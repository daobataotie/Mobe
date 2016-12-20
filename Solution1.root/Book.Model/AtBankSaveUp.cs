//------------------------------------------------------------------------------
//
// file name：AtBankSaveUp.cs
// author: mayanjun
// create date：2010-11-24 09:51:10
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 银行存提
	/// </summary>
	[Serializable]
	public partial class AtBankSaveUp
	{
        private decimal? a;

        public decimal? A
        {
            get { return a; }
            set { a = value; }
        }
        private decimal? b;

        public decimal? B
        {
            get { return b; }
            set { b = value; }
        }
        private decimal? c;

        public decimal? C
        {
            get { return c; }
            set { c = value; }
        }
	}
}
