using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Model
{
    public abstract class Invoice
    {
        #region Data

        /// <summary>
        /// 单据编号
        /// </summary>
        private string _invoiceId;

        /// <summary>
        /// 经手人
        /// </summary>
        private string _employee0Id;

        /// <summary>
        /// 录单人
        /// </summary>
        private string _employee1Id;

        /// <summary>
        /// 过账人
        /// </summary>
        private string _employee2Id;

        /// <summary>
        /// 作废人
        /// </summary>
        private string _employee3Id;


        /// <summary>
        /// 单据录入时间
        /// </summary>
        private DateTime? _invoiceLRTime;

        /// <summary>
        /// 单据过账时间
        /// </summary>
        private DateTime? _invoiceGZTime;

        /// <summary>
        /// 单据作废时间
        /// </summary>
        private DateTime? _invoiceZFTime;

        /// <summary>
        /// 单据作废原因
        /// </summary>
        private string _invoiceZFCause;

        /// <summary>
        /// 单据时间
        /// </summary>
        private DateTime? _invoiceDate;

        /// <summary>
        /// 单据摘要
        /// </summary>
        private string _invoiceAbstract;

        /// <summary>
        /// 单据备注
        /// </summary>
        private string _invoiceNote;

        /// <summary>
        /// 单据状态
        /// </summary>
        private int? _invoiceStatus;

        /// <summary>
        /// 插入时间
        /// </summary>
        private DateTime? _insertTime;

        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime? _updateTime;

        /// <summary>
        /// 员工
        /// </summary>
        private Employee employee2;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee employee0;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee employee1;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee employee3;
        /// <summary>
        /// 审核状态
        /// </summary>
        private int? _auditState;

        /// <summary>
        /// 审核人
        /// </summary>
        private string _auditEmpId;

        /// <summary>
        /// 审核员工
        /// </summary>
        private Employee _auditEmp;
        #endregion

        public string InvoiceStatusDesc
        {
            get
            {
                string r = "";
                switch (this._invoiceStatus)
                {
                    case 0:
                        r = Properties.Resource.InvoiceStatusDescOfDraft;
                        break;

                    case 1:
                        r = Properties.Resource.InvoiceStatusDescOfNormal;
                        break;

                    case 2:
                        r = Properties.Resource.InvoiceStatusDescOfNull;
                        break;
                }
                return r;
            }
        }

        #region Properties


        /// <summary>
        /// 单据编号
        /// </summary>
        public string InvoiceId
        {
            get
            {
                return this._invoiceId;
            }
            set
            {
                this._invoiceId = value;
            }
        }

        /// <summary>
        /// 经手人
        /// </summary>
        public string Employee0Id
        {
            get
            {
                return this._employee0Id;
            }
            set
            {
                this._employee0Id = value;
            }
        }

        /// <summary>
        /// 录单人
        /// </summary>
        public string Employee1Id
        {
            get
            {
                return this._employee1Id;
            }
            set
            {
                this._employee1Id = value;
            }
        }

        /// <summary>
        /// 过账人
        /// </summary>
        public string Employee2Id
        {
            get
            {
                return this._employee2Id;
            }
            set
            {
                this._employee2Id = value;
            }
        }

        /// <summary>
        /// 作废人
        /// </summary>
        public string Employee3Id
        {
            get
            {
                return this._employee3Id;
            }
            set
            {
                this._employee3Id = value;
            }
        }

        /// <summary>
        /// 单据录入时间
        /// </summary>
        public DateTime? InvoiceLRTime
        {
            get
            {
                return this._invoiceLRTime;
            }
            set
            {
                DateTime? dt = value as DateTime?;
                if (dt != null)
                    this._invoiceLRTime = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
                else
                    this._invoiceLRTime = null;
            }
        }

        /// <summary>
        /// 单据过账时间
        /// </summary>
        public DateTime? InvoiceGZTime
        {
            get
            {
                return this._invoiceGZTime;
            }
            set
            {
                DateTime? dt = value as DateTime?;
                if (dt != null)
                    this._invoiceGZTime = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
                else
                    this._invoiceGZTime = null;
            }
        }

        /// <summary>
        /// 单据作废时间
        /// </summary>
        public DateTime? InvoiceZFTime
        {
            get
            {
                return this._invoiceZFTime;
            }
            set
            {
                DateTime? dt = value as DateTime?;
                if (dt != null)
                    this._invoiceZFTime = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
                else
                    this._invoiceZFTime = null;
            }
        }

        /// <summary>
        /// 单据作废原因
        /// </summary>
        public string InvoiceZFCause
        {
            get
            {
                return this._invoiceZFCause;
            }
            set
            {
                this._invoiceZFCause = value;
            }
        }

        /// <summary>
        /// 单据时间
        /// </summary>
        public DateTime? InvoiceDate
        {
            get
            {
                return this._invoiceDate;
            }
            set
            {
                DateTime? dt = value as DateTime?;
                if (dt != null)
                    this._invoiceDate = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
                else
                    this._invoiceDate = null;
            }
        }

        /// <summary>
        /// 单据摘要
        /// </summary>
        public string InvoiceAbstract
        {
            get
            {
                return this._invoiceAbstract;
            }
            set
            {
                this._invoiceAbstract = value;
            }
        }

        /// <summary>
        /// 单据备注
        /// </summary>
        public string InvoiceNote
        {
            get
            {
                return this._invoiceNote;
            }
            set
            {
                this._invoiceNote = value;
            }
        }


        /// <summary>
        /// 单据状态
        /// </summary>
        public int? InvoiceStatus
        {
            get
            {
                return this._invoiceStatus;
            }
            set
            {
                this._invoiceStatus = value;
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
                DateTime? dt = value as DateTime?;
                if (dt != null)
                    this._insertTime = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
                else
                    this._insertTime = null;
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
                int? a = this.AuditState;
                DateTime? dt = value as DateTime?;
                if (dt != null)
                    this._updateTime = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
                else
                    this._updateTime = null;
            }
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int? AuditState
        {
            get
            {
                return this._auditState;
            }
            set
            {
                this._auditState = value;
            }
        }
        

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual string AuditEmpId
        {
            get
            {
                return this._auditEmpId;
                
            }
            set
            {
                this._auditEmpId = value;
            }
        }
        /// <summary>
        /// 审核人
        /// </summary>        
        public virtual Employee AuditEmp
        {
            get
            {
                return this._auditEmp;
            }
            set
            {
                this._auditEmp = value;
            }

        }

        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee2
        {
            get
            {
                return this.employee2;
            }
            set
            {
                this.employee2 = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee0
        {
            get
            {
                return this.employee0;
            }
            set
            {
                this.employee0 = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee1
        {
            get
            {
                return this.employee1;
            }
            set
            {
                this.employee1 = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee3
        {
            get
            {
                return this.employee3;
            }
            set
            {
                this.employee3 = value;
            }

        }
        

        /// <summary>
        /// 单据编号
        /// </summary>
        public readonly static string PROPERTY_INVOICEID = "InvoiceId";

        /// <summary>
        /// 经手人
        /// </summary>
        public readonly static string PROPERTY_EMPLOYEE0ID = "Employee0Id";

        /// <summary>
        /// 录单人
        /// </summary>
        public readonly static string PROPERTY_EMPLOYEE1ID = "Employee1Id";

        /// <summary>
        /// 过账人
        /// </summary>
        public readonly static string PROPERTY_EMPLOYEE2ID = "Employee2Id";

        /// <summary>
        /// 作废人
        /// </summary>
        public readonly static string PROPERTY_EMPLOYEE3ID = "Employee3Id";

        /// <summary>
        /// 单据录入时间
        /// </summary>
        public readonly static string PROPERTY_INVOICELRTIME = "InvoiceLRTime";

        /// <summary>
        /// 单据过账时间
        /// </summary>
        public readonly static string PROPERTY_INVOICEGZTIME = "InvoiceGZTime";

        /// <summary>
        /// 单据作废时间
        /// </summary>
        public readonly static string PROPERTY_INVOICEZFTIME = "InvoiceZFTime";

        /// <summary>
        /// 单据作废原因
        /// </summary>
        public readonly static string PROPERTY_INVOICEZFCAUSE = "InvoiceZFCause";

        /// <summary>
        /// 单据时间
        /// </summary>
        public readonly static string PROPERTY_INVOICEDATE = "InvoiceDate";

        /// <summary>
        /// 单据摘要
        /// </summary>
        public readonly static string PROPERTY_INVOICEABSTRACT = "InvoiceAbstract";

        /// <summary>
        /// 单据备注
        /// </summary>
        public readonly static string PROPERTY_INVOICENOTE = "InvoiceNote";

        /// <summary>
        /// 单据状态
        /// </summary>
        public readonly static string PROPERTY_INVOICESTATUS = "InvoiceStatus";

        /// <summary>
        /// 插入时间
        /// </summary>
        public readonly static string PROPERTY_INSERTTIME = "InsertTime";

        /// <summary>
        /// 修改时间
        /// </summary>
        public readonly static string PROPERTY_UPDATETIME = "UpdateTime";

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is Invoice)
            {
                return (obj as Model.Invoice)._invoiceId == this._invoiceId ? true : false;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual string AuditStateName
        {
            get
            {
                string b = string.Empty;
                switch (this._auditState.HasValue ? this._auditState.Value : 0)
                {

                    case 0: b = "未啟用";
                        break;
                    case 1: b = "待審核";
                        break;
                    case 2: b = "審核中";
                        break;
                    case 3: b = "已審核";
                        break;
                    case 4: b = "棄核";
                        break;
                    default: b = "未啟用";
                        break;
                }
                return b;
            }
        }
 
     
    }
}
