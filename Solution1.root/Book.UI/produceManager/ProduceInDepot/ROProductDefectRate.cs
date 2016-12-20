using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class ROProductDefectRate : DevExpress.XtraReports.UI.XtraReport
    {
        public ROProductDefectRate()
        {
            InitializeComponent();
        }

        public ROProductDefectRate(ChooseProductDefectRateCls condition)
            : this()
        {

            if (condition.StartProduct == null && condition.EndProduct == null)
            {
                throw new Helper.InvalidValueException("商品不能為空!請選擇商品");
            }

            if (!condition.attrJiLuFangShi)
            {
                this.TCRiQi_H.WidthF = 400;
                this.TCRiQi.WidthF = 400;
            }

            //CompanyInfo
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            string PrintYearAndMonth = condition.StartDate.ToString("yyyy-MM-dd") + "~" + condition.EndDate.ToString("yyyy-MM-dd");

            switch (condition.attrProductStates)
            {
                case 0:
                    this.lblReportName.Text = PrintYearAndMonth + "商品不良統計表";
                    break;
                case 1:
                    this.lblReportName.Text = "常態" + PrintYearAndMonth + "不良統計表";
                    break;
                case 2:
                    this.lblReportName.Text = "特殊" + PrintYearAndMonth + "不良統計表";
                    break;
            }

            this.lblDateRange.Text = PrintYearAndMonth;
            if (!(condition.StartProduct == null && condition.EndProduct == null))
            {
                if (condition.StartProduct != null && condition.EndProduct != null && condition.StartProduct.ProductId != condition.EndProduct.ProductId)
                    this.lblProductRange.Text = condition.StartProduct.ProductName + "  ~  " + condition.EndProduct.ProductName;
                else
                    this.lblProductRange.Text = condition.StartProduct == null ? condition.EndProduct.ProductName : condition.StartProduct.ProductName;
            }

            string ProductAttrs = string.Empty;
            ProductAttrs += condition.attrQiangHua ? "強化  " : " ";
            ProductAttrs += condition.attrWuDu ? "霧度  " : " ";
            ProductAttrs += condition.attrWuQiangHuaWuDu ? "無強化霧度  " : " ";
            this.lblProductAttrRange.Text = ProductAttrs;

            this.lblJiLuFangShi.Text = condition.attrJiLuFangShi ? "展開" : "合併";

            //构建合计Total
            DataTable dat = new BL.ProduceInDepotDetailManager().SUMDTSelect_ChooseDefectRateCls(condition.DateType, condition.StartDate, condition.EndDate, null, null, condition.StartProduct, condition.EndProduct, null, null, condition.StartWorkHouse, condition.EndWorkHouse, null, null, condition.attrJiLuFangShi, condition.attrQiangHua, condition.attrWuDu, condition.attrWuQiangHuaWuDu, 0, condition.RejectionRate, condition.RejectionRateCompare, condition.EnableBLV, 0, 0, condition.StarInvoiceXOId, condition.EndInvoiceXOId, condition.InvoiceStates);
            //绑定具体数据
            DataTable dt = new BL.ProduceInDepotDetailManager().DTSelect_ChooseDefectRateCls(condition.DateType, condition.StartDate, condition.EndDate, null, null, condition.StartProduct, condition.EndProduct, null, null, condition.StartWorkHouse, condition.EndWorkHouse, null, null, condition.attrJiLuFangShi, condition.attrQiangHua, condition.attrWuDu, condition.attrWuQiangHuaWuDu, 0, condition.RejectionRate, condition.RejectionRateCompare, condition.EnableBLV, 0, 0, condition.StarInvoiceXOId, condition.EndInvoiceXOId, condition.InvoiceStates);

            if (dt == null || dt.Rows.Count == 0)
                throw new Helper.InvalidValueException("無記錄");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dat.Rows.Count; i++)
            {
                sb.Append(dat.Rows[i]["Workhousename"] + "：總生產數量：" + dat.Rows[i]["ProceduresSum"] + " 合格數量：" + dat.Rows[i]["CheckOutSum"] + " 不良率：" + dat.Rows[i]["RejectionRate_1"] + "\t");
            }

            this.lblTotalDesc.Text = sb.ToString();

            this.DataSource = dt;

            //Details
            this.TCRiQi.DataBindings.Add("Text", this.DataSource, "ProduceInDepotDate");
            this.TCpingming.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TCgongsibumeng.DataBindings.Add("Text", this.DataSource, "Workhousename");
            this.TCshuliang.DataBindings.Add("Text", this.DataSource, "ProceduresSum");
            this.TChegeshuliang.DataBindings.Add("Text", this.DataSource, "CheckOutSum");
            this.TCbulianglv.DataBindings.Add("Text", this.DataSource, "RejectionRate_1");
            this.TCdanwei.DataBindings.Add("Text", this.DataSource, "ProductUnit");
            //this.TCriqi.DataBindings.Add("Text", this.DataSource, "ProduceInDepot." + Model.ProduceInDepot.PRO_ProduceInDepotDate);

            this.TCmYuanliaowenti.DataBindings.Add("Text", this.DataSource, "mYuanliaowenti");
            this.TCmChouliaowenti.DataBindings.Add("Text", this.DataSource, "mChouliaowenti");
            this.TCmPaoguanwenti.DataBindings.Add("Text", this.DataSource, "mPaoguanwenti");
            this.TCmJingdiangudingdian.DataBindings.Add("Text", this.DataSource, "mJingdiangudingdian");
            this.TCmChapiancashang.DataBindings.Add("Text", this.DataSource, "mChapiancashang");
            this.TCmWanMocashang.DataBindings.Add("Text", this.DataSource, "mWanMocashang");
            this.TCmGuaiShouZhuangShang.DataBindings.Add("Text", this.DataSource, "mGuaiShouZhuangShang");
            this.TCmHuabancashang.DataBindings.Add("Text", this.DataSource, "mHuabancashang");
            this.TCmGuohuojizhua.DataBindings.Add("Text", this.DataSource, "mGuohuojizhua");
            this.TCmBaiyanHeiYan.DataBindings.Add("Text", this.DataSource, "mBaiyanHeiYan");
            this.TCmJieHeXianHuiwen.DataBindings.Add("Text", this.DataSource, "mJieHeXianHuiwen");
            this.TCmSuoShui.DataBindings.Add("Text", this.DataSource, "mSuoShui");
            this.TCmQiPao.DataBindings.Add("Text", this.DataSource, "mQiPao");
            this.TCmShechuqita.DataBindings.Add("Text", this.DataSource, "mShechuqita");
            this.TCmCaMoSunHua.DataBindings.Add("Text", this.DataSource, "mCaMoSunHua");
            this.TCmChaipiancashang.DataBindings.Add("Text", this.DataSource, "mChaipiancashang");
            this.TCmHeidianzazhi.DataBindings.Add("Text", this.DataSource, "mHeidianzazhi");
            this.TCmQianghuaqiancashang.DataBindings.Add("Text", this.DataSource, "mQianghuaqiancashang");
            this.TCmQianghuahoucashang.DataBindings.Add("Text", this.DataSource, "mQianghuahoucashang");
            this.TCmHanyao.DataBindings.Add("Text", this.DataSource, "mHanyao");
            this.TCmKeLimianxu.DataBindings.Add("Text", this.DataSource, "mKeLimianxu");
            this.TCmLiuheng.DataBindings.Add("Text", this.DataSource, "mLiuheng");
            this.TCmPengYaodiyao.DataBindings.Add("Text", this.DataSource, "mPengYaodiyao");
            this.TCmQianghuafangwuxian.DataBindings.Add("Text", this.DataSource, "mQianghuafangwuxian");
            this.TCmYoudian.DataBindings.Add("Text", this.DataSource, "mYoudian");
            this.TCmQianghuaQiTa.DataBindings.Add("Text", this.DataSource, "mQianghuaQiTa");
            this.TCmChangshangbuliang.DataBindings.Add("Text", this.DataSource, "mChangshangbuliang");
            this.TCmZuzhuangcashang.DataBindings.Add("Text", this.DataSource, "mZuzhuangcashang");
            this.TCmCashang.DataBindings.Add("Text", this.DataSource, "mCashang");

            #region 绑定List
            //IList<Model.ProduceInDepotDetail> deitals = new BL.ProduceInDepotDetailManager().Select_ChooseDefectRateCls(condition.StartDate, condition.EndDate, null, null, condition.StartProduct, condition.EndProduct, null, null, null, null, null, null, condition.attrJiLuFangShi, condition.attrQiangHua, condition.attrWuDu, condition.attrWuQiangHuaWuDu, condition.attrProductStates, condition.RejectionRate, condition.RejectionRateCompare, condition.EnableBLV);

            //if (deitals == null || deitals.Count == 0)
            //    return;

            //if (condition.attrJiLuFangShi)
            //{
            //    foreach (Model.ProduceInDepotDetail d in deitals)
            //    {
            //        d.ProInDepotDetailDate = d.ProduceInDepot.ProduceInDepotDate.Value.ToString("yyyy-MM-dd");
            //        d.RejectionRate_1 = (d.NoHegeQuantity / (d.ProceduresSum.HasValue && d.ProceduresSum != 0 ? d.ProceduresSum : 1)).Value.ToString("0.#%");
            //    }
            //    this.DataSource = deitals;
            //}
            //else
            //{
            //    this.TCRiQi_H.WidthF = 310;
            //    this.TCRiQi.WidthF = 310;

            //    IList<Model.ProduceInDepotDetail> _mDataSource = new List<Model.ProduceInDepotDetail>();

            //    var GroupQuery = from Model.ProduceInDepotDetail ind in deitals
            //                     group ind by ind.ProduceInDepot.WorkHouseId;

            //    foreach (IGrouping<string, Model.ProduceInDepotDetail> groupDetails in GroupQuery)
            //    {

            //        //合并
            //        Model.ProduceInDepotDetail d = new Book.Model.ProduceInDepotDetail();
            //        d.ProInDepotDetailDate = condition.StartDate.ToString("yyyy#MM#dd").Replace('#', '/') + "-" + condition.EndDate.ToString("yyyy#MM#dd").Replace('#', '/');
            //        d.ProductId = groupDetails.First().ProductId;
            //        d.Product = groupDetails.First().Product;
            //        d.ProduceInDepotId = groupDetails.First().ProduceInDepotId;
            //        d.ProduceInDepot = groupDetails.First().ProduceInDepot;
            //        d.ProceduresSum = (from i in groupDetails select i.ProceduresSum).Sum();       //生产数量
            //        d.CheckOutSum = (from i in groupDetails select i.CheckOutSum).Sum();           //合格数量
            //        string nocheck = (d.NoHegeQuantity / (d.ProceduresSum.HasValue && d.ProceduresSum != 0 ? d.ProceduresSum : 1)).Value.ToString("0.#%");
            //        d.RejectionRate_1 = nocheck;        //转换后的不良率
            //        d.ProductUnit = groupDetails.First().ProductUnit;
            //        d.HeiDian = (from i in groupDetails select i.HeiDian).Sum();
            //        d.ZaZhi = (from i in groupDetails select i.ZaZhi).Sum();
            //        d.mJinDian = (from i in groupDetails select i.mJinDian).Sum();
            //        d.mLiaoDian = (from i in groupDetails select i.mLiaoDian).Sum();
            //        d.mCaiShang = (from i in groupDetails select i.mCaiShang).Sum();
            //        d.mWanMo = (from i in groupDetails select i.mWanMo).Sum();
            //        d.mSuoShui = (from i in groupDetails select i.mSuoShui).Sum();
            //        d.mGuohuo = (from i in groupDetails select i.mGuohuo).Sum();
            //        d.mBaiYan = (from i in groupDetails select i.mBaiYan).Sum();
            //        d.mHeiYan = (from i in groupDetails select i.mHeiYan).Sum();
            //        d.HeiDian = (from i in groupDetails select i.HeiDian).Sum();
            //        d.mJieHeXian = (from i in groupDetails select i.mJieHeXian).Sum();
            //        d.mHuiWen = (from i in groupDetails select i.mHuiWen).Sum();
            //        d.mQiPao = (from i in groupDetails select i.mQiPao).Sum();
            //        d.mLengLiao = (from i in groupDetails select i.mLengLiao).Sum();
            //        d.mGuaiShouZhuangShang = (from i in groupDetails select i.mGuaiShouZhuangShang).Sum();
            //        d.mPengXu = (from i in groupDetails select i.mPengXu).Sum();
            //        d.mCaMoSunHua = (from i in groupDetails select i.mCaMoSunHua).Sum();
            //        d.mChaiCa = (from i in groupDetails select i.mChaiCa).Sum();
            //        d.mSheCa = (from i in groupDetails select i.mSheCa).Sum();
            //        d.mQiangCa = (from i in groupDetails select i.mQiangCa).Sum();
            //        d.mKeLi = (from i in groupDetails select i.mKeLi).Sum();
            //        d.mLiuheng = (from i in groupDetails select i.mLiuheng).Sum();
            //        d.mPengYao = (from i in groupDetails select i.mPengYao).Sum();
            //        d.mYuHuoJiZhua = (from i in groupDetails select i.mYuHuoJiZhua).Sum();
            //        d.mZaZhiJingDian = (from i in groupDetails select i.mZaZhiJingDian).Sum();
            //        d.mQiTa = (from i in groupDetails select i.mQiTa).Sum();
            //        d.mCaMoCiShu = (from i in groupDetails select i.mCaMoCiShu).Sum();

            //        _mDataSource.Add(d);
            //    }
            //    this.DataSource = _mDataSource;
            //}

            ////Details
            //this.TCRiQi.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProInDepotDetailDate);
            //this.TCpingming.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            ////this.TCriqi.DataBindings.Add("Text", this.DataSource, "ProduceInDepot." + Model.ProduceInDepot.PRO_ProduceInDepotDate);
            //this.TCshuliang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProceduresSum);
            //this.TChegeshuliang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_CheckOutSum);
            //this.TCbulianglv.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_RejectionRate_1);
            //this.TCdanwei.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProductUnit);
            //this.TCheidian.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_HeiDian);
            //this.TCzazhi.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ZaZhi);
            //this.TCjindian.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mJinDian);
            //this.TCliaodian.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mLiaoDian);
            //this.TCcashang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mCaiShang);
            //this.TCwanmo.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mWanMo);
            //this.TCsuoshui.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mSuoShui);
            //this.TCguohuo.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mGuohuo);
            //this.TCbaiyan.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mBaiYan);
            //this.TCheiyan.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mHeiYan);
            //this.TCjiehexian.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mJieHeXian);
            //this.TChuiwen.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mHuiWen);
            //this.TCqipao.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mQiPao);
            //this.TClengliao.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mLengLiao);
            //this.TCguaishouzhuangshang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mGuaiShouZhuangShang);
            //this.TCpenxu.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mPengXu);
            //this.TCcamosunhuai.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mCaMoSunHua);
            //this.TCmChaiCa.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mChaiCa);
            //this.TCmSheCa.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mSheCa);
            //this.TCmQiangCa.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mQiangCa);
            //this.TCmKeLi.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mKeLi);
            //this.TCmLiuheng.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mLiuheng);
            //this.TCmPengYao.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mPengYao);
            //this.TCmYuHuoJiZhua.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mYuHuoJiZhua);
            //this.TCmZaZhiJingDian.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mZaZhiJingDian);
            //this.TCqita.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mQiTa);
            //this.TCcamocishu.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mCaMoCiShu);
            //this.TCgongsibumeng.DataBindings.Add("Text", this.DataSource, "ProduceInDepot.WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            #endregion
        }
    }
}