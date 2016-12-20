﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCMaterialCheckAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/10/24 17:47:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCMaterialCheckAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCMaterialCheck Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCMaterialCheck e);
		
		void Update(Model.PCMaterialCheck e);
		
		IList<Model.PCMaterialCheck> Select();
		
		IList<Model.PCMaterialCheck> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);
		bool HasRowsBefore(Model.PCMaterialCheck e);
		
		bool HasRowsAfter(Model.PCMaterialCheck e);
		
		Model.PCMaterialCheck GetFirst();
		
		Model.PCMaterialCheck GetLast();
		
		Model.PCMaterialCheck GetPrev(Model.PCMaterialCheck e);
		
		Model.PCMaterialCheck GetNext(Model.PCMaterialCheck e);

	}
}
