using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA
{
    public interface IInvoice00Accessor : IAccessor
    {
        /// <summary>
        /// 获取所有单据
        /// </summary>
        /// <returns></returns>
        IList<Model.Invoice00> Select();
    }
}
