﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：BomPackageDetailsAccessor.autogenerated.cs
// author: peidun
// create date：2009-11-12 11:47:18
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
    public partial class BomPackageDetailsAccessor
    {
		public Model.BomPackageDetails Get(string id)
		{
			return this.Get<Model.BomPackageDetails>(id);
		}
		
		public void Insert(Model.BomPackageDetails e)
		{
			this.Insert<Model.BomPackageDetails>(e);
		}
		
		public void Update(Model.BomPackageDetails e)
		{
			this.Update<Model.BomPackageDetails>(e);
		}
		
		public IList<Model.BomPackageDetails> Select()
		{
			return this.Select<Model.BomPackageDetails>();
		}
		
		public IList<Model.BomPackageDetails> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.BomPackageDetails>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.BomPackageDetails>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.BomPackageDetails>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.BomPackageDetails>();
		}
		public int Count()
		{
			return this.Count<Model.BomPackageDetails>();
		}

    }
}
