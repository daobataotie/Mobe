//------------------------------------------------------------------------------
//
// file name：AtBankTransferManager.cs
// author: mayanjun
// create date：2010-11-24 15:55:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtBankTransfer.
    /// </summary>
    public partial class AtBankTransferManager
    {
		
		/// <summary>
		/// Delete AtBankTransfer by primary key.
		/// </summary>
		public void Delete(string transferId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(transferId);
		}

		/// <summary>
		/// Insert a AtBankTransfer.
		/// </summary>
        public void Insert(Model.AtBankTransfer atBankTransfer)
        {
			//
			// todo:add other logic here
			//
            Validate(atBankTransfer);
            atBankTransfer.TransferId = Guid.NewGuid().ToString();
            atBankTransfer.InsertTime = DateTime.Now;
            accessor.Insert(atBankTransfer);
        }
		
		/// <summary>
		/// Update a AtBankTransfer.
		/// </summary>
        public void Update(Model.AtBankTransfer atBankTransfer)
        {
			//
			// todo: add other logic here.
			//
            Validate(atBankTransfer);
            atBankTransfer.UpdateTime = DateTime.Now;
            accessor.Update(atBankTransfer);
        }
        private void Validate(Model.AtBankTransfer atBankTransfer)
        {
            if (string.IsNullOrEmpty(atBankTransfer.Id))
            {
                throw new Helper.RequireValueException(Model.AtBankTransfer.PRO_Id);
            }
            if (string.IsNullOrEmpty(atBankTransfer.WithBankId))
            {
                throw new Helper.RequireValueException(Model.AtBankTransfer.PRO_WithBankId);
            }
            if (string.IsNullOrEmpty(atBankTransfer.IntoBankId))
            {
                throw new Helper.RequireValueException(Model.AtBankTransfer.PRO_IntoBankId);
            }
        }
    }
}

