//------------------------------------------------------------------------------
//
// file name：PCFlameRetardantDetail.cs
// author: mayanjun
// create date：2018/12/27 13:17:16
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 阻燃测试详细
    /// </summary>
    [Serializable]
    public partial class PCFlameRetardantDetail
    {
        public string ProductName { get; set; }

        public string EmployeeName { get; set; }

        public string CustomerInvoiceXOId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public bool IsChecked { get; set; }
    }
}