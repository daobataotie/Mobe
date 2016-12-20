﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：Employee.autogenerated.cs
// author: mayanjun
// create date：2010-5-14 11:26:10
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class Employee
	{
		#region Data

		/// <summary>
		/// 编号
		/// </summary>
		private string _employeeId;
		
		/// <summary>
		/// 员工编号
		/// </summary>
		private string _iDNo;
		
		/// <summary>
		/// 卡号
		/// </summary>
		private string _cardNo;
		
		/// <summary>
		/// 姓名
		/// </summary>
		private string _employeeName;
		
		/// <summary>
		/// 银行
		/// </summary>
		private string _bankId;
		
		/// <summary>
		/// 身份证编号
		/// </summary>
		private string _employeeIdentityNO;
		
		/// <summary>
		/// 性别
		/// </summary>
		private int? _employeeGender;
		
		/// <summary>
		/// 婚姻状况
		/// </summary>
		private int? _employeeMarried;
		
		/// <summary>
		/// 血型
		/// </summary>
		private int? _employeeBloodType;
		
		/// <summary>
		/// 部门编号
		/// </summary>
		private string _departmentId;
		
		/// <summary>
		/// 员工电话
		/// </summary>
		private string _contactPhone;
		
		/// <summary>
		/// 员工手机
		/// </summary>
		private string _cellphone;
		
		/// <summary>
		/// 联系地址
		/// </summary>
		private string _contactAddress;
		
		/// <summary>
		/// 紧急联络人
		/// </summary>
		private string _urgentContact;
		
		/// <summary>
		/// 联络电话
		/// </summary>
		private string _urgentPhone;
		
		/// <summary>
		/// 班别
		/// </summary>
		private string _businessHoursId;
		
		/// <summary>
		/// 学历
		/// </summary>
		private string _academicBackGroundId;
		
		/// <summary>
		/// 所属公司
		/// </summary>
		private string _companyId;
		
		/// <summary>
		/// 职务编号
		/// </summary>
		private string _dutyId;
		
		/// <summary>
		/// 出生日期
		/// </summary>
		private DateTime? _employeeBirthday;
		
		/// <summary>
		/// 到职日期
		/// </summary>
		private DateTime? _employeeJoinDate;
		
		/// <summary>
		/// 离职日期
		/// </summary>
		private DateTime? _employeeLeaveDate;
		
		/// <summary>
		/// 照片
		/// </summary>
		private byte[] _employeePhoto;
		
		/// <summary>
		/// 服役情况
		/// </summary>
		private int? _militaryState;
		
		/// <summary>
		/// 日工资
		/// </summary>
		private decimal? _dailyPay;
		
		/// <summary>
		/// 月工资
		/// </summary>
		private decimal? _monthlyPay;
		
		/// <summary>
		/// 责任津贴
		/// </summary>
		private decimal? _dutyPay;
		
		/// <summary>
		/// 职务津贴
		/// </summary>
		private decimal? _postPay;
		
		/// <summary>
		/// 职场津贴
		/// </summary>
		private decimal? _fieldPay;
		
		/// <summary>
		/// 保险
		/// </summary>
		private decimal? _insurance;
		
		/// <summary>
		/// 所得税
		/// </summary>
		private decimal? _tax;
		
		/// <summary>
		/// 银行账号
		/// </summary>
		private string _bankAccount;
		
		/// <summary>
		/// 干部
		/// </summary>
		private bool? _isCadre;
		
		/// <summary>
		/// 员工籍贯
		/// </summary>
		private string _employeeNativePlace;
		
		/// <summary>
		/// 员工经历
		/// </summary>
		private string _employeeExperience;
		
		/// <summary>
		/// 员工密码
		/// </summary>
		private string _employeePassword;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 
		/// </summary>
		private string _pinYin;
		
		/// <summary>
		/// 员工职务
		/// </summary>
		private Duty _duty;
		/// <summary>
		/// 学历
		/// </summary>
		private AcademicBackGround _academicBackGround;
		/// <summary>
		/// 银行
		/// </summary>
		private Bank _bank;
		/// <summary>
		/// 班别
		/// </summary>
		private BusinessHours _businessHours;
		/// <summary>
		/// 公司
		/// </summary>
		private Company _company;
		/// <summary>
		/// 部门
		/// </summary>
		private Department _department;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 编号
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
		/// 员工编号
		/// </summary>
		public string IDNo
		{
			get 
			{
				return this._iDNo;
			}
			set 
			{
				this._iDNo = value;
			}
		}

		/// <summary>
		/// 卡号
		/// </summary>
		public string CardNo
		{
			get 
			{
				return this._cardNo;
			}
			set 
			{
				this._cardNo = value;
			}
		}

		/// <summary>
		/// 姓名
		/// </summary>
		public string EmployeeName
		{
			get 
			{
				return this._employeeName;
			}
			set 
			{
				this._employeeName = value;
			}
		}

		/// <summary>
		/// 银行
		/// </summary>
		public string BankId
		{
			get 
			{
				return this._bankId;
			}
			set 
			{
				this._bankId = value;
			}
		}

		/// <summary>
		/// 身份证编号
		/// </summary>
		public string EmployeeIdentityNO
		{
			get 
			{
				return this._employeeIdentityNO;
			}
			set 
			{
				this._employeeIdentityNO = value;
			}
		}

		/// <summary>
		/// 性别
		/// </summary>
		public int? EmployeeGender
		{
			get 
			{
				return this._employeeGender;
			}
			set 
			{
				this._employeeGender = value;
			}
		}

		/// <summary>
		/// 婚姻状况
		/// </summary>
		public int? EmployeeMarried
		{
			get 
			{
				return this._employeeMarried;
			}
			set 
			{
				this._employeeMarried = value;
			}
		}

		/// <summary>
		/// 血型
		/// </summary>
		public int? EmployeeBloodType
		{
			get 
			{
				return this._employeeBloodType;
			}
			set 
			{
				this._employeeBloodType = value;
			}
		}

		/// <summary>
		/// 部门编号
		/// </summary>
		public string DepartmentId
		{
			get 
			{
				return this._departmentId;
			}
			set 
			{
				this._departmentId = value;
			}
		}

		/// <summary>
		/// 员工电话
		/// </summary>
		public string ContactPhone
		{
			get 
			{
				return this._contactPhone;
			}
			set 
			{
				this._contactPhone = value;
			}
		}

		/// <summary>
		/// 员工手机
		/// </summary>
		public string Cellphone
		{
			get 
			{
				return this._cellphone;
			}
			set 
			{
				this._cellphone = value;
			}
		}

		/// <summary>
		/// 联系地址
		/// </summary>
		public string ContactAddress
		{
			get 
			{
				return this._contactAddress;
			}
			set 
			{
				this._contactAddress = value;
			}
		}

		/// <summary>
		/// 紧急联络人
		/// </summary>
		public string UrgentContact
		{
			get 
			{
				return this._urgentContact;
			}
			set 
			{
				this._urgentContact = value;
			}
		}

		/// <summary>
		/// 联络电话
		/// </summary>
		public string UrgentPhone
		{
			get 
			{
				return this._urgentPhone;
			}
			set 
			{
				this._urgentPhone = value;
			}
		}

		/// <summary>
		/// 班别
		/// </summary>
		public string BusinessHoursId
		{
			get 
			{
				return this._businessHoursId;
			}
			set 
			{
				this._businessHoursId = value;
			}
		}

		/// <summary>
		/// 学历
		/// </summary>
		public string AcademicBackGroundId
		{
			get 
			{
				return this._academicBackGroundId;
			}
			set 
			{
				this._academicBackGroundId = value;
			}
		}

		/// <summary>
		/// 所属公司
		/// </summary>
		public string CompanyId
		{
			get 
			{
				return this._companyId;
			}
			set 
			{
				this._companyId = value;
			}
		}

		/// <summary>
		/// 职务编号
		/// </summary>
		public string DutyId
		{
			get 
			{
				return this._dutyId;
			}
			set 
			{
				this._dutyId = value;
			}
		}

		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? EmployeeBirthday
		{
			get 
			{
				return this._employeeBirthday;
			}
			set 
			{
				this._employeeBirthday = value;
			}
		}

		/// <summary>
		/// 到职日期
		/// </summary>
		public DateTime? EmployeeJoinDate
		{
			get 
			{
				return this._employeeJoinDate;
			}
			set 
			{
				this._employeeJoinDate = value;
			}
		}

		/// <summary>
		/// 离职日期
		/// </summary>
		public DateTime? EmployeeLeaveDate
		{
			get 
			{
				return this._employeeLeaveDate;
			}
			set 
			{
				this._employeeLeaveDate = value;
			}
		}

		/// <summary>
		/// 照片
		/// </summary>
		public byte[] EmployeePhoto
		{
			get 
			{
				return this._employeePhoto;
			}
			set 
			{
				this._employeePhoto = value;
			}
		}

		/// <summary>
		/// 服役情况
		/// </summary>
		public int? MilitaryState
		{
			get 
			{
				return this._militaryState;
			}
			set 
			{
				this._militaryState = value;
			}
		}

		/// <summary>
		/// 日工资
		/// </summary>
		public decimal? DailyPay
		{
			get 
			{
				return this._dailyPay;
			}
			set 
			{
				this._dailyPay = value;
			}
		}

		/// <summary>
		/// 月工资
		/// </summary>
		public decimal? MonthlyPay
		{
			get 
			{
				return this._monthlyPay;
			}
			set 
			{
				this._monthlyPay = value;
			}
		}

		/// <summary>
		/// 责任津贴
		/// </summary>
		public decimal? DutyPay
		{
			get 
			{
				return this._dutyPay;
			}
			set 
			{
				this._dutyPay = value;
			}
		}

		/// <summary>
		/// 职务津贴
		/// </summary>
		public decimal? PostPay
		{
			get 
			{
				return this._postPay;
			}
			set 
			{
				this._postPay = value;
			}
		}

		/// <summary>
		/// 职场津贴
		/// </summary>
		public decimal? FieldPay
		{
			get 
			{
				return this._fieldPay;
			}
			set 
			{
				this._fieldPay = value;
			}
		}

		/// <summary>
		/// 保险
		/// </summary>
		public decimal? Insurance
		{
			get 
			{
				return this._insurance;
			}
			set 
			{
				this._insurance = value;
			}
		}

		/// <summary>
		/// 所得税
		/// </summary>
		public decimal? Tax
		{
			get 
			{
				return this._tax;
			}
			set 
			{
				this._tax = value;
			}
		}

		/// <summary>
		/// 银行账号
		/// </summary>
		public string BankAccount
		{
			get 
			{
				return this._bankAccount;
			}
			set 
			{
				this._bankAccount = value;
			}
		}

		/// <summary>
		/// 干部
		/// </summary>
		public bool? IsCadre
		{
			get 
			{
				return this._isCadre;
			}
			set 
			{
				this._isCadre = value;
			}
		}

		/// <summary>
		/// 员工籍贯
		/// </summary>
		public string EmployeeNativePlace
		{
			get 
			{
				return this._employeeNativePlace;
			}
			set 
			{
				this._employeeNativePlace = value;
			}
		}

		/// <summary>
		/// 员工经历
		/// </summary>
		public string EmployeeExperience
		{
			get 
			{
				return this._employeeExperience;
			}
			set 
			{
				this._employeeExperience = value;
			}
		}

		/// <summary>
		/// 员工密码
		/// </summary>
		public string EmployeePassword
		{
			get 
			{
				return this._employeePassword;
			}
			set 
			{
				this._employeePassword = value;
			}
		}

		/// <summary>
		/// 插入时间
		/// </summary>
		public DateTime? InsertTime
		{
			get 
			{
				return this._insertTime;
			}
			set 
			{
				this._insertTime = value;
			}
		}

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? UpdateTime
		{
			get 
			{
				return this._updateTime;
			}
			set 
			{
				this._updateTime = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string PinYin
		{
			get 
			{
				return this._pinYin;
			}
			set 
			{
				this._pinYin = value;
			}
		}
	
		/// <summary>
		/// 员工职务
		/// </summary>
		public virtual Duty Duty
		{
			get
			{
				return this._duty;
			}
			set
			{
				this._duty = value;
			}
			
		}
		/// <summary>
		/// 学历
		/// </summary>
		public virtual AcademicBackGround AcademicBackGround
		{
			get
			{
				return this._academicBackGround;
			}
			set
			{
				this._academicBackGround = value;
			}
			
		}
		/// <summary>
		/// 银行
		/// </summary>
		public virtual Bank Bank
		{
			get
			{
				return this._bank;
			}
			set
			{
				this._bank = value;
			}
			
		}
		/// <summary>
		/// 班别
		/// </summary>
		public virtual BusinessHours BusinessHours
		{
			get
			{
				return this._businessHours;
			}
			set
			{
				this._businessHours = value;
			}
			
		}
		/// <summary>
		/// 公司
		/// </summary>
		public virtual Company Company
		{
			get
			{
				return this._company;
			}
			set
			{
				this._company = value;
			}
			
		}
		/// <summary>
		/// 部门
		/// </summary>
		public virtual Department Department
		{
			get
			{
				return this._department;
			}
			set
			{
				this._department = value;
			}
			
		}
		/// <summary>
		/// 编号
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEID = "EmployeeId";
		
		/// <summary>
		/// 员工编号
		/// </summary>
		public readonly static string PROPERTY_IDNO = "IDNo";
		
		/// <summary>
		/// 卡号
		/// </summary>
		public readonly static string PROPERTY_CARDNO = "CardNo";
		
		/// <summary>
		/// 姓名
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEENAME = "EmployeeName";
		
		/// <summary>
		/// 银行
		/// </summary>
		public readonly static string PROPERTY_BANKID = "BankId";
		
		/// <summary>
		/// 身份证编号
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEIDENTITYNO = "EmployeeIdentityNO";
		
		/// <summary>
		/// 性别
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEGENDER = "EmployeeGender";
		
		/// <summary>
		/// 婚姻状况
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEMARRIED = "EmployeeMarried";
		
		/// <summary>
		/// 血型
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEBLOODTYPE = "EmployeeBloodType";
		
		/// <summary>
		/// 部门编号
		/// </summary>
		public readonly static string PROPERTY_DEPARTMENTID = "DepartmentId";
		
		/// <summary>
		/// 员工电话
		/// </summary>
		public readonly static string PROPERTY_CONTACTPHONE = "ContactPhone";
		
		/// <summary>
		/// 员工手机
		/// </summary>
		public readonly static string PROPERTY_CELLPHONE = "Cellphone";
		
		/// <summary>
		/// 联系地址
		/// </summary>
		public readonly static string PROPERTY_CONTACTADDRESS = "ContactAddress";
		
		/// <summary>
		/// 紧急联络人
		/// </summary>
		public readonly static string PROPERTY_URGENTCONTACT = "UrgentContact";
		
		/// <summary>
		/// 联络电话
		/// </summary>
		public readonly static string PROPERTY_URGENTPHONE = "UrgentPhone";
		
		/// <summary>
		/// 班别
		/// </summary>
		public readonly static string PROPERTY_BUSINESSHOURSID = "BusinessHoursId";
		
		/// <summary>
		/// 学历
		/// </summary>
		public readonly static string PROPERTY_ACADEMICBACKGROUNDID = "AcademicBackGroundId";
		
		/// <summary>
		/// 所属公司
		/// </summary>
		public readonly static string PROPERTY_COMPANYID = "CompanyId";
		
		/// <summary>
		/// 职务编号
		/// </summary>
		public readonly static string PROPERTY_DUTYID = "DutyId";
		
		/// <summary>
		/// 出生日期
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEBIRTHDAY = "EmployeeBirthday";
		
		/// <summary>
		/// 到职日期
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEJOINDATE = "EmployeeJoinDate";
		
		/// <summary>
		/// 离职日期
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEELEAVEDATE = "EmployeeLeaveDate";
		
		/// <summary>
		/// 照片
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEPHOTO = "EmployeePhoto";
		
		/// <summary>
		/// 服役情况
		/// </summary>
		public readonly static string PROPERTY_MILITARYSTATE = "MilitaryState";
		
		/// <summary>
		/// 日工资
		/// </summary>
		public readonly static string PROPERTY_DAILYPAY = "DailyPay";
		
		/// <summary>
		/// 月工资
		/// </summary>
		public readonly static string PROPERTY_MONTHLYPAY = "MonthlyPay";
		
		/// <summary>
		/// 责任津贴
		/// </summary>
		public readonly static string PROPERTY_DUTYPAY = "DutyPay";
		
		/// <summary>
		/// 职务津贴
		/// </summary>
		public readonly static string PROPERTY_POSTPAY = "PostPay";
		
		/// <summary>
		/// 职场津贴
		/// </summary>
		public readonly static string PROPERTY_FIELDPAY = "FieldPay";
		
		/// <summary>
		/// 保险
		/// </summary>
		public readonly static string PROPERTY_INSURANCE = "Insurance";
		
		/// <summary>
		/// 所得税
		/// </summary>
		public readonly static string PROPERTY_TAX = "Tax";
		
		/// <summary>
		/// 银行账号
		/// </summary>
		public readonly static string PROPERTY_BANKACCOUNT = "BankAccount";
		
		/// <summary>
		/// 干部
		/// </summary>
		public readonly static string PROPERTY_ISCADRE = "IsCadre";
		
		/// <summary>
		/// 员工籍贯
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEENATIVEPLACE = "EmployeeNativePlace";
		
		/// <summary>
		/// 员工经历
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEEXPERIENCE = "EmployeeExperience";
		
		/// <summary>
		/// 员工密码
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEPASSWORD = "EmployeePassword";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PROPERTY_INSERTTIME = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PROPERTY_UPDATETIME = "UpdateTime";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PROPERTY_PINYIN = "PinYin";
		

		#endregion
	}
}
