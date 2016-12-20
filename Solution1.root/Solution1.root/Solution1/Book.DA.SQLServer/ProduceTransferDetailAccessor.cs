//------------------------------------------------------------------------------
//
// file name：ProduceTransferDetailAccessor.cs
// author: mayanjun
// create date：2011-4-6 10:53:38
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
    /// Data accessor of ProduceTransferDetail
    /// </summary>
    public partial class ProduceTransferDetailAccessor : EntityAccessor, IProduceTransferDetailAccessor
    {
        public IList<Book.Model.ProduceTransferDetail> Select(Model.ProduceTransfer produceTransfer)
        {
            return sqlmapper.QueryForList<Model.ProduceTransferDetail>("ProduceTransferDetail.select_byProduceTransferId", produceTransfer.ProduceTransferId);
        }
    }
}
