using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.IO;

using Book.UI.Settings.BasicData.Supplier;
using Book.UI.Invoices.CO;

namespace Book.UI
{
    public partial class MailForm : DevExpress.XtraEditors.XtraForm
    {
        private MailMessage mail = null;
        private IList<FileClass> list = null;
        SmtpClient client;
        private BL.ProductImageManager _productImage = new BL.ProductImageManager();
        public MailForm()
        {
            InitializeComponent();
            CreateFiles(Application.StartupPath + @"\Temp");
            this.newSupplierMail.Choose = new ChooseSupplier();
            mail = new MailMessage();
            list = new List<FileClass>();
            this.bindingSourceFiles.DataSource = list;
        }

        public static void CreateFiles(string strDir)
        {
            if (Directory.Exists(strDir))
            {
                try
                {
                    Directory.Delete(strDir, true);
                    Directory.CreateDirectory(strDir);
                }
                catch { }
            }
            else
            {
                Directory.CreateDirectory(strDir);
            }
        }


        private void textUserMail_Leave(object sender, EventArgs e)
        {
            this.ValidMail(this.textUserMail);
        }

        private void ValidMail(TextEdit con)
        {
            this.ErrorSupplierMail.ClearErrors();
            if (!System.Text.RegularExpressions.Regex.IsMatch(con.Text, @"^([a-zA-Z0-9]|[._])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])"))
            {
                this.ErrorSupplierMail.SetError(con, "郵件格式不正確！");
                //con.Focus();
                con.SelectAll();
            }
        }

        private void textSupplierMail_Leave(object sender, EventArgs e)
        {
            this.ValidMail(this.textSupplierMail);
        }

        private void simpleSend_Click(object sender, EventArgs e)
        {
            try
            {
                mail.From = new MailAddress(this.textUserMail.Text);
                client = new SmtpClient();
                switch (mail.From.Host)
                {
                     
                    case "qq.com":
                        client.Host = "smtp.qq.com";
                        client.Port = 25;
                        break;
                    case "126.com":
                        client.Host = "smtp.126.com";
                        client.Port = 25;
                        break;
                    case "163.com":
                        client.Host = "smtp.163.com";
                        client.Port = 25;
                        break;
                    case "gmail.com":
                        client.Host = "smtp.gmail.com";
                        client.Port = 587;
                        client.EnableSsl = true;
                        break;
                    case "yahoo.com":
                        client.Host = "smtp.yahoo.com";
                        client.Port = 465;
                        client.EnableSsl = true;
                        break;                  
                    default:
                        client.Host = "smtp.163.com";
                        client.Port = 25;
                        break;
                }
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                mail.Attachments.Clear();
                foreach (FileClass item in list)
                {
                    //FileInfo f = new FileInfo(item.filePath);
                    //string ext = f.Extension.ToLower();
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
                    switch (item.Extension)
                    {
                        case ".rar":
                        case ".zip":
                            ct.MediaType = System.Net.Mime.MediaTypeNames.Application.Zip;
                            if (item.Desc == null || item.Desc == "")
                                ct.Name = item.fileName + item.Extension;
                            else
                                ct.Name = item.Desc + item.Extension;
                            mail.Attachments.Add(new Attachment(item.filePath, ct));
                            break;
                        default:
                            ct.MediaType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                            if (item.Desc == null || item.Desc == "")
                                ct.Name = item.fileName + item.Extension;
                            else
                                ct.Name = item.Desc + item.Extension;
                            mail.Attachments.Add(new Attachment(item.filePath, ct));
                            break;
                    }
                }
                client.Credentials = new System.Net.NetworkCredential(this.textUserMail.Text, this.textUserPass.Text);
                client.Timeout = int.MaxValue;
                mail.ReplyTo = new MailAddress(this.textUserMail.Text);
                mail.IsBodyHtml = true;
                mail.Sender = new MailAddress(this.textUserMail.Text);
                mail.Priority = MailPriority.High;
                mail.Subject = this.textTitle.Text;
                mail.BodyEncoding = System.Text.Encoding.UTF8; ;
                mail.To.Add(this.textSupplierMail.Text);
                string bodystr = this.richContent.Text;
                bodystr = bodystr.Replace("\n", "<br />");
                mail.Body = bodystr;
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                client.Send(mail);
                MessageBox.Show("郵件發送成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleAdd_Click(object sender, EventArgs e)
        {
            FileClass fc = null;
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Filter = "文本文件(*.txt)|*.txt|图片文件(*.jpg)|*.jpg|压缩文件(*.zip)|*.zip|所有文件（*.*）|*.*";
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] filenames = this.openFileDialog.FileNames;
                for (int i = 0; i < filenames.Length; i++)
                {
                    FileInfo f = new FileInfo(filenames[i]);
                    string ext = f.Extension.ToLower();
                    switch (ext)
                    {
                        //case ".rtf":
                        //case ".pdf":
                        //case ".txt":
                        //case ".doc":
                        //case ".cs":                            
                        case ".jpg":
                            fc = new FileClass();
                            fc.filePath = filenames[i];
                            fc.fileName = f.Name;
                            fc.size = f.Length + "字節";
                            fc.Desc = f.Name;
                            fc.Extension = ext;
                            fc.image = Image.FromFile(filenames[i]);
                            list.Add(fc);
                            break;
                        default:
                            fc = new FileClass();
                            fc.filePath = filenames[i];
                            fc.fileName = f.Name;
                            fc.Extension = ext;
                            fc.size = f.Length + "字節";
                            list.Add(fc);
                            break;
                    }
                }
                this.gridControl1.RefreshDataSource();
            }

        }

        class FileClass
        {
            public string filePath { get; set; }
            public string fileName { get; set; }
            public string size { get; set; }
            public Image image { get; set; }
            public string Desc { get; set; }
            public string Extension { get; set; }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            FileClass temp = this.bindingSourceFiles.Current as FileClass;
            if (temp != null)
            {
                mail.Attachments.RemoveAt(list.IndexOf(temp));
                list.Remove(temp);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleProduct_Click(object sender, EventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Model.Product product in Book.UI.Invoices.ChooseProductForm.ProductList)
                {
                    if (product == null) continue;
                    IList<Model.ProductImage> productImages = this._productImage.Select(product);
                    foreach (Model.ProductImage item in productImages)
                    {
                        if (item.Images.Length == 0) continue;

                        #region 展示數據

                        MemoryStream ms = new MemoryStream(item.Images);
                        Image image = Image.FromStream(ms);
                        string path = Application.StartupPath + @"\Temp\" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString() + (string.IsNullOrEmpty(item.SuffixalName) ? ".jpg" : item.SuffixalName);
                        image.Save(path);
                        FileClass fc = new FileClass();
                        fc.filePath = path;
                        fc.fileName = product.ProductName;
                        fc.size = item.Images.Length + "字节";
                        fc.image = image;
                        fc.Extension =string.IsNullOrEmpty(item.SuffixalName) ? ".jpg" : item.SuffixalName;
                        list.Add(fc);

                        #endregion

                        //System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
                        //ct.MediaType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                        //ct.Name = item.Description + ".jpg";
                        //Attachment at = new Attachment(ms, ct);
                        //System.Net.Mime.ContentDisposition disposition = at.ContentDisposition;
                        //disposition.ModificationDate = item.InsertTime.Value;
                        //disposition.ReadDate = System.DateTime.Now;
                        //mail.Attachments.Add(at);

                    }
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleOrder_Click(object sender, EventArgs e)
        {
            ChooseInvoiceForm f = new ChooseInvoiceForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceCO temp = f.SelectedItem as Model.InvoiceCO;
                foreach (Model.InvoiceCODetail detail in new BL.InvoiceCODetailManager().Select(temp))
                {
                    if (detail.Product == null) continue;
                    IList<Model.ProductImage> productImages = this._productImage.Select(detail.Product);
                    foreach (Model.ProductImage item in productImages)
                    {
                        if (item.Images.Length == 0) continue;

                        #region 展示數據

                        MemoryStream ms = new MemoryStream(item.Images);
                        Image image = Image.FromStream(ms);
                        string path = Application.StartupPath + @"\Temp\" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString() + (item.SuffixalName == "" ? ".jpg" : item.SuffixalName);
                        image.Save(path);
                        FileClass fc = new FileClass();
                        fc.filePath = path;
                        fc.fileName = detail.Product.ProductName;
                        fc.size = item.Images.Length + "字节";
                        fc.image = image;
                        fc.Extension = string.IsNullOrEmpty(item.SuffixalName) ? ".jpg" : item.SuffixalName;
                        list.Add(fc);

                        #endregion
                    }
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleRevrite_Click(object sender, EventArgs e)
        {
            mail = new MailMessage();
            this.textSupplierMail.Text = "";
            this.textTitle.Text = "";
            this.richContent.Text = "";
            list.Clear();
            this.gridControl1.RefreshDataSource();
        }

        //private void newSupplierMail_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (this.newSupplierMail.EditValue == null) return;
        //    this.textSupplierMail.Text = (this.newSupplierMail.EditValue as Book.Model.Supplier).Email;
        //    mail.To.Clear();
        //    mail.To.Add(this.textSupplierMail.Text);
        //}

        private void newSupplierMail_EditValueChanging()
        {
            if (this.newSupplierMail.EditValue == null) return;
            this.textSupplierMail.Text = (this.newSupplierMail.EditValue as Book.Model.Supplier).Email;
            //mail.To.Clear();
            //mail.To.Add(this.textSupplierMail.Text);
        }
    }
}