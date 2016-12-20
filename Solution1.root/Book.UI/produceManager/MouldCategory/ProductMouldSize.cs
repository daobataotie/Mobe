using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class ProductMouldSize : Settings.BasicData.BaseEditForm
    {
        BL.ProductMouldSizeManager _manager = new Book.BL.ProductMouldSizeManager();
        Model.ProductMouldSize _productMouldSize;
        PictureHelp pictureModel = new PictureHelp();
        string PicturePath = string.Empty;
        IList<PictureHelp> pictureList = new List<PictureHelp>();

        public string ServerSavePath
        {
            get
            {
                string s = string.Empty;
                //取得服务器附件存储地址
                if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["ProductSize"] != null)
                {
                    s = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["ProductSize"].Value;
                }
                return s;
            }
        }

        public ProductMouldSize()
        {
            InitializeComponent();
            this.invalidValueExceptions.Add(Model.ProductMouldSize.PRO_ProductSizeId, new AA(Properties.Resources.ProductSizeIsNotNull, this.txt_ProductSizeId));
            this.bindingSourceMouldCategory.DataSource = (new BL.MouldCategoryManager()).Select();
            this.bindingSourceProductMould.DataSource = (new BL.ProductMouldManager()).Select();

            this.action = "view";
        }

        protected override bool HasRows()
        {
            return this._manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._manager.HasRowsAfter(this._productMouldSize);
        }

        protected override bool HasRowsPrev()
        {
            return this._manager.HasRowsBefore(this._productMouldSize);
        }

        protected override void MoveFirst()
        {
            this._productMouldSize = this._manager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._productMouldSize = this._manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.ProductMouldSize model = this._manager.GetNext(this._productMouldSize);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._productMouldSize = model;
        }

        protected override void MovePrev()
        {
            Model.ProductMouldSize model = this._manager.GetPrev(this._productMouldSize);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._productMouldSize = model;
        }

        protected override void AddNew()
        {
            this._productMouldSize = new Book.Model.ProductMouldSize();
            this._productMouldSize.ProductMouldSizeId = Guid.NewGuid().ToString();
            this._productMouldSize.ProductDate = DateTime.Now;

            this.action = "insert";
        }

        protected override void Save()
        {
            this._productMouldSize.ProductSizeId = this.txt_ProductSizeId.Text;
            this._productMouldSize.Note = this.richTextBoxNote.Rtf;
            this._productMouldSize.MouldCateGoryId1 = this.lookUpEditMouldCategory1.EditValue == null ? null : this.lookUpEditMouldCategory1.EditValue.ToString();
            this._productMouldSize.MouldCategoryId2 = this.lookUpEditMouldCategory2.EditValue == null ? null : this.lookUpEditMouldCategory2.EditValue.ToString();
            this._productMouldSize.MouldCategoryId3 = this.lookUpEditMouldCategory3.EditValue == null ? null : this.lookUpEditMouldCategory3.EditValue.ToString();
            this._productMouldSize.MouldCategoryId4 = this.lookUpEditMouldCategory4.EditValue == null ? null : this.lookUpEditMouldCategory4.EditValue.ToString();
            this._productMouldSize.MouldCategoryId5 = this.lookUpEditMouldCategory5.EditValue == null ? null : this.lookUpEditMouldCategory5.EditValue.ToString();
            this._productMouldSize.MouldCategoryId6 = this.lookUpEditMouldCategory6.EditValue == null ? null : this.lookUpEditMouldCategory6.EditValue.ToString();
            this._productMouldSize.MouldCategoryId7 = this.lookUpEditMouldCategory7.EditValue == null ? null : this.lookUpEditMouldCategory7.EditValue.ToString();
            this._productMouldSize.MouldCategoryId8 = this.lookUpEditMouldCategory8.EditValue == null ? null : this.lookUpEditMouldCategory8.EditValue.ToString();

            this._productMouldSize.MouldId1 = this.lookUpEditMould1.EditValue == null ? null : this.lookUpEditMould1.EditValue.ToString();
            this._productMouldSize.MouldId2 = this.lookUpEditMould2.EditValue == null ? null : this.lookUpEditMould2.EditValue.ToString();
            this._productMouldSize.MouldId3 = this.lookUpEditMould3.EditValue == null ? null : this.lookUpEditMould3.EditValue.ToString();
            this._productMouldSize.MouldId4 = this.lookUpEditMould4.EditValue == null ? null : this.lookUpEditMould4.EditValue.ToString();
            this._productMouldSize.MouldId5 = this.lookUpEditMould5.EditValue == null ? null : this.lookUpEditMould5.EditValue.ToString();
            this._productMouldSize.MouldId6 = this.lookUpEditMould6.EditValue == null ? null : this.lookUpEditMould6.EditValue.ToString();
            this._productMouldSize.MouldId7 = this.lookUpEditMould7.EditValue == null ? null : this.lookUpEditMould7.EditValue.ToString();
            this._productMouldSize.MouldId8 = this.lookUpEditMould8.EditValue == null ? null : this.lookUpEditMould8.EditValue.ToString();

            this._productMouldSize.ProductDate = this.date_Product.EditValue == null ? DateTime.Now : this.date_Product.DateTime;

            //上传图片
            if (this.PicturePath != null && System.IO.File.Exists(this.PicturePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(this.PicturePath);
                if (b != null)
                    this._productMouldSize.Picture = b;
            }
            //上传附件信息
            if (this.pictureList != null && this.pictureList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (PictureHelp mould in this.pictureList)
                {
                    sb.Append(mould.FileFullName + "|");
                }
                this._productMouldSize.Upload = sb.ToString().Substring(0, sb.Length - 1);
            }
            else
                this._productMouldSize.Upload = "";

            switch (this.action)
            {
                case "insert":
                    if (this._productMouldSize.Picture == null)
                        this._productMouldSize.Picture = new byte[] { };
                    this._manager.Insert(this._productMouldSize);
                    break;
                case "update":
                    if (this._productMouldSize.Picture == null)
                        this._productMouldSize.Picture = new byte[] { };
                    this._manager.Update(this._productMouldSize);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._productMouldSize == null)
                AddNew();
            this.txt_ProductSizeId.Text = this._productMouldSize.ProductSizeId;
            this.richTextBoxNote.Rtf = this._productMouldSize.Note;
            this.lookUpEditMouldCategory1.EditValue = this._productMouldSize.MouldCateGoryId1;
            this.lookUpEditMouldCategory2.EditValue = this._productMouldSize.MouldCategoryId2;
            this.lookUpEditMouldCategory3.EditValue = this._productMouldSize.MouldCategoryId3;
            this.lookUpEditMouldCategory4.EditValue = this._productMouldSize.MouldCategoryId4;
            this.lookUpEditMouldCategory5.EditValue = this._productMouldSize.MouldCategoryId5;
            this.lookUpEditMouldCategory6.EditValue = this._productMouldSize.MouldCategoryId6;
            this.lookUpEditMouldCategory7.EditValue = this._productMouldSize.MouldCategoryId7;
            this.lookUpEditMouldCategory8.EditValue = this._productMouldSize.MouldCategoryId8;
            this.lookUpEditMould1.EditValue = this._productMouldSize.MouldId1;
            this.lookUpEditMould2.EditValue = this._productMouldSize.MouldId2;
            this.lookUpEditMould3.EditValue = this._productMouldSize.MouldId3;
            this.lookUpEditMould4.EditValue = this._productMouldSize.MouldId4;
            this.lookUpEditMould5.EditValue = this._productMouldSize.MouldId5;
            this.lookUpEditMould6.EditValue = this._productMouldSize.MouldId6;
            this.lookUpEditMould7.EditValue = this._productMouldSize.MouldId7;
            this.lookUpEditMould8.EditValue = this._productMouldSize.MouldId8;

            //显示图片
            if (this._productMouldSize.Picture != null && this._productMouldSize.Picture.Length > 0)
                this.pictureEdit1.Image = Image.FromStream(new System.IO.MemoryStream(this._productMouldSize.Picture));
            else
                this.pictureEdit1.Image = null;
            //获取已上传文件列表
            GetHasUpLoadFiles();

            base.Refresh();
            this.btn_ProductMould1.Enabled = true;
            this.btn_ProductMould2.Enabled = true;
            this.btn_ProductMould3.Enabled = true;
            this.btn_ProductMould4.Enabled = true;
            this.btn_ProductMould5.Enabled = true;
            this.btn_ProductMould6.Enabled = true;
            this.btn_ProductMould7.Enabled = true;
            this.btn_ProductMould8.Enabled = true;
            this.btn_ProductMould9.Enabled = true;
            this.btn_ProductMould10.Enabled = true;
            this.btn_ProductMould11.Enabled = true;
            this.btn_ProductMould12.Enabled = true;
            this.btn_ProductMould13.Enabled = true;
            this.btn_ProductMould14.Enabled = true;
            this.btn_TestMould1.Enabled = true;
            this.btn_TestMould2.Enabled = true;
            this.btn_TestMould3.Enabled = true;
            this.btn_TestMould4.Enabled = true;
            this.btn_TestMould5.Enabled = true;
            this.btn_TestMould6.Enabled = true;
            this.btn_TestMould7.Enabled = true;
            this.btn_TestMould8.Enabled = true;
            this.btn_TestMould9.Enabled = true;
            this.btn_TestMould10.Enabled = true;
            this.btn_TestMould11.Enabled = true;
            this.btn_TestMould12.Enabled = true;
            this.btn_TestMould13.Enabled = true;
            this.btn_TestMould14.Enabled = true;
        }

        private void GetHasUpLoadFiles()
        {
            this.pictureList.Clear();
            if (Directory.Exists(this.ServerSavePath + "\\" + this._productMouldSize.ProductMouldSizeId))
            {
                string[] hasUpLoad = Directory.GetFiles(this.ServerSavePath + "\\" + this._productMouldSize.ProductMouldSizeId);
                foreach (string str in hasUpLoad)
                {
                    PictureHelp mould = new PictureHelp();
                    mould.FileFullName = str;
                    mould.FileName = str.Substring(str.LastIndexOf("\\") + 1);
                    FileInfo fi = new FileInfo(str);
                    mould.FileSize = fi.Length.ToString();

                    this.pictureList.Add(mould);
                }
            }
            this.bindingSource1.DataSource = this.pictureList;
            this.gridControl1.RefreshDataSource();
        }

        protected override void Delete()
        {
            if (this._productMouldSize == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._manager.Delete(this._productMouldSize.ProductMouldSizeId);
            this._productMouldSize = this._manager.GetNext(this._productMouldSize);
            if (this._productMouldSize == null)
                this._productMouldSize = this._manager.GetLast();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_UpLoadPicture_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.InitialDirectory = "C:\\";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string str in ofd.FileNames)
                    {
                        PictureHelp ph = new PictureHelp();
                        ph.FileFullName = str;
                        ph.FileName = str.Substring(str.LastIndexOf("\\") + 1);

                        FileInfo fi = new FileInfo(str);
                        ph.FileSize = fi.Length.ToString();

                        this.pictureEdit1.Image = Image.FromFile(ph.FileFullName);
                        this.PicturePath = ph.FileFullName;
                    }
                }
            }
            catch
            {
                MessageBox.Show("请选择图片文件！");
                return;
            }
        }

        private void btn_ProductMould1_Click(object sender, EventArgs e)
        {
            SimpleButton btn = (SimpleButton)sender;
            ProductMouldEditForm f = new ProductMouldEditForm();
            switch ((string)btn.Tag)
            {
                case "1":
                    if (this.lookUpEditMould1.EditValue != null)
                        f = new ProductMouldEditForm(this.lookUpEditMould1.EditValue.ToString());
                    break;
                case "2":
                    if (this.lookUpEditMould2.EditValue != null)
                        f = new ProductMouldEditForm(this.lookUpEditMould2.EditValue.ToString());
                    break;
                case "3":
                    if (this.lookUpEditMould3.EditValue != null)
                        f = new ProductMouldEditForm(this.lookUpEditMould3.EditValue.ToString());
                    break;
                case "4":
                    if (this.lookUpEditMould4.EditValue != null)
                        f = new ProductMouldEditForm(this.lookUpEditMould4.EditValue.ToString());
                    break;
                case "5":
                    if (this.lookUpEditMould5.EditValue != null)
                        f = new ProductMouldEditForm(this.lookUpEditMould5.EditValue.ToString());
                    break;
                case "6":
                    if (this.lookUpEditMould6.EditValue != null)
                        f = new ProductMouldEditForm(this.lookUpEditMould6.EditValue.ToString());
                    break;
                case "7":
                    if (this.lookUpEditMould7.EditValue != null)
                        f = new ProductMouldEditForm(this.lookUpEditMould7.EditValue.ToString());
                    break;
                case "8":
                    if (this.lookUpEditMould8.EditValue != null)
                        f = new ProductMouldEditForm(this.lookUpEditMould8.EditValue.ToString());
                    break;
                default:
                    break;
            }
            if (this.action != "view")
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    this.bindingSourceProductMould.DataSource = (new BL.ProductMouldManager()).Select();
                    #region 为按钮对应的LookUpEdit赋值
                    switch ((string)btn.Tag)
                    {
                        case "1":
                            this.lookUpEditMould1.EditValue = f._productMould.MouldId;
                            break;
                        case "2":
                            this.lookUpEditMould2.EditValue = f._productMould.MouldId;
                            break;
                        case "3":
                            this.lookUpEditMould3.EditValue = f._productMould.MouldId;
                            break;
                        case "4":
                            this.lookUpEditMould4.EditValue = f._productMould.MouldId;
                            break;
                        case "5":
                            this.lookUpEditMould5.EditValue = f._productMould.MouldId;
                            break;
                        case "6":
                            this.lookUpEditMould6.EditValue = f._productMould.MouldId;
                            break;
                        case "7":
                            this.lookUpEditMould7.EditValue = f._productMould.MouldId;
                            break;
                        case "8":
                            this.lookUpEditMould8.EditValue = f._productMould.MouldId;
                            break;
                        default:
                            break;
                    #endregion
                    }
                }
            }
            else
            {
                f.ShowDialog();
            }
        }

        private void btn_TestMould1_Click(object sender, EventArgs e)
        {
            string tag = null;
            ProductsMouldTestEditForm f;
            SimpleButton btn = (SimpleButton)sender;
            #region 为LookUpEdit对应的模具试模按钮传值
            switch ((string)btn.Tag)
            {
                case "1":
                    tag = this.lookUpEditMould1.EditValue == null ? null : this.lookUpEditMould1.EditValue.ToString();
                    break;
                case "2":
                    tag = this.lookUpEditMould2.EditValue == null ? null : this.lookUpEditMould2.EditValue.ToString();
                    break;
                case "3":
                    tag = this.lookUpEditMould3.EditValue == null ? null : this.lookUpEditMould3.EditValue.ToString();
                    break;
                case "4":
                    tag = this.lookUpEditMould4.EditValue == null ? null : this.lookUpEditMould4.EditValue.ToString();
                    break;
                case "5":
                    tag = this.lookUpEditMould5.EditValue == null ? null : this.lookUpEditMould5.EditValue.ToString();
                    break;
                case "6":
                    tag = this.lookUpEditMould6.EditValue == null ? null : this.lookUpEditMould6.EditValue.ToString();
                    break;
                case "7":
                    tag = this.lookUpEditMould7.EditValue == null ? null : this.lookUpEditMould7.EditValue.ToString();
                    break;
                case "8":
                    tag = this.lookUpEditMould8.EditValue == null ? null : this.lookUpEditMould8.EditValue.ToString();
                    break;
                default:
                    break;
            }
            #endregion
            if (this.action != "view")
            {
                if (tag == null)
                {
                    MessageBox.Show("請設置要測試的模具", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                f = new ProductsMouldTestEditForm(tag);
                f.ShowDialog();
            }
            else
            {
                //if (tag == null)
                //    f = new ProductsMouldTestEditForm();
                //else
                f = new ProductsMouldTestEditForm(tag);
                f.ShowDialog();
            }
        }

        private void ProductMouldSize_Load(object sender, EventArgs e)
        {

        }

        //搜索
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductMouldSizeList f = new ProductMouldSizeList();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProductMouldSize model = f.SelectItem as Model.ProductMouldSize;
                if (model != null)
                {
                    this._productMouldSize = model;
                    this.Refresh();
                }
            }
        }

        private void btn_UploadAccessory_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.InitialDirectory = "C:\\";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string str in ofd.FileNames)
                {
                    PictureHelp mould = new PictureHelp();
                    mould.FileFullName = str;
                    mould.FileName = str.Substring(str.LastIndexOf("\\") + 1);
                    FileInfo fi = new FileInfo(str);
                    mould.FileSize = fi.Length.ToString();

                    this.pictureList.Add(mould);
                }
            }
            this.bindingSource1.DataSource = this.pictureList;
            this.gridControl1.RefreshDataSource();
        }

        private void btn_RemoveAccessory_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                if (this.pictureList.Count > 0)
                {
                    this.pictureList.Remove(this.bindingSource1.Current as PictureHelp);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ErrorNoMoreRows);
                }
            }
            this.gridControl1.RefreshDataSource();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            PictureHelp mould = this.bindingSource1.Current as PictureHelp;
            System.Diagnostics.Process.Start(mould.FileFullName);
        }

        private void btn_RemovePicture_Click(object sender, EventArgs e)
        {
            this.pictureEdit1.Image = null;
            this.PicturePath = string.Empty;
        }
    }
}

/// <summary>
/// 上传图片帮助类
/// </summary>
public class PictureHelp
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
}
