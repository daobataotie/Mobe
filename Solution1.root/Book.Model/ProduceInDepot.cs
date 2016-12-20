//------------------------------------------------------------------------------
//
// file name：ProduceInDepot.cs
// author: peidun
// create date：2010-1-8 13:43:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 生产入库
    /// </summary>
    [Serializable]
    public partial class ProduceInDepot
    {
        private System.Collections.Generic.IList<ProduceInDepotDetail> details;

        public System.Collections.Generic.IList<ProduceInDepotDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

    }
}
