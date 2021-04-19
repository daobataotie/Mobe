using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI
{
    /// <summary>
    /// 通用帮助类
    /// 1，商品无型号，可获取其母件型号
    /// 2，获取商品原料信息
    /// </summary>
    public class CommonHelp
    {
        static BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();
        static BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        static BL.InvoiceXODetailManager invoiceXODetailManager = new Book.BL.InvoiceXODetailManager();
        static BL.ProductManager productManager = new Book.BL.ProductManager();
        static BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        static BL.MRSHeaderManager mRSHeaderManager = new Book.BL.MRSHeaderManager();

        static BL.MaterialManager materialManager = new Book.BL.MaterialManager();

        internal static string GetCustomerProductNameByPronoteHeaderId(string pronoteHeaderId, string productId)
        {
            Model.PronoteHeader ph = pronoteHeaderManager.Get(pronoteHeaderId);
            if (ph != null)
            {
                return GetCustomerProductNameByPronoteHeaderId(ph, productId, ph.HandbookProductId);
            }
            return "";
        }


        #region 旧版，同一个订单中有不同的母件用到该子件，但是是分开排单的，客户型号却全部计算了，应该只计算对应排单的母件

        ///// <summary>
        ///// 获取该商品对应的母件的商品型号(如果同一笔订单里面多个母件都引用该子件，计算物料需求时该子件用量将合并为一笔，因此要带出所有母件的型号)
        ///// </summary>
        ///// <param name="pronoteHeaderId">加工单号</param>
        ///// <param name="productId">商品编号</param>
        ///// <returns></returns>
        //internal string GetCustomerProductNameByPronoteHeaderId(string pronoteHeaderId, string productId, string handbookProductId)
        //{
        //    List<string> invoiceProductIds = invoiceXODetailManager.SelectProductIDs(pronoteHeaderId, handbookProductId).ToList();

        //    List<string> parentProductIds = new List<string>();
        //    GetParentProductInfo("'" + productId + "'", parentProductIds);
        //    IEnumerable<string> productIds = invoiceProductIds.Intersect(parentProductIds);

        //    string pIds = "";
        //    foreach (var p in productIds)
        //    {
        //        pIds += "'" + p + "',";
        //    }
        //    pIds = pIds.TrimEnd(',');

        //    if (string.IsNullOrEmpty(pIds))   //有些商品是通过 + 上去的，没有对应订单里面的商品
        //        return null;
        //    return productManager.SelectCustomerProductNameByProductIds(pIds).TrimEnd(',');
        //} 
        #endregion

        /// <summary>
        /// 获取该商品对应的母件的商品型号(如果同一笔订单里面多个母件都引用该子件，计算物料需求时该子件用量将合并为一笔，因此要带出所有母件的型号)
        /// </summary>
        /// <param name="pronoteHeaderId">加工单</param>
        /// <param name="productId">商品编号</param>
        /// <param name="handbookProductId">手册项号</param>
        /// <returns></returns>
        internal static string GetCustomerProductNameByPronoteHeaderId(Model.PronoteHeader pronoteHeader, string productId, string handbookProductId)
        {
            List<string> invoiceProductIds = mRSHeaderManager.SelectAllProductIdByMRSHeaderId(pronoteHeader.MRSHeaderId, handbookProductId).ToList();

            List<string> parentProductIds = new List<string>();
            GetParentProductInfo("'" + productId + "'", parentProductIds);
            IEnumerable<string> productIds = invoiceProductIds.Intersect(parentProductIds);

            string pIds = "";
            foreach (var p in productIds)
            {
                pIds += "'" + p + "',";
            }
            pIds = pIds.TrimEnd(',');

            if (string.IsNullOrEmpty(pIds))   //有些商品是通过 + 上去的，没有对应订单里面的商品
                return null;
            return productManager.SelectCustomerProductNameByProductIds(pIds).TrimEnd(',');
        }

        /// <summary>
        /// 获取该商品对应的母件的商品型号(如果同一笔订单里面多个母件都引用该子件，计算物料需求时该子件用量将合并为一笔，因此要带出所有母件的型号)
        /// 适用于没有加工单号，可能存在情况：一笔客户订单对应多笔生产计划，可能导致多计算 非本商品对应的生产计划的母件
        /// </summary>
        /// <param name="invoiceXOId">客户订单号</param>
        /// <param name="productId">商品编号</param>
        /// <returns></returns>
        internal static string GetCustomerProductNameByInvoiceXOId(string invoiceXOId, string productId)
        {
            List<string> invoiceProductIds = mRSHeaderManager.SelectAllProductIdByInvoiceXOId(invoiceXOId).ToList();

            List<string> parentProductIds = new List<string>();
            GetParentProductInfo("'" + productId + "'", parentProductIds);
            IEnumerable<string> productIds = invoiceProductIds.Intersect(parentProductIds);

            string pIds = "";
            foreach (var p in productIds)
            {
                pIds += "'" + p + "',";
            }
            pIds = pIds.TrimEnd(',');

            if (string.IsNullOrEmpty(pIds))   //有些商品是通过 + 上去的，没有对应订单里面的商品
                return null;
            return productManager.SelectCustomerProductNameByProductIds(pIds).TrimEnd(',');
        }

        private static void GetParentProductInfo(string productId, List<string> parentProductIds)
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

            foreach (var comInfo in bomComponentList)
            {
                Model.BomParentPartInfo parent = bomParentList.First(P => P.BomId == comInfo.BomId);
                productIds += "'" + parent.ProductId + "',";

                if (!parentProductIds.Contains(parent.ProductId))
                {
                    parentProductIds.Add(parent.ProductId);
                }
            }

            productIds = productIds.TrimEnd(',');

            GetParentProductInfo(productIds, parentProductIds);   //递归调用
        }


        //换算原料净重
        internal static void ConvertMaterial(List<Model.Product> list)
        {
            #region 老版，1，会查出所有原料种类；2，每个商品循环查数据库-原料
            ////查询出所有原料种类
            //IList<string> str = materialManager.SelectMaterialCategory();
            //Dictionary<string, string> dic = new Dictionary<string, string>();

            //foreach (var pro in list)
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
            //                double value = Convert.ToDouble(materialnums[i]) * Convert.ToDouble(model.JWeight) * Convert.ToDouble(pro.StocksQuantity);

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

            //新版，只查用到的原料种类，且只查一次数据库-原料，缓存下来
            string needMaterialIds = "(";
            foreach (var item in list)
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

            foreach (var pro in list)
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
                            double value = Convert.ToDouble(materialnums[i]) * Convert.ToDouble(model.JWeight) * Convert.ToDouble(pro.StocksQuantity);

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
    }
}
