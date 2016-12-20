﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ICustomerProcessingAccessor.autogenerated.cs
// author: mayanjun
// create date：2010-7-30 19:31:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface ICustomerProcessingAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.CustomerProcessing Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.CustomerProcessing e);
		
		void Update(Model.CustomerProcessing e);
		
		IList<Model.CustomerProcessing> Select();
		
		IList<Model.CustomerProcessing> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.CustomerProcessing e);
		
		bool HasRowsAfter(Model.CustomerProcessing e);
		
		Model.CustomerProcessing GetFirst();
		
		Model.CustomerProcessing GetLast();
		
		Model.CustomerProcessing GetPrev(Model.CustomerProcessing e);
		
		Model.CustomerProcessing GetNext(Model.CustomerProcessing e);

		bool Exists(string id);
		
		Model.CustomerProcessing GetById(string id);
		
		bool ExistsExcept(Model.CustomerProcessing e);
		
	}
}

