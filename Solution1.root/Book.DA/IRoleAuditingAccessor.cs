//------------------------------------------------------------------------------
//
// file name：IRoleAuditingAccessor.cs
// author: mayanjun
// create date：2012/10/19 12:01:58
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.RoleAuditing
    /// </summary>
    public partial interface IRoleAuditingAccessor : IAccessor
    {
        bool IsHasAudit(Book.Model.Operators operations, string invoiceid,string TableName);
        bool IsHasGiveUpAudited(Book.Model.Operators operations, string invoiceid,string TableName);
        bool IsNeedAuditByTableName(string tableName);
        bool IsNeedAuditByKeyTag(string keyTag);
        Model.RoleAuditing SelectByInvoiceIdAndTable(string invoiceid, string tableName);
        bool IsLastAudit(int auditRank, string invoiceid,string tableName);
        IList<Book.Model.RoleAuditing> GetByDate(DateTime startDate, DateTime endDate, Model.Department department, Model.Employee emp0, Model.Operators oper, bool isNoAudit, bool isHasAudit);
       void DeleteByInvoiceIdAndTable(string invoiceid, string tableName);
      
    }
}

