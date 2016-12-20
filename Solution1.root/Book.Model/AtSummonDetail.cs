//------------------------------------------------------------------------------
//
// file name：AtSummonDetail.cs
// author: mayanjun
// create date：2010-11-24 09:40:44
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 传票明细
    /// </summary>
    [Serializable]
    public partial class AtSummonDetail
    {
        private decimal? e;

        public decimal? E
        {
            get { return e; }
            set { e = value; }
        }
        private decimal? f;

        public decimal? F
        {
            get { return f; }
            set { f = value; }
        }
        private decimal? g;

        public decimal? G
        {
            get { return g; }
            set { g = value; }
        }


        private string a;

        public string A
        {
            get { return a; }
            set { a = value; }
        }
        private string b;

        public string B
        {
            get { return b; }
            set { b = value; }
        }
        private decimal? c;

        public decimal? C
        {
            get { return c; }
            set { c = value; }
        }
        private decimal? d;

        public decimal? D
        {
            get { return d; }
            set { d = value; }
        }
        private int j;

        public int J
        {
            get { return j; }
            set { j = value; }
        }
        private int k;

        public int K
        {
            get { return k; }
            set { k = value; }
        }

        /// <summary>
        /// 零时计算余额 科目期初金额 ± 借/贷金额
        /// </summary>
        public decimal? YuE_flat { get; set; }

        /// <summary>
        /// 零时计算余额 科目期初金额 ± 借/贷金额
        /// </summary>
        public readonly static string PRO_YuE_flat = "YuE_flat";
    }
}
