//------------------------------------------------------------------------------
//
// file name:Depot.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 库房
	/// </summary>
	[Serializable]
	public partial class Depot : IComparable
	{
        private System.Collections.Generic.IList<DepotPosition> details;

        public System.Collections.Generic.IList<DepotPosition> Details
        {
            get { return details; }
            set { details = value; }
        }
        public override string ToString()
        {
            return _depotName;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is Depot)
            {
                Depot depot = (Depot)obj;
                return depot.DepotName.CompareTo(this.DepotName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is Depot)
            {
                return (obj as Model.Depot)._depotId == this._depotId ? true : false;
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
