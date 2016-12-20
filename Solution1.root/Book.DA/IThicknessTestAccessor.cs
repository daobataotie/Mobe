//------------------------------------------------------------------------------
//
// file name：IThicknessTestAccessor.cs
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
    /// Interface of data accessor of dbo.ThicknessTest
    /// </summary>
    public partial interface IThicknessTestAccessor : IAccessor
    {
        Model.ThicknessTest mGetFirst(string PCPGOnlineCheckDetailId);

        Model.ThicknessTest mGetLast(string PCPGOnlineCheckDetailId);

        Model.ThicknessTest mGetPrev(DateTime InsertDate, string PCPGOnlineCheckDetailId);

        Model.ThicknessTest mGetNext(DateTime InsertDate, string PCPGOnlineCheckDetailId);

        bool mHasRows(string PCPGOnlineCheckDetailId);

        bool mHasRowsBefore(Model.ThicknessTest e, string PCPGOnlineCheckDetailId);

        bool mHasRowsAfter(Model.ThicknessTest e, string PCPGOnlineCheckDetailId);

        IList<Model.ThicknessTest> mSelect(string PCPGOnlineCheckDetailId);

        IList<Model.ThicknessTest> SelectByDateRage(DateTime startdate, DateTime enddate, string PCPGOnlineCheckDetailId);

        bool ExistsManualId(string ThicknessTestId, string ManualId);

        void DeleteByPCPGOnlineCheckDetailId(string PCPGOnlineCheckDetailId);
    }
}

