//------------------------------------------------------------------------------
//
// file name：AtAccountSubject.cs
// author: mayanjun
// create date：2010-11-10 11:04:52
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class AtAccountSubject
    {
        public override string ToString()
        {
            return this._subjectName;
        }

        /// <summary>
        /// 总分类账计算余额,根据期初设定余额加减计算
        /// </summary>
        private decimal? _ZFLZ_Yue;

        public decimal? ZFLZ_Yue
        {
            get { return _ZFLZ_Yue.HasValue ? _ZFLZ_Yue.Value : 0; }
            set { _ZFLZ_Yue = value; }
        }

        /// <summary>
        /// 总分类账-借方金额统计
        /// </summary>
        private decimal? _ZFLZ_Jie;

        public decimal? ZFLZ_Jie
        {
            get { return _ZFLZ_Jie.HasValue ? _ZFLZ_Jie.Value : 0; }
            set { _ZFLZ_Jie = value; }
        }

        /// <summary>
        /// 总分类账-贷方金额统计
        /// </summary>
        private decimal? _ZFLZ_Dai;

        public decimal? ZFLZ_Dai
        {
            get { return _ZFLZ_Dai.HasValue ? _ZFLZ_Dai.Value : 0; }
            set { _ZFLZ_Dai = value; }
        }
    }

}
