//------------------------------------------------------------------------------
//
// file name:Setting.cs
// author: peidun
// create date:2008/6/24 16:47:26
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 设置
    /// </summary>
    [Serializable]
    public partial class Setting
    {
        private double _Blong;

        private double _BWidth;

        private double _BHeight;

        private double _BJWeight;

        private double _BMWeight;

        private double _BCaiJi;

        private double temp;

        public double Blong
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SettingCurrentValue))
                    double.TryParse(this.SettingCurrentValue.Split(',')[0], out temp);
                return this._Blong = temp;
            }
            set { this._Blong = value; }
        }

        public double BWidth
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SettingCurrentValue))
                {
                    if (this.SettingCurrentValue.IndexOf(',') > 0)
                        double.TryParse(this.SettingCurrentValue.Split(',')[1], out temp);
                }
                return this._BWidth = temp;
            }
            set { this._BWidth = value; }
        }

        public double BHeight
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SettingCurrentValue))
                {
                    if (this.SettingCurrentValue.IndexOf(',') > 0 && this.SettingCurrentValue.Split(',')[0].Length + this.SettingCurrentValue.Split(',')[1].Length + 1 < this.SettingCurrentValue.Length)
                        double.TryParse(this.SettingCurrentValue.Split(',')[2], out temp);
                }
                return this._BHeight = temp;
            }
            set { this._BHeight = value; }
        }

        public double BJWeight
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SettingCurrentValue))
                {
                    string[] items = this.SettingCurrentValue.Split(',');
                    if (items.Length - 1 == 5)
                        double.TryParse(this.SettingCurrentValue.Split(',')[3], out temp);
                }
                return _BJWeight = temp;
            }
            set { _BJWeight = value; }
        }

        public double BMWeight
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SettingCurrentValue))
                {
                    string[] items = this.SettingCurrentValue.Split(',');
                    if (items.Length - 1 == 5)
                        double.TryParse(this.SettingCurrentValue.Split(',')[4], out temp);
                }
                return _BMWeight = temp;
            }
            set { _BMWeight = value; }
        }

        public double BCaiJi
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SettingCurrentValue))
                {
                    string[] items = this.SettingCurrentValue.Split(',');
                    if (items.Length - 1 == 5)
                        double.TryParse(this.SettingCurrentValue.Split(',')[5], out temp);
                }
                return _BCaiJi = temp;
            }
            set { _BCaiJi = value; }
        }

        public readonly static string PRO_Blong = "Blong";

        public readonly static string PRO_BWidth = "BWidth";

        public readonly static string PRO_BHeight = "BHeight";

        public readonly static string PRO_BJWeight = "BJWeight";

        public readonly static string PRO_BMWeight = "BMWeight";

        public readonly static string PRO_BCaiJi = "BCaiJi";


    }
}
