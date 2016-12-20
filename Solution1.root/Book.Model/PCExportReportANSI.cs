//------------------------------------------------------------------------------
//
// file name：PCExportReportANSI.cs
// author: mayanjun
// create date：2012-3-9 17:01:21
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 外销报告
    /// </summary>
    [Serializable]
    public partial class PCExportReportANSI
    {
        /// <summary>
        /// 圆锥坠落检验
        /// </summary>
        public int SUMIsYuanZhuiZhuiLuo { get; set; }

        /// <summary>
        /// 高速冲击检验
        /// </summary>
        public int SUMIsGaoSuChongJi { get; set; }

        /// <summary>
        /// 耐燃性检验
        /// </summary>
        public int SUMIsNaiRanXing { get; set; }

        /// <summary>
        /// 目视检验
        /// </summary>
        public int SUMIsMuShiJianYan { get; set; }

        /// <summary>
        /// 雾度测试
        /// </summary>
        public int SUMIsFogPassing { get; set; }

        /// <summary>
        /// 紫外线测试
        /// </summary>
        public int SUMIsZiWaiXian { get; set; }

        /// <summary>
        /// 可见光测试
        /// </summary>
        public int SUMIsKeJianGuang { get; set; }

        /// <summary>
        /// 镜片穿透测试
        /// </summary>
        public int SUMIsPenetrate { get; set; }

        /// <summary>
        /// 测试数量ANSI表单
        /// </summary>
        public int mCountANSI { get; set; }

        /// <summary>
        /// PCFog表单测试数量
        /// </summary>
        public int mCountFog { get; set; }

        /// <summary>
        /// PCOptics表单测试数量
        /// </summary>
        public int mCountOptics { get; set; }

        /// <summary>
        /// PCPenetrateCheck表单测试数量
        /// </summary>
        public int mCountPenetrate { get; set; }
        /// <summary>
        /// 光学测试
        /// </summary>
        public int MCountOptics { get; set; }
        /// <summary>
        /// 偏光偏差量测试
        /// </summary>
        public int MCountPGPCL { get; set; }
        /// <summary>
        /// 结构测试
        /// </summary>
        public int MCountJG { get; set; }
        /// <summary>
        /// 棱镜度数测试
        /// </summary>
        public int MCountLJDS { get; set; }
        /// <summary>
        /// 坐标测试
        /// </summary>
        public int MCountZB { get; set; }
        /// <summary>
        /// 透视率测试
        /// </summary>
        public int MCountTSL { get; set; }
        /// <summary>
        /// 表面品质测试
        /// </summary>
        public int MCountBMPJ { get; set; }
        /// <summary>
        /// 直式冲击测试
        /// </summary>
        public int MCountZSCJ { get; set; }
        /// <summary>
        /// 记号测试
        /// </summary>
        public int MCountJH { get; set; }
        /// <summary>
        /// 资讯测试
        /// </summary>
        public int MCountZX { get; set; }
        /// <summary>
        /// UV成分测试
        /// </summary>
        public int MCountUVCF { get; set; }

        /// <summary>
        /// CSA高低速冲击测试
        /// </summary>
        public string CSAChongJiCeShi { get; set; }


        #region 2012年6月19日15:52:23ANSI外销报告添加后两列字段

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

        //2012年8月10日16:53:06  新加字段

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

        //2012年8月16日10:41:44 新加字段

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

        //2012年8月24日11:57:40 AS外销报告新增字段

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
    }
}
