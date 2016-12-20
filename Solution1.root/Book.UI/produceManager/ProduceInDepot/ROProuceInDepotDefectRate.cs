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
    public partial class ROProuceInDepotDefectRate : DevExpress.XtraReports.UI.XtraReport
    {
        public ROProuceInDepotDefectRate()
        {
            InitializeComponent();
        }

        public ROProuceInDepotDefectRate(ChooseDefectRateCls condition)
            : this()
        {
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
                    this.lblReportName.Text = PrintYearAndMonth + "生產不良統計表";
                    break;
                case 1:
                    this.lblReportName.Text = "常態" + PrintYearAndMonth + "不良統計表";
                    break;
                case 2:
                    this.lblReportName.Text = "特殊" + PrintYearAndMonth + "不良統計表";
                    break;
            }

            this.lblDateRange.Text = PrintYearAndMonth;
            if (!(condition.StartWorkHouse == null && condition.EndWorkHouse == null))
            {
                if (condition.StartWorkHouse != null && condition.EndWorkHouse != null)
                    this.lblWorkHouseRange.Text = condition.StartWorkHouse.Workhousename + "~" + condition.EndWorkHouse.Workhousename;
                else
                    this.lblWorkHouseRange.Text = condition.StartWorkHouse == null ? condition.EndWorkHouse.Workhousename : condition.StartWorkHouse.Workhousename;
            }

            string ProductAttrs = string.Empty;
            ProductAttrs += condition.attrQiangHua ? "強化 " : "";
            ProductAttrs += condition.attrWuDu ? "霧度" : "";
            ProductAttrs += condition.attrWuQiangHuaWuDu ? "無強化霧度" : "";
            this.lblProductAttrRange.Text = ProductAttrs;

            this.lblJiLuFangShi.Text = condition.attrJiLuFangShi ? "展開" : "合併";


            //IList<Model.ProduceInDepotDetail> deitals = new BL.ProduceInDepotDetailManager().Select_ChooseDefectRateCls(condition.StartDate, condition.EndDate, condition.StartProduceInDepotId, condition.EndProduceInDepotId, condition.StartProduct, condition.EndProduct, condition.StartPronoteHeaderId, condition.EndPronoteHeaderId, condition.StartWorkHouse, condition.EndWorkHouse, condition.StartCustomer, condition.EndCustomer, condition.attrJiLuFangShi, condition.attrQiangHua, condition.attrWuDu, condition.attrWuQiangHuaWuDu, condition.attrProductStates, condition.RejectionRate, condition.RejectionRateCompare, condition.EnableBLV);

            //构建合计Total
            DataTable dat = new BL.ProduceInDepotDetailManager().SUMDTSelect_ChooseDefectRateCls(condition.DateType, condition.StartDate, condition.EndDate, condition.StartProduceInDepotId, condition.EndProduceInDepotId, condition.StartProduct, condition.EndProduct, condition.StartPronoteHeaderId, condition.EndPronoteHeaderId, condition.StartWorkHouse, condition.EndWorkHouse, condition.StartCustomer, condition.EndCustomer, condition.attrJiLuFangShi, condition.attrQiangHua, condition.attrWuDu, condition.attrWuQiangHuaWuDu, condition.attrProductStates, condition.RejectionRate, condition.RejectionRateCompare, condition.EnableBLV, condition.attrOrderColumn, condition.attrOrderType, null, null, condition.InvoiceStates);

            //绑定具体数据
            DataTable dt = new BL.ProduceInDepotDetailManager().DTSelect_ChooseDefectRateCls(condition.DateType, condition.StartDate, condition.EndDate, condition.StartProduceInDepotId, condition.EndProduceInDepotId, condition.StartProduct, condition.EndProduct, condition.StartPronoteHeaderId, condition.EndPronoteHeaderId, condition.StartWorkHouse, condition.EndWorkHouse, condition.StartCustomer, condition.EndCustomer, condition.attrJiLuFangShi, condition.attrQiangHua, condition.attrWuDu, condition.attrWuQiangHuaWuDu, condition.attrProductStates, condition.RejectionRate, condition.RejectionRateCompare, condition.EnableBLV, condition.attrOrderColumn, condition.attrOrderType, null, null, condition.InvoiceStates);

            if (dt == null || dt.Rows.Count == 0 || dat == null || dat.Rows.Count == 0)
                throw new Helper.InvalidValueException("無記錄");

            this.DataSource = dt;

            StringBuilder sb = new StringBuilder();

            double produceSum = 0;
            double PassSum = 0;
            for (int i = 0; i < dat.Rows.Count; i++)
            {
                sb.Append(dat.Rows[i]["Workhousename"] + "：總生產數量：" + dat.Rows[i]["ProceduresSum"] + " 合格數量：" + dat.Rows[i]["CheckOutSum"] + " 不良率：" + dat.Rows[i]["RejectionRate_1"] + "\t");

                produceSum += Convert.ToDouble(dat.Rows[i]["ProceduresSum"]);
                PassSum += Convert.ToDouble(dat.Rows[i]["CheckOutSum"]);
            }

            this.lblworkHouseData.Text = sb.ToString();

            //数据统计
            //this.lblworkHouseData.DataBindings.Add("Text",this.DataSource)
            //Details
            this.TCRiQi.DataBindings.Add("Text", this.DataSource, "ProduceInDepotDate");
            this.TCpingming.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TCgongsibumeng.DataBindings.Add("Text", this.DataSource, "Workhousename");
            this.TCfangwuqinghua.DataBindings.Add("Text", this.DataSource, "ProductWDQHua");
            this.TCProductType.DataBindings.Add("Text", this.DataSource, "ProductType");
            this.TCshuliang.DataBindings.Add("Text", this.DataSource, "ProceduresSum");
            this.TChegeshuliang.DataBindings.Add("Text", this.DataSource, "CheckOutSum");
            this.TCbulianglv.DataBindings.Add("Text", this.DataSource, "RejectionRate_1");
            this.TCdanwei.DataBindings.Add("Text", this.DataSource, "ProductUnit");
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


            this.TCshuliang2.Summary.FormatString = "{0:0.##}";
            this.TCshuliang2.Summary.Func = SummaryFunc.Sum;
            this.TCshuliang2.Summary.IgnoreNullValues = true;
            this.TCshuliang2.Summary.Running = SummaryRunning.Report;
            this.TCshuliang2.DataBindings.Add("Text", this.DataSource, "ProceduresSum", "{0:0.##}");

            this.TChegeshuliang2.Summary.FormatString = "{0:0.##}";
            this.TChegeshuliang2.Summary.Func = SummaryFunc.Sum;
            this.TChegeshuliang2.Summary.IgnoreNullValues = true;
            this.TChegeshuliang2.Summary.Running = SummaryRunning.Report;
            this.TChegeshuliang2.DataBindings.Add("Text", this.DataSource, "CheckOutSum", "{0:0.##}");

            if (produceSum > 0)
                this.TCbulianglv2.Text = ((produceSum - PassSum) / produceSum * 100).ToString("0.##") + "%";
            else
                this.TCbulianglv2.Text = "0%";

            this.TCmYuanliaowenti2.Summary.FormatString = "{0:0.##}";
            this.TCmYuanliaowenti2.Summary.Func = SummaryFunc.Sum;
            this.TCmYuanliaowenti2.Summary.IgnoreNullValues = true;
            this.TCmYuanliaowenti2.Summary.Running = SummaryRunning.Report;
            this.TCmYuanliaowenti2.DataBindings.Add("Text", this.DataSource, "mYuanliaowenti", "{0:0.##}");

            this.TCmChouliaowenti2.Summary.FormatString = "{0:0.##}";
            this.TCmChouliaowenti2.Summary.Func = SummaryFunc.Sum;
            this.TCmChouliaowenti2.Summary.IgnoreNullValues = true;
            this.TCmChouliaowenti2.Summary.Running = SummaryRunning.Report;
            this.TCmChouliaowenti2.DataBindings.Add("Text", this.DataSource, "mChouliaowenti", "{0:0.##}");

            this.TCmPaoguanwenti2.Summary.FormatString = "{0:0.##}";
            this.TCmPaoguanwenti2.Summary.Func = SummaryFunc.Sum;
            this.TCmPaoguanwenti2.Summary.IgnoreNullValues = true;
            this.TCmPaoguanwenti2.Summary.Running = SummaryRunning.Report;
            this.TCmPaoguanwenti2.DataBindings.Add("Text", this.DataSource, "mPaoguanwenti", "{0:0.##}");

            this.TCmJingdiangudingdian2.Summary.FormatString = "{0:0.##}";
            this.TCmJingdiangudingdian2.Summary.Func = SummaryFunc.Sum;
            this.TCmJingdiangudingdian2.Summary.IgnoreNullValues = true;
            this.TCmJingdiangudingdian2.Summary.Running = SummaryRunning.Report;
            this.TCmJingdiangudingdian2.DataBindings.Add("Text", this.DataSource, "mJingdiangudingdian", "{0:0.##}");

            this.TCmChapiancashang2.Summary.FormatString = "{0:0.##}";
            this.TCmChapiancashang2.Summary.Func = SummaryFunc.Sum;
            this.TCmChapiancashang2.Summary.IgnoreNullValues = true;
            this.TCmChapiancashang2.Summary.Running = SummaryRunning.Report;
            this.TCmChapiancashang2.DataBindings.Add("Text", this.DataSource, "mChapiancashang", "{0:0.##}");

            this.TCmWanMocashang2.Summary.FormatString = "{0:0.##}";
            this.TCmWanMocashang2.Summary.Func = SummaryFunc.Sum;
            this.TCmWanMocashang2.Summary.IgnoreNullValues = true;
            this.TCmWanMocashang2.Summary.Running = SummaryRunning.Report;
            this.TCmWanMocashang2.DataBindings.Add("Text", this.DataSource, "mWanMocashang", "{0:0.##}");

            this.TCmGuaiShouZhuangShang2.Summary.FormatString = "{0:0.##}";
            this.TCmGuaiShouZhuangShang2.Summary.Func = SummaryFunc.Sum;
            this.TCmGuaiShouZhuangShang2.Summary.IgnoreNullValues = true;
            this.TCmGuaiShouZhuangShang2.Summary.Running = SummaryRunning.Report;
            this.TCmGuaiShouZhuangShang2.DataBindings.Add("Text", this.DataSource, "mGuaiShouZhuangShang", "{0:0.##}");

            this.TCmHuabancashang2.Summary.FormatString = "{0:0.##}";
            this.TCmHuabancashang2.Summary.Func = SummaryFunc.Sum;
            this.TCmHuabancashang2.Summary.IgnoreNullValues = true;
            this.TCmHuabancashang2.Summary.Running = SummaryRunning.Report;
            this.TCmHuabancashang2.DataBindings.Add("Text", this.DataSource, "mHuabancashang", "{0:0.##}");

            this.TCmGuohuojizhua2.Summary.FormatString = "{0:0.##}";
            this.TCmGuohuojizhua2.Summary.Func = SummaryFunc.Sum;
            this.TCmGuohuojizhua2.Summary.IgnoreNullValues = true;
            this.TCmGuohuojizhua2.Summary.Running = SummaryRunning.Report;
            this.TCmGuohuojizhua2.DataBindings.Add("Text", this.DataSource, "mGuohuojizhua", "{0:0.##}");

            this.TCmBaiyanHeiYan2.Summary.FormatString = "{0:0.##}";
            this.TCmBaiyanHeiYan2.Summary.Func = SummaryFunc.Sum;
            this.TCmBaiyanHeiYan2.Summary.IgnoreNullValues = true;
            this.TCmBaiyanHeiYan2.Summary.Running = SummaryRunning.Report;
            this.TCmBaiyanHeiYan2.DataBindings.Add("Text", this.DataSource, "mBaiyanHeiYan", "{0:0.##}");

            this.TCmJieHeXianHuiwen2.Summary.FormatString = "{0:0.##}";
            this.TCmJieHeXianHuiwen2.Summary.Func = SummaryFunc.Sum;
            this.TCmJieHeXianHuiwen2.Summary.IgnoreNullValues = true;
            this.TCmJieHeXianHuiwen2.Summary.Running = SummaryRunning.Report;
            this.TCmJieHeXianHuiwen2.DataBindings.Add("Text", this.DataSource, "mJieHeXianHuiwen", "{0:0.##}");

            this.TCmSuoShui2.Summary.FormatString = "{0:0.##}";
            this.TCmSuoShui2.Summary.Func = SummaryFunc.Sum;
            this.TCmSuoShui2.Summary.IgnoreNullValues = true;
            this.TCmSuoShui2.Summary.Running = SummaryRunning.Report;
            this.TCmSuoShui2.DataBindings.Add("Text", this.DataSource, "mSuoShui", "{0:0.##}");

            this.TCmQiPao2.Summary.FormatString = "{0:0.##}";
            this.TCmQiPao2.Summary.Func = SummaryFunc.Sum;
            this.TCmQiPao2.Summary.IgnoreNullValues = true;
            this.TCmQiPao2.Summary.Running = SummaryRunning.Report;
            this.TCmQiPao2.DataBindings.Add("Text", this.DataSource, "mQiPao", "{0:0.##}");

            this.TCmShechuqita2.Summary.FormatString = "{0:0.##}";
            this.TCmShechuqita2.Summary.Func = SummaryFunc.Sum;
            this.TCmShechuqita2.Summary.IgnoreNullValues = true;
            this.TCmShechuqita2.Summary.Running = SummaryRunning.Report;
            this.TCmShechuqita2.DataBindings.Add("Text", this.DataSource, "mShechuqita", "{0:0.##}");

            this.TCmCaMoSunHua2.Summary.FormatString = "{0:0.##}";
            this.TCmCaMoSunHua2.Summary.Func = SummaryFunc.Sum;
            this.TCmCaMoSunHua2.Summary.IgnoreNullValues = true;
            this.TCmCaMoSunHua2.Summary.Running = SummaryRunning.Report;
            this.TCmCaMoSunHua2.DataBindings.Add("Text", this.DataSource, "mCaMoSunHua", "{0:0.##}");

            this.TCmChaipiancashang2.Summary.FormatString = "{0:0.##}";
            this.TCmChaipiancashang2.Summary.Func = SummaryFunc.Sum;
            this.TCmChaipiancashang2.Summary.IgnoreNullValues = true;
            this.TCmChaipiancashang2.Summary.Running = SummaryRunning.Report;
            this.TCmChaipiancashang2.DataBindings.Add("Text", this.DataSource, "mChaipiancashang", "{0:0.##}");

            this.TCmHeidianzazhi2.Summary.FormatString = "{0:0.##}";
            this.TCmHeidianzazhi2.Summary.Func = SummaryFunc.Sum;
            this.TCmHeidianzazhi2.Summary.IgnoreNullValues = true;
            this.TCmHeidianzazhi2.Summary.Running = SummaryRunning.Report;
            this.TCmHeidianzazhi2.DataBindings.Add("Text", this.DataSource, "mHeidianzazhi", "{0:0.##}");

            this.TCmQianghuaqiancashang2.Summary.FormatString = "{0:0.##}";
            this.TCmQianghuaqiancashang2.Summary.Func = SummaryFunc.Sum;
            this.TCmQianghuaqiancashang2.Summary.IgnoreNullValues = true;
            this.TCmQianghuaqiancashang2.Summary.Running = SummaryRunning.Report;
            this.TCmQianghuaqiancashang2.DataBindings.Add("Text", this.DataSource, "mQianghuaqiancashang", "{0:0.##}");

            this.TCmQianghuahoucashang2.Summary.FormatString = "{0:0.##}";
            this.TCmQianghuahoucashang2.Summary.Func = SummaryFunc.Sum;
            this.TCmQianghuahoucashang2.Summary.IgnoreNullValues = true;
            this.TCmQianghuahoucashang2.Summary.Running = SummaryRunning.Report;
            this.TCmQianghuahoucashang2.DataBindings.Add("Text", this.DataSource, "mQianghuahoucashang", "{0:0.##}");

            this.TCmHanyao2.Summary.FormatString = "{0:0.##}";
            this.TCmHanyao2.Summary.Func = SummaryFunc.Sum;
            this.TCmHanyao2.Summary.IgnoreNullValues = true;
            this.TCmHanyao2.Summary.Running = SummaryRunning.Report;
            this.TCmHanyao2.DataBindings.Add("Text", this.DataSource, "mHanyao", "{0:0.##}");

            this.TCmKeLimianxu2.Summary.FormatString = "{0:0.##}";
            this.TCmKeLimianxu2.Summary.Func = SummaryFunc.Sum;
            this.TCmKeLimianxu2.Summary.IgnoreNullValues = true;
            this.TCmKeLimianxu2.Summary.Running = SummaryRunning.Report;
            this.TCmKeLimianxu2.DataBindings.Add("Text", this.DataSource, "mKeLimianxu", "{0:0.##}");

            this.TCmLiuheng2.Summary.FormatString = "{0:0.##}";
            this.TCmLiuheng2.Summary.Func = SummaryFunc.Sum;
            this.TCmLiuheng2.Summary.IgnoreNullValues = true;
            this.TCmLiuheng2.Summary.Running = SummaryRunning.Report;
            this.TCmLiuheng2.DataBindings.Add("Text", this.DataSource, "mLiuheng", "{0:0.##}");

            this.TCmPengYaodiyao2.Summary.FormatString = "{0:0.##}";
            this.TCmPengYaodiyao2.Summary.Func = SummaryFunc.Sum;
            this.TCmPengYaodiyao2.Summary.IgnoreNullValues = true;
            this.TCmPengYaodiyao2.Summary.Running = SummaryRunning.Report;
            this.TCmPengYaodiyao2.DataBindings.Add("Text", this.DataSource, "mPengYaodiyao", "{0:0.##}");

            this.TCmQianghuafangwuxian2.Summary.FormatString = "{0:0.##}";
            this.TCmQianghuafangwuxian2.Summary.Func = SummaryFunc.Sum;
            this.TCmQianghuafangwuxian2.Summary.IgnoreNullValues = true;
            this.TCmQianghuafangwuxian2.Summary.Running = SummaryRunning.Report;
            this.TCmQianghuafangwuxian2.DataBindings.Add("Text", this.DataSource, "mQianghuafangwuxian", "{0:0.##}");

            this.TCmYoudian2.Summary.FormatString = "{0:0.##}";
            this.TCmYoudian2.Summary.Func = SummaryFunc.Sum;
            this.TCmYoudian2.Summary.IgnoreNullValues = true;
            this.TCmYoudian2.Summary.Running = SummaryRunning.Report;
            this.TCmYoudian2.DataBindings.Add("Text", this.DataSource, "mYoudian", "{0:0.##}");

            this.TCmQianghuaQiTa2.Summary.FormatString = "{0:0.##}";
            this.TCmQianghuaQiTa2.Summary.Func = SummaryFunc.Sum;
            this.TCmQianghuaQiTa2.Summary.IgnoreNullValues = true;
            this.TCmQianghuaQiTa2.Summary.Running = SummaryRunning.Report;
            this.TCmQianghuaQiTa2.DataBindings.Add("Text", this.DataSource, "mQianghuaQiTa", "{0:0.##}");

            this.TCmChangshangbuliang2.Summary.FormatString = "{0:0.##}";
            this.TCmChangshangbuliang2.Summary.Func = SummaryFunc.Sum;
            this.TCmChangshangbuliang2.Summary.IgnoreNullValues = true;
            this.TCmChangshangbuliang2.Summary.Running = SummaryRunning.Report;
            this.TCmChangshangbuliang2.DataBindings.Add("Text", this.DataSource, "mChangshangbuliang", "{0:0.##}");

            this.TCmZuzhuangcashang2.Summary.FormatString = "{0:0.##}";
            this.TCmZuzhuangcashang2.Summary.Func = SummaryFunc.Sum;
            this.TCmZuzhuangcashang2.Summary.IgnoreNullValues = true;
            this.TCmZuzhuangcashang2.Summary.Running = SummaryRunning.Report;
            this.TCmZuzhuangcashang2.DataBindings.Add("Text", this.DataSource, "mZuzhuangcashang", "{0:0.##}");

            this.TCmCashang2.Summary.FormatString = "{0:0.##}";
            this.TCmCashang2.Summary.Func = SummaryFunc.Sum;
            this.TCmCashang2.Summary.IgnoreNullValues = true;
            this.TCmCashang2.Summary.Running = SummaryRunning.Report;
            this.TCmCashang2.DataBindings.Add("Text", this.DataSource, "mCashang", "{0:0.##}");
            #region 注释绑定List
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
            //    //合并
            //    IList<Model.ProduceInDepotDetail> _mDataSource = new List<Model.ProduceInDepotDetail>();

            //    var sumList = from Model.ProduceInDepotDetail item in deitals
            //                  group item by item.ProductId;
            //    foreach (IGrouping<string, Model.ProduceInDepotDetail> outg in sumList)
            //    {
            //        var sumInlist = from Model.ProduceInDepotDetail item in outg
            //                        group item by item.ProduceInDepot.WorkHouseId;
            //        foreach (IGrouping<string, Model.ProduceInDepotDetail> g in sumInlist)
            //        {
            //            Model.ProduceInDepotDetail d = new Book.Model.ProduceInDepotDetail();
            //            d.ProInDepotDetailDate = condition.StartDate.ToString("yyyy#MM#dd").Replace('#', '/') + "-" + condition.EndDate.ToString("yyyy#MM#dd").Replace('#', '/');
            //            d.ProductId = g.First().ProductId;
            //            d.Product = g.First().Product;
            //            d.ProduceInDepotId = g.First().ProduceInDepotId;
            //            d.ProduceInDepot = g.First().ProduceInDepot;
            //            d.ProceduresSum = (from i in g select i.ProceduresSum).Sum();       //生产数量
            //            d.CheckOutSum = (from i in g select i.CheckOutSum).Sum();           //合格数量
            //            string nocheck = (d.NoHegeQuantity / (d.ProceduresSum.HasValue && d.ProceduresSum != 0 ? d.ProceduresSum : 1)).Value.ToString("0.#%");
            //            d.RejectionRate_1 = nocheck;        //转换后的不良率
            //            d.ProductUnit = g.First().ProductUnit;
            //            d.HeiDian = (from i in g select i.HeiDian).Sum();
            //            d.ZaZhi = (from i in g select i.ZaZhi).Sum();
            //            d.mJinDian = (from i in g select i.mJinDian).Sum();
            //            d.mLiaoDian = (from i in g select i.mLiaoDian).Sum();
            //            d.mCaiShang = (from i in g select i.mCaiShang).Sum();
            //            d.mWanMo = (from i in g select i.mWanMo).Sum();
            //            d.mSuoShui = (from i in g select i.mSuoShui).Sum();
            //            d.mGuohuo = (from i in g select i.mGuohuo).Sum();
            //            d.mBaiYan = (from i in g select i.mBaiYan).Sum();
            //            d.mHeiYan = (from i in g select i.mHeiYan).Sum();
            //            d.HeiDian = (from i in g select i.HeiDian).Sum();
            //            d.mJieHeXian = (from i in g select i.mJieHeXian).Sum();
            //            d.mHuiWen = (from i in g select i.mHuiWen).Sum();
            //            d.mQiPao = (from i in g select i.mQiPao).Sum();
            //            d.mLengLiao = (from i in g select i.mLengLiao).Sum();
            //            d.mGuaiShouZhuangShang = (from i in g select i.mGuaiShouZhuangShang).Sum();
            //            d.mPengXu = (from i in g select i.mPengXu).Sum();
            //            d.mCaMoSunHua = (from i in g select i.mCaMoSunHua).Sum();
            //            d.mCaMoCiShu = (from i in g select i.mCaMoCiShu).Sum();
            //            d.mChaiCa = (from i in g select i.mChaiCa).Sum();
            //            d.mSheCa = (from i in g select i.mSheCa).Sum();
            //            d.mQiangCa = (from i in g select i.mQiangCa).Sum();
            //            d.mKeLi = (from i in g select i.mKeLi).Sum();
            //            d.mLiuheng = (from i in g select i.mLiuheng).Sum();
            //            d.mPengYao = (from i in g select i.mPengYao).Sum();
            //            d.mYuHuoJiZhua = (from i in g select i.mYuHuoJiZhua).Sum();
            //            d.mZaZhiJingDian = (from i in g select i.mZaZhiJingDian).Sum();
            //            d.mQiTa = (from i in g select i.mQiTa).Sum();

            //            _mDataSource.Add(d);
            //        }
            //    }
            //    this.DataSource = _mDataSource;
            //}

            //IList<Model.ProduceInDepotDetail> _orderSource = new List<Model.ProduceInDepotDetail>();

            //switch (condition.attrOrderColumn)
            //{
            //    case 0:
            //        if (condition.attrOrderType == 0)
            //        {
            //            _orderSource = (from Model.ProduceInDepotDetail d in (this.DataSource as IList<Model.ProduceInDepotDetail>)
            //                            orderby d.ProduceInDepotDate
            //                            select d).ToList<Model.ProduceInDepotDetail>();
            //        }
            //        else
            //        {
            //            _orderSource = (from Model.ProduceInDepotDetail d in (this.DataSource as IList<Model.ProduceInDepotDetail>)
            //                            orderby d.ProduceInDepotDate descending
            //                            select d).ToList<Model.ProduceInDepotDetail>();
            //        }
            //        break;
            //    case 1:
            //        if (condition.attrOrderType == 0)
            //        {
            //            _orderSource = (from Model.ProduceInDepotDetail d in (this.DataSource as IList<Model.ProduceInDepotDetail>)
            //                            orderby d.Product.ProductName
            //                            select d).ToList<Model.ProduceInDepotDetail>();
            //        }
            //        else
            //        {
            //            _orderSource = (from Model.ProduceInDepotDetail d in (this.DataSource as IList<Model.ProduceInDepotDetail>)
            //                            orderby d.Product.ProductName descending
            //                            select d).ToList<Model.ProduceInDepotDetail>();
            //        }
            //        break;
            //    case 2:
            //        if (condition.attrOrderType == 0)
            //        {
            //            _orderSource = (from Model.ProduceInDepotDetail d in (this.DataSource as IList<Model.ProduceInDepotDetail>)
            //                            orderby d.WorkHouseHeader.Workhousename
            //                            select d).ToList<Model.ProduceInDepotDetail>();
            //        }
            //        else
            //        {
            //            _orderSource = (from Model.ProduceInDepotDetail d in (this.DataSource as IList<Model.ProduceInDepotDetail>)
            //                            orderby d.WorkHouseHeader.Workhousename descending
            //                            select d).ToList<Model.ProduceInDepotDetail>();
            //        }
            //        break;
            //}

            //this.DataSource = _orderSource;

            ////部门数据统计
            //var _WorkHouseList = from Model.ProduceInDepotDetail d in (this.DataSource as IList<Model.ProduceInDepotDetail>)
            //                     group d by d.ProduceInDepot.WorkHouseId;

            //StringBuilder sb = new StringBuilder();
            //double _mProceduresSum;   //生产数量
            //double _mCheckOutSum;     //合格数量
            //string _mRejectionRate;    //不良率
            //foreach (IGrouping<string, Model.ProduceInDepotDetail> g in _WorkHouseList)
            //{
            //    _mProceduresSum = (from i in g select i.ProceduresSum).Sum().HasValue ? (from i in g select i.ProceduresSum).Sum().Value : 0;
            //    _mCheckOutSum = (from i in g select i.CheckOutSum).Sum().HasValue ? (from i in g select i.CheckOutSum).Sum().Value : 0;
            //    _mRejectionRate = ((_mProceduresSum - _mCheckOutSum) / (_mProceduresSum == 0 ? 1 : _mProceduresSum)).ToString("0.#%");

            //    sb.Append(g.First().WorkHouseHeader == null ? "" : g.First().WorkHouseHeader.Workhousename + ":  總生產數量:  " + _mProceduresSum + " 合格數量:  " + _mCheckOutSum.ToString() + " 不良率:  " + _mRejectionRate + "\t");
            //}

            //this.lblworkHouseData.Text = sb.ToString();



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
            //this.TCcamocishu.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mCaMoCiShu);
            //this.TCmChaiCa.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mChaiCa);
            //this.TCmSheCa.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mSheCa);
            //this.TCmQiangCa.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mQiangCa);
            //this.TCmKeLi.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mKeLi);
            //this.TCmLiuheng.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mLiuheng);
            //this.TCmPengYao.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mPengYao);
            //this.TCmYuHuoJiZhua.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mYuHuoJiZhua);
            //this.TCmZaZhiJingDian.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mZaZhiJingDian);
            //this.TCqita.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_mQiTa);
            //this.TCgongsibumeng.DataBindings.Add("Text", this.DataSource, "ProduceInDepot.WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            //this.TCfangwuqinghua.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_PID_ProductWDQHua);
            //this.TCProductType.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_PID_ProductType);
            #endregion
        }
    }
}
