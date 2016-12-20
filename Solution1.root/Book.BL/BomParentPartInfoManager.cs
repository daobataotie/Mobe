//------------------------------------------------------------------------------
//
// file name：BomParentPartInfoManager.cs
// author: peidun
// create date：2009-08-25 17:08:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BomParentPartInfo.
    /// </summary>
    public partial class BomParentPartInfoManager : BaseManager
    {
        private static readonly DA.IBomComponentInfoAccessor bomComponentInfoAccessor = (DA.IBomComponentInfoAccessor)Accessors.Get("BomComponentInfoAccessor");
        private static readonly DA.IBomPackageDetailsAccessor bomPackageDetailsAccessor = (DA.IBomPackageDetailsAccessor)Accessors.Get("BomPackageDetailsAccessor");
        private static readonly DA.IBOMProductProcessAccessor bomProductProcessAccessor = (DA.IBOMProductProcessAccessor)Accessors.Get("BOMProductProcessAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
       // private static readonly DA.IManProcedureAccessor manProcedureAccessor = (DA.IManProcedureAccessor)Accessors.Get("ManProcedureAccessor");
        /// <summary>
        /// Select by primary key.
        /// </summary>		
        public Model.BomParentPartInfo Get(string bomId)
        {
            Model.BomParentPartInfo par = accessor.Get(bomId);
            if (par != null)
                par.Components = bomComponentInfoAccessor.Select(par);
            return par;
        }

        /// <summary>
        /// Delete BomParentPartInfo by primary key.
        /// </summary>
        public void Delete(string bomId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                accessor.Delete(bomId);
            }
            catch
            {
                throw new Helper.InvalidValueException("DeleteError");
            }
        }

        public void Delete(Model.BomParentPartInfo bomParentPartInfo)
        {
            //
            // todo:add other logic here 
            //

            try
            {
                V.BeginTransaction();
                this.Delete(bomParentPartInfo.BomId);

                //productAccessor.Delete(bomParentPartInfo.CustomerBOMInProductId);

                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        private void Validate(Model.BomParentPartInfo bom)
        {
            if (string.IsNullOrEmpty(bom.Id))
            {
                throw new Helper.RequireValueException(Model.BomParentPartInfo.PRO_Id);
            }

            //if (string.IsNullOrEmpty(bom.ProductId))
            //{
            //    throw new  Exception("請選擇母件");
            //}

            if (bom.Product == null && string.IsNullOrEmpty(bom.ProductId))
            {
                throw new Helper.RequireValueException(Model.BomParentPartInfo.PRO_ProductId);
            }
        }

        /// <summary>
        /// Insert a BomParentPartInfo.
        /// </summary>
        public void Insert(Model.BomParentPartInfo bomParentPartInfo)
        {
            //
            // todo:add other logic here
            //
            Validate(bomParentPartInfo);
            if (this.Exists(bomParentPartInfo.Id))
            {
                bomParentPartInfo.Id = this.GetId();

                // throw new Helper.InvalidValueException("Id");
            }
            string sql = " ProductId= '"+ bomParentPartInfo.ProductId+"'";
            if (this.Exists_Field(sql))
                throw new Helper.InvalidValueException(Model.BomParentPartInfo.PRO_ProductId);
            bomParentPartInfo.BomId = Guid.NewGuid().ToString();

            try
            {
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, bomParentPartInfo.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, bomParentPartInfo.InsertTime.Value.Year, bomParentPartInfo.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, bomParentPartInfo.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                bomParentPartInfo.InsertTime = DateTime.Now;
                bomParentPartInfo.EmployeeAddId = BL.V.ActiveOperator.EmployeeId;
                //if (bomParentPartInfo.Status == 0 &&!string.IsNullOrEmpty( bomParentPartInfo.Product.CustomerBeforeProductName))
                //    this.UpdateSql("UPDATE bomParentPartInfo set Status=1 where productid in(select productid from product where CustomerProductName=  '" + bomParentPartInfo.Product.CustomerProductName + "')");
                _Insert(bomParentPartInfo);
                ////添加物料加工工序
                //if (!string.IsNullOrEmpty(bomParentPartInfo.TechonlogyHeaderId))
                //{
                //    Model.ManProcedure manProceduce = new Model.ManProcedure();
                //    manProceduce.ManProcedureId = Guid.NewGuid().ToString();
                //    manProceduce.BomId = bomParentPartInfo.BomId;
                //    if (bomParentPartInfo.Customer != null)
                //        manProceduce.CustomerId = bomParentPartInfo.Customer.CustomerId;
                //    manProceduce.InsertTime = DateTime.Now;
                //    manProceduce.UpdateTime = DateTime.Now;
                //    manProceduce.TechonlogyHeaderId = bomParentPartInfo.TechonlogyHeaderId;
                //}

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void _Insert(Model.BomParentPartInfo bomParentPartInfo)
        {

            // bomParentPartInfo.CustomerBOMInProductId = product.ProductId;
            foreach (Model.BomComponentInfo component in bomParentPartInfo.Components)
            {
                if (bomParentPartInfo.ProductId == component.ProductId)
                {
                    throw new global::Helper.MessageValueException("主鍵不能與母鍵相同!");
                }
            }
            if (bomParentPartInfo.Status == 0 && !string.IsNullOrEmpty(bomParentPartInfo.Product.CustomerBeforeProductName))
                this.UpdateSql("UPDATE bomParentPartInfo set Status=1 where productid in(select productid from product where CustomerProductName=  '" + bomParentPartInfo.Product.CustomerProductName + "')");
            accessor.Insert(bomParentPartInfo);
            foreach (Model.BomComponentInfo component in bomParentPartInfo.Components)
            {
                if (string.IsNullOrEmpty(component.ProductId) && component.Product == null) continue;
                component.PriamryKeyId = Guid.NewGuid().ToString();
                component.BasicUseQuantity = 1;
                component.BomId = bomParentPartInfo.BomId;
                component.FoundationQuantity = 1;
                if (string.IsNullOrEmpty(component.ProductId))
                {
                    if (component.Product != null)
                        component.ProductId = component.Product.ProductId;
                }
                bomComponentInfoAccessor.Insert(component);
            }
            if (bomParentPartInfo.BomPackageDetails != null)
            {
                foreach (Model.BomPackageDetails bomPackageDetails in bomParentPartInfo.BomPackageDetails)
                {
                    if (bomPackageDetails != null)
                    {
                        bomPackageDetails.BomPackageDetailsId = Guid.NewGuid().ToString();
                        bomPackageDetails.BomId = bomParentPartInfo.BomId;
                        bomPackageDetailsAccessor.Insert(bomPackageDetails);
                    }
                }
            }

        }

        /// <summary>
        /// Update a BomParentPartInfo.
        /// </summary>
        public void Update(Model.BomParentPartInfo bomParentPartInfo)
        {

            this.Validate(bomParentPartInfo);

            if (this.ExistsExcept(bomParentPartInfo))
            {
                throw new Helper.InvalidValueException("Id");
            }
            foreach (Model.BomComponentInfo component in bomParentPartInfo.Components)
            {
                if (bomParentPartInfo.ProductId == component.ProductId)
                {
                    throw new global::Helper.MessageValueException("主鍵不能與母鍵相同!");
                }
            }
            try
            {
                BL.V.BeginTransaction();
                bomParentPartInfo.UpdateTime = DateTime.Now;
                bomParentPartInfo.EmployeeUpdateId = BL.V.ActiveOperator.EmployeeId;
                bomParentPartInfo.Customer = null;
                bomParentPartInfo.CustomerId = null;
                bomParentPartInfo.CustomerProductName = null;
                if (bomParentPartInfo.Status == 0 && !string.IsNullOrEmpty(bomParentPartInfo.Product.CustomerBeforeProductName))
                    this.UpdateSql("UPDATE bomParentPartInfo set Status=1 where productid in(select productid from product where CustomerProductName=  '" + bomParentPartInfo.Product.CustomerProductName + "')  and BomId<>'" + bomParentPartInfo.BomId + "' ");
                accessor.Update(bomParentPartInfo);
                bomComponentInfoAccessor.Delete(bomParentPartInfo);
                bomProductProcessAccessor.Delete(bomParentPartInfo);
                foreach (Model.BomComponentInfo component in bomParentPartInfo.Components)
                {
                    if (string.IsNullOrEmpty(component.ProductId) && component.Product == null) continue;


                    component.PriamryKeyId = Guid.NewGuid().ToString();
                    component.BasicUseQuantity = 1;
                    component.BomId = bomParentPartInfo.BomId;
                    component.FoundationQuantity = 1;
                    if (string.IsNullOrEmpty(component.ProductId))
                    {
                        if (component.Product != null)
                            component.ProductId = component.Product.ProductId;
                    }
                    bomComponentInfoAccessor.Insert(component);
                }

                bomPackageDetailsAccessor.Delete(bomParentPartInfo);
                if (bomParentPartInfo.BomPackageDetails != null)
                {
                    foreach (Model.BomPackageDetails bomPackageDetails in bomParentPartInfo.BomPackageDetails)
                    {
                        if (bomPackageDetails != null)
                        {
                            bomPackageDetails.BomPackageDetailsId = Guid.NewGuid().ToString();
                            bomPackageDetails.BomId = bomParentPartInfo.BomId;
                            bomPackageDetailsAccessor.Insert(bomPackageDetails);
                        }
                    }
                }

                this.ModifySequence();
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }


        protected override string GetInvoiceKind()
        {
            return "bom";
        }

        protected override string GetSettingId()
        {
            return "bomRule";
        }

        public Book.Model.BomParentPartInfo Get(Book.Model.Product product)
        {
            Model.BomParentPartInfo par = accessor.Get(product);
            if (par != null)
            {
                par.Components = bomComponentInfoAccessor.Select(par);
            }
            return par;
        }
        public IList<Model.BomParentPartInfo> SelectNotContent()
        {

            return accessor.SelectNotContent();
        }
        public Book.Model.BomParentPartInfo Get(Book.Model.Product product, Model.Customer customer)
        {


            Model.BomParentPartInfo par = accessor.Get(product, customer);
            if (par != null)
            {
                par.Components = bomComponentInfoAccessor.Select(par);
            }
            return par;
        }
        //public void Delete(Book.Model.Product product, Model.Customer customer)
        //{
        //    accessor.Delete(product, customer);

        //}
        //public void DeleteByInProductCustomer(Book.Model.Product product, Model.Customer customer)
        //{
        //    accessor.DeleteByInProductCustomer(product, customer);
        //}
        public IList<Model.BomParentPartInfo> SelectNotContentByCustomer(Model.Customer customer)
        {
            return accessor.SelectNotContentByCustomer(customer);
        }
        public bool Exists_Field(string sqlWhere)
        {
            return accessor.Exists_Field(sqlWhere);
        }
        public IList<Model.BomParentPartInfo> SelectByIdOrNameKey(string bomid, string proid, string productName, string customerProductName)
        {
            return accessor.SelectByIdOrNameKey(bomid, proid, productName, customerProductName);
        }
        //上一笔等
        public Model.BomParentPartInfo GetPrev1(Model.BomParentPartInfo e)
        {
            return accessor.GetPrev1(e);
        }

        public bool HasRowsBefore1(Model.BomParentPartInfo e)
        {
            return accessor.HasRowsBefore1(e);
        }
        public bool HasRowsAfter1(Model.BomParentPartInfo e)
        {
            return accessor.HasRowsAfter1(e);
        }
        public Model.BomParentPartInfo GetFirst1()
        {
            return accessor.GetFirst1();
        }
        public Model.BomParentPartInfo GetLast1()
        {
            return accessor.GetLast1();
        }
        public Model.BomParentPartInfo GetNext1(Model.BomParentPartInfo e)
        {
            return accessor.GetNext1(e);
        }
        public bool HasRows1()
        {
            return accessor.HasRows1();
        }
        /// <summary>
        /// 查询成品dataset
        /// </summary>
        /// <returns></returns>
        public DataSet SelectNotContentDataSet()
        {
            return accessor.SelectNotContentDataSet();
        }
        public DataSet SelectDataSet()
        {
            return accessor.SelectDataSet();
        }

        public Model.BomParentPartInfo SelectByProductId(string productid)
        {
            return accessor.Select_ProductId(productid);
        }
    }
}

