﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCFinishCheck.autogenerated.cs
// author: mayanjun
// create date：2012-3-16 14:10:49
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    public partial class PCFinishCheck
    {
        #region Data

        /// <summary>
        /// 主键编号
        /// </summary>
        private string _pCFinishCheckID;

        /// <summary>
        /// 商品编号
        /// </summary>
        private string _productId;

        /// <summary>
        /// 工作中心编号
        /// </summary>
        private string _workHouseId;

        /// <summary>
        /// 操作人员
        /// </summary>
        private string _employee0Id;

        /// <summary>
        /// 插入时间
        /// </summary>
        private DateTime? _insertTime;

        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime? _updateTime;

        /// <summary>
        /// 用户订单编号
        /// </summary>
        private string _invoiceCusXOId;

        /// <summary>
        /// 单据日期
        /// </summary>
        private DateTime? _pCFinishCheckDate;

        /// <summary>
        /// 数量
        /// </summary>
        private double? _pCFinishCheckCount;

        /// <summary>
        /// 入库日期数量
        /// </summary>
        private double? _pCFinishCheckInCoiunt;

        /// <summary>
        /// 备注
        /// </summary>
        private string _pCFinishCheckDesc;

        /// <summary>
        /// 豆仔点完全定位
        /// </summary>
        private int? _attrDZDWQDW;

        /// <summary>
        /// 外箱贴标
        /// </summary>
        private int? _attrWXTB;

        /// <summary>
        /// 脚尾圆滑无肉利
        /// </summary>
        private int? _attrJWYHWRL;

        /// <summary>
        /// 正麦侧麦
        /// </summary>
        private int? _attrZMCM;

        /// <summary>
        /// 灌嘴不可有肉利
        /// </summary>
        private int? _attrGZBKYRL;

        /// <summary>
        /// 塑料袋是否密封
        /// </summary>
        private int? _attrSLDSFMF;

        /// <summary>
        /// 正支无变形干净
        /// </summary>
        private int? _attrZZWBXGJ;

        /// <summary>
        /// 内盒吊卡是否正确
        /// </summary>
        private int? _attrNHDQSFZQ;

        /// <summary>
        /// 镜片不可刮伤
        /// </summary>
        private int? _attrJPBKGS;

        /// <summary>
        /// 内盒贴标
        /// </summary>
        private int? _attrNHTB;

        /// <summary>
        /// 镜片(脚)记号正确
        /// </summary>
        private int? _attrJPJHZQ;

        /// <summary>
        /// 镜绳是否正确
        /// </summary>
        private int? _attrJSSFZQ;

        /// <summary>
        /// 镜片(脚)色系
        /// </summary>
        private int? _attrJPSX;

        /// <summary>
        /// 镜袋置入方式
        /// </summary>
        private int? _attrJDZRFS;

        /// <summary>
        /// 镜脚是否太松摇晃
        /// </summary>
        private int? _attrJJSFTSYH;

        /// <summary>
        /// 泡壳置入方式
        /// </summary>
        private int? _attrPKZRFS;

        /// <summary>
        /// 光学
        /// </summary>
        private int? _attrGX;

        /// <summary>
        /// 塑料袋/内盒/外箱条码贴标是否正确
        /// </summary>
        private int? _attrSLDNHWXTMSFZQ;

        /// <summary>
        /// 透视率
        /// </summary>
        private int? _attrTSL;

        /// <summary>
        /// 冲击标准
        /// </summary>
        private int? _attrCJBZ;

        /// <summary>
        /// 
        /// </summary>
        private string _employee1Id;

        /// <summary>
        /// 
        /// </summary>
        private string _pronoteHeaderID;

        /// <summary>
        /// 
        /// </summary>
        private string _customerProductName;

        /// <summary>
        /// 
        /// </summary>
        private bool? _isMuShiJianYan;

        private int? _auditState;

        private string _auditEmpId;

        private string _ProductUnitId;

        private Employee _auditEmp;
        /// <summary>
        /// 生产通知头
        /// </summary>
        private PronoteHeader _pronoteHeader;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee0;
        /// <summary>
        /// 产品
        /// </summary>
        private Product _product;
        /// <summary>
        /// 工作中心
        /// </summary>
        private WorkHouse _workHouse;
        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee1;

        private ProductUnit _ProductUnit;

        #endregion

        #region Properties

        /// <summary>
        /// 主键编号
        /// </summary>
        public string PCFinishCheckID
        {
            get
            {
                return this._pCFinishCheckID;
            }
            set
            {
                this._pCFinishCheckID = value;
            }
        }

        /// <summary>
        /// 商品编号
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
        /// 工作中心编号
        /// </summary>
        public string WorkHouseId
        {
            get
            {
                return this._workHouseId;
            }
            set
            {
                this._workHouseId = value;
            }
        }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string Employee0Id
        {
            get
            {
                return this._employee0Id;
            }
            set
            {
                this._employee0Id = value;
            }
        }

        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime? InsertTime
        {
            get
            {
                return this._insertTime;
            }
            set
            {
                this._insertTime = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            get
            {
                return this._updateTime;
            }
            set
            {
                this._updateTime = value;
            }
        }

        /// <summary>
        /// 用户订单编号
        /// </summary>
        public string InvoiceCusXOId
        {
            get
            {
                return this._invoiceCusXOId;
            }
            set
            {
                this._invoiceCusXOId = value;
            }
        }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime? PCFinishCheckDate
        {
            get
            {
                return this._pCFinishCheckDate;
            }
            set
            {
                this._pCFinishCheckDate = value;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public double? PCFinishCheckCount
        {
            get
            {
                return this._pCFinishCheckCount;
            }
            set
            {
                this._pCFinishCheckCount = value;
            }
        }

        /// <summary>
        /// 入库日期数量
        /// </summary>
        public double? PCFinishCheckInCoiunt
        {
            get
            {
                return this._pCFinishCheckInCoiunt;
            }
            set
            {
                this._pCFinishCheckInCoiunt = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string PCFinishCheckDesc
        {
            get
            {
                return this._pCFinishCheckDesc;
            }
            set
            {
                this._pCFinishCheckDesc = value;
            }
        }

        /// <summary>
        /// 豆仔点完全定位
        /// </summary>
        public int? AttrDZDWQDW
        {
            get
            {
                return this._attrDZDWQDW;
            }
            set
            {
                this._attrDZDWQDW = value;
            }
        }

        /// <summary>
        /// 外箱贴标
        /// </summary>
        public int? AttrWXTB
        {
            get
            {
                return this._attrWXTB;
            }
            set
            {
                this._attrWXTB = value;
            }
        }

        /// <summary>
        /// 脚尾圆滑无肉利
        /// </summary>
        public int? AttrJWYHWRL
        {
            get
            {
                return this._attrJWYHWRL;
            }
            set
            {
                this._attrJWYHWRL = value;
            }
        }

        /// <summary>
        /// 正麦侧麦
        /// </summary>
        public int? AttrZMCM
        {
            get
            {
                return this._attrZMCM;
            }
            set
            {
                this._attrZMCM = value;
            }
        }

        /// <summary>
        /// 灌嘴不可有肉利
        /// </summary>
        public int? AttrGZBKYRL
        {
            get
            {
                return this._attrGZBKYRL;
            }
            set
            {
                this._attrGZBKYRL = value;
            }
        }

        /// <summary>
        /// 塑料袋是否密封
        /// </summary>
        public int? AttrSLDSFMF
        {
            get
            {
                return this._attrSLDSFMF;
            }
            set
            {
                this._attrSLDSFMF = value;
            }
        }

        /// <summary>
        /// 正支无变形干净
        /// </summary>
        public int? AttrZZWBXGJ
        {
            get
            {
                return this._attrZZWBXGJ;
            }
            set
            {
                this._attrZZWBXGJ = value;
            }
        }

        /// <summary>
        /// 内盒吊卡是否正确
        /// </summary>
        public int? AttrNHDQSFZQ
        {
            get
            {
                return this._attrNHDQSFZQ;
            }
            set
            {
                this._attrNHDQSFZQ = value;
            }
        }

        /// <summary>
        /// 镜片不可刮伤
        /// </summary>
        public int? AttrJPBKGS
        {
            get
            {
                return this._attrJPBKGS;
            }
            set
            {
                this._attrJPBKGS = value;
            }
        }

        /// <summary>
        /// 内盒贴标
        /// </summary>
        public int? AttrNHTB
        {
            get
            {
                return this._attrNHTB;
            }
            set
            {
                this._attrNHTB = value;
            }
        }

        /// <summary>
        /// 镜片(脚)记号正确
        /// </summary>
        public int? AttrJPJHZQ
        {
            get
            {
                return this._attrJPJHZQ;
            }
            set
            {
                this._attrJPJHZQ = value;
            }
        }

        /// <summary>
        /// 镜绳是否正确
        /// </summary>
        public int? AttrJSSFZQ
        {
            get
            {
                return this._attrJSSFZQ;
            }
            set
            {
                this._attrJSSFZQ = value;
            }
        }

        /// <summary>
        /// 镜片(脚)色系
        /// </summary>
        public int? AttrJPSX
        {
            get
            {
                return this._attrJPSX;
            }
            set
            {
                this._attrJPSX = value;
            }
        }

        /// <summary>
        /// 镜袋置入方式
        /// </summary>
        public int? AttrJDZRFS
        {
            get
            {
                return this._attrJDZRFS;
            }
            set
            {
                this._attrJDZRFS = value;
            }
        }

        /// <summary>
        /// 镜脚是否太松摇晃
        /// </summary>
        public int? AttrJJSFTSYH
        {
            get
            {
                return this._attrJJSFTSYH;
            }
            set
            {
                this._attrJJSFTSYH = value;
            }
        }

        /// <summary>
        /// 泡壳置入方式
        /// </summary>
        public int? AttrPKZRFS
        {
            get
            {
                return this._attrPKZRFS;
            }
            set
            {
                this._attrPKZRFS = value;
            }
        }

        /// <summary>
        /// 光学
        /// </summary>
        public int? AttrGX
        {
            get
            {
                return this._attrGX;
            }
            set
            {
                this._attrGX = value;
            }
        }

        /// <summary>
        /// 塑料袋/内盒/外箱条码贴标是否正确
        /// </summary>
        public int? AttrSLDNHWXTMSFZQ
        {
            get
            {
                return this._attrSLDNHWXTMSFZQ;
            }
            set
            {
                this._attrSLDNHWXTMSFZQ = value;
            }
        }

        /// <summary>
        /// 透视率
        /// </summary>
        public int? AttrTSL
        {
            get
            {
                return this._attrTSL;
            }
            set
            {
                this._attrTSL = value;
            }
        }

        /// <summary>
        /// 冲击标准
        /// </summary>
        public int? AttrCJBZ
        {
            get
            {
                return this._attrCJBZ;
            }
            set
            {
                this._attrCJBZ = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Employee1Id
        {
            get
            {
                return this._employee1Id;
            }
            set
            {
                this._employee1Id = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PronoteHeaderID
        {
            get
            {
                return this._pronoteHeaderID;
            }
            set
            {
                this._pronoteHeaderID = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustomerProductName
        {
            get
            {
                return this._customerProductName;
            }
            set
            {
                this._customerProductName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsMuShiJianYan
        {
            get
            {
                return this._isMuShiJianYan;
            }
            set
            {
                this._isMuShiJianYan = value;
            }
        }

        public int? AuditState
        {
            get
            {
                return this._auditState;
            }
            set
            {
                this._auditState = value;
            }
        }

        public virtual string AuditEmpId
        {
            get
            {
                return this._auditEmpId;

            }
            set
            {
                this._auditEmpId = value;
            }
        }

        public string ProductUnitId
        {
            get { return _ProductUnitId; }
            set { _ProductUnitId = value; }
        }

        public virtual Employee AuditEmp
        {
            get
            {
                return this._auditEmp;
            }
            set
            {
                this._auditEmp = value;
            }

        }

        /// <summary>
        /// 生产通知头
        /// </summary>
        public virtual PronoteHeader PronoteHeader
        {
            get
            {
                return this._pronoteHeader;
            }
            set
            {
                this._pronoteHeader = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee0
        {
            get
            {
                return this._employee0;
            }
            set
            {
                this._employee0 = value;
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
        /// 工作中心
        /// </summary>
        public virtual WorkHouse WorkHouse
        {
            get
            {
                return this._workHouse;
            }
            set
            {
                this._workHouse = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee1
        {
            get
            {
                return this._employee1;
            }
            set
            {
                this._employee1 = value;
            }

        }

        public ProductUnit ProductUnit
        {
            get { return _ProductUnit; }
            set { _ProductUnit = value; }
        }

        private string _EmployeeCheckId1;

        public string EmployeeCheckId1
        {
            get { return _EmployeeCheckId1; }
            set { _EmployeeCheckId1 = value; }
        }
        public readonly static string PRO_EmployeeCheckId1 = "EmployeeCheckId1";

        private string _EmployeeCheckId2;
        public string EmployeeCheckId2
        {
            get { return _EmployeeCheckId2; }
            set { _EmployeeCheckId2 = value; }
        }
        public readonly static string PRO_EmployeeCheckId2 = "EmployeeCheckId2";

        private string _EmployeeCheckId3;
        public string EmployeeCheckId3
        {
            get { return _EmployeeCheckId3; }
            set { _EmployeeCheckId3 = value; }
        }
        public readonly static string PRO_EmployeeCheckId3 = "EmployeeCheckId3";

        private string _EmployeeCheckId4;
        public string EmployeeCheckId4
        {
            get { return _EmployeeCheckId4; }
            set { _EmployeeCheckId4 = value; }
        }
        public readonly static string PRO_EmployeeCheckId4 = "EmployeeCheckId4";

        private string _EmployeeCheckId5;
        public string EmployeeCheckId5
        {
            get { return _EmployeeCheckId5; }
            set { _EmployeeCheckId5 = value; }
        }
        public readonly static string PRO_EmployeeCheckId5 = "EmployeeCheckId5";

        private Employee _EmployeeCheck1;

        public Employee EmployeeCheck1
        {
            get { return _EmployeeCheck1; }
            set { _EmployeeCheck1 = value; }
        }

        private Employee _EmployeeCheck2;

        public Employee EmployeeCheck2
        {
            get { return _EmployeeCheck2; }
            set { _EmployeeCheck2 = value; }
        }

        private Employee _EmployeeCheck3;

        public Employee EmployeeCheck3
        {
            get { return _EmployeeCheck3; }
            set { _EmployeeCheck3 = value; }
        }

        private Employee _EmployeeCheck4;

        public Employee EmployeeCheck4
        {
            get { return _EmployeeCheck4; }
            set { _EmployeeCheck4 = value; }
        }

        private Employee _EmployeeCheck5;

        public Employee EmployeeCheck5
        {
            get { return _EmployeeCheck5; }
            set { _EmployeeCheck5 = value; }
        }

        /// <summary>
        /// 主键编号
        /// </summary>
        public readonly static string PRO_PCFinishCheckID = "PCFinishCheckID";

        /// <summary>
        /// 商品编号
        /// </summary>
        public readonly static string PRO_ProductId = "ProductId";

        /// <summary>
        /// 工作中心编号
        /// </summary>
        public readonly static string PRO_WorkHouseId = "WorkHouseId";

        /// <summary>
        /// 操作人员
        /// </summary>
        public readonly static string PRO_Employee0Id = "Employee0Id";

        /// <summary>
        /// 插入时间
        /// </summary>
        public readonly static string PRO_InsertTime = "InsertTime";

        /// <summary>
        /// 修改时间
        /// </summary>
        public readonly static string PRO_UpdateTime = "UpdateTime";

        /// <summary>
        /// 用户订单编号
        /// </summary>
        public readonly static string PRO_InvoiceCusXOId = "InvoiceCusXOId";

        /// <summary>
        /// 单据日期
        /// </summary>
        public readonly static string PRO_PCFinishCheckDate = "PCFinishCheckDate";

        /// <summary>
        /// 数量
        /// </summary>
        public readonly static string PRO_PCFinishCheckCount = "PCFinishCheckCount";

        /// <summary>
        /// 入库日期数量
        /// </summary>
        public readonly static string PRO_PCFinishCheckInCoiunt = "PCFinishCheckInCoiunt";

        /// <summary>
        /// 备注
        /// </summary>
        public readonly static string PRO_PCFinishCheckDesc = "PCFinishCheckDesc";

        /// <summary>
        /// 豆仔点完全定位
        /// </summary>
        public readonly static string PRO_AttrDZDWQDW = "AttrDZDWQDW";

        /// <summary>
        /// 外箱贴标
        /// </summary>
        public readonly static string PRO_AttrWXTB = "AttrWXTB";

        /// <summary>
        /// 脚尾圆滑无肉利
        /// </summary>
        public readonly static string PRO_AttrJWYHWRL = "AttrJWYHWRL";

        /// <summary>
        /// 正麦侧麦
        /// </summary>
        public readonly static string PRO_AttrZMCM = "AttrZMCM";

        /// <summary>
        /// 灌嘴不可有肉利
        /// </summary>
        public readonly static string PRO_AttrGZBKYRL = "AttrGZBKYRL";

        /// <summary>
        /// 塑料袋是否密封
        /// </summary>
        public readonly static string PRO_AttrSLDSFMF = "AttrSLDSFMF";

        /// <summary>
        /// 正支无变形干净
        /// </summary>
        public readonly static string PRO_AttrZZWBXGJ = "AttrZZWBXGJ";

        /// <summary>
        /// 内盒吊卡是否正确
        /// </summary>
        public readonly static string PRO_AttrNHDQSFZQ = "AttrNHDQSFZQ";

        /// <summary>
        /// 镜片不可刮伤
        /// </summary>
        public readonly static string PRO_AttrJPBKGS = "AttrJPBKGS";

        /// <summary>
        /// 内盒贴标
        /// </summary>
        public readonly static string PRO_AttrNHTB = "AttrNHTB";

        /// <summary>
        /// 镜片(脚)记号正确
        /// </summary>
        public readonly static string PRO_AttrJPJHZQ = "AttrJPJHZQ";

        /// <summary>
        /// 镜绳是否正确
        /// </summary>
        public readonly static string PRO_AttrJSSFZQ = "AttrJSSFZQ";

        /// <summary>
        /// 镜片(脚)色系
        /// </summary>
        public readonly static string PRO_AttrJPSX = "AttrJPSX";

        /// <summary>
        /// 镜袋置入方式
        /// </summary>
        public readonly static string PRO_AttrJDZRFS = "AttrJDZRFS";

        /// <summary>
        /// 镜脚是否太松摇晃
        /// </summary>
        public readonly static string PRO_AttrJJSFTSYH = "AttrJJSFTSYH";

        /// <summary>
        /// 泡壳置入方式
        /// </summary>
        public readonly static string PRO_AttrPKZRFS = "AttrPKZRFS";

        /// <summary>
        /// 光学
        /// </summary>
        public readonly static string PRO_AttrGX = "AttrGX";

        /// <summary>
        /// 塑料袋/内盒/外箱条码贴标是否正确
        /// </summary>
        public readonly static string PRO_AttrSLDNHWXTMSFZQ = "AttrSLDNHWXTMSFZQ";

        /// <summary>
        /// 透视率
        /// </summary>
        public readonly static string PRO_AttrTSL = "AttrTSL";

        /// <summary>
        /// 冲击标准
        /// </summary>
        public readonly static string PRO_AttrCJBZ = "AttrCJBZ";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Employee1Id = "Employee1Id";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_PronoteHeaderID = "PronoteHeaderID";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_CustomerProductName = "CustomerProductName";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_IsMuShiJianYan = "IsMuShiJianYan";

        public readonly static string PRO_AuditState = "AuditState";

        public readonly static string PRO_AuditEmpId = "AuditEmpId";

        public readonly static string PRO_ProductUnitId = "ProductUnitId";
        #endregion

        private int? _AttrChanpinyanse;

        public int? AttrChanpinyanse
        {
            get { return _AttrChanpinyanse; }
            set { _AttrChanpinyanse = value; }
        }

        private int? _AttrChanpinjihao;

        public int? AttrChanpinjihao
        {
            get { return _AttrChanpinjihao; }
            set { _AttrChanpinjihao = value; }
        }

        private int? _AttrQitapeijianjihao;

        public int? AttrQitapeijianjihao
        {
            get { return _AttrQitapeijianjihao; }
            set { _AttrQitapeijianjihao = value; }
        }

        private int? _AttrErhuzuzhuang;

        public int? AttrErhuzuzhuang
        {
            get { return _AttrErhuzuzhuang; }
            set { _AttrErhuzuzhuang = value; }
        }

        private int? _AttrSujiaodaikuanxing;

        public int? AttrSujiaodaikuanxing
        {
            get { return _AttrSujiaodaikuanxing; }
            set { _AttrSujiaodaikuanxing = value; }
        }

        private int? _AttrSujiaodaitiebiao;

        public int? AttrSujiaodaitiebiao
        {
            get { return _AttrSujiaodaitiebiao; }
            set { _AttrSujiaodaitiebiao = value; }
        }

        private int? _AttrNeihekuanxing;

        public int? AttrNeihekuanxing
        {
            get { return _AttrNeihekuanxing; }
            set { _AttrNeihekuanxing = value; }
        }

        private int? _AttrChuhuochongji;

        public int? AttrChuhuochongji
        {
            get { return _AttrChuhuochongji; }
            set { _AttrChuhuochongji = value; }
        }

        private string _AttrCheckStandard;

        public string AttrCheckStandard
        {
            get { return _AttrCheckStandard; }
            set { _AttrCheckStandard = value; }
        }

        public readonly static string PRO_AttrChanpinyanse = "AttrChanpinyanse";
        public readonly static string PRO_AttrChanpinjihao = "AttrChanpinjihao";
        public readonly static string PRO_AttrQitapeijianjihao = "AttrQitapeijianjihao";
        public readonly static string PRO_AttrErhuzuzhuang = "AttrErhuzuzhuang";
        public readonly static string PRO_AttrSujiaodaikuanxing = "AttrSujiaodaikuanxing";
        public readonly static string PRO_AttrSujiaodaitiebiao = "AttrSujiaodaitiebiao";
        public readonly static string PRO_AttrNeihekuanxing = "AttrNeihekuanxing";
        public readonly static string PRO_AttrChuhuochongji = "AttrChuhuochongji";
    }
}
