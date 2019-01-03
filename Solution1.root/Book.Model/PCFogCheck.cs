//------------------------------------------------------------------------------
//
// file name：PCFogCheck.cs
// author: mayanjun
// create date：2012-3-16 17:42:23
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 雾度测试
    /// </summary>
    [Serializable]
    public partial class PCFogCheck
    {
        public System.Collections.Generic.IList<Model.PCFogCheckDetail> Details { get; set; }

        public bool IsChecked { get; set; }
    }
}
