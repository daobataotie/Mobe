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
    public partial class ImmediateStock : DevExpress.XtraEditors.XtraForm
    {
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.StockManager stockManager = new Book.BL.StockManager();
        BL.ProduceInDepotDetailManager produceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.ProduceMaterialdetailsManager produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();

        public ImmediateStock()
        {
            InitializeComponent();

            this.bindingSourceProductCategory.DataSource = new BL.ProductCategoryManager().Select();
        }

        private void barExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show("请先选择日期", "提示", MessageBoxButtons.OK);
                return;
            }

            DateTime dateTime = this.dateEdit1.DateTime.Date.AddDays(1);
            IList<Model.Product> listPro = productManager.SelectIdAndStock(this.lue_ProductCategory.EditValue == null ? null : this.lue_ProductCategory.EditValue.ToString());

            IList<Model.StockSeach> stockList = new List<Model.StockSeach>();
            IList<Model.ProduceInDepotDetail> inDepotList = new List<Model.ProduceInDepotDetail>();

            foreach (Model.Product item in listPro)
            {
                #region 仓库数量
                stockList = this.stockManager.SelectJiShi(item.ProductId, dateTime, DateTime.Now);

                //如果有 盘点单,盘点算入(ex:由100→200，则算作入库(200-100=100))
                if (stockList != null && stockList.Count > 0)
                {
                    //0 出，1 入，3 盘点     2 调拨，库存不变
                    var panQty = stockList.Where(I => I.InvoiceTypeIndex == 3).Sum(S => S.InvoiceQuantity - S.StockCheckBookQuantity);
                    var outQty = stockList.Where(I => I.InvoiceTypeIndex == 0).Sum(S => S.InvoiceQuantity); //出库数量
                    var inQty = stockList.Where(I => I.InvoiceTypeIndex == 1).Sum(S => S.InvoiceQuantity);  //入库数量

                    item.StocksQuantity = item.StocksQuantity + outQty - inQty - panQty;
                }
                #endregion


                #region 现场数量

                #region 验片：合计前单位转入 - 合计生产数量（包含合计合格数量，合计不良品）
                //查询商品对应的未结案加工单
                IList<Model.PronoteHeader> phList = pronoteHeaderManager.SelectNotClosed(item.ProductId);
                string pronoteHeaderIds = "'";
                string invoiceXOIds = "'";
                foreach (var item in phList)
                {
                    pronoteHeaderIds += item.PronoteHeaderID + "',";
                    invoiceXOIds += item.InvoiceXOId + "',";
                }
                pronoteHeaderIds.TrimEnd(',');
                invoiceXOIds.TrimEnd(',');

                //计算所有转入 验片 部门的数量
                Model.ProduceInDepotDetail pidYanpianIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId, dateTime.AddSeconds(-1), "yanpian", pronoteHeaderIds);
                double yanpianTransferIn = pidYanpianIn.ProduceTransferQuantity;

                //计算 验片 部门的生产数量
                Model.ProduceInDepotDetail pidYanpianOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateTime.AddSeconds(-1), "yanpian", pronoteHeaderIds);
                double yanpianProcedures = pidYanpianOut.ProceduresSum; 

                //yanpianTransferIn-yanpianProcedures
                #endregion



                #region 组装:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）
                //领到 组装 部门的数量
                double materialQty = produceMaterialdetailsManager.SelectMaterialQty(item.ProductId, dateTime.AddSeconds(-1), "zuzhuang", invoiceXOIds);
                //计算所有转入 组装 部门的数量
                Model.ProduceInDepotDetail pidZuzhuangIn = produceInDepotDetailManager.SelectByNextWorkhouse(item.ProductId, dateTime.AddSeconds(-1), "zuzhuang", pronoteHeaderIds);
                double zuzhuangTransferIn = pidZuzhuangIn.ProduceTransferQuantity;

                //计算 组装 部门转入其他部门的数量
                Model.ProduceInDepotDetail pidZuzhuangOut = produceInDepotDetailManager.SelectByThisWorkhouse(item.ProductId, dateTime.AddSeconds(-1), "zuzhuang", pronoteHeaderIds);
                double zuzhuangTransferOut = pidZuzhuangOut.ProduceTransferQuantity;

                //zuzhuangTransferIn+materialQty-zuzhuangTransferOut


                //double zuzhuangBeforeQty = pidZuzhuang.HeJiBeforeTransferQuantity;
                //double zuzhuangInQty = pidZuzhuang.ProduceQuantity;
                //double zuzhuangproduceQty = pidZuzhuang.ProceduresSum;
                //double zuzhuangCheckQty = pidZuzhuang.CheckOutSum;
                //double zuzhuangSceneQty = pidZuzhuang.SceneQty;
                //double zuzhuangAdverseQty = pidZuzhuang.AdverseQty;
                
                #endregion
                #endregion
            }

        }
    }
}