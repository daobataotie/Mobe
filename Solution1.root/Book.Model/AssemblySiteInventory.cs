//------------------------------------------------------------------------------
//
// file name：AssemblySiteInventory.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 组装现场盘点录入
    /// </summary>
    [Serializable]
    public partial class AssemblySiteInventory
    {
        System.Collections.Generic.IList<Model.AssemblySiteInventoryDetail> _details = new System.Collections.Generic.List<Model.AssemblySiteInventoryDetail>();

        public System.Collections.Generic.IList<Model.AssemblySiteInventoryDetail> Details
        {
            get { return _details; }
            set { _details = value; }
        }
    }
}