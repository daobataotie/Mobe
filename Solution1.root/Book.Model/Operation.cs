//------------------------------------------------------------------------------
//
// file name:Operation.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 操作
	/// </summary>
	[Serializable]
    public partial class Operation : IComparable
	{
        public override string ToString()
        {
            return this._operationName;
        }
        public int CompareTo(object obj)
        {
            if (obj is Operation)
            {
                Operation operation = (Operation)obj;
                return operation._operationName.CompareTo(this._operationName);
            }

            throw new ArgumentException("obj");
        }

        public override bool Equals(object obj)
        {
            if (obj is Operation)
            {
                return (obj as Model.Operation)._operationId == this._operationId ? true : false;
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
	}
}
