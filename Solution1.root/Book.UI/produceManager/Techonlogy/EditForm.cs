using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
namespace Book.UI.produceManager.Techonlogy
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.ProduceTransfer produceTransfer = new Book.Model.ProduceTransfer();
        BL.ProduceTransferManager produceTransferManager = new Book.BL.ProduceTransferManager();


        BL.ProduceTransferDetailManager produceTransferDetailManager = new Book.BL.ProduceTransferDetailManager();

        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        protected BL.PronoteHeaderManager pronoteHeaderManager = new BL.PronoteHeaderManager();
        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.ProceduresManager proceduresManager = new BL.ProceduresManager();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceTransfer.PRO_ProduceTransferId, new AA(Properties.Resources.RequireDataForId, this.textEditProduceTransferId));
            //this.requireValueExceptions.Add(Model.ProduceTransfer.PRO_WorkHouseInId, new AA("請選擇移入工作中心", this.newChooseContorlWorkHouseInId));
            this.requireValueExceptions.Add(Model.ProduceTransfer.PRO_WorkHouseOutId, new AA("請選擇移出工作中心", this.newChooseContorlWorkHouseOutId));
            this.newChooseContorlEmployee0Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployee1Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployee2Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            //this.newChooseContorlWorkHouseInId.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.newChooseContorlWorkHouseOutId.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.action = "insert";
        }
        /// <summary>
        /// 带一个参构造函数
        /// </summary>
        public EditForm(Model.ProduceTransfer produceTransfer)
            : this()
        {
            this.produceTransfer = produceTransfer;
            this.produceTransfer.Details = this.produceTransferDetailManager.Select(produceTransfer);
            this.action = "update";
        }
        /// <summary>
        /// 带两个参构造函数
        /// </summary>
        public EditForm(Model.ProduceTransfer produceTransfer, string action)
            : this()
        {
            this.produceTransfer = produceTransfer;
            this.produceTransfer.Details = this.produceTransferDetailManager.Select(produceTransfer);
            this.action = action;
        }
        #region Save  新增

        protected override void Save()
        {
            this.produceTransfer.ProduceTransferId = this.textEditProduceTransferId.Text;
            this.produceTransfer.description = this.memoEditdescription.Text;
            //this.produceTransfer.WorkHouseIn = this.newChooseContorlWorkHouseInId.EditValue as Model.WorkHouse;
            //if (this.produceTransfer.WorkHouseIn != null)
            //{
            //    this.produceTransfer.WorkHouseInId = this.produceTransfer.WorkHouseIn.WorkHouseId;
            //}
            this.produceTransfer.WorkHouseOut = this.newChooseContorlWorkHouseOutId.EditValue as Model.WorkHouse;
            if (this.produceTransfer.WorkHouseOut != null)
            {
                this.produceTransfer.WorkHouseOutId = this.produceTransfer.WorkHouseOut.WorkHouseId;
                this.produceTransfer.WorkHouseInId = this.produceTransfer.WorkHouseOut.WorkHouseId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceTransferDate.DateTime, new DateTime()))
            {
                this.produceTransfer.ProduceTransferDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.produceTransfer.ProduceTransferDate = this.dateEditProduceTransferDate.DateTime;
            }
            this.produceTransfer.Employee0 = (this.newChooseContorlEmployee0Id.EditValue as Model.Employee);
            if (this.produceTransfer.Employee0 != null)
            {
                this.produceTransfer.Employee0Id = this.produceTransfer.Employee0.EmployeeId;
            }
            this.produceTransfer.Employee1 = (this.newChooseContorlEmployee1Id.EditValue as Model.Employee);
            if (this.produceTransfer.Employee1 != null)
            {
                this.produceTransfer.Employee1Id = this.produceTransfer.Employee1.EmployeeId;
            }
            this.produceTransfer.Employee2 = (this.newChooseContorlEmployee2Id.EditValue as Model.Employee);
            if (this.produceTransfer.Employee2 != null)
            {
                this.produceTransfer.Employee2Id = this.produceTransfer.Employee2.EmployeeId;
            }
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.produceTransferManager.Insert(this.produceTransfer);
                    break;

                case "update":
                    this.produceTransferManager.Update(this.produceTransfer);
                    break;
            }

        }
        #endregion
        #region  删除
        protected override void Delete()
        {
            this.produceTransferManager.Delete(this.produceTransfer.ProduceTransferId);
        }

        #endregion

        #region 刷新
        public override void Refresh()
        {

            if (this.produceTransfer == null)
            {
                this.produceTransfer = new Book.Model.ProduceTransfer();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {

                    this.produceTransfer = this.produceTransferManager.GetDetails(produceTransfer.ProduceTransferId);

                }

            }

            this.textEditProduceTransferId.Text = this.produceTransfer.ProduceTransferId;
            this.memoEditdescription.Text = this.produceTransfer.description;
            //this.newChooseContorlWorkHouseInId.EditValue = this.produceTransfer.WorkHouseIn;
            this.newChooseContorlWorkHouseOutId.EditValue = this.produceTransfer.WorkHouseOut;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.produceTransfer.ProduceTransferDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceTransferDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceTransferDate.EditValue = this.produceTransfer.ProduceTransferDate;
            }
            this.newChooseContorlEmployee0Id.EditValue = this.produceTransfer.Employee0;
             this.newChooseContorlEmployee1Id.EditValue = this.produceTransfer.Employee1;
             this.newChooseContorlEmployee2Id.EditValue = this.produceTransfer.Employee2;
            this.bindingSourceDetails.DataSource = this.produceTransfer.Details;
            
            base.Refresh();
        }
         #endregion
          //下一笔
        protected override void MoveNext()
        {
            Model.ProduceTransfer produceTransfer = this.produceTransferManager.GetNext(this.produceTransfer);
            if (produceTransfer == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.produceTransfer = this.produceTransferManager.Get(produceTransfer.ProduceTransferId);
        }

        //上一笔
        protected override void MovePrev()
        {
            Model.ProduceTransfer produceTransfer = this.produceTransferManager.GetPrev(this.produceTransfer);
            if (produceTransfer == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.produceTransfer = this.produceTransferManager.Get(produceTransfer.ProduceTransferId);
        }

        //首笔
        protected override void MoveFirst()
        {
            this.produceTransfer = this.produceTransferManager.Get(this.produceTransferManager.GetFirst() == null ? "" : this.produceTransferManager.GetFirst().ProduceTransferId);
        }

        //尾笔
        protected override void MoveLast()
        {
            // if (produceTransfer == null)
            {
                this.produceTransfer = this.produceTransferManager.Get(this.produceTransferManager.GetLast() == null ? "" : this.produceTransferManager.GetLast().ProduceTransferId);
            }
        }
        protected override bool HasRows()
        {
            return this.produceTransferManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceTransferManager.HasRowsAfter(this.produceTransfer);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceTransferManager.HasRowsBefore(this.produceTransfer);
        }
        protected override void AddNew()
        {

            this.produceTransfer = new Model.ProduceTransfer();
            this.produceTransfer.ProduceTransferId = this.produceTransferManager.GetId();
            this.produceTransfer.ProduceTransferDate = DateTime.Now;
            this.produceTransfer.Details = new List<Model.ProduceTransferDetail>();

            if (this.action == "insert")
            {
                Model.ProduceTransferDetail detail = new Model.ProduceTransferDetail();
                detail.ProduceTransferDetailId = Guid.NewGuid().ToString();
                detail.ScrapQuantity = 0;
                detail.TransferQuantity = 0;
                detail.Product = new Book.Model.Product();
                this.produceTransfer.Details.Add(detail);
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceTransferDetail> details = this.bindingSourceDetails.DataSource as IList<Model.ProduceTransferDetail>;
            if (details == null || details.Count < 1) return;
            Model.Procedures detail = details[e.ListSourceRowIndex].ProceduresOut;
            switch (e.Column.Name)
            {
                case "gridProceduresOut":
                    if (detail == null) return;
                    // e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;


            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.gridView1.FocusedColumn.Name == "gridColumnProductUnit")
                {

                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceTransferDetail).Product;

                        this.repositoryItemComboBox1.Items.Clear();
                        if (p != null)
                        {
                            if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                            {
                                BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                                IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroup);
                                foreach (Model.ProductUnit item in unitList)
                                {
                                    this.repositoryItemComboBox1.Items.Add(item.CnName);
                                }

                            }

                        }
                    }
                }
                if (this.gridView1.FocusedColumn.Name == "gridProceduresOut" || this.gridView1.FocusedColumn.Name == "gridProceduresIn")
                {

                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {

                        this.repositoryItemComboBox2.Items.Clear();
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceTransferDetail).Product;
                        if (p != null)
                        {
                            Model.BomParentPartInfo bom =new BL.BomParentPartInfoManager().Get(p);
                            if (bom == null) return;
                            Model.TechonlogyHeader th = new BL.TechonlogyHeaderManager().GetDetail(bom.TechonlogyHeaderId);

                            if (th != null)
                            {
                                foreach (Model.Technologydetails item in th.detail)
                                {
                                    this.repositoryItemComboBox2.Items.Add(item.Procedures.Id);


                                }
                            }

                        }
                    }
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProduceTransferDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceTransferDetail;
            if (detail == null) return;
            if (e.Column == this.gridProductId)
            {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.ProduceTransferDetailId = Guid.NewGuid().ToString();
                    detail.ScrapQuantity = 0;
                    detail.TransferQuantity = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.ProductUnit = p.ProduceUnit.CnName;
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);               
              
            }
            if (e.Column == this.gridProceduresOut)
            {

                Model.Procedures procedures = this.proceduresManager.GetById(e.Value.ToString());

                    if (procedures != null)
                    {
                        detail.ProceduresOut = procedures;
                        detail.ProceduresOutId = procedures.ProceduresId;
                    }
                          
            }
            if (e.Column == this.gridProceduresIn)
            {
               
                    Model.Procedures procedures = this.proceduresManager.GetById(e.Value.ToString());

                    if (procedures != null)
                    {  
                        detail.ProceduresIn = procedures;
                        detail.ProceduresInId = procedures.ProceduresId;
                    }
               

            }
            this.gridControl1.RefreshDataSource();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            this.bindingSourceWorkHouse.DataSource = new BL.WorkHouseManager().Select();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Product product = f.SelectedItem as Model.Product;
                Model.ProduceTransferDetail detail = new Book.Model.ProduceTransferDetail();
                detail.ProduceTransferDetailId = Guid.NewGuid().ToString();
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.ProductUnit = (f.SelectedItem as Model.Product).ProduceUnit.CnName;
                detail.ScrapQuantity = 0;
                detail.TransferQuantity = 0;
                this.produceTransfer.Details.Add(detail);
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
             
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this.produceTransfer.Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceTransferDetail);

                if (this.produceTransfer.Details.Count == 0)
                {
                    Model.ProduceTransferDetail detail = new Model.ProduceTransferDetail();
                    detail.ProduceTransferDetailId = Guid.NewGuid().ToString();
                    detail.ScrapQuantity = 0;
                    detail.TransferQuantity = 0;
                    detail.Product = new Book.Model.Product();
                    this.produceTransfer.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new XR1(produceTransfer);
        }
        private void barButtonPronoteHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new PronoteHeader.ChoosePronoteHeaderDetailsForm(1);                 
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList.Count != 0)
                {
                    if (this.produceTransfer.Details.Count > 0 && this.produceTransfer.Details[0].ProductId == null)
                        this.produceTransfer.Details.Remove(this.produceTransfer.Details[0]);
                    foreach (Model.PronoteHeader pronoteHeader in PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList)
                    {                
                        Model.ProduceTransferDetail detail = new Book.Model.ProduceTransferDetail();

                        detail.ProduceTransferDetailId = Guid.NewGuid().ToString();
                        if (pronoteHeaderManager.Get(pronoteHeader.PronoteHeaderID) != null)
                        {
                           Model.InvoiceXO xo= new BL.InvoiceXOManager().Get( pronoteHeaderManager.Get(pronoteHeader.PronoteHeaderID).InvoiceXOId);
                           if (xo != null)
                           {
                               detail.CustomerInvoiceXOId = xo.CustomerInvoiceXOId;
                           }
                        }
                        detail.PronoteHeaderId = pronoteHeader.PronoteHeaderID;
                        detail.Product = pronoteHeader.Product;
                        detail.ProductId = pronoteHeader.ProductId;
                        detail.ProductUnit = pronoteHeader.ProductUnit;
                        detail.ScrapQuantity = 0;
                        detail.ProceduresQuantity = pronoteHeader.DetailsSum;
                        detail.TransferQuantity = pronoteHeader.DetailsSum;
                        this.produceTransfer.Details.Add(detail);
                        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);                   
                    
                    }
                    this.gridControl1.RefreshDataSource();
                }
            }





        }
        
    }
}