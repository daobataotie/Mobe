﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCSamplingEarDetailAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/10/31 16:25:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCSamplingEarDetailAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCSamplingEarDetail Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCSamplingEarDetail e);
		
		void Update(Model.PCSamplingEarDetail e);
		
		IList<Model.PCSamplingEarDetail> Select();
		
		IList<Model.PCSamplingEarDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);

	}
}
