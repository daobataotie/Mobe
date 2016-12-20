//------------------------------------------------------------------------------
//
// file name：OpticsTest.cs
// author: mayanjun
// create date：2012-4-21 09:55:32
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 光学制程测试
    /// </summary>
    [Serializable]
    public partial class OpticsTest
    {
        public double? OpticsTestH
        {
            get { return (RinPSM - RoutPSM) + (LinPSM - LoutPSM); }
        }

        public double? OpticsTestV
        {
            get { return (RupPSM - RdowmPSM) - (LupPSM - LdownPSM); }
        }

        public readonly static string PRO_OpticsTestH = "OpticsTestH";

        public static readonly string PRO_OpticsTestV = "OpticsTestV";
    }
}
