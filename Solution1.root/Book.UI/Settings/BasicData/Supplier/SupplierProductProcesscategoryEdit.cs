using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;
using System.Linq;

namespace Book.UI.Settings.BasicData.Supplier
{
    public partial class SupplierProductProcesscategoryEdit : BasicData.BaseEditForm
    {
        private BL.SupplierManager _SupplierManager = new Book.BL.SupplierManager();
        //private BL.ProcessCategoryManager _ProcessCategoryManager = new Book.BL.ProcessCategoryManager();
        private BL.SupplierProductManager _SupplierProductManager = new Book.BL.SupplierProductManager();
        //private BL.SupplierProcesscategoryManager _SupplierProcesscategoryManager = new Book.BL.SupplierProcesscategoryManager();

        //private IList<Model.SupplierProcesscategory> _SPCList = new List<Model.SupplierProcesscategory>();
        private IList<Model.SupplierProduct> _SupProList = new List<Model.SupplierProduct>();
        private IList<PriceRange> _priceRangeList = new List<PriceRange>();

        private bool _SelectIsProduct = false;
        private DataTable _SupplierProductDT = new DataTable();
        private DataTable _QuickDT = new DataTable();
        //private Model.SupplierProcesscategory _SupplierProcesscategory = null;
        private Model.SupplierProduct _SupplierProduct = null;
        private Model.Supplier _CurrentSupplier = new Book.Model.Supplier();
        //private Model.ProcessCategory _CurrentProcessCategory = new Book.Model.ProcessCategory();
        private Model.Product _QuickSearchProduct = new Book.Model.Product();

        //public Model.SupplierProcesscategory _SelectedProc = new Book.Model.SupplierProcesscategory();
        //public Model.SupplierProduct _SelectedPro = new Book.Model.SupplierProduct();
        //选择商品项目

        public object _SelectItem;

        public SupplierProductProcesscategoryEdit()
        {
            InitializeComponent();

            this.bsSupplier.DataSource = this._SupplierManager.Select();
            this.grdQuickSearchPriceRange.DataSource = this.bsPriceAndRange;

            //this.LoadPCForSupplier();               //开始时候先加载供应商下左右加工商品
            //this.RefershSupplierProcessCatetory();  //开始时候加载具体加工下所有商品

            this.grdConSupProductPric.KeyDown += new KeyEventHandler(grdConSupProcProPric_KeyDown);
            this.gridView6.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridviewPriceRange_CustomColumnDisplayText);
            this.bsQuickSearch.CurrentChanged += new EventHandler(bsQuickSearch_CurrentChanged);

            string errorMessage = "價格區間填寫有誤,請查證后重新保存.\r請檢查以下項目\r1.價格區間是否連續.\r2.價格不能為零.\r3.<數量上限>不得小於<數量下限>.\r4.第一段價格區間,<數量下限>必須為1";

            this.invalidValueExceptions.Add("PriceRange.Error", new AA(errorMessage, this.grdConSupplier));
            this.requireValueExceptions.Add(Model.SupplierProduct.PRO_ProductId, new AA("商品不能為空!", this.btnEditProcProduct));
            this.gridColumn13.OptionsColumn.AllowEdit = true;
            this.nccAtCurrencyCategory.Choose = new Accounting.CurrencyCategory.ChooseAtCurrencyCategory();
            this.action = "view";
        }

        protected override void AddNew()
        {
            this._SupplierProduct = new Model.SupplierProduct();
            this._SupplierProduct.Supplier = this.bsSupplier.Current as Model.Supplier;
            if (this._SupplierProduct.Supplier != null)
                this._SupplierProduct.SupplierId = this._SupplierProduct.Supplier.SupplierId;
            this._SupplierProduct.SupplierProductPriceRange = "1/999999999999/0";

            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._SupplierProduct == null)
                this.AddNew();

            this.btnEditProcProduct.EditValue = this._SupplierProduct.Product;
            this.nccAtCurrencyCategory.EditValue = this._SupplierProduct.AtCurrencyCategory;
            this.AnalyzePriceRange(this._SupplierProduct.SupplierProductPriceRange);
            this.bsPriceAndRange.DataSource = this._priceRangeList;
            this.grdConSupProductPric.RefreshDataSource();


            base.Refresh();

            this.btnEdit_SearchALLProduct.Enabled = true;
            this.btnEdit_SearchALLProduct.Properties.ReadOnly = false;
            this.btnEdit_SearchALLProduct.Properties.Buttons[0].Visible = true;
        }

        protected override void Save()
        {
            this._SupplierProduct.Supplier = this.bsSupplier.Current as Model.Supplier;
            if (this._SupplierProduct.Supplier != null)
                this._SupplierProduct.SupplierId = this._SupplierProduct.Supplier.SupplierId;
            this._SupplierProduct.Product = this.btnEditProcProduct.EditValue as Model.Product;
            if (this._SupplierProduct.Product != null)
                this._SupplierProduct.ProductId = this._SupplierProduct.Product.ProductId;
            this._SupplierProduct.AtCurrencyCategory = this.nccAtCurrencyCategory.EditValue as Model.AtCurrencyCategory;
            if (this._SupplierProduct.AtCurrencyCategory != null)
                this._SupplierProduct.AtCurrencyCategoryId = this._SupplierProduct.AtCurrencyCategory.AtCurrencyCategoryId;

            if (!this.gridView6.PostEditor() || !this.gridView6.UpdateCurrentRow())
                return;
            //首先验证价格区间是否正确
            this.VerificationPriceRange();

            this._SupplierProduct.SupplierProductPriceRange = this.AssemblyPriceRange(this.bsPriceAndRange.DataSource as IList<PriceRange>);

            switch (this.action)
            {
                case "insert":
                    this._SupplierProductManager.Insert(this._SupplierProduct);
                    this._SupProList.Add(this._SupplierProduct);
                    this.bsSupplierProduct.DataSource = this._SupProList;
                    this.grdConSupProduct.RefreshDataSource();
                    break;
                case "update":
                    this._SupplierProductManager.Update(this._SupplierProduct);
                    break;
            }


        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this._SupplierProductManager.Delete(this._SupplierProduct.SupplierProductId);
            this._SupProList.Remove(this._SupplierProduct);
            this.bsSupplierProduct.DataSource = this._SupProList;
            this.grdConSupProduct.RefreshDataSource();

        }

        #region 注释 protected override void MoveNext()
        //{
        //    Model.SupplierProcesscategory spc = this._SupplierProcesscategoryManager.mGetNext(this._SupplierProcesscategory.InsertTime.Value, this._SupplierProcesscategory.SupplierId, this._SupplierProcesscategory.ProcessCategoryId);
        //    if (spc == null)
        //        throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

        //    this._SupplierProcesscategory = this._SupplierProcesscategoryManager.Get(spc.SupplierProcesscategoryId);
        #endregion }

        #region 注释 protected override void MovePrev()
        //{
        //    Model.SupplierProcesscategory spc = this._SupplierProcesscategoryManager.mGetPrev(this._SupplierProcesscategory.InsertTime.Value, this._SupplierProcesscategory.SupplierId, this._SupplierProcesscategory.ProcessCategoryId);
        //    if (spc == null)
        //        throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

        //    this._SupplierProcesscategory = this._SupplierProcesscategoryManager.Get(spc.SupplierProcesscategoryId);
        #endregion}

        protected override void MoveFirst()
        {
            Model.Supplier _inSup = this.bsSupplier.Current as Model.Supplier;
            //Model.ProcessCategory _inProc = this.bsProcessCategory.Current as Model.ProcessCategory;

            //Model.SupplierProcesscategory _inSPC = this._SupplierProcesscategoryManager.mGetFirst(_inSup.SupplierId);

            //this._SupplierProcesscategory = _inSPC == null ? null : this._SupplierProcesscategoryManager.Get(_inSPC.SupplierProcesscategoryId);
        }

        protected override void MoveLast()
        {
            Model.Supplier _inSup = this.bsSupplier.Current as Model.Supplier;
            //Model.ProcessCategory _inProc = this.bsProcessCategory.Current as Model.ProcessCategory;

            //Model.SupplierProcesscategory _inSPC = this._SupplierProcesscategoryManager.mGetLast(_inSup.SupplierId);

            //this._SupplierProcesscategory = _inSPC == null ? null : this._SupplierProcesscategoryManager.Get(_inSPC.SupplierProcesscategoryId);
        }

        protected override bool HasRows()
        {
            Model.Supplier _inSup = this.bsSupplier.Current as Model.Supplier;
            //Model.ProcessCategory _inProc = this.bsProcessCategory.Current as Model.ProcessCategory;

            //return this._SupplierProcesscategoryManager.mHasRows(_inSup.SupplierId);
            return true;
        }

        #region 注释 protected override bool HasRowsNext()
        //{
        //    return this._SupplierProcesscategoryManager.mHasRowsAfter(this._SupplierProcesscategory.InsertTime.Value, this._SupplierProcesscategory.SupplierId, this._SupplierProcesscategory.ProcessCategoryId);
        #endregion }

        #region 注释 protected override bool HasRowsPrev()
        //{
        //    return this._SupplierProcesscategoryManager.mHasRowsBefore(this._SupplierProcesscategory.InsertTime.Value, this._SupplierProcesscategory.SupplierId, this._SupplierProcesscategory.ProcessCategoryId);
        #endregion }

        //protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        //{
            //Model.Supplier sup = this.bsSupplier.Current as Model.Supplier;
            //return new ROSupplierProduct(sup, this._SupplierProductDT);
        //}

        //商品选择商品
        private void btnEditProcProduct_Click(object sender, EventArgs e)
        {
            if (this.action != "view")
            {
                ChooseProductForm f = new ChooseProductForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    this._SupplierProduct.Product = f.SelectedItem as Model.Product;
                    if (this._SupplierProduct.Product != null)
                        this._SupplierProduct.ProductId = this._SupplierProduct.Product.ProductId;
                    this._SupplierProduct.Supplier = this._CurrentSupplier;
                    if (this._SupplierProduct.Supplier != null)
                        this._SupplierProduct.SupplierId = this._SupplierProduct.Supplier.SupplierId;

                    foreach (Model.SupplierProduct sp in this.bsSupplierProduct.DataSource as IList<Model.SupplierProduct>)
                    {
                        if (this._SupplierProduct.SupplierId == sp.SupplierId && this._SupplierProduct.ProductId == sp.ProductId)
                        {
                            MessageBox.Show("有重複商品輸入", "提示");
                            break;
                        }
                    }

                    this._SupplierProduct.IsNewTemp = true; //临时
                    this._SupplierProduct.SupplierProductPriceRange = "1/999999999999/0";
                    this.btnEditProcProduct.EditValue = this._SupplierProduct.Product;

                    this.AnalyzePriceRange(this._SupplierProduct.SupplierProductPriceRange);
                    this.bsPriceAndRange.DataSource = this._priceRangeList;
                    this.grdConSupProductPric.RefreshDataSource();
                }
            }
        }

        //选择供应商改变
        private void bsSupplier_CurrentChanged(object sender, EventArgs e)
        {
            this._CurrentSupplier = this.bsSupplier.Current as Model.Supplier;

            this._SupProList = this._SupplierProductManager.mSelect(this._CurrentSupplier.SupplierId);
            this.bsSupplierProduct.DataSource = _SupProList;
            this.grdConSupProduct.RefreshDataSource();
        }

        //快速查询商品 供应商更改
        private void bsQuickSearch_CurrentChanged(object sender, EventArgs e)
        {
            this.bsPriceAndRange.DataSource = null;
            this.grdQuickSearchPriceRange.RefreshDataSource();
            DataRowView dr = this.bsQuickSearch.Current as DataRowView;

            this._SelectIsProduct = true;
            this._SupplierProduct = this._SupplierProductManager.Get(dr.Row["PrimiaryKey"].ToString());
            this.AnalyzePriceRange(this._SupplierProduct.SupplierProductPriceRange);

            this.bsPriceAndRange.DataSource = this._priceRangeList;
            this.grdQuickSearchPriceRange.RefreshDataSource();
        }

        //增加价格区间
        private void grdConSupProcProPric_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                switch (e.KeyData)
                {
                    case Keys.Enter:
                        PriceRange pradd = this._priceRangeList.Last();
                        if (pradd.endRange != 999999999999)
                            this._priceRangeList.Add(new PriceRange { startRange = pradd.endRange + 1, endRange = 999999999999, RangePrice = 0 });
                        break;
                    case Keys.Delete:
                        PriceRange prdel = this.bsPriceAndRange.Current as PriceRange;
                        if (prdel.endRange != 999999999999)
                        {
                            int pr_index = this._priceRangeList.IndexOf(prdel);
                            PriceRange prnext = this._priceRangeList[pr_index];
                            prnext.startRange = prdel.endRange + 1;
                            this._priceRangeList.Remove(prdel);
                            if (this._priceRangeList.Count == 0)
                                this._priceRangeList.Add(new PriceRange { startRange = 1, endRange = 999999999999, RangePrice = 0 });
                        }
                        break;
                }
                this.grdConSupProductPric.RefreshDataSource();
            }
        }

        //解析价格区间
        //DEMO: 0/0/0,0/0/0,0/0/0,0/0/0,0/0/0,0/0/0,0/999999999999/0
        //Means: {起始数量/终止数量/价格}
        private void AnalyzePriceRange(string priceR)
        {
            this._priceRangeList.Clear();
            if (string.IsNullOrEmpty(priceR))
                return;
            string[] inPriceR;
            if (priceR.Contains(','))
                inPriceR = priceR.Split(',');
            else
                inPriceR = new string[] { priceR };
            PriceRange pr = null;
            foreach (string s in inPriceR)
            {
                string[] prs = s.Split('/');
                pr = new PriceRange();
                pr.startRange = Convert.ToDouble(prs[0]);
                pr.endRange = Convert.ToDouble(prs[1]);
                pr.RangePrice = Convert.ToDouble(prs[2]);
                this._priceRangeList.Add(pr);
            }
        }

        //组装价格区间
        private string AssemblyPriceRange(IList<PriceRange> priceRList)
        {
            if (priceRList == null || priceRList.Count == 0)
                return string.Empty;

            StringBuilder Result = new StringBuilder();
            foreach (PriceRange pr in priceRList)
            {
                Result.Append(pr.startRange.ToString() + "/" + pr.endRange.ToString() + "/" + pr.RangePrice.ToString() + ",");
            }

            return Result.ToString().Substring(0, Result.ToString().Length - 1);
        }

        //检验价格区间是否正确
        private void VerificationPriceRange()
        {
            IList<PriceRange> priceRList = this.bsPriceAndRange.DataSource as IList<PriceRange>;
            if (priceRList != null && priceRList.Count > 0)
            {
                try
                {
                    if (priceRList.Last().endRange != 999999999999)
                        throw new Exception();

                    double ComSR, ComER, ComPrice, InSr, InEr, InPrice;
                    bool isContinue = true;
                    PriceRange prFirst = priceRList.First();

                    ComSR = prFirst.startRange; ComER = prFirst.endRange; ComPrice = prFirst.RangePrice;

                    foreach (PriceRange pr in priceRList)
                    {
                        InSr = pr.startRange; InEr = pr.endRange; InPrice = pr.RangePrice;
                        if (priceRList.IndexOf(pr) == 0)
                        {
                            if (InSr != 1 || InPrice <= 0 || InSr > InEr)
                                throw new Exception();
                        }
                        else
                        {
                            if (InSr != ComER + 1 || InPrice <= 0 || InSr > InEr)
                                throw new Exception();
                        }
                        ComSR = pr.startRange; ComER = pr.endRange; InPrice = pr.RangePrice;
                    }
                }
                catch
                {
                    throw new Helper.InvalidValueException("PriceRange.Error");
                }
            }
        }

        //对照商品 行选择变化
        private void gridView4_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this._SupplierProduct = this.bsSupplierProduct.Current as Model.SupplierProduct;
            this.action = this._SupplierProduct == null ? "insert" : "view";
            this.Refresh();
        }

        //选择外购商品
        private void gridView4_DoubleClick(object sender, EventArgs e)
        {
            this._SelectItem = this.bsSupplierProduct.Current as Model.SupplierProduct;
            if (this._SelectItem != null)
                this.DialogResult = DialogResult.OK;
        }

        //选择快速查询
        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            this._SelectItem = this._SupplierProduct;

            if (this._SelectItem != null)
                this.DialogResult = DialogResult.OK;
        }

        //价格区间显示
        private void gridviewPriceRange_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<PriceRange> details = this.bsPriceAndRange.DataSource as IList<PriceRange>;
            if (details == null || details.Count < 1) return;
            PriceRange prcur = details[e.ListSourceRowIndex] as PriceRange;
            if (prcur != null)
            {
                switch (e.Column.Name)
                {
                    case "gridColumn11":
                    case "gridColumn15":
                        e.DisplayText = prcur.endRange == 999999999999 ? "無限大" : prcur.endRange.ToString();
                        break;
                }
            }
        }

        //快速查询选择商品
        private void btnEdit_SearchALLProduct_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._QuickSearchProduct = f.SelectedItem as Model.Product;
                if (this._QuickSearchProduct != null)
                {
                    this.btnEdit_SearchALLProduct.EditValue = this._QuickSearchProduct;
                    this.bsQuickSearch.DataSource = this._SupplierProductManager.SelectALLRefProduct(this._QuickSearchProduct.ProductId);
                }
            }
        }
    }

    public class PriceRange
    {
        /// <summary>
        /// 数量下限
        /// </summary>
        public double startRange { get; set; }

        /// <summary>
        /// 数量上限
        /// </summary>
        public double endRange { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double RangePrice { get; set; }
    }
}