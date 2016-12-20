//------------------------------------------------------------------------------
//
// file name：InvoiceXJProcess.cs
// author: mayanjun
// create date：2010-8-25 16:07:44
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 销售报价单货品加工
    /// </summary>
    [Serializable]
    public partial class InvoiceXJProcess
    {
        public string PriceAndRange { get; set; }

        public bool IsChecked { get; set; }
    }
}
