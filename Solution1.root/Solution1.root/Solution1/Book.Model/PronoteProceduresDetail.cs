//------------------------------------------------------------------------------
//
// file name：PronoteProceduresDetail.cs
// author: mayanjun
// create date：2010-9-16 15:59:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
	/// <summary>
	/// 生产通知工艺流程
	/// </summary>
	[Serializable]
	public partial class PronoteProceduresDetail
	{
        private bool isChecked;

        public bool IsChecked
        {
            get { return  isChecked; }
            set { isChecked = value; }
        
        }
        private string  _machine;

         public string  Machine
        {
            get { return _machine; }
            set { _machine = value; }
        }
         public static readonly string PRO_Machine ="Machine";
        
	}
}
