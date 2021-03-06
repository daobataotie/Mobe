﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IMRSHeaderAccessor.autogenerated.cs
// author: peidun
// create date：2009-12-18 11:23:45
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IMRSHeaderAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.MRSHeader Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.MRSHeader e);
		
		void Update(Model.MRSHeader e);
		
		IList<Model.MRSHeader> Select();
		
		IList<Model.MRSHeader> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		bool HasRowsBefore(Model.MRSHeader e);
		
		bool HasRowsAfter(Model.MRSHeader e);
		
		Model.MRSHeader GetFirst();
		
		Model.MRSHeader GetLast();
		
		Model.MRSHeader GetPrev(Model.MRSHeader e);
		
		Model.MRSHeader GetNext(Model.MRSHeader e);

		bool Exists(string id);
		
		Model.MRSHeader GetById(string id);
		
		bool ExistsExcept(Model.MRSHeader e);
	}
}

