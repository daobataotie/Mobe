//------------------------------------------------------------------------------
//
// file name：BGHandbookDepotIn.cs
// author: mayanjun
// create date：2013/12/19 18:37:48
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class BGHandbookDepotIn
	{
        System.Collections.Generic.IList<Model.BGHandbookDepotInDetail> detail = new System.Collections.Generic.List<Model.BGHandbookDepotInDetail>();

        public System.Collections.Generic.IList<Model.BGHandbookDepotInDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
	}
}
