using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人: 刘永亮              完成时间:2011-01-20
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/
    public class ConditionMaterial : ConditionA
    {
        private string produceMaterialId0;

        public string ProduceMaterialId0
        {
            get { return produceMaterialId0; }
            set { produceMaterialId0 = value; }
        }


        private string produceMaterialId1;

        public string ProduceMaterialId1
        {
            get { return produceMaterialId1; }
            set { produceMaterialId1 = value; }
        }


        private Model.Product product0;

        public Model.Product Product0
        {
            get { return product0; }
            set { product0 = value; }
        }

        private Model.Product product1;

        public Model.Product Product1
        {
            get { return product1; }
            set { product1 = value; }
        }

        private string departmentId0;

        public string DepartmentId0
        {
            get { return departmentId0; }
            set { departmentId0 = value; }
        }
        private string departmentId1;

        public string DepartmentId1
        {
            get { return departmentId1; }
            set { departmentId1 = value; }
        }

        private string pronoteHeaderId0;

        public string PronoteHeaderId0
        {
            get { return pronoteHeaderId0; }
            set { pronoteHeaderId0 = value; }
        }
        private string pronoteHeaderId1;

        public string PronoteHeaderId1
        {
            get { return pronoteHeaderId1; }
            set { pronoteHeaderId1 = value; }
        }


        private string _CusInvoiceXOId;

        public string CusInvoiceXOId
        {
            get { return _CusInvoiceXOId; }
            set { _CusInvoiceXOId = value; }
        }
    }
}
