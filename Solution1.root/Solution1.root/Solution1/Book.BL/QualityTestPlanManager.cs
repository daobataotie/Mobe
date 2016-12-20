//------------------------------------------------------------------------------
//
// file name：QualityTestPlanManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.QualityTestPlan.
    /// </summary>
    public partial class QualityTestPlanManager : BaseManager
    {
		
		/// <summary>
		/// Delete QualityTestPlan by primary key.
		/// </summary>
		public void Delete(string qualityTestPlanId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(qualityTestPlanId);
		}
        public void Delete(Model.QualityTestPlan qualitytextplan)
        {
            accessor.Delete(qualitytextplan.QualityTestPlanId);
        }

		/// <summary>
		/// Insert a QualityTestPlan.
		/// </summary>
        public void Insert(Model.QualityTestPlan qualityTestPlan)
        {
			//
			// todo:add other logic here
			//
            this.Validate(qualityTestPlan);

            if (this.Exists(qualityTestPlan.Id))
            {
                throw new Helper.InvalidValueException(Model.QualityTestPlan.PROPERTY_ID);
            }
            if (this.hasRowsName(qualityTestPlan.QualityTestPlanName))
            {
                throw new Helper.InvalidValueException(Model.QualityTestPlan.PROPERTY_QUALITYTESTPLANNAME);
            }
            qualityTestPlan.QualityTestPlanId = Guid.NewGuid().ToString();
            accessor.Insert(qualityTestPlan);
        }
        public bool hasRowsName(string qualityTestPlanName)
        {
            return  accessor.HasRowsName(qualityTestPlanName);
        
        }
		
		/// <summary>
		/// Update a QualityTestPlan.
		/// </summary>
        public void Update(Model.QualityTestPlan qualityTestPlan)
        {
			//
			// todo: add other logic here.
			//
            this.Validate(qualityTestPlan);

            if (this.ExistsExcept(qualityTestPlan))
            {
                throw new Helper.InvalidValueException(Model.QualityTestPlan.PROPERTY_ID);
            }
            accessor.Update(qualityTestPlan);
        }
        private void Validate(Model.QualityTestPlan qualitytextplan)
        {
            if (string.IsNullOrEmpty(qualitytextplan.Id))
            {
                throw new Helper.RequireValueException(Model.QualityTestPlan.PROPERTY_ID);
            }

            if (string.IsNullOrEmpty(qualitytextplan.QualityTestPlanName))
            {
                throw new Helper.RequireValueException(Model.QualityTestPlan.PROPERTY_QUALITYTESTPLANNAME);
            }

            if (string.IsNullOrEmpty(qualitytextplan.QualityTestCode))
            {
                throw new Helper.RequireValueException(Model.QualityTestPlan.PROPERTY_QUALITYTESTCODE);
            }

            if (string.IsNullOrEmpty(qualitytextplan.QualityTestStandardCode))
            {
                throw new Helper.RequireValueException(Model.QualityTestPlan.PROPERTY_QUALITYTESTSTANDARDCODE);
            }            
        }
        //protected override string GetInvoiceKind()
        //{
        //    return "QualityTestPlan";
        //}

        //protected override string GetSettingId()
        //{
        //    return "QualityTestPlanRule";
        //}
    }
}

