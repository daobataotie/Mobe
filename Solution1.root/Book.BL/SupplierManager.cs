//------------------------------------------------------------------------------
//
// file name：SupplierManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Supplier.
    /// </summary>
    public partial class SupplierManager : BaseManager
    {
        private static readonly DA.ISupplierContactAccessor supplierContactAccessor = (DA.ISupplierContactAccessor)Accessors.Get("SupplierContactAccessor");
        /// <summary>
        /// Delete Supplier by primary key.
        /// </summary>
        public void Delete(string supplierId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(supplierId);
        }
        public void Delete(Model.Supplier supplier)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(supplier.SupplierId);
        }

        /// <summary>
        /// Insert a Supplier.
        /// </summary>
        public void Insert(Model.Supplier supplier)
        {
            //
            // todo:add other logic here			
            //              
            Validate(supplier);
            if (this.Exists(supplier.Id))
            {
                throw new Helper.InvalidValueException(Model.Supplier.PROPERTY_ID);
            }
            try
            {
                V.BeginTransaction();

                //设置KEY值
                if (supplier.SupplierCategory != null)
                {
                    string sequencekey = supplier.SupplierCategory.Id;
                    SequenceManager.Increment("suppliercategory" + sequencekey);
                }


                if (supplier.AreaCategory != null)
                {
                    supplier.AreaCategoryId = supplier.AreaCategory.AreaCategoryId;
                }

                if (supplier.TradeCategory != null)
                {
                    supplier.TradeCategoryId = supplier.TradeCategory.TradeCategoryId;
                }

                if (supplier.SupplierCategory != null)
                {
                    supplier.SupplierCategoryId = supplier.SupplierCategory.SupplierCategoryId;
                }
                if (supplier.PayMethod != null)
                {
                    supplier.PayMethodId = supplier.PayMethod.PayMethodId;
                }
                supplier.SupplierId = Guid.NewGuid().ToString();
                supplier.InsertTime = DateTime.Now;
                supplier.EmployeeChangeId = V.ActiveOperator.OperatorName;

                accessor.Insert(supplier);

                foreach (Model.SupplierContact contact in supplier.Contacts)
                {
                    contact.Supplier = supplier;
                    contact.SupplierId = supplier.SupplierId;
                    if (string.IsNullOrEmpty(contact.SupplierContactId))
                    {
                        contact.SupplierContactId = Guid.NewGuid().ToString();
                    }
                    supplierContactAccessor.Insert(contact);
                }

                V.CommitTransaction();
            }
            catch (Exception ex)
            {
                V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Update a Supplier.
        /// </summary>
        public void Update(Model.Supplier supplier)
        {
            //
            // todo: add other logic here.
            //
            Validate(supplier);
            if (this.ExistsExcept(supplier))
            {
                throw new Helper.InvalidValueException(Model.Supplier.PROPERTY_ID);
            }
            try
            {
                V.BeginTransaction();

                if (supplier.AreaCategory != null)
                {
                    supplier.AreaCategoryId = supplier.AreaCategory.AreaCategoryId;
                }

                if (supplier.TradeCategory != null)
                {
                    supplier.TradeCategoryId = supplier.TradeCategory.TradeCategoryId;
                }

                if (supplier.SupplierCategory != null)
                {
                    supplier.SupplierCategoryId = supplier.SupplierCategory.SupplierCategoryId;
                }

                supplier.EmployeeChangeId = V.ActiveOperator.OperatorName;

                supplier.UpdateTime = DateTime.Now;

                accessor.Update(supplier);

                //删除供应商联系人
                supplierContactAccessor.Delete(supplier);

                //新增供应商联系人
                foreach (Model.SupplierContact contact in supplier.Contacts)
                {
                    contact.Supplier = supplier;
                    contact.SupplierId = supplier.SupplierId;
                    if (string.IsNullOrEmpty(contact.SupplierContactId))
                    {
                        contact.SupplierContactId = Guid.NewGuid().ToString();
                    }
                    supplierContactAccessor.Insert(contact);
                }
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Select by primary key.
        /// </summary>		
        public Model.Supplier Get(string supplierId)
        {
            Model.Supplier supplier = accessor.Get(supplierId);
            if (supplier != null)
            {
                supplier.Contacts = supplierContactAccessor.Select(supplier);
            }
            return supplier;
        }

        private void Validate(Model.Supplier supplier)
        {
            if (string.IsNullOrEmpty(supplier.Id))
            {
                throw new Helper.RequireValueException(Model.Supplier.PROPERTY_ID);
            }
            if (string.IsNullOrEmpty(supplier.SupplierFullName))
            {
                throw new Helper.RequireValueException(Model.Supplier.PROPERTY_SUPPLIERFULLNAME);
            }
        }


        //public IList<Model.Supplier> Select(string SupplierStart, string SupplierEnd, DateTime dateStart, DateTime dateEnd)
        //{
        //    return accessor.Select( SupplierStart,  SupplierEnd,  dateStart,  dateEnd);
        //}
        //protected override string GetInvoiceKind()
        //{
        //    return "Supplier";
        //}

        //protected override string GetSettingId()
        //{
        //    return "CompanyNumberRuleOfSUPPLIER";
        //}
        public string GetNewId(Model.SupplierCategory supplierCategory)
        {
            // this.Validate(product);
            string sequencekey = supplierCategory.Id;

            // SequenceManager.Increment(sequencekey);
            //  string str=  
            // SequenceManager.IncrementVal(sequencekey,);
            string a = "suppliercategory" + sequencekey;
            int sequenceval = SequenceManager.GetCurrentVal(a) + 1;

            return string.Format("{0}{1:d4}", sequencekey, sequenceval);
        }
        public IList<Book.Model.Supplier> Select(Model.SupplierCategory supplierCategory)
        {
            return accessor.Select(supplierCategory);
        }
    }
}

