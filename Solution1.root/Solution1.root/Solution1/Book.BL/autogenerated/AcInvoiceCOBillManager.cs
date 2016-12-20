﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcInvoiceCOBillManager.autogenerated.cs
// author: mayanjun
// create date：2011-06-27 15:08:17
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class AcInvoiceCOBillManager
    {
		///<summary>
		/// Data accessor of dbo.AcInvoiceCOBill
		///</summary>
		private static readonly DA.IAcInvoiceCOBillAccessor accessor = (DA.IAcInvoiceCOBillAccessor)Accessors.Get("AcInvoiceCOBillAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.AcInvoiceCOBill Get(string acInvoiceCOBillId)
		{
			return accessor.Get(acInvoiceCOBillId);
		}
		
		public bool HasRows(string acInvoiceCOBillId)
		{
			return accessor.HasRows(acInvoiceCOBillId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool Exists(string id)
		{
			return accessor.Exists(id);
		}
		
		public Model.AcInvoiceCOBill GetById(string id)
		{
			return accessor.GetById(id);
		}		
		public bool ExistsExcept(Model.AcInvoiceCOBill e)
		{
			return accessor.ExistsExcept(e);
		}
		
		
		public bool HasRowsBefore(Model.AcInvoiceCOBill e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.AcInvoiceCOBill e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.AcInvoiceCOBill GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.AcInvoiceCOBill GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.AcInvoiceCOBill GetPrev(Model.AcInvoiceCOBill e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.AcInvoiceCOBill GetNext(Model.AcInvoiceCOBill e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.AcInvoiceCOBill> Select()
		{
			return accessor.Select();
		}
		
		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int Count()
		{
			return accessor.Count();
		}
		
		/// <summary>
		/// 获取指定状态、指定分页，并按指定要求排序的记录
		/// </summary>
		public IList<Model.AcInvoiceCOBill> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
        protected override string GetInvoiceKind()
        {
            return "AcInvoiceCOBill";
        }

        protected override string GetSettingId()
        {
            return "AcInvoiceCOBillRule";
        }
		
    }
}
