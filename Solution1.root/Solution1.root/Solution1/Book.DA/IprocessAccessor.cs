//------------------------------------------------------------------------------
//
// file name：IprocessAccessor.cs
// author: peidun
// create date：2009-11-18 15:33:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.process
    /// </summary>
    public partial interface IprocessAccessor : IAccessor
    {

        IList<Model.process> SelectProcessbywf(string wfid);
        /// <summary>
        /// 获取该过程的上一过程
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        Model.process getpreprocess(string processid);
        Model.process SelectProcessbyname(string name);


        
        /// <summary>
        /// 获取该过程的下一过程
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        Model.process GetProcess(string processid);

        Model.process GetBeginProcess(string wfid);
        

        
    }
}

