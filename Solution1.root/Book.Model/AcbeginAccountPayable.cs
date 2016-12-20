//------------------------------------------------------------------------------
//
// file name：AcbeginAccountPayable.cs
// author: mayanjun
// create date：2011-6-9 14:42:12
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 期初应付账款
    /// </summary>
    [Serializable]
    public partial class AcbeginAccountPayable
    {
        private System.Collections.Generic.IList<Model.AcbeginAccountPayableDetail> details;

        public System.Collections.Generic.IList<Model.AcbeginAccountPayableDetail> Details
        {
            get { return details; }
            set { details = value; }
        }


        public string Emp0
        {
            get { return this._employee0 == null ? "" : this._employee0.EmployeeName; }
        }

        public string Emp1
        {
            get { return this._employee == null ? "" : this._employee.EmployeeName; }
        }
        /// <summary>
        /// 币别名称
        /// </summary>
        public string Bbh
        {
            get { return this._atCurrencyCategory == null ? "" : this._atCurrencyCategory.AtCurrencyName; }
        }
    }
}
