//------------------------------------------------------------------------------
//
// file name：PronoteProceduresAbility.cs
// author: mayanjun
// create date：2010-9-23 14:25:15
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
	/// <summary>
	/// 生产工序能力
	/// </summary>
	[Serializable]
	public partial class PronoteProceduresAbility
	{
        private IList<Model.PronoteProceduresAbilityDetail> _detail=new  List<Model.PronoteProceduresAbilityDetail>();
        public IList<Model.PronoteProceduresAbilityDetail> Details
        {
            set { this._detail = value;}
            get
            {
                return this._detail;
            }

        }
    

	}
}
