using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Model
{
    public class Invoice00
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
        /// 员工
        /// </summary>
        private Employee employee1;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee employee2;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee employee3;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee employee0;

        private string kind;

        #endregion

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
                    dt = null;
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
                    dt = null;
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
                    dt = null;
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

        public string Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }

        public string KindDesc
        {
            get
            {
                return Properties.Resource.ResourceManager.GetString(string.Format("InvoiceKindDescOf{0}", this.kind.ToUpper()), System.Threading.Thread.CurrentThread.CurrentCulture);
            }
        }

        public string InvoiceStatusDesc
        {
            get
            {
                string r = "";
                switch (this.InvoiceStatus)
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

        #endregion
    }
}
