using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
namespace Book.UI.produceManager.MouldCategory
{
    public partial class ProductMouldtestEditForm : BaseEditForm
    {

        #region 变量对象定义
        BL.ProductMouldTestManager _productMouldTestManager = new Book.BL.ProductMouldTestManager();
        private BL.ProductMouldTestDetailManager _productMouldTestDetailManager = new Book.BL.ProductMouldTestDetailManager();
        BL.EmployeeManager _employeeManager = new Book.BL.EmployeeManager();
        BL.AreaCategoryManager _areCategoryManager = new Book.BL.AreaCategoryManager();
        BL.ProductMouldManager _productMouldManager = new Book.BL.ProductMouldManager();
        Model.ProductMouldTest _productMouldTest;
        BL.PronoteMachineManager _pronoteMachineManager = new Book.BL.PronoteMachineManager();
        private IList<Model.ProductMould> list = new List<Model.ProductMould>();
        private IList<Model.ProductMould> plist = new List<Model.ProductMould>();

        #endregion

        public ProductMouldtestEditForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.ProductMouldTest.PRO_Id, new AA(Properties.Resources.NunsIsExists, this.textEditId as Control));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_Id, new AA(Properties.Resources.NumsIsNotNull, this.textEditId as Control));

            this.action = "insert";
        }

        public ProductMouldtestEditForm(string s)
            : this()
        {

        }

        protected override void AddNew()
        {
            this._productMouldTest = new Book.Model.ProductMouldTest();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            Model.ProductMouldTest productmould = this.bindingSourceProductMouldTest.Current as Model.ProductMouldTest;
            this._productMouldTestManager.Delete(this._productMouldTest.ProductMouldTestId);
        }

        protected override void Save()
        {
            this._productMouldTest.Id = this.textEditId.Text;
            if (newChooseContorlAreaCategoryId.EditValue != null)
            {
                this._productMouldTest.AreaCategory = newChooseContorlAreaCategoryId.EditValue as Model.AreaCategory;
                this._productMouldTest.AreaCategoryId = this._productMouldTest.AreaCategory.AreaCategoryId;
            }

            //if (newChooseContorlEmployee0Id.EditValue != null)
            //{
            //    this._productMouldTest.Employee = newChooseContorlEmployee0Id.EditValue as Model.Employee;
            //    this._productMouldTest.EmployeeId = this._productMouldTest.Employee.EmployeeId;
            //}

            //if (newChooseContorlEmployeeId.EditValue != null)
            //{
            //    this._productMouldTest.Employee0 = newChooseContorlEmployeeId.EditValue as Model.Employee;
            //    this._productMouldTest.Employee0Id = this._productMouldTest.Employee0.EmployeeId;
            //}

            //if (newChooseContorlSupplierId.EditValue != null)
            //{
            //    this._productMouldTest.Supplier = newChooseContorlSupplierId.EditValue as Model.Supplier;
            //    this._productMouldTest.SupplierId = this._productMouldTest.Supplier.SupplierId;
            //}

            if (this.lookUpEditPronoteMachine.EditValue != null)
            {
                this._productMouldTest.PronoteMachineId = this.lookUpEditPronoteMachine.EditValue.ToString();
            }



            this._productMouldTest.MouldId = this.buttonEditMould.Text;

            this._productMouldTest.InFactoryDate = this.dateEditInFactoryDate.Text == "" ? global::Helper.DateTimeParse.NullDate : this.dateEditInFactoryDate.DateTime;

            this._productMouldTest.OutFactoryDate = this.dateEditOutFactoryDate.Text == "" ? global::Helper.DateTimeParse.NullDate : this.dateEditOutFactoryDate.DateTime;

            this._productMouldTest.ProductMouldTestDate = this.dateEditProductMouldTestDate.Text == "" ? global::Helper.DateTimeParse.NullDate : this.dateEditProductMouldTestDate.DateTime;

            this._productMouldTest.TestCause = this.memoEditTestCause.Text;

            switch (this.action)
            {
                case "insert":
                    this._productMouldTestManager.Insert(this._productMouldTest);
                    return;
                case "update":
                    this._productMouldTestManager.Update(this._productMouldTest);
                    return;
            }

        }

        public override void Refresh()
        {

            if (this._productMouldTest == null)
            {
                this._productMouldTest = new Book.Model.ProductMouldTest();
                this.action = "isnert";
            }
            else
            {
                this._productMouldTest = this._productMouldTestManager.Get(this._productMouldTest.ProductMouldTestId);
                if (this._productMouldTest == null)
                {
                    this._productMouldTest = new Book.Model.ProductMouldTest();
                    // this.action = "isnert";
                }
            }

            this.textEditId.Text = this._productMouldTest.Id;

            this.newChooseContorlAreaCategoryId.EditValue = this._productMouldTest.AreaCategory;

            //this.newChooseContorlEmployee0Id.EditValue = this._productMouldTest.Employee;

            //this.newChooseContorlEmployeeId.EditValue = this._productMouldTest.Employee0;

            //this.newChooseContorlSupplierId.EditValue = this._productMouldTest.Supplier;

            this.lookUpEditPronoteMachine.EditValue = this._productMouldTest.PronoteMachineId;



            if (this._productMouldTest.InFactoryDate != null)
                this.dateEditInFactoryDate.DateTime = this._productMouldTest.InFactoryDate.Value;
            if (this._productMouldTest.OutFactoryDate != null)
                this.dateEditOutFactoryDate.DateTime = this._productMouldTest.OutFactoryDate.Value;

            this.memoEditTestCause.Text = this._productMouldTest.TestCause;

            this.bindingSourceProductMouldTest.DataSource = this._productMouldTestManager.Select();
            this.bindingSourcePronteMachine.DataSource = this._pronoteMachineManager.Select();

            this.list = this._productMouldManager.SelectProductMouldByProductMouldTestId(this._productMouldTest.ProductMouldTestId);
            string text = string.Empty;
            foreach (Model.ProductMould item in list)
            {
                if (text == "")
                    text = item.MouldName;
                else
                    text += "," + item.MouldName;
            }

            this.buttonEditMould.Text = text;

            base.Refresh();


        }

        private void ProductMouldtestEditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();

            this.bindingSourceMould.DataSource = this._productMouldManager.Select();
            //this.newChooseContorlSupplierId.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlEmployee0Id.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployeeId.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAreaCategoryId.Choose = new Book.UI.Settings.BasicData.AreaCategory.ChooseAreaCategory();
            //this.newChooseContorlSupplierId.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();

            this.dateEditProductMouldTestDate.DateTime = System.DateTime.Today;
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            this._productMouldTest = this.bindingSourceProductMouldTest.Current as Model.ProductMouldTest;
            if (this._productMouldTest != null)
            {
                this.action = "view";
                this.Refresh();
            }
        }


        private void buttonEditMould_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductMould productMould = new ChooseProductMould(list, this._productMouldTest.ProductMouldTestId);
            if (productMould.ShowDialog(this) == DialogResult.OK)
            {
                this.plist = productMould.SelectItem;
                string text = string.Empty;
                foreach (Model.ProductMould item in plist)
                {
                    if (text == "")
                        text = item.MouldName;
                    else
                        text += "," + item.MouldName;

                    Model.ProductMouldTestDetail productMouldTestDetail = new Book.Model.ProductMouldTestDetail();
                    productMouldTestDetail.ProductMouldTestDetailId = Guid.NewGuid().ToString();
                    productMouldTestDetail.ProductMouldTestId = this._productMouldTest.ProductMouldTestId;
                    productMouldTestDetail.MouldId = item.MouldId;
                    productMouldTestDetail.InsertTime = System.DateTime.Now;
                    this._productMouldTestDetailManager.Insert(productMouldTestDetail);
                }

                buttonEditMould.Text = text;
            }
        }


    }
}