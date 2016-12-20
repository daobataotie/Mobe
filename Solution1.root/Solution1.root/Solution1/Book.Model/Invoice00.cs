using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Model
{
    public class Invoice00
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
        /// Ա��
        /// </summary>
        private Employee employee1;
        /// <summary>
        /// Ա��
        /// </summary>
        private Employee employee2;
        /// <summary>
        /// Ա��
        /// </summary>
        private Employee employee3;
        /// <summary>
        /// Ա��
        /// </summary>
        private Employee employee0;

        private string kind;

        #endregion

        #region Properties

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
                    this._invoiceZFTime = null;
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
        /// Ա��
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
        /// Ա��
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
        /// Ա��
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
        /// Ա��
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
