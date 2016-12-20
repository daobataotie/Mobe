//------------------------------------------------------------------------------
//
// file name：AtBillsIncome.cs
// author: mayanjun
// create date：2010-11-22 14:21:24
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 票据收付
	/// </summary>
	[Serializable]
	public partial class AtBillsIncome
	{
        private string a;

        public string A
        {
            get { return a; }
            set { a = value; }
        }
        private string b;

        public string B
        {
            get { return b; }
            set { b = value; }
        }
        private string c;

        public string C
        {
            get { return c; }
            set { c = value; }
        }
	}
}
