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


        #region 适用于首件上线检查表

        Model.ThicknessTest PFCGetFirst(string PCFirstOnlineCheckDetailId);

        Model.ThicknessTest PFCGetLast(string PCFirstOnlineCheckDetailId);

        Model.ThicknessTest PFCGetPrev(DateTime InsertDate, string PCFirstOnlineCheckDetailId);

        Model.ThicknessTest PFCGetNext(DateTime InsertDate, string PCFirstOnlineCheckDetailId);

        bool PFCHasRows(string PCFirstOnlineCheckDetailId);

        bool PFCHasRowsBefore(Model.ThicknessTest e, string PCFirstOnlineCheckDetailId);

        bool PFCHasRowsAfter(Model.ThicknessTest e, string PCFirstOnlineCheckDetailId);

        IList<Model.ThicknessTest> PFCSelect(string PCFirstOnlineCheckDetailId);

        IList<Model.ThicknessTest> PFCSelectByDateRage(DateTime startdate, DateTime enddate, string PCFirstOnlineCheckDetailId);

        #endregion
    }
}

