using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionPCMouldCheck
    {
        public DateTime OnlineDateStart { get; set; }

        public DateTime OnlineDateEnd { get; set; }

        public DateTime CheckDateStart { get; set; }

        public DateTime CheckDateEnd { get; set; }

        public string ProductId { get; set; }

        public string InvoiceCusId { get; set; }
    }
}
