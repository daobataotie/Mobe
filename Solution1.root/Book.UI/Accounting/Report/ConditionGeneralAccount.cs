using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Query;

namespace Book.UI.Accounting.Report
{
    public class ConditionGeneralAccount : Condition
    {
        private DateTime startDate;
        private DateTime endDate;
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }
        private string startSubjectId;

        public string StartSubjectId
        {
            get { return startSubjectId; }
            set { startSubjectId = value; }
        }
        private string endSubjectId;

        public string EndSubjectId
        {
            get { return endSubjectId; }
            set { endSubjectId = value; }
        }

        /// <summary>
        /// 带出科目期初设定余额
        /// </summary>
        private bool _Include_QiChuYuE;

        /// <summary>
        /// 带出科目期初设定余额
        /// </summary>
        public bool Include_QiChuYuE
        {
            get { return _Include_QiChuYuE; }
            set { _Include_QiChuYuE = value; }
        }

    }
}
