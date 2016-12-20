using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class InDepot : ConditionA
    {
        private string _inDepotIdStart;

        private string _inDepotIdEnd;

        private string _depotIdStart;

        private string _depotIdEnd;

        private Model.Supplier _supplierStart;

        private Model.Supplier _supplierEnd;

        public string InDepotIdStart
        {
            get { return _inDepotIdStart; }
            set { _inDepotIdStart = value; }
        }

        public string InDepotIdEnd
        {
            get { return _inDepotIdEnd; }
            set { _inDepotIdEnd = value; }
        }

        public string DepotIdStart
        {
            get { return _depotIdStart; }
            set { _depotIdStart = value; }
        }

        public string DepotIdEnd
        {
            get { return _depotIdEnd; }
            set { _depotIdEnd = value; }
        }

        public Model.Supplier SupplierStart
        {
            get { return _supplierStart; }
            set { _supplierStart = value; }
        }

        public Model.Supplier SupplierEnd
        {
            get { return _supplierEnd; }
            set { _supplierEnd = value; }
        }
    }
}
