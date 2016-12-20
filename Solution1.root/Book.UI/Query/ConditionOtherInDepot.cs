using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionOtherInDepot : ConditionA
    {
        private Model.Supplier supplier1;

        public Model.Supplier Supplier1
        {
            get { return supplier1; }
            set { supplier1 = value; }
        }

        private Model.Supplier supplier2;

        public Model.Supplier Supplier2
        {
            get { return supplier2; }
            set { supplier2 = value; }
        }
        
        private Model.Product product1;

        public Model.Product Product1
        {
            get { return product1; }
            set { product1 = value; }
        }
        
        private Model.Product product2;

        public Model.Product Product2
        {
            get { return product2; }
            set { product2 = value; }
        }
        
        private string produceOtherCompactId1;

        public string ProduceOtherCompactId1
        {
            get { return produceOtherCompactId1; }
            set { produceOtherCompactId1 = value; }
        }
        
        private string produceOtherCompactId2;

        public string ProduceOtherCompactId2
        {
            get { return produceOtherCompactId2; }
            set { produceOtherCompactId2 = value; }
        }

        private string _InvouceCusIdStart;

        public string InvouceCusIdStart
        {
            get { return _InvouceCusIdStart; }
            set { _InvouceCusIdStart = value; }
        }

        private string _InvoiceCusIdEnd;

        public string InvoiceCusIdEnd
        {
            get { return _InvoiceCusIdEnd; }
            set { _InvoiceCusIdEnd = value; }
        }
    }
}
