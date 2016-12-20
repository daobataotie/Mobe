using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using System.IO;
using System.Linq;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class ProductsMouldTestEditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        #region 变量对象定义

        public Model.ProductMould _ProductMould;
        BL.ProductMouldManager _ProductMouldManager = new Book.BL.ProductMouldManager();
        BL.PronoteMachineManager _PronoteMachineManager = new Book.BL.PronoteMachineManager();
        BL.ProductMaterialManager _ProductMaterialManager = new Book.BL.ProductMaterialManager();
        public Model.MouldCategory _MouldCategory;
        BL.MouldCategoryManager _MouldCategoryManager = new Book.BL.MouldCategoryManager();
        public Model.ProductMouldTest _ProductMouldTest;
        public Model.ProductCategory _ProductCategory;
        public BL.ProductMouldTestManager _ProductMouldTestManager = new Book.BL.ProductMouldTestManager();
        public BL.ProductCategoryManager _ProductCategoryManager = new Book.BL.ProductCategoryManager();

        string _Frompc = string.Empty;
        string _Saveseverpath = string.Empty;
        IList<Model.ProductMouldTest> productMouldTestList = new List<Model.ProductMouldTest>();

        IList<FilesAccessoriesHelper> _FilesAccessoriesHelper = new List<FilesAccessoriesHelper>();

        int index;
        #endregion

        public ProductsMouldTestEditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_Id, new AA(Properties.Resources.NumsIsNotNull, this.TxtMouldTestId));
            this.invalidValueExceptions.Add(Model.ProductMouldTest.PRO_Id, new AA("Id已存在", this.TxtMouldTestId));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_MouldId, new AA("模具不能為空", this.LookMould));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_SupplierId, new AA("廠商不能為空", this.NccSupplier));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_ProductMaterialId, new AA("成品材質不能為空", this.LookPrductMaterial));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_ProductCategoryId, new AA("成品類別不能為空", this.lookUpEditProductCategory));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_PronoteMachineId, new AA("機台不能為空", this.LookTestMouldMachine));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_ProductMouldTestDate, new AA(Properties.Resources.DateIsNull, this.DateMouldTest));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_InFactoryDate, new AA("入廠" + Properties.Resources.DateIsNull, this.DateInTime));
            //this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_OutFactoryDate, new AA("出場" + Properties.Resources.DateIsNull, this.DateOutTime));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_MouldMount, new AA("成品穴數不能為空", this.ComboxMouldMount));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_EmployeeId, new AA("試模人員不能為空", this.NccMouldTestPerson));
            this.requireValueExceptions.Add(Model.ProductMouldTest.PRO_MouldWeight, new AA("成品重量不能為空", this.TxtProductWeigth));


            //模具、机台、成品材质、成品类别页面加载绑定
            this.bindingSourceMould.DataSource = this._ProductMouldManager.Select();
            this.bindingSourcePronteMachine.DataSource = this._PronoteMachineManager.Select();
            this.bindingMouldMaterial.DataSource = this._MouldCategoryManager.Select();

            /*试模人员、射出人员、强化人员、安规、清晰、光学
            雷射人员、冲击人员、内容人员、开发备注人员、主管1、主管2绑定*/
            this.NccSupplier.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
            this.NccMouldTestPerson.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.NccMouldInjectionPerson.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.NccMouldStrengthenPerson.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.NccFourWayPerson.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.NccImpactPerson.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.NccContent.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.NccDevelopDetailPerson.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.NccManagerOne.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.NccManagerTwo.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();

            if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["MouldTestPath"] != null)
            {
                this._Saveseverpath = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["MouldTestPath"].Value;
            }

            //成品类别
            this.bindingSourceProductCategory.DataSource = (new BL.ProductMouldCategoryManager()).Select();

            this.StartPosition = FormStartPosition.CenterParent;

            this.productMouldTestList = this._ProductMouldTestManager.SelectOrderByMouldId();

            this.action = "view";
        }


        int sign = 0;
        string mouldId = string.Empty;
        public ProductsMouldTestEditForm(string s)
            : this()
        {
            mouldId = s;
            this._ProductMouldTest = this._ProductMouldTestManager.SelectByMouldId(s);
            this.sign = 1;
            this.action = "view";
        }

        protected override void MoveLast()
        {
            if (this.sign == 1)
            {
                this.sign = 0;
                return;
            }
            if (this.productMouldTestList != null && this.productMouldTestList.Count > 0)
                this._ProductMouldTest = this.productMouldTestList[this.productMouldTestList.Count - 1];
        }

        protected override void MoveFirst()
        {
            this._ProductMouldTest = this.productMouldTestList[0];
        }

        protected override void MovePrev()
        {
            //Model.ProductMouldTest pro = this._ProductMouldTestManager.GetPrev(this._ProductMouldTest);
            Model.ProductMouldTest pro = this.productMouldTestList[this.productMouldTestList.IndexOf(this.productMouldTestList.Where(p => p.ProductMouldTestId == this._ProductMouldTest.ProductMouldTestId).ToList<Model.ProductMouldTest>().Last<Model.ProductMouldTest>()) - 1];
            if (pro == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._ProductMouldTest = pro;
        }

        protected override void MoveNext()
        {
            //Model.ProductMouldTest pro = this._ProductMouldTestManager.GetNext(this._ProductMouldTest);
            Model.ProductMouldTest pro = this.productMouldTestList[this.productMouldTestList.IndexOf(this.productMouldTestList.Where(p => p.ProductMouldTestId == this._ProductMouldTest.ProductMouldTestId).ToList<Model.ProductMouldTest>().Last<Model.ProductMouldTest>()) + 1];
            if (pro == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._ProductMouldTest = pro;
        }

        protected override bool HasRows()
        {
            return this._ProductMouldTestManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            //return this._ProductMouldTestManager.HasRowsBefore(this._ProductMouldTest);
            if (this.productMouldTestList.IndexOf(this.productMouldTestList.Where(p => p.ProductMouldTestId == this._ProductMouldTest.ProductMouldTestId).ToList<Model.ProductMouldTest>().Last<Model.ProductMouldTest>()) > 0)
                return true;
            return false;
        }

        protected override bool HasRowsNext()
        {
            //return this._ProductMouldTestManager.HasRowsAfter(this._ProductMouldTest);
            if (this.productMouldTestList.IndexOf(this.productMouldTestList.Where(p => p.ProductMouldTestId == this._ProductMouldTest.ProductMouldTestId).ToList<Model.ProductMouldTest>().Last<Model.ProductMouldTest>()) < this.productMouldTestList.Count - 1)
                return true;
            return false;
        }

        protected override void AddNew()
        {
            this._ProductMouldTest = new Book.Model.ProductMouldTest();
            this._ProductMouldTest.ProductMouldTestId = this._ProductMouldTestManager.GetId();
            this._ProductMouldTest.ProductMouldTestDate = DateTime.Now;

            if (!string.IsNullOrEmpty(mouldId))
                this._ProductMouldTest.MouldId = mouldId;
        }

        public override void Refresh()
        {
            if (this._ProductMouldTest == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            //else
            //{
            //    if (this.action == "view")
            //        this._ProductMouldTest = this._ProductMouldTestManager.Get(this._ProductMouldTest.ProductMouldTestId);
            //}
            this.TxtMouldTestId.Text = this._ProductMouldTest.ProductMouldTestId;
            this.TxtProductWeigth.Text = this._ProductMouldTest.MouldWeight.ToString();

            this.LookPrductMaterial.EditValue = this._ProductMouldTest.ProductMaterialId;
            this.LookMould.EditValue = this._ProductMouldTest.MouldId;
            this.LookTestMouldMachine.EditValue = this._ProductMouldTest.PronoteMachineId;

            this.NccSupplier.EditValue = this._ProductMouldTest.Supplier;
            this.ComboxInstallType.EditValue = this._ProductMouldTest.InstallType == null ? "" : this._ProductMouldTest.InstallType.ToString();
            this.CheckMouldInjectionType.Checked = this._ProductMouldTest.MouldInjectionType.HasValue ? this._ProductMouldTest.MouldInjectionType.Value : false;
            this.CheckMouldStrengthenType.Checked = this._ProductMouldTest.MouldStrengthenType.HasValue ? this._ProductMouldTest.MouldStrengthenType.Value : false;
            this.CheckClearType.Checked = this._ProductMouldTest.ClearType.HasValue ? this._ProductMouldTest.ClearType.Value : false;
            this.CheckOpticsType.Checked = this._ProductMouldTest.OpticsType.HasValue ? this._ProductMouldTest.OpticsType.Value : false;
            this.CheckLaserType.Checked = this._ProductMouldTest.LaserType.HasValue ? this._ProductMouldTest.LaserType.Value : false;
            this.CheckImpaceType.Checked = this._ProductMouldTest.ImpaceType.HasValue ? this._ProductMouldTest.ImpaceType.Value : false;
            this.CheckContentType.Checked = this._ProductMouldTest.Content.HasValue ? this._ProductMouldTest.Content.Value : false;
            this.CheckContentBool.Checked = this._ProductMouldTest.ContentType.HasValue ? this._ProductMouldTest.ContentType.Value : false;



            this.RadioClearBool.SelectedIndex = this._ProductMouldTest.ClearBool.HasValue ? (this._ProductMouldTest.ClearBool.HasValue ? 0 : 1) : 0;
            this.RadioOpticsBool.SelectedIndex = this._ProductMouldTest.OpticsBool.HasValue ? (this._ProductMouldTest.OpticsBool.HasValue ? 0 : 1) : 0;
            this.RadioLaserBool.SelectedIndex = this._ProductMouldTest.LaserBool.HasValue ? (this._ProductMouldTest.LaserBool.HasValue ? 0 : 1) : 0;
            this.RadioInstallBool.SelectedIndex = this._ProductMouldTest.InstallBool.HasValue ? (this._ProductMouldTest.InstallBool.HasValue ? 0 : 1) : 0;
            this.RadioImpactBool.SelectedIndex = this._ProductMouldTest.ImpactBool.HasValue ? (this._ProductMouldTest.ImpactBool.HasValue ? 0 : 1) : 0;

            this.ComboxMouldMount.Text = this._ProductMouldTest.MouldMount == null ? "" : this._ProductMouldTest.MouldMount.ToString();


            if (!string.IsNullOrEmpty(this._ProductMouldTest.MouldSize))
            {
                string[] ckg = new string[3];
                ckg = this._ProductMouldTest.MouldSize.Split('*');
                this.TxtMouldLength.Text = ckg[0];
                this.TxtMouldWidth.Text = ckg[1];
                this.TxtMouldHeight.Text = ckg[2];
            }
            else
            {
                this.TxtMouldLength.Text = "";
                this.TxtMouldWidth.Text = "";
                this.TxtMouldHeight.Text = "";
            }

            if (!string.IsNullOrEmpty(this._ProductMouldTest.ContentDetail))
            {
                string[] cckd = new string[3];
                cckd = this._ProductMouldTest.ContentDetail.Split('*');
                this.TxtContentDetail1.Text = cckd[0];
                this.TxtContentDetail2.Text = cckd[1];
                this.TxtContentDetail3.Text = cckd[2];
            }
            else
            {
                this.TxtContentDetail1.Text = "";
                this.TxtContentDetail2.Text = "";
                this.TxtContentDetail3.Text = "";
            }

            this.TxtDevelopDetail.Text = this._ProductMouldTest.DevelopRemarks;
            this.DateMouldTest.EditValue = this._ProductMouldTest.ProductMouldTestDate;
            this.DateInTime.EditValue = this._ProductMouldTest.InFactoryDate;
            this.DateOutTime.EditValue = this._ProductMouldTest.OutFactoryDate;
            this.DateMouldInjection.EditValue = this._ProductMouldTest.MouldInjectionDate;
            this.DateMouldStrengthen.EditValue = this._ProductMouldTest.MouldStrengthenDate;
            this.DateFourWayDate.EditValue = this._ProductMouldTest.FourWayDate;
            this.DateImpact.EditValue = this._ProductMouldTest.ImpactDate;
            this.DateContent.EditValue = this._ProductMouldTest.ContentDate;
            this.DateDevelopDetail.EditValue = this._ProductMouldTest.DevelopRemarksDate;

            this.NccMouldInjectionPerson.EditValue = this._ProductMouldTest.Emp_Employee;
            this.NccMouldStrengthenPerson.EditValue = this._ProductMouldTest.Emp_Employee2;
            this.NccMouldTestPerson.EditValue = this._ProductMouldTest.Employee;
            this.NccDevelopDetailPerson.EditValue = this._ProductMouldTest.Emp_Employee6;
            this.NccFourWayPerson.EditValue = this._ProductMouldTest.Emp_Employee3;
            this.NccImpactPerson.EditValue = this._ProductMouldTest.Emp_Employee4;
            this.NccContent.EditValue = this._ProductMouldTest.Emp_Employee5;
            this.NccManagerOne.EditValue = this._ProductMouldTest.Emp_Employee7;
            this.NccManagerTwo.EditValue = this._ProductMouldTest.Emp_Employee8;
            this.meInjectionNote.EditValue = this._ProductMouldTest.InjectionNote;
            this.meStrengThenNote.EditValue = this._ProductMouldTest.StrengThenNote;
            this.meQualityControlNote.EditValue = this._ProductMouldTest.QualityControlNote;
            this.lookUpEditProductCategory.EditValue = this._ProductMouldTest.ProductCategoryId;
            this._FilesAccessoriesHelper.Clear();

            //通过主键查看附件
            if (Directory.Exists(this._Saveseverpath + "\\" + this._ProductMouldTest.ProductMouldTestId))
            {
                string[] filenames = Directory.GetFiles(this._Saveseverpath + "\\" + this._ProductMouldTest.ProductMouldTestId);

                string[] eachDesc = null;
                if (this._ProductMouldTest.EachMouldDesc != null)
                    eachDesc = this._ProductMouldTest.EachMouldDesc.Split('|');
                foreach (string fn in filenames)
                {
                    FilesAccessoriesHelper fss = new FilesAccessoriesHelper();
                    fss.Filename = fn.Substring(fn.LastIndexOf("\\") + 1);
                    fss.FileFullname = fn;
                    FileInfo fi = new FileInfo(fss.FileFullname);
                    fss.Filesize = fi.Length.ToString();

                    if (eachDesc != null && eachDesc.Length > 0)
                    {
                        foreach (string desc in eachDesc)
                        {
                            if (desc.Substring(0, desc.LastIndexOf('\\')) == fss.Filename)
                                fss.Description = desc.Substring(desc.LastIndexOf('\\') + 1);
                        }
                    }
                    this._FilesAccessoriesHelper.Add(fss);
                }
            }
            this.bindingSourceFiles.DataSource = this._FilesAccessoriesHelper;
            this.gridControl1.RefreshDataSource();
            base.Refresh();

            this.TxtMouldTestId.Enabled = false;

        }

        protected override void Delete()
        {
            if (this._ProductMouldTest == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._ProductMouldTestManager.Delete(this._ProductMouldTest.ProductMouldTestId);

            index = this.productMouldTestList.IndexOf(this.productMouldTestList.Where(p => p.ProductMouldTestId == this._ProductMouldTest.ProductMouldTestId).ToList<Model.ProductMouldTest>().Last<Model.ProductMouldTest>());
            this.productMouldTestList.RemoveAt(index);
            this._ProductMouldTest = this.productMouldTestList[index];
            if (this._ProductMouldTest == null)
                this._ProductMouldTest = this.productMouldTestList[this.productMouldTestList.Count - 1];

        }

        protected override void Save()
        {
            this._ProductMouldTest.Id = this.TxtMouldTestId.Text;
            if (!string.IsNullOrEmpty(TxtProductWeigth.Text))
                this._ProductMouldTest.MouldWeight = Convert.ToDouble(this.TxtProductWeigth.Text);
            if (this.TxtMouldLength.Text != "" && this.TxtMouldWidth.Text != "" && this.TxtMouldHeight.Text != "")
                this._ProductMouldTest.MouldSize = this.TxtMouldLength.Text + "*" + this.TxtMouldWidth.Text + "*" + TxtMouldHeight.Text;
            this._ProductMouldTest.InstallBool = this.RadioInstallBool.SelectedIndex == 0 ? true : false;
            this._ProductMouldTest.ClearBool = this.RadioClearBool.SelectedIndex == 0 ? true : false;
            this._ProductMouldTest.OpticsBool = this.RadioOpticsBool.SelectedIndex == 0 ? true : false;
            this._ProductMouldTest.LaserBool = this.RadioLaserBool.SelectedIndex == 0 ? true : false;
            this._ProductMouldTest.ImpactBool = this.RadioImpactBool.SelectedIndex == 0 ? true : false;
            this._ProductMouldTest.InstallType = this.ComboxInstallType.EditValue == null ? null : this.ComboxInstallType.EditValue.ToString();

            if (string.IsNullOrEmpty(TxtContentDetail1.Text) || string.IsNullOrEmpty(TxtContentDetail2.Text) || string.IsNullOrEmpty(TxtContentDetail3.Text))
            {
                this._ProductMouldTest.ContentDetail = "";
            }
            else
            {
                this._ProductMouldTest.ContentDetail = this.TxtContentDetail1.Text + "*" + this.TxtContentDetail2.Text + "*" + this.TxtContentDetail3.Text;
            }
            if (this.TxtDevelopDetail.EditValue != null)
                this._ProductMouldTest.DevelopRemarks = this.TxtDevelopDetail.EditValue.ToString();

            this._ProductMouldTest.MouldId = this.LookMould.EditValue == null ? "" : this.LookMould.EditValue.ToString();
            this._ProductMouldTest.PronoteMachineId = this.LookTestMouldMachine.EditValue == null ? "" : this.LookTestMouldMachine.EditValue.ToString();
            this._ProductMouldTest.ProductMaterialId = this.LookPrductMaterial.EditValue == null ? "" : this.LookPrductMaterial.EditValue.ToString();

            if (this.DateMouldTest.EditValue != null)
                this._ProductMouldTest.ProductMouldTestDate = this.DateMouldTest.DateTime;
            if (this.DateInTime.EditValue != null)
                this._ProductMouldTest.InFactoryDate = this.DateInTime.DateTime;
            if (this.DateOutTime.EditValue != null)
                this._ProductMouldTest.OutFactoryDate = this.DateOutTime.DateTime;
            if (this.DateMouldInjection.EditValue != null)
                this._ProductMouldTest.MouldInjectionDate = this.DateMouldInjection.DateTime;
            if (this.DateMouldStrengthen.EditValue != null)
                this._ProductMouldTest.MouldStrengthenDate = this.DateMouldStrengthen.DateTime;
            if (this.DateFourWayDate.EditValue != null)
                this._ProductMouldTest.FourWayDate = this.DateFourWayDate.DateTime;
            if (this.DateImpact.EditValue != null)
                this._ProductMouldTest.ImpactDate = this.DateImpact.DateTime;
            if (DateContent.EditValue != null)
                this._ProductMouldTest.ContentDate = this.DateContent.DateTime;
            if (DateDevelopDetail.EditValue != null)
                this._ProductMouldTest.DevelopRemarksDate = this.DateDevelopDetail.DateTime;

            this._ProductMouldTest.MouldInjectionType = this.CheckMouldInjectionType.Checked;
            this._ProductMouldTest.MouldStrengthenType = this.CheckMouldStrengthenType.Checked;
            this._ProductMouldTest.ClearType = this.CheckClearType.Checked;
            this._ProductMouldTest.OpticsType = this.CheckOpticsType.Checked;
            this._ProductMouldTest.LaserType = this.CheckLaserType.Checked;
            this._ProductMouldTest.ImpaceType = this.CheckImpaceType.Checked;
            this._ProductMouldTest.Content = this.CheckContentType.Checked;
            this._ProductMouldTest.ContentType = this.CheckContentBool.Checked;

            //备注
            this._ProductMouldTest.InjectionNote = this.meInjectionNote.Text;
            this._ProductMouldTest.StrengThenNote = this.meStrengThenNote.Text;
            this._ProductMouldTest.QualityControlNote = this.meQualityControlNote.Text;

            //成品类别
            this._ProductMouldTest.ProductCategoryId = this.lookUpEditProductCategory.EditValue == null ? null : this.lookUpEditProductCategory.EditValue.ToString();

            //成品穴数
            if (!string.IsNullOrEmpty(ComboxMouldMount.Text))
            {
                this._ProductMouldTest.MouldMount = int.Parse(this.ComboxMouldMount.Text.ToString());
            }

            //试模人员
            this._ProductMouldTest.Employee = this.NccMouldTestPerson.EditValue as Model.Employee;
            if (this._ProductMouldTest.Employee != null)
            {
                this._ProductMouldTest.EmployeeId = this._ProductMouldTest.Employee.EmployeeId;
            }

            //射出人员
            this._ProductMouldTest.Emp_Employee = this.NccMouldInjectionPerson.EditValue as Model.Employee;
            if (this._ProductMouldTest.Emp_Employee != null)
            {
                this._ProductMouldTest.Emp_EmployeeId = this._ProductMouldTest.Emp_Employee.EmployeeId;
            }

            //厂商
            this._ProductMouldTest.Supplier = this.NccSupplier.EditValue as Model.Supplier;
            if (this._ProductMouldTest.Supplier != null)
            {
                this._ProductMouldTest.SupplierId = this._ProductMouldTest.Supplier.SupplierId;
            }

            //强化人员
            this._ProductMouldTest.Emp_Employee2 = this.NccMouldStrengthenPerson.EditValue as Model.Employee;
            if (NccMouldStrengthenPerson.EditValue != null)
            {
                this._ProductMouldTest.Emp_EmployeeId2 = this._ProductMouldTest.Emp_Employee2.EmployeeId;
            }

            //安规、清晰、光学、雷射人员
            this._ProductMouldTest.Emp_Employee3 = this.NccFourWayPerson.EditValue as Model.Employee;
            if (NccFourWayPerson.EditValue != null)
            {
                this._ProductMouldTest.Emp_EmployeeId3 = this._ProductMouldTest.Emp_Employee3.EmployeeId;
            }

            //冲击人员
            this._ProductMouldTest.Emp_Employee4 = this.NccImpactPerson.EditValue as Model.Employee;
            if (NccImpactPerson.EditValue != null)
            {
                this._ProductMouldTest.Emp_EmployeeId4 = this._ProductMouldTest.Emp_Employee4.EmployeeId;
            }

            //内容人员
            this._ProductMouldTest.Emp_Employee5 = this.NccContent.EditValue as Model.Employee;
            if (NccContent.EditValue != null)
            {
                this._ProductMouldTest.Emp_EmployeeId5 = this._ProductMouldTest.Emp_Employee5.EmployeeId;
            }

            //开发备注人员
            this._ProductMouldTest.Emp_Employee6 = this.NccDevelopDetailPerson.EditValue as Model.Employee;
            if (NccDevelopDetailPerson.EditValue != null)
            {
                this._ProductMouldTest.Emp_EmployeeId6 = this._ProductMouldTest.Emp_Employee6.EmployeeId;
            }

            //主管1
            this._ProductMouldTest.Emp_Employee7 = this.NccManagerOne.EditValue as Model.Employee;
            if (NccManagerOne.EditValue != null)
            {
                this._ProductMouldTest.Emp_EmployeeId7 = this._ProductMouldTest.Emp_Employee7.EmployeeId;
            }

            //主管2
            this._ProductMouldTest.Emp_Employee8 = this.NccManagerTwo.EditValue as Model.Employee;
            if (NccManagerTwo.EditValue != null)
            {
                this._ProductMouldTest.Emp_EmployeeId8 = this._ProductMouldTest.Emp_Employee8.EmployeeId;
            }

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (this._FilesAccessoriesHelper == null || this._FilesAccessoriesHelper.Count == 0)
            {
                this._ProductMouldTest.accessoriesList = string.Empty;
            }
            else
            {
                StringBuilder su = new StringBuilder();
                StringBuilder str = new StringBuilder();
                foreach (FilesAccessoriesHelper fir in this._FilesAccessoriesHelper)
                {
                    su.Append(fir.FileFullname + "|");
                    str.Append(fir.Filename + "\\" + fir.Description + "|");
                }

                this._ProductMouldTest.accessoriesList = su.ToString().Substring(0, su.ToString().Length - 1);
                this._ProductMouldTest.EachMouldDesc = str.ToString().Substring(0, str.Length - 1);
            }

            switch (this.action)
            {
                case "insert":
                    this._ProductMouldTestManager.Insert(this._ProductMouldTest);
                    break;
                case "update":
                    this._ProductMouldTestManager.Update(this._ProductMouldTest);
                    break;
            }

            switch (this.action)
            {
                case "insert":
                    this.productMouldTestList.Add(this._ProductMouldTest);
                    this.productMouldTestList.OrderBy(p => new { p.MouldId, p.InsertTime });
                    break;
                case "update":
                    index = this.productMouldTestList.IndexOf(this.productMouldTestList.Where(p => p.ProductMouldTestId == this._ProductMouldTest.ProductMouldTestId).ToList<Model.ProductMouldTest>().Last<Model.ProductMouldTest>());
                    this.productMouldTestList.RemoveAt(index);
                    this.productMouldTestList.Insert(index, this._ProductMouldTest);
                    this.productMouldTestList.OrderBy(p => new { p.MouldId, p.InsertTime });
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._ProductMouldTest);
        }

        private void CheckContentType_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckContentType.Checked == true)
            {
                TxtContentDetail1.Properties.ReadOnly = false;
                TxtContentDetail2.Properties.ReadOnly = false;
                TxtContentDetail3.Properties.ReadOnly = false;
            }
            else
            {
                TxtContentDetail1.Properties.ReadOnly = true;
                TxtContentDetail2.Properties.ReadOnly = true;
                TxtContentDetail3.Properties.ReadOnly = true;
                TxtContentDetail1.Text = "";
                TxtContentDetail2.Text = "";
                TxtContentDetail3.Text = "";
            }
        }


        private void SimpleButtonAddFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string fn in ofd.FileNames)
                {
                    FilesAccessoriesHelper fh = new FilesAccessoriesHelper();
                    fh.FileFullname = fn;
                    fh.Filename = fn.Substring(fn.LastIndexOf("\\") + 1);
                    FileInfo fi = new FileInfo(fn);
                    fh.Filesize = fi.Length.ToString();

                    if (!this._FilesAccessoriesHelper.Contains(fh))
                        this._FilesAccessoriesHelper.Add(fh);
                }
                this.gridControl1.RefreshDataSource();
            }

        }

        private void SimpleButtonDeleteFile_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceFiles.Current != null)
            {
                this._FilesAccessoriesHelper.Remove(this.bindingSourceFiles.Current as FilesAccessoriesHelper);
                this.gridControl1.RefreshDataSource();
            }
            else
            {
                MessageBox.Show(Properties.Resources.ErrorNoMoreRows);
            }
        }

        //点击附件名称，打开附件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            FilesAccessoriesHelper fss = this.bindingSourceFiles.Current as FilesAccessoriesHelper;
            System.Diagnostics.Process.Start(fss.FileFullname);
        }

        private void ProductsMouldTestEditForm_Load(object sender, EventArgs e)
        {

        }


        //添加商品种类
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ProductCategory f = new ProductCategory("Id", "Name");
            f.ShowDialog();
            this.bindingSourceProductCategory.DataSource = (new BL.ProductMouldCategoryManager()).Select();

        }

        //搜索
        //private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ProductsMouldTestList formlist = new ProductsMouldTestList();
        //    if (formlist.ShowDialog(this) == DialogResult.OK)
        //    {
        //        Model.ProductMouldTest MouldTestModel = formlist.SelectItem as Model.ProductMouldTest;
        //        if (MouldTestModel != null)
        //        {
        //            this._ProductMouldTest = MouldTestModel;
        //            this.Refresh();
        //        }
        //        formlist.Dispose();
        //        GC.Collect();
        //    }
        //}

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductsMouldTestList formlist = new ProductsMouldTestList();
            if (formlist.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProductMouldTest MouldTestModel = formlist.SelectItem as Model.ProductMouldTest;
                if (MouldTestModel != null)
                {
                    this._ProductMouldTest = MouldTestModel;
                    this.Refresh();
                }
                formlist.Dispose();
                GC.Collect();
            }
        }

        public class FilesAccessoriesHelper
        {
            private string _FileFullname;

            public string FileFullname
            {
                get { return _FileFullname; }
                set { _FileFullname = value; }
            }

            private string _Filename;

            /// <summary>
            /// 文件后缀名：文件名+格式名
            /// </summary>
            public string Filename
            {
                get { return _Filename; }
                set { _Filename = value; }
            }

            private string _Filesize;

            public string Filesize
            {
                get
                {
                    string result = string.Empty;
                    double msize = double.Parse(string.IsNullOrEmpty(this._Filesize) ? "0" : this._Filesize) / 1024;
                    if (msize < 1024)
                        result = string.Format("{0:f}", msize) + "kb";
                    else
                        result = string.Format("{0:f}", msize / 1024) + "M";
                    return result;
                }
                set { _Filesize = value; }
            }

            private string _description;

            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }
        }

    }
}