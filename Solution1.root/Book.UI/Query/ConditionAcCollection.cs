using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    class ConditionAcCollection : Condition
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

        private Model.Customer startCustomer;

        public Model.Customer StartCustomer
        {
            get { return startCustomer; }
            set { startCustomer = value; }
        }
        private Model.Customer endCustomer;

        public Model.Customer EndCustomer
        {
            get { return endCustomer; }
            set { endCustomer = value; }
        }
        private string startAcInvoiceXOBill;

        public string StartAcInvoiceXOBill
        {
            get { return startAcInvoiceXOBill; }
            set { startAcInvoiceXOBill = value; }
        }
        private string endAcInvoiceXOBill;

        public string EndAcInvoiceXOBill
        {
            get { return endAcInvoiceXOBill; }
            set { endAcInvoiceXOBill = value; }
        }
        private string startAcCollection;

        public string StartAcCollection
        {
            get { return startAcCollection; }
            set { startAcCollection = value; }
        }
        private string endAcCollection;

        public string EndAcCollection
        {
            get { return endAcCollection; }
            set { endAcCollection = value; }
        }
    }
}
