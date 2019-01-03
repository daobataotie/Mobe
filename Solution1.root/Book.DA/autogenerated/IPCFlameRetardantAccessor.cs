﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCFlameRetardantAccessor.autogenerated.cs
// author: mayanjun
// create date：2018/12/27 13:18:23
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCFlameRetardantAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCFlameRetardant Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCFlameRetardant e);
		
		void Update(Model.PCFlameRetardant e);
		
		IList<Model.PCFlameRetardant> Select();
		
		IList<Model.PCFlameRetardant> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.PCFlameRetardant e);
		
		bool HasRowsAfter(Model.PCFlameRetardant e);
		
		Model.PCFlameRetardant GetFirst();
		
		Model.PCFlameRetardant GetLast();
		
		Model.PCFlameRetardant GetPrev(Model.PCFlameRetardant e);
		
		Model.PCFlameRetardant GetNext(Model.PCFlameRetardant e);

	}
}