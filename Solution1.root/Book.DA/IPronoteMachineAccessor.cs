//------------------------------------------------------------------------------
//
// file name：IPronoteMachineAccessor.cs
// author: mayanjun
// create date：2010-9-16 9:27:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronoteMachine
    /// </summary>
    public partial interface IPronoteMachineAccessor : IAccessor
    {
        DataTable GetAll();
        bool ExistsName(string name, string pid);
        bool ExistsId(string id, string pid);
        void SaveInfo(System.Data.DataTable Deport);
        IList<Model.PronoteMachine> SelectMachineByProduresId(string ProduresId);
        Model.PronoteMachine SelectMachineByName(string name);
        IList<Model.PronoteMachine> GetPronoteMachineByPronoteProceduresDetailId(string PronoteProceduresDetailId);
    }
}

