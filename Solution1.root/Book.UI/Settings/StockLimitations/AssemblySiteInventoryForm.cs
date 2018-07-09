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
    public partial class AssemblySiteInventoryForm : Settings.BasicData.BaseEditForm
    {
        private Model.AssemblySiteInventory _assemblySiteInventory;
        private BL.AssemblySiteInventoryManager manager = new Book.BL.AssemblySiteInventoryManager();
        BL.MaterialManager materialManager = new Book.BL.MaterialManager();
        int isLast = 0;

        public AssemblySiteInventoryForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.AssemblySiteInventory.PRO_InvoiceDate, new AA(Properties.Resources.DateNotNull, this.date_Inventory));

            this.ncc_Employee.Choose = new BasicData.Employees.ChooseEmployee();
            this.action = "view";
        }

        public AssemblySiteInventoryForm(Model.AssemblySiteInventory model)
            : this()
        {
            this._assemblySiteInventory = model;
            this.isLast = 1;
        }

        public AssemblySiteInventoryForm(Model.AssemblySiteInventory model, string action)
            : this()
        {
            this._assemblySiteInventory = model;
            this.isLast = 1;
            this.action = action;
        }

        protected override void AddNew()
        {
            this._assemblySiteInventory = new Book.Model.AssemblySiteInventory();
            this._assemblySiteInventory.AssemblySiteInventoryId = this.manager.GetId();
            this._assemblySiteInventory.Employee = BL.V.ActiveOperator.Employee;
            this.action = "insert";
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            if (this.date_Inventory.EditValue != null)
                this._assemblySiteInventory.InvoiceDate = this.date_Inventory.DateTime;
            this._assemblySiteInventory.EmployeeId = (this.ncc_Employee.EditValue == null ? null : (this.ncc_Employee.EditValue as Model.Employee).EmployeeId);
            this._assemblySiteInventory.Note = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(_assemblySiteInventory);
                    break;
                case "update":
                    this.manager.Update(_assemblySiteInventory);
                    break;
            }
        }

        public override void Refresh()
        {
            if (_assemblySiteInventory == null)
                this.AddNew();
            else if (this.action == "view")
            {
                this._assemblySiteInventory = this.manager.GetDetail(this._assemblySiteInventory.AssemblySiteInventoryId);
            }
            if (this._assemblySiteInventory.InvoiceState.HasValue && this._assemblySiteInventory.InvoiceState.Value)
            {
                this.bar_GenerateInvoice.Enabled = false;

                if (this.action == "update")
                {
                    this.action = "view";
                    MessageBox.Show("该单据已经生成组装现场盘点差异单，请勿修改！", this.Text, MessageBoxButtons.OK);
                    return;
                }
            }
            else
                this.bar_GenerateInvoice.Enabled = true;

            this.txt_ID.EditValue = this._assemblySiteInventory.AssemblySiteInventoryId;
            this.date_Inventory.EditValue = this._assemblySiteInventory.InvoiceDate;
            this.ncc_Employee.EditValue = this._assemblySiteInventory.Employee;
            this.txt_Note.EditValue = this._assemblySiteInventory.Note;


            this.bindingSourceDetail.DataSource = _assemblySiteInventory.Details;
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
        }

        protected override void MoveNext()
        {
            Model.AssemblySiteInventory model = this.manager.GetNext(_assemblySiteInventory);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _assemblySiteInventory = model;
        }

        protected override void MovePrev()
        {
            Model.AssemblySiteInventory model = this.manager.GetPrev(_assemblySiteInventory);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _assemblySiteInventory = model;
        }

        protected override void MoveFirst()
        {
            _assemblySiteInventory = this.manager.Get(this.manager.GetFirst() == null ? "" : this.manager.GetFirst().AssemblySiteInventoryId);
        }

        protected override void MoveLast()
        {
            if (this.isLast == 1)
            {
                this.isLast = 0;
                return;
            }
            _assemblySiteInventory = this.manager.Get(this.manager.GetLast() == null ? "" : this.manager.GetLast().AssemblySiteInventoryId);
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(_assemblySiteInventory);
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(_assemblySiteInventory);
        }

        protected override void Delete()
        {
            if (this._assemblySiteInventory.InvoiceState.HasValue && _assemblySiteInventory.InvoiceState.Value)
            {
                throw new Exception("该单据已经生成组装现场盘点差异单，请勿删除！");
            }

            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.manager.Delete(_assemblySiteInventory.AssemblySiteInventoryId);
                _assemblySiteInventory = this.manager.GetNext(_assemblySiteInventory);
                if (_assemblySiteInventory == null)
                {
                    _assemblySiteInventory = this.manager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new AssemblySiteInventoryRO(this._assemblySiteInventory);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.AssemblySiteInventoryDetail detail = null;
                if (Invoices.ChooseProductForm.ProductList != null || Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in Invoices.ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.AssemblySiteInventoryDetail();
                        detail.AssemblySiteInventoryDetailId = Guid.NewGuid().ToString();
                        detail.AssemblySiteInventoryId = this._assemblySiteInventory.AssemblySiteInventoryId;
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        this._assemblySiteInventory.Details.Add(detail);
                    }
                }
                else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.AssemblySiteInventoryDetail();
                    detail.AssemblySiteInventoryDetailId = Guid.NewGuid().ToString();
                    detail.AssemblySiteInventoryId = this._assemblySiteInventory.AssemblySiteInventoryId;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    this._assemblySiteInventory.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this._assemblySiteInventory.Details.Remove(this.bindingSourceDetail.Current as Book.Model.AssemblySiteInventoryDetail);

                this.gridControl1.RefreshDataSource();
            }
        }

        private void bar_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssemblySiteInventoryList f = new AssemblySiteInventoryList();
            f.ShowDialog(this);
        }

        private void bar_GenerateInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssemblySiteDifferenceForm f = new AssemblySiteDifferenceForm(this._assemblySiteInventory);
            f.ShowDialog(this);
        }

        private void bar_ExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.action != "view" || this._assemblySiteInventory.Details == null || this._assemblySiteInventory.Details.Count < 1)
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

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1,5]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 10;
                excel.Cells[1, 1] = "组装现场盘点差异(" + this.date_Inventory.DateTime.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 5]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "商品编号";
                excel.Cells[2, 2] = "商品名称";
                excel.Cells[2, 3] = "客户型号";
                excel.Cells[2, 4] = "版本";
                excel.Cells[2, 5] = "盘点数量";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 5 + 1 + this._assemblySiteInventory.Details[0].Product.MaterialDic.Keys.Count]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 1]).ColumnWidth = 25;
                excel.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).ColumnWidth = 50;

                int col = 7;
                //原料
                foreach (var item in this._assemblySiteInventory.Details[0].Product.MaterialDic)
                {
                    excel.Cells[2, col++] = item.Key;
                }

                List<Model.AssemblySiteInventoryDetail> haveThreeCategoryPro = this._assemblySiteInventory.Details.Where(P => P.Product.ProductCategory3 != null).ToList();
                List<Model.AssemblySiteInventoryDetail> haveTwoCategoryPro = this._assemblySiteInventory.Details.Where(P => P.Product.ProductCategory2 != null && P.Product.ProductCategory3 == null).ToList();
                List<Model.AssemblySiteInventoryDetail> haveOneCategoryPro = this._assemblySiteInventory.Details.Where(P => P.Product.ProductCategory2 == null && P.Product.ProductCategory3 == null).ToList();

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
                        excel.Cells[row, 5] = pro.Quantity;

                        col = 7;
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
                        excel.Cells[row, 5] = pro.Quantity;

                        col =7;
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
                        excel.Cells[row, 5] = pro.Quantity;

                        col = 7;
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
        private void SetExcelFormat(Microsoft.Office.Interop.Excel.Application excel, ref int col, ref int row, IGrouping<string, Book.Model.AssemblySiteInventoryDetail> item)
        {
            excel.Cells[row, 1] = item.Key;
            excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 5 + 1 + this._assemblySiteInventory.Details[0].Product.MaterialDic.Keys.Count]).Interior.Color = "255";    //红色
            excel.get_Range(excel.Cells[row, 5], excel.Cells[row, 5]).Formula = string.Format("=SUM(E{0}:E{1})", row + 1, row + item.Count());

            col = 7;
            foreach (var ec in this._assemblySiteInventory.Details[0].Product.MaterialDic)
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

            foreach (var detail in this._assemblySiteInventory.Details)
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
                            double value = Convert.ToDouble(materialnums[i]) * Convert.ToDouble(model.JWeight) * Convert.ToDouble(detail.Quantity);
                            pro.MaterialDic[model.MaterialCategoryName] = (Convert.ToDouble(pro.MaterialDic[model.MaterialCategoryName]) + (value / 1000)).ToString("0.####");
                        }
                    }
                }
            }
        }
    }
}