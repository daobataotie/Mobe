using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Query
{
    public partial class Q15JiShiForm : BaseForm
    {
        private BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();
        private BL.StockManager stockManager = new Book.BL.StockManager();
        private BL.DepotPositionManager depotPositionMananger = new BL.DepotPositionManager();
        private BL.DepotManager depotManager = new BL.DepotManager();
        private DataTable dt = new DataTable();
        private BL.ProductManager productManager = new Book.BL.ProductManager();
        public Q15JiShiForm()
        {
            InitializeComponent();
            this.bindingSourceDepot.DataSource = (new BL.DepotManager()).Select();
            this.bindingSourceProductCategory.DataSource = (new BL.ProductCategoryManager()).Select();
            this.dateEdit1.DateTime = DateTime.Now.Date;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.DateNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime date = this.dateEdit1.DateTime.Date.AddDays(1);
            IList<Model.StockSeach> stockList = new List<Model.StockSeach>();
            dt = this.miscDataManager.SelectByCondition("Q15", this.lookUpEditDepotStar.EditValue == null ? null : this.lookUpEditDepotStar.EditValue.ToString(), lookUpEditDepotPosition.EditValue == null ? null : lookUpEditDepotPosition.EditValue.ToString(), null, this.textProductNameOrId.Text, this.btn_ProductNameStart.Text, this.btn_ProductNameEnd.Text, this.LookUpProductCategoryStart.EditValue == null ? null : this.LookUpProductCategoryStart.EditValue.ToString(), this.lookUpProductCategoryEnd.EditValue == null ? null : this.lookUpProductCategoryEnd.EditValue.ToString(), true);//this.checkEditShowZeroProduct.Checked 改为true，因为即时库存查的是当天库存，当天库存不为0，现在为0的情况就会查不出，故此，不显示为0库存要在查出以后做判断。

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0 || dt.Rows[i]["productid"].ToString() != dt.Rows[i - 1]["productid"].ToString())
                {
                    stockList = this.stockManager.SelectJiShi(dt.Rows[i]["productid"].ToString(), date, DateTime.Now);
                    dt.Rows[i]["yifenpeiquantity"] = this.stockManager.SelectJiShidistributioned(dt.Rows[i]["productid"].ToString(), date, DateTime.Now).ToString("0.####");
                }


                if (stockList != null && stockList.Count > 0)
                {

                    var list = from s in stockList
                               where s.PositionName == dt.Rows[i]["posoid"].ToString() //调拨单为进
                               orderby s.InvoiceDate.Value.Date descending
                               select s;

                    if (list.Where(l => l.InvoiceTypeIndex == 3).Count() > 0)
                    {

                        Model.StockSeach seach = list.Where(l => l.InvoiceTypeIndex == 3)
                            .OrderBy(o => o.InvoiceDate.Value.Date).ThenBy(d => d.InsertTime.Value).FirstOrDefault();


                        list = list.Where(l => l.InvoiceDate.Value.Date <= seach.InvoiceDate.Value.Date && l.InsertTime.Value < seach.InsertTime.Value)
                             .OrderByDescending(o => o.InvoiceDate.Value.Date);

                        dt.Rows[i]["Quantity"] = seach.StockCheckBookQuantity;


                    }




                    if (list != null && list.Count() > 0)
                    {
                        foreach (Model.StockSeach stock in list.ToList<Model.StockSeach>())
                        {

                            if (stock.InvoiceTypeIndex == 0)
                            {
                                dt.Rows[i]["Quantity"] = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()) + stock.InvoiceQuantity.Value;

                            }
                            if (stock.InvoiceTypeIndex == 1)
                            {
                                dt.Rows[i]["Quantity"] = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()) - stock.InvoiceQuantity.Value;

                            }
                            if (stock.InvoiceTypeIndex == 2)
                            {
                                dt.Rows[i]["Quantity"] = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()) - stock.InvoiceQuantity.Value;

                            }

                            //已定未入
                            if (stock.InvoiceType == "採購入庫單")
                            {
                                dt.Rows[i]["OrderOnWayQuantity"] = Convert.ToDouble(dt.Rows[i]["OrderOnWayQuantity"]) + stock.InvoiceQuantity.Value;
                            }
                        }

                        var list1 = from s in stockList
                                    where s.OutPositionName == dt.Rows[i]["posoid"].ToString() //挑拨单为出
                                    orderby s.InvoiceDate.Value.Date descending
                                    select s;

                        foreach (Model.StockSeach stock in list1.ToList<Model.StockSeach>())
                        {

                            if (stock.InvoiceTypeIndex == 2)
                            {
                                dt.Rows[i]["Quantity"] = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()) + stock.InvoiceQuantity.Value;

                            }

                        }


                    }
                }

            }

            dt.AcceptChanges();
            if (dt.Rows.Count < 1)
                MessageBox.Show("无数据", this.Text);
            dt.DefaultView.Sort = "spid,Quantity asc";
            dt = dt.DefaultView.ToTable();
            DataRow[] dr = dt.Select("Quantity<>'0'");
            if (!this.checkEditShowZeroProduct.Checked && dr.Count() > 0)
                dt = dr.CopyToDataTable();
            this.gridControl1.DataSource = dt;
            this.labelControl1.Text = dt.Rows.Count.ToString() + " 项";

            //this.condition.DepotEnd = this.lookUpEditDepotEnd.EditValue == null ? null : this.lookUpEditDepotEnd.EditValue.ToString();
            //this.condition.DepotStart = this.lookUpEditDepotStar.EditValue == null ? null : this.lookUpEditDepotStar.EditValue.ToString();
            //this.condition.ProductNameStart = this.btn_ProductNameStart.Text;
            //this.condition.ProductNameEnd = this.btn_ProductNameEnd.Text;
            //this.condition.ProduceCategoryStart = this.LookUpProductCategoryStart.EditValue == null ? null : this.LookUpProductCategoryStart.EditValue.ToString();
            //this.condition.ProductCategoryEnd = this.lookUpProductCategoryEnd.EditValue == null ? null : this.lookUpProductCategoryEnd.EditValue.ToString();
        }

        private void btn_ProductNameStart_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_ProductNameStart.Text = (f.SelectedItem as Model.Product).ProductName;
        }

        private void btn_ProductNameEnd_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_ProductNameEnd.Text = (f.SelectedItem as Model.Product).ProductName;
        }
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R14(dt);
        }

        private void lookUpEditDepotPosition_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void lookUpEditDepotStar_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditDepotStar.EditValue != null)
            {
                //if (this.barEditItem3.EditValue.ToString() == "True")
                this.bindingSourceDepotPosition.DataSource = this.depotPositionMananger.Select(new Model.Depot() { DepotId = this.lookUpEditDepotStar.EditValue.ToString() });
                this.lookUpEditDepotPosition.EditValue = null;
            }
        }

        private void btn_MaterialCount_Click(object sender, EventArgs e)
        {
            if (this.dt.Rows.Count < 1)
            {
                MessageBox.Show("无数据", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Settings.BasicData.Products.ProductConvertMaterial f = new Book.UI.Settings.BasicData.Products.ProductConvertMaterial(dt);
                f.ShowDialog(this);
            }
        }

        private void btn_ProductNameStart_EditValueChanged(object sender, EventArgs e)
        {
            this.btn_ProductNameEnd.EditValue = this.btn_ProductNameStart.EditValue;
        }

        private void LookUpProductCategoryStart_EditValueChanged(object sender, EventArgs e)
        {
            this.lookUpProductCategoryEnd.EditValue = this.LookUpProductCategoryStart.EditValue;
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                return;
            }
            string productIDs = "(";


            DataTable excelDT = new DataTable();
            excelDT.Columns.Add("ProductId", typeof(string));
            excelDT.Columns.Add("Id", typeof(string));
            excelDT.Columns.Add("ProductName", typeof(string));
            excelDT.Columns.Add("Quantity", typeof(string));
            excelDT.Columns.Add("ProductCategoryName1", typeof(string));
            excelDT.Columns.Add("ProductCategoryName2", typeof(string));
            excelDT.Columns.Add("ProductCategoryName3", typeof(string));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0 || dt.Rows[i]["productid"].ToString() != dt.Rows[i - 1]["productid"].ToString())
                {
                    excelDT.Rows.Add(dt.Rows[i]["productid"].ToString(), dt.Rows[i]["spid"].ToString(), dt.Rows[i]["productName"].ToString(), dt.Rows[i]["Quantity"].ToString());
                    productIDs += "'" + dt.Rows[i]["productid"].ToString() + "',";
                }
                else
                {
                    excelDT.Rows[excelDT.Rows.Count - 1]["Quantity"] = Convert.ToDouble(excelDT.Rows[excelDT.Rows.Count - 1]["Quantity"]) + Convert.ToDouble(dt.Rows[i]["Quantity"]);
                }
            }

            productIDs = productIDs.TrimEnd(',') + ")";
            DataTable dtCategory = productManager.SelectProductCategoryByProductIds(productIDs);

            excelDT.AsEnumerable().ToList().ForEach(P =>
            {
                DataRow dr = dtCategory.AsEnumerable().First(D => D.Field<string>("ProductId") == P.Field<string>("productid"));
                P["ProductCategoryName1"] = dr["ProductCategoryName1"];
                P["ProductCategoryName2"] = dr["ProductCategoryName2"];
                P["ProductCategoryName3"] = dr["ProductCategoryName3"];
            });

            DataRow[] dtHaveThreeCategory = excelDT.Select("ProductCategoryName3 is not null");
            DataRow[] dtHaveTwoCategory = excelDT.Select("ProductCategoryName3 is null and ProductCategoryName2 is not null");
            DataRow[] dtHaveOneCategory = excelDT.Select("ProductCategoryName2 is null and ProductCategoryName3 is null");


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

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 3]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 20;
                excel.Cells[1, 1] = "商品即时库存(" + this.dateEdit1.DateTime.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                excel.get_Range(excel.Cells[1, 3], excel.Cells[1, 3]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "商品编号";
                excel.Cells[2, 2] = "商品名称";
                excel.Cells[2, 3] = "即时库存";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 3]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 2]).ColumnWidth = 50;
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
                excel.Cells[row, 1] = dr["Id"];
                excel.Cells[row, 2] = dr["ProductName"];
                excel.Cells[row, 3] = dr["Quantity"].ToString();

                row++;
            }
            row++;
        }
    }
}