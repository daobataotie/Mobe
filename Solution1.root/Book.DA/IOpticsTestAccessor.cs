//------------------------------------------------------------------------------
//
// file name：IOpticsTestAccessor.cs
// author: mayanjun
// create date：2012-4-21 09:55:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.OpticsTest
    /// </summary>
    public partial interface IOpticsTestAccessor : IAccessor
    {
        Model.OpticsTest mGetFirst(string PCPGOnlineCheckDetailId);

        Model.OpticsTest mGetLast(string PCPGOnlineCheckDetailId);

        Model.OpticsTest mGetPrev(DateTime InsertDate, string PCPGOnlineCheckDetailId);

        Model.OpticsTest mGetNext(DateTime InsertDate, string PCPGOnlineCheckDetailId);

        bool mHasRows(string PCPGOnlineCheckDetailId);

        bool mHasRowsBefore(Model.OpticsTest e, string PCPGOnlineCheckDetailId);

        bool mHasRowsAfter(Model.OpticsTest e, string PCPGOnlineCheckDetailId);

        IList<Model.OpticsTest> mSelect(string PCPGOnlineCheckDetailId);

        IList<Model.OpticsTest> SelectByDateRage(DateTime startdate, DateTime enddate, string PCPGOnlineCheckDetailId);

        void DeleteByPCPGOnlineCheckDetailId(string PCPGOnlineCheckDetailId);

        bool ExistsManualId(string OpticsTestid, string ManualId);



        Book.Model.OpticsTest FGetFirst(string PCFinishCheckId);

        Book.Model.OpticsTest FGetLast(string PCFinishCheckId);

        Book.Model.OpticsTest FGetPrev(DateTime InsertDate, string PCFinishCheckId);

        Book.Model.OpticsTest FGetNext(DateTime InsertDate, string PCFinishCheckId);

        bool FHasRows(string PCFinishCheckId);

        bool FHasRowsBefore(Book.Model.OpticsTest e, string PCFinishCheckId);

        bool FHasRowsAfter(Book.Model.OpticsTest e, string PCFinishCheckId);

        IList<Book.Model.OpticsTest> FSelect(string PCFinishCheckId);

        IList<Book.Model.OpticsTest> FSelectByDateRage(DateTime startdate, DateTime enddate, string PCFinishCheckId);
    }
}

