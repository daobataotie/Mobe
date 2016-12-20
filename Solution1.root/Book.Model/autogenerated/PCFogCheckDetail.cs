﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCFogCheckDetail.autogenerated.cs
// author: mayanjun
// create date：2012-3-17 16:10:09
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    public partial class PCFogCheckDetail
    {
        #region Data

        /// <summary>
        /// 
        /// </summary>
        private string _pCImpactCheckDetailId;

        /// <summary>
        /// 
        /// </summary>
        private string _pCFogCheckId;

        /// <summary>
        /// 
        /// </summary>
        private DateTime? _commentLDate;

        /// <summary>
        /// 
        /// </summary>
        private DateTime? _commentRDate;

        /// <summary>
        /// 
        /// </summary>
        private string _method;

        /// <summary>
        /// 
        /// </summary>
        private double? _attrTL;

        /// <summary>
        /// 
        /// </summary>
        private double? _attrTR;

        /// <summary>
        /// 
        /// </summary>
        private double? _attrHL;

        /// <summary>
        /// 
        /// </summary>
        private double? _attrHR;

        /// <summary>
        /// 
        /// </summary>
        private double? _attrCL;

        /// <summary>
        /// 
        /// </summary>
        private double? _attrCR;

        /// <summary>
        /// 
        /// </summary>
        private bool? _passingL;

        /// <summary>
        /// 
        /// </summary>
        private bool? _passingR;

        /// <summary>
        /// 
        /// </summary>
        private DateTime? _commentLTime;

        /// <summary>
        /// 
        /// </summary>
        private DateTime? _commentRTime;

        /// <summary>
        /// 雾度测试
        /// </summary>
        private PCFogCheck _pCFogCheck;

        ///<summary>
        ///日期
        ///</summary>
        private string _checkDate;

        private string _businessHoursId;

        public string BusinessHoursId
        {
            get { return _businessHoursId; }
            set { _businessHoursId = value; }
        }

        private BusinessHours businessHours;

        public virtual BusinessHours BusinessHours
        {
            get { return businessHours; }
            set { businessHours = value; }
        }


        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string PCImpactCheckDetailId
        {
            get
            {
                return this._pCImpactCheckDetailId;
            }
            set
            {
                this._pCImpactCheckDetailId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PCFogCheckId
        {
            get
            {
                return this._pCFogCheckId;
            }
            set
            {
                this._pCFogCheckId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CommentLDate
        {
            get
            {
                return this._commentLDate;
            }
            set
            {
                this._commentLDate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CommentRDate
        {
            get
            {
                return this._commentRDate;
            }
            set
            {
                this._commentRDate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Method
        {
            get
            {
                return this._method;
            }
            set
            {
                this._method = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? attrTL
        {
            get
            {
                return this._attrTL;
            }
            set
            {
                this._attrTL = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? attrTR
        {
            get
            {
                return this._attrTR;
            }
            set
            {
                this._attrTR = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? attrHL
        {
            get
            {
                return this._attrHL;
            }
            set
            {
                this._attrHL = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? attrHR
        {
            get
            {
                return this._attrHR;
            }
            set
            {
                this._attrHR = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? attrCL
        {
            get
            {
                return this._attrCL;
            }
            set
            {
                this._attrCL = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? attrCR
        {
            get
            {
                return this._attrCR;
            }
            set
            {
                this._attrCR = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool? PassingL
        {
            get
            {
                return this._passingL;
            }
            set
            {
                this._passingL = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool? PassingR
        {
            get
            {
                return this._passingR;
            }
            set
            {
                this._passingR = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CommentLTime
        {
            get
            {
                return this._commentLTime;
            }
            set
            {
                this._commentLTime = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CommentRTime
        {
            get
            {
                return this._commentRTime;
            }
            set
            {
                this._commentRTime = value;
            }
        }

        /// <summary>
        /// 雾度测试
        /// </summary>
        public virtual PCFogCheck PCFogCheck
        {
            get
            {
                return this._pCFogCheck;
            }
            set
            {
                this._pCFogCheck = value;
            }

        }
        public string CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_PCImpactCheckDetailId = "PCImpactCheckDetailId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_PCFogCheckId = "PCFogCheckId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_CommentLDate = "CommentLDate";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_CommentRDate = "CommentRDate";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Method = "Method";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_attrTL = "attrTL";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_attrTR = "attrTR";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_attrHL = "attrHL";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_attrHR = "attrHR";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_attrCL = "attrCL";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_attrCR = "attrCR";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_PassingL = "PassingL";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_PassingR = "PassingR";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_CommentLTime = "CommentLTime";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_CommentRTime = "CommentRTime";

        public readonly static string PRO_CheckDate = "CheckDate";
        public readonly static string PRO_BusinessHoursId = "BusinessHoursId";
        #endregion

        private string _checkStandard;

        public string CheckStandard
        {
            get { return _checkStandard; }
            set { _checkStandard = value; }
        }

        public readonly static string PRO_CheckStandard = "CheckStandard";
    }
}
