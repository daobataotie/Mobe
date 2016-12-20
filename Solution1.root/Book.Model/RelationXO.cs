//------------------------------------------------------------------------------
//
// file name：RelationXO.cs
// author: mayanjun
// create date：2015/4/19 下午 08:06:08
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 入料检验单关联客户订单
	/// </summary>
	[Serializable]
	public partial class RelationXO
	{
        System.Collections.Generic.IList<Model.RelationXODetail> _detail = new System.Collections.Generic.List<Model.RelationXODetail>();

        public System.Collections.Generic.IList<Model.RelationXODetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
	}
}