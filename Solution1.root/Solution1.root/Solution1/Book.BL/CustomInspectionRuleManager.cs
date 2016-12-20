//------------------------------------------------------------------------------
//
// file name：CustomInspectionRuleManager.cs
// author: peidun
// create date：2009-08-12 9:45:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomInspectionRule.
    /// </summary>
    public partial class CustomInspectionRuleManager : BaseManager
    {
		
		/// <summary>
		/// Delete CustomInspectionRule by primary key.
		/// </summary>
		public void Delete(string customInspectionRuleId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(customInspectionRuleId);
		}

        public void Delete(Model.CustomInspectionRule customInspectionRule)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(customInspectionRule.CustomInspectionRuleId);
        }

		/// <summary>
		/// Insert a CustomInspectionRule.
		/// </summary>
        public void Insert(Model.CustomInspectionRule customInspectionRule)
        {
			//
			// todo:add other logic here
			//
            this.Validate(customInspectionRule);
            if (this.Exists(customInspectionRule.Id))
            {
                throw new Helper.InvalidValueException(Model.CustomInspectionRule.PROPERTY_ID);
            }
            if (this.HasRows(customInspectionRule.CustomInspectionRuleName))
            {
                throw new Helper.InvalidValueException(Model.CustomInspectionRule.PROPERTY_CUSTOMINSPECTIONRULENAME);

            }
            customInspectionRule.CustomInspectionRuleId = Guid.NewGuid().ToString();
            accessor.Insert(customInspectionRule);
        }
		
		/// <summary>
		/// Update a CustomInspectionRule.
		/// </summary>
        public void Update(Model.CustomInspectionRule customInspectionRule)
        {
			//
			// todo: add other logic here.
			//
            this.Validate(customInspectionRule);

            if (this.ExistsExcept(customInspectionRule))
            {
                throw new Helper.InvalidValueException(Model.CustomInspectionRule.PROPERTY_ID);
            }


            accessor.Update(customInspectionRule);
        }
        private void Validate(Model.CustomInspectionRule customInspectionRule)
        {
            if (string.IsNullOrEmpty(customInspectionRule.Id))
            {
                throw new Helper.RequireValueException(Model.CustomInspectionRule.PROPERTY_ID);
            }
            if (string.IsNullOrEmpty( customInspectionRule.CustomInspectionRuleName))
            {
                throw new Helper.RequireValueException(Model.CustomInspectionRule.PROPERTY_CUSTOMINSPECTIONRULENAME);
            }
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "CustomInspectionRule";
        //}

        //protected override string GetSettingId()
        //{
        //    return "CustomInspectionRuleRule";
        //}
    }
}

