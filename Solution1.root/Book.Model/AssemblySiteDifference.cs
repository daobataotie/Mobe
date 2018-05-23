//------------------------------------------------------------------------------
//
// file name：AssemblySiteDifference.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 组装现场盘点差异
    /// </summary>
    [Serializable]
    public partial class AssemblySiteDifference
    {
        IList<Model.AssemblySiteDifferenceDetai> _details = new List<Model.AssemblySiteDifferenceDetai>();

        public IList<Model.AssemblySiteDifferenceDetai> Details
        {
            get { return _details; }
            set { _details = value; }
        }
    }
}