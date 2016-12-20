using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.produceManager.ProduceInDepot
{
    public class ChooseProductDefectRateCls : Query.Condition
    {
        /// <summary>
        /// 需筛选日期类型 0--生产入库单入库日期,1--加工单结案日期
        /// </summary>
        public int DateType { get; set; }
        
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 终止时间
        /// </summary>
        public DateTime EndDate { get; set; }

        public Model.Product StartProduct { get; set; }

        public Model.Product EndProduct { get; set; }

        public Model.WorkHouse StartWorkHouse { get; set; }

        public Model.WorkHouse EndWorkHouse { get; set; }

        public Model.Customer StartCustomer { get; set; }

        public Model.Customer EndCustomer { get; set; }

        public string StartPronoteHeaderId { get; set; }

        public string EndPronoteHeaderId { get; set; }

        public string StartProduceInDepotId { get; set; }

        public string EndProduceInDepotId { get; set; }

        /// <summary>
        /// 记录方式 展开true 合并false
        /// </summary>
        public bool attrJiLuFangShi { get; set; }

        public bool attrQiangHua { get; set; }

        public bool attrWuDu { get; set; }

        public bool attrWuQiangHuaWuDu { get; set; }

        public int attrProductStates { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public int attrOrderColumn { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public int attrOrderType { get; set; }

        /// <summary>
        /// 不良率
        /// </summary>
        public double RejectionRate { get; set; }

        /// <summary>
        /// 比较方式
        /// </summary>
        public string RejectionRateCompare { get; set; }

        /// <summary>
        /// 是否启用不良率筛选
        /// </summary>
        public bool EnableBLV { get; set; }

        /// <summary>
        /// 起始客户订单编号
        /// </summary>
        public string StarInvoiceXOId { get; set; }

        /// <summary>
        /// 终止客户订单编号
        /// </summary>
        public string EndInvoiceXOId { get; set; }

        /// <summary>
        /// 已结案订单起始
        /// </summary>
        //public string HasJieAnXoStart { get; set; }

        /// <summary>
        /// 已结案订单终止
        /// </summary>
        //public string HasJieAnXoEnd { get; set; }

        /// <summary>
        /// 订单状态 0不限制 1已结案 2未结案
        /// </summary>
        public int InvoiceStates { get; set; }
    }
}
