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

namespace Book.UI.Settings.BasicData.Products
{
    public partial class ProductClassifyForm : BaseEditForm
    {
        Model.ProductClassify _productClassify;
        BL.ProductClassifyManager _manager = new Book.BL.ProductClassifyManager();
        BL.ProductClassifyDetailManager _detailManager = new Book.BL.ProductClassifyDetailManager();
        int LastFlag = 0;

        public ProductClassifyForm()
        {
            InitializeComponent();
            this.invalidValueExceptions.Add(Model.ProductClassify.PRO_KeyWord, new AA("关键字不能为空", this.txt_KeyWord));
            this.invalidValueExceptions.Add(Model.ProductClassify.PRO_ProductClassifyDate, new AA(Properties.Resources.DateNotNull, this.dateEdit1));

            this.NCCEmployee.Choose = new Employees.ChooseEmployee();

            this.action = "view";
        }

        public ProductClassifyForm(Model.ProductClassify ProductClassify)
            : this()
        {
            if (ProductClassify == null)
                throw new ArithmeticException("invoiceid");
            this._productClassify = ProductClassify;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public ProductClassifyForm(Model.ProductClassify ProductClassify, string action)
            : this()
        {
            this._productClassify = ProductClassify;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._productClassify = new Book.Model.ProductClassify();
            this._productClassify.ProductClassifyId = Guid.NewGuid().ToString();
            //this.NCCEmployee.EditValue = BL.V.ActiveOperator.Employee;
            //this.dateEdit1.EditValue = DateTime.Now;
            this._productClassify.Employee = BL.V.ActiveOperator.Employee;
            this._productClassify.ProductClassifyDate = DateTime.Now;
            this._productClassify.Details = new List<Model.ProductClassifyDetail>();
        }

        protected override void Delete()
        {
            if (this._productClassify == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._manager.Delete(this._productClassify.ProductClassifyId);

            this._productClassify = this._manager.GetNext(this._productClassify);
            if (this._productClassify == null)
            {
                this._productClassify = this._manager.GetLast();
            }
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._productClassify = this._manager.GetLast();
        }

        protected override void MoveFirst()
        {
            this._productClassify = this._manager.GetFirst();
        }

        protected override void MovePrev()
        {
            Model.ProductClassify model = this._manager.GetPrev(this._productClassify);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._productClassify = model;
        }

        protected override void MoveNext()
        {
            Model.ProductClassify model = this._manager.GetNext(this._productClassify);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._productClassify = model;
        }

        protected override bool HasRows()
        {
            return this._manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._manager.HasRowsAfter(this._productClassify);
        }

        protected override bool HasRowsPrev()
        {
            return this._manager.HasRowsBefore(this._productClassify);
        }

        public override void Refresh()
        {
            if (this._productClassify == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._productClassify = this._manager.GetDetail(this._productClassify.ProductClassifyId);
                }
            }

            //初始化控件
            this.txt_KeyWord.EditValue = this._productClassify.KeyWord;
            this.dateEdit1.EditValue = this._productClassify.ProductClassifyDate;
            this.NCCEmployee.EditValue = this._productClassify.Employee;

            this.bindingSource1.DataSource = this._productClassify.Details;

            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }

        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (this.dateEdit1.EditValue != null)
                this._productClassify.ProductClassifyDate = this.dateEdit1.DateTime;
            this._productClassify.KeyWord = this.txt_KeyWord.Text;
            this._productClassify.EmployeeId = (this.NCCEmployee.EditValue == null ? null : (this.NCCEmployee.EditValue as Model.Employee).EmployeeId);

            switch (this.action)
            {
                case "insert":
                    if (this._manager.IsExistsKeyWordForInsert(this._productClassify))
                        throw new Helper.MessageValueException("关键字：" + this._productClassify.KeyWord + " 已存在");
                    this._manager.Insert(this._productClassify);
                    break;
                case "update":
                    if (this._manager.IsExistsKeyWordForUpdate(this._productClassify))
                        throw new Helper.MessageValueException("关键字：" + this._productClassify.KeyWord + " 已存在");
                    this._manager.Update(this._productClassify);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new ROProductClassify(this._productClassify);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Model.Product item in ChooseProductForm.ProductList)
                {
                    if (this._productClassify.Details.Any(D => D.ProductId == item.ProductId))
                    {
                        MessageBox.Show("商品：" + item.ProductName + " 已添加", this.Text, MessageBoxButtons.OK);
                        continue;
                    }
                    else
                    {
                        Model.ProductClassifyDetail detail = this._detailManager.GetByProductId(item.ProductId);
                        if (detail == null)
                        {
                            detail = new Book.Model.ProductClassifyDetail();
                            detail.ProductClassifyDetailId = Guid.NewGuid().ToString();
                            detail.ProductClassifyId = this._productClassify.ProductClassifyId;
                            detail.ProductId = item.ProductId;
                            detail.Product = item;
                            detail.Inumber = (this._productClassify.Details.Count + 1).ToString();
                            this._productClassify.Details.Add(detail);
                        }
                        else
                        {
                            MessageBox.Show("商品：" + item.ProductName + " 已属于关键字 " + detail.ProductClassify.KeyWord, this.Text, MessageBoxButtons.OK);
                            continue;
                        }
                    }
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this._productClassify.Details.Remove(this.bindingSource1.Current as Model.ProductClassifyDetail);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListProductClassify f = new ListProductClassify();
            f.ShowDialog(this);
        }
    }
}