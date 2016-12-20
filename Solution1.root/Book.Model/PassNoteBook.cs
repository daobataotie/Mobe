//------------------------------------------------------------------------------
//
// file name：PassNoteBook.cs
// author: mayanjun
// create date：2013-4-3 17:47:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 通关手册
    /// </summary>
    [Serializable]
    public partial class PassNoteBook
    {
        IList<Model.PassNoteBookDetail> detialMaterial = new List<Model.PassNoteBookDetail>();

        /// <summary>
        /// 通关手册详细（料件）
        /// </summary>
        public IList<Model.PassNoteBookDetail> DetialMaterial
        {
            get { return detialMaterial; }
            set { detialMaterial = value; }
        }

        IList<Model.PassNoteBookDetail> detailProduct = new List<Model.PassNoteBookDetail>();

        /// <summary>
        ///  通关手册详细（产品）
        /// </summary>
        public IList<Model.PassNoteBookDetail> DetailProduct
        {
            get { return detailProduct; }
            set { detailProduct = value; }
        }
    }
}
