using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  西安飛馳軟件有限公司
//                     版權所有 圍著必究
// 功能描述: 客戶產品設置
// 文 件 名：CustomerProductForm
// 编 码 人: 马艳军  裴盾             完成时间:2009-10-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    class MonthlySalarysFee
    {
        #region Data

        /// <summary>
        /// 员工编号
        /// </summary>
        private string _employeeid;

        public string Employeeid
        {
            get { return _employeeid; }
            set { _employeeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _dailypay;

        public int Dailypay
        {
            get { return _dailypay; }
            set { _dailypay = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _idno;

        public string Idno
        {
            get { return _idno; }
            set { _idno = value; }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        private string _departmentname;

        public string Departmentname
        {
            get { return _departmentname; }
            set { _departmentname = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _companyname;

        public string Companyname
        {
            get { return _companyname; }
            set { _companyname = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _monthlypay;

        public int Monthlypay
        {
            get { return _monthlypay; }
            set { _monthlypay = value; }
        }

        /// <summary>
        /// 工资
        /// </summary>
        private int _basepay;

        public int Basepay
        {
            get { return _basepay; }
            set { _basepay = value; }
        }

        /// <summary>
        /// 职场
        /// </summary>
        private int _fieldpay;

        public int Fieldpay
        {
            get { return _fieldpay; }
            set { _fieldpay = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _subtotal;

        public int Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        /// <summary>
        /// 伙食费
        /// </summary>
        private int _lunchfee;

        public int Lunchfee
        {
            get { return _lunchfee; }
            set { _lunchfee = value; }
        }

        /// <summary>
        /// 保险费
        /// </summary>
        private int _insurance;

        public int Insurance
        {
            get { return _insurance; }
            set { _insurance = value; }
        }

        /// <summary>
        /// 借支
        /// </summary>
        private int _loanfee;

        public int Loanfee
        {
            get { return _loanfee; }
            set { _loanfee = value; }
        }

        /// <summary>
        /// 所得税
        /// </summary>
        private int _tax;

        public int Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _salarytotal;

        public int Salarytotal
        {
            get { return _salarytotal; }
            set { _salarytotal = value; }
        }

        /// <summary>
        /// 责任
        /// </summary>
        private int _dutypay;

        public int Dutypay
        {
            get { return _dutypay; }
            set { _dutypay = value; }
        }

        /// <summary>
        /// 职务
        /// </summary>
        private int _postpay;

        public int Postpay
        {
            get { return _postpay; }
            set { _postpay = value; }
        }

        /// <summary>
        /// 全勤
        /// </summary>
        private int _allattendbounds;

        public int Allattendbounds
        {
            get { return _allattendbounds; }
            set { _allattendbounds = value; }
        }

        /// <summary>
        /// 班别贴
        /// </summary>
        private int _specialbounds;

        public int Specialbounds
        {
            get { return _specialbounds; }
            set { _specialbounds = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _workbounds;

        public int Workbounds
        {
            get { return _workbounds; }
            set { _workbounds = value; }
        }

        /// <summary>
        /// 绩效
        /// </summary>
        private int _effectbounds;

        public int Effectbounds
        {
            get { return _effectbounds; }
            set { _effectbounds = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _techbounds;

        public int Techbounds
        {
            get { return _techbounds; }
            set { _techbounds = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _effectfactor;

        public int Effectfactor
        {
            get { return _effectfactor; }
            set { _effectfactor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _generalovertime;

        public int Generalovertime
        {
            get { return _generalovertime; }
            set { _generalovertime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _holidayovertime;

        public int Holidayovertime
        {
            get { return _holidayovertime; }
            set { _holidayovertime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _generalovertimefee;

        public int Generalovertimefee
        {
            get { return _generalovertimefee; }
            set { _generalovertimefee = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _holidayovertimefee;

        public int Holidayovertimefee
        {
            get { return _holidayovertimefee; }
            set { _holidayovertimefee = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _overtimefee;

        public int Overtimefee
        {
            get { return _overtimefee; }
            set { _overtimefee = value; }
        }

        /// <summary>
        /// 加班
        /// </summary>
        private int _overtimebound;

        public int Overtimebound
        {
            get { return _overtimebound; }
            set { _overtimebound = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _annualholidayfee;

        public int Annualholidayfee
        {
            get { return _annualholidayfee; }
            set { _annualholidayfee = value; }
        }

        /// <summary>
        /// 其他补
        /// </summary>
        private int _otherpay;

        public int Otherpay
        {
            get { return _otherpay; }
            set { _otherpay = value; }
        }

        /// <summary>
        /// 其他扣
        /// </summary>
        private int _otherpunish;

        public int Otherpunish
        {
            get { return _otherpunish; }
            set { _otherpunish = value; }
        }

        /// <summary>
        /// 实领金额
        /// </summary>
        private int _boundtotal;

        public int Boundtotal
        {
            get { return _boundtotal; }
            set { _boundtotal = value; }
        }

        /// <summary>
        /// 应发金额
        /// </summary>
        private int _shouldpay;

        public int Shouldpay
        {
            get { return _shouldpay; }
            set { _shouldpay = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _annualpay;

        public int Annualpay
        {
            get { return _annualpay; }
            set { _annualpay = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private decimal _givendays;

        public decimal Givendays
        {
            get { return _givendays; }
            set { _givendays = value; }
        }

        /// <summary>
        /// 迟到扣
        /// </summary>
        private string _lastpunish;

        public string Lastpunish
        {
            get { return _lastpunish; }
            set { _lastpunish = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _lastcount;

        public int Lastcount
        {
            get { return _lastcount; }
            set { _lastcount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _totallastinmintes;

        public int Totallastinmintes
        {
            get { return _totallastinmintes; }
            set { _totallastinmintes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private decimal _totallastinhour;

        public decimal Totallastinhour
        {
            get { return _totallastinhour; }
            set { _totallastinhour = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int _punishcount;

        public int Punishcount
        {
            get { return _punishcount; }
            set { _punishcount = value; }
        }

        /// <summary>
        /// 员工名称
        /// </summary>
        private string _employeename;

        public string Employeename
        {
            get { return _employeename; }
            set { _employeename = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _boundtitle;

        public string Boundtitle
        {
            get { return _boundtitle; }
            set { _boundtitle = value; }
        }

        /// <summary>
        /// 薪资标题
        /// </summary>
        private string _salarytitle;

        public string Salarytitle
        {
            get { return _salarytitle; }
            set { _salarytitle = value; }
        }

        /// <summary>
        /// 日基数
        /// </summary>
        private decimal _dayfactor;

        public decimal Dayfactor
        {
            get { return _dayfactor; }
            set { _dayfactor = value; }
        }

        /// <summary>
        /// 月基数
        /// </summary>
        private decimal _monthfactor;

        public decimal Monthfactor
        {
            get { return _monthfactor; }
            set { _monthfactor = value; }
        }

        #endregion
    }
}
