using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.Query
{
    public partial class OutAndInDepotForm : ConditionChooseForm
    {
        private OutAndInDepot condition;
        BL.DepotOutDetailManager manager = new Book.BL.DepotOutDetailManager();

        public override Condition Condition
        {
            get
            {
                return condition;
            }
            set
            {
                condition = value as OutAndInDepot;
            }
        }

        public OutAndInDepotForm()
        {
            InitializeComponent();

            this.bindingSourceDepot.DataSource = (new BL.DepotManager()).Select();
            this.bindingSourceProductCategory.DataSource = (new BL.ProductCategoryManager()).Select();
            this.date_Start.DateTime = DateTime.Now.Date.AddDays(-7);
            this.date_End.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new OutAndInDepot();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_Start.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.date_Start.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_End.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.date_End.DateTime;
            }

            //this.condition.OutDepotIdStart = this.txt_DepotOutIdStart.Text;
            //this.condition.OutDepotIdEnd = this.txt_DepotOutIdEnd.Text;
            this.condition.DepotEnd = this.lookUpEditDepotEnd.EditValue == null ? null : this.lookUpEditDepotEnd.EditValue.ToString();
            this.condition.DepotStart = this.lookUpEditDepotStar.EditValue == null ? null : this.lookUpEditDepotStar.EditValue.ToString();
            //this.condition.ProductNameStart = this.btn_ProductNameStart.Text;
            //this.condition.ProductNameEnd = this.btn_ProductNameEnd.Text;
            this.condition.ProduceCategoryStart = this.LookUpProductCategoryStart.EditValue == null ? null : this.LookUpProductCategoryStart.EditValue.ToString();
            this.condition.ProductCategoryEnd = this.lookUpProductCategoryEnd.EditValue == null ? null : this.lookUpProductCategoryEnd.EditValue.ToString();
            this.condition.ProductIdStart = (this.btn_ProductNameStart.EditValue == null ? "" : (this.btn_ProductNameStart.EditValue as Model.Product).Id);
            this.condition.ProductIdEnd = (this.btn_ProductNameEnd.EditValue == null ? "" : (this.btn_ProductNameEnd.EditValue as Model.Product).Id);
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ProductNameStart_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_ProductNameEnd.EditValue = this.btn_ProductNameStart.EditValue = f.SelectedItem as Model.Product;
        }

        private void btn_ProductNameEnd_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_ProductNameEnd.EditValue = f.SelectedItem as Model.Product;
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            if (this.condition == null)
                this.condition = new OutAndInDepot();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_Start.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.date_Start.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_End.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.date_End.DateTime;
            }

            this.condition.DepotEnd = this.lookUpEditDepotEnd.EditValue == null ? null : this.lookUpEditDepotEnd.EditValue.ToString();
            this.condition.DepotStart = this.lookUpEditDepotStar.EditValue == null ? null : this.lookUpEditDepotStar.EditValue.ToString();
            this.condition.ProduceCategoryStart = this.LookUpProductCategoryStart.EditValue == null ? null : this.LookUpProductCategoryStart.EditValue.ToString();
            this.condition.ProductCategoryEnd = this.lookUpProductCategoryEnd.EditValue == null ? null : this.lookUpProductCategoryEnd.EditValue.ToString();
            this.condition.ProductIdStart = (this.btn_ProductNameStart.EditValue == null ? "" : (this.btn_ProductNameStart.EditValue as Model.Product).Id);
            this.condition.ProductIdEnd = (this.btn_ProductNameEnd.EditValue == null ? "" : (this.btn_ProductNameEnd.EditValue as Model.Product).Id);


            System.Data.DataTable dt = this.manager.SelectOutAndInDepot(condition.StartDate, condition.EndDate, condition.DepotStart, condition.DepotEnd, condition.ProduceCategoryStart, condition.ProductCategoryEnd, condition.ProductIdStart, condition.ProductIdEnd);

            ExportExcel(dt);
        }

        private void ExportExcel(System.Data.DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Type objClassType = null;
                objClassType = Type.GetTypeFromProgID("Excel.Application");
                if (objClassType == null)
                {
                    MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 8]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 20;
                excel.Cells[1, 1] = "商品进出仓明细(" + this.condition.StartDate.ToString("yyyy-MM-dd") + this.condition.EndDate.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 8], excel.Cells[1, 8]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "单据类型";
                excel.Cells[2, 2] = "日期";
                excel.Cells[2, 3] = "单据编号";
                excel.Cells[2, 4] = "库房";
                excel.Cells[2, 5] = "商品名称";
                excel.Cells[2, 6] = "单位";
                excel.Cells[2, 7] = "货位";
                excel.Cells[2, 8] = "异动数量";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 8]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 5], excel.Cells[2, 5]).ColumnWidth = 50;


                DataRow[] dtHaveThreeCategory = dt.Select("ProductCategoryName3 is not null");
                DataRow[] dtHaveTwoCategory = dt.Select("ProductCategoryName3 is null and ProductCategoryName2 is not null");
                DataRow[] dtHaveOneCategory = dt.Select("ProductCategoryName2 is null and ProductCategoryName3 is null");

                int row = 3;

                foreach (var item in dtHaveThreeCategory.AsEnumerable().GroupBy(F => F.Field<string>("ProductCategoryName3")))
                {
                    SetExcelFormat(excel, ref  row, item);
                }

                foreach (var item in dtHaveTwoCategory.AsEnumerable().GroupBy(F => F.Field<string>("ProductCategoryName2")))
                {
                    SetExcelFormat(excel, ref  row, item);
                }

                foreach (var item in dtHaveOneCategory.AsEnumerable().GroupBy(F => F.Field<string>("ProductCategoryName1")))
                {
                    SetExcelFormat(excel, ref row, item);
                }

                excel.Cells[row, 1] = "总计:";
                excel.get_Range(excel.Cells[row, 8], excel.Cells[row, 8]).Formula = string.Format("=SUM(H3:H{0})", row - 1);//设置求和公式

                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void SetExcelFormat(Microsoft.Office.Interop.Excel.Application excel, ref int row, IGrouping<string, DataRow> item)
        {
            excel.Cells[row, 1] = item.Key;
            excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 1]).Interior.Color = "255";

            row++;

            foreach (var dr in item)
            {
                excel.Cells[row, 1] = dr["InvoiceType"];
                excel.Cells[row, 2] = dr["Date"].ToString();
                excel.Cells[row, 3] = dr["InvoiceId"];
                excel.Cells[row, 4] = dr["DepotName"];
                excel.Cells[row, 5] = dr["ProductName"];
                excel.Cells[row, 6] = dr["ProductUnit"];
                excel.Cells[row, 7] = dr["DepotPositionName"];
                excel.Cells[row, 8] = dr["Quantity"].ToString();

                row++;
            }
            row++;
        }
    }
}