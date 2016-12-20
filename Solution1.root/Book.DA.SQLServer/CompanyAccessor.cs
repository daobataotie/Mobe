//------------------------------------------------------------------------------
//
// file name：CompanyAccessor.cs
// author: peidun
// create date：2009-09-02 上午 10:38:13
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
    /// <summary>
    /// Data accessor of Company
    /// </summary>
    public partial class CompanyAccessor : EntityAccessor, ICompanyAccessor
    {
        public bool IsExistsCompanyName(string CompanyId, string CompanyName)
        {
            Hashtable ht=new Hashtable();
            ht.Add("id",CompanyId);
            ht.Add("name",CompanyName);
            return sqlmapper.QueryForObject<bool>("Company.IsExistsCompanyName", ht);
        }
        public Model.Company SelectIsDefaultCompany()
        {
            return sqlmapper.QueryForObject<Model.Company>("Company.SelectIsDefaultCompany", null);
        }
    }
}
