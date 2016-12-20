//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompact.cs
// author: peidun
// create date：2010-1-4 15:32:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 生产外包合同
    /// </summary>
    [Serializable]
    public partial class ProduceOtherCompact
    {
        private System.Collections.Generic.IList<ProduceOtherCompactDetail> details;

        public System.Collections.Generic.IList<ProduceOtherCompactDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
        private System.Collections.Generic.IList<ProduceOtherCompactMaterial> detailMaterial;

        public System.Collections.Generic.IList<ProduceOtherCompactMaterial> DetailMaterial
        {
            get { return detailMaterial; }
            set { detailMaterial = value; }
        }

        private System.Collections.Generic.IList<ProduceOtherCompactMaterial> tempMaterials;

        public System.Collections.Generic.IList<ProduceOtherCompactMaterial> TempMaterials
        {
            get { return tempMaterials; }
            set { tempMaterials = value; }
        }
        private bool? _checkeds;
        /// <summary>
        /// 
        /// </summary>
        public bool? Checkeds
        {
            get
            {
                return this._checkeds;
            }
            set
            {
                this._checkeds = value;
            }
        }

        #region 报表绑定虚拟
        public string NextWorkHouseName { get; set; }

        public string EmployeeName0 { get; set; }

        public string SupplierName { get; set; }

        public string RPCustomerInvoiceXOId { get; set; }

        public readonly static string PRO_NextWorkHouseName = "NextWorkHouseName";
        public readonly static string PRO_EmployeeName0 = "EmployeeName0";
        public readonly static string PRO_SupplierName = "SupplierName";
        public readonly static string PRO_RPCustomerInvoiceXOId = "RPCustomerInvoiceXOId";
        #endregion
    }
}
