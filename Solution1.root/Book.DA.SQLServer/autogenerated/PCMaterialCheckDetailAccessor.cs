﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCMaterialCheckDetailAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/10/24 17:47:35
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
    public partial class PCMaterialCheckDetailAccessor
    {
		public Model.PCMaterialCheckDetail Get(string id)
		{
			return this.Get<Model.PCMaterialCheckDetail>(id);
		}
		
		public void Insert(Model.PCMaterialCheckDetail e)
		{
			this.Insert<Model.PCMaterialCheckDetail>(e);
		}
		
		public void Update(Model.PCMaterialCheckDetail e)
		{
			this.Update<Model.PCMaterialCheckDetail>(e);
		}
		
		public IList<Model.PCMaterialCheckDetail> Select()
		{
			return this.Select<Model.PCMaterialCheckDetail>();
		}
		
		public IList<Model.PCMaterialCheckDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.PCMaterialCheckDetail>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.PCMaterialCheckDetail>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.PCMaterialCheckDetail>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.PCMaterialCheckDetail>();
		}
		public int Count()
		{
			return this.Count<Model.PCMaterialCheckDetail>();
		}

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("PCMaterialCheckDetail.existsPrimary", id);
		}
    }
}
