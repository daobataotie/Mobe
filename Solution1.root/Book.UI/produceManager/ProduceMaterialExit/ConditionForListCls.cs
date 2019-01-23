using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.produceManager.ProduceMaterialExit
{
    public class ConditionForListCls : Query.Condition
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 终止时间
        /// </summary>
        public DateTime EndDate { get; set; }

        public string StartPronoteHeaderId { get; set; }

        public string EndPronoteHeaderId { get; set; }

        public string StartPMEid { get; set; }

        public string EndPMEid { get; set; }

        public Model.Product StartProduct { get; set; }

        public Model.Product EndProduct { get; set; }

        public string WorkhouseId { get; set; }

        public string InvocieXOCusId { get; set; }

        public string HandBookId { get; set; }
    }
}
