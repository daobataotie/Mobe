using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.IO;

namespace Book.UI.produceManager.ExportSendMail
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        private const string _SendTimeOrStatus = "未發送";
        string _ServerSavePath = string.Empty;          //附件存放地址
        string _SMTPHost = string.Empty;                //提供发送邮件的邮件服务器地址
        string _SMTPUser = string.Empty;                //提供发送邮件服务用户
        string _SMTPPassword = string.Empty;            //提供发送邮件服务用户密码
        string _DefSender = string.Empty;               //默认发件人名称
        string _DefSenderAddress = string.Empty;        //默认发件人地址
        Hashtable htFormName = new Hashtable();
        Model.PCExportReportANSI _pcExport = new Book.Model.PCExportReportANSI();
        BL.PCExportReportANSIDetailManager _pcExprotANSIDetailManager = new Book.BL.PCExportReportANSIDetailManager();
        BL.ExportSendMailManager _ESMManager = new Book.BL.ExportSendMailManager();
        Model.ExportSendMail _ESM = null;
        IList<MailAccessoriesHelper> _mailAccessorList = new List<MailAccessoriesHelper>();

        public EditForm()
        {
            InitializeComponent();

            //单据类别集合
            htFormName.Add("MSJY", "目視檢驗");
            htFormName.Add("QXD", "清晰度");
            htFormName.Add("LJDS", "棱鏡度數&&棱鏡平衡度數");
            htFormName.Add("KJGTSL", "可見光透視率");
            htFormName.Add("ZWXTSL", "紫外線透視率");
            htFormName.Add("QMDS", "球面度數");
            htFormName.Add("SGDS", "散光度數");
            htFormName.Add("GSCJCS", "高速衝擊測試");
            htFormName.Add("YZZLZJCS", "圓錐墜落撞擊測試");
            htFormName.Add("JPCTCS", "鏡片穿透測試");
            htFormName.Add("WDCS", "霧度測試");
            htFormName.Add("NRXCS", "耐燃性測試");

            //取得服务器附件存储地址
            if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"] != null)
            {
                this._ServerSavePath = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"].Value;
                this._SMTPHost = System.Configuration.ConfigurationManager.AppSettings["SMTPHost"];
                this._SMTPUser = System.Configuration.ConfigurationManager.AppSettings["SMTPUser"];
                this._SMTPPassword = System.Configuration.ConfigurationManager.AppSettings["SMTPPassword"];
                this._DefSender = System.Configuration.ConfigurationManager.AppSettings["DefSender"];
                this._DefSenderAddress = System.Configuration.ConfigurationManager.AppSettings["DefSenderAddress"];
            }

            this.action = "view";
        }

        protected override void AddNew()
        {
            this._ESM = new Book.Model.ExportSendMail();
            this._ESM.ExportSendMailId = this._ESMManager.GetId();
            this._ESM.SenderMail = this._DefSenderAddress;
            this._ESM.ExportSendMailDate = DateTime.Now;

            //清除原有
            this._pcExport = null;
            this._mailAccessorList.Clear();
            this.gridControl1.RefreshDataSource();
        }

        protected override void Delete()
        {
            if (this._ESM == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._ESMManager.Delete(this._ESM.ExportSendMailId);

            this._ESM = this._ESMManager.GetNext(this._ESM);

            if (this._ESM == null)
            {
                this._ESM = this._ESMManager.GetLast();
            }
        }

        protected override void MoveLast()
        {
            this._ESM = this._ESMManager.GetLast();
        }

        protected override void MoveFirst()
        {
            this._ESM = this._ESMManager.GetFirst();
        }

        protected override bool HasRows()
        {
            return this._ESMManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._ESMManager.HasRowsAfter(this._ESM);
        }

        protected override bool HasRowsPrev()
        {
            return this._ESMManager.HasRowsBefore(this._ESM);
        }

        protected override void MoveNext()
        {
            Model.ExportSendMail esm = this._ESMManager.GetNext(this._ESM);
            if (esm == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._ESM = esm;

        }

        protected override void MovePrev()
        {
            Model.ExportSendMail esm = this._ESMManager.GetPrev(this._ESM);
            if (esm == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._ESM = esm;
        }

        public override void Refresh()
        {
            if (this._ESM == null)
            {
                this.AddNew();
                this.action = "insert";
            }

            this.txtExportSendMailId.Text = this._ESM.ExportSendMailId;
            this.txtExportId.Text = this._ESM.ExportId;
            this.txtExportType.Text = this._ESM.ExportType;
            this.txtExportSendMailDate.Text = this._ESM.ExportSendMailDate.HasValue ? this._ESM.ExportSendMailDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : _SendTimeOrStatus;
            this.txtMailSubject.Text = this._ESM.MailSubject;
            this.txtMailContent.Text = this._ESM.MailContent;
            this.txtSenderMail.Text = this._ESM.SenderMail;
            this.txtReceiverMail.Text = this._ESM.ReceiverMail;

            if (!string.IsNullOrEmpty(this._ESM.ExportId))
                this._pcExport = new BL.PCExportReportANSIManager().Get(this._ESM.ExportId);
            else
                this._pcExport = null;
            //获取附件列表
            this.ConstructAccessoriesData();

            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.barBtn_ChooseExp.Enabled = true;
                    this.barBtn_Search.Enabled = false;
                    break;
                case "update":
                    this.barBtn_ChooseExp.Enabled = true;
                    this.barBtn_Search.Enabled = false;
                    break;
                case "view":
                    this.barBtn_ChooseExp.Enabled = false;
                    this.barBtn_Search.Enabled = true;
                    break;
            }


            this.txtExportSendMailId.Enabled = false;
            this.txtExportSendMailDate.Enabled = false;
            this.txtExportType.Enabled = false;
            this.txtExportId.Enabled = false;
            this.txtSenderMail.Enabled = false;
            this.btn_Send.Enabled = true;
            this.gridView1.OptionsBehavior.Editable = true;

            if (this.action != "view")
            {
                MailAccessoriesHelper mah = this.bindingSource1.Current as MailAccessoriesHelper;
                if (mah != null)
                {
                    if (mah.FromExpType == "郵件本身附件")
                        this.btnDelAccessories.Enabled = true;
                    else
                        this.btnDelAccessories.Enabled = false;
                }
            }
        }

        protected override void Save()
        {
            this._ESM.ExportSendMailId = this.txtExportSendMailId.Text;
            this._ESM.ExportId = this.txtExportId.Text;
            this._ESM.ExportType = this.txtExportType.Text;
            try
            {
                this._ESM.ExportSendMailDate = DateTime.Parse(this.txtExportSendMailDate.Text);
            }
            catch
            {
                this._ESM.ExportSendMailDate = null;
            }
            this._ESM.MailSubject = this.txtMailSubject.Text;
            this._ESM.MailContent = this.txtMailContent.Text;
            this._ESM.SenderMail = this.txtSenderMail.Text;
            this._ESM.ReceiverMail = this.txtReceiverMail.Text;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            //构建附件列表
            if (this._mailAccessorList != null && this._mailAccessorList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (MailAccessoriesHelper mah in this._mailAccessorList)
                    sb.Append(mah.FileFullName + "|"); //这里使用全路径名称.如果是新上传那就是本地路径,若是已有文件,就是服务器路径
                this._ESM.AccessoriesList = sb.ToString().Substring(0, sb.ToString().Length - 1);
            }
            else
            {
                this._ESM.AccessoriesList = "";
            }

            switch (this.action)
            {
                case "insert":
                    this._ESMManager.Insert(this._ESM);
                    break;
                case "update":
                    this._ESMManager.Update(this._ESM);
                    break;
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "確認發送郵件?", "確認操作", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string _ReceiveAddress = this.txtReceiverMail.Text;
                string _MailSubject = this.txtMailSubject.Text;
                string _MailContent = this.txtMailContent.Text;
                string _Accessories = string.Empty;
                StringBuilder sb = new StringBuilder();
                if (this._mailAccessorList != null && this._mailAccessorList.Count > 0)
                {
                    foreach (MailAccessoriesHelper mah in this._mailAccessorList)
                    {
                        if (mah.IsCheck)
                            sb.Append(mah.FileFullName + ";");
                    }
                }
                if (!string.IsNullOrEmpty(sb.ToString()))
                    _Accessories = sb.ToString().Substring(0, sb.ToString().Length - 1);

                if (string.IsNullOrEmpty(_ReceiveAddress))
                {
                    MessageBox.Show("請填寫收件人郵箱");
                    return;
                }

                if (string.IsNullOrEmpty(_MailSubject))
                {
                    MessageBox.Show("請填寫郵件主題");
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(_ReceiveAddress, @"^([a-zA-Z0-9]|[._])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])"))
                {
                    MessageBox.Show("收件人郵箱格式有誤.");
                    return;
                }

                this.SendMail(this._DefSenderAddress, this._DefSender, _ReceiveAddress, "", _MailSubject, _MailContent, _Accessories, this._SMTPHost, this._SMTPUser, this._SMTPPassword);
            }
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SendMailList f = new SendMailList();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.ExportSendMail mESM = f.SelectItem as Model.ExportSendMail;
                if (mESM != null)
                {
                    this._ESM = mESM;
                    this.Refresh();
                }
            }
            f.Dispose();
            GC.Collect();
        }

        private void barBtn_ChooseExp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseExport f = new ChooseExport();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCExportReportANSI currentModel = f.SelectItem as Model.PCExportReportANSI;
                if (currentModel != null)
                {
                    this._pcExport = currentModel;
                    //this.Refresh();
                    this.txtExportId.Text = this._pcExport.ExportReportId;
                    this.txtExportType.Text = this._pcExport.ExpType;
                    this.txtReceiverMail.Text = this._pcExport.Customer == null ? "" : this._pcExport.Customer.CustomerEMail;
                    this.ConstructAccessoriesData();
                }
            }
            f.Dispose();
            GC.Collect();
        }

        private void btnAddAccessories_Click(object sender, EventArgs e)
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
                    mbh.FromExpType = "郵件本身附件";
                    this._mailAccessorList.Add(mbh);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void btnDelAccessories_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                if (this._mailAccessorList.Count > 0)
                {
                    this._mailAccessorList.Remove(this.bindingSource1.Current as MailAccessoriesHelper);
                    this.gridControl1.RefreshDataSource();
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ErrorNoMoreRows);
                }
            }
        }

        //获得所有附件列表
        private void ConstructAccessoriesData()
        {
            this._mailAccessorList.Clear();
            string[] hasAccessories = null;     //记录已发送附件
            if (!string.IsNullOrEmpty(this._ESM.AccessoriesList))
                hasAccessories = this._ESM.AccessoriesList.Split(new char[] { ';' });


            if (this._pcExport != null)
            {


                IList<Model.PCExportReportANSIDetail> details = this._pcExprotANSIDetailManager.SelectByCusXoIdAndProId(this._pcExport.InvoiceCusXOId, this._pcExport.ProductId);

                #region 获得子报表附件
                if (details != null && details.Count > 0)
                {
                    foreach (Model.PCExportReportANSIDetail d in details)
                    {
                        //查看附件,通过主键路径查找,如果没有则数据为空
                        if (Directory.Exists(this._ServerSavePath + "\\" + d.PCExportReportANSIDetailId))
                        {
                            string[] filenames = Directory.GetFiles(this._ServerSavePath + "\\" + d.PCExportReportANSIDetailId);
                            foreach (string fn in filenames)
                            {
                                MailAccessoriesHelper mah = new MailAccessoriesHelper();
                                mah.FromExpType = this.htFormName[d.FromPC].ToString();     //附件来源单据
                                mah.FileName = fn.Substring(fn.LastIndexOf("\\") + 1);  //这里直接使用文件名.不包含路径
                                mah.FileFullName = fn;
                                FileInfo fi = new FileInfo(mah.FileFullName);
                                mah.FileSize = fi.Length.ToString();
                                //判断是否选中
                                if (hasAccessories != null && hasAccessories.Length != 0)
                                {
                                    foreach (string s in hasAccessories)
                                        if (mah.IsCheck = s.Equals(mah.FileName, StringComparison.OrdinalIgnoreCase))
                                            break;
                                }
                                this._mailAccessorList.Add(mah);
                            }
                        }
                    }
                }
                #endregion
            }


            #region 获取外销报告打印附件及发送邮件本身附件
            IList<string> otherFile = new List<string>();
            if (!string.IsNullOrEmpty(this._ESM.ExportId))   //外销报告单号
                otherFile.Add(this._ESM.ExportId);
            if (this._ESM != null && !string.IsNullOrEmpty(this._ESM.ExportSendMailId)) //邮件附件地址
                otherFile.Add(this._ESM.ExportSendMailId);
            foreach (string of in otherFile)
            {
                //查看附件,通过主键路径查找,如果没有则数据为空
                if (Directory.Exists(this._ServerSavePath + "\\" + of))
                {
                    string[] filenames = Directory.GetFiles(this._ServerSavePath + "\\" + of);
                    foreach (string fn in filenames)
                    {
                        MailAccessoriesHelper mah = new MailAccessoriesHelper();
                        mah.FromExpType = of.Contains("ESM") ? "郵件本身附件" : this._pcExport.ExpType;//附件来源单据
                        mah.FileName = fn.Substring(fn.LastIndexOf("\\") + 1);  //这里直接使用文件名.不包含路径
                        mah.FileFullName = fn;
                        FileInfo fi = new FileInfo(mah.FileFullName);
                        mah.FileSize = fi.Length.ToString();
                        //判断是否选中
                        if (hasAccessories != null && hasAccessories.Length != 0)
                            foreach (string s in hasAccessories)
                                if (mah.IsCheck = s.Equals(mah.FileName, StringComparison.OrdinalIgnoreCase))
                                    break;
                        this._mailAccessorList.Add(mah);
                    }
                }
            }
            #endregion

            this.bindingSource1.DataSource = this._mailAccessorList;
            this.gridControl1.RefreshDataSource();

        }

        /// <summary>
        /// 邮件发送方法
        /// </summary>
        /// <param name="SendAddress">发件人邮箱</param>
        /// <param name="Sender">发件人称谓</param>
        /// <param name="ReceiveAddress">收件人邮箱</param>
        /// <param name="Receiver">收件人称谓</param>
        /// <param name="Subject">邮件主题</param>
        /// <param name="Content">邮件类容</param>
        /// <param name="Accessories">邮件附件</param>
        /// <param name="SMTPHost">SMTP服务器</param>
        /// <param name="SMTPUser">SMTP发送邮箱</param>
        /// <param name="SMTPPassword">SMTP发送邮箱密码</param>
        /// <returns></returns>
        private bool SendMail(string SendAddress, string Sender, string ReceiveAddress, string Receiver, string Subject, string Content, string Accessories, string SMTPHost, string SMTPUser, string SMTPPassword)
        {
            //设置发送地址,接受地址
            MailAddress from = new MailAddress(SendAddress, Sender);
            MailAddress to = new MailAddress(ReceiveAddress, Receiver);

            //创建邮件对象
            MailMessage _mail = new MailMessage(from, to);
            _mail.Subject = Subject;
            _mail.Body = Content;
            _mail.IsBodyHtml = false;       //设置邮件正文是否为html格式
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.Priority = MailPriority.High;     //设置发送邮件优先级
            if (!string.IsNullOrEmpty(Accessories))
            {
                foreach (string mfile in Accessories.Split(new char[] { ';' }))
                {
                    _mail.Attachments.Add(new Attachment(mfile));
                }
            }

            //发送邮件
            SmtpClient client = new SmtpClient(SMTPHost);       //使用指定的SMTP服务器来发送邮件
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;     //通过什么方式发送邮件.
            client.Timeout = 600000;        //超时时间为10分钟

            try
            {
                client.Send(_mail);
                MessageBox.Show("郵件發送成功!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                _mail.Dispose();
            }
        }

        //点击附件名称,打开附件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            MailAccessoriesHelper mah = this.bindingSource1.Current as MailAccessoriesHelper;
            System.Diagnostics.Process.Start(mah.FileFullName);
        }

        //选择附件,判定是否可移除
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            if (this.action != "view")
            {
                MailAccessoriesHelper mah = this.bindingSource1.Current as MailAccessoriesHelper;
                if (mah.FromExpType == "郵件本身附件")
                    this.btnDelAccessories.Enabled = true;
                else
                    this.btnDelAccessories.Enabled = false;
            }
        }
    }

    /// <summary>
    /// 邮件发送帮助类
    /// </summary>
    public class MailAccessoriesHelper
    {
        public bool IsCheck { get; set; }

        public string FromExpType { get; set; }

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
    }
}