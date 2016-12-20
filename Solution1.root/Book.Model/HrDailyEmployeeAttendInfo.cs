//------------------------------------------------------------------------------
//
// file name：HrDailyEmployeeAttendInfo.cs
// author: mayanjun
// create date：2010-5-19 11:29:46
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class HrDailyEmployeeAttendInfo
    {
        /// <summary>
        /// 员工姓名
        /// </summary>
        private string _employeeName;
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public readonly static string PRO_EmployeeName = "EmployeeName";

        private Model.Leave _mLeave;
        public Model.Leave MLeave
        {
            get { return _mLeave; }
            set { _mLeave = value; }
        }
        public readonly static string PRO_MLeave = "MLeave";

        private Model.BusinessHours _mBusinessHours;
        public Model.BusinessHours MBusinessHours
        {
            get { return _mBusinessHours; }
            set { _mBusinessHours = value; }
        }
        public readonly static string PRO_MBusinessHours = "MBusinessHours";
    }
}
