﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IOutgoingKindAccessor.autogenerated.cs
// author: peidun
// create date：2010-4-7 15:57:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IOutgoingKindAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.OutgoingKind Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.OutgoingKind e);
		
		void Update(Model.OutgoingKind e);
		
		IList<Model.OutgoingKind> Select();
		
		IList<Model.OutgoingKind> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.OutgoingKind e);
		
		bool HasRowsAfter(Model.OutgoingKind e);
		
		Model.OutgoingKind GetFirst();
		
		Model.OutgoingKind GetLast();
		
		Model.OutgoingKind GetPrev(Model.OutgoingKind e);
		
		Model.OutgoingKind GetNext(Model.OutgoingKind e);

		bool Exists(string id);
		
		Model.OutgoingKind GetById(string id);
		
		bool ExistsExcept(Model.OutgoingKind e);
		
	}
}

