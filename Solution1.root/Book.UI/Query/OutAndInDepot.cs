using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class OutAndInDepot : ConditionA
    {
        private string _outDepotIdStart;

        private string _outDepotIdEnd;

        private string _DepotStart;

        private string _DepotEnd;

        private string _productNameStart;

        private string _productNameEnd;

        private string _produceCategoryStart;

        private string _productCategoryEnd;

        private string _productIdStart;

        private string _productIdEnd;

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

        public string DepotEnd
        {
            get { return _DepotEnd; }
            set { _DepotEnd = value; }
        }

        public string ProductNameStart
        {
            get { return _productNameStart; }
            set { _productNameStart = value; }
        }

        public string ProductNameEnd
        {
            get { return _productNameEnd; }
            set { _productNameEnd = value; }
        }

        public string ProduceCategoryStart
        {
            get { return _produceCategoryStart; }
            set { _produceCategoryStart = value; }
        }

        public string ProductCategoryEnd
        {
            get { return _productCategoryEnd; }
            set { _productCategoryEnd = value; }
        }

        public string ProductIdStart
        {
            get { return _productIdStart; }
            set { _productIdStart = value; }
        }

        public string ProductIdEnd
        {
            get { return _productIdEnd; }
            set { _productIdEnd = value; }
        }
    }
}
