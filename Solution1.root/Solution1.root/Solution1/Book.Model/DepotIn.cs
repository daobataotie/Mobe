//------------------------------------------------------------------------------
//
// file name：DepotIn.cs
// author: mayanjun
// create date：2010-10-25 16:14:52
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 其他入库
    /// </summary>
    [Serializable]
    public partial class DepotIn
    {
        private IList<Model.DepotInDetail> details = new List<Model.DepotInDetail>();

        public IList<Model.DepotInDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
    }
}
