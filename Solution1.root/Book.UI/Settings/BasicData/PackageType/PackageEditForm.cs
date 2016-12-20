using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.PackageType
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：PackageEditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-27
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class PackageEditForm :BaseEditForm
    {
        private Model.PackageDetails _packageDetails;
        private BL.PackageDetailsManager packageDetailsManager = new Book.BL.PackageDetailsManager();
        public PackageEditForm()
        {
            InitializeComponent();
          //  this.buttonEditProduct.Choose =new  Settings.BasicData.Products.ChooseForm as Model.Product;
        }
        public PackageEditForm(Book.Model.PackageDetails packageDetails):this()
        {
            this._packageDetails = packageDetails;           
            this.action = "update";
        }


        public PackageEditForm(Book.Model.PackageDetails packageDetails, string action)
            : this()
        {
            //if(cmpy == null)
            //    throw new ArithmeticException();
            this._packageDetails = packageDetails;

            this.action = action;
        }
        public override object EditedItem
        {
            get
            {
                return this._packageDetails;
            }
        }

        protected override void Delete()
        {
            if (this._packageDetails == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.packageDetailsManager.Delete(this._packageDetails.PackageDetailsId);

          //  this._packageDetails = this.packageDetailsManager.GetNext(this._packageDetails);

            if (this._packageDetails == null)
            {
              //  this._packageDetails = this.packageDetailsManager.GetLast();
            }
        }

        protected override void MoveFirst()
        {
            //this._packageDetails = this.packageDetailsManager.GetFirst();
        }
        protected override void MoveLast()
        {
            //if (this._packageDetails == null)
             //   this._packageDetails = this.dutyManager.GetLast();
        }
        
        protected override void AddNew()
        {
            this._packageDetails = new Book.Model.PackageDetails();
        }

        protected override void MoveNext()
        {
            //Model.PackageDetails packageDetails = this.packageDetailsManager.GetNext(this._packageDetails);
            //if (_packageDetails == null)
            //    throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            //this._packageDetails = packageDetails;
        }

        protected override void MovePrev()
        {
            //Model.PackageDetails packageDetails = this.packageDetailsManager.GetPrev(this._packageDetails);
            //if (packageDetails == null)
            //    throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            //this._packageDetails = packageDetails;
        }

        protected override bool HasRows()
        {
            return this.packageDetailsManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return true ;
            //return this.packageDetailsManager.HasRowsAfter(this._packageDetails);
        }

        protected override bool HasRowsPrev()
        {
            return true ;
            //return this.packageDetailsManager.HasRowsBefore(this._packageDetails);
        }

        public override void Refresh()
        {
            if (this._packageDetails == null)
            {
                this._packageDetails = new Book.Model.PackageDetails();
                this.action = "insert";
            }

            //this.textEditId.Text = string.IsNullOrEmpty(this.duty.Id) ? this.duty.DutyId : this.duty.Id;
            //this.textEditDutyName.Text = this.duty.DutyName;
            //this.memoEditDutyNote.Text = this.duty.DutyNote;
            this.buttonEditProduct.EditValue = this._packageDetails.Product;
            this.calcEditCount.EditValue = this._packageDetails.Quantity;
            this.CalsumeRate.EditValue = this._packageDetails.ConsumeRate;
            this.memoEditDescription.EditValue = this._packageDetails.Description;

            switch (this.action)
            {
                case "insert":
                    this.buttonEditProduct.Properties.Buttons[0].Enabled = true ;
                    this.buttonEditProduct.Properties.ReadOnly = false;
                    this.calcEditCount.Properties.ReadOnly = false;
                    this.CalsumeRate.Properties.ReadOnly = false;
                    this.memoEditDescription.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.buttonEditProduct.Properties.Buttons[0].Enabled = true ;
                    this.buttonEditProduct.Properties.ReadOnly = false;
                    this.calcEditCount.Properties.ReadOnly = false;
                    this.CalsumeRate.Properties.ReadOnly = false;
                    this.memoEditDescription.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.buttonEditProduct.Properties.Buttons[0].Enabled = false;
                    this.buttonEditProduct.Properties.ReadOnly = true ;
                  
                    this.calcEditCount.Properties.ReadOnly = true;
                    this.CalsumeRate.Properties.ReadOnly = true;
                    this.memoEditDescription.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override void Save()
        {
           // this._packageDetails.ProductId = this._packageDetails. ;
            //if(this._packageDetails.Product!=null )
            //this._packageDetails.ProductId= (this.buttonEditProduct.EditValue as Model.Product).ProductId;
            this._packageDetails.Quantity = this.calcEditCount.EditValue == null ? 0 :Convert.ToInt32(this.calcEditCount.EditValue);
            this._packageDetails.ConsumeRate = this.CalsumeRate.EditValue == null ? 0 : Convert.ToDouble(this.CalsumeRate.EditValue);
            this._packageDetails.Description = this.memoEditDescription.Text;

            switch (this.action)
            {
                case "insert":
                    this.packageDetailsManager.Insert(this._packageDetails);
                    break;

                case "update":
                  // this.duty.DutyId = oldId;
                    this.packageDetailsManager.Update(this._packageDetails);
                   break;
            }
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.buttonEditProduct,this.calcEditCount,this.CalsumeRate,this.memoEditDescription, this });
        }

        private void buttonEdit1_Click(object sender, EventArgs e)
        {

            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as DevExpress.XtraEditors.ButtonEdit).EditValue = f.SelectedItem;

                if (f.SelectedItem != null)
                {
                    // this.textEditId.Text = (this.newChooseContorlProductType.EditValue as Model.ProductCategory).Id;
                    //this.product.ProductCategory = (this.newChooseContorlProductType.EditValue as Model.ProductCategory);                     
                    this._packageDetails.Product = f.SelectedItem as Model.Product;
                    this._packageDetails.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    this.buttonEditProduct.Text = (f.SelectedItem as Model.Product).ProductName;
                }
            }
        }
    }
}