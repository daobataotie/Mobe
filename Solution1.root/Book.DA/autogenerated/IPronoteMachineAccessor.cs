﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPronoteMachineAccessor.autogenerated.cs
// author: mayanjun
// create date：2010-9-16 16:37:29
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPronoteMachineAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PronoteMachine Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PronoteMachine e);
		
		void Update(Model.PronoteMachine e);
		
		IList<Model.PronoteMachine> Select();
		
		IList<Model.PronoteMachine> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.PronoteMachine e);
		
		bool HasRowsAfter(Model.PronoteMachine e);
		
		Model.PronoteMachine GetFirst();
		
		Model.PronoteMachine GetLast();
		
		Model.PronoteMachine GetPrev(Model.PronoteMachine e);
		
		Model.PronoteMachine GetNext(Model.PronoteMachine e);

		bool Exists(string id);
		
		Model.PronoteMachine GetById(string id);
		
		bool ExistsExcept(Model.PronoteMachine e);
		
	}
}

