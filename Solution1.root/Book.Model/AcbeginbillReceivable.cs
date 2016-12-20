//------------------------------------------------------------------------------
//
// file name：AcbeginbillReceivable.cs
// author: mayanjun
// create date：2011-6-9 14:42:12
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 期初应收账款
	/// </summary>
	[Serializable]
	public partial class AcbeginbillReceivable
	{
        private System.Collections.Generic.IList<Model.AcbeginbillReceivableDetail> details;

        public System.Collections.Generic.IList<Model.AcbeginbillReceivableDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private string emp0
        {
            get { return this._employee.EmployeeName; }
        }

        private string emp1
        {
            get { return this._employee1.EmployeeName; }
        }
        /// <summary>
        /// 币别名称
        /// </summary>
        public string Bbh
        {
            get { return this._atCurrencyCategory.AtCurrencyName; }
        }
	}
}
