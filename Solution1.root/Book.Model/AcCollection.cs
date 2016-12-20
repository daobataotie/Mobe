//------------------------------------------------------------------------------
//
// file name：AcCollection.cs
// author: mayanjun
// create date：2011-6-23 09:29:22
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 收款结算
	/// </summary>
	[Serializable]
	public partial class AcCollection
	{
        
        private System.Collections.Generic.IList<Model.AcCollectionDetail> detail;

        public System.Collections.Generic.IList<Model.AcCollectionDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
	}
}
