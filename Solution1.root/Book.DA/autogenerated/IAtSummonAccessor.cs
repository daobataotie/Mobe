﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IAtSummonAccessor.autogenerated.cs
// author: mayanjun
// create date：2011-3-3 14:30:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IAtSummonAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.AtSummon Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.AtSummon e);
		
		void Update(Model.AtSummon e);
		
		IList<Model.AtSummon> Select();
		
		IList<Model.AtSummon> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.AtSummon e);
		
		bool HasRowsAfter(Model.AtSummon e);
		
		Model.AtSummon GetFirst();
		
		Model.AtSummon GetLast();
		
		Model.AtSummon GetPrev(Model.AtSummon e);
		
		Model.AtSummon GetNext(Model.AtSummon e);

		bool Exists(string id);
		
		Model.AtSummon GetById(string id);
		
		bool ExistsExcept(Model.AtSummon e);
		
	}
}

