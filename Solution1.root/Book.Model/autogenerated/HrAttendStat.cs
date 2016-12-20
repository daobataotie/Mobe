﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：HrAttendStat.autogenerated.cs
// author: mayanjun
// create date：2010-7-6 11:09:59
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class HrAttendStat
	{
		#region Data

		/// <summary>
		/// 编号
		/// </summary>
		private string _hrAttendStatId;
		
		/// <summary>
		/// 员工编号
		/// </summary>
		private string _employeeId;
		
		/// <summary>
		/// 日期
		/// </summary>
		private DateTime? _hrAttendStatDate;
		
		/// <summary>
		/// 餐费和
		/// </summary>
		private decimal? _loanFee;
		
		/// <summary>
		/// 节支和
		/// </summary>
		private decimal? _lunchFee;
		
		/// <summary>
		/// 加班奖金
		/// </summary>
		private decimal? _overTimeFee;
		
		/// <summary>
		/// 加班酬薪
		/// </summary>
		private decimal? _overTimeBonus;
		
		/// <summary>
		/// 平日加班小时
		/// </summary>
		private double? _generalOverTime;
		
		/// <summary>
		/// 假日加班小时
		/// </summary>
		private double? _holidayOverTime;
		
		/// <summary>
		/// 迟到数量
		/// </summary>
		private int? _lateCount;
		
		/// <summary>
		/// 迟到分钟
		/// </summary>
		private int? _totalLateInMinute;
		
		/// <summary>
		/// 班别津贴
		/// </summary>
		private decimal? _specialBonus;
		
		/// <summary>
		/// 日基数
		/// </summary>
		private double? _daysFactor;
		
		/// <summary>
		/// 月基数
		/// </summary>
		private double? _monthFactor;
		
		/// <summary>
		/// 实际出勤数量
		/// </summary>
		private int? _dutyDateCount;
		
		/// <summary>
		/// 请假日期
		/// </summary>
		private DateTime? _leaveDate;
		
		/// <summary>
		/// 倒扣款假数量
		/// </summary>
		private double? _punishLeaveCount;
		
		/// <summary>
		/// 请假数量
		/// </summary>
		private double? _leaveCount;
		
		/// <summary>
		/// 旷职数量
		/// </summary>
		private int? _absentCount;
		
		/// <summary>
		/// 实际假日
		/// </summary>
		private int? _totalHoliday;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 编号
		/// </summary>
		public string HrAttendStatId
		{
			get 
			{
				return this._hrAttendStatId;
			}
			set 
			{
				this._hrAttendStatId = value;
			}
		}

		/// <summary>
		/// 员工编号
		/// </summary>
		public string EmployeeId
		{
			get 
			{
				return this._employeeId;
			}
			set 
			{
				this._employeeId = value;
			}
		}

		/// <summary>
		/// 日期
		/// </summary>
		public DateTime? HrAttendStatDate
		{
			get 
			{
				return this._hrAttendStatDate;
			}
			set 
			{
				this._hrAttendStatDate = value;
			}
		}

		/// <summary>
		/// 餐费和
		/// </summary>
		public decimal? LoanFee
		{
			get 
			{
				return this._loanFee;
			}
			set 
			{
				this._loanFee = value;
			}
		}

		/// <summary>
		/// 节支和
		/// </summary>
		public decimal? LunchFee
		{
			get 
			{
				return this._lunchFee;
			}
			set 
			{
				this._lunchFee = value;
			}
		}

		/// <summary>
		/// 加班奖金
		/// </summary>
		public decimal? OverTimeFee
		{
			get 
			{
				return this._overTimeFee;
			}
			set 
			{
				this._overTimeFee = value;
			}
		}

		/// <summary>
		/// 加班酬薪
		/// </summary>
		public decimal? OverTimeBonus
		{
			get 
			{
				return this._overTimeBonus;
			}
			set 
			{
				this._overTimeBonus = value;
			}
		}

		/// <summary>
		/// 平日加班小时
		/// </summary>
		public double? GeneralOverTime
		{
			get 
			{
				return this._generalOverTime;
			}
			set 
			{
				this._generalOverTime = value;
			}
		}

		/// <summary>
		/// 假日加班小时
		/// </summary>
		public double? HolidayOverTime
		{
			get 
			{
				return this._holidayOverTime;
			}
			set 
			{
				this._holidayOverTime = value;
			}
		}

		/// <summary>
		/// 迟到数量
		/// </summary>
		public int? LateCount
		{
			get 
			{
				return this._lateCount;
			}
			set 
			{
				this._lateCount = value;
			}
		}

		/// <summary>
		/// 迟到分钟
		/// </summary>
		public int? TotalLateInMinute
		{
			get 
			{
				return this._totalLateInMinute;
			}
			set 
			{
				this._totalLateInMinute = value;
			}
		}

		/// <summary>
		/// 班别津贴
		/// </summary>
		public decimal? SpecialBonus
		{
			get 
			{
				return this._specialBonus;
			}
			set 
			{
				this._specialBonus = value;
			}
		}

		/// <summary>
		/// 日基数
		/// </summary>
		public double? DaysFactor
		{
			get 
			{
				return this._daysFactor;
			}
			set 
			{
				this._daysFactor = value;
			}
		}

		/// <summary>
		/// 月基数
		/// </summary>
		public double? MonthFactor
		{
			get 
			{
				return this._monthFactor;
			}
			set 
			{
				this._monthFactor = value;
			}
		}

		/// <summary>
		/// 实际出勤数量
		/// </summary>
		public int? DutyDateCount
		{
			get 
			{
				return this._dutyDateCount;
			}
			set 
			{
				this._dutyDateCount = value;
			}
		}

		/// <summary>
		/// 请假日期
		/// </summary>
		public DateTime? LeaveDate
		{
			get 
			{
				return this._leaveDate;
			}
			set 
			{
				this._leaveDate = value;
			}
		}

		/// <summary>
		/// 倒扣款假数量
		/// </summary>
		public double? PunishLeaveCount
		{
			get 
			{
				return this._punishLeaveCount;
			}
			set 
			{
				this._punishLeaveCount = value;
			}
		}

		/// <summary>
		/// 请假数量
		/// </summary>
		public double? LeaveCount
		{
			get 
			{
				return this._leaveCount;
			}
			set 
			{
				this._leaveCount = value;
			}
		}

		/// <summary>
		/// 旷职数量
		/// </summary>
		public int? AbsentCount
		{
			get 
			{
				return this._absentCount;
			}
			set 
			{
				this._absentCount = value;
			}
		}

		/// <summary>
		/// 实际假日
		/// </summary>
		public int? TotalHoliday
		{
			get 
			{
				return this._totalHoliday;
			}
			set 
			{
				this._totalHoliday = value;
			}
		}
	
		/// <summary>
		/// 员工
		/// </summary>
		public virtual Employee Employee
		{
			get
			{
				return this._employee;
			}
			set
			{
				this._employee = value;
			}
			
		}
		/// <summary>
		/// 编号
		/// </summary>
		public readonly static string PROPERTY_HRATTENDSTATID = "HrAttendStatId";
		
		/// <summary>
		/// 员工编号
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEID = "EmployeeId";
		
		/// <summary>
		/// 日期
		/// </summary>
		public readonly static string PROPERTY_HRATTENDSTATDATE = "HrAttendStatDate";
		
		/// <summary>
		/// 餐费和
		/// </summary>
		public readonly static string PROPERTY_LOANFEE = "LoanFee";
		
		/// <summary>
		/// 节支和
		/// </summary>
		public readonly static string PROPERTY_LUNCHFEE = "LunchFee";
		
		/// <summary>
		/// 加班奖金
		/// </summary>
		public readonly static string PROPERTY_OVERTIMEFEE = "OverTimeFee";
		
		/// <summary>
		/// 加班酬薪
		/// </summary>
		public readonly static string PROPERTY_OVERTIMEBONUS = "OverTimeBonus";
		
		/// <summary>
		/// 平日加班小时
		/// </summary>
		public readonly static string PROPERTY_GENERALOVERTIME = "GeneralOverTime";
		
		/// <summary>
		/// 假日加班小时
		/// </summary>
		public readonly static string PROPERTY_HOLIDAYOVERTIME = "HolidayOverTime";
		
		/// <summary>
		/// 迟到数量
		/// </summary>
		public readonly static string PROPERTY_LATECOUNT = "LateCount";
		
		/// <summary>
		/// 迟到分钟
		/// </summary>
		public readonly static string PROPERTY_TOTALLATEINMINUTE = "TotalLateInMinute";
		
		/// <summary>
		/// 班别津贴
		/// </summary>
		public readonly static string PROPERTY_SPECIALBONUS = "SpecialBonus";
		
		/// <summary>
		/// 日基数
		/// </summary>
		public readonly static string PROPERTY_DAYSFACTOR = "DaysFactor";
		
		/// <summary>
		/// 月基数
		/// </summary>
		public readonly static string PROPERTY_MONTHFACTOR = "MonthFactor";
		
		/// <summary>
		/// 实际出勤数量
		/// </summary>
		public readonly static string PROPERTY_DUTYDATECOUNT = "DutyDateCount";
		
		/// <summary>
		/// 请假日期
		/// </summary>
		public readonly static string PROPERTY_LEAVEDATE = "LeaveDate";
		
		/// <summary>
		/// 倒扣款假数量
		/// </summary>
		public readonly static string PROPERTY_PUNISHLEAVECOUNT = "PunishLeaveCount";
		
		/// <summary>
		/// 请假数量
		/// </summary>
		public readonly static string PROPERTY_LEAVECOUNT = "LeaveCount";
		
		/// <summary>
		/// 旷职数量
		/// </summary>
		public readonly static string PROPERTY_ABSENTCOUNT = "AbsentCount";
		
		/// <summary>
		/// 实际假日
		/// </summary>
		public readonly static string PROPERTY_TOTALHOLIDAY = "TotalHoliday";
		

		#endregion
	}
}
