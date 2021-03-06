﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCEarProtectCheckDetailAccessor.autogenerated.cs
// author: mayanjun
// create date：2013-09-03 15:16:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCEarProtectCheckDetailAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCEarProtectCheckDetail Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCEarProtectCheckDetail e);
		
		void Update(Model.PCEarProtectCheckDetail e);
		
		IList<Model.PCEarProtectCheckDetail> Select();
		
		IList<Model.PCEarProtectCheckDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);

	}
}

