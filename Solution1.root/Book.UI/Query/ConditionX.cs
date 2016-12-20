using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionX : ConditionA
    {
        private Model.Customer _Customer1;

        public Model.Customer Customer1
        {
            get { return this._Customer1; }
            set { this._Customer1 = value; }
        }
        private Model.Customer _Customer2;

        public Model.Customer Customer2
        {
            get { return this._Customer2; }
            set { this._Customer2 = value; }
        }

        private Model.Employee _employee1;

        public Model.Employee Employee1
        {
            get { return this._employee1; }
            set { this._employee1 = value; }
        }

        private Model.Employee _employee2;

        public Model.Employee Employee2
        {
            get { return this._employee2; }
            set { this._employee2 = value; }
        }

        private string _xoid1;
        public string XOId1
        {
            get { return this._xoid1; }
            set { this._xoid1 = value; }
        }
        private string _xoid2;
        public string XOId2
        {
            get { return this._xoid2; }
            set { this._xoid2 = value; }
        }

        private bool _isclose;
        public bool IsClose
        {
            get { return this._isclose; }
            set { this._isclose = value; }
        }


        private Model.Product _product;
        public Model.Product Product
        {
            get { return this._product; }
            set { _product = value; }
        }
        private Model.Product _product2;
        public Model.Product Product2
        {
            get { return this._product2; }
            set { _product2 = value; }
        }
        private string _cusxoid;
        public string CusXOId
        {
            get { return _cusxoid; }
            set { _cusxoid = value; }
        }
        private DateTime _yjri1;
        public DateTime Yjri1
        {
            get { return _yjri1; }
            set
            {
                _yjri1 = value;
            }
        }
        private DateTime _yjri2;
        public DateTime Yjri2
        {
            get { return _yjri2; }
            set
            {
                _yjri2 = value;
            }
        }

        private int _orderColumn;

        public int OrderColumn
        {
            get { return _orderColumn; }
            set { _orderColumn = value; }
        }
        private int _orderType;

        public int OrderType
        {
            get { return _orderType; }
            set { _orderType = value; }
        }

        private bool _detailFlag;
        public bool DetailFlag
        {
            get { return this._detailFlag; }
            set { this._detailFlag = value; }

        }


    }
}
