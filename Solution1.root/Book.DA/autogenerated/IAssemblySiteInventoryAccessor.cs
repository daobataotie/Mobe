﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IAssemblySiteInventoryAccessor.autogenerated.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IAssemblySiteInventoryAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.AssemblySiteInventory Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.AssemblySiteInventory e);
		
		void Update(Model.AssemblySiteInventory e);
		
		IList<Model.AssemblySiteInventory> Select();
		
		IList<Model.AssemblySiteInventory> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.AssemblySiteInventory e);
		
		bool HasRowsAfter(Model.AssemblySiteInventory e);
		
		Model.AssemblySiteInventory GetFirst();
		
		Model.AssemblySiteInventory GetLast();
		
		Model.AssemblySiteInventory GetPrev(Model.AssemblySiteInventory e);
		
		Model.AssemblySiteInventory GetNext(Model.AssemblySiteInventory e);

	}
}
