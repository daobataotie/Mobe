using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Query;
namespace Book.UI.produceManager.MRSHeader
{
    class Condition1 : ConditionA
    {
        private string _xocusid;

        public string CusXOId
        {
            get { return _xocusid; }
            set { _xocusid = value; }
        }
        private int _sourceType;
        public int SourceType
        {
            get
            { return _sourceType; }
            set 
            { _sourceType = value; }
        }

    }
}
