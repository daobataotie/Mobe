//------------------------------------------------------------------------------
//
// file name：OperatorsManager.cs
// author: peidun
// create date：2009-09-09  下午 04:08:30
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Operators.
    /// </summary>
    public partial class OperatorsManager : BaseManager
    {

        /// <summary>
        /// Delete Operators by primary key.
        /// </summary>
        public void Delete(Model.Operators operators)
        {
            BL.OperationRoleManager operationRoleManager = new Book.BL.OperationRoleManager();

            //
            // todo:add other logic here
            //
            operationRoleManager.Delete(operators);
            accessor.Delete(operators.OperatorsId);
        }

        /// <summary>
        /// Insert a Operators.
        /// </summary>
        public void Insert(Model.Operators operators)
        {
            //Model.RoleOperation roleOperation;
            //BL.RoleOperationManager roleOperationManager = new Book.BL.RoleOperationManager();
            //
            // todo:add other logic here
            //
            //添加至操作员表
            Validate(operators);
            operators.InsertTime = DateTime.Now;
            operators.OperatorsId = Guid.NewGuid().ToString();
            accessor.Insert(operators);
            //添加至权限表

        }

        /// <summary>
        /// Update a Operators.
        /// </summary>
        public void Update(Model.Operators operators)
        {
            BL.OperationRoleManager operationRoleManager = new Book.BL.OperationRoleManager();
            //
            // todo: add other logic here.
            //
            operators.UpdateTime = DateTime.Now;
            accessor.Update(operators);
            //operationRoleManager.Delete(operators);
        }

        public IList<Book.Model.Operators> SelectOperators()
        {
            return accessor.SelectOperators();
        }

        public Book.Model.Operators GetByOperatorName(string operatorName)
        {
            return accessor.GetByOperatorName(operatorName);
        }
        private void Validate(Model.Operators operators)
        {
            if (string.IsNullOrEmpty(operators.OperatorName))
            {
                throw new Helper.RequireValueException(Model.Operators.PROPERTY_OPERATORNAME);
            }

        }

        public IList<Book.Model.Operators> SelectOrderByName()
        {
            return accessor.SelectOrderByName();
        }
    }
}