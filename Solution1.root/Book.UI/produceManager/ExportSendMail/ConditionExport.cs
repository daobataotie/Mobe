using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.produceManager.ExportSendMail
{
    public class ConditionExport : Query.ConditionPronoteHeader
    {
        /// <summary>
        /// 外销报告类型
        /// </summary>
        public string ExpType { get; set; }
    }
}
