//------------------------------------------------------------------------------
//
// file name：Department.cs
// author: peidun
// create date：2008-11-29 11:07:05
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class Department : IComparable
    {
        public override string ToString()
        {
            return this.DepartmentName;
        }

        public override bool Equals(object obj)
        {
            if (obj is Department)
            {
                return (obj as Model.Department)._departmentId == this._departmentId ? true : false;
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

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            if (obj is Department)
            {
                Department department = (Department)obj;
                return department._departmentName.CompareTo(this._departmentName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public bool IsChecked { get; set; }
    }
}
