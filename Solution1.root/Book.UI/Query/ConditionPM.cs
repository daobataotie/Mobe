using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionPM : ConditionA
    {
        private string _mouldId;

        public string MouldId
        {
            get { return _mouldId; }
            set { _mouldId = value; }
        }

        private string _mouldName;

        public string MouldName
        {
            get { return _mouldName; }
            set { _mouldName = value; }
        }

        private Model.MouldCategory _mouldCategory;

        public Model.MouldCategory MouldCategory
        {
            get { return _mouldCategory; }
            set { _mouldCategory = value; }
        }
    }
}
