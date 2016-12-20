using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.AccountPayable.AccQuery
{
    public class ConditionAcInvoiceXOBill : Book.UI.Query.ConditionA
    {
        public string StartXOid { get; set; }
        public string EndXOid { get; set; }

        public Model.Customer mStartCustomer { get; set; }
        public Model.Customer mEndCustomer { get; set; }
    }
}
