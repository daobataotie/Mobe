using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

namespace Book.UI.Query
{
    public partial class ROInvoiceCGlistBiao : BaseReport
    {
        private BL.InvoiceCGDetailManager invoicecgmanager = new Book.BL.InvoiceCGDetailManager();

        public ROInvoiceCGlistBiao()
        {
            InitializeComponent();
        }

        public ROInvoiceCGlistBiao(ConditionCO condition)
            : this()
        {
            this.xrLabelReportName.Text = "應付賬款明細表";
            this.lblSupplierName.Text = condition.SupplierStart == null ? "" : condition.SupplierStart.ToString();
            this.lblInvoiceDateRange.Text += condition.StartDate.ToString("yyyy-MM-dd") + " - " + condition.EndDate.ToString("yyyy-MM-dd");
            //Bind
            this.DataSource = this.invoicecgmanager.SelectByConditionCOBiao(condition.StartDate, condition.EndDate, condition.StartJHDate, condition.EndJHDate, condition.StartFKDate, condition.EndFKDate, condition.SupplierStart, condition.SupplierEnd, condition.ProductStart, condition.ProductEnd, condition.COStartId, condition.COEndId, condition.CusXOId, condition.EmpStart, condition.EmpEnd);

            if (this.DataSource == null || (this.DataSource as DataTable).Rows.Count == 0)
                throw new Helper.InvalidValueException("查無記錄!");

            string sF;
            decimal jine = 0, shuie = 0, zonge = 0;
            foreach (DataRow dr in (this.DataSource as DataTable).Rows)
            {

                if (dr[0].ToString().Contains("PID") || dr[0].ToString().Contains("POD"))
                {
                    try
                    {
                        //string DJ = dr["DanJia"].ToString();
                        //double Quantity = string.IsNullOrEmpty(dr["JHSL"].ToString()) ? 0 : Convert.ToDouble(dr["JHSL"].ToString());
                        //dr["DanJia"] = BL.SupplierProductManager.CountPrice(DJ, Quantity);
                        dr["JinE"] = Convert.ToDouble(dr["DanJia"]) * Convert.ToDouble(dr["JHSL"]);
                        dr["ShuiE"] = Convert.ToDouble(dr["JinE"]) * 0.05;
                        dr["Total"] = Convert.ToDouble(dr["JinE"]) + Convert.ToDouble(dr["ShuiE"]);
                    }
                    catch (Exception ex)
                    {
                        sF = ex.Message;
                    }
                }
                jine += Convert.ToDecimal(dr["JinE"]);
                shuie += Convert.ToDecimal(dr["ShuiE"]);
            }

            zonge = jine + shuie;

            if (this.DataSource == null || (this.DataSource as DataTable).Rows.Count == 0)
                throw new global::Helper.InvalidValueException("無記錄");

            this.tcJHDH.DataBindings.Add("Text", this.DataSource, "JHDN");
            this.tcJHRQ.DataBindings.Add("Text", this.DataSource, "JHRQ", "{0:yyyy-MM-dd}");
            this.tcProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.tcJHSL.DataBindings.Add("Text", this.DataSource, "JHSL");
            this.tcDJ.DataBindings.Add("Text", this.DataSource, "DanJia", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.tcJinE.DataBindings.Add("Text", this.DataSource, "JinE", global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.tcCGorWWDanHao.DataBindings.Add("Text", this.DataSource, "CGorWWDanHao");
            this.tcDW.DataBindings.Add("Text", this.DataSource, "ProductUnit");

            this.TCZJinE.Text = this.invoicecgmanager.GetSiSheWuRu(jine, 0).ToString();
            this.TCTotalShuiE.Text = this.invoicecgmanager.GetSiSheWuRu(shuie, 0).ToString();
            this.TCTotalMoney.Text = this.invoicecgmanager.GetSiSheWuRu(zonge, 0).ToString();

            //this.TCZJinE.Summary.FormatString = "{0:0}";
            //this.TCZJinE.Summary.Func = SummaryFunc.Sum;
            //this.TCZJinE.Summary.IgnoreNullValues = true;
            //this.TCZJinE.Summary.Running = SummaryRunning.Report;
            //this.TCZJinE.DataBindings.Add("Text", this.DataSource, "JinE");

            //this.TCTotalShuiE.Summary.FormatString = "{0:0}";
            //this.TCTotalShuiE.Summary.Func = SummaryFunc.Sum;
            //this.TCTotalShuiE.Summary.IgnoreNullValues = true;
            //this.TCTotalShuiE.Summary.Running = SummaryRunning.Report;
            //this.TCTotalShuiE.DataBindings.Add("Text", this.DataSource, "ShuiE");

            //this.TCTotalMoney.Summary.FormatString = "{0:0}";
            //this.TCTotalMoney.Summary.Func = SummaryFunc.Sum;
            //this.TCTotalMoney.Summary.IgnoreNullValues = true;
            //this.TCTotalMoney.Summary.Running = SummaryRunning.Report;
            //this.TCTotalMoney.DataBindings.Add("Text", this.DataSource, "Total");
        }
    }
}
