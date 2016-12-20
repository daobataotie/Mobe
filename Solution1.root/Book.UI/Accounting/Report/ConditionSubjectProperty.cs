using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Query;

namespace Book.UI.Accounting.Report
{
    public class ConditionSubjectProperty : Condition
    {
        private string respectiveSubject;

        public string RespectiveSubject
        {
            get { return respectiveSubject; }
            set { respectiveSubject = value; }
        }
    }
}
