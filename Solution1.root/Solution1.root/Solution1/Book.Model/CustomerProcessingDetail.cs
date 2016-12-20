//------------------------------------------------------------------------------
//
// file name：CustomerProcessingDetail.cs
// author: mayanjun
// create date：2010-7-30 19:31:58
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class CustomerProcessingDetail
	{
        public string _content;

        public string Content
        {
            get
            {
                return Process == null ? "" : Process.Content;
            }
            set
            {
                _content = value;
            }
        }
        private string _productname;
        public string ProductName
        {
            get
            {
                return this._productname; 
            }
            set
            {
                this._productname = value;
            }
        }

	}
}
