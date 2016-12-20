using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Hr.Attendance.Leave
{
    public class LeaveListModel
    {
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string LeaveDateList { get; set; }
        public string LeaveQuantity { get; set; }

        public readonly static string PRO_EmpId = "EmpId";
        public readonly static string PRO_EmpName = "EmpName";
        public readonly static string PRO_LeaveDateList = "LeaveDateList";
        public readonly static string PRO_LeaveQuantity = "LeaveQuantity";

    }
}
