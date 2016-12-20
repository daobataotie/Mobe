//------------------------------------------------------------------------------
//
// file name：IExportSendMailAccessor.cs
// author: mayanjun
// create date：2012-6-21 10:58:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ExportSendMail
    /// </summary>
    public partial interface IExportSendMailAccessor : IAccessor
    {
        IList<Model.ExportSendMail> SelectByDateRage(DateTime startdate, DateTime enddate);
    }
}

