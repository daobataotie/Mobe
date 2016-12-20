//------------------------------------------------------------------------------
//
// file name：ThicknessTest.cs
// author: mayanjun
// create date：2012-4-24 10:33:15
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 厚度测试
    /// </summary>
    [Serializable]
    public partial class ThicknessTest
    {
        public IList<Model.ThicknessTestDetails> Details { get; set; }
    }
}
