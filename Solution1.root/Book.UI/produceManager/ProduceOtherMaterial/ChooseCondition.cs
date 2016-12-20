using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.produceManager.ProduceOtherMaterial
{
    public class ChooseCondition
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string SupplierStartId { get; set; }

        public string SupplierEndId { get; set; }

        public string ProductStartId { get; set; }

        public string ProductEndId { get; set; }

        public string ProduceOtherCompactStartId { get; set; }

        public string ProduceOtherCompactEndId { get; set; }

        public string InvoiceCusID { get; set; }
    }
}
