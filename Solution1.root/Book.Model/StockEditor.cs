//------------------------------------------------------------------------------
//
// file name：StockEditor.cs
// author: mayanjun
// create date：2010-11-4 11:02:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
	/// <summary>
	/// 库存盘点录入
	/// </summary>
	[Serializable]
	public partial class StockEditor
	{
        private IList<Model.StockEditorDetal> details = new List<Model.StockEditorDetal>();

        public IList<Model.StockEditorDetal> Details
        {
            get { return details; }
            set { details = value; }
        }

        private IList<Model.StockEditorDetal> productPositionNums = new List<Model.StockEditorDetal>();

        public IList<Model.StockEditorDetal> ProductPositionNums
        {
            get { return productPositionNums; }
            set { productPositionNums = value; }
        }
	}
}
