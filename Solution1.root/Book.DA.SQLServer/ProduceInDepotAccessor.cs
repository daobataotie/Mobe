//------------------------------------------------------------------------------
//
// file name：ProduceInDepotAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:37
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
    /// Data accessor of ProduceInDepot
    /// </summary>
    public partial class ProduceInDepotAccessor : EntityAccessor, IProduceInDepotAccessor
    {
        public IList<Book.Model.ProduceInDepot> SelectByDateRange(DateTime stardate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("stardate", stardate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.ProduceInDepot>("ProduceInDepot.SelectByDateRange", ht);
        }

        public IList<Model.ProduceInDepot> SelectExcel(DateTime startDate, DateTime endDate, string workHouseId, string keyWords)
        {
            StringBuilder sb = new StringBuilder("select wh.Workhousename,p.ProductName,pid.ProceduresSum,pid.CheckOutSum,pid.ProduceTransferQuantity,pid.ProduceQuantity from ProduceInDepotDetail pid left join ProduceInDepot pi on pid.ProduceInDepotId=pi.ProduceInDepotId left join WorkHouse wh on pi.WorkHouseId=wh.WorkHouseId left join Product p on pid.ProductId=p.ProductId where pi.ProduceInDepotDate between '" + startDate.Date + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1) + "' and pid.ProductId in (select ProductId from ProductClassifyDetail where ProductClassifyId in (select ProductClassifyId from ProductClassify where KeyWord = '" + keyWords + "'))");
            if (!string.IsNullOrEmpty(workHouseId))
                sb.Append(" and pi.WorkHouseId='" + workHouseId + "'");
            return this.DataReaderBind<Model.ProduceInDepot>(sb.ToString(), null, CommandType.Text);
        }
    }
}
