using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.AccountPayable.AccQuery
{
   public class ChooseColCuiShou : Book.UI.Query.Condition
    {
        private Model.Customer _customer1;
        public Model.Customer Customer1
        {
            get { return this._customer1; }
            set { this._customer1 = value; }
        }
        private Model.Customer _customer2;
        public Model.Customer Customer2
        {
            get { return this._customer2; }
            set { this._customer2 = value; }
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

        private DateTime _YJdate;
        public DateTime YJDate
        {
            get { return this._YJdate; }
            set { this._YJdate = value; }
        }
        private bool _hasother;
        public bool Hasother
        {
            get { return this._hasother; }
            set { this._hasother = value; }
        }


    }
}
