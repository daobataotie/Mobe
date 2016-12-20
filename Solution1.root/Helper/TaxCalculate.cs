using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public enum TaxType
    {
        /// <summary>
        /// 免税
        /// </summary>
        Free_Tax,
        /// <summary>
        /// 外加税
        /// </summary>
        Plus_Tax,
        /// <summary>
        /// 内含税
        /// </summary>
        Within_Tax
    }

    //-----------------------------------------------//
    //-包含一个税别的枚举,与一个计算各种金额的辅助类-//
    //-----------------------------------------------//
    public class TaxCalculateHelper
    {
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal UnitMoney { get; set; }

        /// <summary>
        /// 税额
        /// </summary>
        public decimal UnitTaxMoney { get; set; }

        /// <summary>
        /// 税别
        /// </summary>
        public TaxType UnitTaxType { get; set; }

        public IDictionary<TaxType, TaxCalculateHelper> TaxCalculateDictionary = new Dictionary<TaxType, TaxCalculateHelper>();

        private TaxCalculateHelper()
        { }

        /// <summary>
        /// 返回计算的IDictionary集合
        /// </summary>
        /// <param name="type">税别</param>
        /// <param name="dNewTaxRate">新税率</param>
        /// <param name="dOldTaxRate">旧税率</param>
        /// <param name="JinE">金额</param>
        /// <param name="Quantity">数量</param>
        public TaxCalculateHelper(TaxType type, double dNewTaxRate, double dOldTaxRate, decimal JinE, int Quantity)
        {
            this.TaxCalculateDictionary.Add(TaxType.Free_Tax, new TaxCalculateHelper { UnitPrice = 0, UnitMoney = 0, UnitTaxMoney = 0, UnitTaxType = TaxType.Free_Tax });
            this.TaxCalculateDictionary.Add(TaxType.Plus_Tax, new TaxCalculateHelper { UnitPrice = 0, UnitMoney = 0, UnitTaxMoney = 0, UnitTaxType = TaxType.Plus_Tax });
            this.TaxCalculateDictionary.Add(TaxType.Within_Tax, new TaxCalculateHelper { UnitPrice = 0, UnitMoney = 0, UnitTaxMoney = 0, UnitTaxType = TaxType.Within_Tax });

            decimal _TrueJinE = 0;      //真实金额,每笔详细 : 单价*数量 = 金额.需要用税率反算
            decimal _dNewTaxRate = decimal.Parse(dNewTaxRate.ToString());
            decimal _dOldTaxRate = decimal.Parse(dOldTaxRate.ToString());

            TaxCalculateHelper _Free = this.TaxCalculateDictionary[TaxType.Free_Tax];
            TaxCalculateHelper _Plus = this.TaxCalculateDictionary[TaxType.Plus_Tax];
            TaxCalculateHelper _WithIn = this.TaxCalculateDictionary[TaxType.Within_Tax];

            //首先返回到免税状态.主要是返回原始金额,下面要使用
            switch (type)
            {
                case TaxType.Free_Tax:
                case TaxType.Plus_Tax:
                    _TrueJinE = JinE;
                    break;
                case TaxType.Within_Tax:
                    _TrueJinE = DateTimeParse.GetSiSheWuRu(JinE * (1 + _dOldTaxRate / 100), 0);
                    break;
            }

            //免税
            _Free.UnitPrice = DateTimeParse.GetSiSheWuRu(_TrueJinE / Quantity, 2);
            _Free.UnitMoney = _TrueJinE;
            _Free.UnitTaxMoney = 0;
            //外加税
            _Plus.UnitPrice = DateTimeParse.GetSiSheWuRu(_TrueJinE / Quantity, 2);
            _Plus.UnitMoney = _TrueJinE;
            _Plus.UnitTaxMoney = DateTimeParse.GetSiSheWuRu(_TrueJinE * DateTimeParse.GetSiSheWuRu(_dNewTaxRate / 100, 2), 0);
            //内含税
            _WithIn.UnitMoney = DateTimeParse.GetSiSheWuRu(_TrueJinE / (1 + _dNewTaxRate / 100), 0);
            _WithIn.UnitTaxMoney = _TrueJinE - _WithIn.UnitMoney;
            _WithIn.UnitPrice = DateTimeParse.GetSiSheWuRu(_WithIn.UnitMoney / Quantity, 2);
        }
    }
}

