using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Query;

namespace Book.UI.Accounting.Report
{
    public class ConditionProperty : Condition
    {
        private string startPropertyId;

        public string StartPropertyId
        {
            get { return startPropertyId; }
            set { startPropertyId = value; }
        }
        private string endPropertyId;

        public string EndPropertyId
        {
            get { return endPropertyId; }
            set { endPropertyId = value; }
        }
    }
}
