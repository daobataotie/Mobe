//------------------------------------------------------------------------------
//
// file name:RoleOperation.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 权限
    /// </summary>
    [Serializable]
    public partial class RoleOperation
    {
        public override bool Equals(object obj)
        {
            if (obj is RoleOperation)
            {
                return (obj as Model.RoleOperation)._roleOperationId == this._roleOperationId ? true : false;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool? AllSelect
        {
            get;
            set;
        }

    }
}
