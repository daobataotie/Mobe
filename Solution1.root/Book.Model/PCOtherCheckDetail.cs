//------------------------------------------------------------------------------
//
// file name：PCOtherCheckDetail.cs
// author: mayanjun
// create date：2011-11-10 15:05:59
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 委外质检详细
    /// </summary>
    [Serializable]
    public partial class PCOtherCheckDetail
    {

        /// <summary>
        /// 判定符号
        /// </summary>
        private string _DeterminantDis;
        /// <summary>
        /// 判定符号
        /// </summary>
        public string DeterminantDis
        {
            get { return ValueToDisplay(_determinant); }
        }
        /// <summary>
        /// 判定符号
        /// </summary>
        public readonly static string PRO_DeterminantDis = "DeterminantDis";

        public string ValueToDisplay(string v)
        {
            string str = string.Empty;
            switch (v)
            {
                case "0":
                    str = "√";
                    break;
                case "1":
                    str = "△";
                    break;
                case "2":
                    str = "X";
                    break;
                default:
                    break;
            }
            return str;
        }

        public DateTime PCOtherCheckDate { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeName1 { get; set; }

        public string SupplierFullName { get; set; }

        public string ProductName { get; set; }
    }
}
