//------------------------------------------------------------------------------
//
// file name：MonthlySalary.cs
// author: peidun
// create date：2010-3-24 11:21:44
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 薪资基础
	/// </summary>
	[Serializable]
	public partial class MonthlySalary
	{

	}
    public struct MonthlySalaryStruct
    {
        public string MonthlySalaryId;
        public decimal EffectFactor;
        public decimal OtherPay;
        public decimal OtherPunish;
        public decimal AnnualBonus;
        public string EmployeeId;
        public DateTime IdentifyDate;
        public float HolidayBonusGivenDays;

    }
}
