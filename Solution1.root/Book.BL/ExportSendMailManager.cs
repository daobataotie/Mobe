//------------------------------------------------------------------------------
//
// file name：ExportSendMailManager.cs
// author: mayanjun
// create date：2012-6-21 10:58:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ExportSendMail.
    /// </summary>
    public partial class ExportSendMailManager : BaseManager
    {

        public string ServerSavePath
        {
            get
            {
                string s = string.Empty;
                //取得服务器附件存储地址
                if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"] != null)
                {
                    s = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"].Value;
                }
                return s;
            }
        }

        public void Delete(string exportSendMail)
        {
            accessor.Delete(exportSendMail);

            //删除附件
            string sfdir = this.ServerSavePath + "\\" + exportSendMail;
            if (System.IO.Directory.Exists(sfdir))
                System.IO.Directory.Delete(sfdir, true);
        }

        public void Insert(Model.ExportSendMail exportSendMail)
        {
            Validate(exportSendMail);
            try
            {
                BL.V.BeginTransaction();

                TiGuiExists(exportSendMail);

                exportSendMail.InsertTime = DateTime.Now;
                exportSendMail.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, exportSendMail.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, exportSendMail.InsertTime.Value.Year, exportSendMail.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, exportSendMail.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(exportSendMail);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

            //操作附件
            if (!string.IsNullOrEmpty(exportSendMail.AccessoriesList))
            {
                string sfdir = this.ServerSavePath + "\\" + exportSendMail.ExportSendMailId;
                try
                {
                    System.IO.Directory.CreateDirectory(sfdir);
                }
                catch (Exception ex)
                { throw new Helper.MessageValueException(ex.Message); }
                foreach (string fn in exportSendMail.AccessoriesList.Split('|'))
                {
                    if (!fn.Contains(this.ServerSavePath))
                    {
                        System.IO.File.Copy(fn, sfdir + "\\" + fn.Substring(fn.LastIndexOf("\\") + 1), true);
                    }
                }
            }
        }

        public void Update(Model.ExportSendMail exportSendMail)
        {
            if (exportSendMail != null)
            {
                Validate(exportSendMail);

                exportSendMail.UpdateTime = DateTime.Now;
                accessor.Update(exportSendMail);

                //上传修改文件
                string sfdir = this.ServerSavePath + "\\" + exportSendMail.ExportSendMailId;
                if (!System.IO.Directory.Exists(sfdir))
                    System.IO.Directory.CreateDirectory(sfdir);

                string[] newfilenames = exportSendMail.AccessoriesList.Split('|');    //上传而来
                string[] hasfilenames = System.IO.Directory.GetFiles(sfdir);    //原本有的

                //--添加新附件
                if (!string.IsNullOrEmpty(exportSendMail.AccessoriesList))
                {
                    foreach (string newfile in newfilenames)
                    {
                        if (newfile.Contains(this.ServerSavePath))
                            continue;
                        bool flag = true;
                        foreach (string hasfile in hasfilenames)
                        {
                            //if (hasfile.Equals(newfile.Substring(newfile.LastIndexOf("\\") + 1), StringComparison.OrdinalIgnoreCase))
                            if (hasfile.Equals(newfile, StringComparison.OrdinalIgnoreCase))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag && newfile != "")
                            System.IO.File.Copy(newfile, sfdir + "\\" + newfile.Substring(newfile.LastIndexOf("\\") + 1), true);
                    }
                }

                //删除旧附件
                foreach (string hasfile in hasfilenames)
                {
                    //if (hasfile.Contains(this.ServerSavePath))
                    //    continue;
                    bool flag = true;
                    foreach (string newfile in newfilenames)
                    {
                        string IstrueName = newfile.Substring(newfile.LastIndexOf("\\") + 1);
                        if (IstrueName.Equals(hasfile.Substring(hasfile.LastIndexOf("\\") + 1), StringComparison.OrdinalIgnoreCase))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        System.IO.File.Delete(hasfile);
                }

            }

        }

        protected override string GetSettingId()
        {
            return "ESMRule";
        }

        protected override string GetInvoiceKind()
        {
            return "ESM";
        }

        private void TiGuiExists(Model.ExportSendMail model)
        {
            if (this.ExistsPrimary(model.ExportSendMailId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.InsertTime.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.ExportSendMailId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.ExportSendMail mPCExpANSI)
        {
            if (string.IsNullOrEmpty(mPCExpANSI.ExportSendMailId))
                throw new Helper.RequireValueException(Model.ExportSendMail.PRO_ExportSendMailId);
        }

        public IList<Model.ExportSendMail> SelectByDateRage(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRage(startdate, enddate);
            //return accessor.SelectByDateRage(startdate, enddate, FromPC);
            return null;
        }
    }
}

