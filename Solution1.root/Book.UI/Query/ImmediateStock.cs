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
    public partial class ImmediateStock : DevExpress.XtraEditors.XtraForm
    {
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.StockManager stockManager = new Book.BL.StockManager();
        BL.ProduceInDepotDetailManager produceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.ProduceMaterialdetailsManager produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();
        BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();
        BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        BL.WorkHouseManager workHouseManager = new Book.BL.WorkHouseManager();
        BL.MaterialManager materialManager = new Book.BL.MaterialManager();
        IList<Model.Product> listProduct;
        BL.ProduceMaterialExitDetailManager produceMaterialExitDetailManager = new Book.BL.ProduceMaterialExitDetailManager();

        public ImmediateStock()
        {
            InitializeComponent();

            this.bindingSourceProductCategory.DataSource = new BL.ProductCategoryManager().Select();
        }

        private void GetParentProductInfo(string productId, Dictionary<string, double> parentProductDic)
        {
            IList<Model.BomComponentInfo> bomComponentList = bomComponentInfoManager.SelectBomIdAndUseQty(productId);
            if (bomComponentList == null || bomComponentList.Count == 0)
                return;

            string bomIds = "";
            foreach (var component in bomComponentList)
            {
                bomIds += "'" + component.BomId + "',";
            }
            bomIds = bomIds.TrimEnd(',');

            IList<Model.BomParentPartInfo> bomParentList = bomParentPartInfoManager.SelectProducts(bomIds);
            string productIds = "";
            foreach (var parent in bomParentList)
            {
                productIds += "'" + parent.ProductId + "',";

                Model.BomComponentInfo comInfo = bomComponentList.First(C => C.BomId == parent.BomId);

                if (!parentProductDic.Keys.Contains(parent.ProductId))
                {
                    double value = Convert.ToDouble(comInfo.UseQuantity);
                    if (parentProductDic.Keys.Contains(comInfo.ProductId))
                        value = value * parentProductDic[comInfo.ProductId];

                    parentProductDic.Add(parent.ProductId, value);
                }
            }
            productIds = productIds.TrimEnd(',');

            GetParentProductInfo(productIds, parentProductDic);   //递归调用
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show("请先选择日期", "提示", MessageBoxButtons.OK);
                return;
            }

            DateTime dateTime = this.dateEdit1.DateTime.Date.AddDays(1);
            listProduct = productManager.SelectIdAndStock(this.lue_ProductCategory.EditValue == null ? null : this.lue_ProductCategory.EditValue.ToString());

            IList<Model.StockSeach> stockList = new List<Model.StockSeach>();

            string workHouseYanpian = workHouseManager.SelectWorkHouseIdByName("验片");
            string workHouseZuzhuang = workHouseManager.SelectWorkHouseIdByName("组装现场仓");
            string workHouseChengpinZuzhuang = workHouseManager.SelectWorkHouseIdByName("成品组装");

            foreach (Model.Product item in listProduct)
            {
                item.StocksQuantity = item.StocksQuantity.HasValue ? item.StocksQuantity : 0;
                item.InitialQty = stockManager.SelectStockQuantity0(item.ProductId);

                #region 仓库数量
                stockList = this.stockManager.SelectJiShi(item.ProductId, dateTime, DateTime.Now);

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


                #region 现场数量

                #region 验片：合计前单位转入 - 合计生产数量（包含合计合格数量，合计不良品）
                //查询商品对应的未结案加工单
                IList<Model.PronoteHeader> phList = pronoteHeaderManager.SelectByProductId(item.ProductId);
                if (phList == null || phList.Count == 0)
                    continue;
                string pronoteHeaderIds = "";
                string invoiceXOIds = "";
                foreach (var ph in phList)
                {
                    pronoteHeaderIds += "'" + ph.PronoteHeaderID + "',";
                    invoiceXOIds += "'" + ph.InvoiceXOId + "',";
                }
                pronoteHeaderIds = pronoteHeaderIds.TrimEnd(',');
                invoiceXOIds = invoiceXOIds.TrimEnd(',');

                //计算所有转入 验片 部门的数量
                Model.ProduceInDepotDetail pidYanpianIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId, dateTime.AddSeconds(-1), workHouseYanpian, pronoteHeaderIds);
                double yanpianTransferIn = Convert.ToDouble(pidYanpianIn.ProduceTransferQuantity);

                //计算 验片 部门的生产数量
                Model.ProduceInDepotDetail pidYanpianOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateTime.AddSeconds(-1), workHouseYanpian, pronoteHeaderIds);
                double yanpianProcedures = Convert.ToDouble(pidYanpianOut.ProceduresSum);
                double yanpianBuliang = Convert.ToDouble(pidYanpianOut.ProceduresSum - pidYanpianOut.CheckOutSum);

                double yanpianXianchang = yanpianTransferIn - yanpianProcedures;
                yanpianXianchang = yanpianXianchang < 0 ? 0 : yanpianXianchang;

                item.XianchangYanpian = yanpianXianchang;
                #endregion



                #region 组装现场:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）
                //2018年2月22日13:18:54： 组装现场:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）- 生产退料（从组装现场退的）
                //领到 组装现场 部门的数量
                double materialQty = 0;
                if (!string.IsNullOrEmpty(invoiceXOIds))
                    materialQty = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, dateTime.AddSeconds(-1), workHouseZuzhuang, invoiceXOIds);
                //计算所有转入 组装现场 部门的数量
                Model.ProduceInDepotDetail pidZuzhuangIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId, dateTime.AddSeconds(-1), workHouseZuzhuang, null);   //转入组装现场时没有加工单
                double zuzhuangTransferIn = Convert.ToDouble(pidZuzhuangIn.ProduceTransferQuantity);

                //计算 组装现场 部门转入其他部门的数量
                Model.ProduceInDepotDetail pidZuzhuangOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateTime.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                double zuzhuangTransferOut = Convert.ToDouble(pidZuzhuangOut.ProduceTransferQuantity);

                //计算 从组装现场退回的 生产退料
                double exitQty = produceMaterialExitDetailManager.SelectSumQtyFromZuzhuang(item.ProductId, dateTime.AddSeconds(-1), workHouseZuzhuang);


                #region 查询商品对应的所有母件 入库 扣减

                Dictionary<string, double> parentProductDic = new Dictionary<string, double>();

                GetParentProductInfo("'" + item.ProductId + "'", parentProductDic);

                string proIds = "";
                foreach (var str in parentProductDic.Keys)
                {
                    proIds += "'" + str + "',";
                }
                proIds = proIds.TrimEnd(',');

                double deductionQty = 0;
                if (!string.IsNullOrEmpty(proIds))
                {
                    IList<Model.ProduceInDepotDetail> pids = produceInDepotDetailManager.SelectIndepotQty(proIds, dateTime.AddSeconds(-1), workHouseChengpinZuzhuang, invoiceXOIds);
                    foreach (var pid in pids)
                    {
                        deductionQty += Convert.ToDouble(pid.ProduceQuantity) * parentProductDic[pid.ProductId];
                    }
                }

                #endregion

                //double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty;
                double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty - exitQty;
                zuzhuangXianchang = zuzhuangXianchang < 0 ? 0 : zuzhuangXianchang;

                item.XianchangZuzhuang = zuzhuangXianchang;

                #endregion
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

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 5]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 10;
                excel.Cells[1, 1] = "商品即时库存(" + this.dateEdit1.DateTime.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 5], excel.Cells[1, 5]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "商品名称";
                excel.Cells[2, 2] = "仓库数量";
                excel.Cells[2, 3] = "验片现场";
                excel.Cells[2, 4] = "组装现场";
                excel.Cells[2, 5] = "总数";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 5 + 1 + listProduct[0].MaterialDic.Keys.Count]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).ColumnWidth = 50;

                int col = 7;
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
                        excel.Cells[row, 1] = pro.ProductName;
                        //excel.Cells[row, 2] = pro.StocksQuantity - pro.XianchangTotal;
                        excel.Cells[row, 2] = pro.StocksQuantity;
                        //excel.Cells[row, 3] = pro.XianchangTotal;
                        excel.Cells[row, 3] = pro.XianchangYanpian;
                        excel.Cells[row, 4] = pro.XianchangZuzhuang;
                        excel.Cells[row, 5] = pro.TotalQty;

                        col = 7;
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
                    //excel.Cells[row, 1] = item.Key;
                    //excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 1]).Interior.Color = "255";    //红色
                    //row++;
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.ProductName;
                        //excel.Cells[row, 2] = pro.StocksQuantity - pro.XianchangTotal;
                        excel.Cells[row, 2] = pro.StocksQuantity;
                        //excel.Cells[row, 3] = pro.XianchangTotal;
                        excel.Cells[row, 3] = pro.XianchangYanpian;
                        excel.Cells[row, 4] = pro.XianchangZuzhuang;
                        excel.Cells[row, 5] = pro.TotalQty;

                        col = 7;
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
                    //excel.Cells[row, 1] = item.Key;
                    //excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 1]).Interior.Color = "255";    //红色
                    //row++;
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.ProductName;
                        //excel.Cells[row, 2] = pro.StocksQuantity - pro.XianchangTotal;
                        excel.Cells[row, 2] = pro.StocksQuantity;
                        //excel.Cells[row, 3] = pro.XianchangTotal;
                        excel.Cells[row, 3] = pro.XianchangYanpian;
                        excel.Cells[row, 4] = pro.XianchangZuzhuang;
                        excel.Cells[row, 5] = pro.TotalQty;

                        col = 7;
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
            excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 5 + 1 + listProduct[0].MaterialDic.Keys.Count]).Interior.Color = "255";    //红色
            excel.get_Range(excel.Cells[row, 2], excel.Cells[row, 2]).Formula = string.Format("=SUM(B{0}:B{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 3], excel.Cells[row, 3]).Formula = string.Format("=SUM(C{0}:C{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 4], excel.Cells[row, 4]).Formula = string.Format("=SUM(D{0}:D{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 5], excel.Cells[row, 5]).Formula = string.Format("=SUM(E{0}:E{1})", row + 1, row + item.Count());

            col = 7;
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