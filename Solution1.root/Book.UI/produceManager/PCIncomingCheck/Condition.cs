﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.produceManager.PCIncomingCheck
{
    public class Condition
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ProductId { get; set; }


        public string LotNumber { get; set; }
    }
}
