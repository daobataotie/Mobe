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
    //待转区库存查询，类似现场库存
    public partial class PendingAreaStock : DevExpress.XtraEditors.XtraForm
    {
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.StockManager stockManager = new Book.BL.StockManager();
        BL.ProduceInDepotDetailManager produceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.WorkHouseManager workHouseManager = new Book.BL.WorkHouseManager();
        BL.MaterialManager materialManager = new Book.BL.MaterialManager();
        IList<Model.Product> listProduct;

        public PendingAreaStock()
        {
            InitializeComponent();

            this.bindingSourceProductCategory.DataSource = new BL.ProductCategoryManager().Select();

            IList<string> handBookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in handBookIds)
            {
                this.cob_HandBookId.Properties.Items.Add(item);
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (this.date_End.EditValue == null)
            {
                MessageBox.Show("请先选择查询日期", "提示", MessageBoxButtons.OK);
                return;
            }
            if (this.lue_ProductCategory.EditValue == null)
            {
                MessageBox.Show("请先选择商品类别", "提示", MessageBoxButtons.OK);
                return;
            }

            DateTime dateEnd = this.date_End.DateTime.Date.AddDays(1);
            string handBookId = this.cob_HandBookId.Text;

            listProduct = productManager.SelectIdAndStock(this.lue_ProductCategory.EditValue == null ? null : this.lue_ProductCategory.EditValue.ToString());

            IList<Model.StockSeach> stockList = new List<Model.StockSeach>();

            string workHousePendingArea = workHouseManager.SelectWorkHouseIdByName("仓库待转区");

            foreach (Model.Product item in listProduct)
            {
                item.HandbookId = handBookId;

                item.StocksQuantity = item.StocksQuantity.HasValue ? item.StocksQuantity : 0;
                item.InitialQty = stockManager.SelectStockQuantity0(item.ProductId);

                #region 仓库数量
                stockList = this.stockManager.SelectJiShi(item.ProductId, dateEnd, DateTime.Now);

                double panQty = 0;
                double outQty = 0;
                double inQty = 0;

                //如果有 盘点单,盘点算入(ex:由100→200，则算作入库(200-100=100))
                if (stockList != null && stockList.Count > 0)
                {
                    //0 出，1 入，3 盘点     2 调拨，库存不变
                    panQty = Convert.ToDouble(stockList.Where(I => I.InvoiceTypeIndex == 3).Sum(S => S.InvoiceQuantity - S.StockCheckBookQuantity));
                    outQty = Convert.ToDouble(stockList.Where(I => I.InvoiceTypeIndex == 0).Sum(S => S.InvoiceQuantity)); //出库数量
                    inQty = Convert.ToDouble(stockList.Where(I => I.InvoiceTypeIndex == 1 && !string.IsNullOrEmpty(I.PositionName)).Sum(S => S.InvoiceQuantity));  //入库数量

                    item.StocksQuantity = item.StocksQuantity + outQty - inQty - panQty;
                }
                #endregion


                #region 待转区库存：合计前单位转入 - 合计转出/入库 ,从2019.1.1开始计算
                DateTime startDate = new DateTime(2019, 1, 1);

                IList<Model.PronoteHeader> phList = null;

                if (string.IsNullOrEmpty(handBookId))     //2018年12月7日20:52:30 : 加手册
                    phList = pronoteHeaderManager.SelectByProductId(startDate, dateEnd.AddSeconds(-1), item.ProductId);
                else
                    phList = pronoteHeaderManager.SelectByProductId(startDate, dateEnd.AddSeconds(-1), item.ProductId, handBookId);
                //if (phList == null || phList.Count == 0)
                //    continue;

                string pronoteHeaderIds = "";
                string invoiceXOIds = "";

                if (phList != null && phList.Count > 0)
                {
                    foreach (var ph in phList)
                    {
                        pronoteHeaderIds += "'" + ph.PronoteHeaderID + "',";
                        invoiceXOIds += "'" + ph.InvoiceXOId + "',";
                    }
                    pronoteHeaderIds = pronoteHeaderIds.TrimEnd(',');
                    invoiceXOIds = invoiceXOIds.TrimEnd(',');

                    //计算所有转入 仓库待转区 部门的数量
                    Model.ProduceInDepotDetail pidYanpianIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId, dateEnd.AddSeconds(-1), workHousePendingArea, pronoteHeaderIds);
                    double pendingAreaTransferIn = Convert.ToDouble(pidYanpianIn.ProduceTransferQuantity);

                    //计算 仓库待转区 部门的转出/入库 数量
                    Model.ProduceInDepotDetail pidYanpianOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateEnd.AddSeconds(-1), workHousePendingArea, pronoteHeaderIds);
                    double pendingAreaTransferOut = Convert.ToDouble(pidYanpianOut.ProduceTransferQuantity);
                    double pendingAreaInDepot = Convert.ToDouble(pidYanpianOut.ProduceQuantity);


                    double pendingAreaStock = pendingAreaTransferIn - pendingAreaTransferOut - pendingAreaInDepot;
                    pendingAreaStock = pendingAreaStock < 0 ? 0 : pendingAreaStock;

                    item.PendingAreaStock = pendingAreaStock;
                }

                #endregion
            }

            this.bindingSourceProduct.DataSource = listProduct;
        }

        private void barExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.listProduct == null || this.listProduct.Count == 0)
            {
                MessageBox.Show("无数据！", "提示", MessageBoxButtons.OK);
                return;
            }

            try
            {
                ConvertMaterial();

                Type objClassType = null;
                objClassType = Type.GetTypeFromProgID("Excel.Application");
                if (objClassType == null)
                {
                    MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 6]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 10;
                excel.Cells[1, 1] = "仓库待转区库存(" + this.date_End.DateTime.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 6]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "商品编号";
                excel.Cells[2, 2] = "商品名称";
                excel.Cells[2, 3] = "仓库库存";
                excel.Cells[2, 4] = "待转区库存";
                excel.Cells[2, 5] = "总数";
                excel.Cells[2, 6] = "手册号";

                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 7 + 1 + listProduct[0].MaterialDic.Keys.Count]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).ColumnWidth = 25;
                excel.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).ColumnWidth = 50;

                int col = 8;
                //原料
                foreach (var item in listProduct[0].MaterialDic)
                {
                    excel.Cells[2, col++] = item.Key;
                }


                List<Model.Product> haveThreeCategoryPro = listProduct.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.Product> haveTwoCategoryPro = listProduct.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.Product> haveOneCategoryPro = listProduct.Where(P => string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();


                int row = 3;

                foreach (var item in haveThreeCategoryPro.GroupBy(P => P.ProductCategoryName3))
                {
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.Id;
                        excel.Cells[row, 2] = pro.ProductName;
                        excel.Cells[row, 3] = pro.StocksQuantity;
                        excel.Cells[row, 4] = pro.PendingAreaStock;
                        excel.Cells[row, 5] = pro.TotalQty;
                        excel.Cells[row, 6] = pro.HandbookId;

                        col = 8;
                        foreach (var dic in pro.MaterialDic)
                        {
                            excel.Cells[row, col++] = dic.Value;
                        }

                        row++;
                    }
                    row++;
                }

                foreach (var item in haveTwoCategoryPro.GroupBy(P => P.ProductCategoryName2))
                {
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.Id;
                        excel.Cells[row, 2] = pro.ProductName;
                        excel.Cells[row, 3] = pro.StocksQuantity;
                        excel.Cells[row, 4] = pro.PendingAreaStock;
                        excel.Cells[row, 5] = pro.TotalQty;
                        excel.Cells[row, 6] = pro.HandbookId;

                        col = 8;
                        foreach (var dic in pro.MaterialDic)
                        {
                            excel.Cells[row, col++] = dic.Value;
                        }

                        row++;
                    }
                    row++;
                }

                foreach (var item in haveOneCategoryPro.GroupBy(P => P.ProductCategoryName))
                {
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.Id;
                        excel.Cells[row, 2] = pro.ProductName;
                        excel.Cells[row, 3] = pro.StocksQuantity;
                        excel.Cells[row, 4] = pro.PendingAreaStock;
                        excel.Cells[row, 5] = pro.TotalQty;
                        excel.Cells[row, 6] = pro.HandbookId;

                        col = 8;
                        foreach (var dic in pro.MaterialDic)
                        {
                            excel.Cells[row, col++] = dic.Value;
                        }

                        row++;
                    }
                    row++;
                }

                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = XlWindowState.xlMaximized;
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void SetExcelFormat(Microsoft.Office.Interop.Excel.Application excel, ref int col, ref int row, IGrouping<string, Book.Model.Product> item)
        {
            excel.Cells[row, 1] = item.Key;
            excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 6 + 1 + listProduct[0].MaterialDic.Keys.Count]).Interior.Color = "255";    //红色
            excel.get_Range(excel.Cells[row, 3], excel.Cells[row, 3]).Formula = string.Format("=SUM(C{0}:C{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 4], excel.Cells[row, 4]).Formula = string.Format("=SUM(D{0}:D{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 5], excel.Cells[row, 5]).Formula = string.Format("=SUM(E{0}:E{1})", row + 1, row + item.Count());

            col = 8;
            foreach (var ec in listProduct[0].MaterialDic)
            {
                string excelColumnName = CountExcelColumnName(col);
                excel.get_Range(excel.Cells[row, col], excel.Cells[row, col]).Formula = string.Format("=SUM({2}{0}:{2}{1})", row + 1, row + item.Count(), excelColumnName);
                col++;
            }

            row++;
        }

        private void ConvertMaterial()
        {
            IList<string> str = materialManager.SelectMaterialCategory();
            Dictionary<string, string> dic = new Dictionary<string, string>();


            foreach (var pro in listProduct)
            {
                pro.MaterialDic = new Dictionary<string, string>();
                foreach (var item in str)
                {
                    pro.MaterialDic.Add(item, "0");
                }


                if (!string.IsNullOrEmpty(pro.MaterialIds))
                {
                    string[] materialIds = pro.MaterialIds.Split(',');
                    string[] materialnums = pro.MaterialNum.Split(',');

                    for (int i = 0; i < materialIds.Length; i++)
                    {
                        Model.Material model = materialManager.Get(materialIds[i]);
                        if (model != null)
                        {
                            double value = Convert.ToDouble(materialnums[i]) * Convert.ToDouble(model.JWeight) * pro.TotalQty;
                            pro.MaterialDic[model.MaterialCategoryName] = (Convert.ToDouble(pro.MaterialDic[model.MaterialCategoryName]) + (value / 1000)).ToString("0.####");
                        }
                    }
                }
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
}