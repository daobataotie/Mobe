using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionProduceStatistics : ConditionA
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
        private string startProduceStatisticsId;

        public string StartProduceStatisticsId
        {
            get { return startProduceStatisticsId; }
            set { startProduceStatisticsId = value; }
        }
        private string endProduceStatisticsId;

        public string EndProduceStatisticsId
        {
            get { return endProduceStatisticsId; }
            set { endProduceStatisticsId = value; }
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
