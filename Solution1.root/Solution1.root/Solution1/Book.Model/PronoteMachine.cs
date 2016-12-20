//------------------------------------------------------------------------------
//
// file name：PronoteMachine.cs
// author: mayanjun
// create date：2010-9-16 9:27:27
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 生产机器
	/// </summary>
	[Serializable]
	public partial class PronoteMachine
	{
        public override string ToString()
        {
            return this._pronoteMachineName;
        }
	}
}
