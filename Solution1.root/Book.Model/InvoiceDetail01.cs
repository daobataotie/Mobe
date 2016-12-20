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
        /// 出货货品编号
        /// </summary>
        private string _invoiceDetailId;

        /// <summary>
        /// 商品编号
        /// </summary>
        private string _productId;

        /// <summary>
        /// 单据编号
        /// </summary>
        private string _invoiceId;

        /// <summary>
        /// 出货货???数量
        /// </summary>
        private double? _invoiceDetailQuantity;

        /// <summary>
        /// 出货货品单价
        /// </summary>
        private decimal? _invoiceDetailPrice;

        /// <summary>
        /// 出货退货单货品折扣率，10表示打9折
        /// </summary>
        private double? _invoiceDetailDiscountRate;

        /// <summary>
        /// 出货货品折扣额
        /// </summary>
        private decimal? _invoiceDetailDiscount;

        /// <summary>
        /// 出货货品税率
        /// </summary>
        private double? _invoiceDetailTaxRate;

        /// <summary>
        /// 出货货品税额
        /// </summary>
        private decimal? _invoiceDetailTax;

        /// <summary>
        /// 出货货品应收款
        /// </summary>
        private decimal? _invoiceDetailMoney0;

        /// <summary>
        /// 出货货品备注
        /// </summary>
        private string _invoiceDetailNote;

        /// <summary>
        /// 出货货品赠送否
        /// </summary>
        private bool? _invoiceDetailZS;

        /// <summary>
        /// 出货货品金额
        /// </summary>
        private decimal? _invoiceDetailMoney1;

        /// <summary>
        /// 出货货品成本价
        /// </summary>
        private decimal? _invoiceDetailCostPrice;

        /// <summary>
        /// 出货成本金额
        /// </summary>
        private decimal? _invoiceDetailCostMoney;

        /// <summary>
        /// 商品
        /// </summary>
        private Product product;

        #endregion

        #region Properties

        /// <summary>
        /// 出货货品编号
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
        /// 商品编号
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
        /// 出货货???数量
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
        /// 出货货品单价
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
        /// 出货退货单货品折扣率，10表示打9折
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
        /// 出货货品折扣额
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
        /// 出货货品税率
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
        /// 出货货品税额
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
        /// 出货货品应收款
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
        /// 出货货品备注
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
        /// 出货货品赠送否
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
        /// 出货货品金额
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
        /// 出货货品成本价
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
        /// 出货成本金额
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
        /// 商品
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
                        return "銷售";
                        //return Properties.Resource.XS;
                    case "xt":
                        return "銷退";
                        //return Properties.Resource.XT;
                    case "cg":
                        return "採購";
                        //return Properties.Resource.CG;
                    case "ct":
                        return "採退";
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
        /// 已收
        /// </summary>
        public decimal? YiShou
        {
            get
            {
                return this.InvoiceZongJi - this.InvoiceOwed;
            }
        }
        /// <summary>
        /// 已收
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
        /// 出货货品编号
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILID = "InvoiceDetailId";

        /// <summary>
        /// 商品编号
        /// </summary>
        public readonly static string PROPERTY_PRODUCTID = "ProductId";

        /// <summary>
        /// 单据编号
        /// </summary>
        public readonly static string PROPERTY_INVOICEID = "InvoiceId";

        /// <summary>
        /// 出货货???数量
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILQUANTITY = "InvoiceDetailQuantity";

        /// <summary>
        /// 出货货品单价
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILPRICE = "InvoiceDetailPrice";

        /// <summary>
        /// 出货退货单货品折扣率，10表示打9折
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILDISCOUNTRATE = "InvoiceDetailDiscountRate";

        /// <summary>
        /// 出货货品折扣额
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILDISCOUNT = "InvoiceDetailDiscount";

        /// <summary>
        /// 出货货品税率
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILTAXRATE = "InvoiceDetailTaxRate";

        /// <summary>
        /// 出货货品税额
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILTAX = "InvoiceDetailTax";

        /// <summary>
        /// 出货货品应收款
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILMONEY0 = "InvoiceDetailMoney0";

        /// <summary>
        /// 出货货品备注
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILNOTE = "InvoiceDetailNote";

        /// <summary>
        /// 出货货品赠送否
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILZS = "InvoiceDetailZS";

        /// <summary>
        /// 出货货品金额
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILMONEY1 = "InvoiceDetailMoney1";

        /// <summary>
        /// 出货货品成本价
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILCOSTPRICE = "InvoiceDetailCostPrice";

        /// <summary>
        /// 出货成本金额
        /// </summary>
        public readonly static string PROPERTY_INVOICEDETAILCOSTMONEY = "InvoiceDetailCostMoney";


        #endregion



        /// <summary>
        /// 出货单总额
        /// </summary>
        private decimal? _invoiceHeJi;

        /// <summary>
        /// 出货单优惠额
        /// </summary>
        private decimal? _invoiceYHE;

        /// <summary>
        /// 出货单赠送???
        /// </summary>
        private decimal? _invoiceZSE;

        /// <summary>
        /// 出货单折扣额
        /// </summary>
        private decimal? _invoiceZRE;

        /// <summary>
        /// 出货单税额
        /// </summary>
        private decimal? _invoiceTax;

        /// <summary>
        /// 出货单应收款额
        /// </summary>
        private decimal? _invoiceZongJi;

        /// <summary>
        /// 发票日期
        /// </summary>
        private DateTime? _invoicePayTimeLimit;

        /// <summary>
        /// 出货单未收款额
        /// </summary>
        private decimal? _invoiceOwed;

        /// <summary>
        /// 出货单总额
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
        /// 出货单优惠额
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
        /// 出货单赠送???
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
        /// 出货单折扣额
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
        /// 出货单税额
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
        /// 出货单应收款额
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
        /// 发票日期
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
        /// 出货单未收款额
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
        /// 出货单总额
        /// </summary>
        public readonly static string PROPERTY_INVOICEHEJI = "InvoiceHeJi";

        /// <summary>
        /// 出货单优惠额
        /// </summary>
        public readonly static string PROPERTY_INVOICEYHE = "InvoiceYHE";

        /// <summary>
        /// 出货单赠送???
        /// </summary>
        public readonly static string PROPERTY_INVOICEZSE = "InvoiceZSE";

        /// <summary>
        /// 出货单折扣额
        /// </summary>
        public readonly static string PROPERTY_INVOICEZRE = "InvoiceZRE";

        /// <summary>
        /// 出货单税额
        /// </summary>
        public readonly static string PROPERTY_INVOICETAX = "InvoiceTax";

        /// <summary>
        /// 出货单应收款额
        /// </summary>
        public readonly static string PROPERTY_INVOICEZONGJI = "InvoiceZongJi";

        /// <summary>
        /// 发票日期
        /// </summary>
        public readonly static string PROPERTY_INVOICEPAYTIMELIMIT = "InvoicePayTimeLimit";

        /// <summary>
        /// 出货单未收款额
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
