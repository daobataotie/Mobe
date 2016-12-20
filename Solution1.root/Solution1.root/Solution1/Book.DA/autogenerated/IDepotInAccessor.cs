﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IDepotInAccessor.autogenerated.cs
// author: mayanjun
// create date：2010-10-25 16:14:45
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IDepotInAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.DepotIn Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.DepotIn e);
		
		void Update(Model.DepotIn e);
		
		IList<Model.DepotIn> Select();
		
		IList<Model.DepotIn> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.DepotIn e);
		
		bool HasRowsAfter(Model.DepotIn e);
		
		Model.DepotIn GetFirst();
		
		Model.DepotIn GetLast();
		
		Model.DepotIn GetPrev(Model.DepotIn e);
		
		Model.DepotIn GetNext(Model.DepotIn e);

	}
}

