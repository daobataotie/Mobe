//------------------------------------------------------------------------------
//
// file name：Procedures.cs
// author: peidun
// create date：2009-12-8 10:55:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class Procedures
	{
       
        public override string ToString()
        {
            return this.Procedurename;
        }

	}
}
