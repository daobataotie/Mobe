﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPCBoxFootCheckDetailAccessor.autogenerated.cs
// author: mayanjun
// create date：2013-08-16 10:26:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPCBoxFootCheckDetailAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PCBoxFootCheckDetail Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PCBoxFootCheckDetail e);
		
		void Update(Model.PCBoxFootCheckDetail e);
		
		IList<Model.PCBoxFootCheckDetail> Select();
		
		IList<Model.PCBoxFootCheckDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		
		bool ExistsPrimary(string id);


        IList<Book.Model.PCBoxFootCheckDetail> SelectByPCBoxFootCheckId(string id);

        void DeleteByPCBoxFootCheckId(string Id);
    }
}

