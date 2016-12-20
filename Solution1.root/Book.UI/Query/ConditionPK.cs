using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionPK : Condition
    {
        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        private string nO;

        public string NO
        {
            get { return nO; }
            set { nO = value; }
        }

        private string invoiceOf;

        public string InvoiceOf
        {
            get { return invoiceOf; }
            set { invoiceOf = value; }
        }

        private string shippedById;

        public string ShippedById
        {
            get { return shippedById; }
            set { shippedById = value; }
        }

        private string consigneeId;

        public string ConsigneeId
        {
            get { return consigneeId; }
            set { consigneeId = value; }
        }
    }
}
