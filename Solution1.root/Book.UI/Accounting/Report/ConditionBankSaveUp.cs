using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Query;

namespace Book.UI.Accounting.Report
{
    public class ConditionBankSaveUp : Condition
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
        private string bankAccountId;

        public string BankAccountId
        {
            get { return bankAccountId; }
            set { bankAccountId = value; }
        }
    }
}
