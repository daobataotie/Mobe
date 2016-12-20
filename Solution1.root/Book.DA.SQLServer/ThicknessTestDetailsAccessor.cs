//------------------------------------------------------------------------------
//
// file name：ThicknessTestDetailsAccessor.cs
// author: mayanjun
// create date：2012-4-24 10:33:14
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
    /// Data accessor of ThicknessTestDetails
    /// </summary>
    public partial class ThicknessTestDetailsAccessor : EntityAccessor, IThicknessTestDetailsAccessor
    {
        public IList<Book.Model.ThicknessTestDetails> SelectByHeaderId(string thicknessTestId)
        {
            return sqlmapper.QueryForList<Model.ThicknessTestDetails>("ThicknessTestDetails.SelectByHeaderId", thicknessTestId);
        }

        public void DeleteByheaderId(string thicknessTestId)
        {
            sqlmapper.Delete("ThicknessTestDetails.DeleteByheaderId", thicknessTestId);
        }
    }
}
