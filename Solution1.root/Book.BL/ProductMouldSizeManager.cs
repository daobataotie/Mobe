//------------------------------------------------------------------------------
//
// file name：ProductMouldSizeManager.cs
// author: mayanjun
// create date：2013-2-21 17:11:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductMouldSize.
    /// </summary>
    public partial class ProductMouldSizeManager : BaseManager
    {
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

        /// <summary>
        /// Delete ProductMouldSize by primary key.
        /// </summary>
        public void Delete(string productMouldSizeId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(productMouldSizeId);
            //删除附件
            string sfdir = this.ServerSavePath + "\\" + productMouldSizeId;
            if (System.IO.Directory.Exists(sfdir))
                System.IO.Directory.Delete(sfdir, true);
        }

        /// <summary>
        /// Insert a ProductMouldSize.
        /// </summary>
        public void Insert(Model.ProductMouldSize productMouldSize)
        {
            //
            // todo:add other logic here
            //
            Validate(productMouldSize);
            productMouldSize.InsertTime = DateTime.Now;
            productMouldSize.UpdateTime = DateTime.Now;
            accessor.Insert(productMouldSize);

            //上传附件
            if (!string.IsNullOrEmpty(productMouldSize.Upload))
            {
                string sfdir = this.ServerSavePath + "\\" + productMouldSize.ProductMouldSizeId;
                try
                {
                    System.IO.Directory.CreateDirectory(sfdir);
                }
                catch (Exception ex)
                { throw new Helper.MessageValueException(ex.Message); }
                foreach (string fn in productMouldSize.Upload.Split('|'))
                {
                    if (!fn.Contains(this.ServerSavePath))
                    {
                        System.IO.File.Copy(fn, sfdir + "\\" + fn.Substring(fn.LastIndexOf("\\") + 1), true);
                    }
                }
            }
        }

        /// <summary>
        /// Update a ProductMouldSize.
        /// </summary>
        public void Update(Model.ProductMouldSize productMouldSize)
        {
            //
            // todo: add other logic here.
            //
            Validate(productMouldSize);
            productMouldSize.UpdateTime = DateTime.Now;
            accessor.Update(productMouldSize);

            string path = ServerSavePath + "\\" + productMouldSize.ProductMouldSizeId;
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string[] newfilenames = productMouldSize.Upload.Split('|');
            string[] hasfilenames = System.IO.Directory.GetFiles(path);

            //上传新的
            if (!string.IsNullOrEmpty(productMouldSize.Upload))
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

        private void Validate(Model.ProductMouldSize model)
        {
            if (string.IsNullOrEmpty(model.ProductSizeId))
                throw new Helper.InvalidValueException(Model.ProductMouldSize.PRO_ProductSizeId);
        }

        public IList<Model.ProductMouldSize> SelectByDateRage(DateTime StartDate, DateTime EndDate)
        {
            return accessor.SelectByDateRage(StartDate, EndDate);
        }
    }
}

