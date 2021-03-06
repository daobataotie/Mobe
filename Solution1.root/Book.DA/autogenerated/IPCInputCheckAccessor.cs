﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCInputCheckAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/4/18 下午 12:23:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCInputCheckAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCInputCheck Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCInputCheck e);
		
		void Update(Model.PCInputCheck e);
		
		IList<Model.PCInputCheck> Select();
		
		IList<Model.PCInputCheck> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.PCInputCheck e);
		
		bool HasRowsAfter(Model.PCInputCheck e);
		
		Model.PCInputCheck GetFirst();
		
		Model.PCInputCheck GetLast();
		
		Model.PCInputCheck GetPrev(Model.PCInputCheck e);
		
		Model.PCInputCheck GetNext(Model.PCInputCheck e);

	}
}
