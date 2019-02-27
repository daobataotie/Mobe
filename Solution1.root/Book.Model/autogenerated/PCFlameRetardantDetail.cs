﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCFlameRetardantDetail.autogenerated.cs
// author: mayanjun
// create date：2018/12/27 13:17:17
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    public partial class PCFlameRetardantDetail
    {
        #region Data

        /// <summary>
        /// 
        /// </summary>
        private string _pCFlameRetardantDetailId;

        /// <summary>
        /// 
        /// </summary>
        private string _pCFlameRetardantId;

        private int _number;

        /// <summary>
        /// 
        /// </summary>
        private string _productId;

        /// <summary>
        /// 
        /// </summary>
        private string _pihao;

        /// <summary>
        /// 
        /// </summary>
        private string _yanse;

        /// <summary>
        /// 
        /// </summary>
        private string _qianghua;

        /// <summary>
        /// 
        /// </summary>
        private string _fangwu;

        /// <summary>
        /// 
        /// </summary>
        private string _wuQianghua;

        /// <summary>
        /// 
        /// </summary>
        private string _ranshao;

        /// <summary>
        /// 
        /// </summary>
        private string _employeeId;

        /// <summary>
        /// 
        /// </summary>
        private int? _testQty;

        /// <summary>
        /// 
        /// </summary>
        private string _judge;

        /// <summary>
        /// 
        /// </summary>
        private string _note;

        private string _invoiceXOId;

        private string _productUnit;

        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee;
        /// <summary>
        /// 
        /// </summary>
        private PCFlameRetardant _pCFlameRetardant;
        /// <summary>
        /// 产品
        /// </summary>
        private Product _product;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string PCFlameRetardantDetailId
        {
            get
            {
                return this._pCFlameRetardantDetailId;
            }
            set
            {
                this._pCFlameRetardantDetailId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PCFlameRetardantId
        {
            get
            {
                return this._pCFlameRetardantId;
            }
            set
            {
                this._pCFlameRetardantId = value;
            }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Pihao
        {
            get
            {
                return this._pihao;
            }
            set
            {
                this._pihao = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Yanse
        {
            get
            {
                return this._yanse;
            }
            set
            {
                this._yanse = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Qianghua
        {
            get
            {
                return this._qianghua;
            }
            set
            {
                this._qianghua = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Fangwu
        {
            get
            {
                return this._fangwu;
            }
            set
            {
                this._fangwu = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string WuQianghua
        {
            get
            {
                return this._wuQianghua;
            }
            set
            {
                this._wuQianghua = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Ranshao
        {
            get
            {
                return this._ranshao;
            }
            set
            {
                this._ranshao = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EmployeeId
        {
            get
            {
                return this._employeeId;
            }
            set
            {
                this._employeeId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? TestQty
        {
            get
            {
                return this._testQty;
            }
            set
            {
                this._testQty = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Judge
        {
            get
            {
                return this._judge;
            }
            set
            {
                this._judge = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
            }
        }

        public string InvoiceXOId
        {
            get { return _invoiceXOId; }
            set { _invoiceXOId = value; }
        }

        public string ProductUnit
        {
            get { return _productUnit; }
            set { _productUnit = value; }
        }

        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee
        {
            get
            {
                return this._employee;
            }
            set
            {
                this._employee = value;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public virtual PCFlameRetardant PCFlameRetardant
        {
            get
            {
                return this._pCFlameRetardant;
            }
            set
            {
                this._pCFlameRetardant = value;
            }

        }
        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product
        {
            get
            {
                return this._product;
            }
            set
            {
                this._product = value;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_PCFlameRetardantDetailId = "PCFlameRetardantDetailId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_PCFlameRetardantId = "PCFlameRetardantId";

        public readonly static string PRO_Number = "Number";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_ProductId = "ProductId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Pihao = "Pihao";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Yanse = "Yanse";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Qianghua = "Qianghua";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Fangwu = "Fangwu";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_WuQianghua = "WuQianghua";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Ranshao = "Ranshao";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_EmployeeId = "EmployeeId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_TestQty = "TestQty";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Judge = "Judge";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Note = "Note";

        public readonly static string PRO_InvoicXOId = "InvoicXOId";

        public readonly static string PRO_ProductUnit = "ProductUnit";
        #endregion
    }
}