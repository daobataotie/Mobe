using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Invoices
{
    public interface IChoose
    {
        /// <summary>
        /// 显示编号
        /// </summary>
        string ButtonText { get; }
        
        /// <summary>
        /// 显示名称
        /// </summary>        
        string LableText { get; }

        /// <summary>
        /// 
        /// </summary>
        object EditValue { set;get; }

        void MyClick(ref ChooseItem item);

        void MyLeave(ref ChooseItem item);

        //void myLoad(ref ChooseItem item);
    }
}
