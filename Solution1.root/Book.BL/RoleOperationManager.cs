//------------------------------------------------------------------------------
//
// file name：RoleOperationManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.RoleOperation.
    /// </summary>
    public partial class RoleOperationManager : BaseManager
    {

        /// <summary>
        /// Delete RoleOperation by primary key.
        /// </summary>
        public void Delete(string roleOperationId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(roleOperationId);
        }

        /// <summary>
        /// Insert a RoleOperation.
        /// </summary>
        public void Insert(Model.RoleOperation roleOperation)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(roleOperation);
        }

        private void Validate(Model.RoleOperation roleOperation)
        {
            if (string.IsNullOrEmpty(roleOperation.RoleOperationId))
            {
                throw new Helper.RequireValueException(Model.RoleOperation.PRO_RoleId);
            }
        }

        /// <summary>
        /// Update a RoleOperation.
        /// </summary>
        public void Update(Model.RoleOperation roleOperation)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(roleOperation);
        }
        public IList<Model.RoleOperation> Select(Model.Role role)
        {
            return accessor.Select(role);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return base.GetInvoiceKind();
        //}

        //protected override string GetSettingId()
        //{
        //    return base.GetSettingId();
        //}
        /// <summary>
        /// 根据父查询最子节点dataset
        /// </summary>
        /// <returns></returns>
        public DataSet SelectByType(string parenId, string roleId)
        {
            return accessor.SelectByType(parenId, roleId);
        }
        public int UpdateTable(DataSet ds, string roleid)
        {
            return accessor.UpdateTable(ds, roleid);
        }
        /// <summary>
        /// 根据操作员查询 拥有查询功能窗体
        /// </summary>
        /// <param name="operations"></param>
        /// <returns></returns>
        public IList<Book.Model.RoleOperation> SelectIsSearch(Book.Model.Operators operations)
        {
            return accessor.SelectIsSearch(operations);
        }
        public IList<Book.Model.RoleOperation> SelectbyOperatorsKeyTag(Book.Model.Operators operations, string keytag)
        {
            return accessor.SelectbyOperatorsKeyTag(operations, keytag);
        }
        public string GetbyTable(string tableName)
        {
            return accessor.GetbyTable(tableName);
        }

        public void DeleteByRoleId(string RoleId)
        {
            accessor.DeleteByRoleId(RoleId);
        }
    }
}

