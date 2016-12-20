//------------------------------------------------------------------------------
//
// file name：BusinessHours.cs
// author: peidun
// create date：2009-09-02 上午 10:38:15
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 班别
	/// </summary>
	[Serializable]
	public partial class BusinessHours
	{
        private string _startTime;
        private string _endTime;
        /// <summary>
        /// 起始时间
        /// </summary>
        public string   startTime
        {
            get
            {
                return this._startTime;
            }
            set
            {
                this._startTime = value;
            }
        }
        public string endTime
        {
            get
            {
                return this._endTime;
            }
            set
            {
                this._endTime = value;
            }
        }

        public string BusinessHoursFormat
        {
            get
            {
                return _businessHoursName + "(" + Convert.ToDateTime( _fromtime).ToString("HH:mm") + "~" + Convert.ToDateTime(_toTime).ToString("HH:mm") + ")";
            }

        }

        public override string ToString()
        {
          //  return string.Format("{0}({1:D2}:{2:D2}～{3:D2}:{4:D2})", _businessHoursName, _fromHour, _fromMinute, _toHour, _toMinute);
          // return this.BusinessHoursName;
            return BusinessHoursFormat;
        }
	}
}
