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
    public partial class ProduceMaterialManager:BaseManager
    {
        private static readonly DA.IProduceMaterialdetailsAccessor ProduceMaterialdetailsAccessor = (DA.IProduceMaterialdetailsAccessor)Accessors.Get("ProduceMaterialdetailsAccessor");
		/// <summary>
		/// Delete ProduceMaterial by primary key.
		/// </summary>
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
            produceMaterial.Details = ProduceMaterialdetailsAccessor.Select(produceMaterial);
            return produceMaterial;
        }

        public void Delete(Model.ProduceMaterial produceMaterial)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceMaterial.ProduceMaterialID);
        }
		/// <summary>
		/// Insert a ProduceMaterial.
		/// </summary>
        public void Insert(Model.ProduceMaterial produceMaterial)
        {
			//
			// todo:add other logic here
			//
            Validate(produceMaterial);
            if (this.ExistsId (produceMaterial.ProduceMaterialID))
            {
                produceMaterial.ProduceMaterialID = this.GetId();
                //throw new Helper.InvalidValueException(Model.ProduceMaterial.PRO_ProduceMaterialID);
            }
              try
            {
            
                BL.V.BeginTransaction();
                produceMaterial.InsertTime = DateTime.Now;
                produceMaterial.UpdateTime = DateTime.Now;
            string invoiceKind = this.GetInvoiceKind().ToLower();
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceMaterial.InsertTime.Value.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceMaterial.InsertTime.Value.Year, produceMaterial.InsertTime.Value.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceMaterial.InsertTime.Value.ToString("yyyy-MM-dd"));
            string sequencekey = string.Format(invoiceKind);

            SequenceManager.Increment(sequencekey_y);
            SequenceManager.Increment(sequencekey_m);
            SequenceManager.Increment(sequencekey_d);
            SequenceManager.Increment(sequencekey);
            produceMaterial.Employee0 = BL.V.ActiveOperator.Employee;
            produceMaterial.Employee0Id = BL.V.ActiveOperator.EmployeeId;
            accessor.Insert(produceMaterial);
            foreach (Model.ProduceMaterialdetails produceMaterialdetails in produceMaterial.Details)
            {
                if (produceMaterialdetails.Product  == null || string.IsNullOrEmpty(produceMaterialdetails.Product.ProductId))
                    throw new Exception("貨品不為空");
                produceMaterialdetails.ProduceMaterialID = produceMaterial.ProduceMaterialID;
               //produceMaterialdetails.Materialprocesedsum = produceMaterialdetails.Materialprocessum;
                ProduceMaterialdetailsAccessor.Insert(produceMaterialdetails);
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
		/// Update a ProduceMaterial.
		/// </summary>
        public void Update(Model.ProduceMaterial produceMaterial)
        {
			//
			// todo: add other logic here.
			//
            Validate(produceMaterial);
            if (produceMaterial != null)
            {
                this.Delete(produceMaterial);
                produceMaterial.UpdateTime = DateTime.Now;
                this.Insert(produceMaterial);
            }
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
            //if (string.IsNullOrEmpty(produceMaterial.WorkHouseId))
            //{
            //    throw new Helper.RequireValueException(Model.ProduceMaterial.PRO_WorkHouseId);
            //}
        }
        protected override string GetSettingId()
        {
            return "pdmRule";
        }
        protected override string GetInvoiceKind()
        {
            return "pdm";
        }

        public IList<Model.ProduceMaterial> SelectbypronoteHeaderId(string pronoteHeaderId)
        {
            return accessor.SelectbypronoteHeaderId(pronoteHeaderId);
        }

        public void UpdateProduceMaterial(DataTable dt)
        {
            accessor.UpdateProduceMaterial(dt);
        }

        public DataTable GetbypronoteHeaderId(string pronoteHeaderId)
        {
            return accessor.GetbypronoteHeaderId(pronoteHeaderId);
        }

        public IList<Model.ProduceMaterial> SelectByDateRage(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRage(startdate, enddate);
        }
        public IList<Model.ProduceMaterial> SelectState()
        {
            return accessor.SelectState();
        }
        public bool ExistsId(string id)
        {
            return accessor.ExistsId(id);
        }
        public IList<Model.ProduceMaterial> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, string pId0, string pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
        { 
          return accessor.SelectBycondition( starDate,  endDate,  produceMaterialId0,  produceMaterialId1,  pId0,  pId1,  departmentId0,  departmentId1, PronoteHeaderId0,  PronoteHeaderId1);
        }

    }
}

