﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IInvoiceJRAccessor.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IInvoiceJRAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.InvoiceJR Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.InvoiceJR e);
		
		void Update(Model.InvoiceJR e);
		
		IList<Model.InvoiceJR> Select();
		
		IList<Model.InvoiceJR> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		bool HasRowsBefore(Model.InvoiceJR e);
		
		bool HasRowsAfter(Model.InvoiceJR e);
		
		Model.InvoiceJR GetFirst();
		
		Model.InvoiceJR GetLast();
		
		Model.InvoiceJR GetPrev(Model.InvoiceJR e);
		
		Model.InvoiceJR GetNext(Model.InvoiceJR e);

	}
}

