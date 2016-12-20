﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IwfrecordlogAccessor.autogenerated.cs
// author: peidun
// create date：2009-12-11 14:53:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IwfrecordlogAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.wfrecordlog Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.wfrecordlog e);
		
		void Update(Model.wfrecordlog e);
		
		IList<Model.wfrecordlog> Select();
		
		IList<Model.wfrecordlog> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		bool HasRowsBefore(Model.wfrecordlog e);
		
		bool HasRowsAfter(Model.wfrecordlog e);
		
		Model.wfrecordlog GetFirst();
		
		Model.wfrecordlog GetLast();
		
		Model.wfrecordlog GetPrev(Model.wfrecordlog e);
		
		Model.wfrecordlog GetNext(Model.wfrecordlog e);

	}
}

