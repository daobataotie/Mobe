//------------------------------------------------------------------------------
//
// file name：ProduceMaterialManager.cs
// author: peidun
// create date：2009-12-30 16:33:30
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceMaterial.
    /// </summary>
    public partial class ProduceMaterialManager : BaseManager
    {
        private static readonly DA.IProduceMaterialdetailsAccessor ProduceMaterialdetailsAccessor = (DA.IProduceMaterialdetailsAccessor)Accessors.Get("ProduceMaterialdetailsAccessor");

        private static readonly ProductManager productManager = new ProductManager();

        public void Delete(string produceMaterialID)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceMaterialID);
        }

        public Model.ProduceMaterial GetDetails(string produceMaterialID)
        {
            Model.ProduceMaterial produceMaterial = accessor.Get(produceMaterialID);
            if (produceMaterial != null)
                produceMaterial.Details = ProduceMaterialdetailsAccessor.Select(produceMaterial);
            return produceMaterial;
        }

        public void Delete(Model.ProduceMaterial produceMaterial)
        {
            //
            // todo:add other logic here
            //

            //已分配
            try
            {
                BL.V.BeginTransaction();
                Model.ProduceMaterial produceMaterialOld = this.GetDetails(produceMaterial.ProduceMaterialID);
                foreach (Model.ProduceMaterialdetails produceMaterialdetails in produceMaterialOld.Details)
                {
                    produceMaterialdetails.Product = productManager.Get(produceMaterialdetails.ProductId);

                    produceMaterialdetails.Product.ProduceMaterialDistributioned = (produceMaterialdetails.Product.ProduceMaterialDistributioned == null ? 0 : produceMaterialdetails.Product.ProduceMaterialDistributioned) - (produceMaterialdetails.Materialprocessum == null ? 0 : produceMaterialdetails.Materialprocessum) + (produceMaterialdetails.Materialprocesedsum == null ? 0 : produceMaterialdetails.Materialprocesedsum);
                    productManager.update(produceMaterialdetails.Product);
                }
                this.Delete(produceMaterial.ProduceMaterialID);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;

            }

        }

        public void Insert(Model.ProduceMaterial produceMaterial)
        {
            //
            // todo:add other logic here
            //
            Validate(produceMaterial);
            try
            {

                BL.V.BeginTransaction();
                produceMaterial.InsertTime = produceMaterial.InsertTime == null ? DateTime.Now : produceMaterial.InsertTime;
                produceMaterial.UpdateTime = DateTime.Now;
                TiGuiExists(produceMaterial);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceMaterial.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceMaterial.InsertTime.Value.Year, produceMaterial.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceMaterial.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(produceMaterial);
                foreach (Model.ProduceMaterialdetails produceMaterialdetails in produceMaterial.Details)
                {
                    if (produceMaterialdetails.Product == null || string.IsNullOrEmpty(produceMaterialdetails.Product.ProductId))
                        throw new Exception("貨品不為空");
                    produceMaterialdetails.ProduceMaterialID = produceMaterial.ProduceMaterialID;

                    Model.Product product = productManager.Get(produceMaterialdetails.ProductId);
                    //已分配
                    produceMaterialdetails.Distributioned = (product.OtherMaterialDistributioned == null ? 0 : product.OtherMaterialDistributioned) + (product.ProduceMaterialDistributioned == null ? 0 : product.ProduceMaterialDistributioned) + produceMaterialdetails.Materialprocessum;

                    // produceMaterialdetails.Materialprocesedsum = produceMaterialdetails.Materialprocessum + produceMaterialdetails.Materialprocessum;
                    ProduceMaterialdetailsAccessor.Insert(produceMaterialdetails);
                    product.ProduceMaterialDistributioned = (product.ProduceMaterialDistributioned == null ? 0 : product.ProduceMaterialDistributioned) + (produceMaterialdetails.Materialprocessum == null ? 0 : produceMaterialdetails.Materialprocessum);
                    productManager.update(product);

                    //if (product.ProduceMaterialDistributioned + product.OtherMaterialDistributioned > product.StocksQuantity)
                    //{
                    //    throw new Helper.MessageValueException(product.ProductName + " ," + "領料數量大於未分配數量");
                    //}
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.ProduceMaterial produceMaterial)
        {
            //
            // todo: add other logic here.
            //

            Validate(produceMaterial);
            if (produceMaterial != null)
            {
                //this.Delete(produceMaterial);
                //produceMaterial.UpdateTime = DateTime.Now;
                //this.Insert(produceMaterial);

                //旧方法会导致在修改的时候如果先删除了旧单据新增新单据时出错，旧单据则无法找回
                UpdateAndInsert(produceMaterial);
            }
        }

        private void UpdateAndInsert(Model.ProduceMaterial produceMaterial)
        {
            try
            {
                BL.V.BeginTransaction();

                //删除旧单据
                Model.ProduceMaterial produceMaterialOld = this.GetDetails(produceMaterial.ProduceMaterialID);
                foreach (Model.ProduceMaterialdetails produceMaterialdetails in produceMaterialOld.Details)
                {
                    produceMaterialdetails.Product = productManager.Get(produceMaterialdetails.ProductId);

                    produceMaterialdetails.Product.ProduceMaterialDistributioned = (produceMaterialdetails.Product.ProduceMaterialDistributioned == null ? 0 : produceMaterialdetails.Product.ProduceMaterialDistributioned) - (produceMaterialdetails.Materialprocessum == null ? 0 : produceMaterialdetails.Materialprocessum) + (produceMaterialdetails.Materialprocesedsum == null ? 0 : produceMaterialdetails.Materialprocesedsum);
                    productManager.update(produceMaterialdetails.Product);
                }
                this.Delete(produceMaterial.ProduceMaterialID);

                //新增单据
                produceMaterial.InsertTime = produceMaterial.InsertTime == null ? DateTime.Now : produceMaterial.InsertTime;
                produceMaterial.UpdateTime = DateTime.Now;
                TiGuiExists(produceMaterial);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceMaterial.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceMaterial.InsertTime.Value.Year, produceMaterial.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceMaterial.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(produceMaterial);
                foreach (Model.ProduceMaterialdetails produceMaterialdetails in produceMaterial.Details)
                {
                    if (produceMaterialdetails.Product == null || string.IsNullOrEmpty(produceMaterialdetails.Product.ProductId))
                        throw new Exception("貨品不為空");
                    produceMaterialdetails.ProduceMaterialID = produceMaterial.ProduceMaterialID;

                    Model.Product product = productManager.Get(produceMaterialdetails.ProductId);
                    //已分配
                    produceMaterialdetails.Distributioned = (product.OtherMaterialDistributioned == null ? 0 : product.OtherMaterialDistributioned) + (product.ProduceMaterialDistributioned == null ? 0 : product.ProduceMaterialDistributioned) + Convert.ToDouble(produceMaterialdetails.Materialprocessum);

                    // produceMaterialdetails.Materialprocesedsum = produceMaterialdetails.Materialprocessum + produceMaterialdetails.Materialprocessum;
                    ProduceMaterialdetailsAccessor.Insert(produceMaterialdetails);
                    product.ProduceMaterialDistributioned = (product.ProduceMaterialDistributioned == null ? 0 : product.ProduceMaterialDistributioned) + (produceMaterialdetails.Materialprocessum == null ? 0 : produceMaterialdetails.Materialprocessum);
                    productManager.update(product);

                    ////物料需求领料描述
                    //Model.MRSdetails mrsdetail;
                    //if (produceMaterialdetails.MRSdetailsId != null)
                    //{
                    //    mrsdetail = new BL.MRSdetailsManager().Get(produceMaterialdetails.MRSdetailsId);
                    //    if (mrsdetail != null)
                    //    {
                    //        mrsdetail.MaterialDesc = "已生成領料單";
                    //        new BL.MRSdetailsManager().Update(mrsdetail);
                    //    }
                    //}

                    //if (product.ProduceMaterialDistributioned + product.OtherMaterialDistributioned > product.StocksQuantity)
                    //{
                    //    throw new Helper.MessageValueException(product.ProductName + " ," + "領料數量大於未分配數量");
                    //}
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void _Update(Model.ProduceMaterial produceMaterial)
        {
            accessor.Update(produceMaterial);
        }

        public void UpdateDepotOutState(Model.ProduceMaterial produceMaterial)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(produceMaterial);
        }

        private void Validate(Model.ProduceMaterial produceMaterial)
        {
            if (string.IsNullOrEmpty(produceMaterial.ProduceMaterialID))
            {
                throw new Helper.RequireValueException(Model.ProduceMaterial.PRO_ProduceMaterialID);
            }
            if (string.IsNullOrEmpty(produceMaterial.WorkHouseId))
            {
                throw new Helper.RequireValueException(Model.ProduceMaterial.PRO_WorkHouseId);
            }
        }

        protected override string GetSettingId()
        {
            return "pdmRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pdm";
        }

        public IList<Model.ProduceMaterial> SelectInvoiceId(string invoiceid)
        {
            return accessor.SelectInvoiceId(invoiceid);
        }

        public IList<Model.ProduceMaterial> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, bool isNoClose, string InvoiceCusXOId)
        {
            return accessor.SelectByDateRage(startdate, enddate, product, isNoClose, InvoiceCusXOId);
        }

        public IList<Model.ProduceMaterial> SelectState()
        {
            return accessor.SelectState();
        }

        public IList<Model.ProduceMaterial> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, Model.Product pId0, Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1, string CusInvoiceXOId, string handBookId)
        {
            return accessor.SelectBycondition(starDate, endDate, produceMaterialId0, produceMaterialId1, pId0, pId1, departmentId0, departmentId1, PronoteHeaderId0, PronoteHeaderId1, CusInvoiceXOId, handBookId);
        }

        private void TiGuiExists(Model.ProduceMaterial model)
        {
            if (this.ExistsPrimary(model.ProduceMaterialID))
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
                model.ProduceMaterialID = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        public bool IsDepotOut(string id)
        {
            return accessor.IsDepotOut(id);
        }

    }
}

