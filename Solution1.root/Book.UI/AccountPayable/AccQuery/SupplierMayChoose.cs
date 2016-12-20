using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.AccountPayable.AccQuery
{
  public   class SupplierMayChoose : Book.UI.Query.ConditionA
    {
        private Model.Supplier _supplier1;
        public Model.Supplier Supplier1
        {
            get { return this._supplier1; }
            set { this._supplier1 = value; }
        }
        private Model.Supplier _supplier2;
        public Model.Supplier Supplier2
        {
            get { return this._supplier2; }
            set { this._supplier2 = value; }
        }

        private Model.Employee _employee1;
        public Model.Employee Employee1
        {
            get { return this._employee1; }
            set { this._employee1 = value; }
        }

        private Model.Employee _employee2;
        public Model.Employee Employee2
        {
            get { return this._employee2; }
            set { this._employee2 = value; }
        }
        private bool _hasother;
        public bool Hasother
        {
            get { return this._hasother; }
            set { this._hasother = value; }
        }


    }
}
