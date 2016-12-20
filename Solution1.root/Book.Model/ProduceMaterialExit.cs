//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExit.cs
// author: peidun
// create date：2010-1-6 10:20:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 生产退料
    /// </summary>
    [Serializable]
    public partial class ProduceMaterialExit
    {
        private System.Collections.Generic.IList<Model.ProduceMaterialExitDetail> details;

        public System.Collections.Generic.IList<Model.ProduceMaterialExitDetail> Detail
        {
            get { return details; }
            set { details = value; }
        }

        public Model.PronoteHeader MPronoteHeader { get; set; }


    }
}
