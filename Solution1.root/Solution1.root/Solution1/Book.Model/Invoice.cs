using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Model
{
    public abstract class Invoice
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
        /// ����ʱ��
        /// </summary>
        private DateTime? _insertTime;

        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        private DateTime? _updateTime;

        /// <summary>
        /// Ա��
        /// </summary>
        private Employee employee2;
        /// <summary>
        /// Ա��
        /// </summary>
        private Employee employee0;
        /// <summary>
        /// Ա��
        /// </summary>
        private Employee employee1;
        /// <summary>
        /// Ա��
        /// </summary>
        private Employee employee3;

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
                    this._invoiceLRTime = null;
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
                    this._invoiceGZTime = null;
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
                    this._invoiceDate = null;
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
        /// ����ʱ��
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
        /// �޸�ʱ��
        /// </summary>
        public DateTime? UpdateTime
        {
            get
            {
                return this._updateTime;
            }
            set
            {

                DateTime? dt = value as DateTime?;
                if (dt != null)
                    this._updateTime = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
                else
                    this._updateTime = null;
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
        /// ���ݱ��
        /// </summary>
        public readonly static string PROPERTY_INVOICEID = "InvoiceId";

        /// <summary>
        /// ������
        /// </summary>
        public readonly static string PROPERTY_EMPLOYEE0ID = "Employee0Id";

        /// <summary>
        /// ¼����
        /// </summary>
        public readonly static string PROPERTY_EMPLOYEE1ID = "Employee1Id";

        /// <summary>
        /// ������
        /// </summary>
        public readonly static string PROPERTY_EMPLOYEE2ID = "Employee2Id";

        /// <summary>
        /// ������
        /// </summary>
        public readonly static string PROPERTY_EMPLOYEE3ID = "Employee3Id";

        /// <summary>
        /// ����¼��ʱ��
        /// </summary>
        public readonly static string PROPERTY_INVOICELRTIME = "InvoiceLRTime";

        /// <summary>
        /// ���ݹ���ʱ��
        /// </summary>
        public readonly static string PROPERTY_INVOICEGZTIME = "InvoiceGZTime";

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public readonly static string PROPERTY_INVOICEZFTIME = "InvoiceZFTime";

        /// <summary>
        /// ��������ԭ��
        /// </summary>
        public readonly static string PROPERTY_INVOICEZFCAUSE = "InvoiceZFCause";

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public readonly static string PROPERTY_INVOICEDATE = "InvoiceDate";

        /// <summary>
        /// ����ժҪ
        /// </summary>
        public readonly static string PROPERTY_INVOICEABSTRACT = "InvoiceAbstract";

        /// <summary>
        /// ���ݱ�ע
        /// </summary>
        public readonly static string PROPERTY_INVOICENOTE = "InvoiceNote";

        /// <summary>
        /// ����״̬
        /// </summary>
        public readonly static string PROPERTY_INVOICESTATUS = "InvoiceStatus";

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public readonly static string PROPERTY_INSERTTIME = "InsertTime";

        /// <summary>
        /// �޸�ʱ��
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
    }
}
