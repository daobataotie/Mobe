using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData;
using System.IO;
using System.Linq;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class ProductMouldEditForm : Book.UI.Settings.BasicData.BaseEditForm
    {

        private BL.ProductMouldManager _proMouldManager = new Book.BL.ProductMouldManager();
        private BL.MouldCategoryManager _mouldcateManager = new Book.BL.MouldCategoryManager();
        private BL.ProductMaterialManager _productMaterialManager = new Book.BL.ProductMaterialManager();
        private BL.MouldAttachmentManager _mouldattenachmentManager = new Book.BL.MouldAttachmentManager();
        private Model.MouldAttachment _mouldattachment;
        public Model.ProductMould _productMould;

        IList<MouldHelp> mouldList = new List<MouldHelp>();

        public ProductMouldEditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.ProductMould.PROPERTY_ID, new AA(Properties.Resources.NumsIsNotNull, this.textEditId as Control));
            this.requireValueExceptions.Add(Model.ProductMould.PROPERTY_MOULDNAME, new AA(Properties.Resources.NameIsNotNull, this.textEditMouldName as Control));
            this.requireValueExceptions.Add(Model.ProductMould.PROPERTY_SUPPLIERID, new AA(Properties.Resources.ChooseSupplier, this.newChooseContorlSupplierId as Control));

            this.newChooseContorlSupplierId.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlEmployeeId.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployee0Id.Choose = new Book.UI.Settings.BasicData.Employees.ChooseEmployee();

            this.action = "view";
        }

        private int sign = 0;
        public ProductMouldEditForm(string s)
            : this()
        {
            this._productMould = this._proMouldManager.SelectByMouldId(s);
            if (this._productMould != null)
                this.sign = 1;
            this.action = "view";
        }

        public string ServerSavePath
        {
            get
            {
                string s = string.Empty;
                //取得服务器附件存储地址
                if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["MouldPath"] != null)
                {
                    s = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["MouldPath"].Value;
                }
                return s;
            }
        }

        protected override void AddNew()
        {
            this._productMould = new Book.Model.ProductMould();
            this._productMould.MouldId = Guid.NewGuid().ToString();

        }

        protected override void MoveNext()
        {
            Model.ProductMould pro = this._proMouldManager.GetNext(this._productMould);
            if (pro == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._productMould = pro;
        }

        protected override void MovePrev()
        {
            Model.ProductMould pro = this._proMouldManager.GetPrev(this._productMould);
            if (pro == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._productMould = pro;
        }

        protected override bool HasRows()
        {
            return this._proMouldManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._proMouldManager.HasRowsAfter(this._productMould);
        }
        protected override bool HasRowsPrev()
        {
            return this._proMouldManager.HasRowsBefore(this._productMould);
        }

        protected override void MoveFirst()
        {
            this._productMould = this._proMouldManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.sign == 1)
            {
                this.sign = 0;
                return;
            }
            this._productMould = this._proMouldManager.GetLast();
        }

        protected override void Save()
        {
            this._productMould.Id = this.textEditId.Text;
            this._productMould.MouldName = this.textEditMouldName.Text;

            //this._productMould.Product = this.buttonEditProductId.EditValue as Model.Product;
            //if (this._productMould.Product != null)
            //    this._productMould.ProductId = this._productMould.Product.ProductId;


            if (this.newChooseContorlSupplierId.EditValue != null)
                this._productMould.SupplierId = (this.newChooseContorlSupplierId.EditValue as Model.Supplier).SupplierId;

            this._productMould.MouldCategoryId = this.lookUpEditMouldCategoryId.EditValue == null ? null : this.lookUpEditMouldCategoryId.EditValue.ToString();
            //this._productMould.MachineKind = this.textEditMachineKind.Text;
            this._productMould.MouldDescription = this.memoEditMouldDescription.Text;
            //this._productMould.ProductSpecification = this.textEditProductSpecification.Text;
            //if(this.newChooseContorlKeeper.EditValue!=null)
            // this._productMould.Keeper = (this.newChooseContorlKeeper.EditValue as Model.Employee).EmployeeId ;
            if (this.lookUpEditMouldMaterial.Text != "")
                this._productMould.MouldMaterial = this.lookUpEditMouldMaterial.EditValue.ToString();
            this._productMould.MouldSpecification = this.textEditMouldSpecification.Text;
            //this._productMould.PartName = this.textEditpartName.Text;
            this._productMould.PartWeight = Convert.ToDouble(this.spinEditpartWeight.Text);
            this._productMould.MouldPrice = Convert.ToDecimal(this.spinEditMouldPrice.Text);
            this._productMould.MouldBarCode = this.textEditMouldBarCode.Text;
            this._productMould.DoubleCount = Convert.ToDouble(this.spinEditDoubleCount.Text);

            //this._productMould.StartTime = this.dateEditStartTime.DateTime == new DateTime() ? global::Helper.DateTimeParse.NullDate : this.dateEditStartTime.DateTime;
            //this._productMould.FirstTime = this.dateEditFirstTime.DateTime == new DateTime() ? global::Helper.DateTimeParse.NullDate : this.dateEditFirstTime.DateTime;
            //this._productMould.OkTime = this.dateEditOkTime.DateTime == new DateTime() ? global::Helper.DateTimeParse.NullDate : this.dateEditOkTime.DateTime;

            if (this.dateEditStartTime.EditValue != null && !string.IsNullOrEmpty(this.dateEditStartTime.EditValue.ToString()))
                this._productMould.StartTime = this.dateEditStartTime.DateTime;
            if (this.dateEditFirstTime.EditValue != null && !string.IsNullOrEmpty(this.dateEditFirstTime.EditValue.ToString()))
                this._productMould.FirstTime = this.dateEditFirstTime.DateTime;
            if (this.dateEditOkTime.EditValue != null && !string.IsNullOrEmpty(this.dateEditOkTime.EditValue.ToString()))
                this._productMould.OkTime = this.dateEditOkTime.DateTime;

            if (this.radioGroup.SelectedIndex == 0)
                this._productMould.CodeIsAotu = true;

            if (this.newChooseContorlEmployee0Id.EditValue != null)
                this._productMould.Employee0Id = (this.newChooseContorlEmployee0Id.EditValue as Model.Employee).EmployeeId;
            if (this.newChooseContorlEmployeeId.EditValue != null)
                this._productMould.EmployeeId = (this.newChooseContorlEmployeeId.EditValue as Model.Employee).EmployeeId;

            //上传附件信息
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            if (this.mouldList != null && this.mouldList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder str = new StringBuilder();
                foreach (MouldHelp mould in this.mouldList)
                {
                    sb.Append(mould.FileFullName + "|");

                    str.Append(mould.FileName + "\\" + mould.Description + "|");
                }
                this._productMould.Upload = sb.ToString().Substring(0, sb.Length - 1);

                this._productMould.EachMouldDesc = str.ToString().Substring(0, str.Length - 1);
            }
            else
                this._productMould.Upload = "";

            switch (this.action)
            {
                case "insert":
                    this._proMouldManager.Insert(this._productMould);
                    break;

                case "update":
                    this._proMouldManager.Update(this._productMould);
                    break;
            }
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            Model.ProductMould productMould = this.bindingSource1.Current as Model.ProductMould;
            this._proMouldManager.Delete(this._productMould);
        }

        public override void Refresh()
        {

            if (this._productMould == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            if (this.action == "view")
            {
                this._productMould = this._proMouldManager.Get(this._productMould.MouldId);
                if (this._productMould == null)
                {
                    this.AddNew();
                    this.action = "insert";
                }
            }


            this.textEditId.Text = this._productMould.Id;
            this.textEditMouldName.Text = this._productMould.MouldName;
            //this.buttonEditProductId.EditValue = this._productMould.Product;
            if (this._productMould.SupplierId != "")
                this.newChooseContorlSupplierId.EditValue = this._productMould.Supplier;
            if (this._productMould.MouldCategoryId != "")
                this.lookUpEditMouldCategoryId.EditValue = this._productMould.MouldCategoryId;
            //this.textEditMachineKind.Text = this._productMould.MachineKind;
            this.textEditMouldSpecification.Text = this._productMould.MouldSpecification;
            //this.textEditProductSpecification.Text = this._productMould.ProductSpecification;

            //if (this._productMould.Employee0Id != null)
            //    this.newChooseContorlEmployee0Id.EditValue = this._productMould.Employee0Id;
            //if (this._productMould.EmployeeId != null)
            //    this.newChooseContorlEmployeeId.EditValue = this._productMould.EmployeeId;
            this.newChooseContorlEmployee0Id.EditValue = this._productMould.Employee0;
            this.newChooseContorlEmployeeId.EditValue = this._productMould.Employee;

            //this.newChooseContorlKeeper.EditValue = this._productMould.Keeper;
            if (this._productMould.MouldMaterial != null)
                this.lookUpEditMouldMaterial.EditValue = this._productMould.MouldMaterial;
            this.memoEditMouldDescription.Text = this._productMould.MouldDescription;

            //this.textEditpartName.Text = this._productMould.PartName;
            this.spinEditpartWeight.Text = this._productMould.PartWeight.ToString();
            this.spinEditMouldPrice.Text = this._productMould.MouldPrice.ToString();
            this.textEditMouldBarCode.Text = this._productMould.MouldBarCode;

            if (this._productMould.CodeIsAotu != null)
            {
                if (this._productMould.CodeIsAotu.Value)
                    this.radioGroup.SelectedIndex = 1;
            }
            else
                this.radioGroup.SelectedIndex = 0;

            if (this._productMould.DoubleCount != null)
                this.spinEditDoubleCount.Text = this._productMould.DoubleCount.Value.ToString();
            else
                this.spinEditDoubleCount.Text = "0";
            //if (this._productMould.StartTime != null && this._productMould.StartTime != new DateTime() && this._productMould.StartTime != global::Helper.DateTimeParse.NullDate)
            //    this.dateEditStartTime.Text = this._productMould.StartTime.ToString();
            //else
            //    this.dateEditStartTime.DateTime = System.DateTime.Now;
            //if (this._productMould.FirstTime != null && this._productMould.FirstTime != new DateTime() && this._productMould.FirstTime != global::Helper.DateTimeParse.NullDate)
            //    this.dateEditFirstTime.Text = this._productMould.FirstTime.ToString();
            //else
            //    this.dateEditFirstTime.Text = "";
            //if (this._productMould.OkTime != null && this._productMould.OkTime != new DateTime() && this._productMould.OkTime != global::Helper.DateTimeParse.NullDate)
            //    this.dateEditOkTime.Text = this._productMould.OkTime.ToString();
            //else
            //    this.dateEditOkTime.DateTime = System.DateTime.Now;
            this.dateEditStartTime.EditValue = this._productMould.StartTime;
            this.dateEditFirstTime.EditValue = this._productMould.FirstTime;
            this.dateEditOkTime.EditValue = this._productMould.OkTime;

            //this.buttonEdit1.Text = "";
            //this.memoEditDescription.Text = "1";
            //this.pictureEditpic.Image = null;


            this.bindingSource1.DataSource = this._proMouldManager.Select();
            this._productMould.Details = this._mouldattenachmentManager.SelectByMouldId(this._productMould);
            //this.bindingSourceDetails.DataSource = this._productMould.Details;

            if (this._productMould.Details.Count > 0)
            {
                if (this._mouldattachment == null)
                {
                    this._mouldattachment = this._mouldattenachmentManager.GetFirst();

                }
                //if (this._mouldattachment.Picture != null && this._mouldattachment.Picture.Length > 0)
                //    this.pictureEditpic.Image = Image.FromStream(new System.IO.MemoryStream(this._mouldattachment.Picture));

            }
            //else
            //    this.pictureEditpic.Image = null;

            //获取已上传文件列表
            GetHasUpLoadFiles();

            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    break;
                case "update":
                    //if (this._productMould.OkTime.HasValue)
                    //{
                    //    this.dateEditOkTime.Properties.ReadOnly = true;
                    //}
                    //else
                    //{
                    //    this.dateEditOkTime.Properties.ReadOnly = false;
                    //}
                    break;
                case "view":
                    break;
            }
        }

        //private void buttonEditProductId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        Model.Product product = f.SelectedItem as Model.Product;
        //        if (product != null)
        //        {
        //            this.buttonEditProductId.EditValue = product;
        //            this.textEditProductSpecification.Text = product.ProductSpecification;
        //        }
        //    }
        //}

        private void ProductMouldEditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceMouldCategory.DataSource = _mouldcateManager.Select();
            this.bindingSourceMouldMater.DataSource = _productMaterialManager.Select();
            //this._productMould.Details = this._mouldattenachmentManager.Select();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //    this.openFileDialogpic.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            //    if (this.openFileDialogpic.ShowDialog(this) == DialogResult.OK)
            //    {
            //        string fileName = this.openFileDialogpic.FileName;
            //        if (System.IO.File.Exists(fileName))
            //        {
            //            this.buttonEdit1.Text = fileName;
            //            this.pictureEditpic.Image = Image.FromFile(fileName);
            //        }
            //        else
            //        {
            //            MessageBox.Show(this, "fileNotFound", "文件不存在！", MessageBoxButtons.OK);
            //            return;
            //        }
            //    }
        }

        private void sbtn_upload_Click(object sender, EventArgs e)
        {
            Model.MouldAttachment mouldAttachment = new Book.Model.MouldAttachment();
            mouldAttachment.MouldAttachmentId = Guid.NewGuid().ToString();
            byte[] comsign;
            if (this._productMould != null)
                mouldAttachment.MouldId = this._productMould.MouldId;
            //if (this.buttonEdit1.Text != "")
            //{
            //    comsign = System.IO.File.ReadAllBytes(this.buttonEdit1.Text);
            //    mouldAttachment.Picture = comsign;
            //}
            else
            {
                MessageBox.Show("请选择图片！", this.Text);
                return;
            }

            //mouldAttachment.Description = this.memoEditDescription.Text;
            this._productMould.Details.Add(mouldAttachment);

            //this.buttonEdit1.Text = "";
            //this.memoEditDescription.Text = "";
            //this.bindingSourceDetails.DataSource = this._productMould.Details;
            //this.gridControl2.RefreshDataSource();

        }

        //private void gridView2_Click(object sender, EventArgs e)
        //{
        //    this._mouldattachment = this.bindingSourceDetails.Current as Model.MouldAttachment;
        //}

        private void sbtn_minusDetails_Click(object sender, EventArgs e)
        {
            Model.MouldAttachment mouldAttachment = this.bindingSourceDetails.Current as Model.MouldAttachment;
            this._productMould.Details.Remove(mouldAttachment);
            this.bindingSourceDetails.DataSource = this._productMould.Details;
            //this.gridControl2.RefreshDataSource();
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.radioGroup.SelectedIndex;
            if (index == 0)
            {

                //手工輸入
                this.textEditMouldBarCode.Text = string.Empty;
            }
            if (index == 1)
            {
                //自动输入
                this.textEditMouldBarCode.Text = Guid.NewGuid().ToString();//商品条码
            }

        }

        private void barButtonItemBarCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string bar = this.textEditMouldBarCode.Text;
            if (string.IsNullOrEmpty(bar))
            {
                MessageBox.Show(Properties.Resources.NullBarCode, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BarCode code = new BarCode(bar);
            code.ShowPreviewDialog();
        }


        private void sbtn_last_Click(object sender, EventArgs e)
        {
            if (this._mouldattenachmentManager.HasRowsBefore(this._mouldattachment))
                this._mouldattachment = this._mouldattenachmentManager.GetPrev(this._mouldattachment);
            if (this._mouldattachment == null)
            {
                this._mouldattachment = this._mouldattenachmentManager.GetFirst();

            }
            //if (this._mouldattachment.Picture != null && this._mouldattachment.Picture.Length > 0)
            //    this.pictureEditpic.Image = Image.FromStream(new System.IO.MemoryStream(this._mouldattachment.Picture));
        }

        private void sbtn_next_Click(object sender, EventArgs e)
        {
            if (this._mouldattenachmentManager.HasRowsAfter(this._mouldattachment))
                this._mouldattachment = this._mouldattenachmentManager.GetNext(this._mouldattachment);

            if (this._mouldattachment == null)
            {
                this._mouldattachment = this._mouldattenachmentManager.GetFirst();

            }
            //if (this._mouldattachment.Picture != null && this._mouldattachment.Picture.Length > 0)
            //    this.pictureEditpic.Image = Image.FromStream(new System.IO.MemoryStream(this._mouldattachment.Picture));
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            this._productMould = this.bindingSource1.Current as Model.ProductMould;
            if (_productMould != null)
            {
                this.action = "view";
                this.Refresh();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.InitialDirectory = "C:\\";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string str in ofd.FileNames)
                {
                    MouldHelp mould = new MouldHelp();
                    mould.FileFullName = str;
                    mould.FileName = str.Substring(str.LastIndexOf("\\") + 1);
                    FileInfo fi = new FileInfo(str);
                    mould.FileSize = fi.Length.ToString();

                    this.mouldList.Add(mould);
                }
            }

            this.bindingSourceMould.DataSource = this.mouldList;
            this.gridControl2.RefreshDataSource();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (bindingSourceMould.Current != null)
            {
                if (this.mouldList.Count > 0)
                {
                    this.mouldList.Remove(this.bindingSourceMould.Current as MouldHelp);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ErrorNoMoreRows);
                }
            }
            gridControl2.RefreshDataSource();
        }

        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            MouldHelp mould = this.bindingSourceMould.Current as MouldHelp;
            System.Diagnostics.Process.Start(mould.FileFullName);
        }

        /// <summary>
        /// 获取已上传文件列表
        /// </summary>
        private void GetHasUpLoadFiles()
        {
            this.mouldList.Clear();
            if (Directory.Exists(this.ServerSavePath + "\\" + this._productMould.MouldId))
            {
                string[] hasUpLoad = Directory.GetFiles(this.ServerSavePath + "\\" + this._productMould.MouldId);
                string[] eachDesc = null;
                if (this._productMould.EachMouldDesc != null)
                    eachDesc = this._productMould.EachMouldDesc.Split('|');
                foreach (string str in hasUpLoad)
                {
                    MouldHelp mould = new MouldHelp();
                    mould.FileFullName = str;
                    mould.FileName = str.Substring(str.LastIndexOf("\\") + 1);
                    FileInfo fi = new FileInfo(str);
                    mould.FileSize = fi.Length.ToString();

                    if (eachDesc != null && eachDesc.Length > 0)
                    {
                        foreach (string desc in eachDesc)
                        {
                            if (desc.Substring(0, desc.LastIndexOf('\\')) == mould.FileName)
                                mould.Description = desc.Substring(desc.LastIndexOf('\\') + 1);
                        }
                    }

                    this.mouldList.Add(mould);
                }
            }
            this.bindingSourceMould.DataSource = this.mouldList;
            this.gridControl2.RefreshDataSource();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.action != "view")
            {
                MessageBox.Show("請保存更改！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductMouldEditFormList f = new ProductMouldEditFormList();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProductMould model = f.SelectItem as Model.ProductMould;
                if (model != null)
                {
                    this._productMould = model;
                    this.Refresh();
                }
            }
        }
    }
}
/// <summary>
/// 模具附件帮助类
/// </summary>
public class MouldHelp
{
    public Image FileImage
    {
        get
        {
            string extendname = this.FileName.Substring(this.FileName.LastIndexOf(".") + 1).ToLower();
            if (extendname.Equals("txt", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_txt;
            if (extendname.Equals("doc", StringComparison.OrdinalIgnoreCase) || extendname.Equals("docx", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_Word;
            if (extendname.Equals("ppt", StringComparison.OrdinalIgnoreCase) || extendname.Equals("pptx", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_PowerPoint;
            if (extendname.Equals("mdb", StringComparison.OrdinalIgnoreCase) || extendname.Equals("accdb", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_Access;
            if (extendname.Equals("xls", StringComparison.OrdinalIgnoreCase) || extendname.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_Excel;
            if (extendname.Equals("mp3", StringComparison.OrdinalIgnoreCase) || extendname.Equals("wma", StringComparison.OrdinalIgnoreCase) || extendname.Equals("wmv", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_muisc;
            if (extendname.Equals("avi", StringComparison.OrdinalIgnoreCase) || extendname.Equals("rmvb", StringComparison.OrdinalIgnoreCase) || extendname.Equals("flv", StringComparison.OrdinalIgnoreCase) || extendname.Equals("mkv", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_realplay;
            if (extendname.Equals("jpg", StringComparison.OrdinalIgnoreCase) || extendname.Equals("jpeg", StringComparison.OrdinalIgnoreCase) || extendname.Equals("bmp", StringComparison.OrdinalIgnoreCase) || extendname.Equals("gif", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_pic;
            if (extendname.Equals("pdf", StringComparison.OrdinalIgnoreCase))
                return Book.UI.Properties.Resources.img_pdf;

            return Book.UI.Properties.Resources.img_default;
        }
    }

    private string _FileFullName;

    public string FileFullName
    {
        get { return _FileFullName; }
        set { _FileFullName = value; }
    }

    private string _FileName;

    /// <summary>
    /// 绑定的要显示的单文件名称 {文件名.后缀名}
    /// </summary>
    public string FileName
    {
        get { return _FileName; }
        set { _FileName = value; }
    }

    private string _FileSize;

    public string FileSize
    {
        get
        {
            string Result = string.Empty;
            double mSize = double.Parse(string.IsNullOrEmpty(this._FileSize) ? "0" : this._FileSize) / 1024;
            if (mSize < 1024)
                Result = string.Format("{0:f}", mSize) + " kb";
            else
                Result = string.Format("{0:f}", mSize / 1024) + " M";
            return Result;
        }
        set { _FileSize = value; }
    }

    /// <summary>
    /// 说明
    /// </summary>
    private string _description;

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
}
