//------------------------------------------------------------------------------
//
// file name：PCPGOnlineCheck.cs
// author: mayanjun
// create date：2011-12-6 14:19:09
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 品管线上检查表
    /// </summary>
    [Serializable]
    public partial class PCPGOnlineCheck
    {
        private System.Collections.Generic.IList<Model.PCPGOnlineCheckDetail> details = new System.Collections.Generic.List<Model.PCPGOnlineCheckDetail>();
        public System.Collections.Generic.IList<Model.PCPGOnlineCheckDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private string descTime;

        public string DescTime
        {
            get { return descTime; }
            set { descTime = value; }
        }

        public string EmployeeName { get; set; }

        public string ProductName { get; set; }

        public string CustomerShortName { get; set; }
    }
}
