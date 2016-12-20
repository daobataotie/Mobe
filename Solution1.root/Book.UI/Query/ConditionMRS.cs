using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionMRS : ConditionA
    {
        public string CustomerStart { get; set; }
        public string CustomerEnd { get; set; }
        public string MrsStart { get; set; }
        public string MrsEnd { get; set; }
        public int? SourceType { get; set; }


        private Model.Product _product;
        public Model.Product Product
        {
            get { return this._product; }
            set { this._product = value; }
        }
        private string _id1;
        public string Id1
        {
            get { return this._id1; }
            set { this._id1 = value; }
        }
        private string _id2;
        public string Id2
        {
            get { return this._id2; }
            set { this._id2 = value; }
        }
        private string _cusxoid;
        public string Cusxoid
        {
            get { return this._cusxoid; }
            set
            {
                this._cusxoid = value;
            }
        }

        private int _OrderColumn;

        public int OrderColumn
        {
            get { return _OrderColumn; }
            set { _OrderColumn = value; }
        }

        private int _OrderType;

        public int OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }

        private Model.ProductCategory _productCategory;
        public Model.ProductCategory ProductCategory
        {
            get { return this._productCategory; }
            set { this._productCategory = value; }
        }
    }
}
