//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitDetail.cs
// author: peidun
// create date：2010-1-6 10:26:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 生产退料详细
	/// </summary>
	[Serializable]
	public partial class ProduceMaterialExitDetail
	{
        private bool isChecked=false;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }
	}
}
