//------------------------------------------------------------------------------
//
// file name：StockEditorAccessor.cs
// author: mayanjun
// create date：2010-11-4 11:02:34
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
    /// Data accessor of StockEditor
    /// </summary>
    public partial class StockEditorAccessor : EntityAccessor, IStockEditorAccessor
    {
        public IList<Model.StockEditor> SelectNoStockCheck()
        {
            return sqlmapper.QueryForList<Model.StockEditor>("StockEditor.select_noStockCheck", null);
        }
    }
}
