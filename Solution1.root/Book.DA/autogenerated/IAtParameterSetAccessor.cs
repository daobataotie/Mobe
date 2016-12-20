﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IAtParameterSetAccessor.autogenerated.cs
// author: mayanjun
// create date：2012-3-26 14:47:51
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IAtParameterSetAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.AtParameterSet Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.AtParameterSet e);
		
		void Update(Model.AtParameterSet e);
		
		IList<Model.AtParameterSet> Select();
		
		IList<Model.AtParameterSet> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.AtParameterSet e);
		
		bool HasRowsAfter(Model.AtParameterSet e);
		
		Model.AtParameterSet GetFirst();
		
		Model.AtParameterSet GetLast();
		
		Model.AtParameterSet GetPrev(Model.AtParameterSet e);
		
		Model.AtParameterSet GetNext(Model.AtParameterSet e);

	}
}

