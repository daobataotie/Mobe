using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾             完成时间:2009-5-4
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public class ConditionOtherExit : ConditionA
    {
        private string supplierId1;

        public string SupplierId1
        {
            get { return supplierId1; }
            set { supplierId1 = value; }
        }
        private string supplierId2;

        public string SupplierId2
        {
            get { return supplierId2; }
            set { supplierId2 = value; }
        }
        private string produceOtherCompactId1;

        public string ProduceOtherCompactId1
        {
            get { return produceOtherCompactId1; }
            set { produceOtherCompactId1 = value; }
        }
        private string produceOtherCompactId2;

        public string ProduceOtherCompactId2
        {
            get { return produceOtherCompactId2; }
            set { produceOtherCompactId2 = value; }
        }

        private string productId1;

        public string ProductId1
        {
            get { return productId1; }
            set { productId1 = value; }
        }
        private string productId2;

        public string ProductId2
        {
            get { return productId2; }
            set { productId2 = value; }
        }
    }
}
