using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionAcPayment : Condition
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
        private Model.Supplier startSupplier;

        public Model.Supplier StartSupplier
        {
            get { return startSupplier; }
            set { startSupplier = value; }
        }
        private Model.Supplier endSupplier;

        public Model.Supplier EndSupplier
        {
            get { return endSupplier; }
            set { endSupplier = value; }
        }
        private string startAcInvoiceCOBill;

        public string StartAcInvoiceCOBill
        {
            get { return startAcInvoiceCOBill; }
            set { startAcInvoiceCOBill = value; }
        }
        private string endAcinvoiceCOBill;

        public string EndAcinvoiceCOBill
        {
            get { return endAcinvoiceCOBill; }
            set { endAcinvoiceCOBill = value; }
        }
        private string startAcpayment;

        public string StartAcpayment
        {
            get { return startAcpayment; }
            set { startAcpayment = value; }
        }
        private string endAcpayment;

        public string EndAcpayment
        {
            get { return endAcpayment; }
            set { endAcpayment = value; }
        }
    }
}
