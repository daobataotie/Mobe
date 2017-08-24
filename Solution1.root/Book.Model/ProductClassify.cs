//------------------------------------------------------------------------------
//
// file name：ProductClassify.cs
// author: mayanjun
// create date：2017-08-24 21:36:04
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ProductClassify
    {
        System.Collections.Generic.IList<Model.ProductClassifyDetail> details = new System.Collections.Generic.List<Model.ProductClassifyDetail>();

        public System.Collections.Generic.IList<Model.ProductClassifyDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
    }
}