//------------------------------------------------------------------------------
//
// file name：ProductMouldManager.cs
// author: peidun
// create date：2009-07-24 11:18:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductMould.
    /// </summary>
    public partial class ProductMouldManager : BaseManager
    {
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

        private static readonly DA.IMouldAttachmentAccessor MouldAttachmentAccessor = (DA.IMouldAttachmentAccessor)Accessors.Get("MouldAttachmentAccessor");
        private static readonly DA.IProductMouldDetailAccessor ProductMouldDetailAccessor = (DA.IProductMouldDetailAccessor)Accessors.Get("ProductMouldDetailAccessor");
        //private static readonly DA.IProductMouldTestDetailAccessor ProductMouldTestDetailAccessor = (DA.IProductMouldTestDetailAccessor)Accessors.Get("ProductMouldTestDetailAccessor");
        /// <summary>
        /// Delete ProductMould by primary key.
        /// </summary>
        public void Delete(string mouldId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(mouldId);
            //删除附件
            string sfdir = this.ServerSavePath + "\\" + mouldId;
            if (System.IO.Directory.Exists(sfdir))
                System.IO.Directory.Delete(sfdir, true);
        }

        public void Delete(Model.ProductMould pro)
        {
            try
            {
                BL.V.BeginTransaction();
                //ProductMouldTestDetailAccessor.DeleteByMouldId(pro.MouldId);
                ProductMouldDetailAccessor.DeleteByMouldId(pro.MouldId);
                //(new BL.ProductMouldTestManager()).DeleteByMouldId(pro.MouldId);
                this.Delete(pro.MouldId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a ProductMould.
        /// </summary>
        public void Insert(Model.ProductMould productMould)
        {

            validate(productMould);

            productMould.InsertTime = DateTime.Now;
            productMould.UpdateTime = DateTime.Now;
            accessor.Insert(productMould);

            foreach (Model.MouldAttachment item in productMould.Details)
            {
                item.InsertTime = System.DateTime.Now;
                item.UpdateTime = System.DateTime.Now;
                MouldAttachmentAccessor.Insert(item);
            }

            //上传附件
            if (!string.IsNullOrEmpty(productMould.Upload))
            {
                string sfdir = this.ServerSavePath + "\\" + productMould.MouldId;
                try
                {
                    System.IO.Directory.CreateDirectory(sfdir);
                }
                catch (Exception ex)
                { throw new Helper.MessageValueException(ex.Message); }
                foreach (string fn in productMould.Upload.Split('|'))
                {
                    if (!fn.Contains(this.ServerSavePath))
                    {
                        System.IO.File.Copy(fn, sfdir + "\\" + fn.Substring(fn.LastIndexOf("\\") + 1), true);
                    }
                }
            }
        }

        private void validate(Book.Model.ProductMould productMould)
        {
            if (string.IsNullOrEmpty(productMould.Id))
                throw new Helper.RequireValueException(Model.ProductMould.PROPERTY_ID);
            if (string.IsNullOrEmpty(productMould.MouldName))
                throw new Helper.RequireValueException(Model.ProductMould.PROPERTY_MOULDNAME);
            if (string.IsNullOrEmpty(productMould.SupplierId))
                throw new Helper.RequireValueException(Model.ProductMould.PROPERTY_SUPPLIERID);

        }

        /// <summary>
        /// Update a ProductMould.
        /// </summary>
        public void Update(Model.ProductMould productMould)
        {

            validate(productMould);
            productMould.UpdateTime = DateTime.Now;
            accessor.Update(productMould);

            MouldAttachmentAccessor.DeleteByMouldid(productMould.MouldId);

            foreach (Model.MouldAttachment item in productMould.Details)
            {
                item.InsertTime = System.DateTime.Now;
                item.UpdateTime = System.DateTime.Now;
                MouldAttachmentAccessor.Insert(item);
            }

            string path = ServerSavePath + "\\" + productMould.MouldId;
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string[] newfilenames = productMould.Upload.Split('|');
            string[] hasfilenames = System.IO.Directory.GetFiles(path);

            //上传新的
            if (!string.IsNullOrEmpty(productMould.Upload))
            {
                foreach (string newfile in newfilenames)
                {
                    if (newfile.Contains(ServerSavePath))
                        continue;

                    bool flag = true;
                    foreach (string hasfile in hasfilenames)
                    {
                        if (hasfile.Equals(newfile, StringComparison.OrdinalIgnoreCase))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        System.IO.File.Copy(newfile, path + "\\" + newfile.Substring(newfile.LastIndexOf("\\") + 1), true);
                }
            }

            //删除旧的
            foreach (string hasfile in hasfilenames)
            {
                bool flag = true;
                foreach (string newfile in newfilenames)
                {
                    string str = newfile.Substring(newfile.LastIndexOf("\\") + 1);
                    if (str.Equals(hasfile.Substring(hasfile.LastIndexOf("\\") + 1), StringComparison.OrdinalIgnoreCase))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    System.IO.File.Delete(hasfile);
            }
        }

        public IList<Model.ProductMould> SelectProductMouldByProductMouldTestId(string ProductMouldTestId)
        {
            return accessor.SelectProductMouldByProductMouldTestId(ProductMouldTestId);
        }

        public IList<Model.ProductMould> SelectByDateRage(DateTime StartDate, DateTime EndDate, string MouldId, string MouldName, Model.MouldCategory mouldCategory)
        {
            return accessor.SelectByDateRage(StartDate, EndDate, MouldId, MouldName, mouldCategory);
        }

        public Model.ProductMould SelectByMouldId(string MouldId)
        {
            return accessor.SelectByMouldId(MouldId);
        }
    }
}

