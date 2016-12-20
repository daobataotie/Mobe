//------------------------------------------------------------------------------
//
// file name：PCOtherCheck.cs
// author: mayanjun
// create date：2011-11-10 15:05:59
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 委外质检
    /// </summary>
    [Serializable]
    public partial class PCOtherCheck
    {
        private System.Collections.Generic.IList<Model.PCOtherCheckDetail> _Detail;

        public System.Collections.Generic.IList<Model.PCOtherCheckDetail> Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }

    }
}
