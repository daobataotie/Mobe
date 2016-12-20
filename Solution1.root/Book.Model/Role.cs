//------------------------------------------------------------------------------
//
// file name:Role.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    public partial class Role : IComparable
    {
        public override string ToString()
        {
            return this._roleName;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is Role)
            {
                Role role = (Role)obj;
                return role.RoleName.CompareTo(this.RoleName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is Invoice)
            {
                return (obj as Model.Role)._roleId == this._roleId ? true : false;
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

        private bool _Checked;

        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }
    }
}
