//------------------------------------------------------------------------------
//
// file name：AcOtherShouldCollection.cs
// author: mayanjun
// create date：2011-6-10 11:19:27
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 其他应收款
    /// </summary>
    [Serializable]
    public partial class AcOtherShouldCollection
    {
        private System.Collections.Generic.IList<AcOtherShouldCollectionDetail> details;

        public System.Collections.Generic.IList<AcOtherShouldCollectionDetail> Details
        {
            get { return details; }
            set { details = value; }
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
    }
}
