//------------------------------------------------------------------------------
//
// file name：CustomerManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Customer.
    /// </summary>
    public partial class CustomerManager : BaseManager
    {

        private static readonly DA.ICustomerContactAccessor customerContactAccessor = (DA.ICustomerContactAccessor)Accessors.Get("CustomerContactAccessor");
        /// <summary>
        /// Delete Customer by primary key.
        /// </summary>
        public void Delete(string companyId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(companyId);
        }

        public void Delete(Model.Customer customer)
        {
            //
            // todo:add other logic here
            //
            this.Delete(customer.CustomerId);
        }

        /// <summary>
        /// Insert a Customer.
        /// </summary>
        public void Insert(Model.Customer customer)
        {
            //
            // todo:add other logic here
            //
            Validate(customer);

            if (this.Exists(customer.Id))
            {
                throw new Helper.InvalidValueException(Model.Customer.PRO_Id);
            }

            try
            {
                V.BeginTransaction();
                //设置KEY值

                SequenceManager.Increment("customer");


                if (customer.AreaCategory != null)
                {
                    customer.AreaCategoryId = customer.AreaCategory.AreaCategoryId;
                }

                if (customer.TradeCategory != null)
                {
                    customer.TradeCategoryId = customer.TradeCategory.TradeCategoryId;
                }

                if (customer.CustomerCategory != null)
                {
                    customer.CustomerCategoryId = customer.CustomerCategory.CustomerCategoryId;
                }

                customer.InsertTime = DateTime.Now;
                customer.CustomerId = Guid.NewGuid().ToString();
                accessor.Insert(customer);

                foreach (Model.CustomerContact contact in customer.Contacts)
                {
                    contact.Customer = customer;
                    contact.CustomerId = customer.CustomerId;
                    if (string.IsNullOrEmpty(contact.CustomerContactId))
                    {
                        contact.CustomerContactId = Guid.NewGuid().ToString();
                    }
                    customerContactAccessor.Insert(contact);
                }

                //foreach (Model.CustomerMarks mark in customer.Marks)
                //{
                //    mark.CustomerId = customer.CustomerId;
                //    (new BL.CustomerMarksManager()).Insert(mark);
                //}
                V.CommitTransaction();
            }
            catch (Exception ex)
            {
                V.RollbackTransaction();
                throw ex;
            }
        }
        private void Validate(Model.Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Id))
                throw new Helper.RequireValueException(Model.Customer.PRO_Id);
            if (string.IsNullOrEmpty(customer.CustomerFullName))
                throw new Helper.RequireValueException(Model.Customer.PRO_CustomerFullName);
            if (this.IsExistFullName(customer))
                throw new Helper.InvalidValueException(Model.Customer.PRO_CustomerFullName);
            if (this.IsExistShortName(customer))
                throw new Helper.InvalidValueException(Model.Customer.PRO_CustomerShortName);

            //var ret = from r in customer.Marks
            //          group r by r.Id into s
            //          select new { a = s.Count() };

            //foreach (var mark in ret)
            //{
            //    if (mark.a > 1)
            //        throw new Helper.InvalidValueException(Model.CustomerMarks.PRO_CustomerMarksId);
            //}

        }
        /// <summary>
        /// Update a Customer.
        /// </summary>
        public void Update(Model.Customer customer)
        {
            //
            // todo: add other logic here.
            //
            Validate(customer);

            if (this.ExistsExcept(customer))
            {
                throw new Helper.InvalidValueException(Model.Customer.PRO_Id);
            }

            try
            {
                V.BeginTransaction();

                if (customer.AreaCategory != null)
                {
                    customer.AreaCategoryId = customer.AreaCategory.AreaCategoryId;
                }

                if (customer.TradeCategory != null)
                {
                    customer.TradeCategoryId = customer.TradeCategory.TradeCategoryId;
                }

                if (customer.CustomerCategory != null)
                {
                    customer.CustomerCategoryId = customer.CustomerCategory.CustomerCategoryId;
                }
                customer.EmployeeChangeId = V.ActiveOperator.OperatorName;
                customer.UpdateTime = DateTime.Now;
                accessor.Update(customer);

                customerContactAccessor.Delete(customer);

                foreach (Model.CustomerContact contact in customer.Contacts)
                {
                    contact.CustomerContactId = Guid.NewGuid().ToString();
                    contact.CustomerId = customer.CustomerId;
                    customerContactAccessor.Insert(contact);
                }

                //(new BL.CustomerMarksManager()).DeleteByCustomerId(customer.CustomerId);
                //foreach (Model.CustomerMarks mark in customer.Marks)
                //{
                //    mark.CustomerId = customer.CustomerId;
                //    (new BL.CustomerMarksManager()).Insert(mark);
                //}
                V.CommitTransaction();
            }
            catch (Exception ex)
            {
                V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Select by primary key.
        /// </summary>		
        public Model.Customer Get(string companyId)
        {
            Model.Customer customer = accessor.Get(companyId);
            if (customer != null)
            {
                customer.Contacts = customerContactAccessor.Select(customer);
            }
            return customer;
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "Customer";
        //}

        //protected override string GetSettingId()
        //{
        //    return "CompanyNumberRuleOfCUSTOMER";
        //}
        public IList<Model.Customer> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            return accessor.Select(customerStart, customerEnd, dateStart, dateEnd);
        }
        public string GetNewId()
        {
            // this.Validate(product);
            string sequencekey = "customer";

            // SequenceManager.Increment(sequencekey);
            //  string str=  
            // SequenceManager.IncrementVal(sequencekey,);
            int sequenceval = SequenceManager.GetCurrentVal(sequencekey) + 1;
            return string.Format("{0}{1:d5}", "C", sequenceval);
        }

        public IList<Model.Customer> selectCustomerInXS()
        {
            return accessor.selectCustomerInXS();
        }

        public bool IsExistFullName(Model.Customer customer)
        {
            return accessor.IsExistFullName(customer);
        }

        public bool IsExistShortName(Model.Customer customer)
        {
            return accessor.IsExistShortName(customer);
        }
    }
}

