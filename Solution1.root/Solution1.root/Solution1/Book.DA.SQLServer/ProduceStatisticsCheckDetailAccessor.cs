//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-07-22 10:44:53
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
    /// Data accessor of ProduceStatisticsCheckDetail
    /// </summary>
    public partial class ProduceStatisticsCheckDetailAccessor : EntityAccessor, IProduceStatisticsCheckDetailAccessor
    {
        public IList<Book.Model.ProduceStatisticsCheckDetail> Select(Model.ProduceStatisticsCheck produceStatisticsCheck)
        {
            return sqlmapper.QueryForList<Model.ProduceStatisticsCheckDetail>("ProduceStatisticsCheckDetail.select_byProduceStatisticsCheckId", produceStatisticsCheck.ProduceStatisticsCheckId);
        }
    }
}
