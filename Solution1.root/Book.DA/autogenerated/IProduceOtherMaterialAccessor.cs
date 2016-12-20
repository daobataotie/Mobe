﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IProduceOtherMaterialAccessor.autogenerated.cs
// author: peidun
// create date：2010-1-5 15:36:45
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IProduceOtherMaterialAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.ProduceOtherMaterial Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.ProduceOtherMaterial e);
		
		void Update(Model.ProduceOtherMaterial e);
		
		IList<Model.ProduceOtherMaterial> Select();
		
		IList<Model.ProduceOtherMaterial> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		bool HasRowsBefore(Model.ProduceOtherMaterial e);
		
		bool HasRowsAfter(Model.ProduceOtherMaterial e);
		
		Model.ProduceOtherMaterial GetFirst();
		
		Model.ProduceOtherMaterial GetLast();
		
		Model.ProduceOtherMaterial GetPrev(Model.ProduceOtherMaterial e);
		
		Model.ProduceOtherMaterial GetNext(Model.ProduceOtherMaterial e);

	}
}

