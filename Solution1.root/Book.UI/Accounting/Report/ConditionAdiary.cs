using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Query;


namespace Book.UI.Accounting.Report
{
    public class ConditionAdiary : Condition
    {
        private int summonCatetory;

        public int SummonCatetory
        {
            get { return summonCatetory; }
            set { summonCatetory = value; }
        }
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
    }
}
