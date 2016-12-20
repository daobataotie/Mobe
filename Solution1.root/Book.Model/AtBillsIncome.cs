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
        private bool _checked;
        public bool Checked
        {
            get { return this._checked; }
            set { this._checked = value; }
        }
        public string BillsOftenName
        {
            get
            {
                string a="";
                switch (this._incomeCategory)
                {
                    case "0": a = "未托收";
                        break;
                    case "1": a = "未兌現";
                        break;
                    case "2": a = "已兌現";
                        break;
                
                }
                return a;
            
            }
        }
	}
}
