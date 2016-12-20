﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IPackageTypeAccessor.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    public partial interface IPackageTypeAccessor
    {
        bool HasRows();
		
        bool HasRows(string id);
		
		Model.PackageType Get(string id);
		
		void Delete(string id);
		
		int Count();
		
		void Insert(Model.PackageType e);
		
		void Update(Model.PackageType e);
		
		IList<Model.PackageType> Select();
		
		IList<Model.PackageType> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription);
		bool HasRowsBefore(Model.PackageType e);
		
		bool HasRowsAfter(Model.PackageType e);
		
		Model.PackageType GetFirst();
		
		Model.PackageType GetLast();
		
		Model.PackageType GetPrev(Model.PackageType e);
		
		Model.PackageType GetNext(Model.PackageType e);

		bool Exists(string id);
		
		Model.PackageType GetById(string id);
		
		bool ExistsExcept(Model.PackageType e);
	}
}

