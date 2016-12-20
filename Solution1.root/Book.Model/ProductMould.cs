//------------------------------------------------------------------------------
//
// file name：ProductMould.cs
// author: peidun
// create date：2009-07-24 12:15:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 产品模具
    /// </summary>
    [Serializable]
    public partial class ProductMould
    {

        public override string ToString()
        {
            return this._mouldName;
        }

        private IList<Model.MouldAttachment> details = new List<Model.MouldAttachment>();

        public IList<Model.MouldAttachment> Details
        {
            get { return details; }
            set { details = value; }
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        public string Upload
        {
            get;
            set;
        }
    }
}
