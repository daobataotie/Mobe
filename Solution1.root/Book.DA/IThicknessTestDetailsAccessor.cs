//------------------------------------------------------------------------------
//
// file name：IThicknessTestDetailsAccessor.cs
// author: mayanjun
// create date：2012-4-24 10:33:14
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ThicknessTestDetails
    /// </summary>
    public partial interface IThicknessTestDetailsAccessor : IAccessor
    {
        IList<Model.ThicknessTestDetails> SelectByHeaderId(string thicknessTestId);

        void DeleteByheaderId(string thicknessTestId);
    }
}

