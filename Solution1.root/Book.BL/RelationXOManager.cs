//------------------------------------------------------------------------------
//
// file name：RelationXOManager.cs
// author: mayanjun
// create date：2015/4/19 下午 08:06:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.RelationXO.
    /// </summary>
    public partial class RelationXOManager
    {
        private static readonly DA.IRelationXODetailAccessor detailaccessor = (DA.IRelationXODetailAccessor)Accessors.Get("RelationXODetailAccessor");

        /// <summary>
        /// Delete RelationXO by primary key.
        /// </summary>
        public void Delete(string relationXOId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                //刪除詳細
                detailaccessor.DeleteByHeaderId(relationXOId);
                accessor.Delete(relationXOId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a RelationXO.
        /// </summary>
        public void Insert(Model.RelationXO relationXO)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();

                relationXO.InsertTime = DateTime.Now;
                relationXO.UpdateTime = DateTime.Now;
                accessor.Insert(relationXO);

                foreach (var item in relationXO.Detail)
                {
                    detailaccessor.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a RelationXO.
        /// </summary>
        public void Update(Model.RelationXO relationXO)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                relationXO.UpdateTime = DateTime.Now;
                accessor.Update(relationXO);
                //刪除詳細
                detailaccessor.DeleteByHeaderId(relationXO.RelationXOId);
                foreach (var item in relationXO.Detail)
                {
                    detailaccessor.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Validate(Model.RelationXO model)
        {
            if (model.InvoiceXOId == null)
                throw new Helper.InvalidValueException(Model.RelationXO.PRO_RelationXOId);
        }

        public Model.RelationXO GetDetail(string id)
        {
            Model.RelationXO model = this.Get(id);
            if (model != null)
                model.Detail = detailaccessor.SelectByHeaderId(id);
            return model;
        }

        public bool ExistsXO(string CusId, string RelationXOId)
        {
            return accessor.ExistsXO(CusId, RelationXOId);
        }

        public Model.RelationXO SelectByInvoiceXOCusId(string id)
        {
            return accessor.SelectByInvoiceXOCusId(id);
        }
    }
}
