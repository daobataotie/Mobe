//------------------------------------------------------------------------------
//
// file name：RoleAuditingAccessor.cs
// author: mayanjun
// create date：2012/10/19 12:01:58
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
    /// Data accessor of RoleAuditing
    /// </summary>
    public partial class RoleAuditingAccessor : EntityAccessor, IRoleAuditingAccessor
    {
        public bool IsHasAudit(Book.Model.Operators operations, string invoiceid,string TableName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("operatorsId",operations.OperatorsId);
            ht.Add("invoiceid", invoiceid);
            ht.Add("TableName", TableName);
            return sqlmapper.QueryForObject<bool>("RoleAuditing.select_IsHasAudit", ht);
        }
        public bool IsHasGiveUpAudited(Book.Model.Operators operations, string invoiceid, string TableName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("operatorsId",operations.OperatorsId);
            ht.Add("invoiceid", invoiceid);
            ht.Add("TableName", TableName);
            return sqlmapper.QueryForObject<bool>("RoleAuditing.select_IsHasGiveUpAudited", ht);
        }
        public bool IsNeedAuditByTableName(string tableName)
        {

            return sqlmapper.QueryForObject<bool>("RoleAuditing.select_IsNeedAuditByTableName", tableName);
        }
        public bool IsNeedAuditByKeyTag(string keyTag)
        {

            return sqlmapper.QueryForObject<bool>("RoleAuditing.select_IsNeedAuditByTag", keyTag);
        }
        
         public Model.RoleAuditing SelectByInvoiceIdAndTable(string invoiceid,string tableName)
        {                   
            Hashtable ht = new Hashtable();
            ht.Add("invoiceid", invoiceid);
            ht.Add("tableName", tableName);
            return sqlmapper.QueryForObject<Model.RoleAuditing>("RoleAuditing.selectByInvoiceIdAndTable", ht);
        }
         public bool IsLastAudit(int auditRank, string invoiceid,string tableName)
         {
             Hashtable ht = new Hashtable();
             ht.Add("auditRank", auditRank);
             ht.Add("invoiceid", invoiceid);
             ht.Add("tableName", tableName);
             return sqlmapper.QueryForObject<bool>("RoleAuditing.select_IsLastAudit", ht);
         }

         public IList<Book.Model.RoleAuditing> GetByDate(DateTime startDate, DateTime endDate, Model.Department department, Model.Employee emp0,Model.Operators oper, bool isNoAudit, bool isHasAudit)
         {
             SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@OperatorsId", SqlDbType.VarChar, 50), new SqlParameter("@employee0id", SqlDbType.VarChar, 50) };
             parames[0].Value = startDate;
             parames[1].Value = endDate;
             parames[2].Value = oper.OperatorsId;
             if (emp0 == null)
                 parames[3].Value = DBNull.Value;
             else
             parames[3].Value = emp0.EmployeeId;
              StringBuilder sql = new StringBuilder();
               sql.Append("SELECT  r.*,(select e.EmployeeName from Employee e where e.EmployeeId = r.Employee0Id ) as Employee0Name,(select e.EmployeeName from Employee e where e.EmployeeId = r.Employee1Id ) as Employee1Name  from RoleAuditing r where inserttime between @startdate and @enddate");
               if (emp0 != null)
                   sql.Append(" and employee0id=@employee0id");
               if (oper!= null)
                   sql.Append(" and (NextAuditRoleId IN(select RoleId from OperationRole where OperatorsId = @OperatorsId and IsHold=1) or  NextAuditRoleId is null)");
               if (!isNoAudit || !isHasAudit)
               {
                   if (isNoAudit)
                       sql.Append(" and auditstate<>3 ");
                   if (isHasAudit)
                       sql.Append(" and auditstate=3 ");
               }
            
             sql.Append(" order by inserttime desc ");
             return this.DataReaderBind<Model.RoleAuditing>(sql.ToString(), parames, CommandType.Text);
         }
        public void DeleteByInvoiceIdAndTable(string invoiceid,string tableName)
        {                   
            Hashtable ht = new Hashtable();
            ht.Add("invoiceid", invoiceid);
            ht.Add("tableName", tableName);
             sqlmapper.Delete("RoleAuditing.delete_ByInvoiceIdAndTable", ht);
        }
        
       
    }
}
