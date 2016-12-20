using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public class Condition:Query.Condition
    {
        public DateTime Date_Start { get; set; }

        public DateTime Date_End { get; set; }

        public string CusInvoiceXOId { get; set; }

        public Model.Product Product { get; set; }
    }
}
