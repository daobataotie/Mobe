//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactMaterial.cs
// author: mayanjun
// create date：2010-12-2 16:11:04
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 外包合同配料
	/// </summary>
	[Serializable]
	public partial class ProduceOtherCompactMaterial
	{
        private bool _checked = false;
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
	}
}
