using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public  class ConditionPronoteHeader : ConditionA
    {
        private Model.Customer _Customer;
      
        public Model.Customer Customer
        {
            get { return this._Customer; }
            set { this._Customer = value; }
        }
        private Model.Product _product;
        public Model.Product Product
        {
            get { return this._product; }
            set { _product = value; }
        }
        private string _pronoteHeaderIdStart;
        public string PronoteHeaderIdStart
       {
           get { return _pronoteHeaderIdStart; }
           set { _pronoteHeaderIdStart = value; }
       }
        private string _pronoteHeaderIdEnd;
        public string PronoteHeaderIdEnd
        {
            get { return _pronoteHeaderIdEnd; }
            set { _pronoteHeaderIdEnd = value; }
        }
        private string _cusxoid;
        public string CusXOId
        {
            get { return _cusxoid; }
            set { _cusxoid = value; }
        }
        private int _sourcetpye;
        public int SourceTpye
        {
            get { return _sourcetpye; }
            set { _sourcetpye = value; }
        }

        private string _proNameKey;
        public string ProNameKey
        {
            get { return _proNameKey; }
            set { _proNameKey = value; }
        }
        private string _proCusNameKey;
        public string ProCusNameKey
        {
            get { return _proCusNameKey; }
            set { _proCusNameKey = value; }
        }
        private string _pronoteHeaderIdKey;
        public string PronoteHeaderIdKey
        {
            get { return _pronoteHeaderIdKey; }
            set { _pronoteHeaderIdKey = value; }
        }

        public string HandbookId { get; set; }
    }
}
