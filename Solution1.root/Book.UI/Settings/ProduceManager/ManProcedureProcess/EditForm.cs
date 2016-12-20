using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Customs;

namespace Book.UI.Settings.ProduceManager.ManProcedureProcess
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-22
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.Procedures _procedures = new Book.Model.Procedures();
        BL.ProceduresManager proceduresManager = new Book.BL.ProceduresManager();
        Model.BomParentPartInfo _bomParentPartInfo = new Book.Model.BomParentPartInfo();
        BL.BomParentPartInfoManager bomParentPartInfo = new Book.BL.BomParentPartInfoManager();
        IList<Model.Procedures> _proceduresDetail = new List<Model.Procedures>();
        BL.ManProcedureManager manProceductManager = new Book.BL.ManProcedureManager();
        Model.ManProcedure _manprocedure = new Book.Model.ManProcedure();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.TechnologydetailsManager technologydetailsManager = new BL.TechnologydetailsManager();
        private Model.Product _madeProduct ;
        //加载
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ManProcedure.PRO_BomId, new AA("暫無該貨品BOM單信息", this.textEditBOMID));
            this.action = "insert";

            this.newChooseContorlCustomer.Choose = new ChooseCustoms();
        }

        /// <summary>
        /// 选择货品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Product product = f.SelectedItem as Model.Product;
                if (product != null)
                {
                    this.buttonEditProduct.Text = product.Id;
                    this.textEditProductName.Text = string.IsNullOrEmpty(product.CustomerProductName) ? product.ProductName : product.ProductName + "{" + product.CustomerProductName + "}";

                    if (!string.IsNullOrEmpty(product.CustomerProductName))
                        this.textEditCustomerPro.Text = product.CustomerProductName;

                    if (product.IsCustomerProduct == true && !string.IsNullOrEmpty(product.CustomerProductName))
                    {
                        this._bomParentPartInfo = bomParentPartInfo.Get(this.productManager.Get(product.CustomerBeforeProductId));
                        // this.textEditCustomProduct.Text = product.CustomerProductName;
                        this.newChooseContorlCustomer.EditValue = product.Customer;
                    }
                    else
                    {
                        this._bomParentPartInfo = bomParentPartInfo.Get(product);
                        // this.textEditCustomProduct.Text = "";
                        this.newChooseContorlCustomer.EditValue = null;
                    }
                    this._manprocedure.Bom = this._bomParentPartInfo;
                    if (this._bomParentPartInfo != null)
                    {
                        this.textEditBOMID.EditValue = this._bomParentPartInfo.Id;
                        // this.textEditBOMType.EditValue = this._bomParentPartInfo.MaterialType;                    
                    }
                }
            }
            f.Dispose();
            GC.Collect();
        }





        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this._manprocedure.detail.Remove(this.bindingSource1.Current as Book.Model.Procedures);

                this.gridControl1.RefreshDataSource();
            }

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            ChooseBomProcedureForm f = new ChooseBomProcedureForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Procedures procedures = f.SelectedItem as Model.Procedures;

                //   Model.Procedures procedures = new Book.Model.Procedures();
                this._manprocedure.detail.Add(procedures);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(procedures);
                this.gridControl1.RefreshDataSource();


            }
        }
        protected override void AddNew()
        {
            this._manprocedure = new Model.ManProcedure();
        }
        protected override void Save()
        {


            if (new BL.BomParentPartInfoManager().GetById(this.textEditBOMID.Text) != null)

                this._manprocedure.BomId = new BL.BomParentPartInfoManager().GetById(this.textEditBOMID.Text).BomId;
            if (this.buttonEditTechonlogyHeaderid.EditValue as Model.TechonlogyHeader != null)
                this._manprocedure.TechonlogyHeaderId = (this.buttonEditTechonlogyHeaderid.EditValue as Model.TechonlogyHeader).TechonlogyHeaderId;
            if (this.newChooseContorlCustomer.EditValue != null)
            {
                this._manprocedure.Customer = this.newChooseContorlCustomer.EditValue as Model.Customer;
                this._manprocedure.CustomerId = this._manprocedure.Customer.CustomerId;
            }

            else
                this._manprocedure.CustomerId = null;
           
             this._manprocedure.MadeProduct = this._madeProduct;
             if (this._madeProduct != null)
                 this._manprocedure.MadeProductId = this._madeProduct.ProductId;
            switch (this.action)
            {
                case "insert":
                    this.manProceductManager.Insert(this._manprocedure);
                    break;
                case "update":
                    this.manProceductManager.Update(this._manprocedure);
                    break;
                default:
                    break;
            }
          
        }
        public override void Refresh()
        {
            if(this._manprocedure == null)
            {
                this._manprocedure = new Book.Model.ManProcedure();
                this.action = "insert";
            }
            if (this._manprocedure.Bom != null)
            {
                if (string.IsNullOrEmpty(this._manprocedure.Bom.Product.CustomerProductName))
                    this.textEditProductName.EditValue = this._manprocedure.Bom.Product.ProductName;
                else
                    this.textEditProductName.EditValue = this._manprocedure.Bom.Product.ProductName + "{" + this._manprocedure.Bom.Product.CustomerProductName + "}";
                this.buttonEditProduct.EditValue = this._manprocedure.Bom.Product.Id;
                this.textEditBOMID.EditValue = this._manprocedure.Bom.Id;
                //this.textEditBOMType.EditValue = this._manprocedure.Bom.MaterialType;
                if (!string.IsNullOrEmpty(this._manprocedure.Bom.Product.CustomerProductName))
                    this.textEditCustomerPro.Text = this._manprocedure.Bom.Product.CustomerProductName;
                else
                    this.textEditCustomerPro.Text = string.Empty;
;
                // this.textEditCustomProduct.EditValue = this._manprocedure.Bom.Product.CustomerProductName; ;
                if (this._manprocedure.TechonlogyHeader != null)
                {
                    this.buttonEditTechonlogyHeaderid.EditValue = this._manprocedure.TechonlogyHeader.TechonlogyHeadername;
                }
                if (this._manprocedure.MadeProduct != null)
                    this.buttonEditMadeProduct.EditValue = string.IsNullOrEmpty(this._manprocedure.MadeProduct.CustomerProductName) ? this._manprocedure.MadeProduct.ProductName : this._manprocedure.MadeProduct.ProductName + "{" + this._manprocedure.MadeProduct.CustomerProductName + "}";
                else
                    this.buttonEditMadeProduct.EditValue = null;
                this.bindingSource1.DataSource =this.technologydetailsManager.Select(this._manprocedure.TechonlogyHeader);
            }
            else
            {
                this.textEditProductName.EditValue = null;
                this.buttonEditProduct.EditValue = null;
                this.textEditBOMID.EditValue = null;
                // this.textEditBOMType.EditValue = null;
                this.textEditCustomerPro.EditValue = null;
                // this.textEditCustomProduct.EditValue = null;
                this.bindingSource1.DataSource = null;
                this.buttonEditTechonlogyHeaderid.EditValue = null;
                this.buttonEditMadeProduct.EditValue = null;
            }
            this.newChooseContorlCustomer.EditValue = this._manprocedure.Customer;
            if (this._manprocedure.TechonlogyHeader != null)
            {
                this.bindingSource1.DataSource = this.technologydetailsManager.Select(this._manprocedure.TechonlogyHeader);
            }

            //string sql = "SELECT productid,id,productname FROM ManProcedure WHERE (IsProcee IS null OR IsProcee=0) AND (IsCustomerProduct IS NULL OR IsCustomerProduct =0)";
            //this.bindingSourceTechonlogyLeft.DataSource = this.productManager.DataReaderBind<Model.Product>(sql);
            this.bindingSourceTechonlogyLeft.DataSource = this.manProceductManager.Select();
            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    //this.buttonEditProduct.Properties.Buttons[0].Enabled = true;
                    //this.buttonEditProduct.Properties.ReadOnly = false;
                    //this.buttonEditTechonlogyHeaderid.Properties.Buttons[0].Enabled = true;
                    //this.buttonEditTechonlogyHeaderid.Properties.ReadOnly = false;
                    this.textEditBOMID.Properties.ReadOnly = true;
                    //this.textEditBOMType.Properties.ReadOnly=true;
                    //this.textEditCustomProduct.Properties.ReadOnly=true;
                    this.textEditCustomerPro.Properties.ReadOnly = true;
                    this.textEditProductName.Properties.ReadOnly = true;
                    break;

                case "update":
                    //this.buttonEditProduct.Properties.Buttons[0].Enabled = true;
                    //this.buttonEditProduct.Properties.ReadOnly = false;
                    //this.buttonEditTechonlogyHeaderid.Properties.Buttons[0].Enabled = true;
                    //this.buttonEditTechonlogyHeaderid.Properties.ReadOnly = false;
                    this.textEditBOMID.Properties.ReadOnly = true;
                    //this.textEditBOMType.Properties.ReadOnly=true;
                    //this.textEditCustomProduct.Properties.ReadOnly=true;
                    this.textEditCustomerPro.Properties.ReadOnly = true;
                    this.textEditProductName.Properties.ReadOnly = true;
                    break;

                case "view":
                    //this.buttonEditProduct.Properties.Buttons[0].Enabled = false;
                    //this.buttonEditProduct.Properties.ReadOnly = true;
                    //this.buttonEditTechonlogyHeaderid.Properties.Buttons[0].Enabled = false;
                    //this.buttonEditTechonlogyHeaderid.Properties.ReadOnly = true;
                    this.textEditBOMID.Properties.ReadOnly = true;
                    //this.textEditBOMType.Properties.ReadOnly=true;
                    //this.textEditCustomProduct.Properties.ReadOnly=true;
                    this.textEditCustomerPro.Properties.ReadOnly = true;
                    this.textEditProductName.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
           
        }
        protected override void Delete()
        {
            if (this._manprocedure == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.manProceductManager.Delete(this._manprocedure.ManProcedureId);
                this._manprocedure = this.manProceductManager.GetNext(this._manprocedure);
                if (this._manprocedure == null)
                {
                    this._manprocedure = this.manProceductManager.GetLast();
                }
            }
            catch
            {
                throw new Exception("");
            }

            return;

        }
        protected override void MoveFirst()
        {
            this._manprocedure = this.manProceductManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.ManProcedure ManProcedure = this.manProceductManager.GetPrev(this._manprocedure);
            if (ManProcedure == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._manprocedure = ManProcedure;
        }
        protected override void MoveLast()
        {
            this._manprocedure = this.manProceductManager.GetLast();
            if (this._manprocedure != null)
                this._manprocedure.detail = new BL.ProceduresManager().Select(this._manprocedure.Bom);
        }
        protected override void MoveNext()
        {
            Model.ManProcedure ManProcedure = this.manProceductManager.GetNext(this._manprocedure);
            if (ManProcedure == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._manprocedure = ManProcedure;
        }
        protected override bool HasRows()
        {
            return this.manProceductManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.manProceductManager.HasRowsAfter(this._manprocedure);
        }
        protected override bool HasRowsPrev()
        {
            return this.manProceductManager.HasRowsBefore(this._manprocedure);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditProductName, this.textEditBOMID });
        }


      
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._manprocedure == null || this._manprocedure.Bom == null) return;
            XRpRrocedure f = new XRpRrocedure(this._manprocedure.Bom);

            {
                f.ShowPreview();

            }

        }

        private void buttonEditTechonlogyHeaderid_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ProduceManager.Techonlogy.ChooseTechonlogyForm f = new Book.UI.Settings.ProduceManager.Techonlogy.ChooseTechonlogyForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._manprocedure.TechonlogyHeader = f.SelectedItem as Model.TechonlogyHeader;
                if (this._manprocedure.TechonlogyHeader != null)
                {
                    this.buttonEditTechonlogyHeaderid.EditValue = this._manprocedure.TechonlogyHeader;

                    this.bindingSource1.DataSource = technologydetailsManager.Select(this._manprocedure.TechonlogyHeader);

                }
            }

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.Technologydetails> details = this.bindingSource1.DataSource as IList<Model.Technologydetails>;
            if (details == null || details.Count < 1) return;
            Model.Procedures procedures =details[e.ListSourceRowIndex].Procedures   ;
            if (procedures == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnId":                 
                    e.DisplayText = procedures.Id ;
                    break;
                case "gridColumn8":                 
                    e.DisplayText = procedures.WorkHouse == null ? "" : procedures.WorkHouse.Workhousename;
                    break;                 
                    
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceTechonlogyLeft.DataSource = this.manProceductManager.Select();
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
               if (e.ListSourceRowIndex < 0) return;
            IList<Model.ManProcedure> details = this.bindingSourceTechonlogyLeft.DataSource as IList<Model.ManProcedure>;
            if (details == null || details.Count < 1) return;
            Model.BomParentPartInfo detail = details[e.ListSourceRowIndex].Bom;
            if (detail == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnProductId":

                    e.DisplayText = string.IsNullOrEmpty(detail.Product.CustomerProductName) ? detail.Product.ProductName : detail.Product.ProductName+"{"+detail.Product.CustomerProductName+"}";
                    break;

            }
            
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceTechonlogyLeft.Current != null)
            {
                this._manprocedure = this.bindingSourceTechonlogyLeft.Current as Model.ManProcedure;
                this.action = "view";
                this.Refresh();
            }
           

        }

        private void buttonEditMadeProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
               Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
               if (f.ShowDialog(this) == DialogResult.OK)
               {
                   this._madeProduct = f.SelectedItem as Model.Product;
                   if (this._madeProduct != null)
                   {
                       this.buttonEditMadeProduct.Text = string.IsNullOrEmpty(this._madeProduct.CustomerProductName) ? this._madeProduct.ProductName : this._madeProduct.ProductName +"{"+this._madeProduct.CustomerProductName+"}";
                   }
               }

        }

        private void barButtonItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

           // _manprocedure
        }



    }
}