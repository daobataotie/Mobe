﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCOpticsCheckAccessor.autogenerated.cs
// author: mayanjun
// create date：2012-3-16 17:41:45
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCOpticsCheckAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCOpticsCheck Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCOpticsCheck e);
		
		void Update(Model.PCOpticsCheck e);
		
		IList<Model.PCOpticsCheck> Select();
		
		IList<Model.PCOpticsCheck> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.PCOpticsCheck e);
		
		bool HasRowsAfter(Model.PCOpticsCheck e);
		
		Model.PCOpticsCheck GetFirst();
		
		Model.PCOpticsCheck GetLast();
		
		Model.PCOpticsCheck GetPrev(Model.PCOpticsCheck e);
		
		Model.PCOpticsCheck GetNext(Model.PCOpticsCheck e);

	}
}

