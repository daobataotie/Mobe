//------------------------------------------------------------------------------
//
// file name：PCExportReportANSIDetailManager.cs
// author: mayanjun
// create date：2012-6-13 14:02:25
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCExportReportANSIDetail.
    /// </summary>
    public partial class PCExportReportANSIDetailManager : BaseManager
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

        public void Delete(Model.PCExportReportANSIDetail pCExportReportANSIDetail)
        {
            this.Delete(pCExportReportANSIDetail.PCExportReportANSIDetailId);
        }
        

        public void Delete(string pCExportReportANSIDetailId)
        {
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(pCExportReportANSIDetailId);

                //删除附件
                string sfdir = this.ServerSavePath + "\\" + pCExportReportANSIDetailId;
                if (System.IO.Directory.Exists(sfdir))
                    System.IO.Directory.Delete(sfdir, true);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Insert(Model.PCExportReportANSIDetail pCExportReportANSIDetail)
        {
            Validate(pCExportReportANSIDetail);

            try
            {
                BL.V.BeginTransaction();

                pCExportReportANSIDetail.InsertTime = DateTime.Now;
                pCExportReportANSIDetail.UpdateTime = DateTime.Now;

                TiGuiExists(pCExportReportANSIDetail);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCExportReportANSIDetail.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCExportReportANSIDetail.InsertTime.Value.Year, pCExportReportANSIDetail.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCExportReportANSIDetail.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCExportReportANSIDetail);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
            }

            //上传附件
            if (!string.IsNullOrEmpty(pCExportReportANSIDetail.accessoriesList))
            {
                string sfdir = this.ServerSavePath + "\\" + pCExportReportANSIDetail.PCExportReportANSIDetailId;
                try
                {
                    System.IO.Directory.CreateDirectory(sfdir);     //创建上传目录
                }
                catch (Exception ex)
                { throw new Helper.MessageValueException(ex.Message); }
                foreach (string fn in pCExportReportANSIDetail.accessoriesList.Split('|'))
                {
                    System.IO.File.Copy(fn, sfdir + "\\" + fn.Substring(fn.LastIndexOf("\\") + 1), true);
                }
            }
        }

        public void Update(Model.PCExportReportANSIDetail pCExportReportANSIDetail)
        {
            if (pCExportReportANSIDetail != null)
            {
                Validate(pCExportReportANSIDetail);
                pCExportReportANSIDetail.UpdateTime = DateTime.Now;

                accessor.Update(pCExportReportANSIDetail);

                //上传修改文件
                string sfdir = this.ServerSavePath + "\\" + pCExportReportANSIDetail.PCExportReportANSIDetailId;
                if (!System.IO.Directory.Exists(sfdir))
                    System.IO.Directory.CreateDirectory(sfdir);

                string[] newfilenames = pCExportReportANSIDetail.accessoriesList.Split('|');    //上传而来
                string[] hasfilenames = System.IO.Directory.GetFiles(sfdir);    //原本有的

                if (!string.IsNullOrEmpty(pCExportReportANSIDetail.accessoriesList))
                {
                    //--添加新附件
                    foreach (string newfile in newfilenames)
                    {
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
                        if (flag)
                            System.IO.File.Copy(newfile, sfdir + "\\" + newfile.Substring(newfile.LastIndexOf("\\") + 1), true);
                    }
                }

                //删除旧附件
                foreach (string hasfile in hasfilenames)
                {
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

        private void TiGuiExists(Model.PCExportReportANSIDetail model)
        {
            if (this.ExistsPrimary(model.PCExportReportANSIDetailId))
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
                model.PCExportReportANSIDetailId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.PCExportReportANSIDetail pcExp)
        {
            if (string.IsNullOrEmpty(pcExp.PCExportReportANSIDetailId))
                throw new Helper.RequireValueException(Model.PCExportReportANSIDetail.PRO_PCExportReportANSIDetailId);
            if (string.IsNullOrEmpty(pcExp.CheckDate.ToString()))
                throw new Helper.RequireValueException(Model.PCExportReportANSIDetail.PRO_CheckDate);
            if (string.IsNullOrEmpty(pcExp.InvoiceCusXOId))
                throw new Helper.RequireValueException(Model.PCExportReportANSIDetail.PRO_InvoiceCusXOId);
            if (!pcExp.MustCheckSum.HasValue)
                throw new Helper.RequireValueException(Model.PCExportReportANSIDetail.PRO_MustCheckSum);
            if (!pcExp.HasCheckSum.HasValue)
                throw new Helper.RequireValueException(Model.PCExportReportANSIDetail.PRO_HasCheckSum);
            if (pcExp.HasCheckSum.Value < 0)
                throw new Helper.InvalidValueException(Model.PCExportReportANSIDetail.PRO_HasCheckSum);
            if (pcExp.PassSum.Value < 0)
                throw new Helper.InvalidValueException(Model.PCExportReportANSIDetail.PRO_PassSum);
        }

        protected override string GetSettingId()
        {
            return "pCExpANSIDRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pCExpANSID";
        }

        public IList<Model.PCExportReportANSIDetail> SelectByDateRage(DateTime startdate, DateTime enddate, string FromPC)
        {
            return accessor.SelectByDateRage(startdate, enddate, FromPC);
        }

        public IList<Model.PCExportReportANSIDetail> SelectByCondition(DateTime startdate, DateTime enddate, string CusInvoiceXOId, Model.Product product, string pcExpType)
        {
            return accessor.SelectByCondition(startdate, enddate, CusInvoiceXOId, product, pcExpType);
        }

        public Model.PCExportReportANSIDetail SelectForExpANSIDetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            return accessor.SelectForExpANSIDetailsSUM(InvoiceCusXoId, ProductId);
        }

        public Model.PCExportReportANSIDetail SelectForExpCSADetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            return accessor.SelectForExpCSADetailsSUM(InvoiceCusXoId, ProductId);
        }

        public Model.PCExportReportANSIDetail SelectForExpCEENDetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            return accessor.SelectForExpCEENDetailsSUM(InvoiceCusXoId, ProductId);
        }

        public Model.PCExportReportANSIDetail SelectForExpASDetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            return accessor.SelectForExpASDetailsSUM(InvoiceCusXoId, ProductId);
        }

        public Model.PCExportReportANSIDetail SelectForExpJISDetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            return accessor.SelectForExpJISDetailsSUM(InvoiceCusXoId, ProductId);
        }

        public IList<Model.PCExportReportANSIDetail> SelectByCusXoIdAndProId(string InvoiceCusXoId, string ProductId)
        {
            return accessor.SelectByCusXoIdAndProId(InvoiceCusXoId, ProductId);
        }

        public int HasCheckSum(string InvoiceCusXOId, string ProductId, string FromPC)
        {
            return accessor.HasCheckSum(InvoiceCusXOId, ProductId, FromPC);
        }

        public IList<Model.PCExportReportANSIDetail> SelectAllDetail(DateTime startDate, DateTime endDate, string InvoiceCusXoId, string ProductId, string CustomerId, string type)
        {
            return accessor.SelectAllDetail(startDate, endDate, InvoiceCusXoId, ProductId, CustomerId, type);
        }

        #region 分类构建
        public Model.PCExportReportANSIDetail mGetFirst(string FromPC)
        {
            return accessor.mGetFirst(FromPC);
        }

        public Model.PCExportReportANSIDetail mGetLast(string FromPC)
        {
            return accessor.mGetLast(FromPC);
        }

        public Model.PCExportReportANSIDetail mGetPrev(DateTime InsertDate, string FromPC)
        {
            return accessor.mGetPrev(InsertDate, FromPC);
        }

        public Model.PCExportReportANSIDetail mGetNext(DateTime InsertDate, string FromPC)
        {
            return accessor.mGetNext(InsertDate, FromPC);
        }

        public bool mHasRows(string FromPC)
        {
            return accessor.mHasRows(FromPC);
        }

        public bool mHasRowsBefore(Model.PCExportReportANSIDetail e, string FromPC)
        {
            return accessor.mHasRowsBefore(e, FromPC);
        }

        public bool mHasRowsAfter(Model.PCExportReportANSIDetail e, string FromPC)
        {
            return accessor.mHasRowsAfter(e, FromPC);
        }

        public IList<Model.PCExportReportANSIDetail> mSelect(string FromPC)
        {
            return accessor.mSelect(FromPC);
        }

        #endregion
    }
}

