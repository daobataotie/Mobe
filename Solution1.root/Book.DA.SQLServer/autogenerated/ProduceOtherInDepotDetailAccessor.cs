﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceOtherInDepotDetailAccessor.autogenerated.cs
// author: peidun
// create date：2010-1-8 13:43:36
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
    public partial class ProduceOtherInDepotDetailAccessor
    {
		public Model.ProduceOtherInDepotDetail Get(string id)
		{
			return this.Get<Model.ProduceOtherInDepotDetail>(id);
		}
		
		public void Insert(Model.ProduceOtherInDepotDetail e)
		{
			this.Insert<Model.ProduceOtherInDepotDetail>(e);
		}
		
		public void Update(Model.ProduceOtherInDepotDetail e)
		{
			this.Update<Model.ProduceOtherInDepotDetail>(e);
		}
		
		public IList<Model.ProduceOtherInDepotDetail> Select()
		{
			return this.Select<Model.ProduceOtherInDepotDetail>();
		}
		
		public IList<Model.ProduceOtherInDepotDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.ProduceOtherInDepotDetail>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.ProduceOtherInDepotDetail>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.ProduceOtherInDepotDetail>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.ProduceOtherInDepotDetail>();
		}
		public int Count()
		{
			return this.Count<Model.ProduceOtherInDepotDetail>();
		}

    }
}
