﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCBoxFootCheckDetailManager.autogenerated.cs
// author: mayanjun
// create date：2013-08-16 10:26:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PCBoxFootCheckDetailManager
    {
        ///<summary>
        /// Data accessor of dbo.PCBoxFootCheckDetail
        ///</summary>
        private static readonly DA.IPCBoxFootCheckDetailAccessor accessor = (DA.IPCBoxFootCheckDetailAccessor)Accessors.Get("PCBoxFootCheckDetailAccessor");

        /// <summary>
        /// Select by primary key.
        /// </summary>		
        public Model.PCBoxFootCheckDetail Get(string pCBoxFootCheckDetailId)
        {
            return accessor.Get(pCBoxFootCheckDetailId);
        }

        public bool HasRows(string pCBoxFootCheckDetailId)
        {
            return accessor.HasRows(pCBoxFootCheckDetailId);
        }

        public bool HasRows()
        {
            return accessor.HasRows();
        }


        /// <summary>
        /// Select all.
        /// </summary>
        public IList<Model.PCBoxFootCheckDetail> Select()
        {
            return accessor.Select();
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int Count()
        {
            return accessor.Count();
        }

        /// <summary>
        /// 获取指定状态、指定分页，并按指定要求排序的记录
        /// </summary>
        public IList<Model.PCBoxFootCheckDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
        {
            return accessor.Select(orderDescription, pagingDescription);
        }
        public bool ExistsPrimary(string id)
        {
            return accessor.ExistsPrimary(id);
        }


        public IList<Book.Model.PCBoxFootCheckDetail> SelectByPCBoxFootCheckId(string id)
        {
            return accessor.SelectByPCBoxFootCheckId(id);
        }
    }
}
