﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IInvoiceZSDetailAccessor.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IInvoiceZSDetailAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.InvoiceZSDetail Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.InvoiceZSDetail e);
		
		void Update(Model.InvoiceZSDetail e);
		
		IList<Model.InvoiceZSDetail> Select();
		
		IList<Model.InvoiceZSDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);

	}
}

