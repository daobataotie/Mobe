﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IProductMouldCategoryAccessor.autogenerated.cs
// author: mayanjun
// create date：2013-3-7 14:17:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IProductMouldCategoryAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.ProductMouldCategory Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.ProductMouldCategory e);
		
		void Update(Model.ProductMouldCategory e);
		
		IList<Model.ProductMouldCategory> Select();
		
		IList<Model.ProductMouldCategory> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.ProductMouldCategory e);
		
		bool HasRowsAfter(Model.ProductMouldCategory e);
		
		Model.ProductMouldCategory GetFirst();
		
		Model.ProductMouldCategory GetLast();
		
		Model.ProductMouldCategory GetPrev(Model.ProductMouldCategory e);
		
		Model.ProductMouldCategory GetNext(Model.ProductMouldCategory e);

		bool Exists(string id);
		
		Model.ProductMouldCategory GetById(string id);
		
		bool ExistsExcept(Model.ProductMouldCategory e);
		
	}
}

