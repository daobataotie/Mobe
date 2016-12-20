//------------------------------------------------------------------------------
//
// file name：AtAccountSubjectAccessor.cs
// author: mayanjun
// create date：2010-11-10 11:04:51
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
    /// Data accessor of AtAccountSubject
    /// </summary>
    public partial class AtAccountSubjectAccessor : EntityAccessor, IAtAccountSubjectAccessor
    {
        //public void UpdateDataTable(Model.AtAccountSubject accounts)
        //{
        //    SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

        //    SqlDataAdapter dataAdapter = new SqlDataAdapter();

        //    dataAdapter.UpdateCommand = new SqlCommand("update AtAccountSubject set SubjectName=@SubjectName,TheLending=@TheLending,TheBalance=@TheBalance,AccountingCategoryId=@AccountingCategoryId where SubjectId=@SubjectId", conn);
        //    dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@SubjectId", SqlDbType.VarChar, 50, "SubjectId"));
        //    dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@SubjectName", SqlDbType.VarChar, 50, "SubjectName"));
        //    dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@TheLending", SqlDbType.Money, 8, "TheLending"));
        //    dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@TheBalance", SqlDbType.Money, 8, "TheBalance"));
        //    dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@AccountingCategoryId", SqlDbType.Text, 16, "AccountingCategoryId"));

        //    dataAdapter.Update(accounts);
        //}
        public IList<Book.Model.AtAccountSubject> selectById(string startid, string endid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startid", startid);
            ht.Add("endid", endid);
            return sqlmapper.QueryForList<Model.AtAccountSubject>("AtAccountSubject.selectById", ht);
        }
    }
}
