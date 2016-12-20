using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionPCInputCheck
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ProductId { get; set; }

        public string TestProductId { get; set; }

        public string SupplierId { get; set; }

        public string LotNumber { get; set; }

        public bool IsClosed { set; get; }
    }
}
