﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IProduceMaterialExitAccessor.autogenerated.cs
// author: peidun
// create date：2010-1-6 10:20:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IProduceMaterialExitAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.ProduceMaterialExit Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.ProduceMaterialExit e);
		
		void Update(Model.ProduceMaterialExit e);
		
		IList<Model.ProduceMaterialExit> Select();
		
		IList<Model.ProduceMaterialExit> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		bool HasRowsBefore(Model.ProduceMaterialExit e);
		
		bool HasRowsAfter(Model.ProduceMaterialExit e);
		
		Model.ProduceMaterialExit GetFirst();
		
		Model.ProduceMaterialExit GetLast();
		
		Model.ProduceMaterialExit GetPrev(Model.ProduceMaterialExit e);
		
		Model.ProduceMaterialExit GetNext(Model.ProduceMaterialExit e);

	}
}

