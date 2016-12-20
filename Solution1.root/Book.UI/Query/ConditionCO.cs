using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ConditionCO : ConditionA
    {
        private Model.Supplier _supplierstart;

        public Model.Supplier SupplierStart
        {
            get { return _supplierstart; }
            set { _supplierstart = value; }
        }
        private Model.Supplier _supplierend;

        public Model.Supplier SupplierEnd
        {
            get { return _supplierend; }
            set { _supplierend = value; }
        }
        private Model.Product _productstart;

        public Model.Product ProductStart
        {
            get { return _productstart; }
            set { _productstart = value; }
        }
        private Model.Product _productend;

        public Model.Product ProductEnd
        {
            get { return _productend; }
            set { _productend = value; }
        }
        private string _cusxoid;
        public string CusXOId
        {
            get { return this._cusxoid; }
            set { this._cusxoid = value; }
        }

        private string _cOStartId;
        /// <summary>
        ///采购单编号开始
        /// </summary>
        public string COStartId
        {
            get { return this._cOStartId; }
            set { this._cOStartId = value; }
        }
        private string _cOEndId;
        /// <summary>
        ///采购单编号结束
        /// </summary>
        public string COEndId
        {
            get { return this._cOEndId; }
            set { this._cOEndId = value; }
        }


        /// <summary>
        /// 交货开始时间
        /// </summary>
        private DateTime _startjhDate;

        /// <summary>
        /// 交货结束时间
        /// </summary>
        private DateTime _endjhDate;

        /// <summary>
        /// 交货开始时间
        /// </summary>
        public DateTime StartJHDate
        {
            get
            {
                return this._startjhDate.Date;
            }
            set
            {
                this._startjhDate = value;
            }
        }

        /// <summary>
        /// 交货结束时间
        /// </summary>
        public DateTime EndJHDate
        {
            get
            {
                return this._endjhDate.Date.AddDays(1).AddSeconds(-1);
            }
            set
            {
                this._endjhDate = value;
            }
        }


        private DateTime? _StartFKDate;

        private DateTime? _EndFKDate;

        public DateTime? StartFKDate
        {
            get { return this._StartFKDate; }
            set { this._StartFKDate = value; }
        }

        public DateTime? EndFKDate
        {
            get { return this._EndFKDate; }
            set { this._EndFKDate = value; }
        }


        private int? _invoiceFlag;
        public int? InvoiceFlag
        {
            get { return this._invoiceFlag; }
            set { this._invoiceFlag = value; }

        }

        private Model.Employee _empStart;

        public Model.Employee EmpStart
        {
            get { return _empStart; }
            set { _empStart = value; }
        }

        private Model.Employee _empEnd;

        public Model.Employee EmpEnd
        {
            get { return _empEnd; }
            set { _empEnd = value; }
        }

    }
}
