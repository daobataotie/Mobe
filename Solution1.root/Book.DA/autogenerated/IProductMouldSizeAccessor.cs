﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IProductMouldSizeAccessor.autogenerated.cs
// author: mayanjun
// create date：2013-2-21 17:29:15
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IProductMouldSizeAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.ProductMouldSize Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.ProductMouldSize e);
		
		void Update(Model.ProductMouldSize e);
		
		IList<Model.ProductMouldSize> Select();
		
		IList<Model.ProductMouldSize> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.ProductMouldSize e);
		
		bool HasRowsAfter(Model.ProductMouldSize e);
		
		Model.ProductMouldSize GetFirst();
		
		Model.ProductMouldSize GetLast();
		
		Model.ProductMouldSize GetPrev(Model.ProductMouldSize e);
		
		Model.ProductMouldSize GetNext(Model.ProductMouldSize e);

	}
}

