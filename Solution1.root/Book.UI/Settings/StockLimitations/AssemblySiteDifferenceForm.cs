using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Settings.StockLimitations
{
    public partial class AssemblySiteDifferenceForm : Settings.BasicData.BaseEditForm
    {
        private Model.AssemblySiteDifference _assemblySiteDifference;
        private BL.AssemblySiteDifferenceManager manager = new Book.BL.AssemblySiteDifferenceManager();
        private BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        private BL.ProduceMaterialdetailsManager produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();
        private BL.ProduceInDepotDetailManager produceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();
        private BL.ProduceMaterialExitDetailManager produceMaterialExitDetailManager = new Book.BL.ProduceMaterialExitDetailManager();
        private BL.WorkHouseManager workHouseManager = new Book.BL.WorkHouseManager();
        private BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();
        private BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        int isLast = 0;
        string workHouseZuzhuang = null;
        string workHouseChengpinZuzhuang = null;


        public AssemblySiteDifferenceForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.AssemblySiteDifference.PRO_AssemblySiteInventoryId, new AA("请选择一笔组装现场盘点录入单", this.txt_ID2));

            workHouseZuzhuang = workHouseManager.SelectWorkHouseIdByName("组装现场仓");
            workHouseChengpinZuzhuang = workHouseManager.SelectWorkHouseIdByName("成品组装");

            this.ncc_Employee.Choose = new BasicData.Employees.ChooseEmployee();
            this.action = "view";
        }

        public AssemblySiteDifferenceForm(Model.AssemblySiteDifference model)
            : this()
        {
            this._assemblySiteDifference = model;
            this.isLast = 1;
        }

        public AssemblySiteDifferenceForm(Model.AssemblySiteDifference model, string action)
            : this()
        {
            this._assemblySiteDifference = model;
            this.isLast = 1;
            this.action = action;
        }

        public AssemblySiteDifferenceForm(Model.AssemblySiteInventory model)
            : this()
        {
            this._assemblySiteDifference = new Book.Model.AssemblySiteDifference();
            this._assemblySiteDifference.AssemblySiteDifferenceId = this.manager.GetId();
            this._assemblySiteDifference.Employee = BL.V.ActiveOperator.Employee;
            this._assemblySiteDifference.AssemblySiteInventoryId = model.AssemblySiteInventoryId;
            this._assemblySiteDifference.InvoiceDate = model.InvoiceDate;

            Model.AssemblySiteDifferenceDetai detail;
            foreach (var item in model.Details)
            {
                detail = new Book.Model.AssemblySiteDifferenceDetai();
                detail.AssemblySiteDifferenceDetaiId = Guid.NewGuid().ToString();
                detail.Product = item.Product;
                detail.ProductId = item.ProductId;
                detail.ActualQuantity = item.Quantity;

                detail.TheoryQuantity = this.CountSiteQuantity(detail.ProductId, model.InvoiceDate.Value.Date.AddDays(1));

                this._assemblySiteDifference.Details.Add(detail);
            }

            this.action = "insert";
            this.isLast = 1;
        }

        protected override void AddNew()
        {
            if (this.isLast == 1)
            {
                this.isLast = 0;
                return;
            }
            this._assemblySiteDifference = new Book.Model.AssemblySiteDifference();
            this._assemblySiteDifference.AssemblySiteDifferenceId = this.manager.GetId();
            this._assemblySiteDifference.Employee = BL.V.ActiveOperator.Employee;
            this.action = "insert";
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._assemblySiteDifference.EmployeeId = (this.ncc_Employee.EditValue == null ? null : (this.ncc_Employee.EditValue as Model.Employee).EmployeeId);
            this._assemblySiteDifference.Note = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(_assemblySiteDifference);
                    break;
                case "update":
                    this.manager.Update(_assemblySiteDifference);
                    break;
            }
        }

        public override void Refresh()
        {
            if (_assemblySiteDifference == null)
                this.AddNew();
            else if (this.action == "view")
            {
                this._assemblySiteDifference = this.manager.GetDetail(this._assemblySiteDifference.AssemblySiteDifferenceId);
            }

            this.txt_ID.EditValue = this._assemblySiteDifference.AssemblySiteDifferenceId;
            this.txt_ID2.EditValue = this._assemblySiteDifference.AssemblySiteInventoryId;
            this.date_Difference.EditValue = this._assemblySiteDifference.InvoiceDate;
            this.ncc_Employee.EditValue = this._assemblySiteDifference.Employee;
            this.txt_Note.EditValue = this._assemblySiteDifference.Note;


            this.bindingSourceDetail.DataSource = _assemblySiteDifference.Details;
            this.gridControl1.RefreshDataSource();
            base.Refresh();

            switch (this.action)
            {
                case "insert":
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }

            this.txt_ID.Properties.ReadOnly = true;
            this.txt_ID2.Properties.ReadOnly = true;
            this.date_Difference.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.AssemblySiteDifference model = this.manager.GetNext(_assemblySiteDifference);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _assemblySiteDifference = model;
        }

        protected override void MovePrev()
        {
            Model.AssemblySiteDifference model = this.manager.GetPrev(_assemblySiteDifference);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _assemblySiteDifference = model;
        }

        protected override void MoveFirst()
        {
            _assemblySiteDifference = this.manager.Get(this.manager.GetFirst() == null ? "" : this.manager.GetFirst().AssemblySiteDifferenceId);
        }

        protected override void MoveLast()
        {
            if (this.isLast == 1)
            {
                this.isLast = 0;
                return;
            }
            _assemblySiteDifference = this.manager.Get(this.manager.GetLast() == null ? "" : this.manager.GetLast().AssemblySiteDifferenceId);
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(_assemblySiteDifference);
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(_assemblySiteDifference);
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.manager.Delete(_assemblySiteDifference.AssemblySiteDifferenceId);
                _assemblySiteDifference = this.manager.GetNext(_assemblySiteDifference);
                if (_assemblySiteDifference == null)
                {
                    _assemblySiteDifference = this.manager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new AssemblySiteDifferenceRO(this._assemblySiteDifference);
        }

        private void bar_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssemblySiteDifferenceList f = new AssemblySiteDifferenceList();
            f.ShowDialog();
        }

        private void bar_ChooseInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssemblySiteInventoryList f = new AssemblySiteInventoryList(1);
            if (f.ShowDialog(this) == DialogResult.OK && f.SelectItem != null)
            {
                this.txt_ID2.EditValue = this._assemblySiteDifference.AssemblySiteInventoryId = f.SelectItem.AssemblySiteInventoryId;
                this.date_Difference.EditValue = this._assemblySiteDifference.InvoiceDate = f.SelectItem.InvoiceDate;

                Model.AssemblySiteDifferenceDetai detail;
                this._assemblySiteDifference.Details.Clear();
                foreach (var item in f.SelectItem.Details)
                {
                    detail = new Book.Model.AssemblySiteDifferenceDetai();
                    detail.AssemblySiteDifferenceDetaiId = Guid.NewGuid().ToString();
                    detail.Product = item.Product;
                    detail.ProductId = item.ProductId;
                    detail.ActualQuantity = item.Quantity;

                    detail.TheoryQuantity = this.CountSiteQuantity(detail.ProductId, f.SelectItem.InvoiceDate.Value.Date.AddDays(1));

                    this._assemblySiteDifference.Details.Add(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private decimal CountSiteQuantity(string productId, DateTime dateEnd)
        {
            IList<Model.PronoteHeader> phList = pronoteHeaderManager.SelectByProductId(productId);
            if (phList == null || phList.Count == 0)
                return 0;
            string pronoteHeaderIds = "";
            string invoiceXOIds = "";
            foreach (var ph in phList)
            {
                pronoteHeaderIds += "'" + ph.PronoteHeaderID + "',";
                invoiceXOIds += "'" + ph.InvoiceXOId + "',";
            }
            pronoteHeaderIds = pronoteHeaderIds.TrimEnd(',');
            invoiceXOIds = invoiceXOIds.TrimEnd(',');

            #region 组装现场:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）- 生产退料（从组装现场退的）
            //领到 组装现场 部门的数量
            double materialQty = 0;

            materialQty = produceMaterialdetailsManager.SelectMaterialQty(productId, dateEnd.AddSeconds(-1), workHouseZuzhuang);

            IList<Model.ProduceInDepotDetail> pidZuzhuangIn = produceInDepotDetailManager.SelectTransZuZhuangXianChang(productId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
            double zuzhuangTransferIn = pidZuzhuangIn.Sum(P => P.ProduceTransferQuantity).Value;
            //string xoIDs = "";
            //foreach (string xoid in pidZuzhuangIn.Select(D => D.InvoiceXOId).Distinct())
            //{
            //    xoIDs += "'" + xoid + "',";
            //}
            //xoIDs = xoIDs.TrimEnd(',');

            //计算 组装现场 部门转入其他部门的数量
            Model.ProduceInDepotDetail pidZuzhuangOut = produceInDepotDetailManager.SelectByThisWorkhouse(productId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
            double zuzhuangTransferOut = Convert.ToDouble(pidZuzhuangOut.ProduceTransferQuantity);

            //计算 从组装现场退回的 生产退料
            double exitQty = produceMaterialExitDetailManager.SelectSumQtyFromZuzhuang(productId, dateEnd.AddSeconds(-1), workHouseZuzhuang);


            #region 查询商品对应的所有母件 入库 扣减
            double deductionQty = 0;
            //if (!string.IsNullOrEmpty(xoIDs))
            if (!string.IsNullOrEmpty(invoiceXOIds))
            {
                Dictionary<string, double> parentProductDic = new Dictionary<string, double>();

                GetParentProductInfo("'" + productId + "'", parentProductDic);

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

            //double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty;
            double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty - exitQty;
            zuzhuangXianchang = zuzhuangXianchang < 0 ? 0 : zuzhuangXianchang;

            #endregion

            return (decimal)zuzhuangXianchang;
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

            #region 新版，一个子件没母件引用N次，叠加计算
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
    }
}