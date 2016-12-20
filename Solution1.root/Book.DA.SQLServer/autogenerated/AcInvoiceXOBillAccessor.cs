﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcInvoiceXOBillAccessor.autogenerated.cs
// author: mayanjun
// create date：2011-09-28 10:28:21
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
    public partial class AcInvoiceXOBillAccessor
    {
		public Model.AcInvoiceXOBill Get(string id)
		{
			return this.Get<Model.AcInvoiceXOBill>(id);
		}
		
		public void Insert(Model.AcInvoiceXOBill e)
		{
			this.Insert<Model.AcInvoiceXOBill>(e);
		}
		
		public void Update(Model.AcInvoiceXOBill e)
		{
			this.Update<Model.AcInvoiceXOBill>(e);
		}
		
		public IList<Model.AcInvoiceXOBill> Select()
		{
			return this.Select<Model.AcInvoiceXOBill>();
		}
		
		public IList<Model.AcInvoiceXOBill> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.AcInvoiceXOBill>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.AcInvoiceXOBill>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.AcInvoiceXOBill>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.AcInvoiceXOBill>();
		}
		public int Count()
		{
			return this.Count<Model.AcInvoiceXOBill>();
		}
		public bool HasRowsBefore(Model.AcInvoiceXOBill e)
		{
			return sqlmapper.QueryForObject<bool>("AcInvoiceXOBill.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.AcInvoiceXOBill e)
		{
			return sqlmapper.QueryForObject<bool>("AcInvoiceXOBill.has_rows_after", e);
		}
		public Model.AcInvoiceXOBill GetFirst()
		{
			return sqlmapper.QueryForObject<Model.AcInvoiceXOBill>("AcInvoiceXOBill.get_first", null);
		}
		public Model.AcInvoiceXOBill GetLast()
		{
			return sqlmapper.QueryForObject<Model.AcInvoiceXOBill>("AcInvoiceXOBill.get_last", null);
		}
		public Model.AcInvoiceXOBill GetNext(Model.AcInvoiceXOBill e)
		{
			return sqlmapper.QueryForObject<Model.AcInvoiceXOBill>("AcInvoiceXOBill.get_next", e);
		}
		public Model.AcInvoiceXOBill GetPrev(Model.AcInvoiceXOBill e)
		{
			return sqlmapper.QueryForObject<Model.AcInvoiceXOBill>("AcInvoiceXOBill.get_prev", e);
		}
		

		public bool Exists(string id)
		{
			return sqlmapper.QueryForObject<bool>("AcInvoiceXOBill.exists", id);
		}
		
		public Model.AcInvoiceXOBill GetById(string id)
		{
			return sqlmapper.QueryForObject<Model.AcInvoiceXOBill>("AcInvoiceXOBill.get_by_id", id);
		}
		
		public bool ExistsExcept(Model.AcInvoiceXOBill e)
		{
			Hashtable paras = new Hashtable();
			paras.Add("newId", e.Id);
            paras.Add("oldId", Get(e.AcInvoiceXOBillId)==null?null:Get(e.AcInvoiceXOBillId).Id);
			return sqlmapper.QueryForObject<bool>("AcInvoiceXOBill.existsexcept", paras);
		}
		
		
		
		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("AcInvoiceXOBill.existsPrimary", id);
		}
    }
}
