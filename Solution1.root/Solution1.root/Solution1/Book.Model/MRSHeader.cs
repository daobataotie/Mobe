//------------------------------------------------------------------------------
//
// file name：MRSHeader.cs
// author: peidun
// create date：2009-12-18 11:23:48
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 物料需求计划头
    /// </summary>
    [Serializable]
    public partial class MRSHeader
    {
        private System.Collections.Generic.IList<MRSdetails> details;


        public System.Collections.Generic.IList<MRSdetails> Details
        {
            get { return details; }
            set { details = value; }
        }

        public readonly static string PROPERTY_GETSOURCETYPE = "GetSourceType";

        public string GetSourceType
        {
            get
            {
                string sourceType = string.Empty;
                switch (_sourceType == null ? "-1" : _sourceType)
                {
                    case "0":
                        sourceType = "自製";
                        break;
                    case "1":
                        sourceType = "外購";
                        break;
                    case "2":
                        sourceType = "耗用";
                        break;
                    case "3":
                        sourceType = "委外";
                        break;
                    default:
                        break;
                }
                return sourceType;
            }
        }

        

    }
}
