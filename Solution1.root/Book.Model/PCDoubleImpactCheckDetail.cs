//------------------------------------------------------------------------------
//
// file name：PCDoubleImpactCheckDetail.cs
// author: mayanjun
// create date：2011-11-24 17:38:50
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// ANSI~CSA/H.M冲击测试单详细
    /// </summary>
    [Serializable]
    public partial class PCDoubleImpactCheckDetail
    {
        private string _attrHotL;

        public string attrHotL
        {
            get { return _attrHotL; }
            set { _attrHotL = value; }
        }

        public readonly static string PRO_attrHotL = "attrHotL";

        private string _attrHotR;

        public string attrHotR
        {
            get { return _attrHotR; }
            set { _attrHotR = value; }
        }

        public readonly static string PRO_attrHotR = "attrHotR";

        private string _attrCoolL;

        public string attrCoolL
        {
            get { return _attrCoolL; }
            set { _attrCoolL = value; }
        }

        public readonly static string PRO_attrCoolL = "attrCoolL";

        private string _attrCoolR;

        public string attrCoolR
        {
            get { return _attrCoolR; }
            set { _attrCoolR = value; }
        }

        public readonly static string PRO_attrCoolR = "attrCoolR";
    }
}
