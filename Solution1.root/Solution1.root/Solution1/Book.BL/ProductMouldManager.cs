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
        private static readonly DA.IMouldAttachmentAccessor MouldAttachmentAccessor = (DA.IMouldAttachmentAccessor)Accessors.Get("MouldAttachmentAccessor");
        /// <summary>
        /// Delete ProductMould by primary key.
        /// </summary>
        public void Delete(string mouldId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(mouldId);
        }

        /// <summary>
        /// Insert a ProductMould.
        /// </summary>
        public void Insert(Model.ProductMould productMould)
        {

            validate(productMould);

            accessor.Insert(productMould);

            foreach (Model.MouldAttachment item in productMould.Details)
            {
                item.InsertTime = System.DateTime.Now;
                item.UpdateTime = System.DateTime.Now;
                MouldAttachmentAccessor.Insert(item);
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

            accessor.Update(productMould);

            MouldAttachmentAccessor.DeleteByMouldid(productMould.MouldId);

            foreach (Model.MouldAttachment item in productMould.Details)
            {
                item.InsertTime = System.DateTime.Now;
                item.UpdateTime = System.DateTime.Now;
                MouldAttachmentAccessor.Insert(item);
            }
        }

        public IList<Model.ProductMould> SelectProductMouldByProductMouldTestId(string ProductMouldTestId)
        {
            return accessor.SelectProductMouldByProductMouldTestId(ProductMouldTestId);
        }

    }
}

