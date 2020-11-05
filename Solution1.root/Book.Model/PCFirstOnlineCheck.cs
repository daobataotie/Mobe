//------------------------------------------------------------------------------
//
// file name：PCFirstOnlineCheck.cs
// author: mayanjun
// create date：2020/10/30 22:05:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
	/// <summary>
	/// 首件上线检查表(新)
	/// </summary>
	[Serializable]
	public partial class PCFirstOnlineCheck
	{
        IList<Model.PCFirstOnlineCheckDetail> _detail = new List<Model.PCFirstOnlineCheckDetail>();

        public IList<Model.PCFirstOnlineCheckDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }

	}
}