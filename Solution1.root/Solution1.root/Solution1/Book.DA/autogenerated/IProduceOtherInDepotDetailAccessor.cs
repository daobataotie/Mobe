﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IProduceOtherInDepotDetailAccessor.autogenerated.cs
// author: peidun
// create date：2010-1-8 13:43:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IProduceOtherInDepotDetailAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.ProduceOtherInDepotDetail Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.ProduceOtherInDepotDetail e);
		
		void Update(Model.ProduceOtherInDepotDetail e);
		
		IList<Model.ProduceOtherInDepotDetail> Select();
		
		IList<Model.ProduceOtherInDepotDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);

	}
}

