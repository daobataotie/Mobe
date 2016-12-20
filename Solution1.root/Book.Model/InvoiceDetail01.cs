using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Model
{
    public class InvoiceDetail01
    {

        #region Data

        private string companyId;

        public string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        public readonly static string PROPERTY_COMPANYID = "CompanyId";

        private string employee0Id;

        public string Employee0Id
        {
            get { return employee0Id; }
            set { employee0Id = value; }
        }
        public readonly static string PROPERTY_EMPLOYEE0ID = "Employee0Id";

        private Model.Employee employee;

        public Model.Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }
        //private Model.Company company;

        //public Model.Company Company
        //{
        //    get { return company; }
        //    set { company = value; }
        //}

        /// <summary>
        /// ������Ʒ���
        /// </summary>
        private string _invoiceDetailId;

        /// <summary>
        /// ��Ʒ���
        /// </summary>
        private string _productId;

        /// <summary>
        /// ���ݱ��
        /// </summary>
        private string _invoiceId;

        /// <summary>
        /// ������???����
        /// </summary>
        private double? _invoiceDetailQuantity;

        /// <summary>
        /// ������Ʒ����
        /// </summary>
        private decimal? _invoiceDetailPrice;

        /// <summary>
        /// �����˻�����Ʒ�ۿ��ʣ�10��ʾ��9��
        /// </summary>
        private double? _invoiceDetailDiscountRate;

        /// <summary>
        /// ������Ʒ�ۿ۶�
        /// </summary>
        private decimal? _invoiceDetailDiscount;

        /// <summary>
        /// ������Ʒ˰��
        /// </summary>
        private double? _invoiceDetailTaxRate;

        /// <summary>
        /// ������Ʒ˰��
        /// </summary>
        private decimal? _invoiceDetailTax;

        /// <summary>
        /// ������ƷӦ�տ�
        /// </summary>
        private decimal? _invoiceDetailMoney0;

        /// <summary>
        /// ������Ʒ��ע
        /// </summary>
        private string _invoiceDetailNote;

        /// <summary>
        /// ������Ʒ���ͷ�
        /// </summary>
        private bool? _invoiceDetailZS;

        /// <summary>
        /// ������Ʒ���
        /// </summary>
        private decimal? _invoiceDetailMoney1;

        /// <summary>
        /// ������Ʒ�ɱ���
        /// </summary>
        private decimal? _invoiceDetailCostPrice;

        /// <summary>
        /// �����ɱ����
        /// </summary>
        private decimal? _invoiceDetailCostMoney;

        /// <summary>
        /// ��Ʒ
        /// </summary>
        private Product product;

        #endregion

        #region Properties

        /// <summary>
        /// ������Ʒ���
        /// </summary>
        public string InvoiceDetailId
        {
            get
            {
                return this._invoiceDetailId;
            }
            set
            {
                this._invoiceDetailId = value;
            }
        }

        /// <summary>
        /// ��Ʒ���
        /// </summary>
        public string ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }

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
        /// ������???����
        /// </summary>
        public double? InvoiceDetailQuantity
        {
            get
            {
                return this._invoiceDetailQuantity;
            }
            set
            {
                this._invoiceDetailQuantity = value;
            }
        }

        /// <summary>
        /// ������Ʒ����
        /// </summary>
        public decimal? InvoiceDetailPrice
        {
            get
            {
                return this._invoiceDetailPrice;
            }
            set
            {
                this._invoiceDetailPrice = value;
            }
        }

        /// <summary>
        /// �����˻�����Ʒ�ۿ��ʣ�10��ʾ��9��
        /// </summary>
        public double? InvoiceDetailDiscountRate
        {
            get
            {
                return this._invoiceDetailDiscountRate;
            }
            set
            {
                this._invoiceDetailDiscountRate = value;
            }
        }

        /// <summary>
        /// ������Ʒ�ۿ۶�
        /// </summary>
        public decimal? InvoiceDetailDiscount
        {
            get
            {
                return this._invoiceDetailDiscount;
            }
            set
            {
                this._invoiceDetailDiscount = value;
            }
        }

        /// <summary>
        /// ������Ʒ˰��
        /// </summary>
        public double? InvoiceDetailTaxRate
        {
            get
            {
                return this._invoiceDetailTaxRate;
            }
            set
            {
                this._invoiceDetailTaxRate = value;
            }
        }

        /// <summary>
        /// ������Ʒ˰��
        /// </summary>
        public decimal? InvoiceDetailTax
        {
            get
            {
                return this._invoiceDetailTax;
            }
            set
            {
                this._invoiceDetailTax = value;
            }
        }

        /// <summary>
        /// ������ƷӦ�տ�
        /// </summary>
        public decimal? InvoiceDetailMoney0
        {
            get
            {
                return this._invoiceDetailMoney0;
            }
            set
            {
                this._invoiceDetailMoney0 = value;
            }
        }

        /// <summary>
        /// ������Ʒ��ע
        /// </summary>
        public string InvoiceDetailNote
        {
            get
            {
                return this._invoiceDetailNote;
            }
            set
            {
                this._invoiceDetailNote = value;
            }
        }

        /// <summary>
        /// ������Ʒ���ͷ�
        /// </summary>
        public bool? InvoiceDetailZS
        {
            get
            {
                return this._invoiceDetailZS;
            }
            set
            {
                this._invoiceDetailZS = value;
            }
        }

        /// <summary>
        /// ������Ʒ���
        /// </summary>
        public decimal? InvoiceDetailMoney1
        {
            get
            {
                return this._invoiceDetailMoney1;
            }
            set
            {
                this._invoiceDetailMoney1 = value;
            }
        }

        /// <summary>
        /// ������Ʒ�ɱ���
        /// </summary>
        public decimal? InvoiceDetailCostPrice
        {
            get
            {
                return this._invoiceDetailCostPrice;
            }
            set
            {
                this._invoiceDetailCostPrice = value;
            }
        }

        /// <summary>
        /// �����ɱ����
        /// </summary>
        public decimal? InvoiceDetailCostMoney
        {
            get
            {
                return this._invoiceDetailCostMoney;
            }
            set
            {
                this._invoiceDetailCostMoney = value;
            }
        }

        /// <summary>
        /// ��Ʒ
        /// </summary>
        public virtual Product Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
            }

        }

        private string kind;

        public string Kind
        {
            get
            {
                switch (this.kind)
                {
                    case "xs":
                        return "�N��";
                        //return Properties.Resource.XS;
                    case "xt":
                        return "�N��";
                        //return Properties.Resource.XT;
                    case "cg":
                        return "��ُ";
                        //return Properties.Resource.CG;
                    case "ct":
                        return "����";
                        //return Properties.Resource.CT;
                }
                return "";
            }
            set { kind = value; }
        }

        private string invoiceFpbh;

        public string InvoiceFpbh
        {
            get { return invoiceFpbh; }
            set { invoiceFpbh = value; }
        }
        public readonly static string PROPERTY_INVOICEFPBH = "InvoiceFpbh";

        /// <summary>
        /// ����
        /// </summary>
        public decimal? YiShou
        {
            get
            {
                return this.InvoiceZongJi - this.InvoiceOwed;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public readonly static string PROPERTY_YISHOU = "YiShou";

        private DateTime invoiceDate;

        public DateTime InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; }
        }
        public readonly static string PROPERTY_INVOICEDATE = "InvoiceDate";


        public readonly static string PROPERTY_KIND = "Kind";
        /// <summary>
        /// ������Ʒ���
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILID = "InvoiceDetailId";

        /// <summary>
        /// ��Ʒ���
        /// </summary>
        public readonly static string PROPERTY_PRODUCTID = "ProductId";

        /// <summary>
        /// ���ݱ��
        /// </summary>
        public readonly static string PROPERTY_INVOICEID = "InvoiceId";

        /// <summary>
        /// ������???����
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILQUANTITY = "InvoiceDetailQuantity";

        /// <summary>
        /// ������Ʒ����
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILPRICE = "InvoiceDetailPrice";

        /// <summary>
        /// �����˻�����Ʒ�ۿ��ʣ�10��ʾ��9��
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILDISCOUNTRATE = "InvoiceDetailDiscountRate";

        /// <summary>
        /// ������Ʒ�ۿ۶�
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILDISCOUNT = "InvoiceDetailDiscount";

        /// <summary>
        /// ������Ʒ˰��
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILTAXRATE = "InvoiceDetailTaxRate";

        /// <summary>
        /// ������Ʒ˰��
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILTAX = "InvoiceDetailTax";

        /// <summary>
        /// ������ƷӦ�տ�
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILMONEY0 = "InvoiceDetailMoney0";

        /// <summary>
        /// ������Ʒ��ע
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILNOTE = "InvoiceDetailNote";

        /// <summary>
        /// ������Ʒ���ͷ�
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILZS = "InvoiceDetailZS";

        /// <summary>
        /// ������Ʒ���
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILMONEY1 = "InvoiceDetailMoney1";

        /// <summary>
        /// ������Ʒ�ɱ���
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILCOSTPRICE = "InvoiceDetailCostPrice";

        /// <summary>
        /// �����ɱ����
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILCOSTMONEY = "InvoiceDetailCostMoney";


        #endregion



        /// <summary>
        /// �������ܶ�
        /// </summary>
        private decimal? _invoiceHeJi;

        /// <summary>
        /// �������Żݶ�
        /// </summary>
        private decimal? _invoiceYHE;

        /// <summary>
        /// ����������???
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
        /// ������Ӧ�տ��
        /// </summary>
        private decimal? _invoiceZongJi;

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        private DateTime? _invoicePayTimeLimit;

        /// <summary>
        /// ������δ�տ��
        /// </summary>
        private decimal? _invoiceOwed;

        /// <summary>
        /// �������ܶ�
        /// </summary>
        public decimal? InvoiceHeJi
        {
            get
            {
                return this._invoiceHeJi;
            }
            set
            {
                this._invoiceHeJi = value;
            }
        }

        /// <summary>
        /// �������Żݶ�
        /// </summary>
        public decimal? InvoiceYHE
        {
            get
            {
                return this._invoiceYHE;
            }
            set
            {
                this._invoiceYHE = value;
            }
        }

        /// <summary>
        /// ����������???
        /// </summary>
        public decimal? InvoiceZSE
        {
            get
            {
                return this._invoiceZSE;
            }
            set
            {
                this._invoiceZSE = value;
            }
        }

        /// <summary>
        /// �������ۿ۶�
        /// </summary>
        public decimal? InvoiceZRE
        {
            get
            {
                return this._invoiceZRE;
            }
            set
            {
                this._invoiceZRE = value;
            }
        }

        /// <summary>
        /// ������˰��
        /// </summary>
        public decimal? InvoiceTax
        {
            get
            {
                return this._invoiceTax;
            }
            set
            {
                this._invoiceTax = value;
            }
        }

        /// <summary>
        /// ������Ӧ�տ��
        /// </summary>
        public decimal? InvoiceZongJi
        {
            get
            {
                return this._invoiceZongJi;
            }
            set
            {
                this._invoiceZongJi = value;
            }
        }

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public DateTime? InvoicePayTimeLimit
        {
            get
            {
                return this._invoicePayTimeLimit;
            }
            set
            {
                DateTime? dt = value as DateTime?;
                this._invoicePayTimeLimit = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
            }
        }

        /// <summary>
        /// ������δ�տ��
        /// </summary>
        public decimal? InvoiceOwed
        {
            get
            {
                return this._invoiceOwed;
            }
            set
            {
                this._invoiceOwed = value;
            }
        }

        /// <summary>
        /// �������ܶ�
        /// </summary>
        public readonly static string PROPERTY_INVOICEHEJI = "InvoiceHeJi";

        /// <summary>
        /// �������Żݶ�
        /// </summary>
        public readonly static string PROPERTY_INVOICEYHE = "InvoiceYHE";

        /// <summary>
        /// ����������???
        /// </summary>
        public readonly static string PROPERTY_INVOICEZSE = "InvoiceZSE";

        /// <summary>
        /// �������ۿ۶�
        /// </summary>
        public readonly static string PROPERTY_INVOICEZRE = "InvoiceZRE";

        /// <summary>
        /// ������˰��
        /// </summary>
        public readonly static string PROPERTY_INVOICETAX = "InvoiceTax";

        /// <summary>
        /// ������Ӧ�տ��
        /// </summary>
        public readonly static string PROPERTY_INVOICEZONGJI = "InvoiceZongJi";

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public readonly static string PROPERTY_INVOICEPAYTIMELIMIT = "InvoicePayTimeLimit";

        /// <summary>
        /// ������δ�տ��
        /// </summary>
        public readonly static string PROPERTY_INVOICEOWED = "InvoiceOwed";


        private decimal? _InvoiceFpje;

        public decimal? InvoiceFpje
        {
            get { return _InvoiceFpje; }
            set { _InvoiceFpje = value; }        
        }
        public readonly static string PROPERTY_INVOICEFPJE = "InvoiceFpje";
    }
}
