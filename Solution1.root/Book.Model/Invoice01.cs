using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Model
{
    public class Invoice01
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
        /// 录单人
        /// </summary>
        private Employee employee1;

        /// <summary>
        /// 过账人
        /// </summary>
        private Employee employee2;

        /// <summary>
        /// 作废人
        /// </summary>
        private Employee employee3;

        /// <summary>
        /// 经手人
        /// </summary>
        private Employee employee0;

        /// <summary>
        /// 类别
        /// </summary>
        private string kind;

        /// <summary>
        /// 进货单总额
        /// </summary>
        private decimal? _invoiceTotal1;

        /// <summary>
        /// 进货单优惠额
        /// </summary>
        private decimal? _invoiceYHE;

        /// <summary>
        /// 进货单赠送额
        /// </summary>
        private decimal? _invoiceZSE;

        /// <summary>
        /// 进货单折扣额
        /// </summary>
        private decimal? _invoiceZRE;

        /// <summary>
        /// 进货单税额
        /// </summary>
        private decimal? _invoiceTax;

        /// <summary>
        /// 进货单应付款额
        /// </summary>
        private decimal? _invoiceTotal0;

        /// <summary>
        /// 进货单付款期限
        /// </summary>
        private DateTime? _invoicePayTimeLimit;

        /// <summary>
        /// 进货单未付款额
        /// </summary>
        private decimal? _invoiceOwed;
        
        /// <summary>
        /// 往来单位
        /// </summary>
        //private Model.Company company;

        /// <summary>
        /// 付款或者收款金额
        /// </summary>
        private decimal? payReceived = 0;

        /// <summary>
        /// 冲销编号
        /// </summary>
        //private string xpid;        
        /// <summary>
        /// 冲销金额
        /// </summary>        
        #endregion

        #region Properties
        /// <summary>
        /// 冲销编号
        /// </summary>
        //public string Xpid 
        //{
        //    get { return xpid; }
        //    set { xpid = value; }
        //}
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
                    dt = null;
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
        /// 录单人
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
        /// 过账人
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
        /// 作废人
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
        /// 经手人
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
        /// 单据类别
        /// </summary>
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
        /// <summary>
        /// 总额
        /// </summary>        
        public decimal? InvoiceTotal1
        {
            get { return _invoiceTotal1; }
            set { _invoiceTotal1 = value; }
        }
        /// <summary>
        /// 优惠额
        /// </summary>
        public decimal? InvoiceYHE
        {
            get { return _invoiceYHE; }
            set { _invoiceYHE = value; }
        }
        /// <summary>
        /// 赠送额
        /// </summary>
        public decimal? InvoiceZSE
        {
            get { return _invoiceZSE; }
            set { _invoiceZSE = value; }
        }
        /// <summary>
        /// 折扣额
        /// </summary>
        public decimal? InvoiceZRE
        {
            get { return _invoiceZRE; }
            set { _invoiceZRE = value; }
        }
        /// <summary>
        /// 税额
        /// </summary>
        public decimal? InvoiceTax
        {
            get { return _invoiceTax; }
            set { _invoiceTax = value; }
        }
        /// <summary>
        /// 应付款额
        /// </summary>
        public decimal? InvoiceTotal0
        {
            get { return _invoiceTotal0; }
            set { _invoiceTotal0 = value; }
        }
        /// <summary>
        /// 付款期限
        /// </summary>
        public DateTime? InvoicePayTimeLimit
        {
            get
            {
                return _invoicePayTimeLimit;
            }
            set
            {
                DateTime? dt = value as DateTime?;
                if (dt != null)
                    this._invoicePayTimeLimit = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
                else
                    dt = null;
            }
        }

        /// <summary>
        /// 进货单未付款额
        /// </summary>
        public decimal? InvoiceOwed
        {
            get { return _invoiceOwed; }
            set { _invoiceOwed = value; }
        }
        /// <summary>
        /// 付款或者收款金额
        /// </summary>
        public decimal? PayReceived
        {
            get { return payReceived; }
            set { payReceived = value; }
        }
        #endregion
    }
}
