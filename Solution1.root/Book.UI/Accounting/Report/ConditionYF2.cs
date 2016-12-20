using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Query;

namespace Book.UI.Accounting.Report
{
    public class ConditionYF2 : Condition
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
        private string startBankAccountId;

        public string StartBankAccountId
        {
            get { return startBankAccountId; }
            set { startBankAccountId = value; }
        }
        private string endBankAccountId;

        public string EndBankAccountId
        {
            get { return endBankAccountId; }
            set { endBankAccountId = value; }
        }
    }
}
