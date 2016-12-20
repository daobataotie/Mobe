using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class OutDepot : ConditionA
    {
        private string _outDepotIdStart;

        private string _outDepotIdEnd;

        private string  _DepotStart;

        private string  _DepotEnd;

        public string OutDepotIdStart
        {
            get { return _outDepotIdStart; }
            set { _outDepotIdStart = value; }
        }

        public string OutDepotIdEnd
        {
            get { return _outDepotIdEnd; }
            set { _outDepotIdEnd = value; }
        }

        public string DepotStart
        {
            get { return _DepotStart; }
            set { _DepotStart = value; }
        }

        public string  DepotEnd
        {
            get { return _DepotEnd; }
            set { _DepotEnd = value; }
        }
    }
}
