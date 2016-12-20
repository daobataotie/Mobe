using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Query;
namespace Book.UI.produceManager.ProduceOtherCompact
{
    class Condition1 : ConditionA
    {

        private Model.Product _product;

        public Model.Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public string mCustomerId { get; set; }
        public string mSupplierId { get; set; }

        public string ProduceOtherCompactId { get; set; }

        public string InvoiceCusXOId { get; set; }
    }
}
