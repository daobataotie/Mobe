using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Hr.Attendance.OverTime
{
    public class OverTimeListModel
    {
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string TotalHour { get; set; }
        public string DateList { get; set; }

        #region 注释
        //private string _EmpId;

        //public string EmpId
        //{
        //    get { return _EmpId; }
        //    set { _EmpId = value; }
        //}

        //private string _EmpName;

        //public string EmpName
        //{
        //    get { return _EmpName; }
        //    set { _EmpName = value; }
        //}

        //private string _TotalHour;

        //public string TotalHour
        //{
        //    get { return _TotalHour; }
        //    set { _TotalHour = value; }
        //}

        //private string _DateList;

        //public string DateList
        //{
        //    get { return _DateList; }
        //    set { _DateList = value; }
        //}

        public readonly static string PRO_EmpId = "EmpId";

        public readonly static string PRO_EmpName = "EmpName";

        public readonly static string PRO_DateList = "DateList";

        public readonly static string PRO_TotalHour = "TotalHour";
        #endregion
    }
}
