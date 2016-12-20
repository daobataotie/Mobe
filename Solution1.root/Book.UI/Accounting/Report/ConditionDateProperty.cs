using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Query;
namespace Book.UI.Accounting.Report
{
    public class ConditionDateProperty : Condition
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
    }
}
