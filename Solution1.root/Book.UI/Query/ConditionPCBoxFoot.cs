using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    class ConditionPCBoxFoot : ConditionA
    {
        private string invoiceXOId;

        public string InvoiceXOId
        {
            get { return invoiceXOId; }
            set { invoiceXOId = value; }
        }

        private string pronoteHeaderId;

        public string PronoteHeaderId
        {
            get { return pronoteHeaderId; }
            set { pronoteHeaderId = value; }
        }

        private Model.Product product;

        public Model.Product Product
        {
            get { return product; }
            set { product = value; }
        }
    }
}
