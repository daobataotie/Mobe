//------------------------------------------------------------------------------
//
// file name：BGProductManager.cs
// author: mayanjun
// create date：2013-4-1 11:58:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGProduct.
    /// </summary>
    public partial class BGProductManager
    {

        /// <summary>
        /// Delete BGProduct by primary key.
        /// </summary>
        public void Delete(string bGProductId)
        {
            //
            // todo:add other logic here
            //
            (new BL.BGProductDetaiManager()).DeleteByBGProductId(bGProductId);
            accessor.Delete(bGProductId);
        }

        /// <summary>
        /// Insert a BGProduct.
        /// </summary>
        public void Insert(Model.BGProduct bGProduct)
        {
            //
            // todo:add other logic here
            //
            Validate(bGProduct);
            bGProduct.InsertTime = DateTime.Now;
            bGProduct.UpdateTime = DateTime.Now;
            accessor.Insert(bGProduct);

            foreach (Model.BGProductDetai model in bGProduct.DetailProduct)
            {
                model.BGProductId = bGProduct.BGProductId;
                model.ProType = true;
                (new BL.BGProductDetaiManager()).Insert(model);
            }
            foreach (Model.BGProductDetai model in bGProduct.DetailMaterial)
            {
                model.BGProductId = bGProduct.BGProductId;
                model.ProType = false;
                (new BL.BGProductDetaiManager()).Insert(model);
            }
        }

        /// <summary>
        /// Update a BGProduct.
        /// </summary>
        public void Update(Model.BGProduct bGProduct)
        {
            //
            // todo: add other logic here.
            //
            Validate(bGProduct);
            bGProduct.UpdateTime = DateTime.Now;
            accessor.Update(bGProduct);

            (new BL.BGProductDetaiManager()).DeleteByBGProductId(bGProduct.BGProductId);
            foreach (Model.BGProductDetai model in bGProduct.DetailProduct)
            {
                model.BGProductId = bGProduct.BGProductId;
                model.ProType = true;
                (new BL.BGProductDetaiManager()).Insert(model);
            }
            foreach (Model.BGProductDetai model in bGProduct.DetailMaterial)
            {
                model.BGProductId = bGProduct.BGProductId;
                model.ProType = false;
                (new BL.BGProductDetaiManager()).Insert(model);
            }
        }

        private void Validate(Model.BGProduct bGProduct)
        {
            if (bGProduct.Id == null)
                throw new Helper.RequireValueException(Model.BGProduct.PRO_Id + "NULL");
            if (this.Exists(bGProduct.Id))
                throw new Helper.InvalidValueException(Model.BGProduct.PRO_Id);
        }
    }
}

