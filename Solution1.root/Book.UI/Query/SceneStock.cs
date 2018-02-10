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
    public partial class SceneStock : DevExpress.XtraEditors.XtraForm
    {
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.StockManager stockManager = new Book.BL.StockManager();
        BL.ProduceInDepotDetailManager produceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.ProduceMaterialdetailsManager produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();
        BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();
        BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        BL.WorkHouseManager workHouseManager = new Book.BL.WorkHouseManager();
        List<Model.Product> resultList = new List<Model.Product>();

        public SceneStock()
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
            resultList.Clear();

            DateTime dateTime = global::Helper.DateTimeParse.EndDate;
            IList<Model.Product> listProduct = productManager.SelectIdAndStock(this.lue_ProductCategory.EditValue == null ? null : this.lue_ProductCategory.EditValue.ToString());

            string workHouseYanpian = workHouseManager.SelectWorkHouseIdByName("验片");
            string workHouseZuzhuang = workHouseManager.SelectWorkHouseIdByName("组装现场仓");
            string workHouseChengpinZuzhuang = workHouseManager.SelectWorkHouseIdByName("成品组装");


            foreach (Model.Product item in listProduct)
            {
                item.StocksQuantity = item.StocksQuantity.HasValue ? item.StocksQuantity : 0;
                Dictionary<string, double> parentProductDic = new Dictionary<string, double>();
                GetParentProductInfo("'" + item.ProductId + "'", parentProductDic);

                #region 现场数量

                //查询商品对应的未结案加工单
                IList<Model.PronoteHeader> phList = pronoteHeaderManager.SelectByProductId(item.ProductId);
                if (phList == null || phList.Count == 0)
                    continue;
                foreach (var phGroup in phList.GroupBy(P => P.CustomerInvoiceXOId))
                {
                    string pronoteHeaderIds = "";
                    string invoiceXOIds = "";
                    foreach (var ph in phGroup)
                    {
                        pronoteHeaderIds += "'" + ph.PronoteHeaderID + "',";
                        invoiceXOIds += "'" + ph.InvoiceXOId + "',";
                    }
                    pronoteHeaderIds = pronoteHeaderIds.TrimEnd(',');
                    invoiceXOIds = invoiceXOIds.TrimEnd(',');

                    if (string.IsNullOrEmpty(pronoteHeaderIds))
                        continue;

                    #region 验片：合计前单位转入 - 合计生产数量（包含合计合格数量，合计不良品）

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
                    //领到 组装现场 部门的数量
                    double materialQty = 0;
                    if (!string.IsNullOrEmpty(invoiceXOIds))
                        materialQty = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, dateTime.AddSeconds(-1), workHouseZuzhuang, invoiceXOIds);

                    //计算所有转入 组装现场 部门的数量
                    Model.ProduceInDepotDetail pidZuzhuangIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId, dateTime.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);  //计算即时现场库存时不用区分订单，不需要加工单号，查询所有即可，这里要区分订单，所以要加上加工单号
                    double zuzhuangTransferIn = Convert.ToDouble(pidZuzhuangIn.ProduceTransferQuantity);

                    //计算 组装现场 部门转入其他部门的数量
                    Model.ProduceInDepotDetail pidZuzhuangOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateTime.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                    double zuzhuangTransferOut = Convert.ToDouble(pidZuzhuangOut.ProduceTransferQuantity);


                    #region 查询商品对应的所有母件 入库 扣减

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

                    double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty;
                    zuzhuangXianchang = zuzhuangXianchang < 0 ? 0 : zuzhuangXianchang;
                    item.XianchangZuzhuang = zuzhuangXianchang;

                    #endregion

                    resultList.Add(new Book.Model.Product() { Id = item.Id, ProductVersion = item.ProductVersion, ProductName = item.ProductName, CustomerInvoiceXOId = phGroup.Key, XianchangYanpian = item.XianchangYanpian, XianchangZuzhuang = item.XianchangZuzhuang });
                }

                #endregion
            }

            this.bindingSourceProduct.DataSource = resultList;
            this.gridControl1.RefreshDataSource();
        }


        private void barExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (resultList == null || resultList.Count == 0)
            {
                MessageBox.Show("无数据！", "提示", MessageBoxButtons.OK);
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

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 3]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 25;
                excel.Cells[1, 1] = "商品现场库存";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 3], excel.Cells[1, 3]).HorizontalAlignment = -4108;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).ColumnWidth = 50;
                excel.get_Range(excel.Cells[3, 2], excel.Cells[3 + resultList.Count, 2]).HorizontalAlignment = -4152;

                excel.Cells[2, 1] = "商品名称";
                excel.Cells[2, 2] = "客户订单号码";
                excel.Cells[2, 3] = "现场数量"; ;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 3]).Interior.Color = "12566463";

                int row = 3;

                foreach (var item in resultList)
                {
                    excel.Cells[row, 1] = item.ProductName;
                    excel.Cells[row, 2] = item.CustomerInvoiceXOId;
                    excel.Cells[row, 3] = item.XianchangTotal;

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
    }
}