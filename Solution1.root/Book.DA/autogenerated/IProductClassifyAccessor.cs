﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IProductClassifyAccessor.autogenerated.cs
// author: mayanjun
// create date：2017-08-24 21:38:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IProductClassifyAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.ProductClassify Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.ProductClassify e);
		
		void Update(Model.ProductClassify e);
		
		IList<Model.ProductClassify> Select();
		
		IList<Model.ProductClassify> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.ProductClassify e);
		
		bool HasRowsAfter(Model.ProductClassify e);
		
		Model.ProductClassify GetFirst();
		
		Model.ProductClassify GetLast();
		
		Model.ProductClassify GetPrev(Model.ProductClassify e);
		
		Model.ProductClassify GetNext(Model.ProductClassify e);

	}
}
