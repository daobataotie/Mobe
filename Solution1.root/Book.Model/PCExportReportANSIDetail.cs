//------------------------------------------------------------------------------
//
// file name：PCExportReportANSIDetail.cs
// author: mayanjun
// create date：2012-6-13 14:02:27
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
namespace Book.Model
{
    /// <summary>
    /// ANSI外销报告详细
    /// </summary>
    [Serializable]
    public partial class PCExportReportANSIDetail
    {
        /// <summary>
        /// 附件列表
        /// </summary>
        public string accessoriesList { get; set; }

        /// <summary>
        /// 未通过数量
        /// </summary>
        public int? UnPassSum
        {
            get
            {
                return this.HasCheckSum - this.PassSum;
            }
        }

        /// <summary>
        /// 单据类型显示汉字
        /// </summary>
        public string FormPCText { get; set; }

        public string Type
        {
            get
            {
                return PCExportReportANSIDetail.htFormName[this.FromPC].ToString();
            }
        }

        public static readonly Hashtable htFormName = new Hashtable();

        static PCExportReportANSIDetail()
        {
            htFormName.Add("MSJY", "目視檢驗");
            htFormName.Add("QXD", "清晰度");
            htFormName.Add("LJDS", "棱鏡度數&&棱鏡平衡度數");
            htFormName.Add("KJGTSL", "可見光透視率");
            htFormName.Add("ZWXTSL", "紫外線透視率");
            htFormName.Add("QMDS", "球面度數");
            htFormName.Add("SGDS", "散光度數");
            htFormName.Add("GSCJCS", "高速衝擊測試");
            htFormName.Add("YZZLZJCS", "圓錐墜落撞擊測試");
            htFormName.Add("JPCTCS", "鏡片穿透測試");
            htFormName.Add("WDCS", "霧度測試");
            htFormName.Add("NRXCS", "耐燃性測試");
            htFormName.Add("CSAGX", "屈光/光學");
            htFormName.Add("CSAPGPCL", "偏光偏差量");
            htFormName.Add("CSAQXD", "清晰度");
            htFormName.Add("CSAWDCS", "霧度測試");
            htFormName.Add("CSAKJGTSL", "可見光透視率");
            htFormName.Add("CSAGSCJCS", "高速衝擊測試");
            htFormName.Add("CEENCONSTRUCTION", "结构");
            htFormName.Add("CEENQMDS", "球面度數");
            htFormName.Add("CEENSGDS", "散光度數");
            htFormName.Add("CEENLJDS", "棱鏡度數");
            htFormName.Add("CEENZB", "座標");
            htFormName.Add("CEENTSL", "透視率");
            htFormName.Add("CEENBMPZ", "表面品質");
            htFormName.Add("CEENZSCJ", "直式衝擊");
            htFormName.Add("CEENGSCJ", "高速衝擊");
            htFormName.Add("CEENJH", "記號");
            htFormName.Add("CEENZX", "資訊");
            htFormName.Add("CEENUVCF", "UV成份");
            htFormName.Add("ASNL", "年輪");
            htFormName.Add("ASCCSL", "抽測數量");
            htFormName.Add("ASWGCS", "外觀測試");
            htFormName.Add("ASJRWDX", "加熱穩定性");
            htFormName.Add("ASZB", "座標");
            htFormName.Add("ASQMDSZSD", "球面度數折射度");
            htFormName.Add("ASWD", "霧度");
            htFormName.Add("ASZSCJSL", "中速衝擊數率");
            htFormName.Add("ASGSCJSL", "高速衝擊數率");
            htFormName.Add("ASTGCJSL", "特高速衝擊數率");
            htFormName.Add("ASCTCS", "穿透測試");
            htFormName.Add("ASNRX", "耐燃性");
            htFormName.Add("ASNSX", "耐蝕性");
            htFormName.Add("ASJH", "記號");
            htFormName.Add("JISJPWG", "鏡片外觀");
            htFormName.Add("JISJPLJD", "鏡片棱鏡度");
            htFormName.Add("JISJPQGD", "鏡片曲光度");
            htFormName.Add("JISJPSGD", "鏡片散光度");
            htFormName.Add("JISJPTGL", "鏡片透光率");
            htFormName.Add("JISJPNCCX", "鏡片耐衝擊性");
            htFormName.Add("JISJPBMNMHDK", "鏡片表面耐磨耗抵抗");
            htFormName.Add("JISJPNREX", "鏡片耐熱性");
            htFormName.Add("JISJPNSX", "鏡片耐蝕性");
            htFormName.Add("JISJPNRAX", "鏡片耐燃性");
            htFormName.Add("JISWCPWG", "完成品外觀");
            htFormName.Add("JISWCPNCCX", "完成品耐衝擊性");
            htFormName.Add("JISWCPJMXDYSY", "完成品緊密性第一試驗鏡腳拉伸");
            htFormName.Add("JISWCPJMXDESY", "完成品緊密性第二試驗中樑吊掛測試機");
            htFormName.Add("JISWCPTDQD", "完成品頭帶強度");
            htFormName.Add("JISWCPNXDX", "完成品耐消毒性");
            htFormName.Add("JISWCPGZ", "完成品構造");
            htFormName.Add("JISWCPCL", "完成品材料");
            htFormName.Add("JISWCPJHBS", "完成品記號標示");
            htFormName.Add("JISWCPBZSJH", "完成品包裝上記號");
            htFormName.Add("JISWCPSYSC", "完成品使用手冊");
        }

        public readonly static string PRO_Type = "Type";

        #region 2012年6月19日15:52:23 PCExportReportANSIDetail外销报告添加后两列字段

        public int pMSJY { get; set; }

        public int qQXD { get; set; }

        public int pQXD { get; set; }

        public int qLJPHDS { get; set; }

        public int pLJPHDS { get; set; }

        public int qKJGTSL { get; set; }

        public int pKJGTSL { get; set; }

        public int qZWXTSL { get; set; }

        public int pZWXTSL { get; set; }

        public int qQMDS { get; set; }

        public int pQMDS { get; set; }

        public int qSGDS { get; set; }

        public int pSGDS { get; set; }

        public int qGSCJCS { get; set; }

        public int pGSCJCS { get; set; }

        public int qYZZLZJCS { get; set; }

        public int pYZZLZJCS { get; set; }

        public int qJPCTSC { get; set; }

        public int pJPCTSC { get; set; }

        public int qWDCS { get; set; }

        public int pWDCS { get; set; }

        public int qNRXCS { get; set; }

        public int pNRXCS { get; set; }

        //2012年8月10日18:02:02  新加字段

        public int qCSAQXD { get; set; }

        public int pCSAQXD { get; set; }

        public int qCSAWDCS { get; set; }

        public int pCSAWDCS { get; set; }

        public int qCSAKJGTSL { get; set; }

        public int pCSAKJGTSL { get; set; }

        public int pCSAGX { get; set; }

        public int qCSAGX { get; set; }

        public int pCSAPGPCL { get; set; }

        public int qCSAPGPCL { get; set; }

        public int qCSAGSCJCS { get; set; }

        public int pCSAGSCJCS { get; set; }

        //2012年8月16日10:34:25 新增字段

        public int qCEENCONSTRUCTION { get; set; }

        public int pCEENCONSTRUCTION { get; set; }

        public int qCEENQMDS { get; set; }

        public int pCEENQMDS { get; set; }

        public int qCEENSGDS { get; set; }

        public int pCEENSGDS { get; set; }

        public int qCEENLJDS { get; set; }

        public int pCEENLJDS { get; set; }

        public int qCEENZB { get; set; }

        public int pCEENZB { get; set; }

        public int qCEENTSL { get; set; }

        public int pCEENTSL { get; set; }

        public int qCEENBMPZ { get; set; }

        public int pCEENBMPZ { get; set; }

        public int qCEENZSCJ { get; set; }

        public int pCEENZSCJ { get; set; }

        public int qCEENGSCJ { get; set; }

        public int pCEENGSCJ { get; set; }

        public int qCEENJH { get; set; }

        public int pCEENJH { get; set; }

        public int qCEENZX { get; set; }

        public int pCEENZX { get; set; }

        public int qCEENUVCF { get; set; }

        public int pCEENUVCF { get; set; }

        //2012年8月24日11:45:47   AS外销报告新增字段

        public int qASNL { get; set; }

        public int pASNL { get; set; }

        public int qASCCSL { get; set; }

        public int pASCCSL { get; set; }

        public int qASWGCS { get; set; }

        public int pASWGCS { get; set; }

        public int qASJRWDX { get; set; }

        public int pASJRWDX { get; set; }

        public int qASZB { get; set; }

        public int pASZB { get; set; }

        public int qASQMDSZSD { get; set; }

        public int pASQMDSZSD { get; set; }

        public int qASWD { get; set; }

        public int pASWD { get; set; }

        public int qASZSCJSL { get; set; }

        public int pASZSCJSL { get; set; }

        public int qASGSCJSL { get; set; }

        public int pASGSCJSL { get; set; }

        public int qASTGCJSL { get; set; }

        public int pASTGCJSL { get; set; }

        public int qASCTCS { get; set; }

        public int pASCTCS { get; set; }

        public int qASNRX { get; set; }

        public int pASNRX { get; set; }

        public int qASNSX { get; set; }

        public int pASNSX { get; set; }

        public int qASJH { get; set; }

        public int pASJH { get; set; }

        //2012年9月3日9:30:35 JIS外销报告新增字段

        public int qJISJPWG { get; set; }

        public int pJISJPWG { get; set; }

        public int qJISJPLJD { get; set; }

        public int pJISJPLJD { get; set; }

        public int qJISJPQGD { get; set; }

        public int pJISJPQGD { get; set; }

        public int qJISJPSGD { get; set; }

        public int pJISJPSGD { get; set; }

        public int qJISJPTGL { get; set; }

        public int pJISJPTGL { get; set; }

        public int qJISJPNCCX { get; set; }

        public int pJISJPNCCX { get; set; }

        public int qJISJPBMNMHDK { get; set; }

        public int pJISJPBMNMHDK { get; set; }

        public int qJISJPNREX { get; set; }

        public int pJISJPNREX { get; set; }

        public int qJISJPNSX { get; set; }

        public int pJISJPNSX { get; set; }

        public int qJISJPNRAX { get; set; }

        public int pJISJPNRAX { get; set; }

        public int qJISWCPWG { get; set; }

        public int pJISWCPWG { get; set; }

        public int qJISWCPNCCX { get; set; }

        public int pJISWCPNCCX { get; set; }

        public int qJISWCPJMXDYSY { get; set; }

        public int pJISWCPJMXDYSY { get; set; }

        public int qJISWCPJMXDESY { get; set; }

        public int pJISWCPJMXDESY { get; set; }

        public int qJISWCPTDQD { get; set; }

        public int pJISWCPTDQD { get; set; }

        public int qJISWCPNXDX { get; set; }

        public int pJISWCPNXDX { get; set; }

        public int qJISWCPGZ { get; set; }

        public int pJISWCPGZ { get; set; }

        public int qJISWCPCL { get; set; }

        public int pJISWCPCL { get; set; }

        public int qJISWCPJHBS { get; set; }

        public int pJISWCPJHBS { get; set; }

        public int pJISWCPBZSJH { get; set; }

        public int qJISWCPBZSJH { get; set; }

        public int qJISWCPSYSC { get; set; }

        public int pJISWCPSYSC { get; set; }

        #endregion

        public string ProductName { get; set; }
    }
}
