//------------------------------------------------------------------------------
//
// file name：IQualityTestPlanAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:25
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.QualityTestPlan
    /// </summary>
    public partial interface IQualityTestPlanAccessor:IAccessor
    {
      bool HasRowsName(string name);
    }
}

