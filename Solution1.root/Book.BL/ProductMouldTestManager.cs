//------------------------------------------------------------------------------
//
// file name：ProductMouldTestManager.cs
// author: mayanjun
// create date：2010-9-24 16:24:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductMouldTest.
    /// </summary>
    public partial class ProductMouldTestManager : BaseManager
    {
        private static readonly DA.IProductMouldAccessor ProductMouldAccessor = (DA.IProductMouldAccessor)Accessors.Get("ProductMouldAccessor");

        public string SaveSeverPath
        {
            get
            {
                string s = string.Empty;
                if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["MouldTestPath"] != null)
                {
                    s = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["MouldTestPath"].Value;
                }
                return s;
            }
        }

        public void Delete(string productMouldTestId)
        {
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(productMouldTestId);

                string filedir = this.SaveSeverPath + "\\" + productMouldTestId;
                if (System.IO.Directory.Exists(filedir))
                    System.IO.Directory.Delete(filedir, true);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        public void Insert(Model.ProductMouldTest productMouldTest)
        {
            Validate(productMouldTest);

            try
            {
                BL.V.BeginTransaction();
                productMouldTest.InsertTime = DateTime.Now;
                productMouldTest.UpdateTime = DateTime.Now;
                TiGuiExists(productMouldTest);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, productMouldTest.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, productMouldTest.InsertTime.Value.Year, productMouldTest.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, productMouldTest.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(productMouldTest);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

            //上传附件
            if (!string.IsNullOrEmpty(productMouldTest.accessoriesList))
            {
                string filedir = this.SaveSeverPath + "\\" + productMouldTest.ProductMouldTestId;
                try
                {
                    System.IO.Directory.CreateDirectory(filedir);
                }
                catch (Exception ex)
                {
                    throw new Helper.MessageValueException(ex.Message);
                }
                foreach (string fn in productMouldTest.accessoriesList.Split('|'))
                {
                    System.IO.File.Copy(fn, filedir + "\\" + fn.Substring(fn.LastIndexOf("\\") + 1), true);
                }
            }

        }

        public void Update(Model.ProductMouldTest productMouldTest)
        {
            Validate(productMouldTest);

            productMouldTest.UpdateTime = DateTime.Now;

            accessor.Update(productMouldTest);

            //上传修改附件
            string filedir = this.SaveSeverPath + "\\" + productMouldTest.ProductMouldTestId;
            if (!System.IO.Directory.Exists(filedir))
                System.IO.Directory.CreateDirectory(filedir);
            string[] newfilenames = productMouldTest.accessoriesList.Split('|');//新上传附件
            string[] hasfilenames = System.IO.Directory.GetFiles(filedir);//原始附件

            //添加新附件
            if (!string.IsNullOrEmpty(productMouldTest.accessoriesList))
            {
                foreach (string newfiles in newfilenames)
                {
                    try
                    {
                        bool flag = true;
                        foreach (string hasfile in hasfilenames)
                        {
                            if (hasfile.Equals(newfiles, StringComparison.OrdinalIgnoreCase))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                            System.IO.File.Copy(newfiles, filedir + "\\" + newfiles.Substring(newfiles.LastIndexOf("\\") + 1), true);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            //删除旧附件
            foreach (string hasfiles in hasfilenames)
            {
                try
                {
                    bool flag = true;
                    foreach (string newfiles in newfilenames)
                    {
                        string truenames = newfiles.Substring(newfiles.LastIndexOf("\\") + 1);
                        if (truenames.Equals(hasfiles.Substring(hasfiles.LastIndexOf("\\") + 1), StringComparison.OrdinalIgnoreCase))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        System.IO.File.Delete(hasfiles);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }

        public IList<Model.ProductMouldTest> SelectByDateRage(DateTime StratDate, DateTime EndDate)
        {
            return accessor.SelectByDateRage(StratDate, EndDate);
        }

        public void Validate(Model.ProductMouldTest productMouldTest)
        {
            if (string.IsNullOrEmpty(productMouldTest.Id))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_ProductMouldTestId);
            if (IsExistId(productMouldTest))
                throw new Helper.InvalidValueException(Model.ProductMouldTest.PRO_ProductMouldTestId);
            if (string.IsNullOrEmpty(productMouldTest.MouldId))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_MouldId);
            if (!productMouldTest.ProductMouldTestDate.HasValue)
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_ProductMouldTestDate);
            if (string.IsNullOrEmpty(productMouldTest.SupplierId))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_SupplierId);
            if (string.IsNullOrEmpty(productMouldTest.ProductMaterialId))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_ProductMaterialId);
            if (!productMouldTest.MouldWeight.HasValue)
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_MouldWeight);
            if (string.IsNullOrEmpty(productMouldTest.PronoteMachineId))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_PronoteMachineId);
            if (!productMouldTest.InFactoryDate.HasValue)
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_InFactoryDate);
            if (!productMouldTest.MouldMount.HasValue)
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_MouldMount);
            if (string.IsNullOrEmpty(productMouldTest.EmployeeId))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_EmployeeId);
            //if (!productMouldTest.OutFactoryDate.HasValue)
            //    throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_OutFactoryDate);
            if (string.IsNullOrEmpty(productMouldTest.ProductCategoryId))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PRO_ProductCategoryId);
        }

        public bool IsExistId(Model.ProductMouldTest test)
        {
            return accessor.IsExistId(test);
        }

        private void TiGuiExists(Model.ProductMouldTest model)
        {
            if (this.ExistsPrimary(model.ProductMouldTestId))
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
                model.ProductMouldTestId = this.GetId(model.ProductMouldTestDate.Value);
                TiGuiExists(model);
            }
        }

        protected override string GetSettingId()
        {
            return "PMTRule";
        }

        protected override string GetInvoiceKind()
        {
            return "PMT";
        }

        public Model.ProductMouldTest SelectByMouldId(string MouldId)
        {
            return accessor.SelectByMouldId(MouldId);
        }

        //public void DeleteByMouldId(string MouldId)
        //{
        //    accessor.DeleteByMouldId(MouldId);
        //}

        //public DataTable SelectOrderByMouldId()
        //{
        //    //return accessor.SelectOrderByMouldId();
        //}

        public IList<Model.ProductMouldTest> SelectOrderByMouldId()
        {
            return accessor.SelectOrderByMouldId();
        }
    }
}

