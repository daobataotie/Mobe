﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IInvoiceJCAccessor.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IInvoiceJCAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.InvoiceJC Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.InvoiceJC e);
		
		void Update(Model.InvoiceJC e);
		
		IList<Model.InvoiceJC> Select();
		
		IList<Model.InvoiceJC> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		bool HasRowsBefore(Model.InvoiceJC e);
		
		bool HasRowsAfter(Model.InvoiceJC e);
		
		Model.InvoiceJC GetFirst();
		
		Model.InvoiceJC GetLast();
		
		Model.InvoiceJC GetPrev(Model.InvoiceJC e);
		
		Model.InvoiceJC GetNext(Model.InvoiceJC e);

	}
}

