using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class ProduceOther
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public enum OtherOperationType
        {
            /// <summary>
            /// 訂單委外
            /// </summary>
            InvoiceOther,
            /// <summary>
            /// 工序委外
            /// </summary>
            ProceduresOther,

        }
        /// <summary>
        /// 加工种类
        /// </summary>
        public enum OtherProduceType
        {
            /// <summary>
            /// 正常
            /// </summary>
            NormalOther,
            /// <summary>
            /// 返修
            /// </summary>
            ReverseOther
        }
        /// <summary>
        /// 加工废料,工费,非合理消耗处理方式
        /// </summary>
        public enum OtherProduceDoMode
        {
            /// <summary>
            /// 冲减加工费(-)
            /// </summary>
            ProduceCostDiff,
            /// <summary>
            /// 加工费(+)
            /// </summary>
            ProduceCostAdd
        }
        /// <summary>
        /// 单价标识
        /// </summary>
        public enum OtherPriceTag
        {
            /// <summary>
            /// 内含
            /// </summary>
            OtherPriceIn,
            /// <summary>
            /// 未含
            /// </summary>
            OtherPriceNotIn
        }

        /// <summary>
        /// ANSI外销报告分类
        /// </summary>
        public enum PCExportReportANSIType
        {
            /// <summary>
            /// 目视检验
            /// </summary>
            MSJY,
            /// <summary>
            /// 清晰度
            /// </summary>
            QXD,
            /// <summary>
            /// 棱镜度&&棱镜平衡度数
            /// </summary>
            LJDS,
            /// <summary>
            /// 可见光透视率
            /// </summary>
            KJGTSL,
            /// <summary>
            /// 紫外线透视率
            /// </summary>
            ZWXTSL,
            /// <summary>
            /// 球面度数
            /// </summary>
            QMDS,
            /// <summary>
            /// 散光度数
            /// </summary>
            SGDS,
            /// <summary>
            /// 高速冲击测试
            /// </summary>
            GSCJCS,
            /// <summary>
            /// 圆锥坠落撞击测试
            /// </summary>
            YZZLZJCS,
            /// <summary>
            /// 镜片穿透测试
            /// </summary>
            JPCTCS,
            /// <summary>
            /// 雾度测试
            /// </summary>
            WDCS,
            /// <summary>
            /// 耐燃性测试
            /// </summary>
            NRXCS
        }
    }
}
