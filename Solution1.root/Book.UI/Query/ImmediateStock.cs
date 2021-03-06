﻿using System;
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
    /// <summary>
    /// 即时库存(现场版)
    /// </summary>
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
            //    else
            //    {
            //        double value = Convert.ToDouble(comInfo.UseQuantity);
            //        if (parentProductDic.Keys.Contains(comInfo.ProductId))
            //            value = value * parentProductDic[comInfo.ProductId];

            //        parentProductDic[parent.ProductId] = parentProductDic[parent.ProductId] + value;
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

            listProduct = productManager.SelectIdAndStock(this.lue_ProductCategory.EditValue == null ? null : this.lue_ProductCategory.EditValue.ToString(), this.lue_ProductCategoryEnd.EditValue == null ? null : this.lue_ProductCategoryEnd.EditValue.ToString());

            IList<Model.StockSeach> stockList = new List<Model.StockSeach>();

            string workHouseYanpian = workHouseManager.SelectWorkHouseIdByName("验片");
            string workHouseZuzhuang = workHouseManager.SelectWorkHouseIdByName("组装现场仓");
            string workHouseChengpinZuzhuang = workHouseManager.SelectWorkHouseIdByName("成品组装");

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


                #region 现场数量  这边改动，其他两个地方也要对应修改(1,Book.UI.Query.SceneStock   2,Book.UI.Settings.StockLimitations.AssemblySiteDifferenceForm)

                #region 验片：合计前单位转入 - 合计生产数量（包含合计合格数量，合计不良品）
                //查询商品对应的未结案加工单    2018年7月3日22:17:36 改：只查询2018.1.1 之后的订单
                DateTime startDate = new DateTime(2018, 1, 1);

                IList<Model.PronoteHeader> phList = null;

                if (string.IsNullOrEmpty(handBookId))     //2018年12月7日20:52:30 : 加手册
                    phList = pronoteHeaderManager.SelectByProductId(startDate, dateEnd.AddSeconds(-1), item.ProductId);
                else
                    phList = pronoteHeaderManager.SelectByProductId(startDate, dateEnd.AddSeconds(-1), item.ProductId, handBookId);
                //if (phList == null || phList.Count == 0)
                //    continue;

                string pronoteHeaderIds = "";
                string invoiceXOIds = "";
                string allInvoiceXOIds = "";

                if (phList != null && phList.Count > 0)
                {
                    foreach (var ph in phList)
                    {
                        pronoteHeaderIds += "'" + ph.PronoteHeaderID + "',";
                        invoiceXOIds += "'" + ph.InvoiceXOId + "',";
                    }
                    pronoteHeaderIds = pronoteHeaderIds.TrimEnd(',');
                    allInvoiceXOIds = invoiceXOIds = invoiceXOIds.TrimEnd(',');

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
                }
                #endregion



                #region 组装现场:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）                         2018年5月22日21:48:44,这边修改的时候对应到“组装现场盘点差异”也要修改

                //2018年2月22日13:18:54： 组装现场:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）- 生产退料（从组装现场退的）

                double zuzhuangTransferIn = 0;
                double zuzhuangTransferOut = 0;
                double exitQty = 0;
                double deductionQty = 0;
                //领到 组装现场 部门的数量
                double materialQty = 0;
                //if (!string.IsNullOrEmpty(invoiceXOIds))
                //    materialQty = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, invoiceXOIds);
                //2018年5月17日00:34:42 只要是未结案的订单领到组装现场的都计入     2018年7月3日22:17:36 改：只查询2017.10.1 之后的订单
                //materialQty = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang);
                //2018年7月9日23:07:51 领料单所包含的未结案订单号码拉出来，用于查询母件入库扣减
                System.Data.DataTable dt = null;

                //2018年12月7日21:19:44 ：增加手册
                if (string.IsNullOrEmpty(handBookId))
                    dt = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, invoiceXOIds);
                else
                    dt = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, invoiceXOIds, handBookId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        materialQty += Convert.ToDouble(dr["Materialprocessum"].ToString());

                        if (!invoiceXOIds.Contains(dr["InvoiceId"].ToString()))
                        {
                            allInvoiceXOIds = "'" + dr["InvoiceId"].ToString() + "'," + allInvoiceXOIds;
                        }
                    }
                    allInvoiceXOIds = allInvoiceXOIds.TrimEnd(',');
                }

                #region 查询商品对应的所有母件 入库 扣减
                //if (!string.IsNullOrEmpty(xoIDs))
                //if (!string.IsNullOrEmpty(invoiceXOIds))
                if (!string.IsNullOrEmpty(allInvoiceXOIds))
                {
                    Dictionary<string, double> parentProductDic = new Dictionary<string, double>();

                    GetParentProductInfo("'" + item.ProductId + "'", parentProductDic);

                    string proIds = "";
                    foreach (var str in parentProductDic.Keys)
                    {
                        proIds += "'" + str + "',";
                    }
                    proIds = proIds.TrimEnd(',');

                    if (!string.IsNullOrEmpty(proIds))
                    {
                        //IList<Model.ProduceInDepotDetail> pids = produceInDepotDetailManager.SelectIndepotQty(proIds, dateEnd.AddSeconds(-1), workHouseChengpinZuzhuang, xoIDs); //对应转到组装现场的生产入库单的客户订单，如果订单不在范围内，母件入库不扣减
                        //IList<Model.ProduceInDepotDetail> pids = produceInDepotDetailManager.SelectIndepotQty(proIds, dateEnd.AddSeconds(-1), workHouseChengpinZuzhuang, invoiceXOIds);

                        //2018年7月9日23:21:00  加上领料单对应的订单
                        IList<Model.ProduceInDepotDetail> pids = produceInDepotDetailManager.SelectIndepotQty(proIds, dateEnd.AddSeconds(-1), workHouseChengpinZuzhuang, allInvoiceXOIds);

                        foreach (var pid in pids)
                        {
                            deductionQty += Convert.ToDouble(pid.ProduceQuantity) * parentProductDic[pid.ProductId];
                        }


                        //2018年8月1日22:51:32  对应的母件领到组装现场的数量
                        List<Model.ProduceMaterialdetails> pmds = produceMaterialdetailsManager.SelectMaterialsByProductIds(proIds, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, allInvoiceXOIds).ToList();
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
                        List<Model.ProduceMaterialExitDetail> pmeds = produceMaterialExitDetailManager.SelectSumQtyFromZuzhuangByPros(proIds, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, allInvoiceXOIds).ToList();
                        foreach (var pmed in pmeds)
                        {
                            exitQty += pmed.ProduceQuantity.Value * parentProductDic[pmed.ProductId];
                        }
                    }
                }

                #endregion


                //计算 从组装现场退回的 生产退料
                exitQty += produceMaterialExitDetailManager.SelectSumQtyFromZuzhuang(item.ProductId, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, allInvoiceXOIds);


                #region 计算所有转入 组装现场 部门的数量

                if (phList != null && phList.Count > 0)
                {
                    //Model.ProduceInDepotDetail pidZuzhuangIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId,  dateEnd.AddSeconds(-1), workHouseZuzhuang, null);   //转入组装现场时没有加工单
                    //double zuzhuangTransferIn = Convert.ToDouble(pidZuzhuangIn.ProduceTransferQuantity);
                    IList<Model.ProduceInDepotDetail> pidZuzhuangIn = produceInDepotDetailManager.SelectTransZuZhuangXianChang(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                    zuzhuangTransferIn = pidZuzhuangIn.Sum(P => P.ProduceTransferQuantity).Value;

                    //计算 组装现场 部门转入其他部门的数量
                    Model.ProduceInDepotDetail pidZuzhuangOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                    zuzhuangTransferOut = Convert.ToDouble(pidZuzhuangOut.ProduceTransferQuantity);

                }
                #endregion

                //double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty;
                double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty - exitQty;
                zuzhuangXianchang = zuzhuangXianchang < 0 ? 0 : zuzhuangXianchang;

                item.XianchangZuzhuang = zuzhuangXianchang;

                #endregion

                #endregion
            }

            if (!checkEdit1.Checked)  //不显示0库存商品
                listProduct = listProduct.Where(p => p.XianchangZuzhuang != 0).ToList();

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

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 8]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 10;
                excel.Cells[1, 1] = "商品即时库存(" + this.date_End.DateTime.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 8]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "商品编号";
                excel.Cells[2, 2] = "商品名称";
                excel.Cells[2, 3] = "仓库数量";
                excel.Cells[2, 4] = "验片现场";
                excel.Cells[2, 5] = "组装现场";
                excel.Cells[2, 6] = "总数";
                excel.Cells[2, 7] = "手册号";
                excel.Cells[2, 8] = "单位";
                excel.Cells[2, 9] = "单个商品净重";


                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 8 + 1 + listProduct[0].MaterialDic.Keys.Count]).Interior.Color = "12566463";   //灰色
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).ColumnWidth = 25;
                excel.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).ColumnWidth = 50;

                int col = 10;
                //原料
                foreach (var item in listProduct[0].MaterialDic)
                {
                    excel.Cells[2, col++] = item.Key;
                }


                List<Model.Product> haveThreeCategoryPro = listProduct.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.Product> haveTwoCategoryPro = listProduct.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.Product> haveOneCategoryPro = listProduct.Where(P => string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();


                int row = 3;

                System.Action<IGrouping<string, Model.Product>> setExcelValue = (item) => 
                {
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.Id;
                        excel.Cells[row, 2] = pro.ProductName;
                        //excel.Cells[row, 2] = pro.StocksQuantity - pro.XianchangTotal;
                        excel.Cells[row, 3] = pro.StocksQuantity;
                        //excel.Cells[row, 3] = pro.XianchangTotal;
                        excel.Cells[row, 4] = pro.XianchangYanpian;
                        excel.Cells[row, 5] = pro.XianchangZuzhuang;
                        excel.Cells[row, 6] = pro.TotalQty;
                        excel.Cells[row, 7] = pro.HandbookId;
                        excel.Cells[row, 8] = pro.CnName;
                        excel.Cells[row, 9] = (pro.NetWeight.Value / 1000).ToString("0.#####");

                        col = 10;
                        foreach (var dic in pro.MaterialDic)
                        {
                            excel.Cells[row, col++] = dic.Value;
                        }

                        row++;
                    }
                };

                foreach (var item in haveThreeCategoryPro.GroupBy(P => P.ProductCategoryName3))
                {
                    //SetExcelFormat(excel, ref col, ref row, item);

                    //foreach (var pro in item)
                    //{
                    //    excel.Cells[row, 1] = pro.Id;
                    //    excel.Cells[row, 2] = pro.ProductName;
                    //    //excel.Cells[row, 2] = pro.StocksQuantity - pro.XianchangTotal;
                    //    excel.Cells[row, 3] = pro.StocksQuantity;
                    //    //excel.Cells[row, 3] = pro.XianchangTotal;
                    //    excel.Cells[row, 4] = pro.XianchangYanpian;
                    //    excel.Cells[row, 5] = pro.XianchangZuzhuang;
                    //    excel.Cells[row, 6] = pro.TotalQty;
                    //    excel.Cells[row, 7] = pro.HandbookId;
                    //    excel.Cells[row, 8] = pro.CnName;
                    //    excel.Cells[row, 9] = (pro.NetWeight.Value / 1000).ToString("0.#####");

                    //    col = 10;
                    //    foreach (var dic in pro.MaterialDic)
                    //    {
                    //        excel.Cells[row, col++] = dic.Value;
                    //    }

                    //    row++;
                    //}

                    //每组数据之间不留空行了，方便筛选数据
                    //row++;

                    setExcelValue(item);
                }

                foreach (var item in haveTwoCategoryPro.GroupBy(P => P.ProductCategoryName2))
                {
                    //SetExcelFormat(excel, ref col, ref row, item);

                    //foreach (var pro in item)
                    //{
                    //    excel.Cells[row, 1] = pro.Id;
                    //    excel.Cells[row, 2] = pro.ProductName;
                    //    //excel.Cells[row, 2] = pro.StocksQuantity - pro.XianchangTotal;
                    //    excel.Cells[row, 3] = pro.StocksQuantity;
                    //    //excel.Cells[row, 3] = pro.XianchangTotal;
                    //    excel.Cells[row, 4] = pro.XianchangYanpian;
                    //    excel.Cells[row, 5] = pro.XianchangZuzhuang;
                    //    excel.Cells[row, 6] = pro.TotalQty;
                    //    excel.Cells[row, 7] = pro.HandbookId;
                    //    excel.Cells[row, 8] = pro.CnName;
                    //    excel.Cells[row, 9] = (pro.NetWeight.Value / 1000).ToString("0.#####");

                    //    col = 10;
                    //    foreach (var dic in pro.MaterialDic)
                    //    {
                    //        excel.Cells[row, col++] = dic.Value;
                    //    }

                    //    row++;
                    //}

                    //row++;

                    setExcelValue(item);
                }

                foreach (var item in haveOneCategoryPro.GroupBy(P => P.ProductCategoryName))
                {
                    //SetExcelFormat(excel, ref col, ref row, item);

                    //foreach (var pro in item)
                    //{
                    //    excel.Cells[row, 1] = pro.Id;
                    //    excel.Cells[row, 2] = pro.ProductName;
                    //    //excel.Cells[row, 2] = pro.StocksQuantity - pro.XianchangTotal;
                    //    excel.Cells[row, 3] = pro.StocksQuantity;
                    //    //excel.Cells[row, 3] = pro.XianchangTotal;
                    //    excel.Cells[row, 4] = pro.XianchangYanpian;
                    //    excel.Cells[row, 5] = pro.XianchangZuzhuang;
                    //    excel.Cells[row, 6] = pro.TotalQty;
                    //    excel.Cells[row, 7] = pro.HandbookId;
                    //    excel.Cells[row, 8] = pro.CnName;
                    //    excel.Cells[row, 9] = (pro.NetWeight.Value / 1000).ToString("0.#####");

                    //    col = 10;
                    //    foreach (var dic in pro.MaterialDic)
                    //    {
                    //        excel.Cells[row, col++] = dic.Value;
                    //    }

                    //    row++;
                    //}

                    //row++;

                    setExcelValue(item);
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
            excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 8 + 1 + listProduct[0].MaterialDic.Keys.Count]).Interior.Color = "255";    //红色
            excel.get_Range(excel.Cells[row, 3], excel.Cells[row, 3]).Formula = string.Format("=SUM(C{0}:C{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 4], excel.Cells[row, 4]).Formula = string.Format("=SUM(D{0}:D{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 5], excel.Cells[row, 5]).Formula = string.Format("=SUM(E{0}:E{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 6], excel.Cells[row, 6]).Formula = string.Format("=SUM(F{0}:F{1})", row + 1, row + item.Count());

            col = 10;
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
            #region 旧版
            //IList<string> str = materialManager.SelectMaterialCategory();
            //Dictionary<string, string> dic = new Dictionary<string, string>();


            //foreach (var pro in listProduct)
            //{
            //    pro.MaterialDic = new Dictionary<string, string>();
            //    foreach (var item in str)
            //    {
            //        pro.MaterialDic.Add(item, "0");
            //    }


            //    if (!string.IsNullOrEmpty(pro.MaterialIds))
            //    {
            //        string[] materialIds = pro.MaterialIds.Split(',');
            //        string[] materialnums = pro.MaterialNum.Split(',');

            //        for (int i = 0; i < materialIds.Length; i++)
            //        {
            //            Model.Material model = materialManager.Get(materialIds[i]);
            //            if (model != null)
            //            {
            //                double value = Convert.ToDouble(materialnums[i]) * Convert.ToDouble(model.JWeight) * pro.TotalQty;

            //                if (!pro.MaterialDic.Keys.Contains(model.MaterialCategoryName))
            //                {
            //                    if (!pro.MaterialDic.Keys.Contains(model.MaterialCategoryName.ToLower()))
            //                        model.MaterialCategoryName = model.MaterialCategoryName.ToUpper();
            //                    else
            //                        model.MaterialCategoryName = model.MaterialCategoryName.ToLower();
            //                }
            //                pro.MaterialDic[model.MaterialCategoryName] = (Convert.ToDouble(pro.MaterialDic[model.MaterialCategoryName]) + (value / 1000)).ToString("0.####");
            //            }
            //        }
            //    }
            //} 
            #endregion


            //没有用CommonHelp.ConvertMaterial,是因为此处要乘以商品总量pro.TotalQty(包括线上的)
            //新版，只查用到的原料种类，且只查一次数据库-原料，缓存下来
            string needMaterialIds = "(";
            foreach (var item in listProduct)
            {
                if (!string.IsNullOrEmpty(item.MaterialIds))
                {
                    string[] materialIds = item.MaterialIds.Split(',');
                    for (int i = 0; i < materialIds.Length; i++)
                    {
                        needMaterialIds += "'" + materialIds[i] + "',";
                    }
                }
            }
            needMaterialIds = needMaterialIds.TrimEnd(',') + ")";
            if (needMaterialIds.Length < 5)  //所有商品没有设置净重
                return;

            //根据上面获取的 原料主键Ids 查询所有原料
            IList<Model.Material> listMaterial = materialManager.SelectAllByPrimaryIds(needMaterialIds);
            //分組得到原料分類
            List<string> materialCategory = listMaterial.Select(m => m.MaterialCategoryName).Distinct().OrderBy(o => o).ToList();

            foreach (var pro in listProduct)
            {
                pro.MaterialDic = new Dictionary<string, string>();
                foreach (var item in materialCategory)
                {
                    pro.MaterialDic.Add(item, "0");
                }

                if (!string.IsNullOrEmpty(pro.MaterialIds))
                {
                    string[] materialIds = pro.MaterialIds.Split(',');
                    string[] materialnums = pro.MaterialNum.Split(',');

                    for (int i = 0; i < materialIds.Length; i++)
                    {
                        Model.Material model = listMaterial.FirstOrDefault(m => m.MaterialId == materialIds[i]);
                        if (model != null)
                        {
                            double value = Convert.ToDouble(materialnums[i]) * Convert.ToDouble(model.JWeight) * pro.TotalQty;

                            if (!pro.MaterialDic.Keys.Contains(model.MaterialCategoryName))
                            {
                                if (!pro.MaterialDic.Keys.Contains(model.MaterialCategoryName.ToLower()))
                                    model.MaterialCategoryName = model.MaterialCategoryName.ToUpper();
                                else
                                    model.MaterialCategoryName = model.MaterialCategoryName.ToLower();
                            }
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.date_End.EditValue == null)
            {
                MessageBox.Show("请先选择查询日期", "提示", MessageBoxButtons.OK);
                return;
            }

            DateTime dateEnd = this.date_End.DateTime.Date.AddDays(1);
            listProduct = productManager.SelectIdAndStock(this.lue_ProductCategory.EditValue == null ? null : this.lue_ProductCategory.EditValue.ToString(), this.lue_ProductCategoryEnd.EditValue == null ? null : this.lue_ProductCategoryEnd.EditValue.ToString());

            IList<Model.StockSeach> stockList = new List<Model.StockSeach>();

            string workHouseYanpian = workHouseManager.SelectWorkHouseIdByName("验片");
            string workHouseZuzhuang = workHouseManager.SelectWorkHouseIdByName("组装现场仓");
            string workHouseChengpinZuzhuang = workHouseManager.SelectWorkHouseIdByName("成品组装");

            foreach (Model.Product item in listProduct)
            {
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


                #region 现场数量  这边改动，其他两个地方也要对应修改(1,Book.UI.Query.SceneStock   2,Book.UI.Settings.StockLimitations.AssemblySiteDifferenceForm)

                #region 验片：合计前单位转入 - 合计生产数量（包含合计合格数量，合计不良品）
                //查询商品对应的未结案加工单
                IList<Model.PronoteHeader> phList = pronoteHeaderManager.SelectByProductIdAll(item.ProductId);
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
                }
                #endregion



                #region 组装现场:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）                         2018年5月22日21:48:44,这边修改的时候对应到“组装现场盘点差异”也要修改

                //2018年2月22日13:18:54： 组装现场:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）- 生产退料（从组装现场退的）
                //领到 组装现场 部门的数量
                double materialQty = 0;
                //if (!string.IsNullOrEmpty(invoiceXOIds))
                //    materialQty = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, invoiceXOIds);
                //2018年5月17日00:34:42 只要是未结案的订单领到组装现场的都计入
                materialQty = produceMaterialdetailsManager.SelectMaterialQtyAll(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang);

                //计算所有转入 组装现场 部门的数量
                //Model.ProduceInDepotDetail pidZuzhuangIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId,  dateEnd.AddSeconds(-1), workHouseZuzhuang, null);   //转入组装现场时没有加工单
                //double zuzhuangTransferIn = Convert.ToDouble(pidZuzhuangIn.ProduceTransferQuantity);
                double zuzhuangTransferIn = 0;
                double zuzhuangTransferOut = 0;
                double exitQty = 0;
                double deductionQty = 0;

                if (phList != null && phList.Count > 0)
                {
                    IList<Model.ProduceInDepotDetail> pidZuzhuangIn = produceInDepotDetailManager.SelectTransZuZhuangXianChang(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                    zuzhuangTransferIn = pidZuzhuangIn.Sum(P => P.ProduceTransferQuantity).Value;
                    //string xoIDs = "";
                    //foreach (string xoid in pidZuzhuangIn.Select(D => D.InvoiceXOId).Distinct())
                    //{
                    //    xoIDs += "'" + xoid + "',";
                    //}
                    //xoIDs = xoIDs.TrimEnd(',');

                    //计算 组装现场 部门转入其他部门的数量
                    Model.ProduceInDepotDetail pidZuzhuangOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                    zuzhuangTransferOut = Convert.ToDouble(pidZuzhuangOut.ProduceTransferQuantity);

                    //计算 从组装现场退回的 生产退料
                    exitQty = produceMaterialExitDetailManager.SelectSumQtyFromZuzhuangAll(item.ProductId, dateEnd.AddSeconds(-1), workHouseZuzhuang);


                    #region 查询商品对应的所有母件 入库 扣减
                    //if (!string.IsNullOrEmpty(xoIDs))
                    if (!string.IsNullOrEmpty(invoiceXOIds))
                    {
                        Dictionary<string, double> parentProductDic = new Dictionary<string, double>();

                        GetParentProductInfo("'" + item.ProductId + "'", parentProductDic);

                        string proIds = "";
                        foreach (var str in parentProductDic.Keys)
                        {
                            proIds += "'" + str + "',";
                        }
                        proIds = proIds.TrimEnd(',');

                        if (!string.IsNullOrEmpty(proIds))
                        {
                            IList<Model.ProduceInDepotDetail> pids = produceInDepotDetailManager.SelectIndepotQty(proIds, dateEnd.AddSeconds(-1), workHouseChengpinZuzhuang, invoiceXOIds);
                            //IList<Model.ProduceInDepotDetail> pids = produceInDepotDetailManager.SelectIndepotQty(proIds, dateEnd.AddSeconds(-1), workHouseChengpinZuzhuang, xoIDs); //对应转到组装现场的生产入库单的客户订单，如果订单不在范围内，母件入库不扣减
                            foreach (var pid in pids)
                            {
                                deductionQty += Convert.ToDouble(pid.ProduceQuantity) * parentProductDic[pid.ProductId];
                            }
                        }
                    }

                    #endregion
                }

                //double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty;
                double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty - exitQty;
                zuzhuangXianchang = zuzhuangXianchang < 0 ? 0 : zuzhuangXianchang;

                item.XianchangZuzhuang = zuzhuangXianchang;

                #endregion

                #endregion
            }

            this.bindingSourceProduct.DataSource = listProduct;
        }
    }
}