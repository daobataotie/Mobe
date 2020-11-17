//------------------------------------------------------------------------------
//
// file name:InvoiceCGDetail.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 进货单货品
    /// </summary>
    [Serializable]
    public partial class InvoiceCGDetail
    {
        public override bool Equals(object obj)
        {
            if (obj is InvoiceCGDetail)
            {
                if ((obj as InvoiceCGDetail).InvoiceCGDetailId == this._invoiceCGDetailId)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
                }
                return "";
            }
            set
            {
                this.kind = value;
            }
        }

        public readonly static string PROPERTY_KIND = "Kind";

        private DateTime? _invoiceDate;

        public DateTime? InvoiceDate
        {
            get { return _invoiceDate; }
            set
            {
                DateTime? dt = value as DateTime?;
                this._invoiceDate = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
            }
        }

        public readonly static string PROPERTY_INVOICEDATE = "InvoiceDate";

        private DateTime? _invoicePayTimeLimit;

        public DateTime? InvoicePayTimeLimit
        {
            get { return _invoicePayTimeLimit; }
            set
            {
                DateTime? dt = value as DateTime?;
                this._invoicePayTimeLimit = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
            }
        }

        public readonly static string PROPERTY_INVOICEPAYTIMELIMIT = "InvoicePayTimeLimit";

        private Model.Depot _depot;

        /// <summary>
        /// 所属库房
        /// </summary>
        public Model.Depot Depot
        {
            get
            {
                return this.DepotPosition == null ? _depot : this.DepotPosition.Depot;
            }
            set
            {
                _depot = value;
            }
        }

        private double? _orderQuantity;

        public double? OrderQuantity
        {
            get
            {
                if (this.InvoiceCODetail == null)
                    return null;
                else
                    return this.InvoiceCODetail.OrderQuantity == null ? 0 : this.InvoiceCODetail.OrderQuantity;
            }
            set
            {
                _orderQuantity = value;
            }
        }

        private double? _noArrivalQuantity;

        public double? NoArrivalQuantity
        {
            get
            {
                return this.InvoiceCODetail == null ? null : this.InvoiceCODetail.NoArrivalQuantity;
            }
            set
            {
                _noArrivalQuantity = value;
            }
        }
        private double? _cTquantity;

        public double? CTquantity
        {
            get { return this.InvoiceCODetail == null ? null : this.InvoiceCODetail.InvoiceCTQuantity; }
            set { _cTquantity = value; }
        }

        private string _coDetailID;

        public string CODetailID
        {
            get
            {
                return this.InvoiceCODetail == null ? null : this.InvoiceCODetail.InvoiceCODetailId;
            }
            set
            {
                _coDetailID = value;
            }
        }

        private decimal? _coPrice;

        public decimal? CoPrice
        {
            get
            {
                return _coPrice;
            }
            set
            {
                _coPrice = value;
            }
        }

        private string _coInvoinceID;

        public string COinvoinceID
        {
            get
            {
                return this.InvoiceCODetail == null ? null : this.InvoiceCODetail.InvoiceId;
            }
            set
            {
                _coInvoinceID = value;
            }
        }

        public double? xiaoji
        {
            get
            {
                if (this.InvoiceCODetail == null)
                    return null;
                else
                    return Convert.ToDouble(this.InvoiceCODetail.InvoiceCODetailPrice) * this.InvoiceCGDetailQuantity;
            }
        }

        private bool _Checked;

        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
            }
        }

        ///// <summary>
        ///// 进库单头 批号
        ///// </summary>
        //public string SupplierLotNumber
        //{ get { return (this.InvoiceCODetail != null && this.InvoiceCODetail.Invoice != null) ? this.InvoiceCODetail.Invoice.SupplierLotNumber : string.Empty; } }


        #region 2013年3月28日 Creator:CN Desc: 增加价格区间以做新增时计算单价，修改时为空,单价依照新增
        /// <summary>
        /// 详细价格区间
        /// </summary>
        public string DetailsPriceRange { get; set; }
        #endregion

    }
}
