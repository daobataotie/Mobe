using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionProduceStatisticsCheck : ConditionA
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
        private string startProduceStatisticsCheckId;

        public string StartProduceStatisticsCheckId
        {
            get { return startProduceStatisticsCheckId; }
            set { startProduceStatisticsCheckId = value; }
        }
        private string endProduceStatisticsCheckId;

        public string EndProduceStatisticsCheckId
        {
            get { return endProduceStatisticsCheckId; }
            set { endProduceStatisticsCheckId = value; }
        }
        private string startPronoteHeaderID;

        public string StartPronoteHeaderID
        {
            get { return startPronoteHeaderID; }
            set { startPronoteHeaderID = value; }
        }
        private string endPronoteHeaderID;

        public string EndPronoteHeaderID
        {
            get { return endPronoteHeaderID; }
            set { endPronoteHeaderID = value; }
        }
    }
}
