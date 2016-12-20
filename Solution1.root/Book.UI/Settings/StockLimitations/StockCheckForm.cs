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
    public partial class StockCheckForm : Settings.BasicData.BaseEditForm
    {
        private BL.StockCheckManager stockCheckManager = new Book.BL.StockCheckManager();
        private Model.StockCheck _stockCheck = new Book.Model.StockCheck();
        private BL.DepotManager depotManger = new Book.BL.DepotManager();
        private BL.ProductCategoryManager productCategoryManager = new Book.BL.ProductCategoryManager();
        private BL.ProductManager productManager = new Book.BL.ProductManager();
        private Model.StockEditor _stockEditor = new Book.Model.StockEditor();
        private BL.StockManager stockManager = new Book.BL.StockManager();

        public StockCheckForm()
        {
            InitializeComponent();
            this.action = "view";
            this.newChooseContorlEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
        }

        protected override void AddNew()
        {
            this._stockCheck = new Model.StockCheck();
            this._stockCheck.StockCheckId = this.stockCheckManager.GetId();
            this._stockCheck.StockCheckDate = DateTime.Now;
            this._stockCheck.Employee = BL.V.ActiveOperator.Employee;
            this._stockCheck.Details = new List<Model.StockCheckDetail>();
            //if (this.action == "insert")
            //{
            //    Model detail = new Model._stockCheckCJDetail();
            //    detail._stockCheckCJDetailId = Guid.NewGuid().ToString();
            //    detail._stockCheckCJDetailMoney = 0;
            //    detail._stockCheckCJDetailNote = "";
            //    detail._stockCheckCJDetailPrice = 0;
            //    detail._stockCheckCJDetailQuantity = 0;
            //    detail._stockCheckProductUnit = "";
            //    detail.Product = new Book.Model.Product();
            //    this._stockCheck.Details.Add(detail);
            //    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            //}
        }

        protected override void Save()
        {
            this._stockCheck.ProductCategoryId = this.textEditId.Text;

            if (this.newChooseContorlEmp.EditValue != null)
            {
                this._stockCheck.Employee = this.newChooseContorlEmp.EditValue as Model.Employee;
                this._stockCheck.EmployeeId = this._stockCheck.Employee.EmployeeId;
            }
            //this._stockCheck.Employee0 = this.newChooseContorlEmp.EditValue as Model.Employee;
            //this._stockCheck.Employee0Id = this._stockCheck.Employee0.EmployeeId;
            // }
            this._stockCheck.Directions = this.memoEditDesc.Text;
            if (this.lookUpEditProCate.EditValue != null)
                this._stockCheck.ProductCategoryId = this.lookUpEditProCate.EditValue.ToString();
            if (this.lookUpEditDepot.EditValue != null)
                this._stockCheck.Depot = this.depotManger.Get(this.lookUpEditDepot.EditValue.ToString());
            if (this._stockCheck.Depot == null)
            {
                throw new Helper.MessageValueException("库房不能為空！");
            }
            this._stockCheck.DepotId = this._stockCheck.Depot.DepotId;

            if (global::Helper.DateTimeParse.DateTimeEquls(this._stockCheck.StockCheckDate, new DateTime()))
            {
                this._stockCheck.StockCheckDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._stockCheck.StockCheckDate = this.dateEditDate.DateTime;
            }
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this.stockCheckManager.Insert(this._stockCheck);
                    break;
                case "update":
                    this.stockCheckManager.Update(this._stockCheck);
                    break;
                default:
                    break;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.dateEditDate.EditValue == null)
            {
                MessageBox.Show("请先选择日期！");
                return;
            }
            StockEditorChooseForm f = new StockEditorChooseForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this._stockEditor = f.SelectedItem as Model.StockEditor;


                this.lookUpEditDepot.EditValue = this._stockEditor.DepotId;
                this.lookUpEditProCate.EditValue = this._stockEditor.ProductCategoryId;
                this._stockCheck.StockEditorId = this._stockEditor.StockEditorId;
                foreach (Model.StockEditorDetal editDetail in new BL.StockEditorDetalManager().SelectByStockEditorId(this._stockEditor.StockEditorId))
                {

                    Model.StockCheckDetail detail = new Book.Model.StockCheckDetail();
                    detail.StockCheckDetailId = Guid.NewGuid().ToString();
                    detail.Product = editDetail.Product;
                    detail.ProductId = editDetail.ProductId;
                    detail.ProductUnitName = editDetail.ProductUnitName;
                    detail.StockCheck = this._stockCheck;
                    detail.StockCheckId = this._stockCheck.StockCheckId;
                    detail.StockCheckQuantity = editDetail.StockEditorQuantity;
                    //detail.StockCheckBookQuantity = this.stockManager.GetStockByProductIdAndDepotPositionId(editDetail.ProductId, editDetail.DepotPositionId) == null ? 0 : this.stockManager.GetStockByProductIdAndDepotPositionId(editDetail.ProductId, editDetail.DepotPositionId).StockQuantity1;
                    detail.StockCheckBookQuantity = this.CountJiShiStock(editDetail.ProductId, editDetail.DepotPositionId);
                    detail.DepotPosition = editDetail.DepotPosition;
                    detail.DepotPositionId = editDetail.DepotPositionId;
                    detail.Directions = editDetail.Directions;
                    this._stockCheck.Details.Add(detail);
                    this.gridControl1.RefreshDataSource();

                }

            }

        }

        private double CountJiShiStock(string productId, string depotpositionId)
        {
            double value = 0;
            IList<Model.StockSeach> stockList = new List<Model.StockSeach>();
            DateTime date = this.dateEditDate.DateTime.AddDays(1);

            value = stockManager.SelectStockQuantity1(productId, depotpositionId);
            stockList = this.stockManager.SelectJiShi(productId, date, DateTime.Now);
            if (stockList != null && stockList.Count > 0)
            {

                var list = from s in stockList
                           where s.PositionName == depotpositionId  //调拨单为进
                           orderby s.InvoiceDate.Value.Date descending
                           select s;

                if (list.Where(l => l.InvoiceTypeIndex == 3).Count() > 0)
                {

                    Model.StockSeach seach = list.Where(l => l.InvoiceTypeIndex == 3).OrderBy(o => o.InvoiceDate.Value.Date).ThenBy(d => d.InsertTime.Value).FirstOrDefault();


                    list = list.Where(l => l.InvoiceDate.Value.Date <= seach.InvoiceDate.Value.Date && l.InsertTime.Value < seach.InsertTime.Value)
                         .OrderByDescending(o => o.InvoiceDate.Value.Date);

                    value = (double)seach.StockCheckBookQuantity;


                }

                if (list != null && list.Count() > 0)
                {
                    foreach (Model.StockSeach stock in list.ToList<Model.StockSeach>())
                    {

                        if (stock.InvoiceTypeIndex == 0)
                        {
                            value = value + stock.InvoiceQuantity.Value;

                        }
                        if (stock.InvoiceTypeIndex == 1)
                        {
                            value = value - stock.InvoiceQuantity.Value;

                        }
                        if (stock.InvoiceTypeIndex == 2)
                        {
                            value = value - stock.InvoiceQuantity.Value;

                        }

                    }

                    var list1 = from s in stockList
                                where s.OutPositionName == depotpositionId //挑拨单为出
                                orderby s.InvoiceDate.Value.Date descending
                                select s;

                    foreach (Model.StockSeach stock in list1.ToList<Model.StockSeach>())
                    {

                        if (stock.InvoiceTypeIndex == 2)
                        {
                            value = value + stock.InvoiceQuantity.Value;

                        }

                    }
                }
            }
            return value;
        }

        private void StockCheckForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT productid,id,productname,CustomerProductName FROM product  ";
            this.bindingSourceProduct.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            this.bindingSourceDepot.DataSource = this.depotManger.Select();
            this.bindingSourceProCate.DataSource = this.productCategoryManager.Select();
            // this.bindingSourceProduct.DataSource=this.productManager.SelectNotCustomer();
            this.bindingSourcePosition.DataSource = new BL.DepotPositionManager().Select();
        }

        protected override void MoveNext()
        {
            Model.StockCheck _stockCheck = this.stockCheckManager.GetNext(this._stockCheck);
            if (_stockCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._stockCheck = this.stockCheckManager.Get(_stockCheck.StockCheckId);
        }

        protected override void MovePrev()
        {
            Model.StockCheck _stockCheck = this.stockCheckManager.GetPrev(this._stockCheck);
            if (_stockCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._stockCheck = this.stockCheckManager.Get(_stockCheck.StockCheckId);
        }

        protected override void MoveFirst()
        {
            this._stockCheck = this.stockCheckManager.Get(this.stockCheckManager.GetFirst() == null ? "" : this.stockCheckManager.GetFirst().StockCheckId);
        }

        protected override void MoveLast()
        {
            this._stockCheck = this.stockCheckManager.Get(this.stockCheckManager.GetLast() == null ? "" : this.stockCheckManager.GetLast().StockCheckId);
        }

        protected override void Delete()
        {

            if (this._stockCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            // try
            //{
            this.stockCheckManager.Delete(this._stockCheck);
            this._stockCheck = this.stockCheckManager.GetNext(this._stockCheck);
            if (this._stockCheck == null)
            {
                this._stockCheck = this.stockCheckManager.GetLast();
            }
            // this.stockCheckManager.Delete(this._stockCheck);
        }

        protected override bool HasRows()
        {
            return this.stockCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.stockCheckManager.HasRowsAfter(this._stockCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this.stockCheckManager.HasRowsBefore(this._stockCheck);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            //  return null;
            return new StockCheckReport(this._stockCheck);

        }

        public override void Refresh()
        {
            if (this._stockCheck == null)
            {
                //this._stockCheck = new Book.Model.StockCheck();
                this.action = "insert";
                AddNew();
            }
            else
            {
                if (this.action == "view")
                    this._stockCheck = this.stockCheckManager.GetDetails(_stockCheck.StockCheckId);
            }
            this.textEditId.Text = this._stockCheck.StockCheckId;

            if (global::Helper.DateTimeParse.DateTimeEquls(this._stockCheck.StockCheckDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditDate.EditValue = null;
            }
            else
            {
                this.dateEditDate.EditValue = this._stockCheck.StockCheckDate;
            }

            this.newChooseContorlEmp.EditValue = this._stockCheck.Employee;
            this.memoEditDesc.Text = this._stockCheck.Directions;

            this.lookUpEditProCate.EditValue = this._stockCheck.ProductCategoryId;

            this.lookUpEditDepot.EditValue = this._stockCheck.DepotId;// this.depotManger.Get(this.lookUpEditDepot.EditValue.ToString());
            //this._stockCheck.DepotId = this._stockCheck.Depot.DepotId;

            this.newChooseContorlAuditEmp.EditValue = this._stockCheck.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._stockCheck.AuditState);

            this.bindingSource1.DataSource = this._stockCheck.Details;

            switch (this.action)
            {
                case "insert":
                    //this.textEdit_stockCheckId.Properties.ReadOnly = false;
                    //this.dateEdit_stockCheckDate.Properties.ReadOnly = false;
                    //this.dateEdit_stockCheckDate.Properties.Buttons[0].Visible = true;

                    //this.barButtonItemGeneral.Enabled = false;

                    ////this.textEditAbstract.Properties.ReadOnly = false;
                    //this.textEditNote.Properties.ReadOnly = false;

                    //this.buttonEditCompany.ShowButton = true;
                    //this.buttonEditEmployee.ShowButton = true;

                    //this.buttonEditCompany.ButtonReadOnly = false;
                    //this.buttonEditEmployee.ButtonReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;

                    //this.simpleButtonAppend.Enabled = true;
                    //this.simpleButtonRemove.Enabled = true;
                    break;

                case "update":
                    //this.textEdit_stockCheckId.Properties.ReadOnly = true;
                    //this.dateEdit_stockCheckDate.Properties.ReadOnly = true;
                    //this.dateEdit_stockCheckDate.Properties.Buttons[0].Visible = false;

                    //this.barButtonItemGeneral.Enabled = false;

                    ////this.textEditAbstract.Properties.ReadOnly = false;
                    //this.textEditNote.Properties.ReadOnly = false;

                    //this.buttonEditCompany.ShowButton = true;
                    //this.buttonEditEmployee.ShowButton = true;

                    //this.buttonEditCompany.ButtonReadOnly = false;
                    //this.buttonEditEmployee.ButtonReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;

                    //this.simpleButtonAppend.Enabled = true;
                    //this.simpleButtonRemove.Enabled = true;
                    break;

                case "view":
                    //this.textEdit_stockCheckId.Properties.ReadOnly = true;
                    //this.dateEdit_stockCheckDate.Properties.ReadOnly = true;
                    //this.dateEdit_stockCheckDate.Properties.Buttons[0].Visible = false;

                    //this.barButtonItemGeneral.Enabled = true;

                    ////this.textEditAbstract.Properties.ReadOnly = true;
                    //this.textEditNote.Properties.ReadOnly = true;

                    //this.buttonEditCompany.ShowButton = false;
                    //this.buttonEditEmployee.ShowButton = false;

                    //this.buttonEditCompany.ButtonReadOnly = true;
                    //this.buttonEditEmployee.ButtonReadOnly = true;

                    this.gridView1.OptionsBehavior.Editable = false;

                    //this.simpleButtonAppend.Enabled = false;
                    //this.simpleButtonRemove.Enabled = false;
                    break;

                default:
                    break;
            }

            base.Refresh();
            this.newChooseContorlEmp.ShowButton = false;
            this.newChooseContorlEmp.ButtonReadOnly = true;
            this.lookUpEditDepot.Enabled = false;
            this.lookUpEditProCate.Enabled = false;
        }

        private void dateEditDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action == "insert")
            { this.textEditId.EditValue = this.stockCheckManager.GetId(this.dateEditDate.DateTime); }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column == this.gridColumnQuantity)
            {
                decimal quantity1 = decimal.Zero;
                decimal quantity = decimal.Zero;
                decimal.TryParse(e.Value.ToString(), out quantity);
                decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn5).ToString(), out quantity1);

                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnDiff, quantity1 - quantity);
            }
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StockCheckList f = new StockCheckList();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.StockCheck currentModel = f.SelectItem as Model.StockCheck;
                if (currentModel != null)
                {
                    this._stockCheck = currentModel;
                    this._stockCheck = this.stockCheckManager.GetDetails(this._stockCheck.StockCheckId);
                    this.Refresh();
                }
            }
            f.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.StockCheck.PROPERTY_STOCKCHECKID;
        }

        protected override int AuditState()
        {
            return this._stockCheck.AuditState.HasValue ? this._stockCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "StockCheck" + "," + this._stockCheck.StockCheckId;
        }

        #endregion

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this._stockCheck.Details.Remove(this.bindingSource1.Current as Model.StockCheckDetail);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}