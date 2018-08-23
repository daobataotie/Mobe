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
        BL.MaterialManager materialManager = new Book.BL.MaterialManager();
        int isLast = 0;
        string workHouseYanpian = null;
        string workHouseZuzhuang = null;
        string workHouseChengpinZuzhuang = null;


        public AssemblySiteDifferenceForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.AssemblySiteDifference.PRO_AssemblySiteInventoryId, new AA("请选择一笔组装现场盘点录入单", this.txt_ID2));
            workHouseYanpian = workHouseManager.SelectWorkHouseIdByName("验片");
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
            //  2018年7月3日22:17:36 改：只查询2018.1.1 之后的订单
            DateTime startDate = new DateTime(2018, 1, 1);
            IList<Model.PronoteHeader> phList = pronoteHeaderManager.SelectByProductId(startDate, dateEnd.AddSeconds(-1), productId);
            //if (phList == null || phList.Count == 0)
            //    return 0;
            string pronoteHeaderIds = "";
            string invoiceXOIds = "";
            string allInvoiceXOIds = "";

            #region 验片：合计前单位转入 - 合计生产数量（包含合计合格数量，合计不良品）
            double yanpianTransferIn = 0;
            double yanpianProcedures = 0;
            double yanpianBuliang = 0;
            double yanpianXianchang = 0;

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
                Model.ProduceInDepotDetail pidYanpianIn = produceInDepotDetailManager.SelectByNextWorkhouse(productId, dateEnd.AddSeconds(-1), workHouseYanpian, pronoteHeaderIds);
                yanpianTransferIn = Convert.ToDouble(pidYanpianIn.ProduceTransferQuantity);

                //计算 验片 部门的生产数量
                Model.ProduceInDepotDetail pidYanpianOut = produceInDepotDetailManager.SelectByThisWorkhouse(productId, dateEnd.AddSeconds(-1), workHouseYanpian, pronoteHeaderIds);
                yanpianProcedures = Convert.ToDouble(pidYanpianOut.ProceduresSum);
                yanpianBuliang = Convert.ToDouble(pidYanpianOut.ProceduresSum - pidYanpianOut.CheckOutSum);

                yanpianXianchang = yanpianTransferIn - yanpianProcedures;
                yanpianXianchang = yanpianXianchang < 0 ? 0 : yanpianXianchang;
            }
            #endregion

            #region 组装现场:合计前单位转入+ 合计领料单领出 - 合计出库数量（合计转生产到其他部门，成品入库数量换算后扣减数量）- 生产退料（从组装现场退的）

            double zuzhuangTransferIn = 0;
            double zuzhuangTransferOut = 0;
            double exitQty = 0;
            double deductionQty = 0;
            //领到 组装现场 部门的数量
            double materialQty = 0;

            //materialQty = produceMaterialdetailsManager.SelectMaterialQty(productId, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang);
            //2018年7月9日23:07:51 领料单所包含的未结案订单号码拉出来，用于查询母件入库扣减
            System.Data.DataTable dt = produceMaterialdetailsManager.SelectMaterialQty(productId, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, invoiceXOIds);
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

                GetParentProductInfo("'" + productId + "'", parentProductDic);

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
                                deductionQty -= pmd.Materialprocessum.HasValue ? pmd.Materialprocessum.Value * parentProductDic[pmd.ProductId] * fatherDic[pids.First(P => fatherDic.Keys.Contains(P.ProductId)).ProductId] : 0;
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
            exitQty += produceMaterialExitDetailManager.SelectSumQtyFromZuzhuang(productId, startDate, dateEnd.AddSeconds(-1), workHouseZuzhuang, allInvoiceXOIds);


            #region 计算所有转入 组装现场 部门的数量
            if (phList != null && phList.Count > 0)
            {
                IList<Model.ProduceInDepotDetail> pidZuzhuangIn = produceInDepotDetailManager.SelectTransZuZhuangXianChang(productId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                zuzhuangTransferIn = pidZuzhuangIn.Sum(P => P.ProduceTransferQuantity).Value;

                //计算 组装现场 部门转入其他部门的数量
                Model.ProduceInDepotDetail pidZuzhuangOut = produceInDepotDetailManager.SelectByThisWorkhouse(productId, dateEnd.AddSeconds(-1), workHouseZuzhuang, pronoteHeaderIds);
                zuzhuangTransferOut = Convert.ToDouble(pidZuzhuangOut.ProduceTransferQuantity);
            } 
            #endregion

            //double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty;
            double zuzhuangXianchang = zuzhuangTransferIn + materialQty - zuzhuangTransferOut - deductionQty - exitQty;
            zuzhuangXianchang = zuzhuangXianchang < 0 ? 0 : zuzhuangXianchang;

            #endregion

            return (decimal)zuzhuangXianchang + (decimal)yanpianXianchang;
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

        private void bar_ExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.action != "view" || this._assemblySiteDifference.Details == null || this._assemblySiteDifference.Details.Count < 1)
                return;

            try
            {
                Type objClassType = null;
                objClassType = Type.GetTypeFromProgID("Excel.Application");
                if (objClassType == null)
                {
                    MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                    return;
                }

                ConvertMaterial();   //计算原料净重

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 7]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 10;
                excel.Cells[1, 1] = "组装现场盘点差异(" + this.date_Difference.DateTime.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 7]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "商品编号";
                excel.Cells[2, 2] = "商品名称";
                excel.Cells[2, 3] = "客户型号";
                excel.Cells[2, 4] = "版本";
                excel.Cells[2, 5] = "盘点数量";
                excel.Cells[2, 6] = "理论数量";
                excel.Cells[2, 7] = "差异";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 7 + 1 + this._assemblySiteDifference.Details[0].Product.MaterialDic.Keys.Count]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).ColumnWidth = 25;
                excel.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).ColumnWidth = 50;

                int col = 9;
                //原料
                foreach (var item in this._assemblySiteDifference.Details[0].Product.MaterialDic)
                {
                    excel.Cells[2, col++] = item.Key;
                }

                List<Model.AssemblySiteDifferenceDetai> haveThreeCategoryPro = this._assemblySiteDifference.Details.Where(P => P.Product.ProductCategory3 != null).ToList();
                List<Model.AssemblySiteDifferenceDetai> haveTwoCategoryPro = this._assemblySiteDifference.Details.Where(P => P.Product.ProductCategory2 != null && P.Product.ProductCategory3 == null).ToList();
                List<Model.AssemblySiteDifferenceDetai> haveOneCategoryPro = this._assemblySiteDifference.Details.Where(P => P.Product.ProductCategory2 == null && P.Product.ProductCategory3 == null).ToList();

                int row = 3;

                foreach (var item in haveThreeCategoryPro.GroupBy(P => P.Product.ProductCategory3.ProductCategoryName))
                {
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.Product.Id;
                        excel.Cells[row, 2] = pro.Product.ProductName;
                        excel.Cells[row, 3] = pro.Product.CustomerProductName;
                        excel.Cells[row, 4] = pro.Product.ProductVersion;
                        excel.Cells[row, 5] = pro.ActualQuantity;
                        excel.Cells[row, 6] = pro.TheoryQuantity;
                        excel.Cells[row, 7] = pro.DiffQty;

                        col = 9;
                        foreach (var dic in pro.Product.MaterialDic)
                        {
                            excel.Cells[row, col++] = dic.Value;
                        }

                        row++;
                    }
                    row++;
                }

                foreach (var item in haveTwoCategoryPro.GroupBy(P => P.Product.ProductCategory2.ProductCategoryName))
                {
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.Product.Id;
                        excel.Cells[row, 2] = pro.Product.ProductName;
                        excel.Cells[row, 3] = pro.Product.CustomerProductName;
                        excel.Cells[row, 4] = pro.Product.ProductVersion;
                        excel.Cells[row, 5] = pro.ActualQuantity;
                        excel.Cells[row, 6] = pro.TheoryQuantity;
                        excel.Cells[row, 7] = pro.DiffQty;

                        col = 9;
                        foreach (var dic in pro.Product.MaterialDic)
                        {
                            excel.Cells[row, col++] = dic.Value;
                        }

                        row++;
                    }
                    row++;
                }

                foreach (var item in haveOneCategoryPro.GroupBy(P => P.Product.ProductCategory.ProductCategoryName))
                {
                    SetExcelFormat(excel, ref col, ref row, item);

                    foreach (var pro in item)
                    {
                        excel.Cells[row, 1] = pro.Product.Id;
                        excel.Cells[row, 2] = pro.Product.ProductName;
                        excel.Cells[row, 3] = pro.Product.CustomerProductName;
                        excel.Cells[row, 4] = pro.Product.ProductVersion;
                        excel.Cells[row, 5] = pro.ActualQuantity;
                        excel.Cells[row, 6] = pro.TheoryQuantity;
                        excel.Cells[row, 7] = pro.DiffQty;

                        col = 9;
                        foreach (var dic in pro.Product.MaterialDic)
                        {
                            excel.Cells[row, col++] = dic.Value;
                        }

                        row++;
                    }
                    row++;
                }

                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private void SetExcelFormat(Microsoft.Office.Interop.Excel.Application excel, ref int col, ref int row, IGrouping<string, Book.Model.AssemblySiteDifferenceDetai> item)
        {
            excel.Cells[row, 1] = item.Key;
            excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 7 + 1 + this._assemblySiteDifference.Details[0].Product.MaterialDic.Keys.Count]).Interior.Color = "255";    //红色
            excel.get_Range(excel.Cells[row, 5], excel.Cells[row, 5]).Formula = string.Format("=SUM(E{0}:E{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 6], excel.Cells[row, 6]).Formula = string.Format("=SUM(F{0}:F{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 7], excel.Cells[row, 7]).Formula = string.Format("=SUM(G{0}:G{1})", row + 1, row + item.Count());

            col = 9;
            foreach (var ec in this._assemblySiteDifference.Details[0].Product.MaterialDic)
            {
                string excelColumnName = CountExcelColumnName(col);
                excel.get_Range(excel.Cells[row, col], excel.Cells[row, col]).Formula = string.Format("=SUM({2}{0}:{2}{1})", row + 1, row + item.Count(), excelColumnName);
                col++;
            }

            row++;
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

        private void ConvertMaterial()
        {
            IList<string> str = materialManager.SelectMaterialCategory();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var detail in this._assemblySiteDifference.Details)
            {
                var pro = detail.Product;
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
                            double value = Convert.ToDouble(materialnums[i]) * Convert.ToDouble(model.JWeight) * Convert.ToDouble(detail.DiffQty);
                            pro.MaterialDic[model.MaterialCategoryName] = (Convert.ToDouble(pro.MaterialDic[model.MaterialCategoryName]) + (value / 1000)).ToString("0.####");
                        }
                    }
                }
            }
        }
    }
}