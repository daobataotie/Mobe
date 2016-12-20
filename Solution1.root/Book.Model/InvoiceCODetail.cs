//------------------------------------------------------------------------------
//
// file name:InvoiceCODetail.cs
// author: peidun
// create date:2008/6/20 15:33:25
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 采购订单明细
    /// </summary>
    [Serializable]
    public partial class InvoiceCODetail
    {
        private bool selected;
        private string _DepotPositionId;

        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
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
        public override bool Equals(object obj)
        {
            if (obj is InvoiceCODetail)
            {
                if ((obj as InvoiceCODetail).InvoiceCODetailId == _invoiceCODetailId)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        ///货位编号
        /// </summary>
        public virtual string DepotPositionId
        {
            get
            {
                return this._DepotPositionId;
            }
            set
            {
                this._DepotPositionId = value;
            }

        }
        /// <summary>
        ///毛重
        /// </summary>
        public string GrossWeight
        {
            get
            {
                return this.Product.GrossWeight == null ? null : this.Product.GrossWeight.Value.ToString() + (this.Product.WeightUnit == null ? null : this.Product.WeightUnit.CnName);
            }
            //set
            //{
            //    this._DepotPositionId = value;
            //}

        }
        /// <summary>
        ///净重
        /// </summary>
        public string NetWeight
        {
            get
            {
                return this.Product.NetWeight == null ? null : this.Product.NetWeight.Value.ToString() + (this.Product.WeightUnit == null ? null : this.Product.WeightUnit.CnName);
            }
            //set
            //{
            //    this._DepotPositionId = value;
            //}

        }
        /// <summary>
        ///体积
        /// </summary>
        public string Bulk
        {
            get
            {
                return this.Product.Volume == null ? null : this.Product.Volume.Value.ToString() + (this.Product.VolumeUnit == null ? null : this.Product.VolumeUnit.CnName);
            }
            //set
            //{
            //    this._DepotPositionId = value;
            //}

        }
        public string InvoiceProductDescription
        {
            get { return this.Product.ProductDescription; }
        }

        public DateTime OrderDate
        {
            get
            {
                return this.Invoice.InvoiceDate.Value;
            }
        }

        /// <summary>
        /// 采购订单明细编号
        /// </summary>
        public readonly static string PROPERTY_DEPOTPOSITONID = "DepotPositionId";
        /// <summary>
        /// 毛重
        /// </summary>
        public readonly static string PRO_GrossWeight = "GrossWeight";
        /// <summary>
        /// 净重
        /// </summary>
        public readonly static string PRO_NetWeight = "NetWeight";
        /// <summary>
        /// 体积
        /// </summary>
        public readonly static string PRO_Bulk = "Bulk";
        public readonly static string PRO_OId = "OId";
        private int oId;

        public int OId
        {
            get { return oId; }
            set { oId = value; }
        }

        /// <summary>
        /// 详细价格区间
        /// </summary>
        public string DetailsPriceRange { get; set; }

        public BGHandbookDetail2 BGProduct
        {
            get;
            set;
        }
    }
}
