//------------------------------------------------------------------------------
//
// file name：AssemblySiteInventoryAccessor.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
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
    /// Data accessor of AssemblySiteInventory
    /// </summary>
    public partial class AssemblySiteInventoryAccessor : EntityAccessor, IAssemblySiteInventoryAccessor
    {
        public void UpdateState(bool state, string assemblySiteInventoryId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceState", state);
            ht.Add("AssemblySiteInventoryId", assemblySiteInventoryId);
            sqlmapper.Update("AssemblySiteInventory.UpdateState", ht);
        }
    }
}
