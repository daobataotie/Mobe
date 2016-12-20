//------------------------------------------------------------------------------
//
// file name：ProductCategoryManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductCategory.
    /// </summary>
    public partial class ProductCategoryManager : BaseManager
    {
		
		/// <summary>
		/// Delete ProductCategory by primary key.
		/// </summary>
		public void Delete(string productCategoryId)
		{
			accessor.Delete(productCategoryId);
		}

        public void Delete(Model.ProductCategory productCategory)
        {
            this.Delete(productCategory.ProductCategoryId);
        }

		/// <summary>
		/// Insert a ProductCategory.
		/// </summary>
        public void Insert(Model.ProductCategory productCategory)
        {
            //this.Validate(productCategory);
            //if (this.Exists(productCategory.Id))
            //{
            //    throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_ID);
            //}
            
            //productCategory.ProductCategoryId = Guid.NewGuid().ToString();
            //productCategory.InsertTime = DateTime.Now; 
            if (this.Exists(productCategory.Id))
            {
                throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_ID);
            }
            productCategory.InsertTime = DateTime.Now;
            productCategory.UpdateTime = DateTime.Now;
            accessor.Insert(productCategory);
        }
		
		/// <summary>
		/// Update a ProductCategory.
		/// </summary>
        public void Update(IList<Model.ProductCategory> productCategoryDetail)
        {

            foreach (Model.ProductCategory pc in productCategoryDetail)
            { 
            
              if (string.IsNullOrEmpty(pc.Id)||string.IsNullOrEmpty(pc.ProductCategoryName))     
              {
                    throw new Helper.RequireValueException(Model.ProductCategory.PROPERTY_ID);                    
              }
              if (accessor.ExistsName(pc.ProductCategoryName, pc.ProductCategoryId))
              {
                    throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_PRODUCTCATEGORYNAME);              
              }
              if (accessor.ExistsExcept(pc))
              {
                  throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_ID);
              }              
            
            }           

            foreach (Model.ProductCategory pc in productCategoryDetail)
            {            
              
                if (accessor.ExistsPrimary(pc.ProductCategoryId) )
                {                
                    pc.UpdateTime = DateTime.Now;
                    accessor.Update(pc);
                }          
                    
                else
                {
             
                    this.Insert(pc);
                }
            }      
        }

        public IList<string> SelectALLName()
        {
            return accessor.SelectALLName();
            
        }

        #region Helpers

        private void Validate(Model.ProductCategory productCategory)
        {
            if (string.IsNullOrEmpty(productCategory.Id))
                throw new Helper.RequireValueException(Model.ProductCategory.PROPERTY_ID);

            if (string.IsNullOrEmpty(productCategory.ProductCategoryName))
                throw new Helper.RequireValueException(Model.ProductCategory.PROPERTY_PRODUCTCATEGORYNAME);

           
        }

        #endregion

        //protected override string GetInvoiceKind()
        //{
        //    return "ProductCategory";
        //}

        //protected override string GetSettingId()
        //{
        //    return "ProductCategoryRule";
        //}
    }
}

