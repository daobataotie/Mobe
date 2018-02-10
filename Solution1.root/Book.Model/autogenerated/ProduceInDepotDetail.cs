﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceInDepotDetail.autogenerated.cs
// author: mayanjun
// create date：2012-12-21 15:58:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class ProduceInDepotDetail
	{
		#region Data

		/// <summary>
		/// 编号
		/// </summary>
		private string _produceInDepotDetailId;
		
		/// <summary>
		/// 位置编号
		/// </summary>
		private string _depotPositionId;
		
		/// <summary>
		/// 商品编号
		/// </summary>
		private string _productId;
		
		/// <summary>
		/// 生产入库编号
		/// </summary>
		private string _produceInDepotId;
		
		/// <summary>
		/// 规格
		/// </summary>
		private string _productGuige;
		
		/// <summary>
		/// 入库数量
		/// </summary>
		private double? _produceQuantity;
		
		/// <summary>
		/// 单价
		/// </summary>
		private decimal? _producePrice;
		
		/// <summary>
		/// 金额
		/// </summary>
		private decimal? _produceMoney;
		
		/// <summary>
		/// 入库单价
		/// </summary>
		private decimal? _produceInDepotPrice;
		
		/// <summary>
		/// 销售订单编号
		/// </summary>
		private string _invoiceXOId;
		
		/// <summary>
		/// 通知单编号
		/// </summary>
		private string _pronoteHeaderId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _invoiceXODetailId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _productUnit;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _produceTransferQuantity;
		
		/// <summary>
		/// 
		/// </summary>
		private bool? _isChecked;
		
		/// <summary>
		/// 
		/// </summary>
		private string _proceduresId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _productProceId;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _heJiProceduresSum;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _heJiCheckOutSum;
		
		/// <summary>
		/// 
		/// </summary>
		private string _businessHoursType;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _rejectionRate;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _heiDian;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _zaZhi;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _invoiceQuantity;
		
		/// <summary>
		/// 生产数量
		/// </summary>
		private double? _proceduresSum;
		
		/// <summary>
		/// 合格数量
		/// </summary>
		private double? _checkOutSum;
		
		/// <summary>
		/// 
		/// </summary>
		private string _workHouseId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _detailDesc;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _beforeTransferQuantity;
		
		/// <summary>
		/// 
		/// </summary>
		private double? _pronoteHeaderSum;
		
		/// <summary>
		/// 砲管問題
		/// </summary>
		private double? _mPaoguanwenti;
		
		/// <summary>
		/// 晶点固定点
		/// </summary>
		private double? _mJingdiangudingdian;
		
		/// <summary>
		/// 插片擦伤
		/// </summary>
		private double? _mChapiancashang;
		
		/// <summary>
		/// 挽模擦伤
		/// </summary>
		private double? _mWanMocashang;
		
		/// <summary>
		/// 縮水
		/// </summary>
		private double? _mSuoShui;
		
		/// <summary>
		/// 滑板擦伤
		/// </summary>
		private double? _mHuabancashang;
		
		/// <summary>
		/// 强化防雾线
		/// </summary>
		private double? _mQianghuafangwuxian;
		
		/// <summary>
		/// 白烟黑烟
		/// </summary>
		private double? _mBaiyanHeiYan;
		
		/// <summary>
		/// 结合线回纹
		/// </summary>
		private double? _mJieHeXianHuiwen;
		
		/// <summary>
		/// 原料问题
		/// </summary>
		private double? _mYuanliaowenti;
		
		/// <summary>
		/// 氣泡
		/// </summary>
		private double? _mQiPao;
		
		/// <summary>
		/// 射出其他
		/// </summary>
		private double? _mShechuqita;
		
		/// <summary>
		/// 怪手撞傷
		/// </summary>
		private double? _mGuaiShouZhuangShang;
		
		/// <summary>
		/// 拆片擦伤
		/// </summary>
		private double? _mChaipiancashang;
		
		/// <summary>
		/// 擦模損壞
		/// </summary>
		private double? _mCaMoSunHua;
		
		/// <summary>
		/// 抽料问题
		/// </summary>
		private double? _mChouliaowenti;
		
		/// <summary>
		/// 黑点杂质
		/// </summary>
		private double? _mHeidianzazhi;
		
		/// <summary>
		/// 强化前擦伤
		/// </summary>
		private double? _mQianghuaqiancashang;
		
		/// <summary>
		/// 颗粒棉絮
		/// </summary>
		private double? _mKeLimianxu;
		
		/// <summary>
		/// 流痕
		/// </summary>
		private double? _mLiuheng;
		
		/// <summary>
		/// 喷药滴药
		/// </summary>
		private double? _mPengYaodiyao;
		
		/// <summary>
		/// 過火雞爪
		/// </summary>
		private double? _mGuohuojizhua;
		
		/// <summary>
		/// 油点
		/// </summary>
		private double? _mYoudian;
		
		/// <summary>
		/// 厂商不良
		/// </summary>
		private double? _mChangshangbuliang;
		
		/// <summary>
		/// 强化其他
		/// </summary>
		private double? _mQianghuaQiTa;
		
		/// <summary>
		/// 
		/// </summary>
		private int? _inumber;
		
		/// <summary>
		/// 合计转生产数量
		/// </summary>
		private double? _heJiProduceTransferQuantity;
		
		/// <summary>
		/// 合计入库数量
		/// </summary>
		private double? _heJiProduceQuantity;
		
		/// <summary>
		/// 组装擦伤
		/// </summary>
		private double? _mZuzhuangcashang;
		
		/// <summary>
		/// 含药
		/// </summary>
		private double? _mHanyao;
		
		/// <summary>
		/// 擦伤
		/// </summary>
		private double? _mCashang;
		
		/// <summary>
		/// 强化后擦伤
		/// </summary>
		private double? _mQianghuahoucashang;

        /// <summary>
        /// 手册号
        /// </summary>
        private string _handbookId;

        /// <summary>
        /// 手册项号
        /// </summary>
        private string _handbookProductId;

        private double? _heJiBeforeTransferQuantity;
		
		/// <summary>
		/// 库库货位
		/// </summary>
		private DepotPosition _depotPosition;
		/// <summary>
		/// 标准工序
		/// </summary>
		private Procedures _procedures;
		/// <summary>
		/// 生产入库
		/// </summary>
		private ProduceInDepot _produceInDepot;
		/// <summary>
		/// 产品
		/// </summary>
		private Product _product;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 编号
		/// </summary>
		public string ProduceInDepotDetailId
		{
			get 
			{
				return this._produceInDepotDetailId;
			}
			set 
			{
				this._produceInDepotDetailId = value;
			}
		}

		/// <summary>
		/// 位置编号
		/// </summary>
		public string DepotPositionId
		{
			get 
			{
				return this._depotPositionId;
			}
			set 
			{
				this._depotPositionId = value;
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
		/// 生产入库编号
		/// </summary>
		public string ProduceInDepotId
		{
			get 
			{
				return this._produceInDepotId;
			}
			set 
			{
				this._produceInDepotId = value;
			}
		}

		/// <summary>
		/// 规格
		/// </summary>
		public string ProductGuige
		{
			get 
			{
				return this._productGuige;
			}
			set 
			{
				this._productGuige = value;
			}
		}

		/// <summary>
		/// 入库数量
		/// </summary>
		public double? ProduceQuantity
		{
			get 
			{
				return this._produceQuantity;
			}
			set 
			{
				this._produceQuantity = value;
			}
		}

		/// <summary>
		/// 单价
		/// </summary>
		public decimal? ProducePrice
		{
			get 
			{
				return this._producePrice;
			}
			set 
			{
				this._producePrice = value;
			}
		}

		/// <summary>
		/// 金额
		/// </summary>
		public decimal? ProduceMoney
		{
			get 
			{
				return this._produceMoney;
			}
			set 
			{
				this._produceMoney = value;
			}
		}

		/// <summary>
		/// 入库单价
		/// </summary>
		public decimal? ProduceInDepotPrice
		{
			get 
			{
				return this._produceInDepotPrice;
			}
			set 
			{
				this._produceInDepotPrice = value;
			}
		}

		/// <summary>
		/// 销售订单编号
		/// </summary>
		public string InvoiceXOId
		{
			get 
			{
				return this._invoiceXOId;
			}
			set 
			{
				this._invoiceXOId = value;
			}
		}

		/// <summary>
		/// 通知单编号
		/// </summary>
		public string PronoteHeaderId
		{
			get 
			{
				return this._pronoteHeaderId;
			}
			set 
			{
				this._pronoteHeaderId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string InvoiceXODetailId
		{
			get 
			{
				return this._invoiceXODetailId;
			}
			set 
			{
				this._invoiceXODetailId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ProductUnit
		{
			get 
			{
				return this._productUnit;
			}
			set 
			{
				this._productUnit = value;
			}
		}

		/// <summary>
		/// 转生产数量
		/// </summary>
		public double? ProduceTransferQuantity
		{
			get 
			{
				return this._produceTransferQuantity;
			}
			set 
			{
				this._produceTransferQuantity = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool? IsChecked
		{
			get 
			{
				return this._isChecked;
			}
			set 
			{
				this._isChecked = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ProceduresId
		{
			get 
			{
				return this._proceduresId;
			}
			set 
			{
				this._proceduresId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ProductProceId
		{
			get 
			{
				return this._productProceId;
			}
			set 
			{
				this._productProceId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double? HeJiProceduresSum
		{
			get 
			{
				return this._heJiProceduresSum;
			}
			set 
			{
				this._heJiProceduresSum = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double? HeJiCheckOutSum
		{
			get 
			{
				return this._heJiCheckOutSum;
			}
			set 
			{
				this._heJiCheckOutSum = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string BusinessHoursType
		{
			get 
			{
				return this._businessHoursType;
			}
			set 
			{
				this._businessHoursType = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double? RejectionRate
		{
			get 
			{
				return this._rejectionRate;
			}
			set 
			{
				this._rejectionRate = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double? HeiDian
		{
			get 
			{
				return this._heiDian;
			}
			set 
			{
				this._heiDian = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double? ZaZhi
		{
			get 
			{
				return this._zaZhi;
			}
			set 
			{
				this._zaZhi = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double? InvoiceQuantity
		{
			get 
			{
				return this._invoiceQuantity;
			}
			set 
			{
				this._invoiceQuantity = value;
			}
		}

		/// <summary>
		/// 生产数量
		/// </summary>
		public double? ProceduresSum
		{
			get 
			{
				return this._proceduresSum;
			}
			set 
			{
				this._proceduresSum = value;
			}
		}

		/// <summary>
		/// 合格数量
		/// </summary>
		public double? CheckOutSum
		{
			get 
			{
				return this._checkOutSum;
			}
			set 
			{
				this._checkOutSum = value;
			}
		}

		/// <summary>
		/// 
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
		/// 
		/// </summary>
		public string DetailDesc
		{
			get 
			{
				return this._detailDesc;
			}
			set 
			{
				this._detailDesc = value;
			}
		}

		/// <summary>
		/// 前一天合计前单位转入
		/// </summary>
		public double? beforeTransferQuantity
		{
			get 
			{
				return this._beforeTransferQuantity;
			}
			set 
			{
				this._beforeTransferQuantity = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double? PronoteHeaderSum
		{
			get 
			{
				return this._pronoteHeaderSum;
			}
			set 
			{
				this._pronoteHeaderSum = value;
			}
		}

		/// <summary>
		/// 砲管問題
		/// </summary>
		public double? mPaoguanwenti
		{
			get 
			{
				return this._mPaoguanwenti;
			}
			set 
			{
				this._mPaoguanwenti = value;
			}
		}

		/// <summary>
		/// 晶点固定点
		/// </summary>
		public double? mJingdiangudingdian
		{
			get 
			{
				return this._mJingdiangudingdian;
			}
			set 
			{
				this._mJingdiangudingdian = value;
			}
		}

		/// <summary>
		/// 插片擦伤
		/// </summary>
		public double? mChapiancashang
		{
			get 
			{
				return this._mChapiancashang;
			}
			set 
			{
				this._mChapiancashang = value;
			}
		}

		/// <summary>
		/// 挽模擦伤
		/// </summary>
		public double? mWanMocashang
		{
			get 
			{
				return this._mWanMocashang;
			}
			set 
			{
				this._mWanMocashang = value;
			}
		}

		/// <summary>
		/// 縮水
		/// </summary>
		public double? mSuoShui
		{
			get 
			{
				return this._mSuoShui;
			}
			set 
			{
				this._mSuoShui = value;
			}
		}

		/// <summary>
		/// 滑板擦伤
		/// </summary>
		public double? mHuabancashang
		{
			get 
			{
				return this._mHuabancashang;
			}
			set 
			{
				this._mHuabancashang = value;
			}
		}

		/// <summary>
		/// 强化防雾线
		/// </summary>
		public double? mQianghuafangwuxian
		{
			get 
			{
				return this._mQianghuafangwuxian;
			}
			set 
			{
				this._mQianghuafangwuxian = value;
			}
		}

		/// <summary>
		/// 白烟黑烟
		/// </summary>
		public double? mBaiyanHeiYan
		{
			get 
			{
				return this._mBaiyanHeiYan;
			}
			set 
			{
				this._mBaiyanHeiYan = value;
			}
		}

		/// <summary>
		/// 结合线回纹
		/// </summary>
		public double? mJieHeXianHuiwen
		{
			get 
			{
				return this._mJieHeXianHuiwen;
			}
			set 
			{
				this._mJieHeXianHuiwen = value;
			}
		}

		/// <summary>
		/// 原料问题
		/// </summary>
		public double? mYuanliaowenti
		{
			get 
			{
				return this._mYuanliaowenti;
			}
			set 
			{
				this._mYuanliaowenti = value;
			}
		}

		/// <summary>
		/// 氣泡
		/// </summary>
		public double? mQiPao
		{
			get 
			{
				return this._mQiPao;
			}
			set 
			{
				this._mQiPao = value;
			}
		}

		/// <summary>
		/// 射出其他
		/// </summary>
		public double? mShechuqita
		{
			get 
			{
				return this._mShechuqita;
			}
			set 
			{
				this._mShechuqita = value;
			}
		}

		/// <summary>
		/// 怪手撞傷
		/// </summary>
		public double? mGuaiShouZhuangShang
		{
			get 
			{
				return this._mGuaiShouZhuangShang;
			}
			set 
			{
				this._mGuaiShouZhuangShang = value;
			}
		}

		/// <summary>
		/// 拆片擦伤
		/// </summary>
		public double? mChaipiancashang
		{
			get 
			{
				return this._mChaipiancashang;
			}
			set 
			{
				this._mChaipiancashang = value;
			}
		}

		/// <summary>
		/// 擦模損壞
		/// </summary>
		public double? mCaMoSunHua
		{
			get 
			{
				return this._mCaMoSunHua;
			}
			set 
			{
				this._mCaMoSunHua = value;
			}
		}

		/// <summary>
		/// 抽料问题
		/// </summary>
		public double? mChouliaowenti
		{
			get 
			{
				return this._mChouliaowenti;
			}
			set 
			{
				this._mChouliaowenti = value;
			}
		}

		/// <summary>
		/// 黑点杂质
		/// </summary>
		public double? mHeidianzazhi
		{
			get 
			{
				return this._mHeidianzazhi;
			}
			set 
			{
				this._mHeidianzazhi = value;
			}
		}

		/// <summary>
		/// 强化前擦伤
		/// </summary>
		public double? mQianghuaqiancashang
		{
			get 
			{
				return this._mQianghuaqiancashang;
			}
			set 
			{
				this._mQianghuaqiancashang = value;
			}
		}

		/// <summary>
		/// 颗粒棉絮
		/// </summary>
		public double? mKeLimianxu
		{
			get 
			{
				return this._mKeLimianxu;
			}
			set 
			{
				this._mKeLimianxu = value;
			}
		}

		/// <summary>
		/// 流痕
		/// </summary>
		public double? mLiuheng
		{
			get 
			{
				return this._mLiuheng;
			}
			set 
			{
				this._mLiuheng = value;
			}
		}

		/// <summary>
		/// 喷药滴药
		/// </summary>
		public double? mPengYaodiyao
		{
			get 
			{
				return this._mPengYaodiyao;
			}
			set 
			{
				this._mPengYaodiyao = value;
			}
		}

		/// <summary>
		/// 過火雞爪
		/// </summary>
		public double? mGuohuojizhua
		{
			get 
			{
				return this._mGuohuojizhua;
			}
			set 
			{
				this._mGuohuojizhua = value;
			}
		}

		/// <summary>
		/// 油点
		/// </summary>
		public double? mYoudian
		{
			get 
			{
				return this._mYoudian;
			}
			set 
			{
				this._mYoudian = value;
			}
		}

		/// <summary>
		/// 厂商不良
		/// </summary>
		public double? mChangshangbuliang
		{
			get 
			{
				return this._mChangshangbuliang;
			}
			set 
			{
				this._mChangshangbuliang = value;
			}
		}

		/// <summary>
		/// 强化其他
		/// </summary>
		public double? mQianghuaQiTa
		{
			get 
			{
				return this._mQianghuaQiTa;
			}
			set 
			{
				this._mQianghuaQiTa = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int? Inumber
		{
			get 
			{
				return this._inumber;
			}
			set 
			{
				this._inumber = value;
			}
		}

		/// <summary>
		/// 合计转生产数量
		/// </summary>
		public double? HeJiProduceTransferQuantity
		{
			get 
			{
				return this._heJiProduceTransferQuantity;
			}
			set 
			{
				this._heJiProduceTransferQuantity = value;
			}
		}

		/// <summary>
		/// 合计入库数量
		/// </summary>
		public double? HeJiProduceQuantity
		{
			get 
			{
				return this._heJiProduceQuantity;
			}
			set 
			{
				this._heJiProduceQuantity = value;
			}
		}

		/// <summary>
		/// 组装擦伤
		/// </summary>
		public double? mZuzhuangcashang
		{
			get 
			{
				return this._mZuzhuangcashang;
			}
			set 
			{
				this._mZuzhuangcashang = value;
			}
		}

		/// <summary>
		/// 含药
		/// </summary>
		public double? mHanyao
		{
			get 
			{
				return this._mHanyao;
			}
			set 
			{
				this._mHanyao = value;
			}
		}

		/// <summary>
		/// 擦伤
		/// </summary>
		public double? mCashang
		{
			get 
			{
				return this._mCashang;
			}
			set 
			{
				this._mCashang = value;
			}
		}

		/// <summary>
		/// 强化后擦伤
		/// </summary>
		public double? mQianghuahoucashang
		{
			get 
			{
				return this._mQianghuahoucashang;
			}
			set 
			{
				this._mQianghuahoucashang = value;
			}
		}

        /// <summary>
        /// 手册号
        /// </summary>
        public string HandbookId
        {
            get { return _handbookId; }
            set { _handbookId = value; }
        }

        /// <summary>
        /// 手册项号
        /// </summary>
        public string HandbookProductId
        {
            get { return _handbookProductId; }
            set { _handbookProductId = value; }
        }

        /// <summary>
        /// 总计前单位转入
        /// </summary>
        public double? HeJiBeforeTransferQuantity
        {
            get { return _heJiBeforeTransferQuantity; }
            set { _heJiBeforeTransferQuantity = value; }
        }
	
		/// <summary>
		/// 库库货位
		/// </summary>
		public virtual DepotPosition DepotPosition
		{
			get
			{
				return this._depotPosition;
			}
			set
			{
				this._depotPosition = value;
			}
			
		}
		/// <summary>
		/// 标准工序
		/// </summary>
		public virtual Procedures Procedures
		{
			get
			{
				return this._procedures;
			}
			set
			{
				this._procedures = value;
			}
			
		}
		/// <summary>
		/// 生产入库
		/// </summary>
		public virtual ProduceInDepot ProduceInDepot
		{
			get
			{
				return this._produceInDepot;
			}
			set
			{
				this._produceInDepot = value;
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
		/// 编号
		/// </summary>
		public readonly static string PRO_ProduceInDepotDetailId = "ProduceInDepotDetailId";
		
		/// <summary>
		/// 位置编号
		/// </summary>
		public readonly static string PRO_DepotPositionId = "DepotPositionId";
		
		/// <summary>
		/// 商品编号
		/// </summary>
		public readonly static string PRO_ProductId = "ProductId";
		
		/// <summary>
		/// 生产入库编号
		/// </summary>
		public readonly static string PRO_ProduceInDepotId = "ProduceInDepotId";
		
		/// <summary>
		/// 规格
		/// </summary>
		public readonly static string PRO_ProductGuige = "ProductGuige";
		
		/// <summary>
		/// 数量
		/// </summary>
		public readonly static string PRO_ProduceQuantity = "ProduceQuantity";
		
		/// <summary>
		/// 单价
		/// </summary>
		public readonly static string PRO_ProducePrice = "ProducePrice";
		
		/// <summary>
		/// 金额
		/// </summary>
		public readonly static string PRO_ProduceMoney = "ProduceMoney";
		
		/// <summary>
		/// 入库单价
		/// </summary>
		public readonly static string PRO_ProduceInDepotPrice = "ProduceInDepotPrice";
		
		/// <summary>
		/// 销售订单编号
		/// </summary>
		public readonly static string PRO_InvoiceXOId = "InvoiceXOId";
		
		/// <summary>
		/// 通知单编号
		/// </summary>
		public readonly static string PRO_PronoteHeaderId = "PronoteHeaderId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InvoiceXODetailId = "InvoiceXODetailId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProductUnit = "ProductUnit";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProduceTransferQuantity = "ProduceTransferQuantity";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_IsChecked = "IsChecked";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProceduresId = "ProceduresId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProductProceId = "ProductProceId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_HeJiProceduresSum = "HeJiProceduresSum";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_HeJiCheckOutSum = "HeJiCheckOutSum";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_BusinessHoursType = "BusinessHoursType";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_RejectionRate = "RejectionRate";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_HeiDian = "HeiDian";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ZaZhi = "ZaZhi";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InvoiceQuantity = "InvoiceQuantity";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProceduresSum = "ProceduresSum";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_CheckOutSum = "CheckOutSum";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_WorkHouseId = "WorkHouseId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_DetailDesc = "DetailDesc";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_beforeTransferQuantity = "beforeTransferQuantity";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_PronoteHeaderSum = "PronoteHeaderSum";
		
		/// <summary>
		/// 砲管問題
		/// </summary>
		public readonly static string PRO_mPaoguanwenti = "mPaoguanwenti";
		
		/// <summary>
		/// 晶点固定点
		/// </summary>
		public readonly static string PRO_mJingdiangudingdian = "mJingdiangudingdian";
		
		/// <summary>
		/// 插片擦伤
		/// </summary>
		public readonly static string PRO_mChapiancashang = "mChapiancashang";
		
		/// <summary>
		/// 挽模擦伤
		/// </summary>
		public readonly static string PRO_mWanMocashang = "mWanMocashang";
		
		/// <summary>
		/// 縮水
		/// </summary>
		public readonly static string PRO_mSuoShui = "mSuoShui";
		
		/// <summary>
		/// 滑板擦伤
		/// </summary>
		public readonly static string PRO_mHuabancashang = "mHuabancashang";
		
		/// <summary>
		/// 强化防雾线
		/// </summary>
		public readonly static string PRO_mQianghuafangwuxian = "mQianghuafangwuxian";
		
		/// <summary>
		/// 白烟黑烟
		/// </summary>
		public readonly static string PRO_mBaiyanHeiYan = "mBaiyanHeiYan";
		
		/// <summary>
		/// 结合线回纹
		/// </summary>
		public readonly static string PRO_mJieHeXianHuiwen = "mJieHeXianHuiwen";
		
		/// <summary>
		/// 原料问题
		/// </summary>
		public readonly static string PRO_mYuanliaowenti = "mYuanliaowenti";
		
		/// <summary>
		/// 氣泡
		/// </summary>
		public readonly static string PRO_mQiPao = "mQiPao";
		
		/// <summary>
		/// 射出其他
		/// </summary>
		public readonly static string PRO_mShechuqita = "mShechuqita";
		
		/// <summary>
		/// 怪手撞傷
		/// </summary>
		public readonly static string PRO_mGuaiShouZhuangShang = "mGuaiShouZhuangShang";
		
		/// <summary>
		/// 拆片擦伤
		/// </summary>
		public readonly static string PRO_mChaipiancashang = "mChaipiancashang";
		
		/// <summary>
		/// 擦模損壞
		/// </summary>
		public readonly static string PRO_mCaMoSunHua = "mCaMoSunHua";
		
		/// <summary>
		/// 抽料问题
		/// </summary>
		public readonly static string PRO_mChouliaowenti = "mChouliaowenti";
		
		/// <summary>
		/// 黑点杂质
		/// </summary>
		public readonly static string PRO_mHeidianzazhi = "mHeidianzazhi";
		
		/// <summary>
		/// 强化前擦伤
		/// </summary>
		public readonly static string PRO_mQianghuaqiancashang = "mQianghuaqiancashang";
		
		/// <summary>
		/// 颗粒棉絮
		/// </summary>
		public readonly static string PRO_mKeLimianxu = "mKeLimianxu";
		
		/// <summary>
		/// 流痕
		/// </summary>
		public readonly static string PRO_mLiuheng = "mLiuheng";
		
		/// <summary>
		/// 喷药滴药
		/// </summary>
		public readonly static string PRO_mPengYaodiyao = "mPengYaodiyao";
		
		/// <summary>
		/// 過火雞爪
		/// </summary>
		public readonly static string PRO_mGuohuojizhua = "mGuohuojizhua";
		
		/// <summary>
		/// 油点
		/// </summary>
		public readonly static string PRO_mYoudian = "mYoudian";
		
		/// <summary>
		/// 厂商不良
		/// </summary>
		public readonly static string PRO_mChangshangbuliang = "mChangshangbuliang";
		
		/// <summary>
		/// 强化其他
		/// </summary>
		public readonly static string PRO_mQianghuaQiTa = "mQianghuaQiTa";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_Inumber = "Inumber";
		
		/// <summary>
		/// 合计转生产数量
		/// </summary>
		public readonly static string PRO_HeJiProduceTransferQuantity = "HeJiProduceTransferQuantity";
		
		/// <summary>
		/// 合计入库数量
		/// </summary>
		public readonly static string PRO_HeJiProduceQuantity = "HeJiProduceQuantity";
		
		/// <summary>
		/// 组装擦伤
		/// </summary>
		public readonly static string PRO_mZuzhuangcashang = "mZuzhuangcashang";
		
		/// <summary>
		/// 含药
		/// </summary>
		public readonly static string PRO_mHanyao = "mHanyao";
		
		/// <summary>
		/// 擦伤
		/// </summary>
		public readonly static string PRO_mCashang = "mCashang";
		
		/// <summary>
		/// 强化后擦伤
		/// </summary>
		public readonly static string PRO_mQianghuahoucashang = "mQianghuahoucashang";

        public readonly static string PRO_HandbookId = "HandbookId";

        public readonly static string PRO_HandbookProductId = "HandbookProductId";

        public readonly static string PRO_HeJiBeforeTransferQuantity = "HeJiBeforeTransferQuantity";

		#endregion
	}
}
