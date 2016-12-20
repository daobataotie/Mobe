using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Model
{
    public class Invoice01
    {
        #region Data

        /// <summary>
        /// ���ݱ��
        /// </summary>
        private string _invoiceId;     

        /// <summary>
        /// ������
        /// </summary>
        private string _employee0Id;

        /// <summary>
        /// ¼����
        /// </summary>
        private string _employee1Id;

        /// <summary>
        /// ������
        /// </summary>
        private string _employee2Id;

        /// <summary>
        /// ������
        /// </summary>
        private string _employee3Id;

        /// <summary>
        /// ����¼��ʱ��
        /// </summary>
        private DateTime? _invoiceLRTime;

        /// <summary>
        /// ���ݹ���ʱ��
        /// </summary>
        private DateTime? _invoiceGZTime;

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        private DateTime? _invoiceZFTime;

        /// <summary>
        /// ��������ԭ��
        /// </summary>
        private string _invoiceZFCause;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime? _invoiceDate;

        /// <summary>
        /// ����ժҪ
        /// </summary>
        private string _invoiceAbstract;

        /// <summary>
        /// ���ݱ�ע
        /// </summary>
        private string _invoiceNote;

        /// <summary>
        /// ����״̬
        /// </summary>
        private int? _invoiceStatus;

        /// <summary>
        /// ¼����
        /// </summary>
        private Employee employee1;

        /// <summary>
        /// ������
        /// </summary>
        private Employee employee2;

        /// <summary>
        /// ������
        /// </summary>
        private Employee employee3;

        /// <summary>
        /// ������
        /// </summary>
        private Employee employee0;

        /// <summary>
        /// ���
        /// </summary>
        private string kind;

        /// <summary>
        /// �������ܶ�
        /// </summary>
        private decimal? _invoiceTotal1;

        /// <summary>
        /// �������Żݶ�
        /// </summary>
        private decimal? _invoiceYHE;

        /// <summary>
        /// ���������Ͷ�
        /// </summary>
        private decimal? _invoiceZSE;

        /// <summary>
        /// �������ۿ۶�
        /// </summary>
        private decimal? _invoiceZRE;

        /// <summary>
        /// ������˰��
        /// </summary>
        private decimal? _invoiceTax;

        /// <summary>
        /// ������Ӧ�����
        /// </summary>
        private decimal? _invoiceTotal0;

        /// <summary>
        /// ��������������
        /// </summary>
        private DateTime? _invoicePayTimeLimit;

        /// <summary>
        /// ������δ�����
        /// </summary>
        private decimal? _invoiceOwed;
        
        /// <summary>
        /// ������λ
        /// </summary>
        //private Model.Company company;

        /// <summary>
        /// ��������տ���
        /// </summary>
        private decimal? payReceived = 0;

        /// <summary>
        /// �������
        /// </summary>
        //private string xpid;        
        /// <summary>
        /// �������
        /// </summary>        
        #endregion

        #region Properties
        /// <summary>
        /// �������
        /// </summary>
        //public string Xpid 
        //{
        //    get { return xpid; }
        //    set { xpid = value; }
        //}
        /// <summary>
        /// ���ݱ��
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
        /// ������
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
        /// ¼����
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
        /// ������
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
        /// ������
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
        /// ����¼��ʱ��
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
        /// ���ݹ���ʱ��
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
        /// ��������ʱ��
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
        /// ����ʱ��
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
        /// ��������ԭ��
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
        /// ����ժҪ
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
        /// ���ݱ�ע
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
        /// ����״̬
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
        /// ¼����
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
        /// ������
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
        /// ������
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
        /// ������
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
        /// �������
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
        /// �ܶ�
        /// </summary>        
        public decimal? InvoiceTotal1
        {
            get { return _invoiceTotal1; }
            set { _invoiceTotal1 = value; }
        }
        /// <summary>
        /// �Żݶ�
        /// </summary>
        public decimal? InvoiceYHE
        {
            get { return _invoiceYHE; }
            set { _invoiceYHE = value; }
        }
        /// <summary>
        /// ���Ͷ�
        /// </summary>
        public decimal? InvoiceZSE
        {
            get { return _invoiceZSE; }
            set { _invoiceZSE = value; }
        }
        /// <summary>
        /// �ۿ۶�
        /// </summary>
        public decimal? InvoiceZRE
        {
            get { return _invoiceZRE; }
            set { _invoiceZRE = value; }
        }
        /// <summary>
        /// ˰��
        /// </summary>
        public decimal? InvoiceTax
        {
            get { return _invoiceTax; }
            set { _invoiceTax = value; }
        }
        /// <summary>
        /// Ӧ�����
        /// </summary>
        public decimal? InvoiceTotal0
        {
            get { return _invoiceTotal0; }
            set { _invoiceTotal0 = value; }
        }
        /// <summary>
        /// ��������
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
        /// ������δ�����
        /// </summary>
        public decimal? InvoiceOwed
        {
            get { return _invoiceOwed; }
            set { _invoiceOwed = value; }
        }
        /// <summary>
        /// ��������տ���
        /// </summary>
        public decimal? PayReceived
        {
            get { return payReceived; }
            set { payReceived = value; }
        }
        #endregion
    }
}
