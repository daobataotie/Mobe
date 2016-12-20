//------------------------------------------------------------------------------
//
// file name：ThicknessTestDetailsManager.cs
// author: mayanjun
// create date：2012-4-24 10:33:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ThicknessTestDetails.
    /// </summary>
    public partial class ThicknessTestDetailsManager
    {
        public void Delete(string thicknessTestDetailsId)
        {
            accessor.Delete(thicknessTestDetailsId);
        }

        public void Insert(Model.ThicknessTestDetails thicknessTestDetails)
        {
            accessor.Insert(thicknessTestDetails);
        }

        public void Update(Model.ThicknessTestDetails thicknessTestDetails)
        {
            accessor.Update(thicknessTestDetails);
        }

        public IList<Model.ThicknessTestDetails> SelectByHeaderId(string thicknessTestId)
        {
            return accessor.SelectByHeaderId(thicknessTestId);
        }

        public void DeleteByheaderId(string thicknessTestId)
        {
            accessor.DeleteByheaderId(thicknessTestId);
        }
    }
}

