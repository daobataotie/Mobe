//------------------------------------------------------------------------------
//
// file name：IAcademicBackGroundAccessor.cs
// author: peidun
// create date：2009-09-02 上午 10:38:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcademicBackGround
    /// </summary>
    public partial interface IAcademicBackGroundAccessor : IAccessor
    {
        void UpdateDataTable(DataTable accounts);
        DataSet SelectNoModel();
        bool Selectbyname(string academname);
        bool IsExistName(string id, string name);

    }
}

