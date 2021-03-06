﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCMaterialCheckDetailAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/10/24 17:47:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCMaterialCheckDetailAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCMaterialCheckDetail Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCMaterialCheckDetail e);
		
		void Update(Model.PCMaterialCheckDetail e);
		
		IList<Model.PCMaterialCheckDetail> Select();
		
		IList<Model.PCMaterialCheckDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);

	}
}
