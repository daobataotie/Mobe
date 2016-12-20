//------------------------------------------------------------------------------
//
// file name：Leave.cs
// author: peidun
// create date：2010-3-16 16:05:50
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 员工休假
    /// </summary>
    [Serializable]
    public partial class Leave
    {
        private decimal? _LeaveDateCount;

        public decimal? LeaveDateCount
        {
            get { return _LeaveDateCount; }
            set { _LeaveDateCount = value; }
        }

        public readonly static string PRO_LeaveDateCount = "LeaveDateCount";


        public string GetLeaveRangeName
        {
            get
            {
                string s = string.Empty;
                switch (this.LeaveRange.Value)
                {
                    case 0:
                        s = "整日";
                        break;
                    case 1:
                        s = "上半日";
                        break;
                    case 2:
                        s = "下半日";
                        break;
                }
                return s;
            }
        }

    }
}
