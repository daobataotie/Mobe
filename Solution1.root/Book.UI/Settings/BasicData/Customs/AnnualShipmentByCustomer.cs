using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
//using CrystalDecisions.Shared;
using System.Reflection;
using Microsoft.CSharp;
using DevExpress.XtraCharts;
using Microsoft.Office.Interop.Excel;
using DevExpress.XtraGrid.Columns;
//using Microsoft.Office.Core;


namespace Book.UI.Settings.BasicData.Customs
{
    public partial class AnnualShipmentByCustomer : DevExpress.XtraEditors.XtraForm
    {
        BL.InvoiceXSDetailManager invoiceXSDetailManager = new Book.BL.InvoiceXSDetailManager();
        IList<Model.Product> customerProductList;
        List<ProductShipment> productShipmentList = new List<ProductShipment>();
        List<Model.Customer> Customers = new List<Book.Model.Customer>();

        private string CustomerNames
        {
            get
            {
                if (Customers == null || Customers.Count == 0)
                    return null;
                else
                {
                    string names = "";
                    Customers.ForEach(C =>
                    {
                        names += C.CustomerShortName + ",";
                    });
                    names = names.TrimEnd(',');

                    return names;
                }
            }
        }

        private string CustomerIds
        {
            get
            {
                if (Customers == null || Customers.Count == 0)
                    return null;
                else
                {
                    string ids = "";
                    Customers.ForEach(C =>
                    {
                        ids += "'" + C.CustomerId + "',";
                    });
                    ids = ids.TrimEnd(',');

                    return ids;
                }
            }
        }

        public AnnualShipmentByCustomer()
        {
            InitializeComponent();
            this.bindingSourceProduct.DataSource = new BL.ProductManager().GetProductBaseInfo();

            //this.nccCustomer.Choose = new Customs.ChooseCustoms();
            //this.slue_Customer.Properties.DataSource = new BL.CustomerManager().Select();
            //this.slue_Customer.Properties.DisplayMember = "CustomerShortName";
            //this.slue_Customer.Properties.ValueMember = "CustomerId";

            //this.slue_Customer.Properties.View.Columns.Add(new GridColumn() { FieldName = "IsChecked", Caption = "選擇", Width = 30, Visible = true, VisibleIndex = 0 });
            //this.slue_Customer.Properties.View.Columns.Add(new GridColumn() { FieldName = "Id", Caption = "客戶編號", Width = 100, Visible = true, VisibleIndex = 1 });
            //this.slue_Customer.Properties.View.Columns.Add(new GridColumn() { FieldName = "CustomerShortName", Caption = "客戶簡稱", Width = 150, Visible = true, VisibleIndex = 2 });
            //this.slue_Customer.Properties.View.Columns.Add(new GridColumn() { FieldName = "CustomerFullName", Caption = "客戶全稱", Width = 150, Visible = true, VisibleIndex = 3 });
            //this.slue_Customer.Properties.View.Columns.Add(new GridColumn() { FieldName = "CustomerName", Caption = "客戶", Width = 150, Visible = true, VisibleIndex = 4 });
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            productShipmentList.Clear();
            if (this.customerProductList == null || this.customerProductList.Count == 0)
            {
                MessageBox.Show("客戶商品不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }
            if (this.date_Start.EditValue == null)
            {
                MessageBox.Show("起始日期不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }
            if (this.date_End.EditValue == null)
            {
                MessageBox.Show("結束日期不能為空！", "提示！", MessageBoxButtons.OK);
                return;
            }
            int showType = this.radioGroup1.SelectedIndex;

            //this.bindingSourceHeader.DataSource = invoiceXSDetailManager.SelectAnnualShipment((this.but_Product.EditValue as Model.Product).ProductId, this.date_Start.DateTime, this.date_End.DateTime, (this.nccCustomer.EditValue == null ? null : (this.nccCustomer.EditValue as Model.Customer).CustomerId));
            //this.gridControl1.RefreshDataSource();

            System.Data.DataTable dt;


            foreach (var item in customerProductList)
            {
                dt = invoiceXSDetailManager.SelectAnnualShipment(item.ProductId, this.date_Start.DateTime, this.date_End.DateTime, null, showType);

                ProductShipment ps;

                if (productShipmentList.Any(d => d.CustomerProductName == item.CustomerProductName))
                {
                    ps = productShipmentList.First(d => d.CustomerProductName == item.CustomerProductName);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ShipmentDetail sd = ps.ShipmentDetail.First(d => d.Year == dr["ShipmentYear"].ToString());
                            sd.Quantity = (Convert.ToDecimal(dr["ShipmentQuantity"].ToString()) + Convert.ToDecimal(sd.Quantity)).ToString();
                        }
                    }
                }
                else
                {
                    ps = new ProductShipment();
                    ps.ProductName = item.ProductName;
                    ps.CustomerProductName = item.CustomerProductName;
                    ps.ProductNameGroup = item.ProductCategoryName;
                    ps.ShipmentDetail = new List<ShipmentDetail>();

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ShipmentDetail sd = new ShipmentDetail();
                            sd.Year = dr["ShipmentYear"].ToString();
                            sd.Quantity = dr["ShipmentQuantity"].ToString();
                            ps.ShipmentDetail.Add(sd);
                        }
                    }

                    productShipmentList.Add(ps);
                }
                if (showType == 0)
                {
                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        if (!ps.ShipmentDetail.Any(d => d.Year == i.ToString()))
                        {
                            ShipmentDetail sd = new ShipmentDetail();
                            sd.Year = i.ToString();
                            sd.Quantity = "0";
                            ps.ShipmentDetail.Add(sd);
                        }
                    }
                }
                else
                {
                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            if (!ps.ShipmentDetail.Any(d => d.Year == i.ToString() + "." + j.ToString()))
                            {
                                ShipmentDetail sd = new ShipmentDetail();
                                sd.Year = i.ToString() + "." + j.ToString();
                                sd.Quantity = "0";
                                ps.ShipmentDetail.Add(sd);
                            }
                        }
                    }
                }
                //ps.ShipmentDetail = ps.ShipmentDetail.OrderBy(d => d.Year).ToList();
            }
            if (productShipmentList.Count == 0)
            {
                MessageBox.Show("無數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
            try
            {
                productShipmentList = productShipmentList.OrderBy(P => P.ProductName).ToList();
                var group = productShipmentList.GroupBy(G => G.ProductNameGroup).ToList();
                int moreCol = group.Count;

                Type objClassType = null;
                objClassType = Type.GetTypeFromProgID("Excel.Application");
                if (objClassType == null)
                {
                    MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                    return;
                }

                #region 反射方式
                //productShipmentList = new List<ProductShipment> { new ProductShipment { CustomerProductName = "", ProductName = "", ShipmentDetail = new List<ShipmentDetail> { new ShipmentDetail { Year = "", Quantity = "" } } } };
                //object objExcel = Activator.CreateInstance(objClassType);
                //objExcel.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null, objExcel, new object[] { true });
                //object wookBook = objExcel.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, objExcel, null);
                //wookBook.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, wookBook, new object[] { true });
                //object rangeMerge = objExcel.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objExcel, new object[] { string.Format("A1:A{0}", productShipmentList.Count) });
                //rangeMerge.GetType().InvokeMember("MergeCells", BindingFlags.SetProperty, null, rangeMerge, new object[] { true });
                //object cells = objExcel.GetType().InvokeMember("Cells", BindingFlags.GetProperty, null, objExcel, null);
                //cells.GetType().InvokeMember("ColumnWidth", BindingFlags.SetProperty, null, cells, new object[] { 25 });
                //object cell11 = objExcel.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objExcel, new object[] { "A1:A1" });
                //cell11.GetType().InvokeMember("Value2", BindingFlags.SetProperty, null, cell11, new object[] { (this.nccCustomer.EditValue as Model.Customer).CustomerShortName });
                //cell11.GetType().InvokeMember("RowHeight", BindingFlags.SetProperty, null, cell11, new object[] { 25 });
                //object font = cell11.GetType().InvokeMember("Font", BindingFlags.GetProperty, null, cell11, null);
                //font.GetType().InvokeMember("Size", BindingFlags.SetProperty, null, font, new object[] { 20 });
                //object cellDate = objExcel.GetType().InvokeMember("Range",BindingFlags.GetProperty,null,objExcel,new object[]{string."A:"});
                #endregion

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                //dynamic excel = Activator.CreateInstance(objClassType);
                excel.Application.Workbooks.Add(true);
                //Microsoft.Office.Interop.Excel.Line l = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                #region 明细
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
                Microsoft.Office.Interop.Excel.Range r = sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, productShipmentList.Count + moreCol - 1]);
                //dynamic r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, productShipmentList.Count]);
                r.MergeCells = true;//合并单元格

                //Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter = -4108;
                //Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium= -4138;
                //Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic= -4105;


                sheet.Cells.ColumnWidth = 12;
                sheet.Cells[1, 1] = this.CustomerNames;
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 1]).RowHeight = 25;
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 1]).Font.Size = 20;
                sheet.Cells[1, productShipmentList.Count + moreCol] = DateTime.Now.ToString("yyyy.MM.dd");
                sheet.get_Range(sheet.Cells[1, productShipmentList.Count + moreCol], sheet.Cells[1, productShipmentList.Count + moreCol]).HorizontalAlignment = -4108;

                int rowCount = this.date_End.DateTime.Year - this.date_Start.DateTime.Year + 1;
                if (showType != 0)
                    rowCount = rowCount * 13;

                //excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).BorderAround(1, -4138, -4105, "#000000");
                //excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).BorderAround(1, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");
                sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[3, productShipmentList.Count + moreCol]).Interior.ColorIndex = 15;
                sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[rowCount + 3, productShipmentList.Count + moreCol]).HorizontalAlignment = -4108;
                sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[rowCount + 3, productShipmentList.Count + moreCol]).WrapText = true;
                sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[rowCount + 3, productShipmentList.Count + moreCol]).EntireRow.AutoFit();
                sheet.get_Range(sheet.Cells[4, 1], sheet.Cells[rowCount + 3, productShipmentList.Count + moreCol]).RowHeight = 20;
                sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[rowCount + 3, productShipmentList.Count + moreCol + 1]).Font.Size = 12;
                //sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[this.date_End.DateTime.Year - this.date_Start.DateTime.Year + 1 + 2, productShipmentList.Count + 1]).BorderAround(LineStyle.SingleLine, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");
                sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[rowCount + 3, productShipmentList.Count + moreCol]).BorderAround(1, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, null);
                sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[rowCount + 3, productShipmentList.Count + moreCol]).Borders.Value = 1;

                int rows = 4;
                if (showType == 0)  //年
                {
                    for (int j = 0; j < productShipmentList.Count; j++)
                    {
                        sheet.Cells[2, j + 2] = productShipmentList[j].CustomerProductName;
                        //sheet.get_Range(sheet.Cells[2, j + 2], sheet.Cells[2, j + 2]).BorderAround(1, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");

                        //2017年5月13日21:00:53  第三行加商品名稱
                        sheet.Cells[3, j + 2] = productShipmentList[j].ProductName;
                        //sheet.get_Range(sheet.Cells[3, j + 2], sheet.Cells[3, j + 2]).BorderAround(1, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");
                    }

                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        sheet.Cells[rows, 1] = i.ToString() + "年";
                        //sheet.get_Range(sheet.Cells[rows, 1], sheet.Cells[rows, 1]).BorderAround(1, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");

                        for (int j = 0; j < productShipmentList.Count; j++)
                        {
                            sheet.Cells[rows, j + 2] = productShipmentList[j].ShipmentDetail.First(d => d.Year == i.ToString()).Quantity;
                            //sheet.get_Range(sheet.Cells[rows, j + 2], sheet.Cells[rows, j + 2]).BorderAround(1, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, "#000000");
                        }

                        rows++;
                    }
                }
                else                //月
                {
                    //for (int j = 0; j < productShipmentList.Count; j++)
                    //{
                    //    sheet.Cells[2, j + 2] = productShipmentList[j].CustomerProductName;
                    //    sheet.Cells[3, j + 2] = productShipmentList[j].ProductName;


                    //}

                    int col = 2;
                    foreach (var g in group)
                    {
                        foreach (var pro in g)
                        {
                            sheet.Cells[2, col] = pro.CustomerProductName;
                            sheet.Cells[3, col] = pro.ProductName;

                            col++;
                        }

                        col++;
                    }

                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        for (int l = 1; l <= 13; l++)
                        {
                            if (l <= 12)
                            {
                                sheet.Cells[rows, 1] = i.ToString() + "年" + l.ToString() + "月";

                                //for (int j = 0; j < productShipmentList.Count; j++)
                                //{
                                //    sheet.Cells[rows, j + 2] = productShipmentList[j].ShipmentDetail.First(d => d.Year == i.ToString() + "." + l.ToString()).Quantity;
                                //}

                                col = 2;
                                foreach (var g in group)
                                {
                                    foreach (var pro in g)
                                    {
                                        sheet.Cells[rows, col] = pro.ShipmentDetail.First(d => d.Year == i.ToString() + "." + l.ToString()).Quantity;

                                        col++;
                                    }

                                    col++;
                                }
                            }
                            else
                            {
                                sheet.Cells[rows, 1] = "合計";
                                sheet.get_Range(sheet.Cells[rows, 1], sheet.Cells[rows, productShipmentList.Count + moreCol + 1]).Interior.Color = "6750207";

                                //for (int j = 0; j < productShipmentList.Count; j++)
                                //{
                                //    sheet.get_Range(sheet.Cells[rows, j + 2], sheet.Cells[rows, j + 2]).Formula = string.Format("=SUM({2}{0}:{2}{1})", rows - 12, rows - 1, CountExcelColumnName(j + 2));
                                //}
                                col = 2;
                                foreach (var g in group)
                                {
                                    foreach (var pro in g)
                                    {
                                        sheet.get_Range(sheet.Cells[rows, col], sheet.Cells[rows, col]).Formula = string.Format("=SUM({2}{0}:{2}{1})", rows - 12, rows - 1, CountExcelColumnName(col));

                                        col++;
                                    }

                                    sheet.Cells[rows, col] = string.Format("=SUM({2}{0}:{1}{0})", rows, CountExcelColumnName(col - 1), CountExcelColumnName(col - g.Count()));
                                    col++;
                                }

                                //sheet.Cells[rows, productShipmentList.Count + 2] = string.Format("=SUM(B{0}:{1}{0})", rows, CountExcelColumnName(productShipmentList.Count + 1));
                                //以上是兩種設置Excel格式的寫法
                            }

                            rows++;
                        }
                    }
                }
                #endregion

                #region 汇总
                if (group.Count > 1)
                {
                    excel.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                    Microsoft.Office.Interop.Excel.Worksheet sheet2 = ((Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[2]);
                    sheet2.Rows.AutoFit();
                    sheet2.Rows.WrapText = true;
                    sheet2.Rows.RowHeight = 20;
                    sheet2.Rows.Font.Size = 12;
                    sheet2.Columns.ColumnWidth = 20;
                    sheet2.get_Range(sheet2.Cells[1, 1], sheet2.Cells[group.Count + 1, (this.date_End.DateTime.Year - this.date_Start.DateTime.Year) + 2]).BorderAround(1, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, null);
                    sheet2.get_Range(sheet2.Cells[1, 1], sheet2.Cells[group.Count + 1, (this.date_End.DateTime.Year - this.date_Start.DateTime.Year) + 2]).Borders.Value = 1;

                    sheet2.Cells[1, 2] = "";

                    int yearCol = 2;
                    int dataRow = 2;

                    foreach (var g in group)
                    {
                        sheet2.Cells[dataRow, 1] = g.Key;
                        dataRow++;
                    }
                    for (int i = this.date_Start.DateTime.Year; i <= this.date_End.DateTime.Year; i++)
                    {
                        sheet2.Cells[1, yearCol] = i.ToString() + "年";

                        dataRow = 2;
                        foreach (var g in group)
                        {
                            sheet2.Cells[dataRow, yearCol] = g.Sum(D => D.ShipmentDetail.Where(P => P.Year.Contains(i.ToString())).Sum(S => decimal.Parse(S.Quantity)));
                            dataRow++;
                        }
                        yearCol++;
                    }
                }

                #endregion

                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = XlWindowState.xlMaximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void btn_ChooseCustomerProduct_Click(object sender, EventArgs e)
        {
            //if (this.nccCustomer.EditValue != null)
            //{
            //    CustomerProduct f;
            //    if (this.customerProductList != null && this.customerProductList.Count > 0)
            //        f = new CustomerProduct(this.nccCustomer.EditValue as Model.Customer, this.customerProductList);
            //    else
            //        f = new CustomerProduct(this.nccCustomer.EditValue as Model.Customer);
            //    f.ShowDialog(this);
            //    this.customerProductList = f.SelectProduct;
            //}
            //else
            //{
            //    MessageBox.Show("請先選擇客戶！", "提示！", MessageBoxButtons.OK);
            //    return;
            //}

            if (this.Customers != null && this.Customers.Count > 0)
            {
                CustomerProduct f;
                if (this.customerProductList != null && this.customerProductList.Count > 0)
                    f = new CustomerProduct(this.CustomerIds, this.customerProductList);
                else
                    f = new CustomerProduct(this.CustomerIds);
                f.ShowDialog(this);
                this.customerProductList = f.SelectProduct;
            }
            else
            {
                MessageBox.Show("請先選擇客戶！", "提示！", MessageBoxButtons.OK);
                return;
            }

        }

        private void btn_Customer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseCustomsForm2 f = new ChooseCustomsForm2(Customers);
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.Customers = f.Customers.Where(C => C.IsChecked == true).ToList();

                this.btn_Customer.Text = this.CustomerNames;
            }
        }

        private static string CountExcelColumnName(int i)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (i <= 26)
                return str.ToCharArray()[i - 1].ToString();
            else
            {
                int count = (int)Math.Floor(Convert.ToDecimal(i / 26));
                if (i % 26 == 0)
                {
                    return str.ToCharArray()[count - 2].ToString() + "Z";
                }
                else
                {
                    return str.ToCharArray()[count - 1].ToString() + str.ToCharArray()[i % 26 - 1].ToString();
                }
            }
        }
    }

    internal class ProductShipment
    {
        public string ProductName { get; set; }

        public string CustomerProductName { get; set; }

        public string ProductNameGroup { get; set; }

        public List<ShipmentDetail> ShipmentDetail { get; set; }
    }

    internal class ShipmentDetail
    {
        public string Year { get; set; }

        public string Quantity { get; set; }
    }

}