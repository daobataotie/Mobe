﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCSamplingAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/10/30 17:07:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCSamplingAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCSampling Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCSampling e);
		
		void Update(Model.PCSampling e);
		
		IList<Model.PCSampling> Select();
		
		IList<Model.PCSampling> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.PCSampling e);
		
		bool HasRowsAfter(Model.PCSampling e);
		
		Model.PCSampling GetFirst();
		
		Model.PCSampling GetLast();
		
		Model.PCSampling GetPrev(Model.PCSampling e);
		
		Model.PCSampling GetNext(Model.PCSampling e);

	}
}
