//------------------------------------------------------------------------------
//
// file name：PCImpactCheckDetail.cs
// author: mayanjun
// create date：2011-11-15 14:09:35
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 冲击测试表详细
    /// </summary>
    [Serializable]
    public partial class PCImpactCheckDetail
    {
        /// <summary>
        /// 镜片(上)左
        /// </summary>
        private string _attrGlassUpLDis;

        public string AttrGlassUpLDis
        {
            get { return ValueToDisplay(_attrGlassUpL); }
        }

        public readonly static string PRO_AttrGlassUpLDis = "AttrGlassUpLDis";

        /// <summary>
        /// 镜片(上)右
        /// </summary>
        private string _attrGlassUpRDis;

        public string AttrGlassUpRDis
        {
            get { return ValueToDisplay(_attrGlassUpR); }
        }

        public readonly static string PRO_AttrGlassUpRDis = "AttrGlassUpRDis";

        /// <summary>
        /// 镜片(下)左
        /// </summary>
        private string _attrGlassDownLDis;

        public string AttrGlassDownLDis
        {
            get { return ValueToDisplay(_attrGlassDownL); }
        }

        public readonly static string PRO_AttrGlassDownLDis = "AttrGlassDownLDis";

        /// <summary>
        /// 镜片(下)右
        /// </summary>
        private string _attrGlassDownRDis;

        public string AttrGlassDownRDis
        {
            get { return ValueToDisplay(_attrGlassDownR); }
        }

        public readonly static string PRO_AttrGlassDownRDis = "AttrGlassDownRDis";

        /// <summary>
        /// 镜片(左)左
        /// </summary>
        private string _attrGlassLeftLDis;

        public string AttrGlassLeftLDis
        {
            get { return ValueToDisplay(_attrGlassLeftL); }
        }

        public readonly static string PRO_AttrGlassLeftLDis = "AttrGlassLeftLDis";

        /// <summary>
        /// 镜片(左)右
        /// </summary>
        private string _attrGlassLeftRDis;

        public string AttrGlassLeftRDis
        {
            get { return ValueToDisplay(_attrGlassLeftR); }
        }

        public readonly static string PRO_AttrGlassLeftRDis = "AttrGlassLeftRDis";

        /// <summary>
        /// 镜片(右)左
        /// </summary>
        private string _attrGlassRightLDis;

        public string AttrGlassRightLDis
        {
            get { return ValueToDisplay(_attrGlassRightL); }
        }

        public readonly static string PRO_AttrGlassRightLDis = "AttrGlassRightLDis";

        /// <summary>
        /// 镜片(右)右
        /// </summary>
        private string _attrGlassRightRDis;

        public string AttrGlassRightRDis
        {
            get { return ValueToDisplay(_attrGlassRightR); }
        }

        public readonly static string PRO_AttrGlassRightRDis = "AttrGlassRightRDis";

        /// <summary>
        /// 中央(左)
        /// </summary>
        private string _attrCentralLDis;

        public string AttrCentralLDis
        {
            get { return ValueToDisplay(_attrCentralL); }
        }

        public readonly static string PRO_AttrCentralLDis = "AttrCentralLDis";

        /// <summary>
        /// 中央(右)
        /// </summary>
        private string _attrCentralRDis;

        public string AttrCentralRDis
        {
            get { return ValueToDisplay(_attrCentralR); }
        }

        public readonly static string PRO_AttrCentralRDis = "AttrCentralRDis";

        /// <summary>
        /// 鼻中(左)
        /// </summary>
        private string _attrNoseCentralDis;

        public string AttrNoseCentralDis
        {
            get { return ValueToDisplay(_attrNoseCentral); }
        }

        public readonly static string PRO_AttrNoseCentralDis = "AttrNoseCentralDis";

        /// <summary>
        /// 灌嘴(左)
        /// </summary>
        private string _attrGuanZuiDis;

        public string AttrGuanZuiDis
        {
            get { return ValueToDisplay(_attrGuanZui); }
        }

        public readonly static string PRO_AttrGuanZuiDis = "AttrGuanZuiDis";

        /// <summary>
        /// 接痕(左)
        /// </summary>
        private string _attrJieHenLDis;

        public string AttrJieHenLDis
        {
            get { return ValueToDisplay(_attrJieHenL); }
        }

        public readonly static string PRO_AttrJieHenLDis = "AttrJieHenLDis";

        /// <summary>
        /// 接痕(右)
        /// </summary>
        private string _attrJieHenRDis;

        public string AttrJieHenRDis
        {
            get { return ValueToDisplay(_attrJieHenR); }
        }

        public readonly static string PRO_AttrJieHenRDis = "AttrJieHenRDis";

        /// <summary>
        /// 翅膀(左)
        /// </summary>
        private string _attrWingLDis;

        public string AttrWingLDis
        {
            get { return ValueToDisplay(_attrWingL); }
        }

        public readonly static string PRO_AttrWingLDis = "AttrWingLDis";

        /// <summary>
        /// 翅膀(右)
        /// </summary>
        private string _attrWingRDis;

        public string AttrWingRDis
        {
            get { return ValueToDisplay(_attrWingR); }
        }

        public readonly static string PRO_AttrWingRDis = "AttrWingRDis";

        /// <summary>
        /// 标准-15(左)
        /// </summary>
        private string _attr_15LDis;

        public string Attr_15LDis
        {
            get { return ValueToDisplay(_attr_15L); }
        }

        public readonly static string PRO_Attr_15LDis = "Attr_15LDis";

        /// <summary>
        /// 标准-15(右)
        /// </summary>
        private string _attr_15RDis;

        public string Attr_15RDis
        {
            get { return ValueToDisplay(_attr_15R); }
        }

        public readonly static string PRO_Attr_15RDis = "Attr_15RDis";

        /// <summary>
        /// 标准0(左)
        /// </summary>
        private string _attr0LDis;

        public string Attr0LDis
        {
            get { return ValueToDisplay(_attr0L); }
        }

        public readonly static string PRO_Attr0LDis = "Attr0LDis";

        /// <summary>
        /// 标准0(右)
        /// </summary>
        private string _attr0RDis;

        public string Attr0RDis
        {
            get { return ValueToDisplay(_attr0R); }
        }

        public readonly static string PRO_Attr0RDis = "Attr0RDis";

        /// <summary>
        /// 标准15(左)
        /// </summary>
        private string _attr15LDis;

        public string Attr15LDis
        {
            get { return ValueToDisplay(_attr15L); }
        }

        public readonly static string PRO_Attr15LDis = "Attr15LDis";

        /// <summary>
        /// 标准15(右)
        /// </summary>
        private string _attr15RDis;

        public string Attr15RDis
        {
            get { return ValueToDisplay(_attr15R); }
        }

        public readonly static string PRO_Attr15RDis = "Attr15RDis";

        /// <summary>
        /// 标准30(左)
        /// </summary>
        private string _attr30LDis;

        public string Attr30LDis
        {
            get { return ValueToDisplay(_attr30L); }
        }

        public readonly static string PRO_Attr30LDis = "Attr30LDis";

        /// <summary>
        /// 标准30(右)
        /// </summary>
        private string _attr30RDis;

        public string Attr30RDis
        {
            get { return ValueToDisplay(_attr30R); }
        }

        public readonly static string PRO_Attr30RDis = "Attr30RDis";

        /// <summary>
        /// 标准45(左)
        /// </summary>
        private string _attr45LDis;

        public string Attr45LDis
        {
            get { return ValueToDisplay(_attr45L); }
        }

        public readonly static string PRO_Attr45LDis = "Attr45LDis";

        /// <summary>
        /// 标准45(右)
        /// </summary>
        private string _attr45RDis;

        public string Attr45RDis
        {
            get { return ValueToDisplay(_attr45R); }
        }

        public readonly static string PRO_Attr45RDis = "Attr45RDis";

        /// <summary>
        /// 标准60(左)
        /// </summary>
        private string _attr60LDis;

        public string Attr60LDis
        {
            get { return ValueToDisplay(_attr60L); }
        }

        public readonly static string PRO_Attr60LDis = "Attr60LDis";

        /// <summary>
        /// 标准60(右)
        /// </summary>
        private string _attr60RDis;

        public string Attr60RDis
        {
            get { return ValueToDisplay(_attr60R); }
        }

        public readonly static string PRO_Attr60RDis = "Attr60RDis";

        /// <summary>
        /// 标准75(左)
        /// </summary>
        private string _attr75LDis;

        public string Attr75LDis
        {
            get { return ValueToDisplay(_attr75L); }
        }

        public readonly static string PRO_Attr75LDis = "Attr75LDis";

        /// <summary>
        /// 标准75(右)
        /// </summary>
        private string _attr75RDis;

        public string Attr75RDis
        {
            get { return ValueToDisplay(_attr75R); }
        }

        public readonly static string PRO_Attr75RDis = "Attr75RDis";

        /// <summary>
        /// 标准90(左)
        /// </summary>
        private string _attr90LDis;

        public string Attr90LDis
        {
            get { return ValueToDisplay(_attr90L); }
        }

        public readonly static string PRO_Attr90LDis = "Attr90LDis";

        /// <summary>
        /// 标准90(右)
        /// </summary>
        private string _attr90RDis;

        public string Attr90RDis
        {
            get { return ValueToDisplay(_attr90R); }
        }

        public readonly static string PRO_Attr90RDis = "Attr90RDis";


        private string _attrFootLDis;

        public string AttrFootLDis
        {
            get { return ValueToDisplay(_attrFootL); }
        }

        public readonly static string PRO_AttrFootLDis = "AttrFootLDis";

        private string _attrFootRDis;

        public string AttrFootRDis
        {
            get { return ValueToDisplay(_attrFootR); }
        }

        public readonly static string PRO_AttrFootRDis = "AttrFootRDis";

        public string ValueToDisplay(string v)
        {
            string str = string.Empty;
            switch (v)
            {
                case "0":
                    str = "O";
                    break;
                case "1":
                    str = "△";
                    break;
                case "2":
                    str = "X";
                    break;
                default:
                    break;
            }
            return str;
        }
    }
}
