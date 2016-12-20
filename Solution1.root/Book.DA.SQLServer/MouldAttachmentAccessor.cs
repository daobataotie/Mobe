//------------------------------------------------------------------------------
//
// file name：MouldAttachmentAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:28
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of MouldAttachment
    /// </summary>
    public partial class MouldAttachmentAccessor : EntityAccessor, IMouldAttachmentAccessor
    {
        public void DeleteByMouldid(string mouldid)
        {
            sqlmapper.Delete("MouldAttachment.DeleteByMouldid", mouldid);
        }

        public IList<Model.MouldAttachment> SelectByMouldId(Model.ProductMould mould)
        {
            return sqlmapper.QueryForList<Model.MouldAttachment>("MouldAttachment.selectByMouldId", mould.MouldId);
        }
    }
}
