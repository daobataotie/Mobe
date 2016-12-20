using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Query;

namespace Book.UI.produceManager.ProduceInDepot
{
    class ConditionProduceIn : ConditionA
    {
        private string pronoteHeaderId;

        public string PronoteHeaderId
        {
            get { return pronoteHeaderId; }
            set { pronoteHeaderId = value; }
        }

        private string workHouseId;

        public string WorkHouseId
        {
            get { return workHouseId; }
            set { workHouseId = value; }
        }
        private Model.Product _product;

        public Model.Product Product
        {
            get { return _product; }
            set { _product = value; }
        }
        private string _cusxoid;
        public string CusXOId
        {
            get { return _cusxoid; }
            set { _cusxoid = value; }
        }
    }
}
