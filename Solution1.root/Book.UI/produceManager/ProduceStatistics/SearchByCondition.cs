using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Query;

namespace Book.UI.produceManager.ProduceStatistics
{
    public class SearchByCondition : ConditionA
    {
        private string pronoteHeaderId;

        public string PronoteHeaderId
        {
            get { return pronoteHeaderId; }
            set { pronoteHeaderId = value; }
        }

    }
}
