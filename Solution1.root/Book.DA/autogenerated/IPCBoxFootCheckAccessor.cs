﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCBoxFootCheckAccessor.autogenerated.cs
// author: mayanjun
// create date：2013-1-29 16:06:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCBoxFootCheckAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCBoxFootCheck Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCBoxFootCheck e);
		
		void Update(Model.PCBoxFootCheck e);
		
		IList<Model.PCBoxFootCheck> Select();
		
		IList<Model.PCBoxFootCheck> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.PCBoxFootCheck e);
		
		bool HasRowsAfter(Model.PCBoxFootCheck e);
		
		Model.PCBoxFootCheck GetFirst();
		
		Model.PCBoxFootCheck GetLast();
		
		Model.PCBoxFootCheck GetPrev(Model.PCBoxFootCheck e);
		
		Model.PCBoxFootCheck GetNext(Model.PCBoxFootCheck e);

	}
}

