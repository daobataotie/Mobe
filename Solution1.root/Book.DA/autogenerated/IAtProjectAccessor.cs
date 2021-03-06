﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IAtProjectAccessor.autogenerated.cs
// author: mayanjun
// create date：2011-3-3 16:06:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IAtProjectAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.AtProject Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.AtProject e);
		
		void Update(Model.AtProject e);
		
		IList<Model.AtProject> Select();
		
		IList<Model.AtProject> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.AtProject e);
		
		bool HasRowsAfter(Model.AtProject e);
		
		Model.AtProject GetFirst();
		
		Model.AtProject GetLast();
		
		Model.AtProject GetPrev(Model.AtProject e);
		
		Model.AtProject GetNext(Model.AtProject e);

		bool Exists(string id);
		
		Model.AtProject GetById(string id);
		
		bool ExistsExcept(Model.AtProject e);
		
	}
}

