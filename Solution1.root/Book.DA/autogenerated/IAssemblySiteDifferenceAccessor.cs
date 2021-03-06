﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IAssemblySiteDifferenceAccessor.autogenerated.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IAssemblySiteDifferenceAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.AssemblySiteDifference Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.AssemblySiteDifference e);
		
		void Update(Model.AssemblySiteDifference e);
		
		IList<Model.AssemblySiteDifference> Select();
		
		IList<Model.AssemblySiteDifference> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.AssemblySiteDifference e);
		
		bool HasRowsAfter(Model.AssemblySiteDifference e);
		
		Model.AssemblySiteDifference GetFirst();
		
		Model.AssemblySiteDifference GetLast();
		
		Model.AssemblySiteDifference GetPrev(Model.AssemblySiteDifference e);
		
		Model.AssemblySiteDifference GetNext(Model.AssemblySiteDifference e);

	}
}
