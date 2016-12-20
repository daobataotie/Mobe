//------------------------------------------------------------------------------
//
// file name：PCInputCheck.cs
// author: mayanjun
// create date：2015/4/18 上午 11:58:03
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 入料检验单
    /// </summary>
    [Serializable]
    public partial class PCInputCheck
    {
        public bool IsCheck { get; set; }

        public string IsJieAn
        {
            get { return (this.IsClosed == null || this.IsClosed == false) ? "結案" : "取消結案"; }
        }
    }
}