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
        BL.ProduceMaterialExitDetailManager produceMaterialExitDetailManager = new Book.BL.ProduceMaterialExitDetailManager();

        public SceneStock()
        {
            InitializeComponent();

            this.bindingSourceProductCategory.DataSource = new BL.ProductCategoryManager().Select();

            IList<string> handBookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in handBookIds)
            {
                this.cob_HandBookId.Properties.Items.Add(item);
            }
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

            #region 旧版，弃用
            //foreach (var parent in bomParentList)
            //{
            //    productIds += "'" + parent.ProductId + "',";

            //    Model.BomComponentInfo comInfo = bomComponentList.First(C => C.BomId == parent.BomId);

            //    if (!parentProductDic.Keys.Contains(parent.ProductId))
            //    {
            //        double value = Convert.ToDouble(comInfo.UseQuantity);
            //        if (parentProductDic.Keys.Contains(comInfo.ProductId))
            //            value = value * parentProductDic[comInfo.ProductId];

            //        parentProductDic.Add(parent.ProductId, value);
            //    }
            //} 
            #endregion

            #region 新版，一个子件被母件引用N次，叠加计算
            foreach (var comInfo in bomComponentList)
            {
                Model.BomParentPartInfo parent = bomParentList.First(P => P.BomId == comInfo.BomId);
                productIds += "'" + parent.ProductId + "',";

                if (!parentProductDic.Keys.Contains(parent.ProductId))
                {
                    double value = Convert.ToDouble(comInfo.UseQuantity);
                    if (parentProductDic.Keys.Contains(comInfo.ProductId))
                        value = value * parentProductDic[comInfo.ProductId];

                    parentProductDic.Add(parent.ProductId, value);
                }
                else
                {
                    double value = Convert.ToDouble(comInfo.UseQuantity);
                    if (parentProductDic.Keys.Contains(comInfo.ProductId))
                        value = value * parentProductDic[comInfo.ProductId];

                    parentProductDic[parent.ProductId] = parentProductDic[parent.ProductId] + value;
                }
            }
            #endregion

            productIds = productIds.TrimEnd(',');

            GetParentProductInfo(productIds, parentProductDic);   //递归调用
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            resultList.Clear();

            DateTime dateEnd = global::Helper.DateTimeParse.EndDate;
            IList<Model.Product> listProduct = productManager.SelectIdAndStock(this.lue_ProductCategory.EditValue == null ? null : this.lue_ProductCategory.EditValue.ToString(),null);

            string workHouseYanpian = workHouseManager.SelectWorkHouseIdByName("验片");
            string workHouseZuzhuang = workHouseManager.SelectWorkHouseIdByName("组装现场仓");
            string workHouseChengpinZuzhuang = workHouseManager.SelectWorkHouseIdByName("成品组装");
            string handBookId = this.cob_HandBookId.Text;

            foreach (Model.Product item in listProduct)
            {
                item.HandbookId = handBookId;

                item.StocksQuantity = item.StocksQuantity.HasValue ? item.StocksQuantity : 0;
                Dictionary<string, double> parentProductDic = new Dictionary<string, double>();
                GetParentProductInfo("'" + item.ProductId + "'", parentProductDic);

                #region 现场数量

                //查询商品对应的未结案加工单       2018年7月3日22:17:36 改：只查询2018.1.1 之后的订单
                DateTime startDate = new DateTime(2018, 1, 1);
                IList<Model.PronoteHeader> phList = null;

                if (string.IsNullOrEmpty(handBookId))     //2018年12月7日20:52:30 : 加手册
                    phList = pronoteHeaderManager.SelectByProductId(startDate, dateEnd.AddSeconds(-1), item.ProductId);
                else
                    phList = pronoteHeaderManager.SelectByProductId(startDate, dateEnd.AddSeconds(-1), item.ProductId, handBookId);

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
                    Model.ProduceInDepotDetail pidYanpianIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId, dateEnd.AddSeconds(-1), workHouseYanpian, pronoteHeaderIds);
                    double yanpianTransferIn = Convert.ToDouble(pidYanpianIn.ProduceTransferQuantity);

                    //计算 验片 部门的生产数量
                    Model.ProduceInDepotDetail pidYanpianOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateEnd.AddSeconds(-1), workHouseYanpian, pronoteHeaderIds);
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
                        materialQty = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, invoiceXOIds);

                    //计算所有转入 组装现场 部门的数量
                    //Model.ProduceInDepotDetail pidZuzhuangIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);  //计算即时现场库存时不用区分订单，不需要加工单号，查询所有即可，这里要区分订单，所以要加上加工单号
                    //double zuzhuangTransferIn = Convert.ToDouble(pidZuzhuangIn.ProduceTransferQuantity);
                    IList<Model.ProduceInDepotDetail> pidZuzhuangIn = produceInDepotDetailManager.SelectTransZuZhuangXianChang(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                    double zuzhuangTransferIn = pidZuzhuangIn.Sum(P => P.ProduceTransferQuantity).Value;
                    string xoIDs = "";
                    foreach (string xoid in pidZuzhuangIn.Select(D => D.InvoiceXOId).Distinct())
                    {
                        xoIDs += "'" + xoid + "',";
                    }
                    xoIDs = xoIDs.TrimEnd(',');

                    //计算 组装现场 部门转入其他部门的数量
                    Model.ProduceInDepotDetail pidZuzhuangOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                    double zuzhuangTransferOut = Convert.ToDouble(pidZuzhuangOut.ProduceTransferQuantity);

                    //计算 从组装现场退回的 生产退料
                    double exitQty = produceMaterialExitDetailManager.SelectSumQtyFromZuzhuang(item.ProductId, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, xoIDs);


                    #region 查询商品对应的所有母件 入库 扣减
                    double deductionQty = 0;
                    if (!string.IsNullOrEmpty(xoIDs))
                    {
                        string proIds = "";
                        foreach (var str in parentProductDic.Keys)
                        {
                            proIds += "'" + str + "',";
                        }
                        proIds = proIds.TrimEnd(',');

                        if (!string.IsNullOrEmpty(proIds))
                        {
                            //IList<Model.ProduceInDepotDetail> pids = produceInDepotDetailManager.SelectIndepotQty(proIds, dateEnd.AddSeconds(-1), workHouseChengpinZuzhuang, invoiceXOIds);
                            IList<Model.ProduceInDepotDetail> pids = produceInDepotDetailManager.SelectIndepotQty(proIds, dateEnd.AddSeconds(-1), workHouseChengpinZuzhuang, xoIDs); //对应转到组装现场的生产入库单的客户订单，如果订单不在范围内，母件入库不扣减
                            foreach (var pid in pids)
                            {
                                deductionQty += Convert.ToDouble(pid.ProduceQuantity) * parentProductDic[pid.ProductId];
                            }

                            //2018年8月1日22:51:32  对应的母件领到组装现场的数量
                            List<Model.ProduceMaterialdetails> pmds = produceMaterialdetailsManager.SelectMaterialsByProductIds(proIds, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, xoIDs).ToList();
                            foreach (var pmd in pmds)
                            {
                                //如果母件有领料，对应抵消入库扣减
                                if (pids.Any(P => P.ProductId == pmd.ProductId))
                                {
                                    deductionQty -= pmd.Materialprocessum.HasValue ? pmd.Materialprocessum.Value * parentProductDic[pmd.ProductId] : 0;
                                }
                                else
                                {
                                    Dictionary<string, double> fatherDic = new Dictionary<string, double>();
                                    GetParentProductInfo("'" + pmd.ProductId + "'", fatherDic);
                                    if (pids.Any(P => fatherDic.Keys.Contains(P.ProductId)))
                                    {
                                        //deductionQty -= pmd.Materialprocessum.HasValue ? pmd.Materialprocessum.Value * parentProductDic[pmd.ProductId] * fatherDic[pids.First(P => fatherDic.Keys.Contains(P.ProductId)).ProductId] : 0;
                                        deductionQty -= pmd.Materialprocessum.HasValue ? pmd.Materialprocessum.Value * parentProductDic[pmd.ProductId] : 0;
                                        //这里是商品对应的半成品母件领料扣减，不是对应的成品母件(上面if才是对应的成品母件)，所以系数乘以半成品母件的就够了.
                                    }
                                }
                            }

                            deductionQty = deductionQty < 0 ? 0 : deductionQty;

                            //2018年8月16日11:26:19  对应的母件退料，组装现场数量扣减
                            List<Model.ProduceMaterialExitDetail> pmeds = produceMaterialExitDetailManager.SelectSumQtyFromZuzhuangByPros(proIds, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, xoIDs).ToList();
                            foreach (var pmed in pmeds)
                            {
                                exitQty += pmed.ProduceQuantity.Value * parentProductDic[pmed.ProductId];
                            }
                        }
                    }
                    #endregion

                    //double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty;
                    double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty - exitQty;
                    zuzhuangXianchang = zuzhuangXianchang < 0 ? 0 : zuzhuangXianchang;
                    item.XianchangZuzhuang = zuzhuangXianchang;

                    #endregion

                    resultList.Add(new Book.Model.Product() { Id = item.Id, ProductVersion = item.ProductVersion, ProductName = item.ProductName, CustomerInvoiceXOId = phGroup.Key, XianchangYanpian = item.XianchangYanpian, XianchangZuzhuang = item.XianchangZuzhuang, HandbookId = item.HandbookId, CnName = item.CnName });
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

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 5]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 25;
                excel.Cells[1, 1] = "商品现场库存";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 3], excel.Cells[1, 4]).HorizontalAlignment = -4108;
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).ColumnWidth = 50;
                excel.get_Range(excel.Cells[3, 2], excel.Cells[5 + resultList.Count, 2]).HorizontalAlignment = -4152;

                excel.Cells[2, 1] = "商品名称";
                excel.Cells[2, 2] = "客户订单号码";
                excel.Cells[2, 3] = "现场数量";
                excel.Cells[2, 4] = "手册号";
                excel.Cells[2, 5] = "单位";

                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 5]).Interior.Color = "12566463";

                int row = 3;

                foreach (var item in resultList)
                {
                    excel.Cells[row, 1] = item.ProductName;
                    excel.Cells[row, 2] = item.CustomerInvoiceXOId;
                    excel.Cells[row, 3] = item.XianchangTotal;
                    excel.Cells[row, 4] = item.HandbookId;
                    excel.Cells[row, 5] = item.CnName;

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