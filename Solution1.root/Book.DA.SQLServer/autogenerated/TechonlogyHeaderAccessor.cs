﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：TechonlogyHeaderAccessor.autogenerated.cs
// author: peidun
// create date：2009-12-7 14:57:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    public partial class TechonlogyHeaderAccessor:ITechonlogyHeaderAccessor
    {
		public Model.TechonlogyHeader Get(string id)
		{
			return this.Get<Model.TechonlogyHeader>(id);
		}
		
		public void Insert(Model.TechonlogyHeader e)
		{
			this.Insert<Model.TechonlogyHeader>(e);
		}
		
		public void Update(Model.TechonlogyHeader e)
		{
			this.Update<Model.TechonlogyHeader>(e);
		}
		
		public IList<Model.TechonlogyHeader> Select()
		{
			return this.Select<Model.TechonlogyHeader>();
		}
		
		public IList<Model.TechonlogyHeader> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.TechonlogyHeader>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.TechonlogyHeader>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.TechonlogyHeader>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.TechonlogyHeader>();
		}
		public int Count()
		{
			return this.Count<Model.TechonlogyHeader>();
		}
		public bool HasRowsBefore(Model.TechonlogyHeader e)
		{
			return sqlmapper.QueryForObject<bool>("TechonlogyHeader.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.TechonlogyHeader e)
		{
			return sqlmapper.QueryForObject<bool>("TechonlogyHeader.has_rows_after", e);
		}
		public Model.TechonlogyHeader GetFirst()
		{
			return sqlmapper.QueryForObject<Model.TechonlogyHeader>("TechonlogyHeader.get_first", null);
		}
		public Model.TechonlogyHeader GetLast()
		{
			return sqlmapper.QueryForObject<Model.TechonlogyHeader>("TechonlogyHeader.get_last", null);
		}
		public Model.TechonlogyHeader GetNext(Model.TechonlogyHeader e)
		{
			return sqlmapper.QueryForObject<Model.TechonlogyHeader>("TechonlogyHeader.get_next", e);
		}
		public Model.TechonlogyHeader GetPrev(Model.TechonlogyHeader e)
		{
			return sqlmapper.QueryForObject<Model.TechonlogyHeader>("TechonlogyHeader.get_prev", e);
		}

        public bool Exists(string id)
        {
            return sqlmapper.QueryForObject<bool>("TechonlogyHeader.exists", id);
        }

        public Model.TechonlogyHeader GetById(string id)
        {
            return sqlmapper.QueryForObject<Model.TechonlogyHeader>("TechonlogyHeader.get_by_id", id);
        }

        public bool ExistsExcept(Model.TechonlogyHeader e)
        {
            Hashtable paras = new Hashtable();
            paras.Add("newId", e.Id);
            paras.Add("oldId", Get(e.TechonlogyHeaderId).Id);
            return sqlmapper.QueryForObject<bool>("TechonlogyHeader.existsexcept", paras);
        }

        //#region ITechonlogyHeaderAccessor 成员


        //public bool Exists(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Book.Model.TechonlogyHeader GetById(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool ExistsExcept(Book.Model.TechonlogyHeader e)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region ITechonlogyHeaderAccessor 成员

        //bool ITechonlogyHeaderAccessor.HasRows()
        //{
        //    throw new NotImplementedException();
        //}

        //bool ITechonlogyHeaderAccessor.HasRows(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //Book.Model.TechonlogyHeader ITechonlogyHeaderAccessor.Get(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //void ITechonlogyHeaderAccessor.Delete(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //int ITechonlogyHeaderAccessor.Count()
        //{
        //    throw new NotImplementedException();
        //}

        //void ITechonlogyHeaderAccessor.Insert(Book.Model.TechonlogyHeader e)
        //{
        //    throw new NotImplementedException();
        //}

        //void ITechonlogyHeaderAccessor.Update(Book.Model.TechonlogyHeader e)
        //{
        //    throw new NotImplementedException();
        //}

        //IList<Book.Model.TechonlogyHeader> ITechonlogyHeaderAccessor.Select()
        //{
        //    throw new NotImplementedException();
        //}

        //IList<Book.Model.TechonlogyHeader> ITechonlogyHeaderAccessor.Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
        //{
        //    throw new NotImplementedException();
        //}

        //bool ITechonlogyHeaderAccessor.HasRowsBefore(Book.Model.TechonlogyHeader e)
        //{
        //    throw new NotImplementedException();
        //}

        //bool ITechonlogyHeaderAccessor.HasRowsAfter(Book.Model.TechonlogyHeader e)
        //{
        //    throw new NotImplementedException();
        //}

        //Book.Model.TechonlogyHeader ITechonlogyHeaderAccessor.GetFirst()
        //{
        //    throw new NotImplementedException();
        //}

        //Book.Model.TechonlogyHeader ITechonlogyHeaderAccessor.GetLast()
        //{
        //    throw new NotImplementedException();
        //}

        //Book.Model.TechonlogyHeader ITechonlogyHeaderAccessor.GetPrev(Book.Model.TechonlogyHeader e)
        //{
        //    throw new NotImplementedException();
        //}

        //Book.Model.TechonlogyHeader ITechonlogyHeaderAccessor.GetNext(Book.Model.TechonlogyHeader e)
        //{
        //    throw new NotImplementedException();
        //}

        //bool ITechonlogyHeaderAccessor.Exists(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //Book.Model.TechonlogyHeader ITechonlogyHeaderAccessor.GetById(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //bool ITechonlogyHeaderAccessor.ExistsExcept(Book.Model.TechonlogyHeader e)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion
    }
}
