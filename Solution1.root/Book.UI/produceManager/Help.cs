using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.produceManager
{
    public class Help
    {
        BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();
        BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        BL.InvoiceXODetailManager invoiceXODetailManager = new Book.BL.InvoiceXODetailManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();

        /// <summary>
        /// 获取该商品对应的母件的商品型号(如果同一笔订单里面多个母件都引用该子件，计算物料需求时该子件用量将合并为一笔，因此要带出所有母件的型号)
        /// </summary>
        /// <param name="pronoteHeaderId">加工单号</param>
        /// <param name="productId">商品编号</param>
        /// <returns></returns>
        internal string GetCustomerProductNameByPronoteHeaderId(string pronoteHeaderId, string productId)
        {
            List<string> invoiceProductIds = invoiceXODetailManager.SelectProductIDs(pronoteHeaderId).ToList();
            List<string> parentProductIds = new List<string>();
            GetParentProductInfo("'" + productId + "'", parentProductIds);
            IEnumerable<string> productIds = invoiceProductIds.Intersect(parentProductIds);

            string pIds = "";
            foreach (var p in productIds)
            {
                pIds += "'" + p + "',";
            }
            pIds = pIds.TrimEnd(',');

            if (string.IsNullOrEmpty(pIds))
                return null;
            return productManager.SelectCustomerProductNameByProductIds(pIds).TrimEnd(',');
        }

        private void GetParentProductInfo(string productId, List<string> parentProductIds)
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
    }
}
