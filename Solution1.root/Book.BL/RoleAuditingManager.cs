//------------------------------------------------------------------------------
//
// file name：RoleAuditingManager.cs
// author: mayanjun
// create date：2012/10/19 12:01:57
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.RoleAuditing.
    /// </summary>
    public partial class RoleAuditingManager
    {

        /// <summary>
        /// Delete RoleAuditing by primary key.
        /// </summary>
        public void Delete(string roleAuditingId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(roleAuditingId);
        }

        /// <summary>
        /// Insert a RoleAuditing.
        /// </summary>
        public void Insert(Model.RoleAuditing roleAuditing)
        {
            //
            // todo:add other logic here
            //
            //try
            //{
            //    BL.V.BeginTransaction();
            accessor.Insert(roleAuditing);
            //    BL.V.CommitTransaction();
            //}
            //catch
            //{
            //    BL.V.RollbackTransaction();
            //    throw;
            //}
        }

        /// <summary>
        /// Update a RoleAuditing.
        /// </summary>
        public void Update(Model.RoleAuditing roleAuditing)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(roleAuditing);
        }
        /// <summary>
        /// 是否对单据有审核权限
        /// </summary>
        /// <param name="operations"></param>
        /// <param name="invoiceid"></param>
        /// <returns></returns>
        public bool IsHasAudit(Book.Model.Operators operations, string invoiceid, string TableName)
        {
            return accessor.IsHasAudit(operations, invoiceid, TableName);
        }
        public bool IsHasGiveUpAudited(Book.Model.Operators operations, string invoiceid, string TableName)
        {
            return accessor.IsHasGiveUpAudited(operations, invoiceid, TableName);
        }
        public bool IsNeedAuditByTableName(string tableName)
        {

            return accessor.IsNeedAuditByTableName(tableName);
        }
        public bool IsNeedAuditByKeyTag(string keyTag)
        {
            return accessor.IsNeedAuditByTableName(keyTag);
        }
        public Model.RoleAuditing SelectByInvoiceIdAndTable(string invoiceid, string tableName)
        {
            return accessor.SelectByInvoiceIdAndTable(invoiceid, tableName);
        }
        public bool IsLastAudit(int auditRank, string invoiceid, string tableName)
        {
            return accessor.IsLastAudit(auditRank, invoiceid, tableName);

        }
        public IList<Book.Model.RoleAuditing> GetByDate(DateTime startDate, DateTime endDate, Model.Department department, Model.Employee emp0, Model.Operators oper, bool isNoAudit, bool isHasAudit)
        {
            return accessor.GetByDate(startDate, endDate, department, emp0, oper, isNoAudit, isHasAudit);
        }
        public void DeleteByInvoiceIdAndTable(string invoiceid, string tableName)
        {
            accessor.DeleteByInvoiceIdAndTable(invoiceid, tableName);
        }
    }
}

