using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace Book.UI.Settings.BasicData
{
    public partial class BaseAttachmentView : DevExpress.XtraEditors.XtraForm
    {
        private IList<MailAccessoriesHelper> _mailAccessorList = new List<MailAccessoriesHelper>();
        private readonly string _invoiceType = string.Empty;
        private readonly string _invoiceid = string.Empty;
        public readonly string _ServerSavePath = string.Empty;

        public BaseAttachmentView()
        {
            InitializeComponent();
        }

        public BaseAttachmentView(string invoiceid, string invoiceType)
            : this()
        {
            this.Text = invoiceid + ",附件列表";

            if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["AllAttachment"] != null)
            {
                this._ServerSavePath = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["AllAttachment"].Value;

                if (Book.DA.SQLServer.Accessor.SQLConnectionType == 1)
                    this._ServerSavePath = Path.GetPathRoot(this._ServerSavePath) + "\\Mobe" + this._ServerSavePath.Substring(Path.GetPathRoot(this._ServerSavePath).Length);
                else if (Book.DA.SQLServer.Accessor.SQLConnectionType == 2)
                    this._ServerSavePath = Path.GetPathRoot(this._ServerSavePath) + "\\Ansico" + this._ServerSavePath.Substring(Path.GetPathRoot(this._ServerSavePath).Length);
                else if (Book.DA.SQLServer.Accessor.SQLConnectionType == 3)
                    this._ServerSavePath = Path.GetPathRoot(this._ServerSavePath) + "\\Ansico-Earplugs" + this._ServerSavePath.Substring(Path.GetPathRoot(this._ServerSavePath).Length);
            }

            this._invoiceid = invoiceid;
            this._invoiceType = invoiceType;
        }

        private void BaseAttachmentView_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ConstructAccessoriesData();
        }

        //获得所有附件列表
        private void ConstructAccessoriesData()
        {
            this._mailAccessorList.Clear();

            //查看附件,通过主键路径查找,如果没有则数据为空
            if (Directory.Exists(this._ServerSavePath + "\\" + this._invoiceType + "\\" + this._invoiceid))
            {
                string[] filenames = Directory.GetFiles(this._ServerSavePath + "\\" + this._invoiceType + "\\" + this._invoiceid);
                foreach (string fn in filenames)
                {
                    MailAccessoriesHelper mah = new MailAccessoriesHelper();
                    mah.FileName = fn.Substring(fn.LastIndexOf("\\") + 1);  //这里直接使用文件名.不包含路径
                    mah.FileFullName = fn;
                    FileInfo fi = new FileInfo(mah.FileFullName);
                    mah.FileSize = fi.Length.ToString();
                    mah.FileStatus = AttachmentStatus.Normal;
                    this._mailAccessorList.Add(mah);
                }
            }
            this.bindingSource1.DataSource = this._mailAccessorList;
            this.gridControl1.RefreshDataSource();
        }

        private void btn_AddNew_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\";  //初始化查询路径
            ofd.Multiselect = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string fn in ofd.FileNames)
                {
                    MailAccessoriesHelper mbh = new MailAccessoriesHelper();
                    mbh.FileFullName = fn;
                    mbh.FileName = fn.Substring(fn.LastIndexOf("\\") + 1);
                    FileInfo fi = new FileInfo(fn);
                    mbh.FileSize = fi.Length.ToString();
                    this._mailAccessorList.Add(mbh);

                    //操作附件
                    if (!string.IsNullOrEmpty(mbh.FileFullName))
                    {
                        string sfdir = this._ServerSavePath + "\\" + this._invoiceType + "\\" + this._invoiceid;
                        try
                        {
                            System.IO.Directory.CreateDirectory(sfdir);
                        }
                        catch (Exception ex)
                        { throw new Helper.MessageValueException(ex.Message); }

                        try
                        {
                            System.IO.File.Copy(fn, sfdir + "\\" + fn.Substring(fn.LastIndexOf("\\") + 1), true);
                        }
                        catch
                        {
                            byte[] data = File.ReadAllBytes(fn);
                            File.WriteAllBytes(sfdir + "\\" + fn.Substring(fn.LastIndexOf("\\") + 1), data);
                        }
                    }
                }
            }
            ConstructAccessoriesData();
        }

        //点击文件名称打开文件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            MailAccessoriesHelper mah = this.bindingSource1.Current as MailAccessoriesHelper;
            System.Diagnostics.Process.Start(mah.FileFullName);
        }

        //点击删除,删除文件
        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            MailAccessoriesHelper mah = this.bindingSource1.Current as MailAccessoriesHelper;
            if (DialogResult.OK == MessageBox.Show("是否刪除附件:" + mah.FileName, "提示", MessageBoxButtons.OKCancel))
            {
                System.IO.File.Delete(mah.FileFullName);
                (this.bindingSource1.DataSource as IList<MailAccessoriesHelper>).Remove(mah);
                this.gridControl1.RefreshDataSource();
            }
        }

        //下载文件
        private void repositoryItemHyperLinkEdit3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.Description = "請選擇保存目錄";
            if (DialogResult.OK == fbd.ShowDialog(this))
            {
                string saveFolder = fbd.SelectedPath;
                MailAccessoriesHelper mah = this.bindingSource1.Current as MailAccessoriesHelper;
                if (System.IO.File.Exists(saveFolder + "\\" + mah.FileName))
                {
                    MessageBox.Show("該文件在該目錄已存在！", this.Text, MessageBoxButtons.OK);
                    return;
                }
                System.IO.File.Copy(mah.FileFullName, saveFolder + "\\" + mah.FileName);
                MessageBox.Show("已下載至:" + saveFolder + "\\" + mah.FileName, this.Text, MessageBoxButtons.OK);
            }
        }
    }

    /// <summary>
    /// 邮件发送帮助类
    /// </summary>
    public class MailAccessoriesHelper
    {
        public Image FileImage
        {
            get
            {
                string extendname = this.FileName.Substring(this.FileName.LastIndexOf(".") + 1).ToLower();
                if (extendname.Equals("txt", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_txt;
                if (extendname.Equals("doc", StringComparison.OrdinalIgnoreCase) || extendname.Equals("docx", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_Word;
                if (extendname.Equals("ppt", StringComparison.OrdinalIgnoreCase) || extendname.Equals("pptx", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_PowerPoint;
                if (extendname.Equals("mdb", StringComparison.OrdinalIgnoreCase) || extendname.Equals("accdb", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_Access;
                if (extendname.Equals("xls", StringComparison.OrdinalIgnoreCase) || extendname.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_Excel;
                if (extendname.Equals("mp3", StringComparison.OrdinalIgnoreCase) || extendname.Equals("wma", StringComparison.OrdinalIgnoreCase) || extendname.Equals("wmv", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_muisc;
                if (extendname.Equals("avi", StringComparison.OrdinalIgnoreCase) || extendname.Equals("rmvb", StringComparison.OrdinalIgnoreCase) || extendname.Equals("flv", StringComparison.OrdinalIgnoreCase) || extendname.Equals("mkv", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_realplay;
                if (extendname.Equals("jpg", StringComparison.OrdinalIgnoreCase) || extendname.Equals("jpeg", StringComparison.OrdinalIgnoreCase) || extendname.Equals("bmp", StringComparison.OrdinalIgnoreCase) || extendname.Equals("gif", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_pic;
                if (extendname.Equals("pdf", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_pdf;

                return Properties.Resources.img_default;
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

        public string OptionActionDel
        {
            get { return "刪除"; }
        }

        public string OptionActionDown
        {
            get { return "下載"; }
        }

        public AttachmentStatus FileStatus { get; set; }
    }

    public enum AttachmentStatus
    {
        /// <summary>
        /// 一般状态
        /// </summary>
        Normal,
        /// <summary>
        /// 正在上传
        /// </summary>
        Uping,
        /// <summary>
        /// 正在下载
        /// </summary>
        Downing,
        /// <summary>
        /// 正在删除
        /// </summary>
        Deling
    }
}