using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 够波涛             完成时间:2009-4-15
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public class ConditionF : ConditionA
    {
        private string startId;

        public string StartId
        {
            get { return startId; }
            set { startId = value; }
        }
        private string endId;

        public string EndId
        {
            get { return endId; }
            set { endId = value; }
        }

        private string categoryId;

        public string CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        public string StartChuHuoId { get; set; }

        public string EndChuHuoId { get; set; }
    }
}
