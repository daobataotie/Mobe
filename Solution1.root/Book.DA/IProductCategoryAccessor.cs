﻿//------------------------------------------------------------------------------
//
// file name：IProductCategoryAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductCategory
    /// </summary>
    public partial interface IProductCategoryAccessor : IEntityAccessor
    {
        bool ExistsPrimary(string productCategoryId);
        bool ExistsName(string productCategoryName, string ProductCategoryId);

        IList<string> SelectALLName();

        DataTable SelectDTByFilter(string filter);

        IList<Model.ProductCategory> SelectListByFilter(string CategoryLevel, string ProductCategoryParentId);

        /// <summary>
        /// 2017-12-5 ：真正的查询所有，"Select"只查询Level为1的
        /// </summary>
        /// <returns></returns>
        IList<Model.ProductCategory> SelectAll();
    }
}

