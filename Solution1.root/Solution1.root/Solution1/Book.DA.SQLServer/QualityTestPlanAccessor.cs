//------------------------------------------------------------------------------
//
// file name：QualityTestPlanAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:28
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of QualityTestPlan
    /// </summary>
    public partial class QualityTestPlanAccessor : EntityAccessor, IQualityTestPlanAccessor
    {
        public bool HasRowsName(string name)
        {
            return this.HasRowsName<Model.QualityTestPlan>(name);
        }
    }
}
